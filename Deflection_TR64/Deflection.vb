Imports System.Xml.Serialization

<Serializable> Public Class Deflection
    <XmlElement("DeflectionId")> Public Property DeflectionId As Integer
    <XmlElement("Enable")> Public Property Enable As Boolean
    <XmlElement("Type")> Public Property Type As DeflectionType
    <XmlElement("Number")> Public Property Number As String
    <XmlElement("DeflectionToNumber")> Public Property DeflectionToNumber As String
    <XmlElement("Mode")> Public Property Mode As DeflectionMode
    <XmlElement("Outgoing")> Public Property Outgoing As String
    ''' <summary>
    ''' Only valid if Type==fromPB
    ''' </summary>
    <XmlElement("PhonebookID")> Public Property PhonebookID As String
End Class
