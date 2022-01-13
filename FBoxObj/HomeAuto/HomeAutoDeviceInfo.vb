''' <summary>
''' TR-064 Support – Homeauto
''' Date: 2019-04-29
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf</see>
''' </summary>
Public Class HomeAutoDeviceInfo
    ''' <summary>
    ''' Device identifier
    ''' </summary>
    Public Property AIN As String

    ''' <summary>
    ''' Device ID 
    ''' </summary>
    Public Property DeviceId As Integer

    ''' <summary>
    ''' Device function information
    ''' </summary>
    Public Property FunctionBitMask As Integer

    ''' <summary>
    ''' FRITZ!OS version
    ''' </summary>
    Public Property FirmwareVersion As String

    ''' <summary>
    ''' Manufacturer information
    ''' </summary>
    Public Property Manufacturer As String

    ''' <summary>
    ''' Devicetype, eg. Group, Template, HAN-FUN, AVM DECT Telefon C3, Comet DECT, FRITZ!DECT 200, FRITZ!DECT Repeater 100, unknown
    ''' </summary>
    Public Property ProductName As String

    ''' <summary>
    ''' Devicename
    ''' </summary>
    Public Property DeviceName As String

    ''' <summary>
    ''' Connection status
    ''' </summary>
    Public Property Present As PresentEnum

    ''' <summary>
    ''' Feature is supported
    ''' </summary>
    Public Property MultimeterIsEnabled As EnabledEnum

    ''' <summary>
    ''' Value is valid 
    ''' </summary>
    Public Property MultimeterIsValid As ValidEnum

    ''' <summary>
    ''' Power value [1/100 W] 
    ''' </summary>
    Public Property MultimeterPower As Integer

    ''' <summary>
    ''' Energy value [Wh] 
    ''' </summary>
    Public Property MultimeterEnergy As Integer

    ''' <summary>
    ''' Feature is supported 
    ''' </summary>
    Public Property TemperatureIsEnabled As EnabledEnum

    ''' <summary>
    ''' Value is valid
    ''' </summary>
    Public Property TemperatureIsValid As ValidEnum

    ''' <summary>
    ''' Temperature [1/10°C]
    ''' </summary>
    Public Property TemperatureCelsius As Integer

    ''' <summary>
    ''' Temperature offset [1/10°C]
    ''' </summary>
    Public Property TemperatureOffset As Integer

    ''' <summary>
    ''' Feature is supported 
    ''' </summary>
    Public Property SwitchIsEnabled As EnabledEnum

    ''' <summary>
    ''' Value is valid
    ''' </summary>
    Public Property SwitchIsValid As ValidEnum

    ''' <summary>
    ''' Switch status
    ''' </summary>
    Public Property SwitchState As SwStateEnum

    ''' <summary>
    ''' Switch timer control
    ''' </summary>
    Public Property SwitchMode As SwModeEnum

    ''' <summary>
    ''' Switch keylock
    ''' </summary>
    Public Property SwitchLock As Boolean

    ''' <summary>
    ''' HKR feature is supported
    ''' </summary>
    Public Property HkrIsEnabled As EnabledEnum

    ''' <summary>
    ''' HKR values are valid
    ''' </summary>
    Public Property HkrIsValid As ValidEnum

    ''' <summary>
    ''' Value is temperature [1/10 °C]
    ''' </summary>
    Public Property HkrIsTemperature As Integer

    ''' <summary>
    ''' HKR set valve status
    ''' </summary>
    Public Property HkrSetVentilStatus As VentilEnum

    ''' <summary>
    ''' Value set temperature [1/10 °C]
    ''' </summary>
    Public Property HkrSetTemperature As Integer

    ''' <summary>
    ''' HKR reduce valve status
    ''' </summary>
    Public Property HkrReduceVentilStatus As VentilEnum

    ''' <summary>
    ''' Value reduce temperature [1/10 °C]
    ''' </summary>
    Public Property HkrReduceTemperature As Integer

    ''' <summary>
    ''' HKR comfort valve status
    ''' </summary>
    Public Property HkrComfortVentilStatus As VentilEnum

    ''' <summary>
    ''' Value comfort temperature [1/10 °C]
    ''' </summary>
    Public Property HkrComfortTemperature As Integer
End Class
