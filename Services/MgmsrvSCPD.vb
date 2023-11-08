''' <summary>
''' TR-064 Support – ManagementServer
''' Date: 2013-01-23
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mgmsrvSCPD.pdf</see>
''' </summary>
Friend Class MgmsrvSCPD
    Implements IMgmsrvSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2013, 1, 23) Implements IMgmsrvSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IMgmsrvSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.mgmsrvSCPD Implements IMgmsrvSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IMgmsrvSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Info As ManagementServerInfo) As Boolean Implements IMgmsrvSCPD.GetInfo
        If Info Is Nothing Then Info = New ManagementServerInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewURL", Info.URL) And
                   .TryGetValueEx("NewUsername", Info.Username) And
                   .TryGetValueEx("NewPeriodicInformEnable", Info.PeriodicInformEnable) And
                   .TryGetValueEx("NewPeriodicInformInterval", Info.PeriodicInformInterval) And
                   .TryGetValueEx("NewPeriodicInformTime", Info.PeriodicInformTime) And
                   .TryGetValueEx("NewParameterKey", Info.ParameterKey) And
                   .TryGetValueEx("NewParameterHash", Info.ParameterHash) And
                   .TryGetValueEx("NewConnectionRequestURL", Info.ConnectionRequestURL) And
                   .TryGetValueEx("NewConnectionRequestUsername", Info.ConnectionRequestUsername) And
                   .TryGetValueEx("NewUpgradesManaged", Info.UpgradesManaged)
        End With
    End Function

    Public Function SetManagementServerURL(URL As String) As Boolean Implements IMgmsrvSCPD.SetManagementServerURL
        Return Not TR064Start(ServiceFile, "SetManagementServerURL", ServiceID, New Dictionary(Of String, String) From {{"NewURL", URL}}).ContainsKey("Error")
    End Function

    Public Function SetManagementServerUsername(Username As String) As Boolean Implements IMgmsrvSCPD.SetManagementServerUsername
        Return Not TR064Start(ServiceFile, "SetManagementServerUsername", ServiceID, New Dictionary(Of String, String) From {{"NewUsername", Username}}).ContainsKey("Error")
    End Function

    Public Function SetManagementServerPassword(Password As String) As Boolean Implements IMgmsrvSCPD.SetManagementServerPassword
        Return Not TR064Start(ServiceFile, "SetManagementServerPassword", ServiceID, New Dictionary(Of String, String) From {{"NewPassword", Password}}).ContainsKey("Error")
    End Function

    Public Function SetPeriodicInform(PeriodicInformEnable As Boolean, PeriodicInformInterval As Integer, PeriodicInformTime As String) As Boolean Implements IMgmsrvSCPD.SetPeriodicInform
        Return Not TR064Start(ServiceFile, "SetPeriodicInform", ServiceID,
                              New Dictionary(Of String, String) From {{"NewPeriodicInformEnable", PeriodicInformEnable.ToBoolStr},
                                                                      {"NewPeriodicInformInterval", PeriodicInformInterval.ToString},
                                                                      {"NewPeriodicInformTime", PeriodicInformTime}}).ContainsKey("Error")
    End Function

    Public Function SetConnectionRequestAuthentication(ConnectionRequestUsername As String, ConnectionRequestPassword As String) As Boolean Implements IMgmsrvSCPD.SetConnectionRequestAuthentication
        Return Not TR064Start(ServiceFile, "SetManagementServerPassword", ServiceID,
                              New Dictionary(Of String, String) From {{"NewConnectionRequestUsername", ConnectionRequestUsername},
                                                                      {"NewConnectionRequestPassword", ConnectionRequestPassword}}).ContainsKey("Error")
    End Function

    Public Function SetUpgradeManagement(UpgradesManaged As Boolean) As Boolean Implements IMgmsrvSCPD.SetUpgradeManagement
        Return Not TR064Start(ServiceFile, "SetUpgradeManagement", ServiceID, New Dictionary(Of String, String) From {{"NewUpgradesManaged", UpgradesManaged.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetTR069Enable(TR069Enabled As Boolean) As Boolean Implements IMgmsrvSCPD.SetTR069Enable
        Return Not TR064Start(ServiceFile, "NewTR069Enabled", ServiceID, New Dictionary(Of String, String) From {{"NewUpgradesManaged", TR069Enabled.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetTR069FirmwareDownloadEnabled(ByRef TR069FirmwareDownloadEnabled As Boolean) As Boolean Implements IMgmsrvSCPD.GetTR069FirmwareDownloadEnabled
        Return TR064Start(ServiceFile, "X_AVM_DE_GetTR069FirmwareDownloadEnabled", ServiceID, Nothing).TryGetValueEx("NewTR069FirmwareDownloadEnabled", TR069FirmwareDownloadEnabled)
    End Function

    Public Function SetTR069FirmwareDownloadEnabled(TR069FirmwareDownloadEnabled As Boolean) As Boolean Implements IMgmsrvSCPD.SetTR069FirmwareDownloadEnabled
        Return Not TR064Start(ServiceFile, "X_AVM_DE_SetTR069FirmwareDownloadEnabled", ServiceID, New Dictionary(Of String, String) From {{"NewTR069FirmwareDownloadEnabled", TR069FirmwareDownloadEnabled.ToBoolStr}}).ContainsKey("Error")
    End Function
End Class
