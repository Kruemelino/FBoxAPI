Public Class WLANConnectionInfo
    Public Property AssociatedDeviceMACAddress As String
    Public Property SSID As String
    Public Property BSSID As Boolean

    ''' <summary>
    ''' None, Basic, WPA, 11i, WPAand11i, WPA3, 11iandWPA3, OWE, OWETrans
    ''' </summary>
    Public Property BeaconType As String
    Public Property Channel As String

    ''' <summary>
    ''' b, g, n, ac, ax, ""
    ''' </summary>
    ''' <remarks>Only the highest of the active modes is returned.</remarks>
    Public Property Standard As String
    Public Property SignalStrength As Integer
    Public Property Speed As Integer
    Public Property SpeedRX As Integer
    Public Property SpeedMax As Integer
    Public Property SpeedRXMax As Integer

End Class
