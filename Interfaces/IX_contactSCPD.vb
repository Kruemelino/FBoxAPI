''' <summary>
''' TR-064 Support – X_AVM-DE_OnTel
''' Date: 2021-02-09
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_contactSCPD.pdf</see>
''' </summary>
Public Interface IX_contactSCPD
    Inherits IServiceBase

    Function GetInfoByIndex(Index As Integer,
                            Optional ByRef Enable As Boolean = False,
                            Optional ByRef Status As String = "",
                            Optional ByRef LastConnect As String = "",
                            Optional ByRef Url As String = "",
                            Optional ByRef ServiceId As String = "",
                            Optional ByRef Username As String = "",
                            Optional ByRef Name As String = "") As Boolean

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
    ''' <param name="CallListURL">Represents the URL to the CallList.
    ''' The URL can be extended to limit the number of entries in the XML call list file.
    ''' E.g. max=42 would limit to 42 calls in the list.
    ''' If the parameter Is Not Set Or the value Is 0 all calls will be inserted into the Call list file.
    ''' The URL can be extended To fetch a limited number Of entries Using the parameter days.
    ''' E.g. days=7 would fetch the calls from now until 7 days in the past.
    ''' If the parameter Is Not Set Or the value Is 0 all calls will be inserted into the Call list file.
    ''' The parameter NewCallListURL Is empty, If the feature (CallList) Is disabled. If the feature
    ''' Is Not supported an internal error (820) Is returned. In the other case the URL Is returned.    
    '''     <list type="bullet">
    '''         <listheader>The following URL parameters are supported.</listheader>
    '''         <item><term>name</term> (number): number of days to look back for calls e.g. 1: calls from today and yesterday, 7: calls from the complete last week, Default 999</item>
    '''         <item><term>id</term> (number): calls since this unique ID</item>
    '''         <item><term>maxv</term> (number): maximum number of entries in call list, default 999</item>
    '''         <item><term>sid</term> (hex-string): Session ID for authentication </item>
    '''         <item><term>timestamp</term> (number): value from timestamp tag, to get only entries that are newer (timestamp Is resetted by a factory reset) </item>
    '''         <item><term>tr064sid</term>  (string): Session ID for authentication (obsolete)</item>
    '''         <item><term>type</term>  (string): optional parameter for type of output file: xml (default) or csv </item>
    '''     </list>
    '''     The parameters timestamp and id have to be used in combination. If only one of both is used, the feature Is Not supported. 
    ''' </param>

    ''' <remarks> 
    ''' </remarks>
    Function GetCallList(ByRef CallListURL As String) As Boolean

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
    '''     <item><term>pbid</term> (number): number of days to look back for calls e.g. 1: calls from today and yesterday, 7: calls from the complete last week, Default 999</item>
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
                                 Optional ByRef PhonebookName As String = "",
                                 Optional ByRef PhonebookExtraID As String = "") As Boolean

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
    ''' Get a single telephone book entry from the specified book using the unique ID from the entry.
    ''' </summary>
    ''' <param name="PhonebookID">Number for a single phonebook.</param>
    ''' <param name="PhonebookEntryUniqueID">Unique identifier (number) for a single entry in a phonebook.</param>
    ''' <param name="PhonebookEntryData">XML document with a single entry. </param>

    Function GetPhonebookEntryUID(PhonebookID As Integer, PhonebookEntryUniqueID As Integer, ByRef PhonebookEntryData As String) As Boolean

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
    ''' The action returns the unique ID of the new or changed entry
    ''' </summary>
    ''' <param name="PhonebookID">ID of the phonebook.</param>
    ''' <param name="PhonebookEntryData">XML document with a single entry</param>
    ''' <param name="PhonebookEntryUniqueID">The action returns the unique ID of the new or changed entry.</param>

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
    ''' <param name="PhonebookEntryData">A call barring entry</param>
    Function GetCallBarringEntry(PhonebookEntryID As Integer, ByRef PhonebookEntryData As String) As Boolean

    ''' <summary>
    ''' Returns a call barring entry by its number. If the number exists in the internal phonebook 
    ''' but not in the specific call barring phonebook, error code 714 is returned.
    ''' </summary>
    ''' <param name="Number">phone number</param>
    ''' <param name="PhonebookEntryData">XML document with a single call barring entry.</param>
    Function GetCallBarringEntryByNum(Number As String, ByRef PhonebookEntryData As String) As Boolean

    ''' <summary>
    ''' Returns a url which leads to an xml formatted file which contains all entries of the call barring phonebook.
    ''' </summary>
    ''' <param name="PhonebookURL">Url of the call barring phonebook</param>

    Function GetCallBarringList(ByRef PhonebookURL As String) As Boolean

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

    ''' <summary>
    ''' Enable or disable a deflection.
    ''' DeflectionID is in the range of 0 .. NumberOfDeflections-1
    ''' </summary>
    ''' <param name="DeflectionId">Die ID der Rufumleitung</param>
    ''' <param name="Enable">Neuer Aktivierungszustand</param>
    Function SetDeflectionEnable(DeflectionId As Integer, Enable As Boolean) As Boolean

#End Region
End Interface
