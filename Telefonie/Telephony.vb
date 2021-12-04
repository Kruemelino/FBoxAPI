﻿Imports System.Xml.Serialization

<Serializable(), XmlType("telephony")> Public Class Telephony

    <XmlArray("services"), XmlArrayItem("email")> Public Property Emails As List(Of Email)

    <XmlElement("number")> Public Property Numbers As List(Of Number)

End Class