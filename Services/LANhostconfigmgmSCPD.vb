''' <summary>
''' TR-064 Support – LANHostConfigManagement1
''' Date: 2015-11-20 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanhostconfigmgmSCPD.pdf</see>
''' </summary>
Friend Class LANhostconfigmgmSCPD
    Implements ILANhostconfigmgmSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements ILANhostconfigmgmSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.lanhostconfigmgmSCPD Implements ILANhostconfigmgmSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Info As LANInfo) As Boolean Implements ILANhostconfigmgmSCPD.GetInfo
        If Info Is Nothing Then Info = New LANInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewDHCPServerConfigurable", Info.DHCPServerConfigurable) And
                   .TryGetValueEx("NewDHCPRelay", Info.DHCPRelay) And
                   .TryGetValueEx("NewMinAddress", Info.MinAddress) And
                   .TryGetValueEx("NewMaxAddress", Info.MaxAddress) And
                   .TryGetValueEx("NewReservedAddresses", Info.ReservedAddresses) And
                   .TryGetValueEx("NewDHCPServerEnable", Info.DHCPServerEnable) And
                   .TryGetValueEx("NewDNSServers", Info.DNSServers) And
                   .TryGetValueEx("NewDomainName", Info.DomainName) And
                   .TryGetValueEx("NewIPRouters", Info.IPRouters) And
                   .TryGetValueEx("NewSubnetMask", Info.SubnetMask)

        End With
    End Function

    Public Function SetDHCPServerEnable(DHCPServerEnable As Boolean) As Boolean Implements ILANhostconfigmgmSCPD.SetDHCPServerEnable
        Return Not TR064Start(ServiceFile, "SetDHCPServerEnable", New Dictionary(Of String, String) From {{"NewDHCPServerEnable", DHCPServerEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetSubnetMask(SubnetMask As String) As Boolean Implements ILANhostconfigmgmSCPD.SetSubnetMask
        Return Not TR064Start(ServiceFile, "SetSubnetMask", New Dictionary(Of String, String) From {{"NewSubnetMask", SubnetMask}}).ContainsKey("Error")
    End Function

    Public Function GetSubnetMask(ByRef SubnetMask As String) As Boolean Implements ILANhostconfigmgmSCPD.GetSubnetMask
        Return TR064Start(ServiceFile, "GetSubnetMask", Nothing).TryGetValueEx("NewSubnetMask", SubnetMask)
    End Function

    Public Function SetIPRouter(IPRouters As String) As Boolean Implements ILANhostconfigmgmSCPD.SetIPRouter
        Return Not TR064Start(ServiceFile, "SetIPRouter", New Dictionary(Of String, String) From {{"NewIPRouters", IPRouters}}).ContainsKey("Error")
    End Function

    Public Function GetIPRoutersList(ByRef IPRouters As String) As Boolean Implements ILANhostconfigmgmSCPD.GetIPRoutersList
        Return TR064Start(ServiceFile, "GetIPRoutersList", Nothing).TryGetValueEx("NewIPRouters", IPRouters)
    End Function

    Public Function SetIPInterface(Enable As Boolean, IPAddress As String, SubnetMask As String, IPAddressingType As String) As Boolean Implements ILANhostconfigmgmSCPD.SetIPInterface
        Return Not TR064Start(ServiceFile, "SetIPInterface",
                              New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr},
                                                                      {"NewIPAddress", IPAddress},
                                                                      {"NewSubnetMask", SubnetMask},
                                                                      {"NewIPAddressingType", IPAddressingType}}).ContainsKey("Error")


    End Function

    Public Function GetAddressRange(ByRef MinAddress As String, ByRef MaxAddress As String) As Boolean Implements ILANhostconfigmgmSCPD.GetAddressRange
        With TR064Start(ServiceFile, "GetAddressRange", Nothing)

            Return .TryGetValueEx("NewMinAddress", MinAddress) And
                   .TryGetValueEx("NewMaxAddress", MaxAddress)

        End With
    End Function

    Public Function SetAddressRange(MinAddress As String, MaxAddress As String) As Boolean Implements ILANhostconfigmgmSCPD.SetAddressRange
        Return Not TR064Start(ServiceFile, "SetAddressRange",
                              New Dictionary(Of String, String) From {{"NewMinAddress", MinAddress},
                                                                      {"NewMaxAddress", MaxAddress}}).ContainsKey("Error")


    End Function

    Public Function GetIPInterfaceNumberOfEntries(ByRef IPInterfaceNumberOfEntries As Integer) As Boolean Implements ILANhostconfigmgmSCPD.GetIPInterfaceNumberOfEntries
        Return TR064Start(ServiceFile, "GetIPInterfaceNumberOfEntries", Nothing).TryGetValueEx("NewIPInterfaceNumberOfEntries", IPInterfaceNumberOfEntries)
    End Function

    Public Function GetDNSServer(ByRef DNSServers As String) As Boolean Implements ILANhostconfigmgmSCPD.GetDNSServer
        Return TR064Start(ServiceFile, "GetDNSServer", Nothing).TryGetValueEx("NewDNSServers", DNSServers)
    End Function
End Class
