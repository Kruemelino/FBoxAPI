Imports System.Net
Imports System.Text
Friend Class TR064HttpBasics
    Inherits LogBase

    Private Const DefaultHeaderKeepAlive As Boolean = False
    Private Const DefaultHeaderUserAgent As String = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"
    Private Const DefaultMinTimout As Integer = 100
    Private ReadOnly Property GlobalTimeout As Integer
    Private ReadOnly Property PushStatus As Action(Of LogMessage)

    Public Sub New(Status As Action(Of LogMessage), timeout As Integer)
        PushStatus = Status
        GlobalTimeout = Math.Max(timeout, DefaultMinTimout)
    End Sub

#Region "Netzwerkfunktionen"
    ''' <summary>
    ''' Führt einen Ping zur Gegenstelle aus
    ''' </summary>
    ''' <param name="IPAdresse">IP-Adresse Netzwerkname der Gegenstelle.</param>
    ''' <returns>Boolean</returns>
    Friend Function Ping(IPAdresse As String) As Boolean

        Dim PingSender As New NetworkInformation.Ping()
        Dim Options As New NetworkInformation.PingOptions() With {.DontFragment = True}
        Dim PingReply As NetworkInformation.PingReply = Nothing

        Dim buffer As Byte() = Encoding.ASCII.GetBytes(String.Empty)

        Try
            PingReply = PingSender.Send(IPAdresse, GlobalTimeout, buffer, Options)

        Catch ex As Exception
            PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Ping zu {IPAdresse} nicht erfolgreich (timeout: {GlobalTimeout}).", ex))
        End Try

        Return PingReply IsNot Nothing AndAlso PingReply.Status = NetworkInformation.IPStatus.Success

        PingSender.Dispose()
    End Function

#Region "GET"
    ''' <summary>
    ''' Lädt die angeforderte Ressource als <see cref="String"/> synchron herunter. Die herunterzuladende Ressource ist als <see cref="Uri"/> angegeben.
    ''' </summary>
    ''' <param name="UniformResourceIdentifier">Ein <see cref="Uri"/>-Objekt, das den herunterzuladenden URI enthält.</param>
    ''' <param name="Response">Ein <see cref="String"/> mit der angeforderten Ressource.</param>
    ''' <param name="ZeichenCodierung">(Optional) Legt die <see cref="Encoding"/> für den Download von Zeichenfolgen fest.</param>
    ''' <param name="Headers">(Optional) Zusätzliche Header für den Download von Zeichenfolgen</param>
    ''' <returns>Boolean, je nach Erfolg der Abfrage.</returns>
    Friend Function DownloadString(UniformResourceIdentifier As Uri, ByRef Response As String, Optional ZeichenCodierung As Encoding = Nothing, Optional Headers As WebHeaderCollection = Nothing) As Boolean

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12

        Select Case UniformResourceIdentifier.Scheme
            Case Uri.UriSchemeHttp, Uri.UriSchemeHttps

                Using webClient As New WebClient With {.Proxy = Nothing,
                                                       .CachePolicy = New Cache.HttpRequestCachePolicy(Cache.HttpRequestCacheLevel.BypassCache),
                                                       .Encoding = If(ZeichenCodierung, Encoding.GetEncoding(DfltCodePageFritzBox))}
                    With webClient

                        With .Headers
                            .Set(HttpRequestHeader.UserAgent, DefaultHeaderUserAgent)
                            .Set(HttpRequestHeader.KeepAlive, DefaultHeaderKeepAlive.ToString)

                            If Headers IsNot Nothing Then .Add(Headers)
                        End With

                        Try
                            Response = .DownloadString(UniformResourceIdentifier)

                            PushStatus?.Invoke(CreateLog(LogLevel.Trace, Response))

                            Return True

                        Catch ex As ArgumentNullException
                            PushStatus?.Invoke(CreateLog(LogLevel.Error, ex))

                        Catch ex As WebException
                            ' Der durch Kombinieren von BaseAddress und address gebildete URI ist ungültig.
                            ' - oder -
                            ' Fehler beim Herunterladen der Ressource.
                            PushStatus?.Invoke(CreateLog(LogLevel.Error, $"Link: {UniformResourceIdentifier.AbsoluteUri} ", ex))

                        Catch ex As NotSupportedException
                            PushStatus?.Invoke(CreateLog(LogLevel.Error, ex))

                        End Try
                    End With
                End Using
            Case Else
                PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Unexpected Uri.Scheme: {UniformResourceIdentifier.AbsoluteUri}"))

        End Select
        Return False
    End Function

#End Region

#Region "POST"
    ''' <summary>
    ''' Lädt die angegebene Zeichenfolge in die angegebene Ressource hoch.
    ''' </summary>
    ''' <param name="UniformResourceIdentifier">Der <see cref="Uri"/> der Ressource, die die Zeichenfolge empfangen soll.</param>
    ''' <param name="PostData">Die Uploadzeichenfolge.</param>
    ''' <param name="NC">Legt die Netzwerkanmeldeinformationen als <see cref="ICredentials"/> fest, die an den Host gesendet und für die Authentifizierung der Anforderung verwendet wird.</param>
    ''' <param name="Response">Ein <see cref="String"/>, der die vom Server gesendete Antwort enthält.</param>
    ''' <param name="Headers">(Optional) Zusätzliche Header für den Download von Zeichenfolgen</param>
    ''' <param name="ZeichenCodierung">(Optional) Legt die <see cref="Encoding"/> für den Download von Zeichenfolgen fest.</param>
    ''' <returns>Boolean, je nach Erfolg der Abfrage.</returns>
    Friend Function UploadData(UniformResourceIdentifier As Uri, PostData As String, NC As NetworkCredential, ByRef Response As String, Optional Headers As WebHeaderCollection = Nothing, Optional ZeichenCodierung As Encoding = Nothing) As Boolean

        Response = String.Empty

        Using webClient As New WebClient With {.Credentials = NC,
                                               .Encoding = If(ZeichenCodierung, Encoding.GetEncoding(DfltCodePageFritzBox))}
            With webClient

                With .Headers
                    .Set(HttpRequestHeader.UserAgent, DefaultHeaderUserAgent)
                    .Set(HttpRequestHeader.KeepAlive, DefaultHeaderKeepAlive.ToString)
                    If Headers IsNot Nothing Then .Add(Headers)
                End With

                Try
                    Response = .UploadString(UniformResourceIdentifier, PostData)
                    Return True
                Catch ex As ArgumentException
                    ' Der address-Parameter ist null.
                    ' - oder -
                    ' Der Data - Parameter ist null.
                    PushStatus?.Invoke(CreateLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; Data: '{PostData}' ", ex))

                    ' Rückgabewert festlegen
                    Response = ex.Message
                Catch ex As WebException
                    ' Der durch Kombinieren von BaseAddress und address gebildete URI ist ungültig.
                    ' - oder -
                    ' Der Server, der Host dieser Ressource ist, hat nicht geantwortet.
                    PushStatus?.Invoke(CreateLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; Data: '{PostData}' ", ex))
                    ' Rückgabewert festlegen
                    Response = ex.Message
                End Try
            End With
        End Using

        Return False
    End Function
#End Region

#End Region

End Class
