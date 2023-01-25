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

    ''' <summary>
    ''' Release, Intern, Work, Personal, Modified, Inhaus, Labor_Beta, Labor_RC, Labor_DSL, Labor_Phone, Labor, Labor_Test, Labor_Plus
    ''' </summary>
    Public Property BuildType As String
    Public Property SetupAssistantStatus As Boolean
End Class
