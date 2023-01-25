''' <summary>
''' TR-064 Support – Speedtest
''' Date: 2015-02-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_speedtestSCPD.pdf</see>
''' </summary>
Public Interface IX_speedtestSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As SpeedtestInfo) As Boolean
    Function SetConfig(EnableTcp As Boolean, EnableUdp As Boolean, EnableUdpBidirect As Boolean, WANEnableTcp As Boolean, WANEnableUdp As Boolean) As Boolean

    ''' <summary>
    ''' Return statistic values to current or last speedtest started with EnableTcp/EnableUdp or EnableUdpBidirect. Only incoming packets to the FRITZ!Box on the spezific speedtest ports are counted.
    ''' </summary>
    ''' <param name="Statistics"></param>
    ''' <returns></returns>
    Function GetStatistics(ByRef Statistics As SpeedtestStatistics) As Boolean

    ''' <summary>
    ''' Resets all statistic counter to zero
    ''' </summary>
    Function ResetStatistics() As Boolean

End Interface
