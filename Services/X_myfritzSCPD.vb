''' <summary>
''' TR-064 Support – X_MyFritz
''' Date: 2017-05-16
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf</see>
''' </summary>
Friend Class X_myfritzSCPD
    Implements IX_myfritzSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IX_myfritzSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IX_myfritzSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IX_myfritzSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        ServiceFile = SCPDFiles.x_myfritzSCPD

        TR064Start = Start

        PushStatus = Status

    End Sub

    Public Function GetInfo(ByRef Enabled As Boolean, ByRef DynDNSName As String, ByRef Port As Integer, ByRef DeviceRegistered As Boolean) As Boolean Implements IX_myfritzSCPD.GetInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewEnable") And .ContainsKey("NewDynDNSName") Then

                Enabled = CBool(.Item("NewEnable"))
                DynDNSName = .Item("NewDynDNSName").ToString
                Port = CInt(.Item("NewPort"))
                DeviceRegistered = CBool(.Item("NewDeviceRegistered"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo (MyFritz) konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With

    End Function

    Public Function GetNumberOfServices(ByRef NumberOfServices As Integer) As Boolean Implements IX_myfritzSCPD.GetNumberOfServices
        With TR064Start(ServiceFile, "GetNumberOfServices", Nothing)

            If .ContainsKey("NewNumberOfServices") Then

                NumberOfServices = CInt(.Item("NewNumberOfServices"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetNumberOfServices konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetServiceByIndex(NumberOfServices As Integer, ByRef Info As MyFritzInfo) As Boolean Implements IX_myfritzSCPD.GetServiceByIndex
        If Info Is Nothing Then Info = New MyFritzInfo

        With TR064Start(ServiceFile, "GetServiceByIndex", New Hashtable From {{"NewIndex", NumberOfServices}})

            If .ContainsKey("NewEnable") And .ContainsKey("NewName") Then

                Info.NumberOfServices = NumberOfServices
                Info.Enabled = CBool(.Item("NewEnable"))
                Info.Name = .Item("NewName").ToString
                Info.Scheme = .Item("NewScheme").ToString
                Info.Port = CInt(.Item("NewPort"))
                Info.URLPath = .Item("NewURLPath").ToString
                Info.Type = .Item("NewType").ToString
                Info.IPv4ForwardingWarning = CInt(.Item("NewIPv4ForwardingWarning"))
                Info.IPv4Addresses = .Item("NewIPv4Addresses").ToString
                Info.IPv6Addresses = .Item("NewIPv6Addresses").ToString
                Info.IPv6InterfaceIDs = .Item("NewIPv6InterfaceIDs").ToString
                Info.MACAddress = .Item("NewMACAddress").ToString
                Info.HostName = .Item("NewHostName").ToString
                Info.DynDnsLabel = .Item("NewDynDnsLabel").ToString
                Info.Status = CInt(.Item("NewStatus"))

                PushStatus.Invoke(LogLevel.Debug, $"GetTAMInfo ({NumberOfServices}): {Info.Name}; {Info.Enabled}")

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetServiceByIndex konnte für nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetServiceByIndex(NumberOfServices As Integer, ByRef Info As MyFritzInfo) As Boolean Implements IX_myfritzSCPD.SetServiceByIndex
        With TR064Start(ServiceFile, "SetServiceByIndex", New Hashtable From {{"NewIndex", NumberOfServices},
                                                                              {"NewEnabled", Info.Enabled},
                                                                              {"NewName", Info.Name},
                                                                              {"NewScheme", Info.Scheme},
                                                                              {"NewPort", Info.Port},
                                                                              {"NewURLPath", Info.URLPath},
                                                                              {"NewIPv4Address", Info.IPv4Addresses},
                                                                              {"NewIPv6Address", Info.IPv6Addresses},
                                                                              {"NewIPv6InterfaceID", Info.IPv6InterfaceIDs},
                                                                              {"NewMACAddress", Info.MACAddress},
                                                                              {"NewHostName", Info.HostName}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function DeleteServiceByIndex(NumberOfServices As Integer) As Boolean Implements IX_myfritzSCPD.DeleteServiceByIndex
        With TR064Start(ServiceFile, "DeleteServiceByIndex", New Hashtable From {{"NewIndex", NumberOfServices}})
            Return Not .ContainsKey("Error")
        End With
    End Function
End Class
