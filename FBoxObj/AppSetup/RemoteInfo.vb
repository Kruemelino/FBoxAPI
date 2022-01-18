Public Class RemoteInfo
    ''' <summary>
    ''' Subnetmask of the local CPE network
    ''' </summary>
    Public Property SubnetMask As String

    ''' <summary>
    ''' Local IP address of the CPE device
    ''' </summary>
    Public Property IPAddress As String

    ''' <summary>
    ''' IP address of the WAN interface 
    ''' </summary>
    Public Property ExternalIPAddress As String

    ''' <summary>
    ''' IPv6 address of the WAN interface
    ''' </summary>
    Public Property ExternalIPv6Address As String

    ''' <summary>
    ''' Shows if a specific DynDNS is activated 
    ''' </summary>
    Public Property RemoteAccessDDNSEnabled As Boolean

    ''' <summary>
    ''' Domain of the DynDNS 
    ''' </summary>
    Public Property RemoteAccessDDNSDomain As String

    ''' <summary>
    ''' Shows if the MyFritz of AVM GmbH is activated
    ''' </summary>
    Public Property MyFritzEnabled As Boolean

    ''' <summary>
    ''' MyFritz URL
    ''' </summary>
    Public Property MyFritzDynDNSName As String

End Class
