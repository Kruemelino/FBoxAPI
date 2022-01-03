''' <summary>
''' TR-064 Support – Speedtest
''' Date: 2015-02-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf</see>
''' </summary>
Public Interface IX_speedtestSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As SpeedtestInfo) As Boolean
    Function SetConfig(EnableTcp As Boolean, EnableUdp As Boolean, EnableUdpBidirect As Boolean, WANEnableTcp As Boolean, WANEnableUdp As Boolean) As Boolean

End Interface
