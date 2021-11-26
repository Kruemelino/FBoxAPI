Public Class LogMessage
    Public Sub New(l As LogLevel, m As String)
        Level = l
        Message = m
    End Sub

    Public Property Level As LogLevel
    Public Property Message As String
End Class

''' <summary>
''' Korrespondiert mit NLOG
''' </summary>
Public Enum LogLevel
    ''' <summary>
    ''' For trace debugging; begin method X, end method X
    ''' </summary>
    Trace = 0

    ''' <summary>
    ''' For debugging; executed query, user authenticated, session expired
    ''' </summary>
    Debug = 1

    ''' <summary>
    ''' Normal behavior like mail sent, user updated profile etc.
    ''' </summary>
    Info = 2

    ''' <summary>
    ''' Something unexpected; application will continue
    ''' </summary>
    Warn = 3

    ''' <summary>
    ''' Something failed; application may or may not continue
    ''' </summary>
    [Error] = 4

    ''' <summary>
    ''' Something bad happened; application is going down
    ''' </summary>
    Fatal = 5
End Enum
