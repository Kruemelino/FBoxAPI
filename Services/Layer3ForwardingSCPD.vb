''' <summary>
''' TR-064 Support – Layer3Forwarding
''' Date: 2009-07-15 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/layer3forwardingSCPD.pdf</see>
''' </summary>
Friend Class Layer3ForwardingSCPD
    Implements ILayer3ForwardingSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements ILayer3ForwardingSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.layer3forwardingSCPD Implements ILayer3ForwardingSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function SetDefaultConnectionService(DefaultConnectionService As String) As Boolean Implements ILayer3ForwardingSCPD.SetDefaultConnectionService
        Return Not TR064Start(ServiceFile, "SetDefaultConnectionService", New Dictionary(Of String, String) From {{"NewDefaultConnectionService", DefaultConnectionService}}).ContainsKey("Error")
    End Function

    Public Function GetDefaultConnectionService(ByRef DefaultConnectionService As String) As Boolean Implements ILayer3ForwardingSCPD.GetDefaultConnectionService
        Return TR064Start(ServiceFile, "GetDefaultConnectionService", Nothing).TryGetValueEx("NewDefaultConnectionService", DefaultConnectionService)
    End Function

    Public Function GetForwardNumberOfEntries(ByRef ForwardNumberOfEntries As Integer) As Boolean Implements ILayer3ForwardingSCPD.GetForwardNumberOfEntries
        Return TR064Start(ServiceFile, "GetForwardNumberOfEntries", Nothing).TryGetValueEx("NewForwardNumberOfEntries", ForwardNumberOfEntries)
    End Function

    Public Function AddForwardingEntry(Entry As ForwardingEntry) As Boolean Implements ILayer3ForwardingSCPD.AddForwardingEntry
        Return Entry IsNot Nothing AndAlso
            Not TR064Start(ServiceFile, "AddForwardingEntry",
                           New Dictionary(Of String, String) From {{"NewType", Entry.Type},
                                                                   {"NewDestIPAddress", Entry.DestIPAddress},
                                                                   {"NewDestSubnetMask", Entry.DestSubnetMask},
                                                                   {"NewSourceIPAddress", Entry.SourceIPAddress},
                                                                   {"NewSourceSubnetMask", Entry.SourceSubnetMask},
                                                                   {"NewGatewayIPAddress", Entry.GatewayIPAddress},
                                                                   {"NewInterface", Entry.Interface},
                                                                   {"NewForwardingMetric", Entry.ForwardingMetric}}).ContainsKey("Error")

    End Function

    Public Function DeleteForwardingEntry(DestIPAddress As String, DestSubnetMask As String, SourceIPAddress As String, SourceSubnetMask As String) As Boolean Implements ILayer3ForwardingSCPD.DeleteForwardingEntry
        Return Not TR064Start(ServiceFile, "DeleteForwardingEntry",
                              New Dictionary(Of String, String) From {{"NewDestIPAddress", DestIPAddress},
                                                                      {"NewDestSubnetMask", DestSubnetMask},
                                                                      {"NewSourceIPAddress", SourceIPAddress},
                                                                      {"NewSourceSubnetMask", SourceSubnetMask}}).ContainsKey("Error")


    End Function

    Public Function GetSpecificForwardingEntry(DestIPAddress As String, DestSubnetMask As String, SourceIPAddress As String, SourceSubnetMask As String, ByRef Entry As ForwardingEntry) As Boolean Implements ILayer3ForwardingSCPD.GetSpecificForwardingEntry

        If Entry Is Nothing Then Entry = New ForwardingEntry

        With TR064Start(ServiceFile, "GetSpecificForwardingEntry",
                        New Dictionary(Of String, String) From {{"NewDestIPAddress", DestIPAddress},
                                                                {"NewDestSubnetMask", DestSubnetMask},
                                                                {"NewSourceIPAddress", SourceIPAddress},
                                                                {"NewSourceSubnetMask", SourceSubnetMask}})

            Entry.DestIPAddress = DestIPAddress
            Entry.DestSubnetMask = DestSubnetMask
            Entry.SourceIPAddress = SourceIPAddress
            Entry.SourceSubnetMask = SourceSubnetMask

            Return .TryGetValueEx("NewGatewayIPAddress", Entry.GatewayIPAddress) And
                   .TryGetValueEx("NewEnable", Entry.Enable) And
                   .TryGetValueEx("NewStatus", Entry.Status) And
                   .TryGetValueEx("NewType", Entry.Type) And
                   .TryGetValueEx("NewForwardingMetric", Entry.ForwardingMetric) And
                   .TryGetValueEx("NewInterface", Entry.Interface)
        End With
    End Function

    Public Function GetGenericForwardingEntry(Index As Integer, ByRef Entry As ForwardingEntry) As Boolean Implements ILayer3ForwardingSCPD.GetGenericForwardingEntry
        If Entry Is Nothing Then Entry = New ForwardingEntry

        With TR064Start(ServiceFile, "GetGenericForwardingEntry", New Dictionary(Of String, String) From {{"NewForwardingIndex", Index}})

            Return .TryGetValueEx("NewEnable", Entry.Enable) And
                   .TryGetValueEx("NewStatus", Entry.Status) And
                   .TryGetValueEx("NewType", Entry.Type) And
                   .TryGetValueEx("NewDestIPAddress", Entry.DestIPAddress) And
                   .TryGetValueEx("NewDestSubnetMask", Entry.DestSubnetMask) And
                   .TryGetValueEx("NewSourceIPAddress", Entry.SourceIPAddress) And
                   .TryGetValueEx("NewSourceSubnetMask", Entry.SourceSubnetMask) And
                   .TryGetValueEx("NewGatewayIPAddress", Entry.GatewayIPAddress) And
                   .TryGetValueEx("NewInterface", Entry.Interface) And
                   .TryGetValueEx("NewForwardingMetric", Entry.ForwardingMetric)

        End With
    End Function

    Public Function SetForwardingEntryEnable(DestIPAddress As String, DestSubnetMask As String, SourceIPAddress As String, SourceSubnetMask As String, Enable As Boolean) As Boolean Implements ILayer3ForwardingSCPD.SetForwardingEntryEnable
        Return Not TR064Start(ServiceFile, "SetForwardingEntryEnable",
                              New Dictionary(Of String, String) From {{"NewDestIPAddress", DestIPAddress},
                                                                      {"NewDestSubnetMask", DestSubnetMask},
                                                                      {"NewSourceIPAddress", SourceIPAddress},
                                                                      {"NewSourceSubnetMask", SourceSubnetMask},
                                                                      {"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function
End Class
