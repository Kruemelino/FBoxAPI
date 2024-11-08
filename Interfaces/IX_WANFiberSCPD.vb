''' <summary>
''' TR-064 Support – X_AVM-DE_WANFiber
''' Date: 2023-08-30
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanfiberSCPD.pdf</see>
''' </summary>
Public Interface IX_WANFiberSCPD
    Inherits IServiceBase

    ''' <summary>    ''' 
    ''' Returns information about the optical device. 
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetInfo(ByRef Info As WANFiberInfo) As Boolean

    ''' <summary>
    ''' Returns additional information about GPON
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetInfoGPON(ByRef Info As WANGPONInfo) As Boolean

    ''' <summary>
    ''' Returns the total fiber data statistics
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetStatistics(ByRef Statistics As WANFiberStat) As Boolean

End Interface
