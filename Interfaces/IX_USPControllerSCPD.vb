''' <summary>
''' TR-064 Support – X_AVM-DE_USPController 
''' Date: 2022-10-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_uspcontrollerSCPD.pdf</see>
''' </summary>
Public Interface IX_USPControllerSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Returns information about variables for the USPController actions.
    ''' </summary>
    ''' <remarks>Required rights: Any</remarks>
    Function GetInfo(ByRef Info As USPInfo) As Boolean

    ''' <summary>
    ''' Retrieves all attributes of a USP controller by Index
    ''' </summary>
    Function GetUSPControllerByIndex(ByRef USPCntrlr As USPController, Index As Integer) As Boolean

    ''' <summary>
    ''' Returns the number of USP controllers configured on this device
    ''' </summary>
    Function GetUSPControllerNumberOfEntries(ByRef USPControllerNumberOfEntries As Integer) As Boolean

    ''' <summary>
    ''' Adds a new USP controller and returns the Index of it
    ''' </summary>
    Function AddUSPController(USPCntrlr As USPController, Password As String, ByRef Index As Integer) As Boolean

    ''' <summary>
    ''' Removes a USP controller by Index
    ''' </summary>
    Function DeleteUSPControllerByIndex(Index As Integer) As Boolean

    ''' <summary>
    ''' Enables/Disables a USP controller by Index
    ''' </summary>
    Function SetUSPControllerEnableByIndex(Index As Integer, Enable As Boolean) As Boolean

    ''' <summary>
    ''' Retrieves the Enabled/Disabled status of the USP MyFRITZ controller
    ''' </summary>
    ''' <remarks>This action is optional.<br/>Required rights: Any</remarks>
    Function GetUSPMyFRITZEnable(ByRef USPMyFRITZEnabled As Boolean) As Boolean

    ''' <summary>
    ''' REnables/Disables the USP MyFRITZ controller<br/>1 to enable, 0 to disable
    ''' </summary>
    ''' <remarks>This action is optional.<br/>Required rights: AppRights</remarks>
    Function SetUSPMyFRITZEnable(USPMyFRITZEnabled As Boolean) As Boolean
End Interface