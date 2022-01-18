''' <summary>
''' TR-064 Support – LANConfigSecurity
''' Date: 2020-02-27
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanconfigsecuritySCPD.pdf</see>
''' </summary>
Friend Class LANConfigSecuritySCPD
    Implements ILANConfigSecuritySCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements ILANConfigSecuritySCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.lanconfigsecuritySCPD Implements ILANConfigSecuritySCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef MaxCharsPassword As Integer, ByRef MinCharsPassword As Integer, ByRef AllowedCharsPassword As String) As Boolean Implements ILANConfigSecuritySCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("MaxCharsPassword", MaxCharsPassword) And
                   .TryGetValueEx("MinCharsPassword", MinCharsPassword) And
                   .TryGetValueEx("AllowedCharsPassword", AllowedCharsPassword)

        End With
    End Function

    Public Function GetAnonymousLogin(ByRef AnonymousLoginEnabled As Boolean) As Boolean Implements ILANConfigSecuritySCPD.GetAnonymousLogin
        Return TR064Start(ServiceFile, "X_AVM-DE_GetAnonymousLogin", Nothing).TryGetValueEx("NewX_AVM-DE_AnonymousLoginEnabled", AnonymousLoginEnabled)
    End Function

    Public Function GetCurrentUser(ByRef CurrentUsername As String, ByRef CurrentUserRights As String) As Boolean Implements ILANConfigSecuritySCPD.GetCurrentUser
        With TR064Start(ServiceFile, "X_AVM-DE_GetCurrentUser", Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_CurrentUsername", CurrentUsername) And
                   .TryGetValueEx("NewX_AVM-DE_CurrentUserRights", CurrentUserRights)

        End With
    End Function

    Public Function SetConfigPassword(ConfigPassword As String) As Boolean Implements ILANConfigSecuritySCPD.SetConfigPassword
        Return Not TR064Start(ServiceFile, "SetConfigPassword", New Dictionary(Of String, String) From {{"NewPassword", ConfigPassword}}).ContainsKey("Error")
    End Function

    Public Function GetUserList(ByRef UserList As String) As Boolean Implements ILANConfigSecuritySCPD.GetUserList
        Return TR064Start(ServiceFile, "X_AVM-DE_GetUserList", Nothing).TryGetValueEx("NewX_AVM-DE_UserList", UserList)
    End Function

End Class