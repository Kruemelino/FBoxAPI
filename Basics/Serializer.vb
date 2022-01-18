Imports System.IO
Imports System.Xml
Imports System.Xml.Serialization

Friend Class Serializer
    Inherits LogBase

    Private Property PushStatus As Action(Of LogMessage)

    Public Sub New(Status As Action(Of LogMessage))
        PushStatus = Status
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
                        .Load(InputData)

                        PushStatus?.Invoke(CreateLog(LogLevel.Trace, $"{InputData}: { .InnerXml}"))
                    Else
                        .LoadXml(InputData)

                        PushStatus?.Invoke(CreateLog(LogLevel.Trace, .InnerXml))
                    End If

                End With

                Return True

            Catch ex As XmlException
                PushStatus?.Invoke(CreateLog(LogLevel.Fatal, $"Die XML-Datan weist einen Lade- oder Analysefehler auf: '{InputData}')", ex))

                Return False

            Catch ex As FileNotFoundException
                PushStatus?.Invoke(CreateLog(LogLevel.Fatal, $"Die XML-Datan kann nicht gefunden werden '{InputData}'", ex))
                Return False

            End Try
        Else
            PushStatus?.Invoke(CreateLog(LogLevel.Fatal, "Die übergebenen XML-Datan sind leer."))

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
    Friend Function Deserialize(Of T)(Data As String, IsPath As Boolean, ByRef ReturnObj As T) As Boolean

        Dim xDoc As New XmlDocument
        If CheckXMLData(Data, IsPath, xDoc) Then

            Dim Serializer As New XmlSerializer(GetType(T))

            ' Erstelle einen XMLReader zum Deserialisieren des XML-Documentes
            Using Reader As New XmlNodeReader(xDoc)

                If Serializer.CanDeserialize(Reader) Then
                    Try
                        ReturnObj = CType(Serializer.Deserialize(Reader, New XmlDeserializationEvents With {.OnUnknownAttribute = AddressOf On_UnknownAttribute,
                                                                                                            .OnUnknownElement = AddressOf On_UnknownElement,
                                                                                                            .OnUnknownNode = AddressOf On_UnknownNode,
                                                                                                            .OnUnreferencedObject = AddressOf On_UnreferencedObject}), T)

                        Return True

                    Catch ex As InvalidOperationException

                        PushStatus?.Invoke(CreateLog(LogLevel.Fatal, "Bei der Deserialisierung ist ein Fehler aufgetreten.", ex))
                        Return False
                    End Try
                Else
                    PushStatus?.Invoke(CreateLog(LogLevel.Fatal, New Exception("Fehler beim Deserialisieren."), xDoc.InnerXml))
                    Return False
                End If


            End Using

        Else
            PushStatus?.Invoke(CreateLog(LogLevel.Fatal, New Exception($"Fehler beim Deserialisieren: {Data} kann nicht deserialisert werden.")))
            Return False
        End If
        xDoc = Nothing
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

                        PushStatus?.Invoke(CreateLog(LogLevel.Fatal, New Exception($"Fehler beim Serialisieren von {GetType(T).FullName}: {objectData}")))

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
        PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Unknown Attribute: {e.Attr.Name} in {e.ObjectBeingDeserialized}"))
    End Sub

    Private Sub On_UnknownElement(sender As Object, e As XmlElementEventArgs)
        PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Unknown Element: {e.Element.Name} in {e.ObjectBeingDeserialized}"))
    End Sub

    Private Sub On_UnknownNode(sender As Object, e As XmlNodeEventArgs)
        PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Unknown Node: {e.Name} in {e.ObjectBeingDeserialized}"))
    End Sub

    Private Sub On_UnreferencedObject(sender As Object, e As UnreferencedObjectEventArgs)
        PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Unreferenced Object: {e.UnreferencedId}"))
    End Sub
#End Region

#End Region

End Class
