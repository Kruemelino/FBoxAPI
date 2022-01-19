''' <summary>
''' TR-064 Support – ManagementServer
''' Date: 2013-01-23
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/mgmsrvSCPD.pdf</see>
''' </summary>
Public Interface IMgmsrvSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As ManagementServerInfo) As Boolean

    Function SetManagementServerURL(URL As String) As Boolean

    Function SetManagementServerUsername(Username As String) As Boolean

    Function SetManagementServerPassword(Password As String) As Boolean

    Function SetPeriodicInform(PeriodicInformEnable As Boolean,
                               PeriodicInformInterval As Integer,
                               PeriodicInformTime As String) As Boolean

    Function SetConnectionRequestAuthentication(ConnectionRequestUsername As String,
                                                ConnectionRequestPassword As String) As Boolean

    Function SetUpgradeManagement(UpgradesManaged As Boolean) As Boolean

    Function SetTR069Enable(TR069Enabled As Boolean) As Boolean

    Function GetTR069FirmwareDownloadEnabled(ByRef TR069FirmwareDownloadEnabled As Boolean) As Boolean

    Function SetTR069FirmwareDownloadEnabled(TR069FirmwareDownloadEnabled As Boolean) As Boolean

End Interface
