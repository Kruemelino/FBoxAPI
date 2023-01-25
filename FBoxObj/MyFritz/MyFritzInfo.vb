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
    ''' 0 Fritz!Box is not registered<br/>
    ''' 1 Fritz!Box is registered, but MyFRITZ is disabled<br/>
    ''' 10 Device register failed<br/>
    ''' 99 Fritz!Box is registered, device will be registered<br/>
    ''' 200 DynDNS update is running<br/>
    ''' 250 Unknown DynDNS update error<br/>
    ''' 251 DynDNS update error (authentication error)<br/>
    ''' 252 DynDNS update error (no internet)<br/>
    ''' 253 DynDNS update error (not reachable)<br/>
    ''' 254 DynDNS update error (bad reply)<br/>
    ''' 255 DynDNS update error (update failed)<br/>
    ''' 300 DynDNS successfully updated, IP validation is running<br/>
    ''' 301 DynDNS successfully updated, IP is validated
    ''' </summary>
    Public Property Status As Integer
End Class
