''' <summary>
''' TR-064 Support – X_MyFritz
''' Date: 2017-05-16
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_myfritzSCPD.pdf</see>
''' </summary>
Public Interface IX_myfritzSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Enabled As Boolean, ByRef DynDNSName As String, ByRef Port As Integer, ByRef DeviceRegistered As Boolean) As Boolean

    Function GetNumberOfServices(ByRef NumberOfServices As Integer) As Boolean

    ''' <summary>
    ''' Read and entry to the list of MyFRITZ! services. 
    ''' </summary>
    ''' <remarks>The list of services may be reordered by invoking SetServiceByIndex. </remarks>
    Function GetServiceByIndex(Index As Integer, ByRef Info As MyFritzInfo) As Boolean

    ''' <summary>
    ''' Add a new entry to the list of MyFRITZ! services or change an existing entry in the list of MyFRITZ! Services.
    ''' </summary>
    ''' <remarks>The list of services may be reordered after a change. Clients can only add services for themselves (their IP address).</remarks>
    Function SetServiceByIndex(NumberOfServices As Integer, ByRef Info As MyFritzInfo) As Boolean

    Function DeleteServiceByIndex(NumberOfServices As Integer) As Boolean
End Interface
