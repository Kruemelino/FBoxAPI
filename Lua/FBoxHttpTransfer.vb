Imports System.IO

Friend Class FBoxHttpTransfer
    Inherits APIConnectorBase
    Implements IFBoxHttpTransfer

    Private ReadOnly Property Client As WebFunctions

    Public Sub New(http As WebFunctions)
        Client = http
    End Sub

    Public Async Function GetLuaResponse(SessionID As String, Abfrage As IEnumerable(Of String)) As Task(Of String) Implements IFBoxHttpTransfer.GetLuaResponse
        Return Await Client.GetStringWebClientAsync(New Uri($"https://{FritzBoxTR64.FBoxIPAdresse}/query.lua?{SessionID}&{String.Join("&", Abfrage.ToArray)}"))
    End Function

    Public Async Function GetLuaPostResponse(LuaFile As String, SessionID As String, Data As String) As Task(Of String) Implements IFBoxHttpTransfer.GetLuaPostResponse
        Return Await Client.PostStringWebClientAsyncLua(New Uri($"https://{FritzBoxTR64.FBoxIPAdresse}/{LuaFile}.lua"), $"{SessionID}&{Data}")
    End Function

    Public Async Function GetLuaResponseStream(SessionID As String, Abfrage As IEnumerable(Of String)) As Task(Of Stream) Implements IFBoxHttpTransfer.GetLuaResponseStream
        Return Await Client.GetStreamWebClientAsync(New Uri($"https://{FritzBoxTR64.FBoxIPAdresse}/query.lua?{SessionID}&{String.Join("&", Abfrage.ToArray)}"))
    End Function

    Public Async Function DownloadToFileSystem(UniformResourceIdentifier As Uri, DateiPfad As String) As Task(Of Boolean) Implements IFBoxHttpTransfer.DownloadToFileSystem
        Return Await Client.GetFileWebClientAsync(UniformResourceIdentifier, DateiPfad)
    End Function

    Public Async Function GetData(UniformResourceIdentifier As Uri) As Task(Of Byte()) Implements IFBoxHttpTransfer.GetData
        Return Await Client.GetDataWebClientAsync(UniformResourceIdentifier)
    End Function

End Class
