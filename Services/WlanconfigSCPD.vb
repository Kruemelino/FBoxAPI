''' <summary>
''' TR-064 Support – WLANConfiguration
''' Date: 2022-10-13
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wlanconfigSCPD.pdf</see>
''' </summary>
Friend Class WlanconfigSCPD
    Implements IWlanconfigSCPD

    Public ReadOnly Property DocumentationDate As Date = New Date(2022, 10, 13) Implements IWlanconfigSCPD.DocumentationDate
    Private Property TR064Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)) Implements IWlanconfigSCPD.TR064Start
    Private ReadOnly Property ServiceFile As SCPDFiles = SCPDFiles.wlanconfigSCPD Implements IWlanconfigSCPD.Servicefile
    Private ReadOnly Property ServiceID As Integer Implements IWlanconfigSCPD.ServiceID

    Private Property XML As Serializer


    Public Sub New(Start As Func(Of SCPDFiles, String, Integer, Dictionary(Of String, String), Dictionary(Of String, String)),
                   ServiceNumberID As Integer,
                   XMLSerializer As Serializer)

        TR064Start = Start
        ServiceID = ServiceNumberID
        XML = XMLSerializer
    End Sub

    Public Function SetEnable(Enable As Boolean) As Boolean Implements IWlanconfigSCPD.SetEnable
        Return Not TR064Start(ServiceFile, "SetEnable", ServiceID, New Dictionary(Of String, String) From {{"NewEnable", Enable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetInfo(ByRef Info As WLANInfo) As Boolean Implements IWlanconfigSCPD.GetInfo
        If Info Is Nothing Then Info = New WLANInfo

        With TR064Start(ServiceFile, "GetInfo", ServiceID, Nothing)
            If .ContainsKey("NewX_AVM-DE_PossibleBeaconTypes") Then
                Info.PossibleBeaconTypes = .Item("NewX_AVM-DE_PossibleBeaconTypes").Split(","c)

                Return .TryGetValueEx("NewEnable", Info.Enable) And
                       .TryGetValueEx("NewStatus", Info.Status) And
                       .TryGetValueEx("NewChannel", Info.Channel) And
                       .TryGetValueEx("NewSSID", Info.SSID) And
                       .TryGetValueEx("NewBeaconType", Info.BeaconType) And
                       .TryGetValueEx("NewMACAddressControlEnabled", Info.MACAddressControlEnabled) And
                       .TryGetValueEx("NewStandard", Info.Standard) And
                       .TryGetValueEx("NewBSSID", Info.BSSID) And
                       .TryGetValueEx("NewBasicEncryptionModes", Info.BasicEncryptionModes) And
                       .TryGetValueEx("NewBasicAuthenticationMode", Info.BasicAuthenticationMode) And
                       .TryGetValueEx("NewMaxCharsSSID", Info.MaxCharsSSID) And
                       .TryGetValueEx("NewMinCharsSSID", Info.MinCharsSSID) And
                       .TryGetValueEx("NewAllowedCharsSSID", Info.AllowedCharsSSID) And
                       .TryGetValueEx("NewMinCharsPSK", Info.MinCharsPSK) And
                       .TryGetValueEx("NewMaxCharsPSK", Info.MaxCharsPSK) And
                       .TryGetValueEx("NewAllowedCharsPSK", Info.AllowedCharsPSK) And
                       .TryGetValueEx("NewX_AVM-DE_FrequencyBand", Info.FrequencyBand) And
                       .TryGetValueEx("NewX_AVM-DE_WLANGlobalEnable", Info.WLANGlobalEnable)

            Else
                Return False
            End If

        End With
    End Function

    Public Function SetConfig(Config As WLANInfo) As Boolean Implements IWlanconfigSCPD.SetConfig
        Return Not TR064Start(ServiceFile, "SetConfig", ServiceID,
                              New Dictionary(Of String, String) From {{"NewMaxBitRate", Config.MaxBitRate},
                                                                      {"NewChannel", Config.Channel},
                                                                      {"NewSSID", Config.SSID},
                                                                      {"NewBeaconType", Config.BeaconType},
                                                                      {"NewMacAddressControlEnabled", Config.MACAddressControlEnabled.ToBoolStr},
                                                                      {"NewBasicEncryptionModes", Config.BasicEncryptionModes},
                                                                      {"NewBasicAuthenticationMode", Config.BasicAuthenticationMode}}).ContainsKey("Error")
    End Function

    Public Function SetSecurityKeys(PreSharedKey As String, KeyPassphrase As String) As Boolean Implements IWlanconfigSCPD.SetSecurityKeys
        Return Not TR064Start(ServiceFile, "SetSecurityKeys", ServiceID,
                              New Dictionary(Of String, String) From {{"NewWEPKey0", String.Empty},
                                                                      {"NewWEPKey1", String.Empty},
                                                                      {"NewWEPKey2", String.Empty},
                                                                      {"NewWEPKey3", String.Empty},
                                                                      {"NewPreSharedKey", PreSharedKey},
                                                                      {"NewKeyPassphrase", KeyPassphrase}}).ContainsKey("Error")

    End Function

    Public Function GetSecurityKeys(ByRef PreSharedKey As String, ByRef KeyPassphrase As String) As Boolean Implements IWlanconfigSCPD.GetSecurityKeys
        With TR064Start(ServiceFile, "GetSecurityKeys", ServiceID, Nothing)

            Return .TryGetValueEx("NewPreSharedKey", PreSharedKey) And
                   .TryGetValueEx("NewKeyPassphrase", KeyPassphrase)

        End With
    End Function

    Public Function SetBasBeaconSecurityProperties(BasicEncryptionModes As String, BasicAuthenticationMode As String) As Boolean Implements IWlanconfigSCPD.SetBasBeaconSecurityProperties
        Return Not TR064Start(ServiceFile, "SetBasBeaconSecurityProperties", ServiceID,
                              New Dictionary(Of String, String) From {{"NewBasicEncryptionModes", BasicEncryptionModes},
                                                                      {"NewBasicAuthenticationMode", BasicAuthenticationMode}}).ContainsKey("Error")
    End Function

    Public Function GetBasBeaconSecurityProperties(ByRef BasicEncryptionModes As String, ByRef BasicAuthenticationMode As String) As Boolean Implements IWlanconfigSCPD.GetBasBeaconSecurityProperties
        With TR064Start(ServiceFile, "GetBasBeaconSecurityProperties", ServiceID, Nothing)

            Return .TryGetValueEx("NewBasicEncryptionModes", BasicEncryptionModes) And
                   .TryGetValueEx("NewBasicAuthenticationMode", BasicAuthenticationMode)

        End With
    End Function

    Public Function GetBSSID(ByRef BSSID As String) As Boolean Implements IWlanconfigSCPD.GetBSSID
        Return TR064Start(ServiceFile, "GetBSSID", ServiceID, Nothing).TryGetValueEx("NewBSSID", BSSID)
    End Function

    Public Function GetSSID(ByRef SSID As String) As Boolean Implements IWlanconfigSCPD.GetSSID
        Return TR064Start(ServiceFile, "GetSSID", ServiceID, Nothing).TryGetValueEx("NewSSID", SSID)
    End Function

    Public Function SetSSID(SSID As String) As Boolean Implements IWlanconfigSCPD.SetSSID
        Return Not TR064Start(ServiceFile, "SetSSID", ServiceID,
                              New Dictionary(Of String, String) From {{"NewSSID", SSID}}).ContainsKey("Error")
    End Function

    Public Function GetBeaconType(ByRef BeaconType As String, ByRef PossibleBeaconTypes() As String) As Boolean Implements IWlanconfigSCPD.GetBeaconType
        With TR064Start(ServiceFile, "GetBeaconType", ServiceID, Nothing)

            If .ContainsKey("NewX_AVM-DE_PossibleBeaconTypes") Then
                PossibleBeaconTypes = .Item("NewX_AVM-DE_PossibleBeaconTypes").Split(","c)
                Return .TryGetValueEx("NewBeaconType", BeaconType)
            Else
                Return False
            End If
        End With
    End Function

    Public Function SetBeaconType(BeaconType As String) As Boolean Implements IWlanconfigSCPD.SetBeaconType
        Return Not TR064Start(ServiceFile, "SetBeaconType", ServiceID,
                              New Dictionary(Of String, String) From {{"NewBeaconType", BeaconType}}).ContainsKey("Error")
    End Function

    Public Function GetChannelInfo(ByRef Channel As Integer, ByRef PossibleChannels As String, ByRef AutoChannelEnabled As Boolean, ByRef FrequencyBand As String) As Boolean Implements IWlanconfigSCPD.GetChannelInfo
        With TR064Start(ServiceFile, "GetChannelInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewChannel", Channel) And
                   .TryGetValueEx("NewPossibleChannels", PossibleChannels) And
                   .TryGetValueEx("NewX_AVM-DE_AutoChannelEnabled", AutoChannelEnabled) And
                   .TryGetValueEx("NewX_AVM-DE_FrequencyBand", FrequencyBand)

        End With
    End Function

    Public Function SetChannel(Channel As Integer) As Boolean Implements IWlanconfigSCPD.SetChannel
        Return Not TR064Start(ServiceFile, "SetChannel", ServiceID,
                              New Dictionary(Of String, String) From {{"NewChannel", Channel.ToString}}).ContainsKey("Error")
    End Function

    Public Function GetBeaconAdvertisement(ByRef BeaconAdvertisementEnabled As Boolean) As Boolean Implements IWlanconfigSCPD.GetBeaconAdvertisement
        Return TR064Start(ServiceFile, "GetBeaconAdvertisement", ServiceID, Nothing).TryGetValueEx("NewBeaconAdvertisementEnabled", BeaconAdvertisementEnabled)
    End Function

    Public Function SetBeaconAdvertisement(BeaconAdvertisementEnabled As Boolean) As Boolean Implements IWlanconfigSCPD.SetBeaconAdvertisement
        Return Not TR064Start(ServiceFile, "SetBeaconAdvertisement", ServiceID,
                              New Dictionary(Of String, String) From {{"NewBeaconAdvertisementEnabled", BeaconAdvertisementEnabled.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetTotalAssociations(ByRef TotalAssociations As Integer) As Boolean Implements IWlanconfigSCPD.GetTotalAssociations
        Return TR064Start(ServiceFile, "GetTotalAssociations", ServiceID, Nothing).TryGetValueEx("NewTotalAssociations", TotalAssociations)
    End Function

    Public Function GetGenericAssociatedDeviceInfo(DeviceIndex As Integer, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetGenericAssociatedDeviceInfo
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice With {.AssociatedDeviceIndex = DeviceIndex}

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", ServiceID,
                        New Dictionary(Of String, String) From {{"NewAssociatedDeviceIndex", DeviceIndex.ToString}})

            Return .TryGetValueEx("NewAssociatedDeviceMACAddress", DeviceInfo.AssociatedDeviceMACAddress) And
                   .TryGetValueEx("NewAssociatedDeviceIPAddress", DeviceInfo.AssociatedDeviceIPAddress) And
                   .TryGetValueEx("NewAssociatedDeviceAuthState", DeviceInfo.AssociatedDeviceAuthState) And
                   .TryGetValueEx("X_AVM-DE_Speed", DeviceInfo.Speed) And
                   .TryGetValueEx("X_AVM-DE_SignalStrength", DeviceInfo.SignalStrength) And
                   .TryGetValueEx("X_AVM-DE_ChannelWidth", DeviceInfo.ChannelWidth)

        End With
    End Function

    Public Function GetSpecificAssociatedDeviceInfo(AssociatedDeviceMACAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetSpecificAssociatedDeviceInfo
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", ServiceID,
                        New Dictionary(Of String, String) From {{"NewAssociatedDeviceMACAddress", AssociatedDeviceMACAddress}})

            DeviceInfo.AssociatedDeviceMACAddress = AssociatedDeviceMACAddress

            Return .TryGetValueEx("NewAssociatedDeviceMACAddress", DeviceInfo.AssociatedDeviceMACAddress) And
                   .TryGetValueEx("NewAssociatedDeviceIPAddress", DeviceInfo.AssociatedDeviceIPAddress) And
                   .TryGetValueEx("NewAssociatedDeviceAuthState", DeviceInfo.AssociatedDeviceAuthState) And
                   .TryGetValueEx("X_AVM-DE_Speed", DeviceInfo.Speed) And
                   .TryGetValueEx("X_AVM-DE_SignalStrength", DeviceInfo.SignalStrength) And
                   .TryGetValueEx("X_AVM-DE_ChannelWidth", DeviceInfo.ChannelWidth)

        End With
    End Function

    Public Function GetSpecificAssociatedDeviceInfoByIp(AssociatedDeviceIPAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean Implements IWlanconfigSCPD.GetSpecificAssociatedDeviceInfoByIp
        If DeviceInfo Is Nothing Then DeviceInfo = New AssociatedDevice

        With TR064Start(ServiceFile, "GetGenericAssociatedDeviceInfo", ServiceID,
                        New Dictionary(Of String, String) From {{"NewAssociatedDeviceIPAddress", AssociatedDeviceIPAddress}})

            DeviceInfo.AssociatedDeviceIPAddress = AssociatedDeviceIPAddress

            Return .TryGetValueEx("NewAssociatedDeviceMACAddress", DeviceInfo.AssociatedDeviceMACAddress) And
                   .TryGetValueEx("NewAssociatedDeviceAuthState", DeviceInfo.AssociatedDeviceAuthState) And
                   .TryGetValueEx("X_AVM-DE_Speed", DeviceInfo.Speed) And
                   .TryGetValueEx("X_AVM-DE_SignalStrength", DeviceInfo.SignalStrength) And
                   .TryGetValueEx("X_AVM-DE_ChannelWidth", DeviceInfo.ChannelWidth)
        End With
    End Function

    Public Function GetWLANDeviceListPath(ByRef WLANDeviceListPath As String) As Boolean Implements IWlanconfigSCPD.GetWLANDeviceListPath
        Return TR064Start(ServiceFile, "X_AVM-DE_GetWLANDeviceListPath", ServiceID, Nothing).TryGetValueEx("NewX_AVM-DE_WLANDeviceListPath", WLANDeviceListPath)
    End Function

    <Obsolete> Public Function GetWLANDeviceList(AssociatedDevices As WLANDeviceList) As Boolean Implements IWlanconfigSCPD.GetWLANDeviceList
        Dim LuaPath As String = String.Empty
        Return GetWLANDeviceListPath(LuaPath) AndAlso XML.Deserialize($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{LuaPath}", True, AssociatedDevices)
    End Function

    Public Async Function GetWLANDeviceList() As Task(Of WLANDeviceList) Implements IWlanconfigSCPD.GetWLANDeviceList
        ' Ermittle den Pfad zu AssociatedDevices und deserialisiere die Daten
        Dim WLANDeviceListUrl As String = String.Empty

        If GetWLANDeviceListPath(WLANDeviceListUrl) Then
            ' X_AVM-DE_GetWLANDeviceListPath liefert nur den lua-Part. Der Rest muss vorangefügt werden.
            Return Await XML.DeserializeAsyncFromPath(Of WLANDeviceList)($"{Uri.UriSchemeHttp}://{FritzBoxTR64.FBoxIPAdresse}:{49000}{WLANDeviceListUrl}")
        Else
            Return New WLANDeviceList
        End If
    End Function

    Public Function SetStickSurfEnable(StickSurfEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetStickSurfEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetStickSurfEnable", ServiceID,
                              New Dictionary(Of String, String) From {{"NewStickSurfEnable", StickSurfEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetIPTVOptimized(ByRef IPTVoptimize As Boolean) As Boolean Implements IWlanconfigSCPD.GetIPTVOptimized
        Return TR064Start(ServiceFile, "X_AVM-DE_GetIPTVOptimized", ServiceID, Nothing).TryGetValueEx("NewX_AVM-DE_IPTVoptimize", IPTVoptimize)
    End Function

    Public Function SetIPTVOptimized(IPTVoptimize As Boolean) As Boolean Implements IWlanconfigSCPD.SetIPTVOptimized
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetIPTVOptimized", ServiceID,
                              New Dictionary(Of String, String) From {{"NewX_AVM-DE_IPTVoptimize", IPTVoptimize.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean Implements IWlanconfigSCPD.GetStatistics
        With TR064Start(ServiceFile, "GetStatistics", ServiceID, Nothing)

            Return .TryGetValueEx("NewTotalPacketsSent", TotalPacketsSent) And
                   .TryGetValueEx("NewTotalPacketsReceived", TotalPacketsReceived)

        End With
    End Function

    Public Function GetPacketStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean Implements IWlanconfigSCPD.GetPacketStatistics
        With TR064Start(ServiceFile, "GetPacketStatistics", ServiceID, Nothing)

            Return .TryGetValueEx("NewTotalPacketsSent", TotalPacketsSent) And
                   .TryGetValueEx("NewTotalPacketsReceived", TotalPacketsReceived)

        End With
    End Function

    Public Function GetNightControl(ByRef NightControl As String, ByRef NightTimeControlNoForcedOff As Boolean) As Boolean Implements IWlanconfigSCPD.GetNightControl
        With TR064Start(ServiceFile, "X_AVM-DE_GetNightControl", ServiceID, Nothing)

            Return .TryGetValueEx("NewNightControl", NightControl) And
                   .TryGetValueEx("NewNightTimeControlNoForcedOff", NightTimeControlNoForcedOff)

        End With
    End Function

    Public Function SetHighFrequencyBand(EnableHighFrequency As Boolean) As Boolean Implements IWlanconfigSCPD.SetHighFrequencyBand
        Return Not TR064Start(ServiceFile, "X_SetHighFrequencyBand", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnableHighFrequency", EnableHighFrequency.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetWLANHybridMode(ByRef Info As WLANHybridMode) As Boolean Implements IWlanconfigSCPD.GetWLANHybridMode
        If Info Is Nothing Then Info = New WLANHybridMode

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANHybridMode", ServiceID, Nothing)

            Return .TryGetValueEx("NewEnable", Info.Enable) And
                   .TryGetValueEx("NewBeaconType", Info.BeaconType) And
                   .TryGetValueEx("NewKeyPassphrase", Info.KeyPassphrase) And
                   .TryGetValueEx("NewSSID", Info.SSID) And
                   .TryGetValueEx("NewBSSID", Info.BSSID) And
                   .TryGetValueEx("NewTrafficMode", Info.TrafficMode) And
                   .TryGetValueEx("NewManualSpeed", Info.ManualSpeed) And
                   .TryGetValueEx("NewMaxSpeedDS", Info.MaxSpeedDS) And
                   .TryGetValueEx("NewMaxSpeedUS", Info.MaxSpeedUS)

        End With
    End Function

    Public Function SetWLANHybridMode(Info As WLANHybridMode) As Boolean Implements IWlanconfigSCPD.SetWLANHybridMode
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWLANHybridMode", ServiceID,
                              New Dictionary(Of String, String) From {{"NewEnable", Info.Enable.ToBoolStr},
                                                                      {"NewBeaconType", Info.BeaconType},
                                                                      {"NewKeyPassphrase", Info.KeyPassphrase},
                                                                      {"NewSSID", Info.SSID},
                                                                      {"NewBSSID", Info.BSSID},
                                                                      {"NewTrafficMode", Info.TrafficMode},
                                                                      {"NewManualSpeed", Info.ManualSpeed.ToBoolStr},
                                                                      {"NewMaxSpeedDS", Info.MaxSpeedDS.ToString},
                                                                      {"NewMaxSpeedUS", Info.MaxSpeedUS.ToString}}).ContainsKey("Error")

    End Function

    Public Function GetWLANExtInfo(ByRef Info As WLANExtInfo) As Boolean Implements IWlanconfigSCPD.GetWLANExtInfo
        If Info Is Nothing Then Info = New WLANExtInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANExtInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_APEnabled", Info.APEnabled) And
                   .TryGetValueEx("NewX_AVM-DE_APType", Info.APType) And
                   .TryGetValueEx("NewX_AVM-DE_FrequencyBand", Info.FrequencyBand) And
                   .TryGetValueEx("NewX_AVM-DE_TimeoutActive", Info.TimeoutActive) And
                   .TryGetValueEx("NewX_AVM-DE_Timeout", Info.Timeout) And
                   .TryGetValueEx("NewX_AVM-DE_TimeRemain", Info.TimeRemain) And
                   .TryGetValueEx("NewX_AVM-DE_NoForcedOff", Info.NoForcedOff) And
                   .TryGetValueEx("NewX_AVM-DE_UserIsolation", Info.UserIsolation) And
                   .TryGetValueEx("NewX_AVM-DE_EncryptionMode", Info.EncryptionMode) And
                   .TryGetValueEx("NewX_AVM-DE_LastChangedStamp", Info.LastChangedStamp)

        End With
    End Function

    Public Function SetWLANGlobalEnable(WLANGlobalEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetWLANGlobalEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWLANGlobalEnable", ServiceID, New Dictionary(Of String, String) From {{"NewX_AVM-DE_WLANGlobalEnable", WLANGlobalEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetWPSInfo(ByRef WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean Implements IWlanconfigSCPD.GetWPSInfo
        With TR064Start(ServiceFile, "X_AVM-DE_GetWPSInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewX_AVM-DE_WPSMode", WPSMode) And
                   .TryGetValueEx("NewX_AVM-DE_WPSStatus", WPSStatus)

        End With
    End Function

    Public Function SetWPSConfig(WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean Implements IWlanconfigSCPD.SetWPSConfig
        Return TR064Start(ServiceFile, "X_AVM-DE_GetWPSInfo", ServiceID,
                          New Dictionary(Of String, String) From {{"NewX_AVM-DE_WPSMode", WPSMode.ToString}}).
                          TryGetValueEx("NewX_AVM-DE_WPSStatus", WPSStatus)

    End Function

    Public Function SetWPSEnable(WPSEnable As Boolean) As Boolean Implements IWlanconfigSCPD.SetWPSEnable
        Return Not TR064Start(ServiceFile, "X_AVM-DE_SetWPSEnable", ServiceID, New Dictionary(Of String, String) From {{"NewX_AVM-DE_WPSEnable", WPSEnable.ToBoolStr}}).ContainsKey("Error")
    End Function

    Public Function GetWLANConnectionInfo(ByRef Info As WLANConnectionInfo) As Boolean Implements IWlanconfigSCPD.GetWLANConnectionInfo
        If Info Is Nothing Then Info = New WLANConnectionInfo

        With TR064Start(ServiceFile, "X_AVM-DE_GetWLANConnectionInfo", ServiceID, Nothing)

            Return .TryGetValueEx("NewAssociatedDeviceMACAddress", Info.AssociatedDeviceMACAddress) And
                   .TryGetValueEx("NewSSID", Info.SSID) And
                   .TryGetValueEx("NewBSSID", Info.BSSID) And
                   .TryGetValueEx("NewBeaconType", Info.BeaconType) And
                   .TryGetValueEx("NewChannel", Info.Channel) And
                   .TryGetValueEx("NewStandard", Info.Standard) And
                   .TryGetValueEx("NewX_AVM-DE_AutoChannelEnabled", Info.AutoChannelEnabled) And
                   .TryGetValueEx("NewX_AVM-DE_ChannelWidth", Info.ChannelWidth) And
                   .TryGetValueEx("NewX_AVM-DE_FrequencyBand", Info.FrequencyBand) And
                   .TryGetValueEx("NewX_AVM-DE_SignalStrength", Info.SignalStrength) And
                   .TryGetValueEx("NewX_AVM-DE_Speed", Info.Speed) And
                   .TryGetValueEx("NewX_AVM-DE_SpeedRX", Info.SpeedRX) And
                   .TryGetValueEx("NewX_AVM-DE_SpeedMax", Info.SpeedMax) And
                   .TryGetValueEx("NewX_AVM-DE_SpeedRXMax", Info.SpeedRXMax)

        End With
    End Function

End Class
