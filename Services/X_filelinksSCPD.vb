''' <summary>
''' TR-064 Support – X_AVM-DE_Filelinks
''' Date: 2016-07-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf</see>
''' </summary>
Friend Class X_filelinksSCPD
    Implements IX_filelinksSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2016, 7, 7) Implements IX_filelinksSCPD.DocumentationDate

    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_filelinksSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_filelinksSCPD Implements IX_filelinksSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_filelinksSCPD.ServiceID
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)),
                   XMLSerializer As Serializer)

        TR064Start = Start
        XML = XMLSerializer

    End Sub

    Public Function GetNumberOfFilelinkEntries(ByRef NumberOfEntries As Integer) As Boolean Implements IX_filelinksSCPD.GetNumberOfFilelinkEntries
        Return TR064Start(ServiceFile, "GetNumberOfFilelinkEntries", ServiceID, Nothing).TryGetValueEx("NewNumberOfEntries", NumberOfEntries)
    End Function

    Public Function GetGenericFilelinkEntry(Index As Integer, ByRef Entry As FileLinkEntry) As Boolean Implements IX_filelinksSCPD.GetGenericFilelinkEntry
        If Entry Is Nothing Then Entry = New FileLinkEntry

        With TR064Start(ServiceFile, "GetGenericFilelinkEntry", ServiceID, New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})

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

        With TR064Start(ServiceFile, "GetSpecificFilelinkEntry", ServiceID, New Dictionary(Of String, String) From {{"NewID", ID}})

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
        Return TR064Start(ServiceFile, "NewFilelinkEntry", ServiceID,
                          New Dictionary(Of String, String) From {{"NewPath", Path},
                                                                  {"NewAccessCountLimit", AccessCountLimit.ToString},
                                                                  {"NewExpire", Expire.ToString}}).TryGetValueEx("NewID", ID)
    End Function

    Public Function SetFilelinkEntry(ID As String, AccessCountLimit As Integer, Expire As Integer) As Boolean Implements IX_filelinksSCPD.SetFilelinkEntry
        Return Not TR064Start(ServiceFile, "SetFilelinkEntry", ServiceID,
                              New Dictionary(Of String, String) From {{"NewID", ID},
                                                                      {"NewAccessCountLimit", AccessCountLimit.ToString},
                                                                      {"NewExpire", Expire.ToString}}).ContainsKey("Error")
    End Function

    Public Function DeleteFilelinkEntry(ID As String) As Boolean Implements IX_filelinksSCPD.DeleteFilelinkEntry
        Return Not TR064Start(ServiceFile, "DeleteFilelinkEntry", ServiceID, New Dictionary(Of String, String) From {{"NewID", ID}}).ContainsKey("Error")
    End Function

    Public Function GetFilelinkListPath(ByRef FilelinkListPath As String) As Boolean Implements IX_filelinksSCPD.GetFilelinkListPath
        Return TR064Start(ServiceFile, "GetFilelinkListPath", ServiceID, Nothing).TryGetValueEx("NewFilelinkListPath", FilelinkListPath)
    End Function

    Public Function GetFilelinkList(ByRef List As FileLinkList) As Boolean Implements IX_filelinksSCPD.GetFilelinkList
        Dim LuaPath As String = String.Empty
        Return GetFilelinkListPath(LuaPath) AndAlso XML.Deserialize($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{LuaPath}", True, List)
    End Function

    Public Async Function GetFilelinkList() As Task(Of FileLinkList) Implements IX_filelinksSCPD.GetFilelinkList
        ' Ermittle den Pfad zu AssociatedDevices und deserialisiere die Daten
        Dim FilelinkListUrl As String = String.Empty

        If GetFilelinkListPath(FilelinkListUrl) Then
            ' GetFilelinkListPath liefert nur den lua-Part. Der Rest muss vorangefügt werden.
            Return Await XML.DeserializeAsyncFromPath(Of FileLinkList)($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{FilelinkListUrl}")
        Else
            ' Gib eine leere Liste zurück
            Return New FileLinkList
        End If

    End Function
End Class
