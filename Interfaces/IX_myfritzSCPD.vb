''' <summary>
''' TR-064 Support – X_MyFritz
''' Date: 2022-02-14
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf</see>
''' </summary>
Public Interface IX_myfritzSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Required rights: App, Phone, NAS or Homeauto
    ''' </summary>
    Function GetInfo(ByRef Enabled As Boolean,
                     ByRef DynDNSName As String,
                     ByRef Port As Integer,
                     ByRef DeviceRegistered As Boolean,
                     ByRef State As MyFritzStateEnum,
                     ByRef Email As String) As Boolean

    ''' <summary>
    ''' Enables or disables MyFRITZ
    ''' To enable -> Enabled = "1" and Email = "example@example.com"
    ''' To disable -> Enabled = "0" and Email = ""
    ''' Required rights: App
    ''' </summary>
    Function SetMyFritz(Enabled As Boolean, Email As String) As Boolean

    ''' <summary>
    ''' Required rights: App or Phone
    ''' </summary>
    Function GetNumberOfServices(ByRef NumberOfServices As Integer) As Boolean

    ''' <summary>
    ''' Read and entry to the list of MyFRITZ! services.
    ''' Required rights: App or Phone
    ''' </summary>
    ''' <remarks>The list of services may be reordered by invoking SetServiceByIndex. </remarks>
    Function GetServiceByIndex(Index As Integer, ByRef Info As MyFritzInfo) As Boolean

    ''' <summary>
    ''' Add a new entry to the list of MyFRITZ! services or change an existing entry in the list of MyFRITZ! Services.
    ''' Required rights: App or Phone
    ''' </summary>
    ''' <remarks>The list of services may be reordered after a change. Clients can only add services for themselves (their IP address).</remarks>
    Function SetServiceByIndex(NumberOfServices As Integer, ByRef Info As MyFritzInfo) As Boolean

    ''' <summary>
    ''' Required rights: App or Phone
    ''' </summary>
    Function DeleteServiceByIndex(NumberOfServices As Integer) As Boolean
End Interface
