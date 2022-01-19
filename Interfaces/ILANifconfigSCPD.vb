''' <summary>
''' TR-064 Support – LAN Ethernet Interface Config
''' Date: 2009-07-15 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/lanifconfigSCPD.pdf</see>
''' </summary>
Public Interface ILANifconfigSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Writeable but internally ignored. 
    ''' </summary>
    Function SetEnable(Enable As Boolean) As Boolean

    Function GetInfo(ByRef Enable As Boolean,
                     ByRef Status As String,
                     ByRef MACAddress As String,
                     ByRef MaxBitRate As String,
                     ByRef DuplexMode As String) As Boolean

    Function GetStatistics(ByRef BytesSent As Integer,
                           ByRef BytesReceived As Integer,
                           ByRef PacketsSent As Integer,
                           ByRef PacketsReceived As Integer) As Boolean
End Interface
