''' <summary>
''' TR-064 Support – X_ AVM-DE_TAM 
''' Date: 2019-06-28
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_tam.pdf</see>
''' </summary>
Friend Class X_tamSCPD
    Implements IX_tamSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_tamSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_tamSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_tamSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String), XMLSerializer As Serializer)

        ServiceFile = SCPDFiles.x_tamSCPD

        TR064Start = Start

        PushStatus = Status

        XML = XMLSerializer
    End Sub

    Public Function GetTAMInfo(ByRef TAMInfo As TAMInfo, i As Integer) As Boolean Implements IX_tamSCPD.GetTAMInfo

        If TAMInfo Is Nothing Then TAMInfo = New TAMInfo

        With TR064Start(ServiceFile, "GetInfo", New Hashtable From {{"NewIndex", i}})

            If .ContainsKey("NewEnable") And .ContainsKey("NewPhoneNumbers") Then

                TAMInfo.Enable = CBool(.Item("NewEnable"))
                TAMInfo.Name = .Item("NewName").ToString
                TAMInfo.TAMRunning = CBool(.Item("NewTAMRunning"))
                TAMInfo.Stick = CUShort(.Item("NewStick"))
                TAMInfo.Status = CUShort(.Item("NewStatus"))
                TAMInfo.Capacity = CULng(.Item("NewCapacity"))
                TAMInfo.Mode = .Item("NewMode").ToString
                TAMInfo.RingSeconds = CUShort(.Item("NewRingSeconds"))
                TAMInfo.PhoneNumbers = .Item("NewPhoneNumbers").ToString.Split(",")

                PushStatus.Invoke(LogLevel.Debug, $"GetTAMInfo ({i}): {TAMInfo.Name}; {TAMInfo.Enable}")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTAMInfo konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function SetEnable(Index As Integer, Enable As Boolean) As Boolean Implements IX_tamSCPD.SetEnable

        With TR064Start(ServiceFile, "SetEnable", New Hashtable From {{"NewIndex", Index}, {"NewEnable", Enable.ToInt}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function GetMessageList(ByRef GetMessageListURL As String, i As Integer) As Boolean Implements IX_tamSCPD.GetMessageList
        With TR064Start(ServiceFile, "GetMessageList", New Hashtable From {{"NewIndex", i}})
            If .ContainsKey("NewURL") Then

                GetMessageListURL = .Item("NewURL").ToString

                PushStatus.Invoke(LogLevel.Debug, $"GetMessageListURL: {GetMessageListURL} ")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetMessageList konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function MarkMessage(Index As Integer, MessageIndex As Integer, MarkedAsRead As Boolean) As Boolean Implements IX_tamSCPD.MarkMessage

        With TR064Start(ServiceFile, "MarkMessage", New Hashtable From {{"NewIndex", Index}, {"NewMessageIndex", MessageIndex}, {"NewMarkedAsRead", MarkedAsRead.ToInt}})
            Return Not .ContainsKey("Error")
        End With

    End Function

    Public Function DeleteMessage(Index As Integer, MessageIndex As Integer) As Boolean Implements IX_tamSCPD.DeleteMessage

        With TR064Start(ServiceFile, "DeleteMessage", New Hashtable From {{"NewIndex", Index},
                                                                                           {"NewMessageIndex", MessageIndex}})


            If Not .ContainsKey("Error") Then

                PushStatus.Invoke(LogLevel.Info, $"Nachricht auf Anrufbeantworter {Index} mit ID {MessageIndex} gelöscht, '{ .Item("Error")}'")
                Return True
            Else

                PushStatus.Invoke(LogLevel.Warn, $"Nachricht auf Anrufbeantworter {Index} mit ID {MessageIndex} nicht gelöscht, '{ .Item("Error")}'")
                Return False
            End If
        End With

    End Function

    Public Function GetList(ByRef List As TAMList) As Boolean Implements IX_tamSCPD.GetList

        With TR064Start(ServiceFile, "GetList", Nothing)

            If .ContainsKey("NewTAMList") Then

                If Not XML.Deserialize(.Item("NewTAMList").ToString(), False, List) Then
                    PushStatus.Invoke(LogLevel.Warn, $"GetTAMList konnte für nicht deserialisiert werden.")
                End If

                ' Wenn keine TAM angeschlossen wurden, gib eine leere Klasse zurück
                If List Is Nothing Then List = New TAMList

                PushStatus.Invoke(LogLevel.Debug, $"GetList: {List.Items.Count} Anrufbeantworter ermittelt.")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTAMList konnte für nicht aufgelößt werden. '{ .Item("Error")}'")
                List = Nothing

                Return False
            End If
        End With

    End Function

End Class
