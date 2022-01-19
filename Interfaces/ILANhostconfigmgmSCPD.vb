''' <summary>
''' TR-064 Support – LANHostConfigManagement1
''' Date: 2015-11-20 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanhostconfigmgmSCPD.pdf</see>
''' </summary>
Public Interface ILANhostconfigmgmSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As LANInfo) As Boolean

    Function SetDHCPServerEnable(DHCPServerEnable As Boolean) As Boolean

    Function SetSubnetMask(SubnetMask As String) As Boolean

    Function GetSubnetMask(ByRef SubnetMask As String) As Boolean

    Function SetIPRouter(IPRouters As String) As Boolean

    Function GetIPRoutersList(ByRef IPRouters As String) As Boolean

    ''' <param name="Enable">Only 'true' supported</param>
    ''' <param name="IPAddressingType">Only 'Static' supported</param>
    Function SetIPInterface(Enable As Boolean,
                            IPAddress As String,
                            SubnetMask As String,
                            IPAddressingType As String) As Boolean

    Function GetAddressRange(ByRef MinAddress As String,
                             ByRef MaxAddress As String) As Boolean

    Function SetAddressRange(MinAddress As String,
                             MaxAddress As String) As Boolean

    Function GetIPInterfaceNumberOfEntries(ByRef IPInterfaceNumberOfEntries As Integer) As Boolean

    Function GetDNSServer(ByRef DNSServers As String) As Boolean

End Interface
