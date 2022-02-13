﻿''' <summary>
''' TR-064 Support – AURA (AVM USB Remote Access)
''' Date: 2020-02-26 by Black Senator<br/>
''' <see href="link">https://github.com/blacksenator/fritzsoap/blob/master/docs/auraSCPD.pdf</see>
''' </summary>
''' <remarks>Please note: You must activate USB remote access in FRITZ!Box settings to get a access to this service!</remarks>
Friend Class AuraSCPD
    Implements IAuraSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IAuraSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.auraSCPD Implements IAuraSCPD.Servicefile

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)))

        TR064Start = Start

    End Sub

    Public Function GetVersion(ByRef ServerVersion As String, ByRef ProtocolVersion As String) As Boolean Implements IAuraSCPD.GetVersion
        With TR064Start(ServiceFile, "GetVersion", Nothing)

            Return .TryGetValueEx("NewServerVersion", ServerVersion) And
                   .TryGetValueEx("NewProtocolVersion", ProtocolVersion)

        End With
    End Function

    Public Function GetListInfo(ByRef Number As Integer) As Boolean Implements IAuraSCPD.GetListInfo
        Return TR064Start(ServiceFile, "GetListInfo", Nothing).TryGetValueEx("NewNumber", Number)
    End Function

    Public Function GetDeviceByIndex(Index As Integer, ByRef Info As AuraInfo) As Boolean Implements IAuraSCPD.GetDeviceByIndex
        If Info Is Nothing Then Info = New AuraInfo

        With TR064Start(ServiceFile,
                        "GetDeviceByIndex",
                        New Dictionary(Of String, String) From {{"NewIndex", Index}})

            Return .TryGetValueEx("NewDeviceHandle", Info.DeviceHandle) And
                   .TryGetValueEx("NewName", Info.Name) And
                   .TryGetValueEx("NewHardwareId", Info.HardwareId) And
                   .TryGetValueEx("NewSerialNumber", Info.SerialNumber) And
                   .TryGetValueEx("NewTopologyId", Info.TopologyId) And
                   .TryGetValueEx("NewClass", Info.Class) And
                   .TryGetValueEx("NewManufacturer", Info.Manufacturer) And
                   .TryGetValueEx("NewStatus", Info.Status) And
                   .TryGetValueEx("NewClientIP", Info.ClientIP)

        End With
    End Function

    Public Function GetDeviceByHandle(DeviceHandle As Integer, ByRef Info As AuraInfo) As Boolean Implements IAuraSCPD.GetDeviceByHandle
        If Info Is Nothing Then Info = New AuraInfo

        With TR064Start(ServiceFile,
                        "GetDeviceByHandle",
                        New Dictionary(Of String, String) From {{"NewDeviceHandle", DeviceHandle}})

            Info.DeviceHandle = DeviceHandle

            Return .TryGetValueEx("NewName", Info.Name) And
                   .TryGetValueEx("NewHardwareId", Info.HardwareId) And
                   .TryGetValueEx("NewSerialNumber", Info.SerialNumber) And
                   .TryGetValueEx("NewTopologyId", Info.TopologyId) And
                   .TryGetValueEx("NewClass", Info.Class) And
                   .TryGetValueEx("NewManufacturer", Info.Manufacturer) And
                   .TryGetValueEx("NewStatus", Info.Status) And
                   .TryGetValueEx("NewClientIP", Info.ClientIP)

        End With
    End Function

    Public Function ConnectDevice(DeviceHandle As Integer) As Boolean Implements IAuraSCPD.ConnectDevice
        Return Not TR064Start(ServiceFile, "ConnectDevice", New Dictionary(Of String, String) From {{"NewDeviceHandle", DeviceHandle}}).ContainsKey("Error")
    End Function

    Public Function DisconnectDevice(DeviceHandle As Integer) As Boolean Implements IAuraSCPD.DisconnectDevice
        Return Not TR064Start(ServiceFile, "DisconnectDevice", New Dictionary(Of String, String) From {{"NewDeviceHandle", DeviceHandle}}).ContainsKey("Error")
    End Function
End Class
