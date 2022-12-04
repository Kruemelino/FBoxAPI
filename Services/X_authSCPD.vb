''' <summary>
''' TR-064 Support – Authentication
''' Date: 2016-10-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf</see>
''' </summary>
Friend Class X_authSCPD
    Implements IX_authSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_authSCPD.TR064Start

    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_authSCPD Implements IX_authSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Enabled As Boolean) As Boolean Implements IX_authSCPD.GetInfo
        Return TR064Start(ServiceFile, "GetInfo", Nothing).TryGetValueEx("NewEnabled", Enabled)
    End Function

    Public Function GetState(ByRef State As AuthStateEnum) As Boolean Implements IX_authSCPD.GetState
        Return TR064Start(ServiceFile, "GetState", Nothing).TryGetValueEx("NewState", State)
    End Function

    Public Function SetConfig(Action As AuthActionEnum, ByRef Token As String, ByRef State As AuthStateEnum, ByRef Methods As String) As Boolean Implements IX_authSCPD.SetConfig
        With TR064Start(ServiceFile, "SetConfig", New Dictionary(Of String, String) From {{"NewAction", Action.ToString}})

            Return .TryGetValueEx("NewToken", Token) And
                   .TryGetValueEx("NewState", State) And
                   .TryGetValueEx("NewMethods", Methods)
        End With
    End Function
End Class