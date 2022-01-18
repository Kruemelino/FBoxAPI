''' <summary>
''' TR-064 Support – X_AVM-DE_WebDAVClient  
''' Date: 2009-09-18
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_webdavSCPD.pdf</see>
''' </summary>
Public Interface IX_webdavSCPD
    Inherits IServiceBase

    ''' <param name="HostURL">0 - 256 Zeichen</param>
    ''' <param name="Username">0 - 128 Zeichen </param>
    ''' <param name="MountpointName">0 - 256 Zeichen</param>
    Function GetInfo(ByRef Enable As Boolean,
                     ByRef HostURL As String,
                     ByRef Username As String,
                     ByRef MountpointName As String) As Boolean

    ''' <param name="HostURL">0 - 256 Zeichen</param>
    ''' <param name="Username">0 - 128 Zeichen </param>
    ''' <param name="Password">0 - 128 Zeichen </param>
    ''' <param name="MountpointName">0 - 256 Zeichen</param>
    Function SetConfig(Enable As Boolean,
                       HostURL As String,
                       Username As String,
                       Password As String,
                       MountpointName As String) As Boolean

End Interface
