Imports System.Xml.Serialization
<Serializable(), XmlType("Message")> Public Class Message

    ''' <summary>
    ''' Number of called party 
    ''' </summary>
    <XmlElement("Called", GetType(String))> Public Property ID As String
    ''' <summary>
    ''' 31.07.12 12:03
    ''' </summary>
    <XmlElement("Date", GetType(String))> Public Property [Date] As String
    ''' <summary>
    ''' hh:mm (minutes rounded up)
    ''' </summary>
    <XmlElement("Duration", GetType(String))> Public Property Duration As String
    ''' <summary>
    ''' 0 not in a phone book,
    ''' 1 stored in a phone book
    ''' </summary>
    <XmlElement("Inbook", GetType(Boolean))> Public Property Inbook As Boolean
    ''' <summary>
    ''' Message index (ID), smallest value is 0. It grows with the number of messages.
    ''' Deleting a message don´t change the index of other messages, so the index is not a continuous counter.
    ''' </summary>
    <XmlElement("Index", GetType(Integer))> Public Property Index As Integer
    ''' <summary>
    ''' Name of Called number 
    ''' </summary>
    <XmlElement("Name", GetType(String))> Public Property Name As String
    ''' <summary>
    ''' 0 message is new,
    ''' 1 message has been marked 
    ''' </summary>
    <XmlElement("New", GetType(Boolean))> Public Property [New] As Boolean
    ''' <summary>
    ''' Own number
    ''' </summary>
    <XmlElement("Number", GetType(String))> Public Property Number As String
    ''' <summary>
    ''' URL path to TAM file
    ''' </summary>
    <XmlElement("Path", GetType(String))> Public Property Path As String
    ''' <summary>
    ''' TAM index
    ''' </summary>
    <XmlElement("Tam", GetType(Integer))> Public Property Tam As Integer

End Class

