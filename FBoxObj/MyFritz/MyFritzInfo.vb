Public Class MyFritzInfo
    Public Property Index As Integer
    Public Property Enabled As String
    Public Property Name As String

    ''' <summary>
    ''' Example http:// 
    ''' </summary>
    Public Property Scheme As String
    Public Property Port As Integer
    Public Property URLPath As String
    Public Property Type As String

    ''' <summary>
    ''' <list type="bullet">
    ''' <item>0 – unknown</item>
    ''' <item>1 – IPv4Forwarding not necessary or succeeded</item>
    ''' <item>2 – IPv4Forwarding failed</item>
    ''' </list>
    ''' </summary>
    Public Property IPv4ForwardingWarning As Integer
    Public Property IPv4Addresses As String
    Public Property IPv6Addresses As String
    Public Property IPv6InterfaceIDs As String
    Public Property MACAddress As String
    Public Property HostName As String
    Public Property DynDnsLabel As String

    ''' <summary>
    ''' TODO values!
    ''' </summary>
    Public Property Status As Integer
End Class
