Imports System.Xml.Serialization
<Serializable(), XmlType("contact")> Public Class Contact
    Implements IEquatable(Of Contact)

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

    <XmlElement("features")> Public Property Features As Features

    <XmlElement("mod_time")> Public Property Mod_Time As String

    Public Overloads Function Equals(other As Contact) As Boolean Implements IEquatable(Of Contact).Equals
        ' Vergleich auf Mod_Time und Uniqueid wird übergangen
        With other
            Return _Category.AreEqual(._Category) AndAlso
                   _Person.Equals(._Person) AndAlso
                   _Telephony.Equals(._Telephony) AndAlso
                   (_Features Is Nothing And ._Features Is Nothing) OrElse
                   (_Features IsNot Nothing And ._Features IsNot Nothing AndAlso _Features.Equals(._Features))
        End With
    End Function
End Class
