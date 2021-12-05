Imports System.Net
Imports System.Text
Friend Class HttpFunctions

    Private Const DefaultHeaderKeepAlive As Boolean = False
    Private Const DefaultHeaderUserAgent As String = "Mozilla/5.0 (Windows NT 10.0; WOW64; Trident/7.0; rv:11.0) like Gecko"

    Private Property PushStatus As Action(Of LogLevel, Exception, String)

    Public Sub New(Status As Action(Of LogLevel, Exception, String))
        PushStatus = Status
    End Sub

#Region "Netzwerkfunktionen"
    ''' <summary>
    ''' Führt einen Ping zur Gegenstelle aus.
    ''' </summary>
    ''' <param name="IPAdresse">IP-Adresse Netzwerkname der Gegenstelle. Rückgabe der IP-Adresse</param>
    ''' <returns>Boolean</returns>
    Friend Function Ping(ByRef IPAdresse As String) As Boolean
        Ping = False

        Dim IPHostInfo As IPHostEntry
        Dim PingSender As New NetworkInformation.Ping()
        Dim Options As New NetworkInformation.PingOptions() With {.DontFragment = True}
        Dim PingReply As NetworkInformation.PingReply = Nothing

        Dim buffer As Byte() = Encoding.ASCII.GetBytes(String.Empty)
        Dim timeout As Integer = 120

        Try
            PingReply = PingSender.Send(IPAdresse, timeout, buffer, Options)

        Catch ex As Exception

            PushStatus.Invoke(LogLevel.Warn, ex, $"Ping zu {IPAdresse} nicht erfolgreich")
            Ping = False
        End Try

        If PingReply IsNot Nothing Then
            With PingReply
                If .Status = NetworkInformation.IPStatus.Success Then
                    If .Address.AddressFamily = Sockets.AddressFamily.InterNetworkV6 Then
                        'Zugehörige IPv4 ermitteln
                        IPHostInfo = Dns.GetHostEntry(.Address)
                        For Each _IPAddress As IPAddress In IPHostInfo.AddressList
                            If _IPAddress.AddressFamily = Sockets.AddressFamily.InterNetwork Then
                                IPAdresse = _IPAddress.ToString
                                ' Prüfen ob es eine generel gültige lokale IPv6 Adresse gibt: fd00::2665:11ff:fed8:6086
                                ' und wie die zu ermitteln ist
                                PushStatus.Invoke(LogLevel.Debug, Nothing, $"IPv6: { .Address}, IPv4: {IPAdresse}")
                                Exit For
                            End If
                        Next
                    Else
                        IPAdresse = .Address.ToString
                    End If
                    Ping = True
                Else
                    PushStatus.Invoke(LogLevel.Warn, Nothing, $"Ping zu '{IPAdresse}' nicht erfolgreich: { .Status}")
                    Ping = False
                End If
            End With
        End If
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
                                                       .Encoding = If(ZeichenCodierung, Encoding.GetEncoding(FritzBoxInformations.DfltCodePageFritzBox))}
                    With webClient

                        With .Headers
                            .Set(HttpRequestHeader.UserAgent, DefaultHeaderUserAgent)
                            .Set(HttpRequestHeader.KeepAlive, DefaultHeaderKeepAlive.ToString)

                            If Headers IsNot Nothing Then .Add(Headers)
                        End With

                        Try
                            Response = .DownloadString(UniformResourceIdentifier)

                            Return True

                        Catch ex As ArgumentNullException
                            ' Der address-Parameter ist null.
                            PushStatus.Invoke(LogLevel.Error, ex, "Der address-Parameter ist null.")

                        Catch ex As WebException
                            ' Der durch Kombinieren von BaseAddress und address gebildete URI ist ungültig.
                            ' - oder -
                            ' Fehler beim Herunterladen der Ressource.
                            PushStatus.Invoke(LogLevel.Error, ex, $"Link: {UniformResourceIdentifier.AbsoluteUri} ")

                        Catch ex As NotSupportedException
                            ' Die Methode wurde gleichzeitig für mehrere Threads aufgerufen.
                            PushStatus.Invoke(LogLevel.Error, ex, "Die Methode wurde gleichzeitig für mehrere Threads aufgerufen.")

                        End Try
                    End With
                End Using
            Case Else
                'NLogger.Warn($"Uri.Scheme: {UniformResourceIdentifier.Scheme}")

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
                    PushStatus.Invoke(LogLevel.Error, ex, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; Data: '{PostData}' ")

                Catch ex As WebException
                    ' Der durch Kombinieren von BaseAddress und address gebildete URI ist ungültig.
                    ' - oder -
                    ' Der Server, der Host dieser Ressource ist, hat nicht geantwortet.
                    PushStatus.Invoke(LogLevel.Error, ex, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; Data: '{PostData}' ")

                End Try
            End With
        End Using

        Return False
    End Function
#End Region

#End Region

End Class
