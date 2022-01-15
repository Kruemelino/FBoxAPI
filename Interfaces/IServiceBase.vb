Public Interface IServiceBase
    Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String))
    ReadOnly Property Servicefile As SCPDFiles
End Interface



