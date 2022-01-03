''' <summary>
''' TR-064 Support – X_AVM-DE_Filelinks
''' Date: 2016-07-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf</see>
''' </summary>
Friend Class X_filelinksSCPD
    Implements IX_filelinksSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_filelinksSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_filelinksSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_filelinksSCPD.Servicefile

    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String), XMLSerializer As Serializer)

        ServiceFile = SCPDFiles.x_filelinksSCPD

        TR064Start = Start

        PushStatus = Status

        XML = XMLSerializer
    End Sub

    Public Function GetNumberOfFilelinkEntries(ByRef NumberOfEntries As Integer) As Boolean Implements IX_filelinksSCPD.GetNumberOfFilelinkEntries
        With TR064Start(ServiceFile, "GetNumberOfFilelinkEntries", Nothing)
            If .ContainsKey("NewNumberOfEntries") Then

                NumberOfEntries = .Item("NewNumberOfEntries").ToString

                PushStatus.Invoke(LogLevel.Debug, $"GetNumberOfFilelinkEntries erfolgreich")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfFilelinkEntries konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetGenericFilelinkEntry(Index As Integer, ByRef Entry As FileLinkEntry) As Boolean Implements IX_filelinksSCPD.GetGenericFilelinkEntry
        If Entry Is Nothing Then Entry = New FileLinkEntry

        With TR064Start(ServiceFile, "GetGenericFilelinkEntry", New Hashtable From {{"NewIndex", Index}})

            If .ContainsKey("NewID") Then

                Entry.Index = Index
                Entry.ID = .Item("NewID").ToString
                Entry.Valid = CBool(.Item("NewValid"))
                Entry.Path = .Item("NewPath").ToString
                Entry.IsDirectory = CBool(.Item("NewIsDirectory"))
                Entry.Url = .Item("NewUrl").ToString
                Entry.Username = .Item("NewUsername").ToString
                Entry.AccessCountLimit = CInt(.Item("NewAccessCountLimit"))
                Entry.AccessCount = CInt(.Item("NewAccessCount"))
                Entry.Expire = CInt(.Item("NewExpire"))
                Entry.ExpireDate = CDate(.Item("NewExpireDate"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericFilelinkEntry konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSpecificFilelinkEntry(ID As String, ByRef Entry As FileLinkEntry) As Boolean Implements IX_filelinksSCPD.GetSpecificFilelinkEntry
        If Entry Is Nothing Then Entry = New FileLinkEntry

        With TR064Start(ServiceFile, "GetSpecificFilelinkEntry", New Hashtable From {{"NewID", ID}})

            If .ContainsKey("NewValid") Then

                Entry.ID = ID
                Entry.Valid = CBool(.Item("NewValid"))
                Entry.Path = .Item("NewPath").ToString
                Entry.IsDirectory = CBool(.Item("NewIsDirectory"))
                Entry.Url = .Item("NewUrl").ToString
                Entry.Username = .Item("NewUsername").ToString
                Entry.AccessCountLimit = CInt(.Item("NewAccessCountLimit"))
                Entry.AccessCount = CInt(.Item("NewAccessCount"))
                Entry.Expire = CInt(.Item("NewExpire"))
                Entry.ExpireDate = CDate(.Item("NewExpireDate"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificFilelinkEntry konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function NewFilelinkEntry(Path As String, AccessCountLimit As Integer, Expire As Integer, ByRef ID As String) As Boolean Implements IX_filelinksSCPD.NewFilelinkEntry
        With TR064Start(ServiceFile, "NewFilelinkEntry", New Hashtable From {{"NewPath", Path},
                                                                             {"NewAccessCountLimit", AccessCountLimit},
                                                                             {"NewExpire", Expire}})
            If .ContainsKey("NewID") Then

                ID = .Item("NewID").ToString

                PushStatus.Invoke(LogLevel.Debug, $"NewFilelinkEntry erfolgreich: {ID}")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"NewFilelinkEntry konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetFilelinkEntry(ID As String, AccessCountLimit As Integer, Expire As Integer) As Boolean Implements IX_filelinksSCPD.SetFilelinkEntry
        With TR064Start(ServiceFile, "SetFilelinkEntry", New Hashtable From {{"NewID", ID},
                                                                             {"NewAccessCountLimit", AccessCountLimit},
                                                                             {"NewExpire", Expire}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DeleteFilelinkEntry(ID As String) As Boolean Implements IX_filelinksSCPD.DeleteFilelinkEntry
        With TR064Start(ServiceFile, "DeleteFilelinkEntry", New Hashtable From {{"NewID", ID}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetFilelinkListPath(FilelinkListPath As String) As Boolean Implements IX_filelinksSCPD.GetFilelinkListPath
        With TR064Start(ServiceFile, "GetFilelinkListPath", Nothing)
            If .ContainsKey("NewFilelinkListPath") Then

                FilelinkListPath = .Item("NewFilelinkListPath").ToString

                PushStatus.Invoke(LogLevel.Debug, $"GetFilelinkListPath erfolgreich")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetFilelinkListPath konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetFilelinkList(ByRef List As FileLinkList) As Boolean Implements IX_filelinksSCPD.GetFilelinkList
        With TR064Start(ServiceFile, "GetFilelinkListPath", Nothing)

            If .ContainsKey("GetFilelinkListPath") Then

                If Not XML.Deserialize(.Item("GetFilelinkListPath").ToString(), False, List) Then
                    PushStatus.Invoke(LogLevel.Warn, $"GetFilelinkListPath konnte für nicht deserialisiert werden.")
                End If

                ' Wenn keine Hosts angeschlossen wurden, gib eine leere Klasse zurück
                If List Is Nothing Then List = New FileLinkList

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetHostListPath konnte für nicht aufgelößt werden. '{ .Item("Error")}'")
                List = Nothing

                Return False
            End If
        End With
    End Function
End Class
