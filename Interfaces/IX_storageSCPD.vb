''' <summary>
''' TR-064 Support – X_AVM-DE_Storage  
''' Date: 2019-02-21
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_storageSCPD.pdf</see>
''' </summary>
Public Interface IX_storageSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef FTPEnable As Boolean,
                     ByRef FTPStatus As EnableEnum,
                     ByRef SMBEnable As Boolean,
                     ByRef FTPWANEnable As Boolean,
                     ByRef FTPWANSSLOnly As Boolean,
                     ByRef FTPWANPort As Integer) As Boolean

    Function SetFTPServer(FTPEnable As Boolean) As Boolean

    Function SetFTPServerWAN(FTPWANEnable As Boolean, FTPWANSSLOnly As Boolean) As Boolean

    ''' <summary>
    ''' Open the configured FTP WAN port for IPv4 and IPv6 (if available). 
    ''' The port is opened permanently if service is enabled and WAN access is configured. 
    ''' The port is opened for a limited time if service is enabled and WAN access is not configured.
    ''' </summary>
    ''' <remarks>Required rights: app or nas<br/>
    ''' The security context of this action must belong to an app instance.</remarks>
    Function RequestFTPServerWAN(ByRef FTPWANPort As Integer,
                                 ByRef FTPWANLifetime As Integer) As Boolean

    Function SetSMBServer(SMBEnable As Boolean) As Boolean

    ''' <summary>
    ''' The action uses the FRITZ!Box user with the username “ftpuser”. Uppercase and lowercase letters are ignored.
    ''' If the user does not exist, Enable is 0. If the user exists but has no rights to access any storage, Enable is 0. 
    ''' </summary>
    Function GetUserInfo(ByRef Enable As Boolean,
                         ByRef Username As String,
                         ByRef NetworkAccessReadOnly As Boolean) As Boolean

    ''' <summary>
    ''' If no user with the name “ftpuser” exists, internal error status code should be retrieved.
    ''' </summary>
    ''' <param name="Password">Allowed characters for Password are all ASCII characters with decimal values from 32 to 126.
    ''' The length of Password may be from 1 to 32 characters and the string “nil” is not allowed. </param>
    ''' <param name="NetworkAccessReadOnly">If Enable is 0, <paramref name="NetworkAccessReadOnly"/> will always be set to 1. </param>
    ''' <returns></returns>
    Function SetUserConfig(Enable As Boolean,
                           Password As String,
                           NetworkAccessReadOnly As Boolean) As Boolean
End Interface
