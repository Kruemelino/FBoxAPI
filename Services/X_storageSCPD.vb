﻿''' <summary>
''' TR-064 Support – X_AVM-DE_Storage  
''' Date: 2022-02-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_storageSCPD.pdf</see>
''' </summary>
Friend Class X_storageSCPD
    Implements IX_storageSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 14) Implements IX_storageSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_storageSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_storageSCPD Implements IX_storageSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_storageSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef FTPEnable As Boolean, ByRef FTPStatus As EnableEnum, ByRef SMBEnable As Boolean, ByRef FTPWANEnable As Boolean, ByRef FTPWANSSLOnly As Boolean, ByRef FTPWANPort As Integer) As Boolean Implements IX_storageSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)
            Return .TryGetValueEx("NewFTPEnable", FTPEnable) And
                   .TryGetValueEx("NewFTPStatus", FTPStatus) And
                   .TryGetValueEx("NewSMBEnable", SMBEnable) And
                   .TryGetValueEx("NewFTPWANEnable", FTPWANEnable) And
                   .TryGetValueEx("NewFTPWANSSLOnly", FTPWANSSLOnly) And
                   .TryGetValueEx("NewFTPWANPort", FTPWANPort)
        End With
    End Function

    Public Function SetFTPServer(FTPEnable As Boolean) As Boolean Implements IX_storageSCPD.SetFTPServer
        Return Not TR064Start(ServiceFile, "SetFTPServer", ServiceID, New Dictionary(Of String, String) From {{"NewFTPEnable", FTPEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetFTPServerWAN(FTPWANEnable As Boolean, FTPWANSSLOnly As Boolean) As Boolean Implements IX_storageSCPD.SetFTPServerWAN
        Return Not TR064Start(ServiceFile, "SetFTPServerWAN", ServiceID,
                              New Dictionary(Of String, String) From {{"NewFTPWANEnable", FTPWANEnable.ToBoolStr},
                                                                      {"NewFTPWANSSLOnly", FTPWANSSLOnly.ToBoolStr}}).ContainsKey("Error")

    End Function

    Public Function RequestFTPServerWAN(ByRef FTPWANPort As Integer, ByRef FTPWANLifetime As Integer) As Boolean Implements IX_storageSCPD.RequestFTPServerWAN
        With TR064Start(ServiceFile, "RequestFTPServerWAN", ServiceID, Nothing)
            Return .TryGetValueEx("NewFTPWANPort", FTPWANPort) And
                   .TryGetValueEx("NewFTPWANLifetime", FTPWANLifetime)
        End With
    End Function

    Public Function SetSMBServer(SMBEnable As Boolean) As Boolean Implements IX_storageSCPD.SetSMBServer
        Return Not TR064Start(ServiceFile, "SetSMBServer", ServiceID, New Dictionary(Of String, String) From {{"NewSMBEnable", SMBEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetUserInfo(ByRef Enable As Boolean, ByRef Username As String, ByRef NetworkAccessReadOnly As Boolean) As Boolean Implements IX_storageSCPD.GetUserInfo
        With TR064Start(ServiceFile, "GetUserInfo", ServiceID, Nothing)
            Return .TryGetValueEx("NewEnable", Enable) And
                   .TryGetValueEx("NewUsername", Username) And
                   .TryGetValueEx("NewX_AVM-DE_NetworkAccessReadOnly", NetworkAccessReadOnly)
        End With
    End Function

    Public Function SetUserConfig(Enable As Boolean, Password As String, NetworkAccessReadOnly As Boolean) As Boolean Implements IX_storageSCPD.SetUserConfig
        Return Not TR064Start(ServiceFile, "SetUserConfig", ServiceID,
                      New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr},
                                                              {"NewPassword", Password},
                                                              {"NewX_AVM-DE_NetworkAccessReadOnly", NetworkAccessReadOnly.ToBoolStr}}).ContainsKey("Error")


    End Function

End Class