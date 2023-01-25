''' <summary>
''' TR-064 Support – X AVM Media 
''' Date: 2022-02-25
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_mediaSCPD.pdf</see>
''' </summary>
Public Interface IX_MediaSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Returns status information to DVB-C 
    ''' </summary>
    ''' <remarks>Required rights: None</remarks>
    Function GetInfo(ByRef DVBCEnabled As Boolean, ByRef StationSearchStatus As StationSearchStatusEnum, ByRef SearchProgress As Integer) As Boolean

    ''' <summary>
    ''' Returns enabled/disabled status of DVB-C
    ''' </summary>
    ''' <remarks>Required rights: None</remarks>
    Function GetDVBCEnable(ByRef DVBCEnabled As Boolean) As Boolean

    ''' <summary>
    ''' Enables/disables DVB-C
    ''' </summary>
    ''' <remarks>The Fritz!Box will reboot if the DVBCEnabled was changed.<br/>Required rights: App</remarks>
    Function SetDVBCEnable(DVBCEnabled As Boolean) As Boolean

    ''' <summary>
    ''' Starts and stops StationSearch and returns the status.
    ''' </summary>
    ''' <remarks>This action can only be used while DVBC is enabled<br/>Required rights: App</remarks>
    Function StationSearch(StationSearchMode As StationSearchModeEnum, ByRef StationSearchStatus As StationSearchStatusEnum) As Boolean

    ''' <summary>
    ''' Returns the station search progress and station search status
    ''' </summary>
    ''' <remarks>Required rights: None</remarks>
    Function GetSearchProgress(ByRef StationSearchStatus As StationSearchStatusEnum, ByRef SearchProgress As Integer) As Boolean
End Interface
