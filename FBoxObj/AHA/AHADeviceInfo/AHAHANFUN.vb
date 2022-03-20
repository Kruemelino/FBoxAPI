Imports System.Xml.Serialization

<Serializable(), XmlType("etsiunitinfo")> Public Class AHAHANFUN
    <XmlElement("etsideviceid")> Public Property ETSIDeviceID As String
    <XmlElement("unittype")> Public Property UnitType As HAN_FUN_UnitType
    <XmlElement("interfaces")> Public Property Interfaces As HAN_FUN_Interfaces
End Class
