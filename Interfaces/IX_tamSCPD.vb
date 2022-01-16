''' <summary>
''' TR-064 Support – X_AVM-DE_TAM 
''' Date: 2019-06-28
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_tam.pdf</see>
''' </summary>
Public Interface IX_tamSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Return a informations of tam index <paramref name="i"/>. 
    ''' </summary>
    ''' <param name="TAMInfo">Structure, which holds all data of the TAM</param>
    ''' <param name="i">Represents the index of all tam.</param>
    Function GetTAMInfo(ByRef TAMInfo As TAMInfo, i As Integer) As Boolean

    ''' <summary>
    ''' If Enable is set to true, the TAM will be visible in WebGUI. 
    ''' </summary>
    ''' <param name="Index">Index of TAM</param>
    ''' <param name="Enable">Enable state</param>
    Function SetEnable(Index As Integer, Enable As Boolean) As Boolean

    ''' <summary>
    ''' Create an URL to download the list of message for a specified TAM. 
    ''' </summary>
    ''' <remarks>If the HTTP request for the resulting URL fails, it is recommended to make a New SOAP request For GetMessageList or call the SOAP action DeviceConfig:X_AVM-DE_CreateUrlSID for a New session ID.<br/>
    ''' The following URL parameters are supported.
    ''' <list type="bullet">
    ''' <item>max: maximum number of entries in message list, default 999</item>
    ''' <item>sid: Session ID for authentication</item>
    ''' </list>
    ''' </remarks>
    ''' <param name="GetMessageListURL">URL to download the list of message for a specified TAM</param>
    ''' <param name="i">ID of the specified TAM</param>
    Function GetMessageList(ByRef GetMessageListURL As String, i As Integer) As Boolean

    ''' <summary>
    ''' Mark a specified message as read. A specific TAM is selected by Index.
    ''' The Index field from a message in the MessageList should be taken for the MessageIndex
    ''' to select a specific message. If the MarkedAsRead state variable is set to 1, the message
    ''' is marked as read, when it is 0, the message is marked as unread. The default value is 1
    ''' to guarantee downward compatibility to older clients.
    ''' </summary>
    ''' <param name="Index">Index of the MessageList</param>
    ''' <param name="MessageIndex">Index of the Message</param>
    ''' <param name="MarkedAsRead">Optional, to stay compatible with older clients, default value is 1</param>
    Function MarkMessage(Index As Integer, MessageIndex As Integer, MarkedAsRead As Boolean) As Boolean

    ''' <summary>
    ''' Delete a specified message. A specific TAM is selected by Index.
    ''' The Index field from a message in the MessageList should be taken for the MessageIndex
    ''' to select a specific message. 
    ''' </summary>
    ''' <param name="Index">Index of the MessageList</param>
    ''' <param name="MessageIndex">Index of the Message</param>
    Function DeleteMessage(Index As Integer, MessageIndex As Integer) As Boolean

    ''' <summary>
    ''' Returns the global information and the specific answering machine information as xml list.
    ''' </summary>
    ''' <param name="List">Represents the list of all tam.</param>
    Function GetList(ByRef List As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: GetList wird als <see cref="TAMList"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetList(ByRef List As TAMList) As Boolean
End Interface
