Public Class DeviceinfoSCPD
    Implements IService
    Private Property TR064Start As Func(Of String, String, Hashtable, Hashtable) Implements IService.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IService.PushStatus

    Public Sub New(Start As Func(Of String, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        TR064Start = Start

        PushStatus = Status
    End Sub

#Region "deviceinfoSCPD"
    Friend Function GetInfo(Optional ByRef ManufacturerName As String = "",
                            Optional ByRef ManufacturerOUI As String = "",
                            Optional ByRef ModelName As String = "",
                            Optional ByRef Description As String = "",
                            Optional ByRef ProductClass As String = "",
                            Optional ByRef SerialNumber As String = "",
                            Optional ByRef SoftwareVersion As String = "",
                            Optional ByRef HardwareVersion As String = "",
                            Optional ByRef SpecVersion As String = "",
                            Optional ByRef ProvisioningCode As String = "",
                            Optional ByRef UpTime As Integer = 0,
                            Optional ByRef DeviceLog As String = "") As Boolean

        With TR064Start(Tr064Files.deviceinfoSCPD, "GetInfo", Nothing)

            If .ContainsKey("NewSoftwareVersion") Then

                ManufacturerName = .Item("NewManufacturerName").ToString
                ManufacturerOUI = .Item("NewManufacturerOUI").ToString
                ModelName = .Item("NewModelName").ToString
                Description = .Item("NewDescription").ToString
                ProductClass = .Item("NewProductClass").ToString
                SerialNumber = .Item("NewSerialNumber").ToString
                SoftwareVersion = .Item("NewSoftwareVersion").ToString
                HardwareVersion = .Item("NewHardwareVersion").ToString
                SpecVersion = .Item("NewSpecVersion").ToString
                ProvisioningCode = .Item("NewProvisioningCode").ToString
                UpTime = CInt(.Item("NewUpTime"))
                DeviceLog = .Item("NewDeviceLog").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Geräteinformationen der Fritz!Box: {Description}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Keine Geräteinformationen der Fritz!Box erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    ''' <param name="ProvisioningCode">ddd.ddd.ddd.ddd, d == [0-9] </param>
    Friend Function SetProvisioningCode(ProvisioningCode As String) As Boolean
        With TR064Start(Tr064Files.deviceinfoSCPD, "SetProvisioningCode", New Hashtable From {{"NewProvisioningCode", ProvisioningCode}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Friend Function GetDeviceLog(ByRef DeviceLog As String) As Boolean
        With TR064Start(Tr064Files.deviceinfoSCPD, "GetDeviceLog", Nothing)
            If .ContainsKey("NewDeviceLog") Then

                DeviceLog = .Item("NewDeviceLog").ToString

                PushStatus.Invoke(LogLevel.Debug, $"DeviceLog der Fritz!Box: {DeviceLog}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"DeviceLog der Fritz!Box nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Friend Function GetSecurityPort(ByRef SecurityPort As Integer) As Boolean
        With TR064Start(Tr064Files.deviceinfoSCPD, "GetSecurityPort", Nothing)
            If .ContainsKey("NewSecurityPort") Then

                SecurityPort = CInt(.Item("NewSecurityPort"))

                PushStatus.Invoke(LogLevel.Debug, $"SecurityPort der Fritz!Box: {SecurityPort}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"SecurityPort der Fritz!Box nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
#End Region

End Class