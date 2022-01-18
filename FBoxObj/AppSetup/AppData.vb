Public Class AppData
    ''' <summary>
    ''' Unique identifier of the app instance (unique within the single box).
    ''' </summary>
    Public Property AppId As String

    ''' <summary>
    ''' User friendly display name of the app instance.
    ''' </summary>
    Public Property AppDisplayName As String

    ''' <summary>
    ''' MAC address of the device (or device interface). Empty string means: unknown
    ''' </summary>
    Public Property AppDeviceMAC As String

    ''' <summary>
    ''' Username for the app instance.
    ''' This must be unique (case insensitive) among all usernames used for authentication at the box
    ''' (e.g. name and email addresses of box users and usernames of other app instances).<br/>
    ''' Allowed characters: a-z, A-Z, 0-9<br/>
    ''' Must not begin with a digit.<br/>
    ''' Length: 1-32 characters
    ''' </summary>
    Public Property AppUsername As String

    ''' <summary>
    ''' Password for the app instance. Username and password can be used by the app as credentials to authenticate at the following services of the box: HTTPS, TR-064, FTPS.<br/>
    ''' Allowed characters: ASCII 32 – ASCII 126<br/>
    ''' Length: 8-32 characters<br/>
    ''' Strength: must contain at least one digit, one uppercase letter, one lowercase letter and one other (special) character. OPEN 
    ''' </summary>
    Public Property AppPassword As String

    ''' <summary>
    ''' FRITZ!OS app specific configuration right. 
    ''' </summary>
    Public Property AppRight As RightEnum

    ''' <summary>
    ''' FRITZ!OS NAS specific right.
    ''' </summary>
    Public Property NasRight As RightEnum

    ''' <summary>
    ''' FRITZ!OS phone specific right. 
    ''' </summary>
    Public Property PhoneRight As RightEnum

    ''' <summary>
    ''' FRITZ!OS home automation specific right
    ''' </summary>
    Public Property HomeautoRight As RightEnum

    ''' <summary>
    ''' Does the app instance want access the box from the internet?
    ''' </summary>
    Public Property AppInternetRights As Boolean

End Class
