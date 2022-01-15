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

            Return .TryGetValue("NewAllowedCharsAIN", AllowedCharsAIN) And
                   .TryGetValue("MaxCharsAIN", MaxCharsAIN) And
                   .TryGetValue("MinCharsAIN", MinCharsAIN) And
                   .TryGetValue("MaxCharsDeviceName", MaxCharsDeviceName) And
                   .TryGetValue("MinCharsDeviceName", MinCharsDeviceName)

        End With

    End Function

    Public Function GetGenericDeviceInfos(Index As Integer, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean Implements IX_homeautoSCPD.GetGenericDeviceInfos

        If DeviceInfo Is Nothing Then DeviceInfo = New HomeAutoDeviceInfo

        With TR064Start(ServiceFile, "GetGenericDeviceInfos", New Dictionary(Of String, String) From {{"NewIndex", Index}})

            Return .TryGetValue("NewAIN", DeviceInfo.AIN) And
                   .TryGetValue("NewDeviceId", DeviceInfo.DeviceId) And
                   .TryGetValue("NewFunctionBitMask", DeviceInfo.FunctionBitMask) And
                   .TryGetValue("NewFirmwareVersion", DeviceInfo.FirmwareVersion) And
                   .TryGetValue("NewManufacturer", DeviceInfo.Manufacturer) And
                   .TryGetValue("NewProductName", DeviceInfo.ProductName) And
                   .TryGetValue("NewDeviceName", DeviceInfo.DeviceName) And
                   .TryGetValue("NewPresent", DeviceInfo.Present) And
                   .TryGetValue("NewMultimeterIsEnabled", DeviceInfo.MultimeterIsEnabled) And
                   .TryGetValue("NewMultimeterIsValid", DeviceInfo.MultimeterIsValid) And
                   .TryGetValue("NewMultimeterPower", DeviceInfo.MultimeterPower) And
                   .TryGetValue("NewMultimeterEnergy", DeviceInfo.MultimeterEnergy) And
                   .TryGetValue("NewTemperatureIsEnabled", DeviceInfo.TemperatureIsEnabled) And
                   .TryGetValue("NewTemperatureIsValid", DeviceInfo.TemperatureIsValid) And
                   .TryGetValue("NewTemperatureCelsius", DeviceInfo.TemperatureCelsius) And
                   .TryGetValue("NewTemperatureOffset", DeviceInfo.TemperatureOffset) And
                   .TryGetValue("NewSwitchIsEnabled", DeviceInfo.SwitchIsEnabled) And
                   .TryGetValue("NewSwitchIsValid", DeviceInfo.SwitchIsValid) And
                   .TryGetValue("NewSwitchState", DeviceInfo.SwitchState) And
                   .TryGetValue("NewSwitchMode", DeviceInfo.SwitchMode) And
                   .TryGetValue("NewSwitchLock", DeviceInfo.SwitchLock) And
                   .TryGetValue("NewHkrIsEnabled", DeviceInfo.HkrIsEnabled) And
                   .TryGetValue("NewHkrIsValid", DeviceInfo.HkrIsValid) And
                   .TryGetValue("NewHkrIsTemperature", DeviceInfo.HkrIsTemperature) And
                   .TryGetValue("NewHkrSetVentilStatus", DeviceInfo.HkrSetVentilStatus) And
                   .TryGetValue("NewHkrSetTemperature", DeviceInfo.HkrSetTemperature) And
                   .TryGetValue("NewHkrReduceVentilStatus", DeviceInfo.HkrReduceVentilStatus) And
                   .TryGetValue("NewHkrReduceTemperature", DeviceInfo.HkrReduceTemperature) And
                   .TryGetValue("NewHkrComfortVentilStatus", DeviceInfo.HkrComfortVentilStatus) And
                   .TryGetValue("NewHkrComfortTemperature", DeviceInfo.HkrComfortTemperature)

        End With

    End Function

    Public Function GetSpecificDeviceInfos(AIN As String, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean Implements IX_homeautoSCPD.GetSpecificDeviceInfos

        If DeviceInfo Is Nothing Then DeviceInfo = New HomeAutoDeviceInfo

        With TR064Start(ServiceFile, "GetSpecificDeviceInfos", New Dictionary(Of String, String) From {{"NewAIN", AIN}})

            DeviceInfo.AIN = AIN

            Return .TryGetValue("NewDeviceId", DeviceInfo.DeviceId) And
                   .TryGetValue("NewFunctionBitMask", DeviceInfo.FunctionBitMask) And
                   .TryGetValue("NewFirmwareVersion", DeviceInfo.FirmwareVersion) And
                   .TryGetValue("NewManufacturer", DeviceInfo.Manufacturer) And
                   .TryGetValue("NewProductName", DeviceInfo.ProductName) And
                   .TryGetValue("NewDeviceName", DeviceInfo.DeviceName) And
                   .TryGetValue("NewPresent", DeviceInfo.Present) And
                   .TryGetValue("NewMultimeterIsEnabled", DeviceInfo.MultimeterIsEnabled) And
                   .TryGetValue("NewMultimeterIsValid", DeviceInfo.MultimeterIsValid) And
                   .TryGetValue("NewMultimeterPower", DeviceInfo.MultimeterPower) And
                   .TryGetValue("NewMultimeterEnergy", DeviceInfo.MultimeterEnergy) And
                   .TryGetValue("NewTemperatureIsEnabled", DeviceInfo.TemperatureIsEnabled) And
                   .TryGetValue("NewTemperatureIsValid", DeviceInfo.TemperatureIsValid) And
                   .TryGetValue("NewTemperatureCelsius", DeviceInfo.TemperatureCelsius) And
                   .TryGetValue("NewTemperatureOffset", DeviceInfo.TemperatureOffset) And
                   .TryGetValue("NewSwitchIsEnabled", DeviceInfo.SwitchIsEnabled) And
                   .TryGetValue("NewSwitchIsValid", DeviceInfo.SwitchIsValid) And
                   .TryGetValue("NewSwitchState", DeviceInfo.SwitchState) And
                   .TryGetValue("NewSwitchMode", DeviceInfo.SwitchMode) And
                   .TryGetValue("NewSwitchLock", DeviceInfo.SwitchLock) And
                   .TryGetValue("NewHkrIsEnabled", DeviceInfo.HkrIsEnabled) And
                   .TryGetValue("NewHkrIsValid", DeviceInfo.HkrIsValid) And
                   .TryGetValue("NewHkrIsTemperature", DeviceInfo.HkrIsTemperature) And
                   .TryGetValue("NewHkrSetVentilStatus", DeviceInfo.HkrSetVentilStatus) And
                   .TryGetValue("NewHkrSetTemperature", DeviceInfo.HkrSetTemperature) And
                   .TryGetValue("NewHkrReduceVentilStatus", DeviceInfo.HkrReduceVentilStatus) And
                   .TryGetValue("NewHkrReduceTemperature", DeviceInfo.HkrReduceTemperature) And
                   .TryGetValue("NewHkrComfortVentilStatus", DeviceInfo.HkrComfortVentilStatus) And
                   .TryGetValue("NewHkrComfortTemperature", DeviceInfo.HkrComfortTemperature)
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
