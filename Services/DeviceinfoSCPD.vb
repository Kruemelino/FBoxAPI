''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2009-7-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceinfoSCPD.pdf</see>
''' </summary>
Friend Class DeviceinfoSCPD
    Implements IDeviceinfoSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IDeviceinfoSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IDeviceinfoSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        ServiceFile = SCPDFiles.deviceinfoSCPD

        TR064Start = Start

    End Sub

#Region "deviceinfoSCPD"

    Public Function GetInfo(ByRef Info As DeviceInfo) As Boolean Implements IDeviceinfoSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewManufacturerName", Info.ManufacturerName) And
                   .TryGetValueEx("NewManufacturerOUI", Info.ManufacturerOUI) And
                   .TryGetValueEx("NewModelName", Info.ModelName) And
                   .TryGetValueEx("NewDescription", Info.Description) And
                   .TryGetValueEx("NewProductClass", Info.ProductClass) And
                   .TryGetValueEx("NewSerialNumber", Info.SerialNumber) And
                   .TryGetValueEx("NewSoftwareVersion", Info.SoftwareVersion) And
                   .TryGetValueEx("NewHardwareVersion", Info.HardwareVersion) And
                   .TryGetValueEx("NewSpecVersion", Info.SpecVersion) And
                   .TryGetValueEx("NewProvisioningCode", Info.ProvisioningCode) And
                   .TryGetValueEx("NewUpTime", Info.UpTime) And
                   .TryGetValueEx("NewDeviceLog", Info.DeviceLog)

        End With
    End Function

    ''' <param name="ProvisioningCode">ddd.ddd.ddd.ddd, d == [0-9] </param>
    Public Function SetProvisioningCode(ProvisioningCode As String) As Boolean Implements IDeviceinfoSCPD.SetProvisioningCode
        Return Not TR064Start(ServiceFile, "SetProvisioningCode", New Dictionary(Of String, String) From {{"NewProvisioningCode", ProvisioningCode}}).ContainsKey("Error")
    End Function

    Public Function GetDeviceLog(ByRef DeviceLog As String) As Boolean Implements IDeviceinfoSCPD.GetDeviceLog
        Return TR064Start(ServiceFile, "GetDeviceLog", Nothing).TryGetValueEx("NewDeviceLog", DeviceLog)
    End Function

    Public Function GetSecurityPort(ByRef SecurityPort As Integer) As Boolean Implements IDeviceinfoSCPD.GetSecurityPort
        Return TR064Start(ServiceFile, "GetSecurityPort", Nothing).TryGetValueEx("NewSecurityPort", SecurityPort)
    End Function
#End Region

End Class