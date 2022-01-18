''' <summary>
''' TR-064 Support – X_AVM-DE_Filelinks
''' Date: 2016-07-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf</see>
''' </summary>
Friend Class X_filelinksSCPD
    Implements IX_filelinksSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_filelinksSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_filelinksSCPD Implements IX_filelinksSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)), XMLSerializer As Serializer)

        TR064Start = Start

        XML = XMLSerializer

    End Sub

    Public Function GetNumberOfFilelinkEntries(ByRef NumberOfEntries As Integer) As Boolean Implements IX_filelinksSCPD.GetNumberOfFilelinkEntries
        Return TR064Start(ServiceFile, "GetNumberOfFilelinkEntries", Nothing).TryGetValueEx("NewNumberOfEntries", NumberOfEntries)
    End Function

    Public Function GetGenericFilelinkEntry(Index As Integer, ByRef Entry As FileLinkEntry) As Boolean Implements IX_filelinksSCPD.GetGenericFilelinkEntry
        If Entry Is Nothing Then Entry = New FileLinkEntry

        With TR064Start(ServiceFile, "GetGenericFilelinkEntry", New Dictionary(Of String, String) From {{"NewIndex", Index}})

            Entry.Index = Index

            Return .TryGetValueEx("NewID", Entry.ID) And
                   .TryGetValueEx("NewValid", Entry.Valid) And
                   .TryGetValueEx("NewPath", Entry.Path) And
                   .TryGetValueEx("NewIsDirectory", Entry.IsDirectory) And
                   .TryGetValueEx("NewUrl", Entry.Url) And
                   .TryGetValueEx("NewUsername", Entry.Username) And
                   .TryGetValueEx("NewAccessCountLimit", Entry.AccessCountLimit) And
                   .TryGetValueEx("NewAccessCount", Entry.AccessCount) And
                   .TryGetValueEx("NewExpire", Entry.Expire) And
                   .TryGetValueEx("NewExpireDate", Entry.ExpireDate)

        End With
    End Function

    Public Function GetSpecificFilelinkEntry(ID As String, ByRef Entry As FileLinkEntry) As Boolean Implements IX_filelinksSCPD.GetSpecificFilelinkEntry
        If Entry Is Nothing Then Entry = New FileLinkEntry

        With TR064Start(ServiceFile, "GetSpecificFilelinkEntry", New Dictionary(Of String, String) From {{"NewID", ID}})

            Entry.ID = ID

            Return .TryGetValueEx("NewValid", Entry.Valid) And
                   .TryGetValueEx("NewPath", Entry.Path) And
                   .TryGetValueEx("NewIsDirectory", Entry.IsDirectory) And
                   .TryGetValueEx("NewUrl", Entry.Url) And
                   .TryGetValueEx("NewUsername", Entry.Username) And
                   .TryGetValueEx("NewAccessCountLimit", Entry.AccessCountLimit) And
                   .TryGetValueEx("NewAccessCount", Entry.AccessCount) And
                   .TryGetValueEx("NewExpire", Entry.Expire) And
                   .TryGetValueEx("NewExpireDate", Entry.ExpireDate)
        End With
    End Function

    Public Function NewFilelinkEntry(Path As String, AccessCountLimit As Integer, Expire As Integer, ByRef ID As String) As Boolean Implements IX_filelinksSCPD.NewFilelinkEntry
        Return TR064Start(ServiceFile, "NewFilelinkEntry",
                          New Dictionary(Of String, String) From {{"NewPath", Path},
                                                                  {"NewAccessCountLimit", AccessCountLimit},
                                                                  {"NewExpire", Expire}}).TryGetValueEx("NewID", ID)
    End Function

    Public Function SetFilelinkEntry(ID As String, AccessCountLimit As Integer, Expire As Integer) As Boolean Implements IX_filelinksSCPD.SetFilelinkEntry
        Return Not TR064Start(ServiceFile, "SetFilelinkEntry", New Dictionary(Of String, String) From {{"NewID", ID},
                                                                                                       {"NewAccessCountLimit", AccessCountLimit},
                                                                                                       {"NewExpire", Expire}}).ContainsKey("Error")
    End Function

    Public Function DeleteFilelinkEntry(ID As String) As Boolean Implements IX_filelinksSCPD.DeleteFilelinkEntry
        Return Not TR064Start(ServiceFile, "DeleteFilelinkEntry", New Dictionary(Of String, String) From {{"NewID", ID}}).ContainsKey("Error")
    End Function

    Public Function GetFilelinkListPath(ByRef FilelinkListPath As String) As Boolean Implements IX_filelinksSCPD.GetFilelinkListPath
        Return TR064Start(ServiceFile, "GetFilelinkListPath", Nothing).TryGetValueEx("NewFilelinkListPath", FilelinkListPath)
    End Function

    Public Function GetFilelinkList(ByRef List As FileLinkList) As Boolean Implements IX_filelinksSCPD.GetFilelinkList
        Dim LuaPath As String = String.Empty
        Return GetFilelinkListPath(LuaPath) AndAlso XML.Deserialize($"http://{FritzBoxTR64.FBoxIPAdresse}:{DfltTR064Port}{LuaPath}", True, List)
    End Function
End Class
