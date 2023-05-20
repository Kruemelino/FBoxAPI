''' <summary>
''' TR-064 Support – Internet Gateway Device Support
''' Date: 2023-01-20
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/IGD2.pdf</see>
''' </summary>
''' <remarks>
''' BBased on the Internet Gateway Device (IGD) V2.0 specification proposed by UpnP™ Forum at
''' <see href="http://upnp.org/specs/gw/igd2/."/><br/>
''' All information is based on the FRITZ!OS 6.93.<br/>
''' WANIPConnection:1<br/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPConnection-v1-Service.pdf"/>
''' <seealso href="http://upnp.org/specs/gw/UPnP-gw-WANIPConnection-v2-Service.pdf"/>
''' </remarks> 
Public Interface IIGD2connSCPD
    Inherits IIGD1connSCPD

#Region "WANIPConnection:1"
    ''' <summary>
    ''' This action sets the time (in seconds) after which an active connection is automatically disconnected.
    ''' </summary>
    ''' <param name="AutoDisconnectTime">This argument sets the autodisconnect time for the connection</param>
    Function SetAutoDisconnectTime(AutoDisconnectTime As Integer) As Boolean

    ''' <summary>
    ''' This action specifies the idle time (in seconds) after which a connection MAY be disconnected.
    ''' </summary>
    ''' <param name="IdleDisconnectTime">This argument set the time of the connection idle before the connection is terminated.</param>
    Function SetIdleDisconnectTime(IdleDisconnectTime As Integer) As Boolean

    ''' <summary>
    ''' This action specifies the number of seconds of warning to each (potentially) active user of a connection before a connection is terminated.
    ''' </summary>
    ''' <param name="WarnDisconnectDelay">This argument sets the time when IGD is expected to warn before a connection is terminated.</param>
    Function SetWarnDisconnectDelay(WarnDisconnectDelay As Integer) As Boolean

    ''' <summary>
    ''' This action retrieves the values of WarnDisconnectDelay. This value indicates how long before a disconnection, the user is warned.
    ''' </summary>
    ''' <param name="WarnDisconnectDelay">This argument returns the value of WarnDisconnectDelay state variable.</param>
    Function GetWarnDisconnectDelay(ByRef WarnDisconnectDelay As Integer) As Boolean

#End Region

#Region "WANIPConnection:2"
    ''' <summary>
    ''' his action deletes port mapping entries defined by a range. As the range is deleted, the array is compacted, 
    ''' the evented variable PortMappingNumberOfEntries is decremented, and the evented variable SystemUpdateID is incremented. 
    ''' When issued, this action will remove all port mapping entries between NewStartPort and NewEndPort.<br/>
    ''' The NewManage argument is used to describe the intent of this action:
    ''' <list type="bullet">
    ''' <item>If NewManage is set to “0” (false), then the gateway SHALL only remove port mappings having the InternalClient value matching IP address of the control point,</item>
    ''' <item>If NewManage is set to “1” (true), the gateway SHALL remove all port mappings between NewStartPort and NewEndPort values.</item>
    ''' </list>
    ''' </summary>
    ''' <param name="Protocol">This string variable represents the protocol of the port mapping. Possible values are TCP or UDP.</param>
    ''' <param name="Manage">This argument type is used to describe management intent when issuing certain actions with elevated level of access. 
    ''' The type of this argument is boolean</param>
    Function DeletePortMappingRange(StartPort As Integer,
                                    EndPort As Integer,
                                    Protocol As PortMappingProtocolEnum,
                                    Manage As Boolean) As Boolean
    ''' <summary>
    ''' This action returns a list of port mappings matching the arguments. The operation of this action has two modes depending on NewManage value
    ''' <list type="bullet">
    ''' <item>If NewManage is set to “0” (false), then the gateway SHALL only remove port mappings having the InternalClient value matching IP address of the control point,</item>
    ''' <item>If NewManage is set to “1” (true), the gateway SHALL remove all port mappings between NewStartPort and NewEndPort values.</item>
    ''' </list>
    ''' With the argument NewNumberOfPorts, a control point MAY limit the size of the list returned in order to limit the length of the list returned. 
    ''' If NewNumberOfPorts is equal to 0, then the gateway MUST return all port mappings between NewStartPort and NewEndPort
    ''' </summary>
    Function GetListOfPortMappings(StartPort As Integer,
                                   EndPort As Integer,
                                   Protocol As PortMappingProtocolEnum,
                                   Manage As Boolean,
                                   NumberOfPorts As Integer,
                                   ByRef PortListing As String) As Boolean
    Function AddAnyPortMapping(NewPortMappingEntry As PortMappingEntry,
                               ByRef ReservedPort As Integer) As Boolean
#End Region

End Interface
