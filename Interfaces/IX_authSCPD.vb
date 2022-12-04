''' <summary>
''' TR-064 Support – Authentication
''' Date: 2016-10-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf</see>
''' </summary>
Public Interface IX_authSCPD
    Inherits IServiceBase

    ''' <summary>Read restrictive values for action parameters.</summary>
    ''' <param name="Enabled">2-Factor-Authentication enabled</param>
    ''' <remarks>Required rights: any</remarks>
    Function GetInfo(ByRef Enabled As Boolean) As Boolean

    Function GetState(ByRef State As AuthStateEnum) As Boolean

    ''' <summary>
    ''' Start or stop a two-factor-authentication for the current user. 
    ''' </summary>
    Function SetConfig(Action As AuthActionEnum,
                       ByRef Token As String,
                       ByRef State As AuthStateEnum,
                       ByRef Methods As String) As Boolean

End Interface
