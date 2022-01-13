Public Class WLANInfo
    Public Property Enable As Boolean
    ''' <summary>
    ''' Up, Disabled 
    ''' </summary>

    Public Property Status As String
    ''' <summary>
    ''' 0 means auto channel
    ''' </summary>

    Public Property MaxBitRate As String

    Public Property Channel As String

    Public Property SSID As String

    ''' <summary>
    ''' None, Basic, WPA, 11i, WPAand11i, WPA3, 11iandWPA3, OWE, OWETrans
    ''' </summary>
    Public Property BeaconType As String

    ''' <summary>
    ''' Comma separated string of possible BeaconTypes. e.g. "WPA,None,WPAand11i"
    ''' </summary>
    Public Property PossibleBeaconTypes As String()

    Public Property MACAddressControlEnabled As Boolean

    ''' <summary>
    ''' b, g, n, ac, ax, ""
    ''' </summary>
    ''' <remarks>Only the highest of the active modes is returned.</remarks>
    Public Property Standard As String

    Public Property BSSID As String

    Public Property BasicEncryptionModes As String

    Public Property BasicAuthenticationMode As String

    ''' <summary>
    ''' 32
    ''' </summary>
    Public Property MaxCharsSSID As Integer

    ''' <summary>
    ''' 3
    ''' </summary>
    Public Property MinCharsSSID As Integer

    ''' <summary>
    ''' 01234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz !"#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~(*) 
    ''' </summary>
    Public Property AllowedCharsSSID As String

    ''' <summary>
    ''' 64
    ''' </summary>
    Public Property MinCharsPSK As Integer

    ''' <summary>
    ''' 64
    ''' </summary>
    Public Property MaxCharsPSK As Integer

    ''' <summary>
    ''' 01234567890ABCDEFabcdef
    ''' </summary>
    Public Property AllowedCharsPSK As String
End Class
