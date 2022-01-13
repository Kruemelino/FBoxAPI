Public Class DeviceInfo

    Public Property ManufacturerName As String
    Public Property ManufacturerOUI As String
    Public Property ModelName As String
    Public Property Description As String
    Public Property ProductClass As String
    Public Property SerialNumber As String
    Public Property SoftwareVersion As String
    Public Property HardwareVersion As String
    Public Property SpecVersion As String
    ''' <summary>
    ''' ddd.ddd.ddd.ddd, d == [0-9]
    ''' </summary>
    Public Property ProvisioningCode As String
    Public Property UpTime As Integer
    Public Property DeviceLog As String

End Class
