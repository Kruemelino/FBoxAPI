''' <summary>
''' TR-064 Support – X_AVM-DE_WANFiber
''' Date: 2023-08-30
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanfiberSCPD.pdf</see>
''' </summary>
Public Class WANGPONInfo
    Public Property GponSerial As String
    Public Property PONId As String
    Public Property ONUId As Integer
    ''' <summary>
    ''' PPTP or VEIP
    ''' </summary>
    Public Property UNIType As String
    Public Property GEMPortCount As Integer
End Class
