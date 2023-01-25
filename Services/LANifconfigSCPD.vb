''' <summary>
''' TR-064 Support – LAN Ethernet Interface Config
''' Date: 2009-07-15 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanifconfigSCPD.pdf</see>
''' </summary>
Friend Class LANifconfigSCPD
    Implements ILANifconfigSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2009, 7, 15) Implements ILANifconfigSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements ILANifconfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.lanifconfigSCPD Implements ILANifconfigSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))
        TR064Start = Start
    End Sub

    Public Function SetEnable(Enable As Boolean) As Boolean Implements ILANifconfigSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetInfo(ByRef Enable As Boolean, ByRef Status As String, ByRef MACAddress As String, ByRef MaxBitRate As String, ByRef DuplexMode As String) As Boolean Implements ILANifconfigSCPD.GetInfo
        With TR064Start(ServiceFile, "GetInfo", Nothing)

            Return .TryGetValueEx("NewEnable", Enable) And
                   .TryGetValueEx("NewStatus", Status) And
                   .TryGetValueEx("NewMACAddress", MACAddress) And
                   .TryGetValueEx("NewMaxBitRate", MaxBitRate) And
                   .TryGetValueEx("NewDuplexMode", DuplexMode)
        End With
    End Function

    Public Function GetStatistics(ByRef BytesSent As Integer, ByRef BytesReceived As Integer, ByRef PacketsSent As Integer, ByRef PacketsReceived As Integer) As Boolean Implements ILANifconfigSCPD.GetStatistics
        With TR064Start(ServiceFile, "GetStatistics", Nothing)

            Return .TryGetValueEx("NewBytesSent", BytesSent) And
                   .TryGetValueEx("NewBytesReceived", BytesReceived) And
                   .TryGetValueEx("NewPacketsSent", PacketsSent) And
                   .TryGetValueEx("NewPacketsReceived", PacketsReceived)
        End With
    End Function
End Class
