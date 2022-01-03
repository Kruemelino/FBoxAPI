''' <summary>
''' TR-064 Support – X_AVM-DE_Homeplug
''' Date: 2017-01-06 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_homeplugSCPD.pdf</see>
''' </summary>
Public Class X_homePlugSCPD
    Implements IX_homeplugSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_homeplugSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_homeplugSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_homeplugSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_homeplugSCPD

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function GetNumberOfDeviceEntries(NumberOfEntries As Integer) As Boolean Implements IX_homeplugSCPD.GetNumberOfDeviceEntries
        With TR064Start(ServiceFile, "GetNumberOfDeviceEntries", Nothing)
            If .ContainsKey("NewNumberOfEntries") Then

                NumberOfEntries = CInt(.Item("NewNumberOfEntries"))

                PushStatus.Invoke(LogLevel.Debug, $"GetNumberOfDeviceEntries (HomePlug): {NumberOfEntries}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfDeviceEntries (HomePlug) konnte n. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetNumberOfDeviceEntries(Index As Integer, ByRef Device As HomePlugDevice) As Boolean Implements IX_homeplugSCPD.GetNumberOfDeviceEntries
        If Device Is Nothing Then Device = New HomePlugDevice

        With TR064Start(ServiceFile, "GetGenericDeviceEntry", New Hashtable From {{"NewIndex", Index}})

            If .ContainsKey("NewMACAddress") Then

                Device.Index = Index
                Device.MACAddress = .Item("NewMACAddress").ToString
                Device.Active = CBool(.Item("NewActive"))
                Device.Name = .Item("NewName").ToString
                Device.Model = .Item("NewModel").ToString
                Device.UpdateAvailable = CBool(.Item("NewUpdateAvailable"))
                Device.UpdateSuccessful = CType(.Item("NewUpdateSuccessful"), UpdateEnum)

                PushStatus.Invoke(LogLevel.Debug, $"GetGenericDeviceEntry (HomePlug): {Device.Name} - {Device.Model}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericDeviceEntry konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSpecificDeviceEntry(MACAddress As String, ByRef Device As HomePlugDevice) As Boolean Implements IX_homeplugSCPD.GetSpecificDeviceEntry
        If Device Is Nothing Then Device = New HomePlugDevice

        With TR064Start(ServiceFile, "GetSpecificDeviceEntry", New Hashtable From {{"NewMACAddress", MACAddress}})

            If .ContainsKey("NewActive") Then
                Device.MACAddress = MACAddress
                Device.Active = CBool(.Item("NewActive"))
                Device.Name = .Item("NewName").ToString
                Device.Model = .Item("NewModel").ToString
                Device.UpdateAvailable = CBool(.Item("NewUpdateAvailable"))
                Device.UpdateSuccessful = CType(.Item("NewUpdateSuccessful"), UpdateEnum)

                PushStatus.Invoke(LogLevel.Debug, $"GetSpecificDeviceEntry (HomePlug): {Device.Name} - {Device.Model}")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificDeviceEntry konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function DeviceDoUpdate(MACAddress As String) As Boolean Implements IX_homeplugSCPD.DeviceDoUpdate
        With TR064Start(ServiceFile, "DeviceDoUpdate", New Hashtable From {{"NewMACAddress", MACAddress}})
            Return Not .ContainsKey("Error")
        End With
    End Function
End Class
