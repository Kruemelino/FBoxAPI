Public Class DeviceInfo

    Friend Property ManufacturerName As String
    Friend Property ManufacturerOUI As String
    Friend Property ModelName As String
    Friend Property Description As String
    Friend Property ProductClass As String
    Friend Property SerialNumber As String
    Friend Property SoftwareVersion As String
    Friend Property HardwareVersion As String
    Friend Property SpecVersion As String
    ''' <summary>
    ''' ddd.ddd.ddd.ddd, d == [0-9]
    ''' </summary>
    Friend Property ProvisioningCode As String
    Friend Property UpTime As Integer
    Friend Property DeviceLog As String

End Class
