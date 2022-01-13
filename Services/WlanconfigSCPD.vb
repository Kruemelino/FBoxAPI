''' <summary>
''' TR-064 Support – WLANConfiguration
''' Date: 2020-11-02
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wlanconfigSCPD.pdf</see>
''' </summary>
Friend Class WlanconfigSCPD
    Implements IWlanconfigSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Hashtable, Hashtable) Implements IWlanconfigSCPD.TR064Start
    Private Property PushStatus As Action(Of LogLevel, String) Implements IWlanconfigSCPD.PushStatus
    Private ReadOnly Property ServiceFile As SCPDFiles Implements IWlanconfigSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Hashtable, Hashtable), Status As Action(Of LogLevel, String), XMLSerializer As Serializer)

        ServiceFile = SCPDFiles.wlanconfigSCPD

        TR064Start = Start

        PushStatus = Status

        XML = XMLSerializer
    End Sub

    Public Function SetEnable(Enable As Boolean) As Boolean Implements IWlanconfigSCPD.SetEnable
        With TR064Start(ServiceFile, "SetEnable", New Hashtable From {{"NewEnable", Enable.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetInfo(ByRef Info As WLANInfo) As Boolean Implements IWlanconfigSCPD.GetInfo
        If Info Is Nothing Then Info = New WLANInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)

            If .ContainsKey("NewEnable") Then
                Info.Enable = CBool(.Item("NewEnable"))
                Info.Status = .Item("NewStatus").ToString
                Info.Channel = .Item("NewChannel").ToString
                Info.SSID = .Item("NewSSID").ToString
                Info.BeaconType = .Item("NewBeaconType").ToString
                Info.PossibleBeaconTypes = .Item("NewX_AVM-DE_PossibleBeaconTypes").ToString.Split(",")
                Info.MACAddressControlEnabled = CBool(.Item("NewMACAddressControlEnabled"))
                Info.Standard = .Item("NewStandard").ToString
                Info.BSSID = .Item("NewBSSID").ToString
                Info.BasicEncryptionModes = .Item("NewBasicEncryptionModes").ToString
                Info.BasicAuthenticationMode = .Item("NewBasicAuthenticationMode").ToString
                Info.MaxCharsSSID = CInt(.Item("NewMaxCharsSSID"))
                Info.MinCharsSSID = CInt(.Item("NewMinCharsSSID"))
                Info.AllowedCharsSSID = .Item("NewAllowedCharsSSID").ToString
                Info.MinCharsPSK = CInt(.Item("NewMinCharsPSK"))
                Info.MaxCharsPSK = CInt(.Item("NewMaxCharsPSK"))
                Info.AllowedCharsPSK = .Item("NewAllowedCharsPSK").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetInfo (WLAN) konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetConfig(Config As WLANInfo) As Boolean Implements IWlanconfigSCPD.SetConfig
        With TR064Start(ServiceFile, "SetConfig", New Hashtable From {{"NewMaxBitRate", Config.MaxBitRate},
                                                                      {"NewChannel", Config.Channel},
                                                                      {"NewSSID", Config.SSID},
                                                                      {"NewBeaconType", Config.BeaconType},
                                                                      {"NewMacAddressControlEnabled", Config.MACAddressControlEnabled.ToInt},
                                                                      {"NewBasicEncryptionModes", Config.BasicEncryptionModes},
                                                                      {"NewBasicAuthenticationMode", Config.BasicAuthenticationMode}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function SetSecurityKeys(PreSharedKey As String, KeyPassphrase As String) As Boolean Implements IWlanconfigSCPD.SetSecurityKeys
        With TR064Start(ServiceFile, "SetSecurityKeys", New Hashtable From {{"NewWEPKey0", String.Empty},
                                                                            {"NewWEPKey1", String.Empty},
                                                                            {"NewWEPKey2", String.Empty},
                                                                            {"NewWEPKey3", String.Empty},
                                                                            {"NewPreSharedKey", PreSharedKey},
                                                                            {"NewKeyPassphrase", KeyPassphrase}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetSecurityKeys(ByRef PreSharedKey As String, ByRef KeyPassphrase As String) As Boolean Implements IWlanconfigSCPD.GetSecurityKeys
        With TR064Start(ServiceFile, "GetSecurityKeys", Nothing)
            If .ContainsKey("NewPreSharedKey") And .ContainsKey("NewKeyPassphrase") Then

                PreSharedKey = .Item("NewPreSharedKey").ToString
                KeyPassphrase = .Item("NewKeyPassphrase").ToString


                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSecurityKeys von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetBasBeaconSecurityProperties(BasicEncryptionModes As String, BasicAuthenticationMode As String) As Boolean Implements IWlanconfigSCPD.SetBasBeaconSecurityProperties
        With TR064Start(ServiceFile, "SetBasBeaconSecurityProperties", New Hashtable From {{"NewBasicEncryptionModes", BasicEncryptionModes}, {"NewBasicAuthenticationMode", BasicAuthenticationMode}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetBasBeaconSecurityProperties(ByRef BasicEncryptionModes As String, ByRef BasicAuthenticationMode As String) As Boolean Implements IWlanconfigSCPD.GetBasBeaconSecurityProperties
        With TR064Start(ServiceFile, "GetBasBeaconSecurityProperties", Nothing)
            If .ContainsKey("NewBasicEncryptionModes") And .ContainsKey("NewBasicAuthenticationMode") Then

                BasicEncryptionModes = .Item("NewBasicEncryptionModes").ToString
                BasicAuthenticationMode = .Item("NewBasicAuthenticationMode").ToString

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetBasBeaconSecurityProperties von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetBSSID(ByRef BSSID As String) As Boolean Implements IWlanconfigSCPD.GetBSSID
        With TR064Start(ServiceFile, "GetBSSID", Nothing)
            If .ContainsKey("NewBSSID") Then

                BSSID = .Item("NewBSSID").ToString

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetBSSID von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSSID(ByRef SSID As String) As Boolean Implements IWlanconfigSCPD.GetSSID
        With TR064Start(ServiceFile, "GetSSID", Nothing)
            If .ContainsKey("NewSSID") Then

                SSID = .Item("NewSSID").ToString

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetSSID von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetSSID(SSID As String) As Boolean Implements IWlanconfigSCPD.SetSSID
        With TR064Start(ServiceFile, "SetSSID", New Hashtable From {{"NewSSID", SSID}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetBeaconType(ByRef BeaconType As String, ByRef PossibleBeaconTypes() As String) As Boolean Implements IWlanconfigSCPD.GetBeaconType
        With TR064Start(ServiceFile, "GetBeaconType", Nothing)
            If .ContainsKey("NewBeaconType") And .ContainsKey("NewX_AVM-DE_PossibleBeaconTypes") Then

                BeaconType = .Item("NewBeaconType").ToString
                PossibleBeaconTypes = .Item("NewX_AVM-DE_PossibleBeaconTypes").ToString.Split(",")

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetBeaconType von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetBeaconType(BeaconType As String) As Boolean Implements IWlanconfigSCPD.SetBeaconType
        With TR064Start(ServiceFile, "SetBeaconType", New Hashtable From {{"NewBeaconType", BeaconType}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetChannelInfo(ByRef Channel As Integer, ByRef PossibleChannels As String) As Boolean Implements IWlanconfigSCPD.GetChannelInfo
        With TR064Start(ServiceFile, "GetChannelInfo", Nothing)
            If .ContainsKey("NewChannel") And .ContainsKey("NewPossibleChannels") Then

                Channel = CInt(.Item("NewChannel"))
                PossibleChannels = .Item("NewPossibleChannels").ToString

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetChannelInfo von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetChannel(Channel As Integer) As Boolean Implements IWlanconfigSCPD.SetChannel
        With TR064Start(ServiceFile, "SetChannel", New Hashtable From {{"NewChannel", Channel}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetBeaconAdvertisement(ByRef BeaconAdvertisementEnabled As Boolean) As Boolean Implements IWlanconfigSCPD.GetBeaconAdvertisement
        With TR064Start(ServiceFile, "GetBeaconAdvertisement", Nothing)
            If .ContainsKey("NewBeaconAdvertisementEnabled") Then

                BeaconAdvertisementEnabled = .Item("NewBeaconAdvertisementEnabled").ToString

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetBeaconAdvertisement von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetBeaconAdvertisement(BeaconAdvertisementEnabled As Boolean) As Boolean Implements IWlanconfigSCPD.SetBeaconAdvertisement
        With TR064Start(ServiceFile, "SetBeaconAdvertisement", New Hashtable From {{"NewBeaconAdvertisementEnabled", BeaconAdvertisementEnabled}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetTotalAssociations(ByRef TotalAssociations As Integer) As Boolean Implements IWlanconfigSCPD.GetTotalAssociations
        With TR064Start(ServiceFile, "GetTotalAssociations", Nothing)
            If .ContainsKey("NewTotalAssociations") Then

                TotalAssociations = CInt(.Item("NewTotalAssociations"))

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetTotalAssociations von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetGenericAssociatedDeviceInfo(DeviceIndex As Integer, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetGenericAssociatedDeviceInfo
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", New Hashtable From {{"NewAssociatedDeviceIndex", DeviceIndex}})

            If .ContainsKey("NewAssociatedDeviceMACAddress") Then
                DeviceInfo.AssociatedDeviceIndex = DeviceIndex
                DeviceInfo.AssociatedDeviceMACAddress = .Item("NewAssociatedDeviceMACAddress").ToString
                DeviceInfo.AssociatedDeviceIPAddress = .Item("NewAssociatedDeviceIPAddress").ToString
                DeviceInfo.AssociatedDeviceAuthState = CBool(.Item("NewAssociatedDeviceAuthState"))
                DeviceInfo.Speed = CInt(.Item("X_AVM-DE_Speed"))
                DeviceInfo.SignalStrength = .Item("X_AVM-DE_SignalStrength").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericAssociatedDeviceInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSpecificAssociatedDeviceInfo(AssociatedDeviceMACAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetSpecificAssociatedDeviceInfo
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", New Hashtable From {{"NewAssociatedDeviceMACAddress", AssociatedDeviceMACAddress}})

            If .ContainsKey("NewAssociatedDeviceMACAddress") Then
                DeviceInfo.AssociatedDeviceMACAddress = AssociatedDeviceMACAddress
                DeviceInfo.AssociatedDeviceIPAddress = .Item("NewAssociatedDeviceIPAddress").ToString
                DeviceInfo.AssociatedDeviceAuthState = CBool(.Item("NewAssociatedDeviceAuthState"))
                DeviceInfo.Speed = CInt(.Item("X_AVM-DE_Speed"))
                DeviceInfo.SignalStrength = .Item("X_AVM-DE_SignalStrength").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericAssociatedDeviceInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetSpecificAssociatedDeviceInfoByIp(AssociatedDeviceIPAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetSpecificAssociatedDeviceInfoByIp
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", New Hashtable From {{"NewAssociatedDeviceIPAddress", AssociatedDeviceIPAddress}})

            If .ContainsKey("NewAssociatedDeviceMACAddress") Then
                DeviceInfo.AssociatedDeviceMACAddress = .Item("NewAssociatedDeviceMACAddress").ToString
                DeviceInfo.AssociatedDeviceIPAddress = AssociatedDeviceIPAddress
                DeviceInfo.AssociatedDeviceAuthState = CBool(.Item("NewAssociatedDeviceAuthState"))
                DeviceInfo.Speed = CInt(.Item("X_AVM-DE_Speed"))
                DeviceInfo.SignalStrength = .Item("X_AVM-DE_SignalStrength").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetGenericAssociatedDeviceInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetWLANDeviceListPath(ByRef WLANDeviceListPath As String) As Boolean Implements IWlanconfigSCPD.GetWLANDeviceListPath
        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANDeviceListPath", Nothing)

            If .ContainsKey("NewX_AVM-DE_WLANDeviceListPath") Then
                WLANDeviceListPath = CBool(.Item("NewX_AVM-DE_WLANDeviceListPath"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-NewX_AVM-DE_GetWLANDeviceListPath konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetWLANDeviceList(AssociatedDevices As WLANDeviceList) As Boolean Implements IWlanconfigSCPD.GetWLANDeviceList
        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANDeviceListPath", Nothing)

            If .ContainsKey("NewX_AVM-DE_WLANDeviceListPath") Then

                If Not XML.Deserialize(.Item("NewX_AVM-DE_WLANDeviceListPath").ToString(), False, AssociatedDevices) Then
                    PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetWLANDeviceListPath konnte für nicht deserialisiert werden.")
                End If

                ' Wenn keine Hosts angeschlossen wurden, gib eine leere Klasse zurück
                If AssociatedDevices Is Nothing Then AssociatedDevices = New WLANDeviceList

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetHostListPath konnte für nicht aufgelößt werden. '{ .Item("Error")}'")
                AssociatedDevices = Nothing

                Return False
            End If
        End With
    End Function

    Public Function SetStickSurfEnable(StickSurfEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetStickSurfEnable
        With TR064Start(ServiceFile, "X_AVM-DE_SetStickSurfEnable", New Hashtable From {{"NewStickSurfEnable", StickSurfEnable}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetIPTVOptimized(ByRef IPTVoptimize As Boolean) As Boolean Implements IWlanconfigSCPD.GetIPTVOptimized
        With TR064Start(ServiceFile, "X_AVM-DE_GetIPTVOptimized", Nothing)
            If .ContainsKey("NewX_AVM-DE_IPTVoptimize") Then

                IPTVoptimize = CInt(.Item("NewX_AVM-DE_IPTVoptimize"))

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetIPTVOptimized von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetIPTVOptimized(IPTVoptimize As Boolean) As Boolean Implements IWlanconfigSCPD.SetIPTVOptimized
        With TR064Start(ServiceFile, "X_AVM-DE_SetIPTVOptimized", New Hashtable From {{"NewX_AVM-DE_IPTVoptimize", IPTVoptimize.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean Implements IWlanconfigSCPD.GetStatistics
        With TR064Start(ServiceFile, "GetStatistics", Nothing)
            If .ContainsKey("NewTotalPacketsSent") And .ContainsKey("NewTotalPacketsReceived") Then

                TotalPacketsSent = CInt(.Item("NewTotalPacketsSent"))
                TotalPacketsReceived = CInt(.Item("NewTotalPacketsReceived"))

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetStatistics von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetPacketStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean Implements IWlanconfigSCPD.GetPacketStatistics
        With TR064Start(ServiceFile, "GetPacketStatistics", Nothing)
            If .ContainsKey("NewTotalPacketsSent") And .ContainsKey("NewTotalPacketsReceived") Then

                TotalPacketsSent = CInt(.Item("NewTotalPacketsSent"))
                TotalPacketsReceived = CInt(.Item("NewTotalPacketsReceived"))

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"GetPacketStatistics von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function GetNightControl(ByRef NightControl As String, ByRef NightTimeControlNoForcedOff As Boolean) As Boolean Implements IWlanconfigSCPD.GetNightControl
        With TR064Start(ServiceFile, "X_AVM-DE_GetNightControl", Nothing)
            If .ContainsKey("NewNightControl") And .ContainsKey("NewNightTimeControlNoForcedOff") Then

                NightControl = .Item("NewNightControl").ToString
                NightTimeControlNoForcedOff = CBool(.Item("NewNightTimeControlNoForcedOff"))

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetNightControl von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetHighFrequencyBand(EnableHighFrequency As Boolean) As Boolean Implements IWlanconfigSCPD.SetHighFrequencyBand
        With TR064Start(ServiceFile, "X_SetHighFrequencyBand", New Hashtable From {{"NewEnableHighFrequency", EnableHighFrequency.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetWLANHybridMode(ByRef Info As WLANHybridMode) As Boolean Implements IWlanconfigSCPD.GetWLANHybridMode
        If Info Is Nothing Then Info = New WLANHybridMode

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANHybridMode", Nothing)

            If .ContainsKey("NewEnable") Then
                Info.Enable = CBool(.Item("NewEnable"))
                Info.BeaconType = .Item("NewBeaconType").ToString
                Info.KeyPassphrase = .Item("NewKeyPassphrase").ToString
                Info.SSID = .Item("NewSSID").ToString
                Info.BSSID = .Item("NewBSSID").ToString
                Info.TrafficMode = .Item("NewTrafficMode").ToString
                Info.ManualSpeed = CBool(.Item("NewManualSpeed"))
                Info.MaxSpeedDS = CInt(.Item("NewMaxSpeedDS"))
                Info.MaxSpeedUS = CInt(.Item("NewMaxSpeedUS"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetWLANHybridMode konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetWLANHybridMode(Info As WLANHybridMode) As Boolean Implements IWlanconfigSCPD.SetWLANHybridMode
        With TR064Start(ServiceFile, "X_AVM-DE_SetWLANHybridMode", New Hashtable From {{"NewEnable", Info.Enable.ToInt},
                                                                                       {"NewBeaconType", Info.BeaconType},
                                                                                       {"NewKeyPassphrase", Info.KeyPassphrase},
                                                                                       {"NewSSID", Info.SSID},
                                                                                       {"NewBSSID", Info.BSSID},
                                                                                       {"NewTrafficMode", Info.TrafficMode},
                                                                                       {"NewManualSpeed", Info.ManualSpeed.ToInt},
                                                                                       {"NewMaxSpeedDS", Info.MaxSpeedDS},
                                                                                       {"NewMaxSpeedUS", Info.MaxSpeedUS}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetWLANExtInfo(ByRef Info As WLANExtInfo) As Boolean Implements IWlanconfigSCPD.GetWLANExtInfo
        If Info Is Nothing Then Info = New WLANExtInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANExtInfo", Nothing)

            If .ContainsKey("NewX_AVM-DE_APEnabled") Then
                Info.APEnabled = CBool(.Item("NewX_AVM-DE_APEnabled"))
                Info.APType = .Item("NewX_AVM-DE_APType").ToString
                Info.TimeoutActive = CBool(.Item("NewX_AVM-DE_TimeoutActive"))
                Info.Timeout = CInt(.Item("NewX_AVM-DE_Timeout"))
                Info.TimeRemain = CInt(.Item("NewX_AVM-DE_TimeRemain"))
                Info.NoForcedOff = CBool(.Item("NewX_AVM-DE_NoForcedOff"))
                Info.UserIsolation = CBool(.Item("NewX_AVM-DE_UserIsolation"))
                Info.EncryptionMode = .Item("NewX_AVM-DE_EncryptionMode").ToString
                Info.LastChangedStamp = .Item("NewX_AVM-DE_LastChangedStamp").ToString

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetWLANExtInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetWLANGlobalEnable(WLANGlobalEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetWLANGlobalEnable
        With TR064Start(ServiceFile, "X_AVM-DE_SetWLANGlobalEnable", New Hashtable From {{"NewX_AVM-DE_WLANGlobalEnable", WLANGlobalEnable.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetWPSInfo(ByRef WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean Implements IWlanconfigSCPD.GetWPSInfo
        With TR064Start(ServiceFile, "X_AVM-DE_GetWPSInfo", Nothing)
            If .ContainsKey("NewX_AVM-DE_WPSMode") And .ContainsKey("NewX_AVM-DE_WPSStatus") Then

                WPSMode = CType(.Item("NewX_AVM-DE_WPSMode"), WPSModeEnum)
                WPSStatus = CType(.Item("NewX_AVM-DE_WPSStatus"), WPSStatusEnum)

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetWPSInfo von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetWPSConfig(WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean Implements IWlanconfigSCPD.SetWPSConfig
        With TR064Start(ServiceFile, "X_AVM-DE_GetWPSInfo", New Hashtable From {{"NewX_AVM-DE_WPSMode", WPSMode.ToString}})
            If .ContainsKey("NewX_AVM-DE_WPSStatus") Then

                WPSStatus = CType(.Item("NewX_AVM-DE_WPSStatus"), WPSStatusEnum)

                Return True
            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetWPSInfo von Fritz!Box nicht erhalten. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function

    Public Function SetWPSEnable(WPSEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetWPSEnable
        With TR064Start(ServiceFile, "X_AVM-DE_SetWPSEnable", New Hashtable From {{"NewX_AVM-DE_WPSEnable", WPSEnable.ToInt}})
            Return Not .ContainsKey("Error")
        End With
    End Function

    Public Function GetWLANConnectionInfo(ByRef Info As WLANConnectionInfo) As Boolean Implements IWlanconfigSCPD.GetWLANConnectionInfo
        If Info Is Nothing Then Info = New WLANConnectionInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANConnectionInfo", Nothing)

            If .ContainsKey("NewEnable") Then
                Info.AssociatedDeviceMACAddress = CBool(.Item("NewAssociatedDeviceMACAddress"))
                Info.SSID = .Item("NewSSID").ToString
                Info.BSSID = .Item("NewBSSID").ToString
                Info.BeaconType = .Item("NewBeaconType").ToString
                Info.Channel = .Item("NewChannel").ToString
                Info.Standard = .Item("NewStandard").ToString
                Info.Speed = CInt(.Item("NewX_AVM-DE_Speed"))
                Info.SpeedRX = CInt(.Item("NewX_AVM-DE_SpeedRX"))
                Info.SpeedMax = CInt(.Item("NewX_AVM-DE_SpeedMax"))
                Info.SpeedRXMax = CInt(.Item("NewX_AVM-DE_SpeedRXMax"))

                Return True

            Else
                PushStatus.Invoke(LogLevel.Warn, $"X_AVM-DE_GetWLANConnectionInfo konnte nicht aufgelößt werden. '{ .Item("Error")}'")

                Return False
            End If
        End With
    End Function
End Class
