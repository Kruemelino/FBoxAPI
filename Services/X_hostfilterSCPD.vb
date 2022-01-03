''' <summary>
''' TR-064 Support – X_AVM-DE_HostFilter
''' Date:  2020-04-01
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_hostfilterSCPD.pdf</see>
''' </summary>
Public Class X_hostfilterSCPD
    Implements IX_hostfilterSCPD
    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_hostfilterSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_hostfilterSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_hostfilterSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_hostfilterSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function DisallowWANAccessByIP(IPv4Address As String, Disallow As Boolean) As Boolean Implements IX_hostfilterSCPD.DisallowWANAccessByIP
        With TR064Start(ServiceFile, "DisallowWANAccessByIP", New Hashtable From {{"NewIPv4Address", IPv4Address},
                                                                                  {"NewDisallow", Disallow.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DiscardAllTickets() As Boolean Implements IX_hostfilterSCPD.DiscardAllTickets
        With TR064Start(ServiceFile, "DiscardAllTickets", Nothing)
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetTicketIDStatus(TicketID As String, ByRef TicketIDStatus As TicketIDStatusEnum) As Boolean Implements IX_hostfilterSCPD.GetTicketIDStatus
        With TR064Start(ServiceFile, "GetTicketIDStatus", New Hashtable From {{"NewTicketID", TicketID}})

            If .ContainsKey("NewTicketIDStatus") Then

                TicketIDStatus = CType(.Item("NewTicketIDStatus"), TicketIDStatusEnum)

                PushStatus.Invoke(LogLevel.Info, $"GetTicketIDStatus {TicketID}: '{TicketIDStatus}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTicketIDStatus konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetWANAccessByIP(IPv4Address As String, ByRef WANAccess As WANAccessEnum, ByRef Disallow As Boolean) As Boolean Implements IX_hostfilterSCPD.GetWANAccessByIP
        With TR064Start(ServiceFile, "GetWANAccessByIP", New Hashtable From {{"NewIPv4Address", IPv4Address}})

            If .ContainsKey("NewWANAccess") And .ContainsKey("NewDisallow") Then

                WANAccess = CType(.Item("NewWANAccess"), WANAccessEnum)
                Disallow = CBool(.Item("NewDisallow"))

                PushStatus.Invoke(LogLevel.Info, $"GetWANAccessByIP {IPv4Address}: '{WANAccess}', Disallow: {Disallow}")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetWANAccessByIP konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function MarkTicket(ByRef TicketID As String) As Boolean Implements IX_hostfilterSCPD.MarkTicket
        With TR064Start(ServiceFile, "MarkTicket", Nothing)

            If .ContainsKey("NewTicketID") Then

                TicketID = .Item("NewTicketIDStatus").ToString

                PushStatus.Invoke(LogLevel.Info, $"MarkTicket: '{TicketID}'")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"MarkTicket konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
End Class
