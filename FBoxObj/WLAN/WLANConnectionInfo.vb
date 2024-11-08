Public Class WLANConnectionInfo
    Public Property AssociatedDeviceMACAddress As String
    Public Property SSID As String
    Public Property BSSID As String

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
    Public Property AutoChannelEnabled As Boolean
    Public Property ChannelWidth As Integer
    Public Property FrequencyBand As String
    Public Property SignalStrength As Integer
    Public Property Speed As Integer
    Public Property SpeedRX As Integer
    Public Property SpeedMax As Integer
    Public Property SpeedRXMax As Integer

    ''' <summary>
    ''' Lower case MAC address or “00:00:00:00:00:00”, if no MLO connection is used<br/>
    ''' It is the client device MLD MAC
    ''' </summary>
    Public Property MLDMACAddress As String

    ''' <summary>
    ''' Lower case MAC address or “00:00:00:00:00:00”, if MLO is not supported<br/>
    ''' It is the access point MLD MAC
    ''' </summary>
    Public Property APMLDMACAddress As String
    Public Property MLOModes As MLOModes

    ''' <summary>
    ''' XML formatted list of additional MLO connections
    ''' </summary>
    Public Property MLOList As String

End Class
