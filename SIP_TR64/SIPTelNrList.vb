Imports System.Xml.Serialization

<Serializable()>
    <XmlRoot("List")> Public Class SIPTelNrList

    <XmlElement("Item")> Public Property TelNrList As List(Of SIPTelNr)

    Private _ToXMLString As String
    <XmlIgnore> Friend ReadOnly Property ToXMLString As String
        Get
            If XmlSerializeToString(Me, _ToXMLString) Then
                Return _ToXMLString
            Else
                Return ""
            End If
        End Get
    End Property

    Public Sub New()
        TelNrList = New List(Of SIPTelNr)
    End Sub

End Class