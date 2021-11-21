Friend Interface IService
    Property TR064Start As Func(Of String, String, Hashtable, Hashtable)
    Property PushStatus As Action(Of LogLevel, String)
End Interface



