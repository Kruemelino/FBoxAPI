''' <summary>
''' TR-064 Support – X_AVM-DE_WANMobileConnection
''' Date: 2022-11-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanmobileconnSCPD.pdf</see>
''' </summary>
Public Class WANMobileConnectionInfo
    Public Property Enabled As Boolean
    ''' <summary>
    ''' <list type="table">
    ''' <item><term>factory default</term>FRITZ!Box has factory default configuration</item>
    ''' <item><term>unconfigured</term>No PIN is configured</item>
    ''' <item><term>pin configured</term>PIN is set</item>
    ''' <item><term>checking SIM card</term>Checking SIM card and PIN</item>
    ''' <item><term>PIN successful</term>PIN is set and correct</item>
    ''' <item><term>no PIN mode</term>SIM card needs no PIN</item>
    ''' <item><term>change PIN</term>PIN has to be changed</item>
    ''' <item><term>SIM unlock</term>Device can be used with any SIM card</item>
    ''' <item><term>SIM lock</term>Device can only used with special SIM cards</item>
    ''' <item><term>SIM card defect </term>The SIM card is defect</item>
    ''' <item><term>SIM card locked </term>SIM card is completely locked, because entering PUK failed too many times</item>
    ''' <item><term>no SIM card</term>No SIM card is in device</item>
    ''' <item><term>enter PIN</term>Enter the SIM card PIN</item>
    ''' <item><term>enter PUK</term>Enter the SIM card PUK and a new PIN</item>
    ''' <item><term>PIN not possible </term>New PIN can not be used for this SIM card</item>
    ''' <item><term>Empty String </term>Unknown Status</item>
    ''' </list>
    ''' </summary>
    Public Property Status As String
    Public Property PINFailureCount As Integer
    Public Property PUKFailureCount As Integer
End Class
