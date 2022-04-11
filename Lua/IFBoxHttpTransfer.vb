''' <summary>
''' Schnittstelle für verschiedene Funktionen, die von der Fritz!Box via http abgerufen werden können.<br/>
''' Herunterladen von Kontaktbildern, TAM-Messages, Fax-Nachrichten.<br/>
''' Starten von Lua-Abfragen
''' </summary>
Public Interface IFBoxHttpTransfer

    ''' <summary>
    ''' Startet eine Lua Abfrage
    ''' </summary>
    ''' <param name="SessionID">Übergabe einer SessionID in der Form: sid=0000000000000000</param>
    ''' <param name="Abfrage">Auflistung der einzelnen Abfragen.</param>
    ''' <returns><see cref="String"/></returns>
    Function GetLuaResponse(SessionID As String, Abfrage As IEnumerable(Of String)) As Task(Of String)

    ''' <summary>
    ''' Experimentell: Sendet Daten an eine Lua-Script
    ''' </summary>
    ''' <param name="LuaFile">Lua-Script, an die die Daten gesendet werden sollen. z.B. data (Eingabe ohne Dateierweiterung)</param>
    ''' <param name="SessionID">Übergabe einer SessionID in der Form: sid=0000000000000000</param>
    ''' <param name="Data">Daten, die gesendet werden sollen</param>
    ''' <returns>Antwort der Fritz!Box</returns>
    Function GetLuaPostResponse(LuaFile As String, SessionID As String, Data As String) As Task(Of String)

    ''' <summary>
    ''' Startet eine Lua Abfrage
    ''' </summary>
    ''' <param name="SessionID">Übergabe einer SessionID in der Form: sid=0000000000000000</param>
    ''' <param name="Abfrage">Auflistung der einzelnen Abfragen.</param>
    ''' <returns><see cref="IO.Stream"/></returns>
    Function GetLuaResponseStream(SessionID As String, Abfrage As IEnumerable(Of String)) As Task(Of IO.Stream)

    ''' <summary>
    ''' Lädt eine Datei von der Fritz!Box auf das Dateisystem herunter.
    ''' </summary>
    ''' <param name="UniformResourceIdentifier">Uri zur Datei auf der Fritz!Box.</param>
    ''' <param name="DateiPfad">Dateipfad auf dem Dateisystem.</param>
    Function DownloadToFileSystem(UniformResourceIdentifier As Uri, DateiPfad As String) As Task(Of Boolean)

    ''' <summary>
    ''' Lädt eine Datei von der Fritz!Box auf in ein Byte-Array.
    ''' </summary>
    ''' <param name="UniformResourceIdentifier">Uri zur Datei auf der Fritz!Box.</param>
    Function GetData(UniformResourceIdentifier As Uri) As Task(Of Byte())

End Interface
