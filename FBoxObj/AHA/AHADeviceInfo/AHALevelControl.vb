Imports System.Xml.Serialization
<Serializable(), XmlType("levelcontrol")> Public Class AHALevelControl
    ''' <summary>
    ''' Level/Niveau von 0(0%) bis 255(100%)
    ''' </summary>
    <XmlElement("level")> Public Property Level As String

    ''' <summary>
    ''' Level/Niveau in Prozent, 0 bis 100 Prozent
    ''' </summary>
    <XmlElement("levelpercentage")> Public Property LevelPercentage As String
End Class
