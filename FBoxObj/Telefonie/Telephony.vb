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

            Return Emails.SequenceEqual(.Emails) AndAlso
                   Numbers.SequenceEqual(.Numbers) AndAlso
                   (Doorphone Is Nothing And .Doorphone Is Nothing) OrElse
                   (Doorphone IsNot Nothing And .Doorphone IsNot Nothing AndAlso Doorphone.Equals(.Doorphone))

        End With

    End Function
End Class
