Public Class UserModeSCPD
    Implements IServiceBase

    Public ReadOnly Property DocumentationDate As Date = New Date(2023, 1, 22) Implements IServiceBase.DocumentationDate

    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IServiceBase.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IServiceBase.Servicefile
    Private ReadOnly Property ServiceID As Integer Implements IServiceBase.ServiceID

    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function StartAction(ServiceFile As SCPDFiles, ActionName As String, ServiceID As Integer, InputParameter As Dictionary(Of String, String), ByRef OutputParameter As Dictionary(Of String, String)) As Boolean

        OutputParameter = TR064Start(ServiceFile, ActionName, ServiceID, InputParameter)
        Return Not OutputParameter.ContainsKey("Error")

    End Function
End Class
