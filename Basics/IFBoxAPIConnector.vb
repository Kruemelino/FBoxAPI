Public Interface IFBoxAPIConnector
    ''' <summary>
    ''' Routine, welche die Log-Message in das Log der Zielanwendung schreibt.
    ''' </summary>
    ''' <param name="MessageContainer">Containerklasse vom Typ <see cref="FBoxAPI.LogMessage"/>, welche alle Informationen enthält, aus der <see cref="FBoxAPI"/> Schnittstelle enthält.</param>
    Sub LogMessage(MessageContainer As LogMessage)

    ''' <summary>
    ''' Signalisiert eine gestartete Zwei-Faktor-Authentifikation.
    ''' </summary>
    ''' <param name="Methods">Erlaubte Methoden: z. B. button,dtmf;*14048</param>
    Sub Signal2FAuthentication(Methods As String)

    Property AbortAuthentication As Boolean
    Property AuthenticationSuccesful As Boolean
End Interface
