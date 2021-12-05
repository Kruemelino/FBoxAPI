''' <summary>
''' TR-064 Support – Hosts
''' Date:  2020-12-01
''' <see cref="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/hostsSCPD.pdf"/>
''' </summary>
Friend Class HostsSCPD
    Implements IHostsSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IHostsSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IHostsSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IHostsSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String), XMLSerializer As Serializer)

        ServiceFile = SCPDFiles.hostsSCPD

        TR064Start = Start

        PushStatus = Status

        XML = XMLSerializer
    End Sub

    Public Function GetHostNumberOfEntries(ByRef HostNumberOfEntries As Integer) As Boolean Implements IHostsSCPD.GetHostNumberOfEntries
        With TR064Start(ServiceFile, "GetHostNumberOfEntries", Nothing)

            If .ContainsKey("NewHostNumberOfEntries") Then
                HostNumberOfEntries = CInt(.Item("NewHostNumberOfEntries"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetHostNumberOfEntries konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSpecificHostEntry(MACAddress As String, ByRef Host As HostEntry) As Boolean Implements IHostsSCPD.GetSpecificHostEntry
        If Host Is Nothing Then Host = New HostEntry

        With TR064Start(ServiceFile, "GetSpecificHostEntry", New Hashtable From {{"NewMACAddress", MACAddress}})

            If .ContainsKey("NewIPAddress") Then
                Host.MACAddress = MACAddress
                Host.IPAddress = .Item("NewIPAddress").ToString
                Host.AddressSource = .Item("NewAddressSource").ToString
                Host.LeaseTimeRemaining = CInt(.Item("NewLeaseTimeRemaining"))
                Host.InterfaceType = .Item("NewInterfaceType").ToString
                Host.Active = CBool(.Item("NewActive"))
                Host.HostName = .Item("NewHostName").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificHostEntry konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetGenericHostEntry(HostNumberOfEntries As Integer, ByRef Host As HostEntry) As Boolean Implements IHostsSCPD.GetGenericHostEntry
        If Host Is Nothing Then Host = New HostEntry

        With TR064Start(ServiceFile, "GetSpecificHostEntry", New Hashtable From {{"NewIndex", HostNumberOfEntries}})

            If .ContainsKey("NewIPAddress") Then
                Host.Index = HostNumberOfEntries
                Host.MACAddress = .Item("NewMACAddress").ToString
                Host.IPAddress = .Item("NewIPAddress").ToString
                Host.AddressSource = .Item("NewAddressSource").ToString
                Host.LeaseTimeRemaining = CInt(.Item("NewLeaseTimeRemaining"))
                Host.InterfaceType = .Item("NewInterfaceType").ToString
                Host.Active = CBool(.Item("NewActive"))
                Host.HostName = .Item("NewHostName").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificHostEntry konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetChangeCounter(ByRef ChangeCounter As Integer) As Boolean Implements IHostsSCPD.GetChangeCounter
        With TR064Start(ServiceFile, "X_AVM-DE_GetChangeCounter", Nothing)

            If .ContainsKey("NewX_AVM-DE_ChangeCounter") Then
                ChangeCounter = CInt(.Item("NewX_AVM-DE_ChangeCounter"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetChangeCounter konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetAutoWakeOnLANByMACAddress(MACAddress As String, ByRef AutoWOLEnabled As Boolean) As Boolean Implements IHostsSCPD.GetAutoWakeOnLANByMACAddress
        With TR064Start(ServiceFile, "X_AVM-DE_GetAutoWakeOnLANByMACAddress", New Hashtable From {{"NewMACAddress", MACAddress}})

            If .ContainsKey("NewAutoWOLEnabled") Then
                AutoWOLEnabled = CBool(.Item("NewAutoWOLEnabled"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetAutoWakeOnLANByMACAddress konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetAutoWakeOnLANByMACAddress(MACAddress As String, AutoWOLEnabled As Boolean) As Boolean Implements IHostsSCPD.SetAutoWakeOnLANByMACAddress
        With TR064Start(ServiceFile, "X_AVM-DE_SetAutoWakeOnLANByMACAddress", New Hashtable From {{"NewMACAddress", MACAddress}, {"NewAutoWOLEnabled", AutoWOLEnabled.ToInt}})

            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function SetHostNameByMACAddress(MACAddress As String, HostName As String) As Boolean Implements IHostsSCPD.SetHostNameByMACAddress
        With TR064Start(ServiceFile, "X_AVM-DE_SetAutoWakeOnLANByMACAddress", New Hashtable From {{"NewMACAddress", MACAddress}, {"NewHostName", HostName}})

            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function WakeOnLANByMACAddress(MACAddress As String) As Boolean Implements IHostsSCPD.WakeOnLANByMACAddress
        With TR064Start(ServiceFile, "X_AVM-DE_WakeOnLANByMACAddress", New Hashtable From {{"NewMACAddress", MACAddress}})

            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetSpecificHostEntryByIp(IPAddress As String, ByRef Host As HostEntry) As Boolean Implements IHostsSCPD.GetSpecificHostEntryByIp
        If Host Is Nothing Then Host = New HostEntry

        With TR064Start(ServiceFile, "GetSpecificHostEntry", New Hashtable From {{"NewIPAddress", IPAddress}})

            If .ContainsKey("NewMACAddress") Then
                Host.IPAddress = IPAddress
                Host.MACAddress = .Item("NewMACAddress").ToString
                Host.Active = CBool(.Item("NewActive"))
                Host.HostName = .Item("NewHostName").ToString
                Host.Port = .Item("NewX_AVM-DE_Port").ToString
                Host.Speed = CInt(.Item("NewX_AVM-DE_Speed"))
                Host.UpdateAvailable = CBool(.Item("NewX_AVM-DE_UpdateAvailable"))
                Host.UpdateSuccessful = .Item("NewX_AVM-DE_UpdateSuccessful").ToString
                Host.InfoURL = .Item("NewX_AVM-DE_InfoURL").ToString
                Host.Model = .Item("NewX_AVM-DE_Model").ToString
                Host.URL = .Item("NewX_AVM-DE_URL").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSpecificHostEntryByIp konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function HostsCheckUpdate() As Boolean Implements IHostsSCPD.HostsCheckUpdate
        With TR064Start(ServiceFile, "X_AVM-DE_HostsCheckUpdate", Nothing)

            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function HostDoUpdate(MACAddress As String) As Boolean Implements IHostsSCPD.HostDoUpdate
        With TR064Start(ServiceFile, "X_AVM-DE_HostDoUpdate", New Hashtable From {{"NewMACAddress", MACAddress}})

            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetHostListPath(ByRef HostListPath As String) As Boolean Implements IHostsSCPD.GetHostListPath
        With TR064Start(ServiceFile, "X_AVM-DE_GetHostListPath", Nothing)

            If .ContainsKey("NewX_AVM-DE_HostListPath") Then
                HostListPath = CBool(.Item("NewX_AVM-DE_HostListPath"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-NewX_AVM-DE_HostListPath konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetHostList(ByRef Hosts As HostList) As Boolean Implements IHostsSCPD.GetHostList
        With TR064Start(ServiceFile, "X_AVM-DE_GetHostListPath", Nothing)

            If .ContainsKey("NewX_AVM-DE_HostListPath") Then

                If Not XML.Deserialize(.Item("NewX_AVM-DE_HostListPath").ToString(), False, Hosts) Then
                    PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetHostListPath konnte für nicht deserialisiert werden.")
                End If

                ' Wenn keine Hosts angeschlossen wurden, gib eine leere Klasse zurück
                If Hosts Is Nothing Then Hosts = New HostList

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetHostListPath konnte für nicht aufgelößt werden. '{ .Item("Error")}'")
                Hosts = Nothing

                Return False
            End If
        End With
    End Function

    Public Function GetMeshListPath(ByRef MeshListPath As String) As Boolean Implements IHostsSCPD.GetMeshListPath
        With TR064Start(ServiceFile, "X_AVM-DE_GetMeshListPath", Nothing)

            If .ContainsKey("NewX_AVM-DE_MeshListPath") Then
                MeshListPath = CBool(.Item("NewX_AVM-DE_MeshListPath"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-NewX_AVM-DE_GetMeshListPath konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
End Class
