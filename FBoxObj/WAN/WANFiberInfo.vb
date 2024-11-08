''' <summary>
''' TR-064 Support – X_AVM-DE_WANFiber
''' Date: 2023-08-30
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanfiberSCPD.pdf</see>
''' </summary>
Public Class WANFiberInfo

    Public Property OpticalSignalLevel As Integer

    ''' <summary>
    ''' Optical level that is used to declare the downstream low received optical power alarm in dBm/1000
    ''' </summary>
    Public Property LowerOpticalThreshold As Integer

    ''' <summary>
    ''' Optical level that is used to declare the downstream high received optical power alarm in dBm/1000
    ''' </summary>
    Public Property UpperOpticalThreshold As Integer

    ''' <summary>
    ''' Current measurement of mean optical launch power in dBm/1000
    ''' </summary>
    Public Property TransmitOpticalLevel As Integer

    ''' <summary>
    ''' Minimum mean optical launch power that is used to declare the low transmit optical power alarm in dBm/1000
    ''' </summary>
    Public Property LowerTransmitPowerThreshold As Integer

    ''' <summary>
    ''' Maximum mean optical launch power that is used to declare the high transmit optical power alarm in dBm/1000
    ''' </summary>
    Public Property UpperTransmitPowerThreshold As Integer
    Public Property SFPVendor As String
    Public Property SFPPartNumber As String
    Public Property SFPSerialNumber As String
    Public Property SFPType As Integer
    Public Property TXWaveLength As Integer

    ''' <summary>
    ''' Current Fiber mode (AON, GPON, XGSPON,...)
    ''' </summary>
    Public Property FiberMode As String
End Class
