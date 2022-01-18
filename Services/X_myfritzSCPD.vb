''' <summary>
''' TR-064 Support – X_MyFritz
''' Date: 2017-05-16
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf</see>
''' </summary>
Friend Class X_myfritzSCPD
    Implements IX_myfritzSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_myfritzSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_myfritzSCPD Implements IX_myfritzSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Enabled As Boolean, ByRef DynDNSName As String, ByRef Port As Integer, ByRef DeviceRegistered As Boolean) As Boolean Implements IX_myfritzSCPD.GetInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewEnable", Enabled) And
                   .TryGetValueEx("NewDynDNSName", DynDNSName) And
                   .TryGetValueEx("NewPort", Port) And
                   .TryGetValueEx("NewDeviceRegistered", DeviceRegistered)

        End With

    End Function

    Public Function GetNumberOfServices(ByRef NumberOfServices As Integer) As Boolean Implements IX_myfritzSCPD.GetNumberOfServices
        Return TR064Start(ServiceFile, "GetNumberOfServices", Nothing).TryGetValueEx("NewNumberOfServices", NumberOfServices)
    End Function

    Public Function GetServiceByIndex(Index As Integer, ByRef Info As MyFritzInfo) As Boolean Implements IX_myfritzSCPD.GetServiceByIndex
        If Info Is Nothing Then Info = New MyFritzInfo

        With TR064Start(ServiceFile, "GetServiceByIndex", New Dictionary(Of String, String) From {{"NewIndex", Index}})

            Info.Index = Index

            Return .TryGetValueEx("NewEnable", Info.Enabled) And
                   .TryGetValueEx("NewName", Info.Name) And
                   .TryGetValueEx("NewScheme", Info.Scheme) And
                   .TryGetValueEx("NewPort", Info.Port) And
                   .TryGetValueEx("NewURLPath", Info.URLPath) And
                   .TryGetValueEx("NewType", Info.Type) And
                   .TryGetValueEx("NewIPv4ForwardingWarning", Info.IPv4ForwardingWarning) And
                   .TryGetValueEx("NewIPv4Addresses", Info.IPv4Addresses) And
                   .TryGetValueEx("NewIPv6Addresses", Info.IPv6Addresses) And
                   .TryGetValueEx("NewIPv6InterfaceIDs", Info.IPv6InterfaceIDs) And
                   .TryGetValueEx("NewMACAddress", Info.MACAddress) And
                   .TryGetValueEx("NewHostName", Info.HostName) And
                   .TryGetValueEx("NewDynDnsLabel", Info.DynDnsLabel) And
                   .TryGetValueEx("NewStatus", Info.Status)
        End With
    End Function

    Public Function SetServiceByIndex(NumberOfServices As Integer, ByRef Info As MyFritzInfo) As Boolean Implements IX_myfritzSCPD.SetServiceByIndex
        Return Not TR064Start(ServiceFile, "SetServiceByIndex", New Dictionary(Of String, String) From {{"NewIndex", NumberOfServices},
                                                                                                        {"NewEnabled", Info.Enabled},
                                                                                                        {"NewName", Info.Name},
                                                                                                        {"NewScheme", Info.Scheme},
                                                                                                        {"NewPort", Info.Port},
                                                                                                        {"NewURLPath", Info.URLPath},
                                                                                                        {"NewIPv4Address", Info.IPv4Addresses},
                                                                                                        {"NewIPv6Address", Info.IPv6Addresses},
                                                                                                        {"NewIPv6InterfaceID", Info.IPv6InterfaceIDs},
                                                                                                        {"NewMACAddress", Info.MACAddress},
                                                                                                        {"NewHostName", Info.HostName}}).ContainsKey("Error")
    End Function

    Public Function DeleteServiceByIndex(NumberOfServices As Integer) As Boolean Implements IX_myfritzSCPD.DeleteServiceByIndex
        Return Not TR064Start(ServiceFile, "DeleteServiceByIndex", New Dictionary(Of String, String) From {{"NewIndex", NumberOfServices}}).ContainsKey("Error")
    End Function
End Class
