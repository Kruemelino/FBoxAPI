﻿''' <summary>
''' TR-064 Support – X_AVM-DE_TAM 
''' Date: 2019-06-28
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_tam.pdf</see>
''' </summary>
Friend Class X_tamSCPD
    Implements IX_tamSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_tamSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_tamSCPD Implements IX_tamSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)), XMLSerializer As Serializer)

        TR064Start = Start

        XML = XMLSerializer

    End Sub

    Public Function GetTAMInfo(ByRef TAMInfo As TAMInfo, i As Integer) As Boolean Implements IX_tamSCPD.GetTAMInfo

        If TAMInfo Is Nothing Then TAMInfo = New TAMInfo

        With TR064Start(ServiceFile, "GetInfo", New Dictionary(Of String, String) From {{"NewIndex", i}})

            If .ContainsKey("NewPhoneNumbers") Then

                TAMInfo.PhoneNumbers = .Item("NewPhoneNumbers").Split(",")

                Return .TryGetValue("NewEnable", TAMInfo.Enable) And
                       .TryGetValue("NewName", TAMInfo.Name) And
                       .TryGetValue("NewTAMRunning", TAMInfo.TAMRunning) And
                       .TryGetValue("NewStick", TAMInfo.Stick) And
                       .TryGetValue("NewStatus", TAMInfo.Status) And
                       .TryGetValue("NewCapacity", TAMInfo.Capacity) And
                       .TryGetValue("NewMode", TAMInfo.Mode) And
                       .TryGetValue("NewRingSeconds", TAMInfo.RingSeconds)
            Else
                Return False
            End If
        End With

    End Function

    Public Function SetEnable(Index As Integer, Enable As Boolean) As Boolean Implements IX_tamSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", New Dictionary(Of String, String) From {{"NewIndex", Index},
                                                                                                {"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetMessageList(ByRef GetMessageListURL As String, i As Integer) As Boolean Implements IX_tamSCPD.GetMessageList
        Return TR064Start(ServiceFile, "GetMessageList",
                          New Dictionary(Of String, String) From {{"NewIndex", i}}).
                          TryGetValue("NewURL", GetMessageListURL)
    End Function

    Public Function MarkMessage(Index As Integer, MessageIndex As Integer, MarkedAsRead As Boolean) As Boolean Implements IX_tamSCPD.MarkMessage
        Return Not TR064Start(ServiceFile, "MarkMessage", New Dictionary(Of String, String) From {{"NewIndex", Index},
                                                                                                  {"NewMessageIndex", MessageIndex},
                                                                                                  {"NewMarkedAsRead", MarkedAsRead.ToBoolStr}}).ContainsKey("Error")

    End Function

    Public Function DeleteMessage(Index As Integer, MessageIndex As Integer) As Boolean Implements IX_tamSCPD.DeleteMessage
        Return Not TR064Start(ServiceFile, "DeleteMessage", New Dictionary(Of String, String) From {{"NewIndex", Index}, {"NewMessageIndex", MessageIndex}}).ContainsKey("Error")
    End Function

    Public Function GetList(ByRef List As TAMList) As Boolean Implements IX_tamSCPD.GetList

        With TR064Start(ServiceFile, "GetList", Nothing)

            If .ContainsKey("NewTAMList") Then

                XML.Deserialize(.Item("NewTAMList"), False, List)
                ' Wenn keine TAM angeschlossen wurden, gib eine leere Klasse zurück
                If List Is Nothing Then List = New TAMList

                Return True
            Else
                List = Nothing

                Return False
            End If
        End With

    End Function

End Class
