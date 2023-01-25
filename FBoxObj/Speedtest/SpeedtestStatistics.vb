Public Class SpeedtestStatistics
    ''' <summary>
    ''' Total number of bytes counted since reset
    ''' </summary>
    Public Property ByteCount As Integer

    ''' <summary>
    ''' Current data rate in kbits
    ''' </summary>
    Public Property KbitsCurrent As Integer

    ''' <summary>
    ''' Data rate in kbits since start of measurement
    ''' </summary>
    Public Property KbitsAvg As Integer

    ''' <summary>
    ''' Total number of packets counted since reset
    ''' </summary>
    Public Property PacketCount As Integer

    ''' <summary>
    ''' Current data rate in packets per second
    ''' </summary>
    Public Property PPSCurrent As Integer

    ''' <summary>
    ''' Data rate in packets per second since start of measurement
    ''' </summary>
    Public Property PPSAvg As Integer
End Class
