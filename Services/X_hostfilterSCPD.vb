''' <summary>
''' TR-064 Support – X_AVM-DE_HostFilter
''' Date:  2020-04-01
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_hostfilterSCPD.pdf</see>
''' </summary>
Public Class X_hostfilterSCPD
    Implements IX_hostfilterSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_hostfilterSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_hostfilterSCPD Implements IX_hostfilterSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function DisallowWANAccessByIP(IPv4Address As String, Disallow As Boolean) As Boolean Implements IX_hostfilterSCPD.DisallowWANAccessByIP
        Return Not TR064Start(ServiceFile, "DisallowWANAccessByIP", New Dictionary(Of String, String) From {{"NewIPv4Address", IPv4Address},
                                                                                                            {"NewDisallow", Disallow.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function DiscardAllTickets() As Boolean Implements IX_hostfilterSCPD.DiscardAllTickets
        Return Not TR064Start(ServiceFile, "DiscardAllTickets", Nothing).ContainsKey("Error")
    End Function

    Public Function GetTicketIDStatus(TicketID As String, ByRef TicketIDStatus As TicketIDStatusEnum) As Boolean Implements IX_hostfilterSCPD.GetTicketIDStatus
        Return TR064Start(ServiceFile, "GetTicketIDStatus", New Dictionary(Of String, String) From {{"NewTicketID", TicketID}}).TryGetValue("NewTicketIDStatus", TicketIDStatus)
    End Function

    Public Function GetWANAccessByIP(IPv4Address As String, ByRef WANAccess As WANAccessEnum, ByRef Disallow As Boolean) As Boolean Implements IX_hostfilterSCPD.GetWANAccessByIP
        With TR064Start(ServiceFile, "GetWANAccessByIP", New Dictionary(Of String, String) From {{"NewIPv4Address", IPv4Address}})
            Return .TryGetValue("NewWANAccess", WANAccess) And
                   .TryGetValue("NewDisallow", Disallow)
        End With
    End Function

    Public Function MarkTicket(ByRef TicketID As String) As Boolean Implements IX_hostfilterSCPD.MarkTicket
        Return TR064Start(ServiceFile, "MarkTicket", Nothing).TryGetValue("NewTicketIDStatus", TicketID)
    End Function
End Class
