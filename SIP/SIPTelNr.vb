Imports System.Xml.Serialization

<Serializable()> Public Class SIPTelNr
	<XmlElement("Number")> Public Property Number As String
	<XmlElement("Type")> Public Property Type As SIPType
	<XmlElement("Index")> Public Property Index As String
	<XmlElement("Name")> Public Property Name As String

End Class