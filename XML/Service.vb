Imports System.Xml
Imports System.Xml.Serialization

<DebuggerStepThrough>
<Serializable()> Public Class Service
    Inherits APIConnectorBase

    <XmlElement("serviceType")> Public Property ServiceType As String
    <XmlElement("serviceId")> Public Property ServiceId As String
    <XmlElement("controlURL")> Public Property ControlURL As String
    <XmlElement("eventSubURL")> Public Property EventSubURL As String
    <XmlElement("SCPDURL")> Public Property SCPDURL As String

    <XmlIgnore> Private Property SCPD As ServiceControlProtocolDefinition
    <XmlIgnore> Private Property XML As Serializer
    <XmlIgnore> Private Property Client As WebFunctions

    <XmlIgnore> Friend ReadOnly Property Initialized As Boolean
        Get
            Return SCPD IsNot Nothing
        End Get
    End Property

    <XmlIgnore> Private Property S As XNamespace = "http://schemas.xmlsoap.org/soap/envelope/"

    Friend Sub Init(XML As Serializer, Client As WebFunctions)
        _XML = XML
        _Client = Client
    End Sub

    ''' <summary>
    ''' Führt die gewählte Action dieses Service mit den übergebenen Argumenten aus. 
    ''' </summary>
    ''' <param name="[Action]">Auszuführende Action</param>
    ''' <param name="InputArguments">Daten, die an die Fritz!Box als Argumente hochgeladen werden sollen.</param>
    ''' <param name="http">Klasse für den http-Transfer</param>
    ''' <param name="NetworkCredential">Daten zur Anmeldung auf der Fritz!Box</param>
    Friend Function StartAction(ActionName As String, InputArguments As Dictionary(Of String, String), Token As String) As Dictionary(Of String, String)
        Dim ReturnXMLDoc As New XmlDocument
        Dim ResponseData As New Dictionary(Of String, String)
        Dim Response As String = String.Empty

        If SCPD Is Nothing Then
            ' Wenn ServiceControlProtocolDefinition noch nicht geladen wurde, dann lade sie von der Fritz!Box
            If Not XML.Deserialize($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{SCPDURL}", True, SCPD) Then
                ' Fehlerfall
                SendLog(LogLevel.Error, New Exception($"ServiceControlProtocolDefinition nicht geladen."))
            End If
        End If

        Dim Action = SCPD?.ActionList.Find(Function(A) A.Name = ActionName)

        With ResponseData
            If Action IsNot Nothing Then

                Response = Client.PostStringWebClient($"{Uri.UriSchemeHttps}://{FritzBoxTR64.FBoxIPAdresse}:{49443}{ControlURL}",
                                                      GetRequestXML(Action, InputArguments, Token),
                                                      $"""{ServiceType}#{Action.Name}""")

                If Response.IsNotStringNothingOrEmpty Then
                    ' Ermittle das Dictionary zur Rückgabe
                    ResponseData = GetResponseDictionary(Action, Response)
                Else
                    ' Fehlerfall: Response ist Nothing
                    .Add("Error", Response)
                End If
            Else
                .Add("Error", "Action is not set")
            End If
        End With

        Return ResponseData
    End Function

    ''' <summary>
    ''' Erstellt den XML-Request für die jeweilige Action 
    ''' </summary>
    ''' <param name="Action">Die <paramref name="Action"/>, die ausgeführt werden soll.</param>
    ''' <param name="InputValues">Die Daten, welche müt übergeben werden sollen.</param>
    ''' <param name="Token">Authentifizierungs-Token</param>
    Private Function GetRequestXML(Action As Action, InputValues As Dictionary(Of String, String), Token As String) As String

        Dim u As XNamespace = ServiceType
        Dim avm As XNamespace = "avm.de"


        With New XDocument(New XDeclaration("1.0", "utf-8", "yes"),
                           New XElement(S + "Envelope",
                                        New XAttribute(XNamespace.Xmlns + "s", S),
                                        If(Token.IsStringNothingOrEmpty, Nothing, New XElement(S + "Header",
                                                     New XElement(avm + "token", New XAttribute(XNamespace.Xmlns + "avm", avm),
                                                                                 New XAttribute(S + "mustUnderstand", "1"),
                                                                                 Token))),
                                        New XAttribute(S + "encodingStyle", "http://schemas.xmlsoap.org/soap/encoding/"),
                                        New XElement(S + "Body",
                                                     New XElement(u + Action.Name,
                                                                  New XAttribute(XNamespace.Xmlns + "u", u),
                                                                  InputValues?.Select(Function(X) New XElement(X.Key) With {.Value = X.Value})))))
            Return .ToString
        End With
    End Function


    ''' <summary>
    ''' Erzeugt aus der Antwort der Fritz!Box ein <see cref="Dictionary(Of TKey, TValue)"/>. 
    ''' Hierzu wird die Liste aller möglichen ausgehenden Argumente (direction=out) der jeweiligen <see cref="Action"/> verarbeitet.
    ''' </summary>
    ''' <param name="Action">Zugehörige Action</param>
    ''' <param name="Response">Antwort der Fritz!Box.</param>
    Private Function GetResponseDictionary(Action As Action, Response As String) As Dictionary(Of String, String)
        With XDocument.Parse(Response)

            If .Descendants.Where(Function(E) E.Name.Equals(S + "Fault")).Any Then
                With .Descendants.Where(Function(E) E.Name.LocalName.Equals("UPnPError")).First
                    ' Fehler auslesen
                    Return New Dictionary(Of String, String) From {{"errorCode", .Descendants(.Name.Namespace + "errorCode").First.Value},
                                                                   {"errorDescription", .Descendants(.Name.Namespace + "errorDescription").First.Value}}
                End With

            Else
                Return Action.ArgumentList.Where(Function(A) A.Direction = "out").ToDictionary(Function(k) k.Name,
                                                                                               Function(v) .Descendants(v.Name).First.Value)
            End If

        End With
    End Function
End Class

