''' <summary>
''' TR-064 Support – Homeauto
''' Date: 2019-04-29
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf</see>
''' </summary>
Friend Class X_homeautoSCPD
    Implements IX_homeautoSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_homeautoSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_homeautoSCPD Implements IX_homeautoSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef AllowedCharsAIN As String, ByRef MaxCharsAIN As Integer, ByRef MinCharsAIN As Integer, ByRef MaxCharsDeviceName As Integer, ByRef MinCharsDeviceName As Integer) As Boolean Implements IX_homeautoSCPD.GetInfo

        With TR064Start(ServiceFile, "GetMessageList", Nothing)

            Return .TryGetValueEx("NewAllowedCharsAIN", AllowedCharsAIN) And
                   .TryGetValueEx("MaxCharsAIN", MaxCharsAIN) And
                   .TryGetValueEx("MinCharsAIN", MinCharsAIN) And
                   .TryGetValueEx("MaxCharsDeviceName", MaxCharsDeviceName) And
                   .TryGetValueEx("MinCharsDeviceName", MinCharsDeviceName)

        End With

    End Function

    Public Function GetGenericDeviceInfos(Index As Integer, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean Implements IX_homeautoSCPD.GetGenericDeviceInfos

        If DeviceInfo Is Nothing Then DeviceInfo = New HomeAutoDeviceInfo

        With TR064Start(ServiceFile, "GetGenericDeviceInfos", New Dictionary(Of String, String) From {{"NewIndex", Index}})

            Return .TryGetValueEx("NewAIN", DeviceInfo.AIN) And
                   .TryGetValueEx("NewDeviceId", DeviceInfo.DeviceId) And
                   .TryGetValueEx("NewFunctionBitMask", DeviceInfo.FunctionBitMask) And
                   .TryGetValueEx("NewFirmwareVersion", DeviceInfo.FirmwareVersion) And
                   .TryGetValueEx("NewManufacturer", DeviceInfo.Manufacturer) And
                   .TryGetValueEx("NewProductName", DeviceInfo.ProductName) And
                   .TryGetValueEx("NewDeviceName", DeviceInfo.DeviceName) And
                   .TryGetValueEx("NewPresent", DeviceInfo.Present) And
                   .TryGetValueEx("NewMultimeterIsEnabled", DeviceInfo.MultimeterIsEnabled) And
                   .TryGetValueEx("NewMultimeterIsValid", DeviceInfo.MultimeterIsValid) And
                   .TryGetValueEx("NewMultimeterPower", DeviceInfo.MultimeterPower) And
                   .TryGetValueEx("NewMultimeterEnergy", DeviceInfo.MultimeterEnergy) And
                   .TryGetValueEx("NewTemperatureIsEnabled", DeviceInfo.TemperatureIsEnabled) And
                   .TryGetValueEx("NewTemperatureIsValid", DeviceInfo.TemperatureIsValid) And
                   .TryGetValueEx("NewTemperatureCelsius", DeviceInfo.TemperatureCelsius) And
                   .TryGetValueEx("NewTemperatureOffset", DeviceInfo.TemperatureOffset) And
                   .TryGetValueEx("NewSwitchIsEnabled", DeviceInfo.SwitchIsEnabled) And
                   .TryGetValueEx("NewSwitchIsValid", DeviceInfo.SwitchIsValid) And
                   .TryGetValueEx("NewSwitchState", DeviceInfo.SwitchState) And
                   .TryGetValueEx("NewSwitchMode", DeviceInfo.SwitchMode) And
                   .TryGetValueEx("NewSwitchLock", DeviceInfo.SwitchLock) And
                   .TryGetValueEx("NewHkrIsEnabled", DeviceInfo.HkrIsEnabled) And
                   .TryGetValueEx("NewHkrIsValid", DeviceInfo.HkrIsValid) And
                   .TryGetValueEx("NewHkrIsTemperature", DeviceInfo.HkrIsTemperature) And
                   .TryGetValueEx("NewHkrSetVentilStatus", DeviceInfo.HkrSetVentilStatus) And
                   .TryGetValueEx("NewHkrSetTemperature", DeviceInfo.HkrSetTemperature) And
                   .TryGetValueEx("NewHkrReduceVentilStatus", DeviceInfo.HkrReduceVentilStatus) And
                   .TryGetValueEx("NewHkrReduceTemperature", DeviceInfo.HkrReduceTemperature) And
                   .TryGetValueEx("NewHkrComfortVentilStatus", DeviceInfo.HkrComfortVentilStatus) And
                   .TryGetValueEx("NewHkrComfortTemperature", DeviceInfo.HkrComfortTemperature)

        End With

    End Function

    Public Function GetSpecificDeviceInfos(AIN As String, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean Implements IX_homeautoSCPD.GetSpecificDeviceInfos

        If DeviceInfo Is Nothing Then DeviceInfo = New HomeAutoDeviceInfo

        With TR064Start(ServiceFile, "GetSpecificDeviceInfos", New Dictionary(Of String, String) From {{"NewAIN", AIN}})

            DeviceInfo.AIN = AIN

            Return .TryGetValueEx("NewDeviceId", DeviceInfo.DeviceId) And
                   .TryGetValueEx("NewFunctionBitMask", DeviceInfo.FunctionBitMask) And
                   .TryGetValueEx("NewFirmwareVersion", DeviceInfo.FirmwareVersion) And
                   .TryGetValueEx("NewManufacturer", DeviceInfo.Manufacturer) And
                   .TryGetValueEx("NewProductName", DeviceInfo.ProductName) And
                   .TryGetValueEx("NewDeviceName", DeviceInfo.DeviceName) And
                   .TryGetValueEx("NewPresent", DeviceInfo.Present) And
                   .TryGetValueEx("NewMultimeterIsEnabled", DeviceInfo.MultimeterIsEnabled) And
                   .TryGetValueEx("NewMultimeterIsValid", DeviceInfo.MultimeterIsValid) And
                   .TryGetValueEx("NewMultimeterPower", DeviceInfo.MultimeterPower) And
                   .TryGetValueEx("NewMultimeterEnergy", DeviceInfo.MultimeterEnergy) And
                   .TryGetValueEx("NewTemperatureIsEnabled", DeviceInfo.TemperatureIsEnabled) And
                   .TryGetValueEx("NewTemperatureIsValid", DeviceInfo.TemperatureIsValid) And
                   .TryGetValueEx("NewTemperatureCelsius", DeviceInfo.TemperatureCelsius) And
                   .TryGetValueEx("NewTemperatureOffset", DeviceInfo.TemperatureOffset) And
                   .TryGetValueEx("NewSwitchIsEnabled", DeviceInfo.SwitchIsEnabled) And
                   .TryGetValueEx("NewSwitchIsValid", DeviceInfo.SwitchIsValid) And
                   .TryGetValueEx("NewSwitchState", DeviceInfo.SwitchState) And
                   .TryGetValueEx("NewSwitchMode", DeviceInfo.SwitchMode) And
                   .TryGetValueEx("NewSwitchLock", DeviceInfo.SwitchLock) And
                   .TryGetValueEx("NewHkrIsEnabled", DeviceInfo.HkrIsEnabled) And
                   .TryGetValueEx("NewHkrIsValid", DeviceInfo.HkrIsValid) And
                   .TryGetValueEx("NewHkrIsTemperature", DeviceInfo.HkrIsTemperature) And
                   .TryGetValueEx("NewHkrSetVentilStatus", DeviceInfo.HkrSetVentilStatus) And
                   .TryGetValueEx("NewHkrSetTemperature", DeviceInfo.HkrSetTemperature) And
                   .TryGetValueEx("NewHkrReduceVentilStatus", DeviceInfo.HkrReduceVentilStatus) And
                   .TryGetValueEx("NewHkrReduceTemperature", DeviceInfo.HkrReduceTemperature) And
                   .TryGetValueEx("NewHkrComfortVentilStatus", DeviceInfo.HkrComfortVentilStatus) And
                   .TryGetValueEx("NewHkrComfortTemperature", DeviceInfo.HkrComfortTemperature)
        End With
    End Function

    Public Function SetDeviceName(AIN As String, DeviceName As String) As Boolean Implements IX_homeautoSCPD.SetDeviceName
        Return Not TR064Start(ServiceFile, "SetDeviceName", New Dictionary(Of String, String) From {{"NewAIN", AIN},
                                                                                                    {"NewDeviceName", DeviceName}}).ContainsKey("Error")
    End Function

    Public Function SetSwitch(AIN As String, SwitchState As SwStateEnum) As Boolean Implements IX_homeautoSCPD.SetSwitch
        Return Not TR064Start(ServiceFile, "SetSwitch", New Dictionary(Of String, String) From {{"NewAIN", AIN},
                                                                                                {"NewSwitchState", SwitchState}}).ContainsKey("Error")
    End Function
End Class
