Imports System.Xml.Serialization

<Serializable(), XmlType("telephony")> Public Class Telephony
    Implements IEquatable(Of Telephony)

    <XmlArray("services"), XmlArrayItem("email")> Public Property Emails As List(Of Email)
    <XmlElement("number")> Public Property Numbers As List(Of NumberType)
    <XmlElement("doorphone")> Public Property Doorphone As DoorPhone
    <XmlAttribute("nid")> Public Property ID As Integer

    Public Sub New()
        Emails = New List(Of Email)
        Numbers = New List(Of NumberType)
    End Sub

    Public Overloads Function Equals(other As Telephony) As Boolean Implements IEquatable(Of Telephony).Equals
        ' Vergleich auf ID wird übergangen
        With other

            Return _Emails.SequenceEqual(._Emails) AndAlso
                   _Numbers.SequenceEqual(._Numbers) AndAlso
                   (_Doorphone Is Nothing And ._Doorphone Is Nothing) OrElse
                   (_Doorphone IsNot Nothing And ._Doorphone IsNot Nothing AndAlso _Doorphone.Equals(._Doorphone))

        End With

    End Function
End Class
