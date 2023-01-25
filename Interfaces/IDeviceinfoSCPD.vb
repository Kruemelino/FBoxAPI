''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2022-02-16
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceinfoSCPD.pdf</see>
''' </summary>
''' 
Public Interface IDeviceinfoSCPD
    Inherits IServiceBase
    Function GetInfo(ByRef Info As DeviceInfo) As Boolean
    Function SetProvisioningCode(ProvisioningCode As String) As Boolean
    Function GetDeviceLog(ByRef DeviceLog As String) As Boolean
    Function GetSecurityPort(ByRef SecurityPort As Integer) As Boolean

End Interface
