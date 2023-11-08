''' <summary>
''' TR-064 Support – Authentication
''' Date: 2022-02-11
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf</see>
''' </summary>
Friend Class X_authSCPD
    Implements IX_authSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 11) Implements IX_authSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_authSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_authSCPD Implements IX_authSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_authSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Enabled As Boolean) As Boolean Implements IX_authSCPD.GetInfo
        Return TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing).TryGetValueEx("NewEnabled", Enabled)
    End Function

    Public Function GetState(ByRef State As AuthStateEnum) As Boolean Implements IX_authSCPD.GetState
        Return TR064Start(ServiceFile, "GetState", ServiceID, Nothing).TryGetValueEx("NewState", State)
    End Function

    Public Function SetConfig(Action As AuthActionEnum, ByRef Token As String, ByRef State As AuthStateEnum, ByRef Methods As String) As Boolean Implements IX_authSCPD.SetConfig
        With TR064Start(ServiceFile, "SetConfig", ServiceID, New Dictionary(Of String, String) From {{"NewAction", Action.ToString}})

            Return .TryGetValueEx("NewToken", Token) And
                   .TryGetValueEx("NewState", State) And
                   .TryGetValueEx("NewMethods", Methods)
        End With
    End Function
End Class