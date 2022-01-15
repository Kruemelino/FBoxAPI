''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2009-7-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_dectSCPD.pdf</see>
''' </summary>
Friend Class DECT_SCPD
    Implements IDECT_SCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IDECT_SCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_dectSCPD Implements IDECT_SCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetNumberOfDectEntries(ByRef NumberOfEntries As Integer) As Boolean Implements IDECT_SCPD.GetNumberOfDectEntries
        Return TR064Start(ServiceFile,
                        "GetNumberOfDectEntries",
                        Nothing).TryGetValue("NewNumberOfEntries", NumberOfEntries)
    End Function

    Public Function GetGenericDectEntry(ByRef GenericDectEntry As DectEntry, NumberOfEntries As Integer) As Boolean Implements IDECT_SCPD.GetGenericDectEntry
        If GenericDectEntry Is Nothing Then GenericDectEntry = New DectEntry

        With TR064Start(ServiceFile,
                        "GetGenericDectEntry",
                        New Dictionary(Of String, String) From {{"NewIndex", NumberOfEntries}})

            Return .TryGetValue("NewID", GenericDectEntry.ID) And
                   .TryGetValue("NewActive", GenericDectEntry.Active) And
                   .TryGetValue("NewName", GenericDectEntry.Name) And
                   .TryGetValue("NewModel", GenericDectEntry.Model) And
                   .TryGetValue("NewUpdateAvailable", GenericDectEntry.UpdateAvailable) And
                   .TryGetValue("NewUpdateSuccessful", GenericDectEntry.UpdateSuccessful) And
                   .TryGetValue("NewUpdateInfo", GenericDectEntry.UpdateInfo)

        End With
    End Function

    Public Function GetSpecificDectEntry(ByRef SpecificDectEntry As DectEntry, ID As String) As Boolean Implements IDECT_SCPD.GetSpecificDectEntry
        If SpecificDectEntry Is Nothing Then SpecificDectEntry = New DectEntry

        With TR064Start(ServiceFile,
                        "GetSpecificDectEntry",
                        New Dictionary(Of String, String) From {{"NewID", ID}})

            SpecificDectEntry.ID = ID

            Return .TryGetValue("NewID", SpecificDectEntry.ID) And
                   .TryGetValue("NewActive", SpecificDectEntry.Active) And
                   .TryGetValue("NewName", SpecificDectEntry.Name) And
                   .TryGetValue("NewModel", SpecificDectEntry.Model) And
                   .TryGetValue("NewUpdateAvailable", SpecificDectEntry.UpdateAvailable) And
                   .TryGetValue("NewUpdateSuccessful", SpecificDectEntry.UpdateSuccessful) And
                   .TryGetValue("NewUpdateInfo", SpecificDectEntry.UpdateInfo)

        End With
    End Function

    Public Function DectDoUpdate(ByRef ID As String) As Boolean Implements IDECT_SCPD.DectDoUpdate
        Return Not TR064Start(ServiceFile, "DectDoUpdate", New Dictionary(Of String, String) From {{"NewID", ID}}).ContainsKey("Error")
    End Function
End Class
