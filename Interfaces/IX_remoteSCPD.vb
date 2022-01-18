''' <summary>
''' TR-064 Support – X AVM Remote Access
''' Date: 2017-11-22 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_remoteSCPD.pdf</see>
''' </summary>
Public Interface IX_remoteSCPD
    Inherits IServiceBase

    ''' <param name="Username">The username may be empty if no user has been configured with box configuration rights from internet.<br/>
    ''' The username may be an email address if configured. </param>
    Function GetInfo(ByRef Enabled As Boolean,
                     ByRef Port As Integer,
                     ByRef Username As String) As Boolean

    ''' <summary>
    ''' Configure one user for remote access from internet. 
    ''' The user will have box configuration rights from internet on success. 
    ''' An existing user which has been used last for this service can be renamed by this action. 
    ''' </summary>
    ''' <param name="Enabled">A disabled user with the matching name will be enabled.</param>
    ''' <param name="Port">The argument Port value must be in the ranges 1 - 65535.</param>
    ''' <param name="Username">The argument Username may be an username or an email address. </param>
    Function SetConfig(Enabled As Boolean,
                       Port As Integer,
                       Username As String,
                       Password As String) As Boolean

    ''' <summary>
    ''' Gets the DDNS Info.
    ''' </summary>
    ''' <remarks>Required rights: CONFIG or APP or HOMEAUTO or PHONE or NAS</remarks>
    Function GetDDNSInfo(ByRef Info As DDNSInfo) As Boolean

    Function GetDDNSProviders(ByRef ProviderList As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: GetDDNSProviders wird als <see cref="ProviderList"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetDDNSProviders(ByRef List As ProviderList) As Boolean

    ''' <summary>
    ''' To configure dynamic DNS the value of NewProviderName has to match an existing ProviderName in Item retrieved by GetDDNSProviders.
    ''' A user defined configuration uses the localized ProviderName e.g. in German "Benutzerdefiniert". 
    ''' To use HTTP with SSL the value of NewUpdateURL has to look like <c>https://&lt;server&gt;:&lt;sslport&gt;/path?arg1=xxx</c>.<br/>
    ''' If the value of NewUpdateURL does not begin with https:// or http:// HTTP without SSL is used.<br/>
    ''' The value of NewUpdateURL requires a path containing at least one slash.<br/>
    ''' </summary>
    Function SetDDNSConfig(Info As DDNSInfo, Password As String) As Boolean

    ''' <remarks>Required rights: CONFIG</remarks>
    Function SetEnable(Enabled As Boolean, ByRef Port As Integer) As Boolean
End Interface
