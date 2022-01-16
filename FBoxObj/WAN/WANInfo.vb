Public Class WANInfo
    Public Property Enable As Boolean
    Public Property ConnectionStatus As ConnectionStatusEnum
    Public Property PossibleConnectionTypes As ConnectionTypeEnum
    Public Property ConnectionType As ConnectionTypeEnum
    Public Property Name As String
    Public Property Uptime As Integer
    Public Property LastConnectionError As LastConnectionErrorEnum
    Public Property RSIPAvailable As Boolean
    Public Property NATEnabled As Boolean
    Public Property ExternalIPAddress As String
    Public Property DNSServers As String
    Public Property MACAddress As String
    Public Property ConnectionTrigger As String
    Public Property RouteProtocolRx As String
    Public Property DNSEnabled As Boolean
    Public Property DNSOverrideAllowed As Boolean
End Class
