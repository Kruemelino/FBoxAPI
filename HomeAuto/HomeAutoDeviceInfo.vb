''' <summary>
''' TR-064 Support – Homeauto
''' Date: 2019-04-29
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf"/>
''' </summary>
Public Class HomeAutoDeviceInfo
    ''' <summary>
    ''' Device identifier
    ''' </summary>
    Friend Property AIN As String

    ''' <summary>
    ''' Device ID 
    ''' </summary>
    Friend Property DeviceId As Integer

    ''' <summary>
    ''' Device function information
    ''' </summary>
    Friend Property FunctionBitMask As Integer

    ''' <summary>
    ''' FRITZ!OS version
    ''' </summary>
    Friend Property FirmwareVersion As String

    ''' <summary>
    ''' Manufacturer information
    ''' </summary>
    Friend Property Manufacturer As String

    ''' <summary>
    ''' Devicetype, eg. Group, Template, HAN-FUN, AVM DECT Telefon C3, Comet DECT, FRITZ!DECT 200, FRITZ!DECT Repeater 100, unknown
    ''' </summary>
    Friend Property ProductName As String

    ''' <summary>
    ''' Devicename
    ''' </summary>
    Friend Property DeviceName As String

    ''' <summary>
    ''' Connection status
    ''' </summary>
    Friend Property Present As PresentEnum

    ''' <summary>
    ''' Feature is supported
    ''' </summary>
    Friend Property MultimeterIsEnabled As EnabledEnum

    ''' <summary>
    ''' Value is valid 
    ''' </summary>
    Friend Property MultimeterIsValid As ValidEnum

    ''' <summary>
    ''' Power value [1/100 W] 
    ''' </summary>
    Friend Property MultimeterPower As Integer

    ''' <summary>
    ''' Energy value [Wh] 
    ''' </summary>
    Friend Property MultimeterEnergy As Integer

    ''' <summary>
    ''' Feature is supported 
    ''' </summary>
    Friend Property TemperatureIsEnabled As EnabledEnum

    ''' <summary>
    ''' Value is valid
    ''' </summary>
    Friend Property TemperatureIsValid As ValidEnum

    ''' <summary>
    ''' Temperature [1/10°C]
    ''' </summary>
    Friend Property TemperatureCelsius As Integer

    ''' <summary>
    ''' Temperature offset [1/10°C]
    ''' </summary>
    Friend Property TemperatureOffset As Integer

    ''' <summary>
    ''' Feature is supported 
    ''' </summary>
    Friend Property SwitchIsEnabled As EnabledEnum

    ''' <summary>
    ''' Value is valid
    ''' </summary>
    Friend Property SwitchIsValid As ValidEnum

    ''' <summary>
    ''' Switch status
    ''' </summary>
    Friend Property SwitchState As SwStateEnum

    ''' <summary>
    ''' Switch timer control
    ''' </summary>
    Friend Property SwitchMode As SwModeEnum

    ''' <summary>
    ''' Switch keylock
    ''' </summary>
    Friend Property SwitchLock As Boolean

    ''' <summary>
    ''' HKR feature is supported
    ''' </summary>
    Friend Property HkrIsEnabled As EnabledEnum

    ''' <summary>
    ''' HKR values are valid
    ''' </summary>
    Friend Property HkrIsValid As ValidEnum

    ''' <summary>
    ''' Value is temperature [1/10 °C]
    ''' </summary>
    Friend Property HkrIsTemperature As Integer

    ''' <summary>
    ''' HKR set valve status
    ''' </summary>
    Friend Property HkrSetVentilStatus As VentilEnum

    ''' <summary>
    ''' Value set temperature [1/10 °C]
    ''' </summary>
    Friend Property HkrSetTemperature As Integer

    ''' <summary>
    ''' HKR reduce valve status
    ''' </summary>
    Friend Property HkrReduceVentilStatus As VentilEnum

    ''' <summary>
    ''' Value reduce temperature [1/10 °C]
    ''' </summary>
    Friend Property HkrReduceTemperature As Integer

    ''' <summary>
    ''' HKR comfort valve status
    ''' </summary>
    Friend Property HkrComfortVentilStatus As VentilEnum

    ''' <summary>
    ''' Value comfort temperature [1/10 °C]
    ''' </summary>
    Friend Property HkrComfortTemperature As Integer
End Class
