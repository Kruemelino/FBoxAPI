Public Interface IServiceBase
    Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String))
    ReadOnly Property Servicefile As SCPDFiles
    ReadOnly Property DocumentationDate As Date
    ReadOnly Property ServiceID As Integer

End Interface



