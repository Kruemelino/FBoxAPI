Imports System.IO
Imports System.Text
Imports System.Xml
Imports System.Xml.Serialization
Imports System.Xml.Xsl

'<DebuggerStepThrough()>
Friend Class Serializer
    Inherits LogBase

    Private Property Client As WebFunctions

    Public Sub New(http As WebFunctions)
        Client = http
    End Sub

#Region "XML"
#Region "CheckXMLData"
    ''' <summary>
    ''' Überprüft, ob die einzulesenden Daten überhaupt eine XML sind. Dazu wird versucht die XML Daten einzulesen. 
    ''' Wenn die Daten eingelesen werden können, werden sie als <see cref="XmlDocument"/> zur weiteren Verarbeitung in <paramref name="xDoc"/> bereitgestellt.
    ''' </summary>
    ''' <param name="InputData">Die einzulesenden Daten</param>
    ''' <param name="IsPfad">Angabe, ob ein Dateipfad oder XML-Daten geprüft werden sollen.</param>
    ''' <param name="xDoc">XML-Daten zur weiteren Verwendung</param>
    ''' <returns>Boolean</returns>
    Private Function CheckXMLData(InputData As String, IsPfad As Boolean, ByRef xDoc As XmlDocument) As Boolean

        If InputData.IsNotStringNothingOrEmpty Then
            Try
                ' Versuche die Datei zu laden, wenn es keine Exception gibt, ist alles ok

                With xDoc
                    ' Verhindere, dass etwaige HTML-Seiten validiert werden. Hier friert der Prozess ein.
                    .XmlResolver = Nothing

                    If IsPfad Then
                        Try
                            .Load(InputData)
                            SendLog(LogLevel.Trace, $"{InputData}: { .InnerXml}")
                        Catch ex As Exception
                            SendLog(LogLevel.Error, InputData, ex)
                        End Try
                    Else
                        Try
                            .LoadXml(InputData)
                            SendLog(LogLevel.Trace, .InnerXml)
                        Catch ex As Exception
                            SendLog(LogLevel.Error, InputData, ex)
                        End Try

                    End If

                End With

                Return True

            Catch ex As XmlException
                SendLog(LogLevel.Error, $"Die XML-Datan weist einen Lade- oder Analysefehler auf: '{InputData}')", ex)

                Return False

            Catch ex As FileNotFoundException
                SendLog(LogLevel.Error, $"Die XML-Datan kann nicht gefunden werden '{InputData}'", ex)
                Return False

            End Try
        Else
            SendLog(LogLevel.Error, "Die übergebenen XML-Daten sind leer.")

            Return False
        End If
    End Function
#End Region

#Region "XML Deserialisieren"

    ''' <summary>
    ''' Deserialisiert die XML-Datei, die unter <paramref name="Data"/> gespeichert ist.
    ''' </summary>
    ''' <typeparam name="T">Zieltdatentyp</typeparam>
    ''' <param name="Data">Speicherort</param>
    ''' <param name="IsPath">Angabe, ob es sich um einen Pfad handelt.</param>
    ''' <param name="ReturnObj">Deserialisiertes Datenobjekt vom Type <typeparamref name="T"/>.</param>
    ''' <returns>True oder False, je nach Ergebnis der Deserialisierung</returns>
    Friend Function Deserialize(Of T)(Data As String, IsPath As Boolean, ByRef ReturnObj As T, Optional xslt As XslCompiledTransform = Nothing) As Boolean

        Dim xDoc As New XmlDocument
        If CheckXMLData(Data, IsPath, xDoc) Then

            ' Erstelle einen XMLReader zum Deserialisieren des XML-Documentes
            Using Reader As New IgnoreNameSpaceXmlNodeReader(xDoc)

                If xslt Is Nothing Then
                    ' Deserialisiere das XML-Objekt ohne Transformation
                    Return DeserializeObject(Reader, ReturnObj)

                Else
                    ' Führe eine Transformation durch
                    Dim TransformationOutput As New StringBuilder

                    ' Erstelle einen XMLWriter
                    Using transformedData As XmlWriter = XmlWriter.Create(TransformationOutput, New XmlWriterSettings With {.OmitXmlDeclaration = True})
                        ' Transformiere das XML-Objekt
                        xslt.Transform(Reader, transformedData)

                        ' Lies das transformierte XML-Objekt ein
                        Using ReaderTransformed As XmlReader = XmlReader.Create(New StringReader(TransformationOutput.ToString()))

                            ' Deserialisiere das transformierte XML-Objekt
                            Return DeserializeObject(ReaderTransformed, ReturnObj)

                        End Using
                    End Using
                End If

            End Using

        Else
            SendLog(LogLevel.Error, New Exception($"Fehler beim Deserialisieren: {Data} kann nicht deserialisert werden."))
            Return False

        End If
        xDoc = Nothing
    End Function

    ''' <summary>
    ''' Deserialisiert den übergebenen <paramref name="Reader"/> (<see cref="XmlReader"/>).
    ''' </summary>
    ''' <typeparam name="T">Typ des deserialsierten Objektes.</typeparam>
    ''' <param name="Reader">Der <see cref="XmlReader"/>.</param>
    ''' <param name="ReturnObj">Deserialisiertes Datenobjekt vom Type <typeparamref name="T"/>.</param>
    ''' <returns>True oder False, je nach Ergebnis der Deserialisierung</returns>
    Private Function DeserializeObject(Of T)(Reader As XmlReader, ByRef ReturnObj As T) As Boolean

        Dim Serializer As New XmlSerializer(GetType(T))

        If Serializer.CanDeserialize(Reader) Then
            Try
                ReturnObj = CType(Serializer.Deserialize(Reader, New XmlDeserializationEvents With {.OnUnknownAttribute = AddressOf On_UnknownAttribute,
                                                                                                    .OnUnknownElement = AddressOf On_UnknownElement,
                                                                                                    .OnUnknownNode = AddressOf On_UnknownNode,
                                                                                                    .OnUnreferencedObject = AddressOf On_UnreferencedObject}), T)

                Return True

            Catch ex As InvalidOperationException

                SendLog(LogLevel.Error, "Bei der Deserialisierung ist ein Fehler aufgetreten.", ex)
                Return False
            End Try
        Else
            SendLog(LogLevel.Error, New Exception("Fehler beim Deserialisieren."))
            Return False
        End If

    End Function

    Friend Async Function DeserializeAsyncFromPath(Of T)(Path As String, Optional xslt As XslCompiledTransform = Nothing) As Task(Of T)
        SendLog(LogLevel.Debug, $"Deserialisierung von Pfad {Path} ")
        ' Lade Daten herunter und starte das Deserialisiere der XML-Daten
        Return Await DeserializeAsyncData(Of T)(Await Client.GetStringWebClientAsync(Path), xslt)
    End Function

    Friend Function DeserializeAsyncData(Of T)(Data As String, Optional xslt As XslCompiledTransform = Nothing) As Task(Of T)
        Return Task.Run(Function()
                            Dim ReturnObj As T
                            Return If(Deserialize(Data, False, ReturnObj, xslt), ReturnObj, Nothing)
                        End Function)
    End Function
#End Region

#Region "XML Serialisieren"

    Friend Function SerializeToString(Of T)(objectData As T, ByRef result As String) As Boolean

        If objectData IsNot Nothing Then
            Dim XmlSerializerNamespace As New XmlSerializerNamespaces()
            XmlSerializerNamespace.Add("", "")

            Using XmlSchreiber As New Utf8StringWriter

                With New XmlSerializer(GetType(T))
                    Try
                        .Serialize(XmlSchreiber, objectData, XmlSerializerNamespace)
                        result = XmlSchreiber.ToString

                        Return True
                    Catch ex As InvalidOperationException

                        SendLog(LogLevel.Error, New Exception($"Fehler beim Serialisieren von {GetType(T).FullName}: {objectData}"))

                        Return False
                    End Try

                End With
            End Using
        End If

        Return False
    End Function

#End Region

#Region "XmlDeserializationEvents"
    Private Sub On_UnknownAttribute(sender As Object, e As XmlAttributeEventArgs)
        SendLog(LogLevel.Warning, $"Unknown Attribute: {e.Attr.Name} in {e.ObjectBeingDeserialized}")
    End Sub

    Private Sub On_UnknownElement(sender As Object, e As XmlElementEventArgs)
        SendLog(LogLevel.Warning, $"Unknown Element: {e.Element.Name} in {e.ObjectBeingDeserialized}")
    End Sub

    Private Sub On_UnknownNode(sender As Object, e As XmlNodeEventArgs)
        SendLog(LogLevel.Warning, $"Unknown Node: {e.Name} in {e.ObjectBeingDeserialized}")
    End Sub

    Private Sub On_UnreferencedObject(sender As Object, e As UnreferencedObjectEventArgs)
        SendLog(LogLevel.Warning, $"Unreferenced Object: {e.UnreferencedId}")
    End Sub
#End Region

#End Region

End Class
