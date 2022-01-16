''' <summary>
''' TR-064 Support – WLANConfiguration
''' Date: 2020-11-02
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wlanconfigSCPD.pdf</see>
''' </summary>
Friend Class WlanconfigSCPD
    Implements IWlanconfigSCPD

    Private Property TR064Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWlanconfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wlanconfigSCPD Implements IWlanconfigSCPD.Servicefile
    Private Property XML As Serializer

    Public Sub New(Start As Func(Of SCPDFiles, String, Dictionary(Of String, String), Dictionary(Of String, String)), XMLSerializer As Serializer)

        TR064Start = Start

        XML = XMLSerializer
    End Sub

    Public Function SetEnable(Enable As Boolean) As Boolean Implements IWlanconfigSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetInfo(ByRef Info As WLANInfo) As Boolean Implements IWlanconfigSCPD.GetInfo
        If Info Is Nothing Then Info = New WLANInfo

        With TR064Start(ServiceFile, "GetInfo", Nothing)
            If .ContainsKey("NewX_AVM-DE_PossibleBeaconTypes") Then
                Info.PossibleBeaconTypes = .Item("NewX_AVM-DE_PossibleBeaconTypes").Split(",")

                Return .TryGetValue("NewEnable", Info.Enable) And
                       .TryGetValue("NewStatus", Info.Status) And
                       .TryGetValue("NewChannel", Info.Channel) And
                       .TryGetValue("NewSSID", Info.SSID) And
                       .TryGetValue("NewBeaconType", Info.BeaconType) And
                       .TryGetValue("NewMACAddressControlEnabled", Info.MACAddressControlEnabled) And
                       .TryGetValue("NewStandard", Info.Standard) And
                       .TryGetValue("NewBSSID", Info.BSSID) And
                       .TryGetValue("NewBasicEncryptionModes", Info.BasicEncryptionModes) And
                       .TryGetValue("NewBasicAuthenticationMode", Info.BasicAuthenticationMode) And
                       .TryGetValue("NewMaxCharsSSID", Info.MaxCharsSSID) And
                       .TryGetValue("NewMinCharsSSID", Info.MinCharsSSID) And
                       .TryGetValue("NewAllowedCharsSSID", Info.AllowedCharsSSID) And
                       .TryGetValue("NewMinCharsPSK", Info.MinCharsPSK) And
                       .TryGetValue("NewMaxCharsPSK", Info.MaxCharsPSK) And
                       .TryGetValue("NewAllowedCharsPSK", Info.AllowedCharsPSK)

            Else
                Return False
            End If

        End With
    End Function

    Public Function SetConfig(Config As WLANInfo) As Boolean Implements IWlanconfigSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", New Dictionary(Of String, String) From {{"NewMaxBitRate", Config.MaxBitRate},
                                                                                                {"NewChannel", Config.Channel},
                                                                                                {"NewSSID", Config.SSID},
                                                                                                {"NewBeaconType", Config.BeaconType},
                                                                                                {"NewMacAddressControlEnabled", Config.MACAddressControlEnabled.ToBoolStr},
                                                                                                {"NewBasicEncryptionModes", Config.BasicEncryptionModes},
                                                                                                {"NewBasicAuthenticationMode", Config.BasicAuthenticationMode}}).ContainsKey("Error")
    End Function

    Public Function SetSecurityKeys(PreSharedKey As String, KeyPassphrase As String) As Boolean Implements IWlanconfigSCPD.SetSecurityKeys
        Return Not TR064Start(ServiceFile, "SetSecurityKeys", New Dictionary(Of String, String) From {{"NewWEPKey0", String.Empty},
                                                                                                      {"NewWEPKey1", String.Empty},
                                                                                                      {"NewWEPKey2", String.Empty},
                                                                                                      {"NewWEPKey3", String.Empty},
                                                                                                      {"NewPreSharedKey", PreSharedKey},
                                                                                                      {"NewKeyPassphrase", KeyPassphrase}}).ContainsKey("Error")

    End Function

    Public Function GetSecurityKeys(ByRef PreSharedKey As String, ByRef KeyPassphrase As String) As Boolean Implements IWlanconfigSCPD.GetSecurityKeys
        With TR064Start(ServiceFile, "GetSecurityKeys", Nothing)

            Return .TryGetValue("NewPreSharedKey", PreSharedKey) And
                   .TryGetValue("NewKeyPassphrase", KeyPassphrase)

        End With
    End Function

    Public Function SetBasBeaconSecurityProperties(BasicEncryptionModes As String, BasicAuthenticationMode As String) As Boolean Implements IWlanconfigSCPD.SetBasBeaconSecurityProperties
        Return Not TR064Start(ServiceFile, "SetBasBeaconSecurityProperties", New Dictionary(Of String, String) From {{"NewBasicEncryptionModes", BasicEncryptionModes},
                                                                                                                     {"NewBasicAuthenticationMode", BasicAuthenticationMode}}).ContainsKey("Error")
    End Function

    Public Function GetBasBeaconSecurityProperties(ByRef BasicEncryptionModes As String, ByRef BasicAuthenticationMode As String) As Boolean Implements IWlanconfigSCPD.GetBasBeaconSecurityProperties
        With TR064Start(ServiceFile, "GetBasBeaconSecurityProperties", Nothing)

            Return .TryGetValue("NewBasicEncryptionModes", BasicEncryptionModes) And
                   .TryGetValue("NewBasicAuthenticationMode", BasicAuthenticationMode)

        End With
    End Function

    Public Function GetBSSID(ByRef BSSID As String) As Boolean Implements IWlanconfigSCPD.GetBSSID
        Return TR064Start(ServiceFile, "GetBSSID", Nothing).TryGetValue("NewBSSID", BSSID)
    End Function

    Public Function GetSSID(ByRef SSID As String) As Boolean Implements IWlanconfigSCPD.GetSSID
        Return TR064Start(ServiceFile, "GetSSID", Nothing).TryGetValue("NewSSID", SSID)
    End Function

    Public Function SetSSID(SSID As String) As Boolean Implements IWlanconfigSCPD.SetSSID
        Return Not TR064Start(ServiceFile, "SetSSID", New Dictionary(Of String, String) From {{"NewSSID", SSID}}).ContainsKey("Error")
    End Function

    Public Function GetBeaconType(ByRef BeaconType As String, ByRef PossibleBeaconTypes() As String) As Boolean Implements IWlanconfigSCPD.GetBeaconType
        With TR064Start(ServiceFile, "GetBeaconType", Nothing)

            If .ContainsKey("NewX_AVM-DE_PossibleBeaconTypes") Then
                PossibleBeaconTypes = .Item("NewX_AVM-DE_PossibleBeaconTypes").Split(",")
                Return .TryGetValue("NewBeaconType", BeaconType)
            Else
                Return False
            End If
        End With
    End Function

    Public Function SetBeaconType(BeaconType As String) As Boolean Implements IWlanconfigSCPD.SetBeaconType
        Return Not TR064Start(ServiceFile, "SetBeaconType", New Dictionary(Of String, String) From {{"NewBeaconType", BeaconType}}).ContainsKey("Error")
    End Function

    Public Function GetChannelInfo(ByRef Channel As Integer, ByRef PossibleChannels As String) As Boolean Implements IWlanconfigSCPD.GetChannelInfo
        With TR064Start(ServiceFile, "GetChannelInfo", Nothing)

            Return .TryGetValue("NewChannel", Channel) And
                   .TryGetValue("NewPossibleChannels", PossibleChannels)

        End With
    End Function

    Public Function SetChannel(Channel As Integer) As Boolean Implements IWlanconfigSCPD.SetChannel
        Return Not TR064Start(ServiceFile, "SetChannel", New Dictionary(Of String, String) From {{"NewChannel", Channel}}).ContainsKey("Error")
    End Function

    Public Function GetBeaconAdvertisement(ByRef BeaconAdvertisementEnabled As Boolean) As Boolean Implements IWlanconfigSCPD.GetBeaconAdvertisement
        Return TR064Start(ServiceFile, "GetBeaconAdvertisement", Nothing).TryGetValue("NewBeaconAdvertisementEnabled", BeaconAdvertisementEnabled)
    End Function

    Public Function SetBeaconAdvertisement(BeaconAdvertisementEnabled As Boolean) As Boolean Implements IWlanconfigSCPD.SetBeaconAdvertisement
        Return Not TR064Start(ServiceFile, "SetBeaconAdvertisement", New Dictionary(Of String, String) From {{"NewBeaconAdvertisementEnabled", BeaconAdvertisementEnabled.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetTotalAssociations(ByRef TotalAssociations As Integer) As Boolean Implements IWlanconfigSCPD.GetTotalAssociations
        Return TR064Start(ServiceFile, "GetTotalAssociations", Nothing).TryGetValue("NewTotalAssociations", TotalAssociations)
    End Function

    Public Function GetGenericAssociatedDeviceInfo(DeviceIndex As Integer, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetGenericAssociatedDeviceInfo
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", New Dictionary(Of String, String) From {{"NewAssociatedDeviceIndex", DeviceIndex}})

            DeviceInfo.AssociatedDeviceIndex = DeviceIndex

            Return .TryGetValue("NewAssociatedDeviceMACAddress", DeviceInfo.AssociatedDeviceMACAddress) And
                   .TryGetValue("NewAssociatedDeviceIPAddress", DeviceInfo.AssociatedDeviceIPAddress) And
                   .TryGetValue("NewAssociatedDeviceAuthState", DeviceInfo.AssociatedDeviceAuthState) And
                   .TryGetValue("X_AVM-DE_Speed", DeviceInfo.Speed) And
                   .TryGetValue("X_AVM-DE_SignalStrength", DeviceInfo.SignalStrength)

        End With
    End Function

    Public Function GetSpecificAssociatedDeviceInfo(AssociatedDeviceMACAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetSpecificAssociatedDeviceInfo
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", New Dictionary(Of String, String) From {{"NewAssociatedDeviceMACAddress", AssociatedDeviceMACAddress}})

            DeviceInfo.AssociatedDeviceMACAddress = AssociatedDeviceMACAddress

            Return .TryGetValue("NewAssociatedDeviceMACAddress", DeviceInfo.AssociatedDeviceMACAddress) And
                   .TryGetValue("NewAssociatedDeviceIPAddress", DeviceInfo.AssociatedDeviceIPAddress) And
                   .TryGetValue("NewAssociatedDeviceAuthState", DeviceInfo.AssociatedDeviceAuthState) And
                   .TryGetValue("X_AVM-DE_Speed", DeviceInfo.Speed) And
                   .TryGetValue("X_AVM-DE_SignalStrength", DeviceInfo.SignalStrength)

        End With
    End Function

    Public Function GetSpecificAssociatedDeviceInfoByIp(AssociatedDeviceIPAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetSpecificAssociatedDeviceInfoByIp
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", New Dictionary(Of String, String) From {{"NewAssociatedDeviceIPAddress", AssociatedDeviceIPAddress}})

            DeviceInfo.AssociatedDeviceIPAddress = AssociatedDeviceIPAddress

            Return .TryGetValue("NewAssociatedDeviceMACAddress", DeviceInfo.AssociatedDeviceMACAddress) And
                   .TryGetValue("NewAssociatedDeviceAuthState", DeviceInfo.AssociatedDeviceAuthState) And
                   .TryGetValue("X_AVM-DE_Speed", DeviceInfo.Speed) And
                   .TryGetValue("X_AVM-DE_SignalStrength", DeviceInfo.SignalStrength)
        End With
    End Function

    Public Function GetWLANDeviceListPath(ByRef WLANDeviceListPath As String) As Boolean Implements IWlanconfigSCPD.GetWLANDeviceListPath
        Return TR064Start(ServiceFile, "X_AVM-DE_GetWLANDeviceListPath", Nothing).TryGetValue("NewX_AVM-DE_WLANDeviceListPath", WLANDeviceListPath)
    End Function

    Public Function GetWLANDeviceList(AssociatedDevices As WLANDeviceList) As Boolean Implements IWlanconfigSCPD.GetWLANDeviceList
        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANDeviceListPath", Nothing)

            If .ContainsKey("NewX_AVM-DE_WLANDeviceListPath") Then

                XML.Deserialize(.Item("NewX_AVM-DE_WLANDeviceListPath"), False, AssociatedDevices)

                ' Wenn keine Hosts angeschlossen wurden, gib eine leere Klasse zurück
                If AssociatedDevices Is Nothing Then AssociatedDevices = New WLANDeviceList

                Return True

            Else
                AssociatedDevices = Nothing

                Return False
            End If
        End With
    End Function

    Public Function SetStickSurfEnable(StickSurfEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetStickSurfEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetStickSurfEnable", New Dictionary(Of String, String) From {{"NewStickSurfEnable", StickSurfEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetIPTVOptimized(ByRef IPTVoptimize As Boolean) As Boolean Implements IWlanconfigSCPD.GetIPTVOptimized
        Return TR064Start(ServiceFile, "X_AVM-DE_GetIPTVOptimized", Nothing).TryGetValue("NewX_AVM-DE_IPTVoptimize", IPTVoptimize)
    End Function

    Public Function SetIPTVOptimized(IPTVoptimize As Boolean) As Boolean Implements IWlanconfigSCPD.SetIPTVOptimized
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetIPTVOptimized", New Dictionary(Of String, String) From {{"NewX_AVM-DE_IPTVoptimize", IPTVoptimize.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean Implements IWlanconfigSCPD.GetStatistics
        With TR064Start(ServiceFile, "GetStatistics", Nothing)

            Return .TryGetValue("NewTotalPacketsSent", TotalPacketsSent) And
                   .TryGetValue("NewTotalPacketsReceived", TotalPacketsReceived)

        End With
    End Function

    Public Function GetPacketStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean Implements IWlanconfigSCPD.GetPacketStatistics
        With TR064Start(ServiceFile, "GetPacketStatistics", Nothing)

            Return .TryGetValue("NewTotalPacketsSent", TotalPacketsSent) And
                   .TryGetValue("NewTotalPacketsReceived", TotalPacketsReceived)

        End With
    End Function

    Public Function GetNightControl(ByRef NightControl As String, ByRef NightTimeControlNoForcedOff As Boolean) As Boolean Implements IWlanconfigSCPD.GetNightControl
        With TR064Start(ServiceFile, "X_AVM-DE_GetNightControl", Nothing)

            Return .TryGetValue("NewNightControl", NightControl) And
                   .TryGetValue("NewNightTimeControlNoForcedOff", NightTimeControlNoForcedOff)

        End With
    End Function

    Public Function SetHighFrequencyBand(EnableHighFrequency As Boolean) As Boolean Implements IWlanconfigSCPD.SetHighFrequencyBand
        Return Not TR064Start(ServiceFile, "X_SetHighFrequencyBand", New Dictionary(Of String, String) From {{"NewEnableHighFrequency", EnableHighFrequency.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetWLANHybridMode(ByRef Info As WLANHybridMode) As Boolean Implements IWlanconfigSCPD.GetWLANHybridMode
        If Info Is Nothing Then Info = New WLANHybridMode

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANHybridMode", Nothing)

            Return .TryGetValue("NewEnable", Info.Enable) And
                   .TryGetValue("NewBeaconType", Info.BeaconType) And
                   .TryGetValue("NewKeyPassphrase", Info.KeyPassphrase) And
                   .TryGetValue("NewSSID", Info.SSID) And
                   .TryGetValue("NewBSSID", Info.BSSID) And
                   .TryGetValue("NewTrafficMode", Info.TrafficMode) And
                   .TryGetValue("NewManualSpeed", Info.ManualSpeed) And
                   .TryGetValue("NewMaxSpeedDS", Info.MaxSpeedDS) And
                   .TryGetValue("NewMaxSpeedUS", Info.MaxSpeedUS)

        End With
    End Function

    Public Function SetWLANHybridMode(Info As WLANHybridMode) As Boolean Implements IWlanconfigSCPD.SetWLANHybridMode
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWLANHybridMode", New Dictionary(Of String, String) From {{"NewEnable", Info.Enable.ToBoolStr},
                                                                                                                 {"NewBeaconType", Info.BeaconType},
                                                                                                                 {"NewKeyPassphrase", Info.KeyPassphrase},
                                                                                                                 {"NewSSID", Info.SSID},
                                                                                                                 {"NewBSSID", Info.BSSID},
                                                                                                                 {"NewTrafficMode", Info.TrafficMode},
                                                                                                                 {"NewManualSpeed", Info.ManualSpeed.ToBoolStr},
                                                                                                                 {"NewMaxSpeedDS", Info.MaxSpeedDS},
                                                                                                                 {"NewMaxSpeedUS", Info.MaxSpeedUS}}).ContainsKey("Error")

    End Function

    Public Function GetWLANExtInfo(ByRef Info As WLANExtInfo) As Boolean Implements IWlanconfigSCPD.GetWLANExtInfo
        If Info Is Nothing Then Info = New WLANExtInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANExtInfo", Nothing)

            Return .TryGetValue("NewX_AVM-DE_APEnabled", Info.APEnabled) And
                   .TryGetValue("NewX_AVM-DE_APType", Info.APType) And
                   .TryGetValue("NewX_AVM-DE_TimeoutActive", Info.TimeoutActive) And
                   .TryGetValue("NewX_AVM-DE_Timeout", Info.Timeout) And
                   .TryGetValue("NewX_AVM-DE_TimeRemain", Info.TimeRemain) And
                   .TryGetValue("NewX_AVM-DE_NoForcedOff", Info.NoForcedOff) And
                   .TryGetValue("NewX_AVM-DE_UserIsolation", Info.UserIsolation) And
                   .TryGetValue("NewX_AVM-DE_EncryptionMode", Info.EncryptionMode) And
                   .TryGetValue("NewX_AVM-DE_LastChangedStamp", Info.LastChangedStamp)

        End With
    End Function

    Public Function SetWLANGlobalEnable(WLANGlobalEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetWLANGlobalEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWLANGlobalEnable", New Dictionary(Of String, String) From {{"NewX_AVM-DE_WLANGlobalEnable", WLANGlobalEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetWPSInfo(ByRef WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean Implements IWlanconfigSCPD.GetWPSInfo
        With TR064Start(ServiceFile, "X_AVM-DE_GetWPSInfo", Nothing)

            Return .TryGetValue("NewX_AVM-DE_WPSMode", WPSMode) And
                   .TryGetValue("NewX_AVM-DE_WPSStatus", WPSStatus)

        End With
    End Function

    Public Function SetWPSConfig(WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean Implements IWlanconfigSCPD.SetWPSConfig
        Return TR064Start(ServiceFile, "X_AVM-DE_GetWPSInfo",
                          New Dictionary(Of String, String) From {{"NewX_AVM-DE_WPSMode", WPSMode}}).
                          TryGetValue("NewX_AVM-DE_WPSStatus", WPSStatus)

    End Function

    Public Function SetWPSEnable(WPSEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetWPSEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWPSEnable", New Dictionary(Of String, String) From {{"NewX_AVM-DE_WPSEnable", WPSEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetWLANConnectionInfo(ByRef Info As WLANConnectionInfo) As Boolean Implements IWlanconfigSCPD.GetWLANConnectionInfo
        If Info Is Nothing Then Info = New WLANConnectionInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANConnectionInfo", Nothing)

            Return .TryGetValue("NewAssociatedDeviceMACAddress", Info.AssociatedDeviceMACAddress) And
                   .TryGetValue("NewSSID", Info.SSID) And
                   .TryGetValue("NewBSSID", Info.BSSID) And
                   .TryGetValue("NewBeaconType", Info.BeaconType) And
                   .TryGetValue("NewChannel", Info.Channel) And
                   .TryGetValue("NewStandard", Info.Standard) And
                   .TryGetValue("NewX_AVM-DE_Speed", Info.Speed) And
                   .TryGetValue("NewX_AVM-DE_SpeedRX", Info.SpeedRX) And
                   .TryGetValue("NewX_AVM-DE_SpeedMax", Info.SpeedMax) And
                   .TryGetValue("NewX_AVM-DE_SpeedRXMax", Info.SpeedRXMax)

        End With
    End Function
End Class
