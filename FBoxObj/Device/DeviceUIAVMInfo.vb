Public Class DeviceUIAVMInfo
    Public Property AutoUpdateMode As AutoUpdateModeEnum
    Public Property UpdateTime As String
    Public Property LastFwVersion As String

    ''' <summary>
    ''' Not supported.
    ''' </summary>
    ''' <returns>Returns Default string.</returns>
    Public Property LastInfoUrl As String
    Public Property CurrentFwVersion As String
    Public Property UpdateSuccessful As UpdateEnum

End Class
