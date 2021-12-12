Imports System.Xml.Serialization
<Serializable(), XmlType("contact")> Public Class Contact

    ''' <summary>
    ''' Wichtige Person = 1, Optional, VIP == 1
    ''' </summary>
    <XmlElement("category", GetType(Integer))> Public Property Category As Integer

    <XmlElement("person")> Public Property Person As Person

    ''' <summary>
    ''' Unique ID for a single contact (new since 2013-04-20) 
    ''' </summary> 
    <XmlElement("uniqueid", GetType(Integer))> Public Property Uniqueid As Integer

    <XmlElement("telephony")> Public Property Telephony As Telephony

    <XmlElement("mod_time")> Public Property Mod_Time As String

End Class
