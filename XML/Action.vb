﻿Imports System.Xml.Serialization
<Serializable()>
Public Class Action
    <XmlElement("name")> Public Name As String
    <XmlArray("argumentList")> <XmlArrayItem("argument")> Public Property ArgumentList As List(Of Argument)
End Class