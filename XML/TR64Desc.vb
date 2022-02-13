Imports System.Xml.Serialization

<Serializable()>
<XmlRoot("root", IsNullable:=False)> Public Class TR64Desc
    <XmlElement("specVersion")> Public Property [SpecVersion] As SpecVersion
    <XmlElement("systemVersion")> Public Property [SystemVersion] As SystemVersion
    <XmlElement("device")> Public Property [Device] As Device
End Class