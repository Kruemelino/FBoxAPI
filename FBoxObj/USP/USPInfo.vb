''' <summary>
''' TR-064 Support – X_AVM-DE_USPController 
''' Date: 2022-10-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_uspcontrollerSCPD.pdf</see>
''' </summary>
Public Class USPInfo

    ''' <summary>
    ''' Min number of characters for EndpointID: 0
    ''' </summary>
    Public Property MinCharsEndpointID As Integer

    ''' <summary>
    ''' Max number of characters for EndpointID: 64
    ''' </summary>
    Public Property MaxCharsEndpointID As Integer

    ''' <summary>
    ''' Usable characters for EndpointID
    ''' </summary>
    ''' <remarks>0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz:-._%</remarks>
    Public Property AllowedCharsEndpointID As String

    ''' <summary>
    ''' Min number of characters for Hostname: 0
    ''' </summary>
    Public Property MinCharsHostname As Integer

    ''' <summary>
    ''' Max number of characters for Hostname: 255
    ''' </summary>
    Public Property MaxCharsHostname As Integer

    ''' <summary>
    ''' Min number of characters for Path: 0
    ''' </summary>
    Public Property MinCharsPath As Integer

    ''' <summary>
    ''' Max number of characters for Path: 1024
    ''' </summary>
    Public Property MaxCharsPath As Integer

    ''' <summary>
    ''' Min number of characters for Username: 0
    ''' </summary>
    Public Property MinCharsUsername As Integer

    ''' <summary>
    ''' Max number of characters for Username:255
    ''' </summary>
    Public Property MaxCharsUsername As Integer

    ''' <summary>
    ''' Min number of characters for Password: 0
    ''' </summary>
    Public Property MinCharsPassword As Integer

    ''' <summary>
    ''' Max number of characters for Password: 255
    ''' </summary>
    Public Property MaxCharsPassword As Integer

    ''' <summary>
    ''' Enabled/Disabled status for MyFRITZ USP contoller
    ''' </summary>
    Public Property USPMyFRITZEnabled As Boolean
End Class
