''' <summary>
''' TR-064 Support – X_AVM-DE_WANFiber
''' Date: 2023-08-30
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanfiberSCPD.pdf</see>
''' </summary>
Public Class WANFiberStat
    Public Property BytesSent As Integer
    Public Property BytesReceived As Integer
    Public Property PacketsSent As Integer
    Public Property PacketsReceived As Integer
    Public Property PacketErrorsSent As Integer
    Public Property PacketErrorsReceived As Integer
    Public Property PacketsMulticast As Integer
    Public Property ConnectionRateDown As Integer
    Public Property ConnectionRateUp As Integer
    Public Property BestTrainState As Integer
    Public Property Resyncs As Integer
    Public Property MinutesInShowtime As Integer
End Class
