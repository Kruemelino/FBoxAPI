''' <summary>
''' TR-064 Support – X_AVM-DE_Filelinks
''' Date: 2016-07-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf</see>
''' </summary>
Public Interface IX_filelinksSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Returns the number of NAS filelinks.
    ''' </summary>
    ''' <remarks>Required rights: NASRight</remarks>
    Function GetNumberOfFilelinkEntries(ByRef NumberOfEntries As Integer) As Boolean

    ''' <summary>
    ''' Read values/states for a NAS filelink by <paramref name="Index"/>.
    ''' </summary>
    ''' <param name="Index">Index can have a value from 0 .. NumberOfEntries-1 (from <see cref="GetNumberOfFilelinkEntries(ByRef Integer)"/>)</param>
    ''' <remarks>Required rights: NASRight</remarks>
    Function GetGenericFilelinkEntry(Index As Integer, ByRef Entry As FileLinkEntry) As Boolean

    ''' <summary>
    ''' Read values/states for a NAS filelink by <paramref name="ID"/>. 
    ''' </summary>
    ''' <remarks>Required rights: NASRight</remarks>
    Function GetSpecificFilelinkEntry(ID As String, ByRef Entry As FileLinkEntry) As Boolean

    ''' <summary>
    ''' Create a new filelink entry
    ''' </summary>
    ''' <remarks>Required rights: NASRight</remarks>
    Function NewFilelinkEntry(Path As String, AccessCountLimit As Integer, Expire As Integer, ByRef ID As String) As Boolean

    ''' <summary>
    ''' Change a filelink entry selected by <paramref name="ID"/>.
    ''' </summary>
    ''' <remarks>Required rights: NASRight</remarks>
    Function SetFilelinkEntry(ID As String, AccessCountLimit As Integer, Expire As Integer) As Boolean

    ''' <summary>
    ''' Delete a filelink entry selected by <paramref name="ID"/>.
    ''' </summary>
    ''' <remarks>Required rights: NASRight</remarks>
    Function DeleteFilelinkEntry(ID As String) As Boolean

    ''' <summary>
    ''' Gets a path to a lua script file, which generates an XML structured list of file links depending on the granted
    ''' ghts of the user.
    ''' </summary>
    ''' <remarks>Required rights: NASRight, ConfigRight</remarks>
    Function GetFilelinkListPath(ByRef FilelinkListPath As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: GetFilelinkListPath wird als <see cref="FileLinkList"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetFilelinkList(ByRef List As FileLinkList) As Boolean
End Interface
