''' <summary>
''' TR-064 Support – AURA (AVM USB Remote Access)
''' Date: 2020-02-26 by Black Senator<br/>
''' <see href="link">https://github.com/blacksenator/fritzsoap/blob/master/docs/auraSCPD.pdf</see>
''' </summary>
''' <remarks>Please note: You must activate USB remote access in FRITZ!Box settings to get a access to this service!</remarks>
Public Interface IAuraSCPD
    Inherits IServiceBase

    Function GetVersion(ByRef ServerVersion As String, ByRef ProtocolVersion As String) As Boolean

    Function GetListInfo(ByRef Number As Integer) As Boolean

    Function GetDeviceByIndex(Index As Integer, ByRef Info As AuraInfo) As Boolean

    Function GetDeviceByHandle(DeviceHandle As Integer, ByRef Info As AuraInfo) As Boolean

    Function ConnectDevice(DeviceHandle As Integer) As Boolean

    Function DisconnectDevice(DeviceHandle As Integer) As Boolean
End Interface
