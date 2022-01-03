''' <summary>
''' TR-064 Support – X_AVM-DE_HostFilter
''' Date: 2020-04-01
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_hostfilterSCPD.pdf</see>
''' </summary>
Public Interface IX_hostfilterSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' In addition to filter profile settings, Disallow is a boolean configuration value to disallow WAN access of a LAN device.
    ''' Have in mind that even if the state is set to "0", the LAN device may still have no WAN access because Of filter profile.
    ''' Setting the Disallow state variable is an asynchronous process, therefore it may need some time to take effect.
    ''' </summary>
    Function DisallowWANAccessByIP(IPv4Address As String, Disallow As Boolean) As Boolean

    ''' <summary>
    ''' Because we have a limit of 10 TickedIDs which can be "marked" (retrieved via MarkTicketID) at the same time, 
    ''' it could be necessary to invalidate all TicketIDs and create a new List of "unmarked" TicketIDs.
    ''' </summary>
    Function DiscardAllTickets() As Boolean

    ''' <summary>
    ''' Returns the state of the TicketID.
    ''' </summary>
    ''' <param name="TicketID">Numerical string of 6 character length e.g. "123456"</param>
    Function GetTicketIDStatus(TicketID As String, ByRef TicketIDStatus As TicketIDStatusEnum) As Boolean

    ''' <summary>
    ''' Returns the state of WANAccess for the given LAN device’s IP address. The value of
    ''' WANAccess represents the state of WAN access derived by configuration settings of
    ''' Disallow (see 2.1 <see cref="DisallowWANAccessByIP(String, Boolean)"/>) and the LAN device’s filter profile.
    ''' </summary>
    Function GetWANAccessByIP(IPv4Address As String, ByRef WANAccess As WANAccessEnum, ByRef Disallow As Boolean) As Boolean

    ''' <summary>
    ''' Get a TicketID and set their state to "marked". Only "unmarked" TicketIDs can be retrieved.
    ''' The number of TicketIDs which can be "marked" at the same time is limited to 10. If the return code 714 is retrieved, no "unmarked" TicketID is available.
    ''' If a new TicketID is needed, it is necessary to discard all (old) TicketIDs (see 2.2 <see cref="DiscardAllTickets()"/>) or activate at least one of them.
    ''' </summary>
    ''' <param name="TicketID">Numerical string of 6 character length e.g. "123456"</param>
    Function MarkTicket(ByRef TicketID As String) As Boolean

End Interface