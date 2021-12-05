Public Class WLANHybridMode
    Public Property Enable As Boolean
    ''' <summary>
    ''' None, Basic, WPA, 11i, WPAand11i, WPA3, 11iandWPA3, OWE, OWETrans
    ''' </summary>
    Public Property BeaconType As String
    Public Property KeyPassphrase As String
    Public Property SSID As String
    Public Property BSSID As String
    Public Property TrafficMode As String
    Public Property ManualSpeed As Boolean
    Public Property MaxSpeedDS As Integer
    Public Property MaxSpeedUS As Integer

End Class
