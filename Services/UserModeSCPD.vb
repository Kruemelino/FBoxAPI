Public Class UserModeSCPD
    Implements IServiceBase

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IServiceBase.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IServiceBase.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IServiceBase.Servicefile
    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String))

        TR064Start = Start

        PushStatus = Status
    End Sub

    Public Function StartAction(ServiceFile As SCPDFiles, ActionName As String, InputParameter As Hashtable, ByRef OutputParameter As Hashtable) As Boolean

        OutputParameter = TR064Start(ServiceFile, ActionName, InputParameter)
        Return Not OutputParameter.ContainsKey("Error")

    End Function
End Class
