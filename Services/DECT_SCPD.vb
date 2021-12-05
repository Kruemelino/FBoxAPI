''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2009-7-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_dectSCPD.pdf</see>
''' </summary>
Friend Class DECT_SCPD
    Implements IDECT_SCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IDECT_SCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IDECT_SCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IDECT_SCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_dectSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetNumberOfDectEntries(ByRef NumberOfEntries As Integer) As Boolean Implements IDECT_SCPD.GetNumberOfDectEntries
        With TR064Start(ServiceFile, "GetNumberOfDectEntries", Nothing)
            If .ContainsKey("NewNumberOfEntries") Then

                NumberOfEntries = .Item("NewNumberOfEntries").ToString

                PushStatus.Invoke(LogLevel.Debug, $"GetNumberOfDectEntries der Fritz!Box: {NumberOfEntries}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfDectEntries der Fritz!Box nicht ermittelt. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetGenericDectEntry(ByRef GenericDectEntry As DectEntry, NumberOfEntries As Integer) As Boolean Implements IDECT_SCPD.GetGenericDectEntry
        If GenericDectEntry Is Nothing Then GenericDectEntry = New DectEntry

        With TR064Start(ServiceFile, "GetGenericDectEntry", New Hashtable From {{"NewIndex", NumberOfEntries}})

            If .ContainsKey("NewID") Then
                GenericDectEntry.ID = .Item("NewID").ToString
                GenericDectEntry.Active = CBool(.Item("NewActive"))
                GenericDectEntry.Name = .Item("NewName").ToString
                GenericDectEntry.Model = .Item("NewModel").ToString
                GenericDectEntry.UpdateAvailable = CBool(.Item("NewUpdateAvailable"))
                GenericDectEntry.UpdateSuccessful = CType(.Item("NewUpdateSuccessful"), UpdateEnum)
                GenericDectEntry.UpdateInfo = .Item("NewUpdateInfo").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericDectEntry konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSpecificDectEntry(ByRef SpecificDectEntry As DectEntry, ID As String) As Boolean Implements IDECT_SCPD.GetSpecificDectEntry
        If SpecificDectEntry Is Nothing Then SpecificDectEntry = New DectEntry

        With TR064Start(ServiceFile, "GetSpecificDectEntry", New Hashtable From {{"NewID", ID}})

            If .ContainsKey("NewID") Then
                SpecificDectEntry.ID = ID

                SpecificDectEntry.Active = CBool(.Item("NewActive"))
                SpecificDectEntry.Name = .Item("NewName").ToString
                SpecificDectEntry.Model = .Item("NewModel").ToString
                SpecificDectEntry.UpdateAvailable = CBool(.Item("NewUpdateAvailable"))
                SpecificDectEntry.UpdateSuccessful = CType(.Item("NewUpdateSuccessful"), UpdateEnum)
                SpecificDectEntry.UpdateInfo = .Item("NewUpdateInfo").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificDectEntry konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function DectDoUpdate(ByRef ID As String) As Boolean Implements IDECT_SCPD.DectDoUpdate
        With TR064Start(ServiceFile, "DectDoUpdate", New Hashtable From {{"NewID", ID}})
            Return Not .ContainsKey("Error")
        End With
    End Function
End Class
