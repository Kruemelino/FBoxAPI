''' <summary>
''' TR-064 Support – WANDSLLinkConfig
''' Date: 2015-11-20 
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wandsllinkconfigSCPD.pdf</see>
''' </summary>
Public Interface IWANDSLLinkConfigSCPD
    Inherits IServiceBase

    Function GetInfo(ByRef Info As DSLLinkConfigInfo) As Boolean

    Function GetStatisticsTotal(ByRef StatisticsTotal As DSLLinkStatTotal) As Boolean

    ''' <summary>
    ''' Returns the state of a DSLDiagnose which is automatically started in IGD, when the DSLSync parameter is set to 0 for at least 300 s at DSLSignalLossTime. 
    ''' When DSLDiagnoseState is "DONE_CABLE_NOK" the CableNokDistance parameter shows the cable breakage location in meters. 
    ''' The DSLActive parameter is set to 1 when the CPE device is willing to initiate a sync DSL state. 
    ''' </summary>
    ''' <remarks>Required rights: None</remarks>
    Function GetDSLDiagnoseInfo(ByRef Info As DSLDiagnoseInfo) As Boolean

    ''' <summary>
    ''' Returns more DSL information than the GetInfo action.
    ''' </summary>
    ''' <remarks>Required rights: None</remarks>
    Function GetDSLInfo(ByRef Info As DSLInfo) As Boolean

End Interface
