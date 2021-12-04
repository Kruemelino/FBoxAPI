''' <summary>
''' TR-064 Support – Homeauto
''' Date: 2019-04-29
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeauto.pdf"/>
''' </summary>
Public Class X_homeautoSCPD
    Implements IX_homeauto

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_homeauto.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_homeauto.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_homeauto.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_homeautoSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetInfo(ByRef AllowedCharsAIN As String, ByRef MaxCharsAIN As Integer, ByRef MinCharsAIN As Integer, ByRef MaxCharsDeviceName As Integer, ByRef MinCharsDeviceName As Integer) As Boolean Implements IX_homeauto.GetInfo

        With TR064Start(ServiceFile, "GetMessageList", Nothing)
            If .ContainsKey("NewAllowedCharsAIN") Then

                AllowedCharsAIN = .Item("NewAllowedCharsAIN").ToString
                MaxCharsAIN = .Item("MaxCharsAIN").ToString
                MinCharsAIN = .Item("MinCharsAIN").ToString
                MaxCharsDeviceName = .Item("MaxCharsDeviceName").ToString
                MinCharsDeviceName = .Item("MinCharsDeviceName").ToString

                PushStatus.Invoke(LogLevel.Debug, $"GetInfo (HomeAuto) erfolgreich")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo (HomeAuto) konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetGenericDeviceInfos(Index As Integer, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean Implements IX_homeauto.GetGenericDeviceInfos

        If DeviceInfo Is Nothing Then DeviceInfo = New HomeAutoDeviceInfo

        With TR064Start(ServiceFile, "GetGenericDeviceInfos", New Hashtable From {{"NewIndex", Index}})

            If .ContainsKey("NewAIN") Then

                DeviceInfo.AIN = .Item("NewAIN").ToString
                DeviceInfo.DeviceId = CInt(.Item("NewDeviceId"))
                DeviceInfo.FunctionBitMask = CInt(.Item("NewFunctionBitMask"))
                DeviceInfo.FirmwareVersion = .Item("NewFirmwareVersion").ToString
                DeviceInfo.Manufacturer = .Item("NewManufacturer").ToString
                DeviceInfo.ProductName = .Item("NewProductName").ToString
                DeviceInfo.DeviceName = .Item("NewDeviceName").ToString
                DeviceInfo.Present = CType(.Item("NewPresent"), PresentEnum)
                DeviceInfo.MultimeterIsEnabled = CType(.Item("NewMultimeterIsEnabled"), EnabledEnum)
                DeviceInfo.MultimeterIsValid = CType(.Item("NewMultimeterIsValid"), ValidEnum)
                DeviceInfo.MultimeterPower = CInt(.Item("NewMultimeterPower"))
                DeviceInfo.MultimeterEnergy = CInt(.Item("NewMultimeterEnergy"))
                DeviceInfo.TemperatureIsEnabled = CType(.Item("NewTemperatureIsEnabled"), EnabledEnum)
                DeviceInfo.TemperatureIsValid = CType(.Item("NewTemperatureIsValid"), ValidEnum)
                DeviceInfo.TemperatureCelsius = CInt(.Item("NewTemperatureCelsius"))
                DeviceInfo.TemperatureOffset = CInt(.Item("NewTemperatureOffset"))
                DeviceInfo.SwitchIsEnabled = CType(.Item("NewSwitchIsEnabled"), EnabledEnum)
                DeviceInfo.SwitchIsValid = CType(.Item("NewSwitchIsValid"), ValidEnum)
                DeviceInfo.SwitchState = CType(.Item("NewSwitchState"), SwStateEnum)
                DeviceInfo.SwitchMode = CType(.Item("NewSwitchMode"), SwModeEnum)
                DeviceInfo.SwitchLock = CBool(.Item("NewSwitchLock"))
                DeviceInfo.HkrIsEnabled = CType(.Item("NewHkrIsEnabled"), EnabledEnum)
                DeviceInfo.HkrIsValid = CType(.Item("NewHkrIsValid"), ValidEnum)
                DeviceInfo.HkrIsTemperature = CInt(.Item("NewHkrIsTemperature"))
                DeviceInfo.HkrSetVentilStatus = CType(.Item("NewHkrSetVentilStatus"), VentilEnum)
                DeviceInfo.HkrSetTemperature = CInt(.Item("NewHkrSetTemperature"))
                DeviceInfo.HkrReduceVentilStatus = CType(.Item("NewHkrReduceVentilStatus"), VentilEnum)
                DeviceInfo.HkrReduceTemperature = CInt(.Item("NewHkrReduceTemperature"))
                DeviceInfo.HkrComfortVentilStatus = CType(.Item("NewHkrComfortVentilStatus"), VentilEnum)
                DeviceInfo.HkrComfortTemperature = CInt(.Item("NewHkrComfortTemperature"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericDeviceInfos konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetSpecificDeviceInfos(AIN As String, ByRef DeviceInfo As HomeAutoDeviceInfo) As Boolean Implements IX_homeauto.GetSpecificDeviceInfos

        If DeviceInfo Is Nothing Then DeviceInfo = New HomeAutoDeviceInfo

        With TR064Start(ServiceFile, "GetSpecificDeviceInfos", New Hashtable From {{"NewAIN", AIN}})

            If .ContainsKey("NewDeviceId") Then

                DeviceInfo.AIN = AIN
                DeviceInfo.DeviceId = CInt(.Item("NewDeviceId"))
                DeviceInfo.FunctionBitMask = CInt(.Item("NewFunctionBitMask"))
                DeviceInfo.FirmwareVersion = .Item("NewFirmwareVersion").ToString
                DeviceInfo.Manufacturer = .Item("NewManufacturer").ToString
                DeviceInfo.ProductName = .Item("NewProductName").ToString
                DeviceInfo.DeviceName = .Item("NewDeviceName").ToString
                DeviceInfo.Present = CType(.Item("NewPresent"), PresentEnum)
                DeviceInfo.MultimeterIsEnabled = CType(.Item("NewMultimeterIsEnabled"), EnabledEnum)
                DeviceInfo.MultimeterIsValid = CType(.Item("NewMultimeterIsValid"), ValidEnum)
                DeviceInfo.MultimeterPower = CInt(.Item("NewMultimeterPower"))
                DeviceInfo.MultimeterEnergy = CInt(.Item("NewMultimeterEnergy"))
                DeviceInfo.TemperatureIsEnabled = CType(.Item("NewTemperatureIsEnabled"), EnabledEnum)
                DeviceInfo.TemperatureIsValid = CType(.Item("NewTemperatureIsValid"), ValidEnum)
                DeviceInfo.TemperatureCelsius = CInt(.Item("NewTemperatureCelsius"))
                DeviceInfo.TemperatureOffset = CInt(.Item("NewTemperatureOffset"))
                DeviceInfo.SwitchIsEnabled = CType(.Item("NewSwitchIsEnabled"), EnabledEnum)
                DeviceInfo.SwitchIsValid = CType(.Item("NewSwitchIsValid"), ValidEnum)
                DeviceInfo.SwitchState = CType(.Item("NewSwitchState"), SwStateEnum)
                DeviceInfo.SwitchMode = CType(.Item("NewSwitchMode"), SwModeEnum)
                DeviceInfo.SwitchLock = CBool(.Item("NewSwitchLock"))
                DeviceInfo.HkrIsEnabled = CType(.Item("NewHkrIsEnabled"), EnabledEnum)
                DeviceInfo.HkrIsValid = CType(.Item("NewHkrIsValid"), ValidEnum)
                DeviceInfo.HkrIsTemperature = CInt(.Item("NewHkrIsTemperature"))
                DeviceInfo.HkrSetVentilStatus = CType(.Item("NewHkrSetVentilStatus"), VentilEnum)
                DeviceInfo.HkrSetTemperature = CInt(.Item("NewHkrSetTemperature"))
                DeviceInfo.HkrReduceVentilStatus = CType(.Item("NewHkrReduceVentilStatus"), VentilEnum)
                DeviceInfo.HkrReduceTemperature = CInt(.Item("NewHkrReduceTemperature"))
                DeviceInfo.HkrComfortVentilStatus = CType(.Item("NewHkrComfortVentilStatus"), VentilEnum)
                DeviceInfo.HkrComfortTemperature = CInt(.Item("NewHkrComfortTemperature"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificDeviceInfos konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetDeviceName(AIN As String, DeviceName As String) As Boolean Implements IX_homeauto.SetDeviceName
        With TR064Start(ServiceFile, "SetDeviceName", New Hashtable From {{"NewAIN", AIN}, {"NewDeviceName", DeviceName}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function SetSwitch(AIN As String, SwitchState As SwStateEnum) As Boolean Implements IX_homeauto.SetSwitch
        With TR064Start(ServiceFile, "SetSwitch", New Hashtable From {{"NewAIN", AIN}, {"NewSwitchState", SwitchState.ToString}})
            Return Not .ContainsKey("Error")
        End With
    End Function
End Class
