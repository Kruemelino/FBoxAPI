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

    ''' <summary>
    ''' Gets a path to a lua script, which generates an XML structured list of devicelog events.
    ''' The URL can be extended by “filter” to get only a specific group of events. E. g. &filter=sys
    ''' </summary>
    Function GetDeviceLogPath(ByRef DeviceLogPath As String) As Boolean

    Function GetDeviceLogXML(Filter As DeviceLogFilter) As Task(Of DeviceLog)
End Interface
