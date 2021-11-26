Friend Interface IService
    Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable)
    Property PushStatus As Action(Of LogLevel, String)
    ReadOnly Property Servicefile As SCPDFiles
End Interface



