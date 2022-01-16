''' <summary>
''' TR-064 Support – WAN Ethernet Link Config
''' Date:: 2009-07-15 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wanethlinkconfigSCPD.pdf</see>
''' </summary>
Friend Class WANEthernetLinkConfigSCPD
    Implements IWANEthernetLinkConfigSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWANEthernetLinkConfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wanpppconnSCPD Implements IWANEthernetLinkConfigSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetEthernetLinkStatus(ByRef EthernetLinkStatus As EthernetLinkStatusEnum) As Object Implements IWANEthernetLinkConfigSCPD.GetEthernetLinkStatus
        Return TR064Start(ServiceFile, "GetEthernetLinkStatus", Nothing).TryGetValue("NewEthernetLinkStatus", EthernetLinkStatus)
    End Function
End Class
