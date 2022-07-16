''' <summary>
''' TR-064 Support – X_AVM-DE_TAM 
''' Date: 2019-06-28
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_tam.pdf</see>
''' </summary>
Friend Class X_tamSCPD
    Implements IX_tamSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_tamSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_tamSCPD Implements IX_tamSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)),
                   XMLSerializer As Serializer)

        TR064Start = Start
        XML = XMLSerializer

    End Sub

    Public Function GetTAMInfo(ByRef TAMInfo As TAMInfo, i As Integer) As Boolean Implements IX_tamSCPD.GetTAMInfo

        If TAMInfo Is Nothing Then TAMInfo = New TAMInfo

        With TR064Start(ServiceFile, "GetInfo", New Dictionary(Of String, String) From {{"NewIndex", i.ToString}})

            If .ContainsKey("NewPhoneNumbers") Then

                TAMInfo.PhoneNumbers = .Item("NewPhoneNumbers").Split(","c)

                Return .TryGetValueEx("NewEnable", TAMInfo.Enable) And
                       .TryGetValueEx("NewName", TAMInfo.Name) And
                       .TryGetValueEx("NewTAMRunning", TAMInfo.TAMRunning) And
                       .TryGetValueEx("NewStick", TAMInfo.Stick) And
                       .TryGetValueEx("NewStatus", TAMInfo.Status) And
                       .TryGetValueEx("NewCapacity", TAMInfo.Capacity) And
                       .TryGetValueEx("NewMode", TAMInfo.Mode) And
                       .TryGetValueEx("NewRingSeconds", TAMInfo.RingSeconds)
            Else
                Return False
            End If
        End With

    End Function

    Public Function SetEnable(Index As Integer, Enable As Boolean) As Boolean Implements IX_tamSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", New Dictionary(Of String, String) From {{"NewIndex", Index.ToString},
                                                                                                {"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetMessageList(ByRef GetMessageListURL As String, i As Integer) As Boolean Implements IX_tamSCPD.GetMessageList
        Return TR064Start(ServiceFile, "GetMessageList",
                          New Dictionary(Of String, String) From {{"NewIndex", i.ToString}}).
                          TryGetValueEx("NewURL", GetMessageListURL)
    End Function

    Public Async Function GetMessageList(i As Integer) As Task(Of MessageList) Implements IX_tamSCPD.GetMessageList
        'Ermittle den Pfad zum Telefonbuch und deserialisiere die Daten
        Dim MessageListUrl As String = String.Empty

        If GetMessageList(MessageListUrl, i) Then
            Return Await XML.DeserializeAsyncFromPath(Of MessageList)(MessageListUrl)
        Else
            ' Gib eine leere Liste zurück
            Return New MessageList
        End If

    End Function

    Public Function MarkMessage(Index As Integer, MessageIndex As Integer, MarkedAsRead As Boolean) As Boolean Implements IX_tamSCPD.MarkMessage
        Return Not TR064Start(ServiceFile, "MarkMessage", New Dictionary(Of String, String) From {{"NewIndex", Index.ToString},
                                                                                                  {"NewMessageIndex", MessageIndex.ToString},
                                                                                                  {"NewMarkedAsRead", MarkedAsRead.ToBoolStr}}).ContainsKey("Error")

    End Function

    Public Function DeleteMessage(Index As Integer, MessageIndex As Integer) As Boolean Implements IX_tamSCPD.DeleteMessage
        Return Not TR064Start(ServiceFile, "DeleteMessage", New Dictionary(Of String, String) From {{"NewIndex", Index.ToString},
                                                                                                    {"NewMessageIndex", MessageIndex.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetList(ByRef List As String) As Boolean Implements IX_tamSCPD.GetList
        Return TR064Start(ServiceFile, "GetList", Nothing).TryGetValueEx("NewTAMList", List)
    End Function

    Public Async Function GetList() As Task(Of TAMList) Implements IX_tamSCPD.GetList
        Return Await XML.DeserializeAsyncData(Of TAMList)((TR064Start(ServiceFile,
                                                                      "GetList",
                                                                      Nothing)).TryGetValueEx(Of String)("NewTAMList"))
    End Function

    Public Function GetList(ByRef List As TAMList) As Boolean Implements IX_tamSCPD.GetList
        Dim XMLList As String = String.Empty
        Return GetList(XMLList) AndAlso XML.Deserialize(XMLList, False, List)
    End Function

End Class
