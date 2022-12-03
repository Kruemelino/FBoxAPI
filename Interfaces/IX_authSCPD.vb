''' <summary>
''' TR-064 Support – Authentication
''' Date: 2016-10-06
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_auth.pdf</see>
''' </summary>
Public Interface IX_authSCPD
    Inherits IServiceBase

    Property TR064StartWithToken As Func(Of SCPDFiles, String, String, Dictionary(Of String, String), Dictionary(Of String, String))


    ''' <summary>Read restrictive values for action parameters.</summary>
    ''' <param name="Enabled">2-Factor-Authentication enabled</param>
    ''' <remarks>Required rights: any</remarks>
    Function GetInfo(ByRef Enabled As Boolean) As Boolean

    Function GetState(ByRef State As AuthStateEnum) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: Die Action <see cref="GetState(ByRef AuthStateEnum)"/> wird mit einem Authentifizierungstoken im SOAP Header ausgeführt.
    ''' </summary>
    ''' <param name="Token">Token, welcher von der Fritz!Box per <see cref="SetConfig(AuthActionEnum, ByRef String, ByRef AuthStateEnum, ByRef String)"/> übermittelt wurde."/></param>
    ''' <param name="State">Aktueller Status des 2-Faktor-Authentikation</param>
    Function GetStateWithToken(Token As String, ByRef State As AuthStateEnum) As Boolean

    ''' <summary>
    ''' Start or stop a two-factor-authentication for the current user. 
    ''' </summary>
    Function SetConfig(Action As AuthActionEnum,
                       ByRef Token As String,
                       ByRef State As AuthStateEnum,
                       ByRef Methods As String) As Boolean

End Interface
