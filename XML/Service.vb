Imports System.Net
Imports System.Xml
Imports System.Xml.Serialization

<DebuggerStepThrough>
<Serializable()> Public Class Service
    Inherits LogBase

    <XmlElement("serviceType")> Public Property ServiceType As String
    <XmlElement("serviceId")> Public Property ServiceId As String
    <XmlElement("controlURL")> Public Property ControlURL As String
    <XmlElement("eventSubURL")> Public Property EventSubURL As String
    <XmlElement("SCPDURL")> Public Property SCPDURL As String

    <XmlIgnore> Friend Property SCPD As ServiceControlProtocolDefinition
    <XmlIgnore> Friend Property FBoxIPAdresse As String
    <XmlIgnore> Friend Property XML As Serializer
    <XmlIgnore> Friend Property PushStatus As Action(Of LogMessage)

    Friend Function GetActionByName(ActionName As String) As Action
        Return SCPD?.ActionList.Find(Function(Action) Action.Name = ActionName)
    End Function

    ''' <summary>
    ''' Prüft, ob die geforderte Action mit dem <paramref name="ActionName"/> existiert.
    ''' </summary>
    ''' <param name="ActionName">Name der auszuführenden Action.</param>
    ''' <returns>Boolean</returns>
    Friend Function ActionExists(ActionName As String) As Boolean

        ' Wenn ServiceControlProtocolDefinition noch nicht geladen wurde, dann lade sie von der Fritz!Box
        If SCPD Is Nothing Then
            ' Wenn keine IPAddresse vorhanden ist, was eigentlich nicht möglich ist, dann wirf einen Fehler aus.
            If FBoxIPAdresse.IsStringNothingOrEmpty Then
                PushStatus?.Invoke(CreateLog(LogLevel.Error, New Exception($"Action '{ActionName}': IP Adresse nicht vorhanden.")))
            Else
                If Not XML.Deserialize($"{Uri.UriSchemeHttp}://{FBoxIPAdresse}:{DfltTR064Port}{SCPDURL}", True, SCPD) Then
                    ' Fehlerfall
                    PushStatus?.Invoke(CreateLog(LogLevel.Error, New Exception($"Action '{ActionName}': ServiceControlProtocolDefinition nicht geladen.")))
                End If
            End If
        End If

        Return SCPD IsNot Nothing AndAlso SCPD.ActionList.Exists(Function(Action) Action.Name = ActionName)

    End Function

    ''' <summary>
    ''' Prüft, ob die übergebenen Argumente zu der jeweiligen Action passen.
    ''' </summary>
    Friend Function CheckInput(ActionName As String, InputData As Dictionary(Of String, String)) As Boolean
        CheckInput = False
        Dim ActionInputData As Dictionary(Of String, String) = GetActionByName(ActionName).ArgumentList.Where(Function(A) A.Direction = ArgumentDirection.IN).ToDictionary(Function(k) k.Name, Function(v) String.Empty)
        If InputData Is Nothing Then
            Return ActionInputData.Count.IsZero
        Else
            ' Prüfe Anzahl der zu übergebenden Daten
            If ActionInputData.Count.AreEqual(InputData.Count) Then
                CheckInput = True
                For Each submitItem In ActionInputData
                    If Not InputData.ContainsKey(submitItem.Key) Then Return False
                Next
            End If

        End If
        ActionInputData.Clear()
    End Function

    Friend Function StartAction([Action] As Action, InputArguments As Dictionary(Of String, String), http As TR064HttpBasics, NetworkCredential As NetworkCredential) As Dictionary(Of String, String)
        Dim ReturnXMLDoc As New XmlDocument
        Dim ResponseData As New Dictionary(Of String, String)
        Dim Response As String = String.Empty

        With ResponseData

            If FBoxIPAdresse.IsStringNothingOrEmpty Then
                ' Wenn keine IPAddresse vorhanden ist, was eigentlich nicht möglich ist, dann wirf einen Fehler aus.
                PushStatus?.Invoke(CreateLog(LogLevel.Error, New Exception($"Action '{[Action]}': IP Adresse nicht vorhanden.")))
                .Add("Error", $"Action '{[Action]}': IP Adresse nicht vorhanden.")
            Else
                ' Header festlegen
                Dim TR064Header As New WebHeaderCollection From {{HttpRequestHeader.ContentType, TR064ContentType},
                                                                 {HttpRequestHeader.UserAgent, TR064UserAgent},
                                                                 {"SOAPACTION", $"""{ServiceType}#{Action.Name}"""}}

                If http.UploadData(New UriBuilder(Uri.UriSchemeHttps, FBoxIPAdresse, DfltTR064PortSSL, ControlURL).Uri,
                                   GetRequest(Action, InputArguments).InnerXml,
                                   NetworkCredential,
                                   Response,
                                   TR064Header) Then

                    PushStatus?.Invoke(CreateLog(LogLevel.Trace, $"{Action.Name}: {Response} "))

                    Try
                        ReturnXMLDoc.LoadXml(Response)
                    Catch ex As XmlException
                        ' Fehlerfall
                        .Add("Error", Response)
                        PushStatus?.Invoke(CreateLog(LogLevel.Error, Response, ex))

                    End Try

                    If ReturnXMLDoc.InnerXml.IsNotStringNothingOrEmpty Then
                        ResponseData = Action.ArgumentList.Where(Function(A) A.Direction = ArgumentDirection.OUT).ToDictionary(Function(k) k.Name, Function(v) ReturnXMLDoc.GetElementsByTagName(v.Name).Item(0).InnerText)
                        ' Erzeuge eine Debug-Message
                        PushStatus?.Invoke(CreateLog(LogLevel.Debug, ResponseData.SerializeDictionary(Action.Name)))

                    End If

                Else
                    ' Fehlerfall
                    .Add("Error", Response)
                End If

            End If
        End With

        Return ResponseData
    End Function

    ''' <summary>
    ''' Erstellt den XML-Request für die jeweilige Action 
    ''' </summary>
    ''' <param name="Action">Die <paramref name="Action"/>, die ausgeführt werden soll.</param>
    ''' <param name="InputValues">Die Daten, welche müt übergeben werden sollen.</param>
    Private Function GetRequest(Action As Action, InputValues As Dictionary(Of String, String)) As XmlDocument

        GetRequest = New XmlDocument

        With GetRequest
            ' XML-Schemata hinzufügen
            .Schemas.Add(DfltSOAPRequestSchema)

            ' XML Deklaration hinzufügen
            .AppendChild(.CreateXmlDeclaration("1.0", "utf-8", ""))

            ' XML-RootElement "Envelope" generieren
            With .AppendChild(.CreateElement("s", "Envelope", DfltTR064RequestNameSpaceEnvelope))
                ' Das Attribut "encodingStyle" dem XML-Root-Element hinzufügen
                With .Attributes.Append(GetRequest.CreateAttribute("s", "encodingStyle", DfltTR064RequestNameSpaceEnvelope))
                    ' Den Wert des Attributes "encodingStyle" setzen
                    .Value = DfltTR064RequestNameSpaceEncoding
                End With

                ' XML-BodyElement "Body" generieren und dem XML-RootElement anhängen
                With .AppendChild(GetRequest.CreateElement("s", "Body", DfltTR064RequestNameSpaceEnvelope))

                    ' XML-Element mit dem namen der Action generieren und dem XML-BodyElement anhängen
                    With .AppendChild(GetRequest.CreateElement("u", Action.Name, ServiceType))

                        ' Die zu übergebenden Daten generieren, falls welche übergeben werden sollen
                        If InputValues IsNot Nothing Then
                            ' Schleife durch jedes Wertepaar
                            For Each submitItem In InputValues

                                ' XML-Element mit dem namen des Inputwertes generieren und dem XML-Action Element anhängen
                                With .AppendChild(GetRequest.CreateElement("u", submitItem.Key, ServiceType))
                                    .InnerText = submitItem.Value?.ToString
                                End With ' InputValue XML Element
                            Next
                        End If

                    End With ' XML-ActionElement 

                End With ' XML-BodyElement 

            End With ' XML-RootElement 

            PushStatus?.Invoke(CreateLog(LogLevel.Debug, $"Request: { .InnerXml}"))

        End With ' XML Document GetRequest

    End Function

End Class
