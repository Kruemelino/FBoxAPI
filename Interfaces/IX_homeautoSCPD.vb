''' <summary>
''' TR-064 Support – Homeauto
''' Date: 2019-04-29
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf</see>
''' </summary>
Public Interface IX_homeautoSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Return a string with all allowed chars for state variable AIN and max and min allowed value length for AIN and DeviceName. 
    ''' </summary>
    Function GetInfo(ByRef AllowedCharsAIN As String,
                     ByRef MaxCharsAIN As Integer,
                     ByRef MinCharsAIN As Integer,
                     ByRef MaxCharsDeviceName As Integer,
                     ByRef MinCharsDeviceName As Integer) As Boolean

    ''' <summary>
    ''' Read values/states for action parameters for devices by index
    ''' When a smart home group is requested, the DeviceName represents the name of the group. When a virtual
    ''' device (groups or templates) are requested the ProductName is set to “Group” or “Template”.
    ''' </summary>
    ''' <remarks>Required rights: Homeauto</remarks>
    Function GetGenericDeviceInfos(Index As Integer, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean

    ''' <summary>
    ''' Read values/states for action parameters for devices by AIN
    ''' When a smart home group is requested, the DeviceName represents the name of the group. When a virtual
    ''' device (groups or templates) are requested the ProductName is set to “Group” or “Template”.
    ''' </summary>
    ''' <remarks>Required rights: Homeauto</remarks>
    Function GetSpecificDeviceInfos(AIN As String, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean

    ''' <summary>
    ''' This action allows to set the name of a smart home device (physical device, group or template).
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function SetDeviceName(AIN As String, DeviceName As String) As Boolean

    ''' <summary>
    ''' This action allows to configure the state of the socket.
    ''' </summary>
    ''' <remarks>Required rights: Homeauto</remarks>
    Function SetSwitch(AIN As String, SwitchState As SwStateEnum) As Boolean
End Interface