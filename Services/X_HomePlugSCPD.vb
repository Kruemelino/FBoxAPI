''' <summary>
''' TR-064 Support – X_AVM-DE_Homeplug
''' Date: 2022-02-11
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeplugSCPD.pdf</see>
''' </summary>
Friend Class X_homePlugSCPD
    Implements IX_homeplugSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 11) Implements IX_homeplugSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_homeplugSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_homeplugSCPD Implements IX_homeplugSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_homeplugSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetNumberOfDeviceEntries(NumberOfEntries As Integer) As Boolean Implements IX_homeplugSCPD.GetNumberOfDeviceEntries
        Return TR064Start(ServiceFile, "GetNumberOfDeviceEntries", ServiceID, Nothing).TryGetValueEx("NewNumberOfEntries", NumberOfEntries)
    End Function

    Public Function GetNumberOfDeviceEntries(Index As Integer, ByRef Device As HomePlugDevice) As Boolean Implements IX_homeplugSCPD.GetNumberOfDeviceEntries
        If Device Is Nothing Then Device = New HomePlugDevice

        With TR064Start(ServiceFile, "GetGenericDeviceEntry", ServiceID, New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})

            Device.Index = Index

            Return .TryGetValueEx("NewMACAddress", Device.MACAddress) And
                   .TryGetValueEx("NewActive", Device.Active) And
                   .TryGetValueEx("NewName", Device.Name) And
                   .TryGetValueEx("NewModel", Device.Model) And
                   .TryGetValueEx("NewUpdateAvailable", Device.UpdateAvailable) And
                   .TryGetValueEx("NewUpdateSuccessful", Device.UpdateSuccessful)
        End With
    End Function

    Public Function GetSpecificDeviceEntry(MACAddress As String, ByRef Device As HomePlugDevice) As Boolean Implements IX_homeplugSCPD.GetSpecificDeviceEntry
        If Device Is Nothing Then Device = New HomePlugDevice

        With TR064Start(ServiceFile, "GetSpecificDeviceEntry", ServiceID, New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress}})

            Device.MACAddress = MACAddress

            Return .TryGetValueEx("NewActive", Device.Active) And
                   .TryGetValueEx("NewName", Device.Name) And
                   .TryGetValueEx("NewModel", Device.Model) And
                   .TryGetValueEx("NewUpdateAvailable", Device.UpdateAvailable) And
                   .TryGetValueEx("NewUpdateSuccessful", Device.UpdateSuccessful)
        End With
    End Function

    Public Function DeviceDoUpdate(MACAddress As String) As Boolean Implements IX_homeplugSCPD.DeviceDoUpdate
        Return Not TR064Start(ServiceFile, "DeviceDoUpdate", ServiceID, New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress}}).ContainsKey("Error")
    End Function
End Class
