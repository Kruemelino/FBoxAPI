''' <summary>
''' TR-064 Support – X_AVM-DE_OnTel
''' Date: 2023-01-19
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_contactSCPD.pdf</see>
''' </summary>
Public Interface IX_contactSCPD
    Inherits IServiceBase

    <Obsolete("This function is obsolete and will be removed in a future version. Use the function GetInfoByIndex(Index As Integer, ByRef Info As OnTelInfo) instead.")>
    Function GetInfoByIndex(Index As Integer,
                                       Optional ByRef Enable As Boolean = False,
                                       Optional ByRef Status As String = "",
                                       Optional ByRef LastConnect As String = "",
                                       Optional ByRef Url As String = "",
                                       Optional ByRef ServiceId As String = "",
                                       Optional ByRef Username As String = "",
                                       Optional ByRef Name As String = "") As Boolean

    Function GetInfoByIndex(Index As Integer, ByRef Info As OnTelInfo) As Boolean
    ''' <summary>
    ''' The action is used to trigger the telephone book synchronization manually. The
    ''' synchronization starts if switching from false to true. After enabling, the synchronization is
    ''' automatically started periodically once within 24 hours.
    ''' All accounts are triggered to check for updates on COMS by invoking this action. If the
    ''' revision has not increased, no synchronization will be made.  
    ''' </summary>
    Function SetEnableByIndex(Index As Integer, Enable As Boolean) As Boolean

    ''' <summary>
    ''' If the given index addresses an existing account the configuration is changed. If the index
    ''' addresses a new account and the index is OntelNumberOfEntries + 1 then a new account is generated.
    ''' </summary>
    ''' <param name="Name">Telephone book name</param>
    Function SetConfigByIndex(Index As Integer, Enable As Boolean, Url As String, ServiceId As String, Username As String, Password As String, Name As String) As Boolean

    Function GetNumberOfEntries(ByRef OntelNumberOfEntries As Integer) As Boolean

    Function DeleteByIndex(Index As Integer) As Boolean

    ''' <summary>
    ''' Ermittelt die URL zum Herunterladen des Anrufliste.
    ''' </summary>
    ''' <remarks>
    ''' <para>The URL can be extended to limit the number of entries in the XML call list file.<br/>
    ''' E.g. max=42 would limit to 42 calls in the list.<br/>
    ''' If the parameter is not set or the value is 0 all calls will be inserted into the call list file.</para>
    ''' <para>The URL can be extended to fetch a limited number of entries using the parameter days.<br/>
    ''' E.g. days=7 would fetch the calls from now until 7 days in the past.<br/>
    ''' If the parameter is not set or the value is 0 all calls will be inserted into the call list file.</para>
    ''' <para>The parameter NewCallListURL is empty, if the feature (CallList) is disabled. If the feature is not supported an internal error (820) is returned. In the other case the URL is returned.</para>
    ''' <para>The parameters <paramref name="timestamp"/> and <paramref name="id"/> have to be used in combination. If only one of both is used, the feature is not supported.</para>
    ''' </remarks>
    ''' <param name="CallListURL">Represents the URL to the CallList.</param>
    ''' <param name="days">number of days to look back for calls e.g. 1: calls from today and yesterday, 7: calls from the complete last week, Default 999</param>
    ''' <param name="id">calls since this unique ID</param>
    ''' <param name="max">maximum number of entries in call list, default 999</param>
    ''' <param name="sid">Session ID for authentication</param>
    ''' <param name="timestamp">Timestamp of call list creation (unique ID per call list).<br/>
    ''' Value from timestamp tag, to get only entries that are newer (timestamp is resetted by a factory reset)</param>
    ''' <param name="type">optional parameter for type of output file: xml (default) or csv</param>
    Function GetCallList(ByRef CallListURL As String,
                         Optional days As Integer = 999,
                         Optional id As Integer = 0,
                         Optional max As Integer = 999,
                         Optional sid As String = "",
                         Optional timestamp As Integer = 0,
                         Optional typeCSV As Boolean = False) As Boolean

    ''' <summary>
    ''' Inoffizielle asynchrone Action: Ermittelt die Anrufliste als deserialisiertes Datenobjekt vom Typ <see cref="CallList"/>.
    ''' </summary>
    ''' <remarks>
    ''' <para>The URL can be extended to limit the number of entries in the XML call list file.<br/>
    ''' E.g. max=42 would limit to 42 calls in the list.<br/>
    ''' If the parameter is not set or the value is 0 all calls will be inserted into the call list file.</para>
    ''' <para>The URL can be extended to fetch a limited number of entries using the parameter days.<br/>
    ''' E.g. days=7 would fetch the calls from now until 7 days in the past.<br/>
    ''' If the parameter is not set or the value is 0 all calls will be inserted into the call list file.</para>
    ''' <para>The parameter NewCallListURL is empty, if the feature (CallList) is disabled. If the feature is not supported an internal error (820) is returned. In the other case the URL is returned.</para>
    ''' <para>The parameters <paramref name="timestamp"/> and <paramref name="id"/> have to be used in combination. If only one of both is used, the feature is not supported.</para>
    ''' </remarks>
    ''' <param name="days">number of days to look back for calls e.g. 1: calls from today and yesterday, 7: calls from the complete last week, Default 999</param>
    ''' <param name="id">calls since this unique ID</param>
    ''' <param name="max">maximum number of entries in call list, default 999</param>
    ''' <param name="sid">Session ID for authentication</param>
    ''' <param name="timestamp">Timestamp of call list creation (unique ID per call list).<br/>
    ''' Value from timestamp tag, to get only entries that are newer (timestamp is resetted by a factory reset)</param>
    Function GetCallList(Optional days As Integer = 999,
                         Optional id As Integer = 0,
                         Optional max As Integer = 999,
                         Optional sid As String = "",
                         Optional timestamp As Integer = 0) As Task(Of CallList)

#Region "Phonebook"
    ''' <summary>
    ''' Ermittelt die Liste der Telefonbücher. 
    ''' </summary>
    ''' <param name="PhonebookList">Liste der Telefonbuch IDs</param>

    Function GetPhonebookList(ByRef PhonebookList As Integer()) As Boolean

    ''' <summary>
    ''' Ermittelt die URL zum Herunterladen des Telefonbuches mit der <paramref name="PhonebookID"/>.
    ''' </summary>
    ''' <param name="PhonebookURL"> Represents the URL to the phone book with <paramref name="PhonebookID"/>.
    '''     The following URL parameters are supported.
    '''     <list type="bullet">
    '''     <listheader>The following URL parameters are supported.</listheader>
    '''     <item><term>pbid</term> (number): Phonebook ID</item>
    '''     <item><term>max</term> (number): maximum number of entries in call list, default 999</item>
    '''     <item><term>sid</term> (hex-string): Session ID for authentication </item>
    '''     <item><term>timestamp</term> (number): value from timestamp tag, to get the phonebook content only if last modification was made after this timestamp</item>
    '''     <item><term>tr064sid</term> (string): Session ID for authentication (obsolete)</item>
    ''' </list></param>
    ''' <param name="PhonebookID">ID of the phonebook.</param>
    ''' <param name="PhonebookName">Name of the phonebook.</param>
    ''' <param name="PhonebookExtraID">The value of <paramref name="PhonebookExtraID"/> may be an empty string. </param>
    Function GetPhonebook(PhonebookID As Integer,
                          ByRef PhonebookURL As String,
                          ByRef PhonebookName As String,
                          ByRef PhonebookExtraID As String) As Boolean

    ''' <summary>
    ''' Inoffizielle asynchrone Action: Ermittelt das Telefonbuches als deserialisiertes Datenobjekt vom Typ <see cref="PhonebooksType"/>.
    ''' </summary>
    ''' <param name="PhonebookID">ID of the phonebook.</param>
    ''' <param name="TimeStamp">value from timestamp tag, to get the phonebook content only if last modification was made after this timestamp</param>
    Function GetPhonebook(PhonebookID As Integer,
                          Optional TimeStamp As Integer = 0) As Task(Of PhonebooksType)

    ''' <summary>
    ''' Fügt ein neues Telefonbuch hinzu.
    ''' </summary>
    ''' <param name="PhonebookName">Name des neuen Telefonbuches.</param>
    ''' <param name="PhonebookExtraID">ExtraID des neuen Telefonbuches. (Optional)</param>
    Function AddPhonebook(PhonebookName As String, Optional PhonebookExtraID As String = "") As Boolean

    ''' <summary>
    ''' Löscht das Telefonbuch mit der <paramref name="NewPhonebookID"/>.
    ''' </summary>
    ''' <remarks>The default phonebook (PhonebookID = 0) is not deletable, but therefore, each entry will be deleted And the phonebook will be empty afterwards.</remarks>
    ''' <param name="NewPhonebookID">ID of the phonebook.</param>
    ''' <param name="PhonebookExtraID">Optional parameter to make a phonebook unique.</param>
    Function DeletePhonebook(NewPhonebookID As Integer, Optional PhonebookExtraID As String = "") As Boolean
#End Region

#Region "PhonebookEntry"
    ''' <summary>
    ''' Get a single telephone book entry from the specified book.
    ''' </summary>
    ''' <param name="PhonebookID">Number for a single phonebook.</param>
    ''' <param name="PhonebookEntryID">Unique identifier (number) for a single entry in a phonebook.</param>
    ''' <param name="PhonebookEntryData">XML document with a single entry. </param>

    Function GetPhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: GetPhonebookEntry wird als <see cref="Contact"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetPhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer) As Task(Of Contact)

    ''' <summary>
    ''' Get a single telephone book entry from the specified book using the unique ID from the entry.
    ''' </summary>
    ''' <param name="PhonebookID">Number for a single phonebook.</param>
    ''' <param name="PhonebookEntryUniqueID">Unique identifier (number) for a single entry in a phonebook.</param>
    ''' <param name="PhonebookEntryData">XML document with a single entry. </param>

    Function GetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryUniqueID As Integer, ByRef PhonebookEntryData As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: GetPhonebookEntryUID wird als <see cref="Contact"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryUniqueID As Integer) As Task(Of Contact)

    ''' <summary>
    ''' Add a new or change an existing entry in a telephone book using the unique ID of the entry
    ''' <list type="bullet">
    '''     <listheader>
    '''         <term>Add new entry:</term>    
    '''     </listheader>
    '''     <item>set phonebook ID and XML entry data structure (without the unique ID tag)</item>
    ''' </list>
    ''' <list type="bullet">
    '''     <listheader>
    '''         <term>Change existing entry:</term>    
    '''     </listheader>
    '''     <item>set phonebook ID and XML entry data structure with the unique ID tag (e.g. <uniqueid>28</uniqueid>)</item>
    ''' </list>
    ''' Changes to online phonebooks are not allowed.
    ''' </summary>
    ''' <param name="PhonebookID">ID of the phonebook.</param>
    ''' <param name="PhonebookEntryData">XML document with a single entry</param>
    ''' <param name="PhonebookEntryUniqueID">The action returns the unique ID of the new or changed entry.</param>
    ''' <returns>The action returns the unique ID of the new or changed entry.</returns>
    Function SetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryData As String, ByRef PhonebookEntryUniqueID As Integer) As Boolean

    ''' <summary>
    ''' Delete an existing telephone book entry.
    ''' Changes to online phonebooks are not allowed.
    ''' </summary>
    ''' <param name="PhonebookID">ID of the phonebook.</param>
    ''' <param name="PhonebookEntryID">Number for a single entry in a phonebook.</param>

    Function DeletePhonebookEntry(PhonebookID As Integer, PhonebookEntryID As Integer) As Boolean

    ''' <summary>
    ''' Delete an existing telephone book entry using the unique ID from the entry.
    ''' Changes to online phonebooks are not allowed.
    ''' </summary>
    ''' <param name="PhonebookID">ID of the phonebook.</param>
    ''' <param name="NewPhonebookEntryUniqueID">Unique identifier (number) for a single entry in a phonebook.</param>

    Function DeletePhonebookEntryUID(PhonebookID As Integer, NewPhonebookEntryUniqueID As Integer) As Boolean
#End Region

#Region "CallBarring"
    ''' <summary>
    ''' Returns a call barring entry by its PhonebookEntryID of the specific call barring phonebook. 
    ''' </summary>
    ''' <param name="PhonebookEntryID">ID of the specific call barring phonebook.</param>
    ''' <param name="PhonebookEntryData">>XML string with a call barring entry</param>
    Function GetCallBarringEntry(PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean

    ''' <summary>
    ''' Inoffizielle asynchrone Action: Ermittelt das den zugehörigen Sperrlisteneintrag als deserialisiertes Datenobjekt vom Typ <see cref="Contact"/>.
    ''' </summary>
    ''' <param name="PhonebookEntryID">ID of the specific call barring phonebook.</param>
    Function GetCallBarringEntry(PhonebookEntryID As Integer) As Task(Of Contact)

    ''' <summary>
    ''' Returns a call barring entry by its number. If the number exists in the internal phonebook 
    ''' but not in the specific call barring phonebook, error code 714 is returned.
    ''' </summary>
    ''' <param name="Number">phone number</param>
    ''' <param name="PhonebookEntryData">XML string with a call barring entry.</param>
    Function GetCallBarringEntryByNum(Number As String, ByRef PhonebookEntryData As String) As Boolean

    ''' <summary>
    ''' Inoffizielle asynchrone Action: Ermittelt das den zugehörigen Sperrlisteneintrag als deserialisiertes Datenobjekt vom Typ <see cref="Contact"/>.
    ''' </summary>
    ''' <param name="Number">phone number</param>
    Function GetCallBarringEntryByNum(Number As String) As Task(Of Contact)

    ''' <summary>
    ''' Returns a url which leads to an xml formatted file which contains all entries of the call barring phonebook.
    ''' </summary>
    ''' <param name="PhonebookURL">Url of the call barring phonebook</param>
    Function GetCallBarringList(ByRef PhonebookURL As String) As Boolean

    ''' <summary>
    ''' Inoffizielle asynchrone Action: Ermittelt das die Sperrlisten als deserialisiertes Datenobjekt vom Typ <see cref="PhonebooksType"/>.
    ''' </summary>
    Function GetCallBarringList() As Task(Of PhonebooksType)

    ''' <summary>
    ''' Add a phonebook entry to the specific call barring phonebook. When no uniqueid is given 
    ''' a new entry is created. Even when an entry with the given number is already existing.
    ''' When a uniqueid is set which already exist, this entry will be overwritten. When a uniqueid
    ''' is given which does not exist, a new entry is created and the new uniqueid is returned in argument NewPhonebookEntryUniqueID.
    ''' </summary>
    ''' <param name="PhonebookEntryData">XML document with a single call barring entry.</param>
    ''' <param name="PhonebookEntryUniqueID">Unique identifier (number) for a single entry in the specific call barring phonebook.</param>
    Function SetCallBarringEntry(PhonebookEntryData As String, Optional ByRef PhonebookEntryUniqueID As Integer = 0) As Boolean

    ''' <summary>
    ''' Delete an entry of the call barring phonebook by its uniqueid.
    ''' </summary>
    ''' <param name="NewPhonebookEntryUniqueID">uniqueid of an entry</param>
    Function DeleteCallBarringEntryUID(NewPhonebookEntryUniqueID As Integer) As Boolean
#End Region

#Region "DECTHandset"
    ''' <summary>
    ''' Ermittelt die Liste der DECTHandset. 
    ''' </summary>
    ''' <param name="DectIDList">Comma separated list of DectID</param>
    Function GetDECTHandsetList(ByRef DectIDList As String()) As Boolean

    Function GetDECTHandsetInfo(DectID As Integer, ByRef HandsetName As String, ByRef PhonebookID As String) As Boolean

    Function SetDECTHandsetPhonebook(DectID As Integer, PhonebookID As Integer) As Boolean
#End Region

#Region "Deflections"
    ''' <summary>
    ''' Get the number of deflection entrys.
    ''' </summary>
    ''' <param name="NumberOfDeflections">Returns the number of deflection entrys</param>
    Function GetNumberOfDeflections(ByRef NumberOfDeflections As String) As Boolean

    ''' <summary>
    ''' Get the parameter for a deflection entry.
    ''' DeflectionID is in the range of 0 .. NumberOfDeflections-1.
    ''' </summary>
    ''' <param name="DeflectionInfo">Komplexes Datenelement, was alle Informationen zu der Rufumleitung enthält.</param>
    ''' <param name="DeflectionId">Die ID der Rufumleitung</param>
    Function GetDeflection(ByRef DeflectionInfo As Deflection, DeflectionId As Integer) As Boolean

    ''' <summary>
    ''' Returns a list of deflections.
    ''' </summary>
    ''' <param name="DeflectionList">List of deflections</param>
    Function GetDeflections(ByRef DeflectionList As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: GetDeflections wird als <see cref="DeflectionList"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetDeflections(ByRef DeflectionList As DeflectionList) As Boolean

    Function GetDeflections() As Task(Of DeflectionList)

    ''' <summary>
    ''' Enable or disable a deflection.
    ''' DeflectionID is in the range of 0 .. NumberOfDeflections-1
    ''' </summary>
    ''' <param name="DeflectionId">Die ID der Rufumleitung</param>
    ''' <param name="Enable">Neuer Aktivierungszustand</param>
    Function SetDeflectionEnable(DeflectionId As Integer, Enable As Boolean) As Boolean

#End Region
End Interface
