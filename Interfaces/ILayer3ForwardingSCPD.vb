''' <summary>
''' TR-064 Support – Layer3Forwarding
''' Date: 2009-07-15 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/layer3forwardingSCPD.pdf</see>
''' </summary>
Public Interface ILayer3ForwardingSCPD
    Inherits IServiceBase

    Function SetDefaultConnectionService(DefaultConnectionService As String) As Boolean
    Function GetDefaultConnectionService(ByRef DefaultConnectionService As String) As Boolean
    Function GetForwardNumberOfEntries(ByRef ForwardNumberOfEntries As Integer) As Boolean
    Function AddForwardingEntry(Entry As ForwardingEntry) As Boolean
    Function DeleteForwardingEntry(DestIPAddress As String,
                                   DestSubnetMask As String,
                                   SourceIPAddress As String,
                                   SourceSubnetMask As String) As Boolean
    Function GetSpecificForwardingEntry(DestIPAddress As String,
                                        DestSubnetMask As String,
                                        SourceIPAddress As String,
                                        SourceSubnetMask As String,
                                        ByRef Entry As ForwardingEntry) As Boolean
    Function GetGenericForwardingEntry(Index As Integer,
                                       ByRef Entry As ForwardingEntry) As Boolean
    Function SetForwardingEntryEnable(DestIPAddress As String,
                                      DestSubnetMask As String,
                                      SourceIPAddress As String,
                                      SourceSubnetMask As String,
                                      Enable As Boolean) As Boolean
End Interface
