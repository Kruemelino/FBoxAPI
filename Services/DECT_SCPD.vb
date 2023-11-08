''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2022-10-17
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_dectSCPD.pdf</see>
''' </summary>
Friend Class DECT_SCPD
    Implements IDECT_SCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 10, 17) Implements IDECT_SCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IDECT_SCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_dectSCPD Implements IDECT_SCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IDECT_SCPD.ServiceID
    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start
    End Sub

    Public Function GetNumberOfDectEntries(ByRef NumberOfEntries As Integer) As Boolean Implements IDECT_SCPD.GetNumberOfDectEntries
        Return TR064Start(ServiceFile, "GetNumberOfDectEntries", ServiceID, Nothing).TryGetValueEx("NewNumberOfEntries", NumberOfEntries)
    End Function

    Public Function GetGenericDectEntry(ByRef GenericDectEntry As DectEntry, NumberOfEntries As Integer) As Boolean Implements IDECT_SCPD.GetGenericDectEntry
        If GenericDectEntry Is Nothing Then GenericDectEntry = New DectEntry

        With TR064Start(ServiceFile,
                        "GetGenericDectEntry", ServiceID,
                        New Dictionary(Of String, String) From {{"NewIndex", NumberOfEntries.ToString}})

            Return .TryGetValueEx("NewID", GenericDectEntry.ID) And
                   .TryGetValueEx("NewActive", GenericDectEntry.Active) And
                   .TryGetValueEx("NewName", GenericDectEntry.Name) And
                   .TryGetValueEx("NewModel", GenericDectEntry.Model) And
                   .TryGetValueEx("NewUpdateAvailable", GenericDectEntry.UpdateAvailable) And
                   .TryGetValueEx("NewUpdateSuccessful", GenericDectEntry.UpdateSuccessful) And
                   .TryGetValueEx("NewUpdateInfo", GenericDectEntry.UpdateInfo)

        End With
    End Function

    Public Function GetSpecificDectEntry(ByRef SpecificDectEntry As DectEntry, ID As String) As Boolean Implements IDECT_SCPD.GetSpecificDectEntry
        If SpecificDectEntry Is Nothing Then SpecificDectEntry = New DectEntry

        With TR064Start(ServiceFile,
                        "GetSpecificDectEntry", ServiceID,
                        New Dictionary(Of String, String) From {{"NewID", ID}})

            SpecificDectEntry.ID = ID

            Return .TryGetValueEx("NewID", SpecificDectEntry.ID) And
                   .TryGetValueEx("NewActive", SpecificDectEntry.Active) And
                   .TryGetValueEx("NewName", SpecificDectEntry.Name) And
                   .TryGetValueEx("NewModel", SpecificDectEntry.Model) And
                   .TryGetValueEx("NewUpdateAvailable", SpecificDectEntry.UpdateAvailable) And
                   .TryGetValueEx("NewUpdateSuccessful", SpecificDectEntry.UpdateSuccessful) And
                   .TryGetValueEx("NewUpdateInfo", SpecificDectEntry.UpdateInfo)

        End With
    End Function

    Public Function DectDoUpdate(ByRef ID As String) As Boolean Implements IDECT_SCPD.DectDoUpdate
        Return Not TR064Start(ServiceFile, "DectDoUpdate", ServiceID, New Dictionary(Of String, String) From {{"NewID", ID}}).ContainsKey("Error")
    End Function

    Public Function GetDectListPath(ByRef DectListPath As String) As Boolean Implements IDECT_SCPD.GetDectListPath
        Return Not TR064Start(ServiceFile, "GetDectListPath", ServiceID, New Dictionary(Of String, String) From {{"NewDectListPath", DectListPath}}).ContainsKey("Error")
    End Function
End Class
