''' <summary>
''' TR-064 Support – X_MyFritz
''' Date: 2022-02-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf</see>
''' </summary>
Friend Class X_myfritzSCPD
    Implements IX_myfritzSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 2, 14) Implements IX_myfritzSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IX_myfritzSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.x_myfritzSCPD Implements IX_myfritzSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer = 1 Implements IX_myfritzSCPD.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetInfo(ByRef Enabled As Boolean,
                            ByRef DynDNSName As String,
                            ByRef Port As Integer,
                            ByRef DeviceRegistered As Boolean,
                            ByRef State As MyFritzStateEnum,
                            ByRef Email As String) As Boolean Implements IX_myfritzSCPD.GetInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewEnable", Enabled) And
                   .TryGetValueEx("NewDynDNSName", DynDNSName) And
                   .TryGetValueEx("NewPort", Port) And
                   .TryGetValueEx("NewDeviceRegistered", DeviceRegistered) And
                   .TryGetValueEx("NewState", State) And
                   .TryGetValueEx("NewEmail", Email)

        End With

    End Function

    Public Function SetMyFritz(Enabled As Boolean, Email As String) As Boolean Implements IX_myfritzSCPD.SetMyFritz
        Return Not TR064Start(ServiceFile, "SetMyFritz", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnabled", Enabled.ToBoolStr},
                                                                      {"NewEmail", Email}}).ContainsKey("Error")
    End Function

    Public Function GetNumberOfServices(ByRef NumberOfServices As Integer) As Boolean Implements IX_myfritzSCPD.GetNumberOfServices
        Return TR064Start(ServiceFile, "GetNumberOfServices", ServiceID, Nothing).TryGetValueEx("NewNumberOfServices", NumberOfServices)
    End Function

    Public Function GetServiceByIndex(Index As Integer, ByRef Info As MyFritzInfo) As Boolean Implements IX_myfritzSCPD.GetServiceByIndex
        If Info Is Nothing Then Info = New MyFritzInfo

        With TR064Start(ServiceFile, "GetServiceByIndex", ServiceID, New Dictionary(Of String, String) From {{"NewIndex", Index.ToString}})

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
        Return Not TR064Start(ServiceFile, "SetServiceByIndex", ServiceID,
                              New Dictionary(Of String, String) From {{"NewIndex", NumberOfServices.ToString},
                                                                      {"NewEnabled", Info.Enabled},
                                                                      {"NewName", Info.Name},
                                                                      {"NewScheme", Info.Scheme},
                                                                      {"NewPort", Info.Port.ToString},
                                                                      {"NewURLPath", Info.URLPath},
                                                                      {"NewIPv4Address", Info.IPv4Addresses},
                                                                      {"NewIPv6Address", Info.IPv6Addresses},
                                                                      {"NewIPv6InterfaceID", Info.IPv6InterfaceIDs},
                                                                      {"NewMACAddress", Info.MACAddress},
                                                                      {"NewHostName", Info.HostName}}).ContainsKey("Error")
    End Function

    Public Function DeleteServiceByIndex(NumberOfServices As Integer) As Boolean Implements IX_myfritzSCPD.DeleteServiceByIndex
        Return Not TR064Start(ServiceFile, "DeleteServiceByIndex", ServiceID, New Dictionary(Of String, String) From {{"NewIndex", NumberOfServices.ToString}}).ContainsKey("Error")
    End Function

End Class
