''' <summary>
''' TR-064 Support – X_AVM-DE_USPController 
''' Date: 2022-10-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_uspcontrollerSCPD.pdf</see>
''' </summary>
Public Class USPController

    ''' <summary>
    ''' Controller index (0 - USPController NumberOfEntries-1)
    ''' </summary>
    Public Property Index As Integer

    ''' <summary>
    ''' Enabled/Disabled status for this controller
    ''' </summary>
    Public Property Enable As Boolean = True

    ''' <summary>
    ''' Unique Key for this controller
    ''' </summary>
    Public Property EndpointID As String

    ''' <summary>
    ''' Used Message Transfer Protocol
    ''' </summary>
    Public Property MTP As String

    ''' <summary>
    ''' Server hostname
    ''' </summary>
    Public Property Hostname As String

    ''' <summary>
    ''' Server path
    ''' </summary>
    Public Property Path As String

    Public Property Port As Integer

    Public Property UseTLS As Boolean = True

    ''' <summary>
    ''' Boolean: access right for "Smarthome"
    ''' </summary>
    Public Property AccessRightSmarthome As Boolean = False

    ''' <summary>
    ''' Boolean: access right for "Mesh"
    ''' </summary>
    Public Property AccessRightMesh As Boolean = False

    ''' <summary>
    ''' Boolean: access right for "Internet"
    ''' </summary>
    Public Property AccessRightInternet As Boolean = False

    ''' <summary>
    ''' Boolean: access right for "System"
    ''' </summary>
    Public Property AccessRightSystem As Boolean = False

    ''' <summary>
    ''' Boolean: access right for "Controller"
    ''' </summary>
    Public Property AccessRightController As Boolean = False

    ''' <summary>
    ''' Boolean: access right for "WiFi"
    ''' </summary>
    Public Property AccessRightWiFi As Boolean = False

    ''' <summary>
    ''' Boolean: access right for "VoIP"
    ''' </summary>
    Public Property AccessRightVoIP As Boolean = False

    ''' <summary>
    ''' Server username (for login)
    ''' </summary>
    Public Property Username As String

End Class
