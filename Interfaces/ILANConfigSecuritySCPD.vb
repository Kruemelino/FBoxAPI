''' <summary>
''' TR-064 Support – LANConfigSecurity
''' Date: 2020-02-27
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanconfigsecuritySCPD.pdf</see>
''' </summary>

Public Interface ILANConfigSecuritySCPD
    Inherits IServiceBase

    ''' <param name="MaxCharsPassword">32</param>
    ''' <param name="MinCharsPassword">0</param>
    ''' <param name="AllowedCharsPassword">01234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz!”#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~(*)</param>
    Function GetInfo(ByRef MaxCharsPassword As Integer, ByRef MinCharsPassword As Integer, ByRef AllowedCharsPassword As String) As Boolean

    ''' <summary>
    ''' This action can be invoked without authentication.
    ''' </summary>
    Function GetAnonymousLogin(ByRef AnonymousLoginEnabled As Boolean) As Boolean

    ''' <summary>
    ''' The current username might be empty. If anonymous login is enabled, the client may use any username for authentication instead of a configured one. 
    ''' Return list of configured rights of the current user, too. 
    ''' </summary>
    Function GetCurrentUser(ByRef CurrentUsername As String, ByRef CurrentUserRights As String) As Boolean

    ''' <summary>
    ''' Changing the password needs up to 20 seconds. 
    ''' </summary>
    Function SetConfigPassword(ConfigPassword As String) As Boolean

    ''' <summary>
    ''' Get the usernames of all users in a xml-list. Each item has an attribute “last_user”, which is set to '1' for only that username, which was used since last login.
    ''' </summary>
    ''' <param name="UserList">Get the usernames of all users in a xml-list.</param>
    Function GetUserList(ByRef UserList As String) As Boolean

End Interface

