''' <summary>
''' TR-064 Support – X_AVM-DE_WebDAVClient  
''' Date: 2020-09-03
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_appsetup.pdf</see>
''' </summary>
Public Interface IX_appsetupSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Returns a list of all message-filters configured for an app instance.
    ''' </summary>
    ''' <remarks>Required rights: any<br/>
    ''' The security context of this action must belong to an app instance. 
    ''' The app instance of the security context must the same as the one identified by the parameter AppId.</remarks>
    ''' <param name="AppId">Identifier of the app instance</param>
    ''' <param name="FilterList">The result of action in a XML format</param>
    Function GetAppMessageFilter(AppId As String, ByRef FilterList As String) As Boolean

    ''' <summary>
    ''' Return information which is needed for apps to have access to the FRITZ!Box via WAN.<br/>
    ''' Legacy behavior:<br/>
    ''' Either ExternalIPAddress or ExternalIPv6Address has returned a valid address. If WAN interface has both addresses, only IPv4 was returned.<br/>
    ''' Changed behavior:<br/>
    ''' If WAN interface has IPv4 and IPv6 addresses, both parameter return a valid address.<br/>
    ''' E.g. to create and handle a vpn connection.
    ''' </summary>
    ''' <remarks>Required rights: Config or App</remarks>
    Function GetAppRemoteInfo(ByRef Info As RemoteInfo) As Boolean

    ''' <summary>
    ''' This action queries the current access rights of the current TR-064 security context. This security context results from the authentication of a box user or an app instance.
    ''' </summary>
    ''' <remarks>Required rights: any</remarks>
    Function GetConfig(ByRef Rights As ConfigRights) As Boolean

    ''' <summary>
    ''' Read restrictive values for action parameters.
    ''' </summary>
    ''' <remarks>Required rights: any</remarks>
    Function GetInfo(ByRef Info As AppInfo) As Boolean

    ''' <summary>
    ''' This action registers a new app instance. A new app instance is created if an app instance with identifier AppId does not already exists. 
    ''' Otherwise the existing app instance is overwritten (any VoIP and VPN configuration of this app instance is deleted).  
    ''' <list type="bullet">
    ''' <item>Required rights: C or P or N or H (only allowed from home network)</item>
    ''' <item>The security context of this action must belong to a box user (no app instance allowed).</item>
    ''' <item>In case AppInternetRights is true and the app instance will get access rights from the internet and new port forwardings must be (automatically) activated (e.g. for HTTPS and FTP) configuration rights (write access) are required.</item>
    ''' <item>This action must be called from within the home network (from internet not allowed).</item>
    ''' <item>The effective access rights of the app instance are determined by the intersection of these rights and the rights of the box user of the security context this action was called in.
    ''' Specific NAS directory access rights are not included in this parameter but determined by the box user only. </item>
    ''' </list>
    ''' </summary>
    Function RegisterApp(App As AppData) As Boolean

    ''' <summary>
    ''' Reset an event specified by a event ID. If more than one event with the same ID exist, only one event is reset.<br/> 
    ''' This action must be called from within the home network (from internet not allowed).</summary>
    ''' <remarks>Required rights: configuration</remarks>
    Function ResetEvent(EventId As Integer) As Boolean

    ''' <summary>
    ''' Multiple message filters of different types can be added to each app instance. 
    ''' If an app instance already contains a filter with the same type, it is overwritten. 
    ''' A filter consists of the type and a list of filter criteria. 
    ''' If an empty list of filter criteria is passed, the filter of this type is completely removed from the app configuration.<br/>
    ''' So far, the following filter criteria are specified:
    ''' <list type="bullet">
    ''' <item><term>Type</term> aha_ident (blacklist filter)</item>
    ''' <item><term>Filter</term> Comma-separated list of AHA identifiers (eg. ain, aha_group_id, mac or vendor specific identifier)</item>
    ''' <item><term>Type</term> tel_local_number (blacklist filter)</item>
    ''' <item><term>Filter</term> Comma-separated list of phone numbers</item>
    ''' </list>
    ''' </summary>
    ''' <remarks>Required rights: any<br/>
    ''' The security context of this action <b>must belong to an app instance</b>. The app instance of the security context must the same as the one identified by the parameter AppId.
    ''' </remarks>
    ''' <param name="AppId">Identifier of the app instance the message filter belongs to.</param>
    ''' <param name="Type">Type of the message filter, eg. “aha_ident”</param>
    ''' <param name="Filter">A stringlist of filter criteria (e.g. “08761 0000444,34:45:12:43:55” for type aha_ident)<br/>
    ''' Allowed characters: a-Z,0-9, Space and ,+:-_</param>
    Function SetAppMessageFilter(AppId As String, Type As String, Filter As String) As Boolean

    ''' <summary>
    ''' Configuration of a message receiver for the app instance. 
    ''' Every app instance can have at most only one message receiver configuration. 
    ''' In case a message receiver already exists for the app instance, the old configuration is overwritten (deleted).<br/>
    ''' This action must be called from within the home network (from internet not allowed).</summary>
    ''' <remarks>
    ''' Required rights: any<br/>
    ''' The security context of this action must belong to an app instance. 
    ''' The app instance of the security context must the same as the one identified by the parameter AppId.
    ''' </remarks>
    ''' <param name="AppId">Identifier of the app instance the message belongs to.</param>
    ''' <param name="CryptAlgos">Comma separated list of additional crypt algorithms the app understands beside AES128-CBC-HMAC-SHA-256. 
    ''' If no other crypt algorithms are supported this parameter can be left blank. 
    ''' Naming according to RFC7518 (JWA).</param>
    ''' <param name="AppAVMAddress">"App-AVM-Address" of the app Instance. 
    ''' An empty string means that the app instance will no longer receive any messages from this box (message receiver delete operation). 
    ''' The app gets this value from the AVM message relay web service.</param>
    ''' <param name="AppAVMPasswordHash">BASE64URL encoding (without padding) of first 16 Bytes of the SHA-256 hash of the app's “App-AVMPassword”. 
    ''' The app gets this value from the AVM message relay web service.</param>
    ''' <param name="EncryptionSecret">Shared secret used to build the crypt key for encryption and authentication for messages from the box to the app.</param>
    ''' <param name="BoxSenderId">Sender Id used in messages from this box to the app. 
    ''' The app can associate this BoxSenderId to the EncryptionKey to find the needed key to decrypt a received message from a box.</param>
    Function SetAppMessageReceiver(AppId As String,
                                   CryptAlgos As String,
                                   AppAVMAddress As String,
                                   AppAVMPasswordHash As String,
                                   ByRef EncryptionSecret As String,
                                   ByRef BoxSenderId As String) As Boolean

    ''' <summary>
    ''' Configuration of a VPN (IPsec) access for the app instance. 
    ''' Every app instance can have at most only one VPN access configuration. 
    ''' In case a VPN access already exists for the app instance, the old configuration is overwritten. 
    ''' In case a VPN access already exists and all IPSec parameter are empty, the existing VPN configuration will be deleted. 
    ''' </summary>
    ''' <param name="AppId">Identifier of the app instance where the VPN configuration belongs to.</param>
    ''' <param name="IPSecIdentifier">IPSec identifier</param>
    ''' <param name="IPSecPreSharedKey">IPSec pre-shared-key</param>
    ''' <param name="IPSecXauthUsername">Username for xauth</param>
    ''' <param name="IPSecXauthPassword">Password for xauth</param>
    ''' <remarks>Required rights: configuration (write access).<br/>
    ''' NOTE that an app instance never has any configuration rights, so <b>box user credentials must be used</b> for TR-064 authentication to perform this action.</remarks>
    Function SetAppVPN(AppId As String,
                       IPSecIdentifier As String,
                       IPSecPreSharedKey As String,
                       IPSecXauthUsername As String,
                       IPSecXauthPassword As String) As Boolean

    ''' <summary>
    ''' Configuration of a VPN (IPsec) access for the app instance with PFS (Perfect Forward Secrecy) support.
    ''' Every app instance can have at most only one VPN access configuration.
    ''' In case a VPN access already exists for the app instance, the old configuration is overwritten. 
    ''' In case a VPN access already exists and all IPSec parameter are empty, the existing VPN configuration will be deleted. 
    ''' </summary>
    ''' <param name="AppId">Identifier of the app instance where the VPN configuration belongs to.</param>
    ''' <param name="IPSecIdentifier">IPSec identifier</param>
    ''' <param name="IPSecPreSharedKey">IPSec pre-shared-key</param>
    ''' <param name="IPSecXauthUsername">Username for xauth</param>
    ''' <param name="IPSecXauthPassword">Password for xauth</param>
    ''' <remarks>Required rights: configuration (write access).<br/>
    ''' NOTE that an app instance never has any configuration rights, so <b>box user credentials must be used</b> for TR-064 authentication to perform this action.</remarks>
    Function SetAppVPNwithPFS(AppId As String,
                              IPSecIdentifier As String,
                              IPSecPreSharedKey As String,
                              IPSecXauthUsername As String,
                              IPSecXauthPassword As String) As Boolean


End Interface
