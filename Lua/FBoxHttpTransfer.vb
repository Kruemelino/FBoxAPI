Friend Class FBoxHttpTransfer
    Inherits LogBase
    Implements IFBoxHttpTransfer

    Private ReadOnly Property Client As TR064WebFunctions

    Public Sub New(http As TR064WebFunctions)
        Client = http
    End Sub

    ''' <summary>
    ''' Führt eine einfache Lua-Abfrage durch.
    ''' </summary>
    ''' <param name="SessionID">sid=...</param>
    ''' <param name="Abfrage">Auflistung der einzelnen Abfragen</param>
    Public Async Function GetLuaResponse(SessionID As String, Abfrage As IEnumerable(Of String)) As Task(Of String) Implements IFBoxHttpTransfer.GetLuaResponse
        Return Await Client.GetStringWebClientAsync(New Uri($"https://{FritzBoxTR64.FBoxIPAdresse}/query.lua?{SessionID}&{String.Join("&", Abfrage.ToArray)}"))
    End Function

    Public Async Function DownloadToFileSystem(UniformResourceIdentifier As Uri, DateiPfad As String) As Task(Of Boolean) Implements IFBoxHttpTransfer.DownloadToFileSystem
        Return Await Client.GetFileWebClientAsync(UniformResourceIdentifier, DateiPfad)
    End Function

    Public Async Function GetData(UniformResourceIdentifier As Uri) As Task(Of Byte()) Implements IFBoxHttpTransfer.GetData
        Return Await Client.GetDataWebClientAsync(UniformResourceIdentifier)
    End Function
End Class
