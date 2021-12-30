''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2009-7-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceinfoSCPD.pdf</see>
''' </summary>
Friend Class DeviceinfoSCPD
    Implements IDeviceinfoSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IDeviceinfoSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IDeviceinfoSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IDeviceinfoSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.deviceinfoSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

#Region "deviceinfoSCPD"

    Public Function GetInfo(ByRef Info As DeviceInfo) As Boolean Implements IDeviceinfoSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewSoftwareVersion") Then

                Info.ManufacturerName = .Item("NewManufacturerName").ToString
                Info.ManufacturerOUI = .Item("NewManufacturerOUI").ToString
                Info.ModelName = .Item("NewModelName").ToString
                Info.Description = .Item("NewDescription").ToString
                Info.ProductClass = .Item("NewProductClass").ToString
                Info.SerialNumber = .Item("NewSerialNumber").ToString
                Info.SoftwareVersion = .Item("NewSoftwareVersion").ToString
                Info.HardwareVersion = .Item("NewHardwareVersion").ToString
                Info.SpecVersion = .Item("NewSpecVersion").ToString
                Info.ProvisioningCode = .Item("NewProvisioningCode").ToString
                Info.UpTime = CInt(.Item("NewUpTime"))
                Info.DeviceLog = .Item("NewDeviceLog").ToString

                PushStatus.Invoke(LogLevel.Debug, $"Geräteinformationen der Fritz!Box: {Info.Description}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"Keine Geräteinformationen der Fritz!Box erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    ''' <param name="ProvisioningCode">ddd.ddd.ddd.ddd, d == [0-9] </param>
    Public Function SetProvisioningCode(ProvisioningCode As String) As Boolean Implements IDeviceinfoSCPD.SetProvisioningCode
        With TR064Start(ServiceFile, "SetProvisioningCode", New Hashtable From {{"NewProvisioningCode", ProvisioningCode}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetDeviceLog(ByRef DeviceLog As String) As Boolean Implements IDeviceinfoSCPD.GetDeviceLog
        With TR064Start(ServiceFile, "GetDeviceLog", Nothing)
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

    Public Function GetSecurityPort(ByRef SecurityPort As Integer) As Boolean Implements IDeviceinfoSCPD.GetSecurityPort
        With TR064Start(ServiceFile, "GetSecurityPort", Nothing)
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