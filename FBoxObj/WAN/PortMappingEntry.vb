Public Class PortMappingEntry
    Public Property RemoteHost As String
    Public Property ExternalPort As Integer
    Public Property PortMappingProtocol As PortMappingProtocolEnum
    Public Property InternalPort As Integer
    Public Property InternalClient As String
    Public Property PortMappingEnabled As Boolean
    Public Property PortMappingDescription As String
    Public Property PortMappingLeaseDuration As Integer
End Class