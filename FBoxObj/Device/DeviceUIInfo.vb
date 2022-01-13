Public Class DeviceUIInfo
    Public Property UpgradeAvailable As Boolean
    Public Property PasswordRequired As Boolean
    Public Property PasswordUserSelectable As Boolean

    ''' <summary>
    ''' Not supported.
    ''' </summary>
    ''' <returns>Returns Default string.</returns>
    Public Property WarrantyDate As String
    Public Property Version As String
    Public Property DownloadURL As String
    Public Property InfoURL As String
    Public Property UpdateState As UpdateStateEnum
    Public Property LaborVersion As String
End Class
