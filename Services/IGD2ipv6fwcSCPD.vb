''' <summary>
''' TR-064 Support – Internet Gateway Device Support
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/IGD1.pdf</see>
''' </summary>
''' <remarks>
''' Based on the Internet Gateway Device (IGD) V1.0 and Internet Gateway Device (IGD) V2.0 
''' specification proposed by UpnP™ Forum at <see href="http://upnp.org/specs/gw/igd1/"/> and 
''' <see href="http://upnp.org/specs/gw/igd2/."/><br/>
''' All information is based on the FRITZ!OS 6.93.<br/>
''' WANIPv6FirewallControl:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPv6FirewallControl-v1-Service.pdf"/>
''' </remarks> 
Public Class IGD2ipv6fwcSCPD
    Implements IIGD2ipv6fwcSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 20) Implements IIGD2ipv6fwcSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IIGD2ipv6fwcSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.igd2ipv6fwcSCPD Implements IIGD2ipv6fwcSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetFirewallStatus(ByRef FirewallEnabled As Boolean, ByRef InboundPinholeAllowed As Boolean) As Boolean Implements IIGD2ipv6fwcSCPD.GetFirewallStatus

        With TR064Start(ServiceFile, "GetFirewallStatus", Nothing)

            Return .TryGetValueEx("FirewallEnabled", FirewallEnabled) And
                   .TryGetValueEx("InboundPinholeAllowed", InboundPinholeAllowed)
        End With

    End Function

    Public Function GetOutboundPinholeTimeout(RemoteHost As String, RemotePort As Integer, InternalClient As String, InternalPort As Integer, Protocol As Integer, ByRef OutboundPinholeTimeout As Integer) As Boolean Implements IIGD2ipv6fwcSCPD.GetOutboundPinholeTimeout

        Return TR064Start(ServiceFile, "GetFirewallStatus", New Dictionary(Of String, String) From {{"RemoteHost", RemoteHost},
                                                                                                    {"RemotePort", RemotePort.ToString},
                                                                                                    {"InternalClient", InternalClient},
                                                                                                    {"InternalPort", InternalPort.ToString},
                                                                                                    {"Protocol", Protocol.ToString}}).TryGetValueEx("OutboundPinholeTimeout", OutboundPinholeTimeout)


    End Function

    Public Function AddPinhole(RemoteHost As String, RemotePort As Integer, InternalClient As String, InternalPort As Integer, Protocol As Integer, LeaseTime As Integer, ByRef UniqueID As Integer) As Boolean Implements IIGD2ipv6fwcSCPD.AddPinhole
        Return TR064Start(ServiceFile, "AddPinhole", New Dictionary(Of String, String) From {{"RemoteHost", RemoteHost},
                                                                                             {"RemotePort", RemotePort.ToString},
                                                                                             {"InternalClient", InternalClient},
                                                                                             {"InternalPort", InternalPort.ToString},
                                                                                             {"LeaseTime", LeaseTime.ToString},
                                                                                             {"Protocol", Protocol.ToString}}).TryGetValueEx("UniqueID", UniqueID)

    End Function

    Public Function UpdatePinhole(UniqueID As Integer, NewLeaseTime As Integer) As Boolean Implements IIGD2ipv6fwcSCPD.UpdatePinhole
        Return Not TR064Start(ServiceFile, "UpdatePinhole", New Dictionary(Of String, String) From {{"UniqueID", UniqueID.ToString}, {"NewLeaseTime", NewLeaseTime.ToString}}).ContainsKey("Error")
    End Function

    Public Function DeletePinhole(UniqueID As Integer) As Boolean Implements IIGD2ipv6fwcSCPD.DeletePinhole
        Return Not TR064Start(ServiceFile, "DeletePinhole", New Dictionary(Of String, String) From {{"UniqueID", UniqueID.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetPinholePackets(UniqueID As Integer, ByRef PinholePackets As Integer) As Boolean Implements IIGD2ipv6fwcSCPD.GetPinholePackets
        Return TR064Start(ServiceFile, "GetPinholePackets", New Dictionary(Of String, String) From {{"UniqueID", UniqueID.ToString}}).TryGetValueEx("PinholePackets", PinholePackets)
    End Function

    Public Function CheckPinholeWorking(UniqueID As Integer, ByRef IsWorking As Boolean) As Boolean Implements IIGD2ipv6fwcSCPD.CheckPinholeWorking
        Return TR064Start(ServiceFile, "CheckPinholeWorking", New Dictionary(Of String, String) From {{"UniqueID", UniqueID.ToString}}).TryGetValueEx("IsWorking", IsWorking)
    End Function
End Class
