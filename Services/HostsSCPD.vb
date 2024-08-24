''' <summary>
''' TR-064 Support – Hosts
''' Date: 2024-07-02
''' <see href="https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/hostsSCPD.pdf"/>
''' </summary>
Friend Class HostsSCPD
    Implements IHostsSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 10, 13) Implements IHostsSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IHostsSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.hostsSCPD Implements IHostsSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IHostsSCPD.ServiceID
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)),
                   XMLSerializer As Serializer)

        TR064Start = Start
        XML = XMLSerializer
    End Sub

    Public Function GetHostNumberOfEntries(ByRef HostNumberOfEntries As Integer) As Boolean Implements IHostsSCPD.GetHostNumberOfEntries
        Return TR064Start(ServiceFile, "GetHostNumberOfEntries", ServiceID, Nothing).TryGetValueEx("NewHostNumberOfEntries", HostNumberOfEntries)
    End Function

    Public Function GetSpecificHostEntry(MACAddress As String, ByRef Host As HostEntry) As Boolean Implements IHostsSCPD.GetSpecificHostEntry
        If Host Is Nothing Then Host = New HostEntry

        With TR064Start(ServiceFile, "GetSpecificHostEntry", ServiceID, New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress}})

            Host.MACAddress = MACAddress

            Return .TryGetValueEx("NewIPAddress", Host.IPAddress) And
                   .TryGetValueEx("NewAddressSource", Host.AddressSource) And
                   .TryGetValueEx("NewLeaseTimeRemaining", Host.LeaseTimeRemaining) And
                   .TryGetValueEx("NewInterfaceType", Host.InterfaceType) And
                   .TryGetValueEx("NewActive", Host.Active) And
                   .TryGetValueEx("NewHostName", Host.HostName)

        End With
    End Function

    Public Function GetGenericHostEntry(Index As Integer, ByRef Host As HostEntry) As Boolean Implements IHostsSCPD.GetGenericHostEntry
        If Host Is Nothing Then Host = New HostEntry With {.Index = Index}

        With TR064Start(ServiceFile, "GetGenericHostEntry", ServiceID, New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})

            Return .TryGetValueEx("NewMACAddress", Host.MACAddress) And
                   .TryGetValueEx("NewIPAddress", Host.IPAddress) And
                   .TryGetValueEx("NewAddressSource", Host.AddressSource) And
                   .TryGetValueEx("NewLeaseTimeRemaining", Host.LeaseTimeRemaining) And
                   .TryGetValueEx("NewInterfaceType", Host.InterfaceType) And
                   .TryGetValueEx("NewActive", Host.Active) And
                   .TryGetValueEx("NewHostName", Host.HostName)

        End With
    End Function

    Public Function GetInfo(ByRef Info As HostsInfo) As Boolean Implements IHostsSCPD.GetInfo
        If Info Is Nothing Then Info = New HostsInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetInfo", ServiceID, New Dictionary(Of String, String))

            Return .TryGetValueEx("NewX_AVM-DE_FriendlynameMinChars", Info.FriendlynameMinChars) And
                   .TryGetValueEx("NewX_AVM-DE_FriendlynameMaxChars", Info.FriendlynameMaxChars) And
                   .TryGetValueEx("NewX_AVM-DE_HostnameMinChars", Info.HostnameMinChars) And
                   .TryGetValueEx("NewX_AVM-DE_HostnameMaxChars", Info.HostnameMaxChars) And
                   .TryGetValueEx("NewX_AVM-DE_HostnameAllowedChars", Info.HostnameAllowedChars)

        End With
    End Function

    Public Function GetChangeCounter(ByRef ChangeCounter As Integer) As Boolean Implements IHostsSCPD.GetChangeCounter
        Return TR064Start(ServiceFile, "X_AVM-DE_GetChangeCounter", ServiceID, Nothing).TryGetValueEx("NewX_AVM-DE_ChangeCounter", ChangeCounter)
    End Function

    Public Function GetAutoWakeOnLANByMACAddress(MACAddress As String, ByRef AutoWOLEnabled As Boolean) As Boolean Implements IHostsSCPD.GetAutoWakeOnLANByMACAddress
        Return TR064Start(ServiceFile, "X_AVM-DE_GetAutoWakeOnLANByMACAddress", ServiceID,
                          New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress}}).
                          TryGetValueEx("NewAutoWOLEnabled", AutoWOLEnabled)

    End Function

    Public Function SetAutoWakeOnLANByMACAddress(MACAddress As String, AutoWOLEnabled As Boolean) As Boolean Implements IHostsSCPD.SetAutoWakeOnLANByMACAddress
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetAutoWakeOnLANByMACAddress", ServiceID,
                              New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress},
                                                                      {"NewAutoWOLEnabled", AutoWOLEnabled.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function SetHostNameByMACAddress(MACAddress As String, HostName As String) As Boolean Implements IHostsSCPD.SetHostNameByMACAddress
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetAutoWakeOnLANByMACAddress", ServiceID,
                              New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress},
                                                                      {"NewHostName", HostName}}).ContainsKey("Error")
    End Function

    Public Function WakeOnLANByMACAddress(MACAddress As String) As Boolean Implements IHostsSCPD.WakeOnLANByMACAddress
        Return Not TR064Start(ServiceFile, "X_AVM-DE_WakeOnLANByMACAddress", ServiceID,
                              New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress}}).ContainsKey("Error")
    End Function

    Public Function GetSpecificHostEntryByIP(IPAddress As String, ByRef Host As HostEntry) As Boolean Implements IHostsSCPD.GetSpecificHostEntryByIP
        If Host Is Nothing Then Host = New HostEntry

        With TR064Start(ServiceFile, "X_AVM-DE_GetSpecificHostEntryByIP", ServiceID,
                        New Dictionary(Of String, String) From {{"NewIPAddress", IPAddress}})

            Host.IPAddress = IPAddress

            Return .TryGetValueEx("NewAddressSource", Host.AddressSource) And
                   .TryGetValueEx("NewLeaseTimeRemaining", Host.LeaseTimeRemaining) And
                   .TryGetValueEx("NewInterfaceType", Host.InterfaceType) And
                   .TryGetValueEx("NewActive", Host.Active) And
                   .TryGetValueEx("NewHostName", Host.HostName) And
                   .TryGetValueEx("NewInterfaceType", Host.InterfaceType) And
                   .TryGetValueEx("NewX_AVM-DE_Port", Host.Port) And
                   .TryGetValueEx("NewX_AVM-DE_UpdateAvailable", Host.UpdateAvailable) And
                   .TryGetValueEx("NewX_AVM-DE_UpdateSuccessful", Host.UpdateSuccessful) And
                   .TryGetValueEx("NewX_AVM-DE_InfoURL", Host.InfoURL) And
                   .TryGetValueEx("NewX_AVM-DE_MACAddressList", Host.MACAddressList) And
                   .TryGetValueEx("NewX_AVM-DE_Model", Host.Model) And
                   .TryGetValueEx("NewX_AVM-DE_URL", Host.URL) And
                   .TryGetValueEx("NewX_AVM-DE_Guest", Host.Guest) And
                   .TryGetValueEx("NewX_AVM-DE_RequestClient", Host.RequestClient) And
                   .TryGetValueEx("NewX_AVM-DE_VPN", Host.VPN) And
                   .TryGetValueEx("NewX_AVM-DE_WANAccess", Host.WANAccess) And
                   .TryGetValueEx("NewX_AVM-DE_Disallow", Host.Disallow) And
                   .TryGetValueEx("NewX_AVM-DE_IsMeshable", Host.IsMeshable) And
                   .TryGetValueEx("NewX_AVM-DE_Priority", Host.Priority) And
                   .TryGetValueEx("NewX_AVM-DE_FriendlyName", Host.FriendlyName) And
                   .TryGetValueEx("NewX_AVM-DE_FriendlyNameIsWriteable", Host.FriendlyNameIsWriteable)

        End With
    End Function

    Public Function HostsCheckUpdate() As Boolean Implements IHostsSCPD.HostsCheckUpdate
        Return Not TR064Start(ServiceFile, "X_AVM-DE_HostsCheckUpdate", ServiceID, Nothing).ContainsKey("Error")
    End Function

    Public Function HostDoUpdate(MACAddress As String) As Boolean Implements IHostsSCPD.HostDoUpdate
        Return Not TR064Start(ServiceFile, "X_AVM-DE_HostDoUpdate", ServiceID, New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress}}).ContainsKey("Error")
    End Function

    Public Function SetPrioritizationByIP(IPAddress As String, Priority As Boolean) As Boolean Implements IHostsSCPD.SetPrioritizationByIP
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetPrioritizationByIP", ServiceID, New Dictionary(Of String, String) From {{"NewIPAddress", IPAddress},
                                                                                                                     {"NewX_AVM-DE_Priority", Priority.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetHostListPath(ByRef HostListPath As String) As Boolean Implements IHostsSCPD.GetHostListPath
        Return TR064Start(ServiceFile, "X_AVM-DE_GetHostListPath", ServiceID, Nothing).TryGetValueEx("NewX_AVM-DE_HostListPath", HostListPath)
    End Function

    <Obsolete> Public Function GetHostList(ByRef Hosts As HostList) As Boolean Implements IHostsSCPD.GetHostList
        Dim LuaPath As String = String.Empty
        Return GetHostListPath(LuaPath) AndAlso XML.Deserialize($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{LuaPath}", True, Hosts)
    End Function

    Public Async Function GetHostList() As Task(Of HostList) Implements IHostsSCPD.GetHostList
        ' Ermittle den Pfad zu Hostlost und deserialisiere die Daten
        Dim HostListUrl As String = String.Empty

        If GetHostListPath(HostListUrl) Then
            ' X_AVM-DE_GetHostListPath liefert nur den lua-Part. Der Rest muss vorangefügt werden.
            Return Await XML.DeserializeAsyncFromPath(Of HostList)($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{HostListUrl}")
        Else
            ' Gib eine leere Liste zurück
            Return New HostList
        End If

    End Function

    Public Function GetMeshListPath(ByRef MeshListPath As String) As Boolean Implements IHostsSCPD.GetMeshListPath
        Return TR064Start(ServiceFile, "X_AVM-DE_GetMeshListPath", ServiceID, Nothing).TryGetValueEx("NewX_AVM-DE_MeshListPath", MeshListPath)
    End Function

    Public Function GetFriendlyName(ByRef FriendlyName As String) As Boolean Implements IHostsSCPD.GetFriendlyName
        Return TR064Start(ServiceFile, "X_AVM-DE_GetFriendlyName", ServiceID, Nothing).TryGetValueEx("NewX_AVM-DE_FriendlyName", FriendlyName)
    End Function

    Public Function SetFriendlyName(FriendlyName As String) As Boolean Implements IHostsSCPD.SetFriendlyName
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetFriendlyName", ServiceID, New Dictionary(Of String, String) From {{"NewX_AVM-DE_FriendlyName", FriendlyName}}).ContainsKey("Error")
    End Function

    Public Function SetFriendlyNameByIP(IPAddress As String, FriendlyName As String) As Boolean Implements IHostsSCPD.SetFriendlyNameByIP
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetFriendlyNameByIP", ServiceID, New Dictionary(Of String, String) From {{"NewIPAddress", IPAddress},
                                                                                                                   {"NewX_AVM-DE_FriendlyName", FriendlyName}}).ContainsKey("Error")
    End Function

    Public Function SetFriendlyNameByMAC(MACAddress As String, FriendlyName As String) As Boolean Implements IHostsSCPD.SetFriendlyNameByMAC
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetFriendlyNameByMAC", ServiceID, New Dictionary(Of String, String) From {{"NewMACAddress", MACAddress},
                                                                                                                    {"NewX_AVM-DE_FriendlyName", FriendlyName}}).ContainsKey("Error")
    End Function
End Class
