Imports System.Reflection
Imports System.Runtime.CompilerServices

<DebuggerStepThrough>
Public MustInherit Class LogBase

    Protected Shared Sub SendLog(l As LogLevel,
                          m As String,
                          e As Exception,
                          <CallerMemberName> Optional propertyName As String = Nothing,
                          <CallerFilePath> Optional sourcefilePath As String = Nothing,
                          <CallerLineNumber> Optional sourceLineNumber As Integer = 0)

        FritzBoxTR64._LogWriter?.LogMessage(New LogMessage With {.Level = l,
                                                                 .Message = m,
                                                                 .Ex = e,
                                                                 .CallerMemberName = propertyName,
                                                                 .CallerFilePath = sourcefilePath,
                                                                 .CallerClassName = NameOfCallingClass,
                                                                 .CallerLineNumber = sourceLineNumber})
    End Sub

    Protected Shared Sub SendLog(l As LogLevel,
                          m As String,
                          <CallerMemberName> Optional propertyName As String = Nothing,
                          <CallerFilePath> Optional sourcefilePath As String = Nothing,
                          <CallerLineNumber> Optional sourceLineNumber As Integer = 0)

        FritzBoxTR64._LogWriter?.LogMessage(New LogMessage With {.Level = l,
                                                                 .Message = m,
                                                                 .CallerMemberName = propertyName,
                                                                 .CallerFilePath = sourcefilePath,
                                                                 .CallerClassName = NameOfCallingClass,
                                                                 .CallerLineNumber = sourceLineNumber})
    End Sub

    Protected Shared Sub SendLog(l As LogLevel,
                          e As Exception,
                          <CallerMemberName> Optional propertyName As String = Nothing,
                          <CallerFilePath> Optional sourcefilePath As String = Nothing,
                          <CallerLineNumber> Optional sourceLineNumber As Integer = 0)

        FritzBoxTR64._LogWriter?.LogMessage(New LogMessage With {.Level = l,
                                                                 .Ex = e,
                                                                 .CallerFilePath = sourcefilePath,
                                                                 .CallerMemberName = propertyName,
                                                                 .CallerClassName = NameOfCallingClass,
                                                                 .CallerLineNumber = sourceLineNumber})
    End Sub

    ''' <remarks><see href="https://stackoverflow.com/a/48570616"/></remarks>
    Private Shared ReadOnly Property NameOfCallingClass As String
        Get
            Dim fullName As String
            Dim declaringType As Type
            Dim skipFrames As Integer = 2

            Do
                Dim method As MethodBase = New StackFrame(skipFrames, False).GetMethod()
                declaringType = method.DeclaringType

                If declaringType Is Nothing Then Return method.Name

                skipFrames += 1
                fullName = declaringType.FullName
            Loop While declaringType.[Module].Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase)

            Return fullName
        End Get
    End Property
End Class

<DebuggerStepThrough>
Public Class LogMessage
    Public Property Level As LogLevel
    Public Property Message As String
    Public Property Ex As Exception
    Public Property CallerMemberName As String
    Public Property CallerFilePath As String
    Public Property CallerClassName As String
    Public Property CallerLineNumber As Integer
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
    Warning = 3

    ''' <summary>
    ''' Something failed; application may or may not continue
    ''' </summary>
    [Error] = 4

    ''' <summary>
    ''' Something bad happened; application is going down
    ''' </summary>
    Fatal = 5
End Enum
