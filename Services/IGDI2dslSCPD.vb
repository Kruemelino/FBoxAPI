''' <summary>
''' TR-064 Support – Internet Gateway Device Support
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/IGD2.pdf</see>
''' </summary>
''' <remarks>
''' BBased on the Internet Gateway Device (IGD) V2.0 specification proposed by UpnP™ Forum at
''' <see href="http://upnp.org/specs/gw/igd2/."/><br/>
''' All information is based on the FRITZ!OS 6.93.<br/>
''' WANDSLLinkConfig:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANDSLLinkConfig-v1-Service.pdf"/>
''' </remarks> 
Public Class IGDI2dslSCPD
    Implements IIGDI2dslSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 20) Implements IIGDI2dslSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IIGDI2dslSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.igd2dslSCPD Implements IIGDI2dslSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IIGDI2dslSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetDSLLinkInfo(ByRef LinkType As LinkTypeEnum, LinkStatus As LinkStatusEnum) As Boolean Implements IIGDI1dslSCPD.GetDSLLinkInfo
        With TR064Start(ServiceFile, "GetDSLLinkInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewLinkType", LinkType) And
                   .TryGetValueEx("NewLinkStatus", LinkStatus)
        End With
    End Function

    Public Function GetAutoConfig(ByRef AutoConfig As Boolean) As Boolean Implements IIGDI1dslSCPD.GetAutoConfig
        Return TR064Start(ServiceFile, "GetAutoConfig", ServiceID, Nothing).TryGetValueEx("NewAutoConfig", AutoConfig)
    End Function

    Public Function GetModulationType(ByRef ModulationType As String) As Boolean Implements IIGDI1dslSCPD.GetModulationType
        Return TR064Start(ServiceFile, "GetModulationType", ServiceID, Nothing).TryGetValueEx("NewModulationType", ModulationType)
    End Function

    Public Function GetDestinationAddress(ByRef DestinationAddress As String) As Boolean Implements IIGDI1dslSCPD.GetDestinationAddress
        Return TR064Start(ServiceFile, "GetDestinationAddress", ServiceID, Nothing).TryGetValueEx("NewDestinationAddress", DestinationAddress)
    End Function

    Public Function GetATMEncapsulation(ByRef ATMEncapsulation As ATMEncapsulationEnum) As Boolean Implements IIGDI1dslSCPD.GetATMEncapsulation
        Return TR064Start(ServiceFile, "GetATMEncapsulation", ServiceID, Nothing).TryGetValueEx("NewATMEncapsulation", ATMEncapsulation)
    End Function

    Public Function GetFCSPreserved(ByRef FCSPreserved As Boolean) As Boolean Implements IIGDI1dslSCPD.GetFCSPreserved
        Return TR064Start(ServiceFile, "GetFCSPreserved", ServiceID, Nothing).TryGetValueEx("NewFCSPreserved", FCSPreserved)
    End Function
End Class
