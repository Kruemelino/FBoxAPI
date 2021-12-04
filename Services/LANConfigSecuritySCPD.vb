''' <summary>
''' TR-064 Support – LANConfigSecurity
''' Date: 2020-02-27
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanconfigsecuritySCPD.pdf"/>
''' </summary>
Public Class LANConfigSecuritySCPD
    Implements ILANConfigSecuritySCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements ILANConfigSecuritySCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements ILANConfigSecuritySCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements ILANConfigSecuritySCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.lanconfigsecuritySCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetInfo(ByRef MaxCharsPassword As Integer, ByRef MinCharsPassword As Integer, AllowedCharsPassword As String) As Boolean Implements ILANConfigSecuritySCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewMaxCharsPassword") And .ContainsKey("NewMinCharsPassword") And .ContainsKey("NewAllowedCharsPassword") Then
                MaxCharsPassword = CInt(.Item("MaxCharsPassword"))
                MinCharsPassword = CInt(.Item("MinCharsPassword"))
                AllowedCharsPassword = .Item("AllowedCharsPassword").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetAnonymousLogin(ByRef AnonymousLoginEnabled As Boolean) As Boolean Implements ILANConfigSecuritySCPD.GetAnonymousLogin
        With TR064Start(ServiceFile, "X_AVM-DE_GetAnonymousLogin", Nothing)

            If .ContainsKey("NewX_AVM-DE_AnonymousLoginEnabled") Then
                AnonymousLoginEnabled = CBool(.Item("NewX_AVM-DE_AnonymousLoginEnabled"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetAnonymousLogin konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetCurrentUser(ByRef CurrentUsername As String, ByRef CurrentUserRights As String) As Boolean Implements ILANConfigSecuritySCPD.GetCurrentUser
        With TR064Start(ServiceFile, "X_AVM-DE_GetCurrentUser", Nothing)

            If .ContainsKey("NewX_AVM-DE_CurrentUsername") And .ContainsKey("NewX_AVM-DE_CurrentUserRights") Then
                CurrentUsername = .Item("NewX_AVM-DE_CurrentUsername").ToString
                CurrentUserRights = .Item("NewX_AVM-DE_CurrentUserRights").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetConfigPassword(ConfigPassword As String) As Boolean Implements ILANConfigSecuritySCPD.SetConfigPassword
        With TR064Start(ServiceFile, "SetConfigPassword", New Hashtable From {{"NewPassword", ConfigPassword}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetUserList(ByRef UserList As String) As Boolean Implements ILANConfigSecuritySCPD.GetUserList

        With TR064Start(ServiceFile, "X_AVM-DE_GetUserList", Nothing)

            If .ContainsKey("NewX_AVM-DE_UserList") Then

                UserList = .Item("NewX_AVM-DE_UserList").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Userliste der Fritz!Box: '{UserList}'")

                Return True
            Else
                UserList = String.Empty

                PushStatus.Invoke(LogLevel.Warn, $"Userliste der Fritz!Box konnte nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

End Class