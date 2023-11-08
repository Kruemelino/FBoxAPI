''' <summary>
''' TR-064 Support – X_AVM-DE_WebDAVClient  
''' Date: 2009-09-18
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_webdavSCPD.pdf</see>
''' </summary>
Friend Class X_webdavSCPD
    Implements IX_webdavSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2009, 9, 18) Implements IX_webdavSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_webdavSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_webdavSCPD Implements IX_webdavSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_webdavSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function GetInfo(ByRef Enable As Boolean, ByRef HostURL As String, ByRef Username As String, ByRef MountpointName As String) As Boolean Implements IX_webdavSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)
            Return .TryGetValueEx("NewEnable", Enable) And
                   .TryGetValueEx("NewHostURL", HostURL) And
                   .TryGetValueEx("NewUsername", Username) And
                   .TryGetValueEx("NewMountpointName", MountpointName)
        End With
    End Function

    Public Function SetConfig(Enable As Boolean, HostURL As String, Username As String, Password As String, MountpointName As String) As Boolean Implements IX_webdavSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr},
                                                                      {"NewHostURL", HostURL},
                                                                      {"NewUsername", Username},
                                                                      {"NewPassword", Password},
                                                                      {"NewMountpointName", MountpointName}}).ContainsKey("Error")
    End Function


End Class