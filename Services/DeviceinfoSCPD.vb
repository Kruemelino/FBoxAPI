﻿''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2022-02-16
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/deviceinfoSCPD.pdf</see>
''' </summary>
Friend Class DeviceinfoSCPD
    Implements IDeviceinfoSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 16) Implements IDeviceinfoSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IDeviceinfoSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IDeviceinfoSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IDeviceinfoSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        ServiceFile = SCPDFiles.deviceinfoSCPD

        TR064Start = Start

    End Sub

#Region "deviceinfoSCPD"

    Public Function GetInfo(ByRef Info As DeviceInfo) As Boolean Implements IDeviceinfoSCPD.GetInfo
        If Info Is Nothing Then Info = New DeviceInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

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
        Return Not TR064Start(ServiceFile, "SetProvisioningCode", ServiceID, New Dictionary(Of String, String) From {{"NewProvisioningCode", ProvisioningCode}}).ContainsKey("Error")
    End Function

    Public Function GetDeviceLog(ByRef DeviceLog As String) As Boolean Implements IDeviceinfoSCPD.GetDeviceLog
        Return TR064Start(ServiceFile, "GetDeviceLog", ServiceID, Nothing).TryGetValueEx("NewDeviceLog", DeviceLog)
    End Function

    Public Function GetSecurityPort(ByRef SecurityPort As Integer) As Boolean Implements IDeviceinfoSCPD.GetSecurityPort
        Return TR064Start(ServiceFile, "GetSecurityPort", ServiceID, Nothing).TryGetValueEx("NewSecurityPort", SecurityPort)
    End Function
#End Region

End Class