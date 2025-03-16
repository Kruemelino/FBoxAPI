Imports System.Net
Imports System.Text

<DebuggerStepThrough>
Friend Class WebFunctions
    Inherits APIConnectorBase
    Implements IDisposable

    Private disposedValue As Boolean
    Private Property NetworkCredentials As NetworkCredential

    Public Sub New(credentials As NetworkCredential)

        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12


        _NetworkCredentials = credentials

    End Sub

    Friend Sub UpdateCredential(Anmeldeinformationen As NetworkCredential)
        ' Netzwerkanmeldeinformationen zuweisen
        NetworkCredentials = Anmeldeinformationen
    End Sub

    Private Function CreateWebClient(UniformResourceIdentifier As Uri,
                                     Optional UserAgent As String = "AVM UPnP/1.0 Client 1.0",
                                     Optional ContentType As String = "text/xml;charset=UTF-8") As WebClient

#Disable Warning SYSLIB0014 ' Typ oder Element ist veraltet
        Dim Client As New WebClient With {.CachePolicy = New Cache.HttpRequestCachePolicy(Cache.HttpRequestCacheLevel.BypassCache),
                                          .Encoding = Encoding.UTF8}
#Enable Warning SYSLIB0014 ' Typ oder Element ist veraltet

        With Client.Headers
            .Set(HttpRequestHeader.UserAgent, UserAgent)
            .Set(HttpRequestHeader.KeepAlive, "false")
            .Set(HttpRequestHeader.ContentType, ContentType)
        End With


        If UniformResourceIdentifier.Scheme = Uri.UriSchemeHttps Then
            ' ByPass SSL Certificate Validation Checking
            ServicePointManager.ServerCertificateValidationCallback = Function(se As Object,
                                                                               cert As System.Security.Cryptography.X509Certificates.X509Certificate,
                                                                               chain As System.Security.Cryptography.X509Certificates.X509Chain,
                                                                               sslerror As Security.SslPolicyErrors) True
        End If

        Return Client
    End Function

#Region "GET"
#Region "String für TR-064"
    Friend Async Function GetStringWebClientAsync(Path As String) As Task(Of String)
        If Path.IsNotStringNothingOrEmpty Then
            Return Await GetStringWebClientAsync(New Uri(Path))
        Else
            Return Nothing
        End If
    End Function

    Friend Async Function GetStringWebClientAsync(UniformResourceIdentifier As Uri) As Task(Of String)
        Dim ReturnString As String = String.Empty

        Using Client As WebClient = CreateWebClient(UniformResourceIdentifier)

            Try

                ReturnString = Await Client.DownloadStringTaskAsync(UniformResourceIdentifier)

                SendLog(LogLevel.Trace, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; Response:{vbCrLf}{ReturnString}")

            Catch ex As ArgumentNullException
                SendLog(LogLevel.Error, ex)

            Catch ex As WebException
                ' Gib die Antwort des Servers zurück
                With CType(ex.Response, HttpWebResponse)
                    If ex.Status = WebExceptionStatus.ProtocolError AndAlso .StatusCode = HttpStatusCode.InternalServerError Then
                        Using DataStream As IO.Stream = .GetResponseStream()
                            Using reader = New IO.StreamReader(DataStream)
                                SendLog(LogLevel.Warning, $"SOAPFault: ' {UniformResourceIdentifier.AbsoluteUri} '; Response: {reader.ReadToEnd()} ")
                            End Using
                        End Using

                        .Close()
                    Else
                        SendLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; StatusCode: {ex.Message} ")
                    End If
                End With

            Finally
                ' Restore SSL Certificate Validation Checking
                ServicePointManager.ServerCertificateValidationCallback = Nothing

                ' Releases the resources used by the WebClient.
                Client.Dispose()
            End Try

        End Using

        Return ReturnString
    End Function

    Friend Async Function GetStreamWebClientAsync(UniformResourceIdentifier As Uri) As Task(Of IO.Stream)
        Dim ReturnStream As IO.Stream = Nothing

        Using Client As WebClient = CreateWebClient(UniformResourceIdentifier)

            Try
                SendLog(LogLevel.Debug, $"{UniformResourceIdentifier.AbsoluteUri} ")
                ReturnStream = Await Client.OpenReadTaskAsync(UniformResourceIdentifier)

            Catch ex As ArgumentNullException
                SendLog(LogLevel.Error, ex)

            Catch ex As WebException
                ' Gib die Antwort des Servers zurück
                With CType(ex.Response, HttpWebResponse)
                    If ex.Status = WebExceptionStatus.ProtocolError AndAlso .StatusCode = HttpStatusCode.InternalServerError Then
                        Using DataStream As IO.Stream = .GetResponseStream()
                            Using reader = New IO.StreamReader(DataStream)
                                SendLog(LogLevel.Warning, $"SOAPFault: ' {UniformResourceIdentifier.AbsoluteUri} '; Response: {reader.ReadToEnd()} ")
                            End Using
                        End Using

                        .Close()
                    Else
                        SendLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; StatusCode: {ex.Message} ")
                    End If
                End With

            Finally
                ' Restore SSL Certificate Validation Checking
                ServicePointManager.ServerCertificateValidationCallback = Nothing

                ' Releases the resources used by the WebClient.
                Client.Dispose()
            End Try

        End Using

        Return ReturnStream
    End Function
#End Region

#Region "Data - Byte()"
    Friend Async Function GetDataWebClientAsync(UniformResourceIdentifier As Uri) As Task(Of Byte())
        Dim ReturnByte As Byte() = Array.Empty(Of Byte)

        Using Client As WebClient = CreateWebClient(UniformResourceIdentifier)

            Try
                ReturnByte = Await Client.DownloadDataTaskAsync(UniformResourceIdentifier)
            Catch ex As ArgumentNullException
                SendLog(LogLevel.Error, ex)

            Catch ex As WebException
                ' Gib die Antwort des Servers zurück
                With CType(ex.Response, HttpWebResponse)
                    If ex.Status = WebExceptionStatus.ProtocolError AndAlso .StatusCode = HttpStatusCode.InternalServerError Then
                        Using DataStream As IO.Stream = .GetResponseStream()
                            Using reader = New IO.StreamReader(DataStream)
                                SendLog(LogLevel.Warning, $"SOAPFault: ' {UniformResourceIdentifier.AbsoluteUri} '; Response:{vbCrLf}{reader.ReadToEnd()}")
                            End Using
                        End Using

                        .Close()
                    Else
                        SendLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; StatusCode: {ex.Message} ")
                    End If

                End With

            Finally
                ' Restore SSL Certificate Validation Checking
                ServicePointManager.ServerCertificateValidationCallback = Nothing

                ' Releases the resources used by the WebClient.
                Client.Dispose()
            End Try

        End Using

        Return ReturnByte
    End Function
#End Region

#Region "File"
    Friend Async Function GetFileWebClientAsync(UniformResourceIdentifier As Uri, DateiPfad As String) As Task(Of Boolean)
        Dim ReturnBoolean As Boolean = False

        Using Client As WebClient = CreateWebClient(UniformResourceIdentifier)

            Try
                Await Client.DownloadFileTaskAsync(UniformResourceIdentifier, DateiPfad)

                SendLog(LogLevel.Debug, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; Dateipfad: {DateiPfad} ")

                ReturnBoolean = True
            Catch ex As ArgumentNullException
                SendLog(LogLevel.Error, ex)

            Catch ex As WebException
                ' Gib die Antwort des Servers zurück
                With CType(ex.Response, HttpWebResponse)
                    If ex.Status = WebExceptionStatus.ProtocolError AndAlso .StatusCode = HttpStatusCode.InternalServerError Then
                        Using DataStream As IO.Stream = .GetResponseStream()
                            Using reader = New IO.StreamReader(DataStream)
                                SendLog(LogLevel.Warning, $"SOAPFault: ' {UniformResourceIdentifier.AbsoluteUri} '; Response:{vbCrLf}{reader.ReadToEnd()}")
                            End Using
                        End Using
                        .Close()
                    Else
                        SendLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; StatusCode: {ex.Message} ")
                    End If

                End With

            Finally
                ' Restore SSL Certificate Validation Checking
                ServicePointManager.ServerCertificateValidationCallback = Nothing

                ' Releases the resources used by the WebClient.
                Client.Dispose()
            End Try

        End Using

        Return ReturnBoolean
    End Function
#End Region

#End Region

#Region "POST"
    Friend Function PostStringWebClient(Path As String, PostData As String, SOAPActionHeader As String) As String
        Dim T As Task(Of String) = Task.Run(Function() PostStringWebClientAsyncTR064(New Uri(Path), PostData, SOAPActionHeader))
        T.Wait()
        Return T.Result
    End Function

    Private Async Function PostBase(Client As WebClient, UniformResourceIdentifier As Uri, PostData As String) As Task(Of String)
        Dim ReturnString As String = String.Empty

        With Client
            Try
                ReturnString = Await .UploadStringTaskAsync(UniformResourceIdentifier, PostData)

                SendLog(LogLevel.Debug, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; RequestContent: {vbCrLf}{PostData}{vbCrLf}Response: {vbCrLf}{ReturnString}")
            Catch ex As WebException
                With CType(ex.Response, HttpWebResponse)
                    If ex.Status = WebExceptionStatus.ProtocolError AndAlso .StatusCode = HttpStatusCode.InternalServerError Then
                        Using DataStream As IO.Stream = .GetResponseStream()
                            Using reader = New IO.StreamReader(DataStream)
                                ' Ermittle die übermittelte Fehlermeldung
                                ReturnString = reader.ReadToEnd()

                                SendLog(LogLevel.Warning, $"SOAPFault: ' {UniformResourceIdentifier.AbsoluteUri} '; RequestContent: {vbCrLf}{PostData}{vbCrLf}Response:{vbCrLf}{ReturnString}")
                            End Using
                        End Using

                        ' Prüfen: Potentieller Fehler beim Debuggen: ServerCertificateValidationCallback bereits auf Nothing gesetzt.
                        .Close()
                    Else
                        SendLog(LogLevel.Error, $"URI: ' {UniformResourceIdentifier.AbsoluteUri} '; StatusCode: {ex.Message}; RequestContent: {vbCrLf}{PostData} ")
                    End If
                End With
            Finally
                ' Restore SSL Certificate Validation Checking
                ServicePointManager.ServerCertificateValidationCallback = Nothing

                ' Releases the resources used by the WebClient.
                .Dispose()
            End Try
        End With

        Return ReturnString
    End Function

    Private Async Function PostStringWebClientAsyncTR064(UniformResourceIdentifier As Uri, PostData As String, SOAPActionHeader As String) As Task(Of String)
        Dim ReturnString As String = String.Empty

        Using Client As WebClient = CreateWebClient(UniformResourceIdentifier)
            With Client
                .Credentials = NetworkCredentials
                .Headers.Set("SOAPACTION", SOAPActionHeader)
            End With

            SendLog(LogLevel.Trace, $"SOAPActionHeader: SOAPACTION={SOAPActionHeader}")

            ReturnString = Await PostBase(Client, UniformResourceIdentifier, PostData)

        End Using

        Return ReturnString
    End Function

    Friend Async Function PostStringWebClientAsyncLua(UniformResourceIdentifier As Uri, PostData As String) As Task(Of String)
        Dim ReturnString As String = String.Empty

        Using Client As WebClient = CreateWebClient(UniformResourceIdentifier,
                                                    "application/x-www-form-urlencoded",
                                                    "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.75 Safari/537.36")

            ReturnString = Await PostBase(Client, UniformResourceIdentifier, PostData)
        End Using

        Return ReturnString
    End Function
#End Region

#Region "WebResponse - Legacy"
    'Private Function CreateTR064GetWebRequest(UniformResourceIdentifier As Uri) As HttpWebRequest
    '    Dim Request As HttpWebRequest = CType(WebRequest.Create(UniformResourceIdentifier), HttpWebRequest)

    '    With Request
    '        .UserAgent = "AVM UPnP/1.0 Client 1.0"
    '        .Method = "GET"

    '        If UniformResourceIdentifier.Scheme = Uri.UriSchemeHttps Then
    '            ' ByPass SSL Certificate Validation Checking
    '            .ServerCertificateValidationCallback = Function(se As Object,
    '                                                            cert As System.Security.Cryptography.X509Certificates.X509Certificate,
    '                                                            chain As System.Security.Cryptography.X509Certificates.X509Chain,
    '                                                            sslerror As Security.SslPolicyErrors) True
    '        End If
    '    End With

    '    Return Request
    'End Function

    'Private Function CreateTR064PostWebRequest(UniformResourceIdentifier As Uri) As HttpWebRequest
    '    Dim Request As HttpWebRequest = CType(WebRequest.Create(UniformResourceIdentifier), HttpWebRequest)

    '    With Request
    '        .UserAgent = "AVM UPnP/1.0 Client 1.0"
    '        .Method = "POST"
    '        .ContentType = "text/xml"

    '        ' ByPass SSL Certificate Validation Checking
    '        .ServerCertificateValidationCallback = Function(se As Object,
    '                                                        cert As System.Security.Cryptography.X509Certificates.X509Certificate,
    '                                                        chain As System.Security.Cryptography.X509Certificates.X509Chain,
    '                                                        sslerror As Security.SslPolicyErrors) True
    '    End With

    '    Return Request
    'End Function

#Region "GET"
    'Friend Async Function GetStringWebRequestAsync(Path As String) As Task(Of String)
    '    Dim Resonse As String = String.Empty

    '    With CreateTR064GetWebRequest(New Uri(Path))

    '        Try
    '            With Await .GetResponseAsync()
    '                Using DataStream As IO.Stream = .GetResponseStream()
    '                    Using reader = New IO.StreamReader(DataStream, Encoding.UTF8)
    '                        Resonse = reader.ReadToEnd()

    '                        PushStatus?.Invoke(CreateLog(LogLevel.Trace, $"Get: ' {Path} '; Resonse: {Resonse} "))
    '                    End Using
    '                    DataStream.Close()
    '                    DataStream.Dispose()
    '                End Using
    '                .Close()
    '                .Dispose()
    '            End With

    '        Catch ex As Exception
    '            PushStatus?.Invoke(CreateLog(LogLevel.Error, Path, ex))
    '        End Try

    '    End With
    '    Return Resonse
    'End Function

    'Friend Async Function GetDataWebRequestAsync(UniformResourceIdentifier As Uri) As Task(Of Byte())

    '    Dim ReturnByte As Byte() = Array.Empty(Of Byte)

    '    With CreateTR064GetWebRequest(UniformResourceIdentifier)

    '        Try
    '            With Await .GetResponseAsync()
    '                Using DataStream As New IO.MemoryStream
    '                    Await .GetResponseStream.CopyToAsync(DataStream)
    '                    ReturnByte = DataStream.ToArray

    '                    DataStream.Close()
    '                    DataStream.Dispose()
    '                End Using
    '                .Close()
    '                .Dispose()
    '            End With

    '        Catch ex As Exception
    '            PushStatus?.Invoke(CreateLog(LogLevel.Error, UniformResourceIdentifier.AbsoluteUri, ex))
    '        End Try

    '    End With
    '    Return ReturnByte
    'End Function

    'Friend Async Function GetFileWebRequestAsync(UniformResourceIdentifier As Uri, DateiPfad As String) As Task(Of Boolean)

    '    With CreateTR064GetWebRequest(UniformResourceIdentifier)

    '        Try
    '            With Await .GetResponseAsync()
    '                Using DataStream As IO.FileStream = IO.File.Create(DateiPfad)
    '                    Await .GetResponseStream.CopyToAsync(DataStream)
    '                    DataStream.Close()
    '                    DataStream.Dispose()
    '                End Using
    '                .Close()
    '                .Dispose()
    '            End With

    '            If IO.File.Exists(DateiPfad) Then
    '                PushStatus?.Invoke(CreateLog(LogLevel.Debug, $"Daten von {UniformResourceIdentifier.AbsoluteUri} erfolgreich in Datei {DateiPfad} gespeichert."))
    '                Return True
    '            Else
    '                PushStatus?.Invoke(CreateLog(LogLevel.Warn, $"Daten von {UniformResourceIdentifier.AbsoluteUri} nicht in Datei {DateiPfad} gespeichert."))
    '                Return False
    '            End If

    '        Catch ex As Exception
    '            PushStatus?.Invoke(CreateLog(LogLevel.Error, UniformResourceIdentifier.AbsoluteUri, ex))
    '            Return False
    '        End Try

    '    End With
    'End Function
#End Region
#Region "POST"
    'Friend Function PostStringWebRequest(Path As String, PostData As String, SOAPActionHeader As String) As String
    '    Dim ReturnString As String = String.Empty
    '    Dim Request As HttpWebRequest = CreateTR064PostWebRequest(New Uri(Path))

    '    With Request
    '        .Headers.Set("SOAPACTION", SOAPActionHeader)
    '        .Credentials = NetworkCredentials

    '        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(PostData)
    '        .ContentLength = byteArray.Length

    '        Using DataStream As IO.Stream = .GetRequestStream()
    '            With DataStream
    '                .Write(byteArray, 0, byteArray.Length)
    '                .Close()
    '                .Dispose()
    '            End With
    '        End Using

    '        Dim Response As HttpWebResponse = Nothing

    '        Try
    '            Response = CType(.GetResponse, HttpWebResponse)

    '            Using DataStream As IO.Stream = Response.GetResponseStream()
    '                Using reader = New IO.StreamReader(DataStream)
    '                    ReturnString = reader.ReadToEnd()

    '                    PushStatus?.Invoke(CreateLog(LogLevel.Trace, $"POST: ' {Path} '; RequestContent: '{PostData}'; Resonse: {ReturnString} "))
    '                End Using
    '                DataStream.Close()
    '                DataStream.Dispose()
    '            End Using

    '        Catch ex As WebException
    '            Response = CType(ex.Response, HttpWebResponse)

    '            If ex.Status = WebExceptionStatus.ProtocolError AndAlso Response.StatusCode = HttpStatusCode.InternalServerError Then
    '                Using DataStream As IO.Stream = Response.GetResponseStream()
    '                    Using reader = New IO.StreamReader(DataStream)
    '                        PushStatus?.Invoke(CreateLog(LogLevel.Error, $"SOAPFault: ' {Path} '; RequestContent: '{PostData}'; Response: {reader.ReadToEnd()} "))
    '                    End Using
    '                End Using
    '            Else
    '                PushStatus?.Invoke(CreateLog(LogLevel.Error, $"URI: ' {Path} '; RequestContent: '{PostData}'; StatusCode: {ex.Status} "))
    '            End If

    '        Finally
    '            Response?.Close()
    '        End Try

    '    End With
    '    Return ReturnString
    'End Function
#End Region
#End Region

#Disable Warning CA1822 ' Member als statisch markieren
    Friend Function Ping(IPAdresse As String) As Boolean
        Dim PingSender As New NetworkInformation.Ping()
        Dim Options As New NetworkInformation.PingOptions() With {.DontFragment = True}
        Dim PingReply As NetworkInformation.PingReply = Nothing

        Dim buffer As Byte() = Encoding.ASCII.GetBytes(String.Empty)

        Try
            PingReply = PingSender.Send(IPAdresse, 100, buffer, Options)

        Catch ex As Exception
            SendLog(LogLevel.Warning, $"Ping zu {IPAdresse} nicht erfolgreich (timeout: {100}).", ex)
        End Try

        Return PingReply IsNot Nothing AndAlso PingReply.Status = NetworkInformation.IPStatus.Success

        PingSender.Dispose()
    End Function
#Enable Warning CA1822 ' Member als statisch markieren

#Region "IDisposable Support"
    Private Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then : End If

            disposedValue = True
        End If
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in der Methode "Dispose(disposing As Boolean)" ein.
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
