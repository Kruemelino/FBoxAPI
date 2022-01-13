''' <summary>
''' TR-064 Support – WLANConfiguration
''' Date: 2020-11-02
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/wlanconfigSCPD.pdf</see>
''' </summary>
Public Interface IWlanconfigSCPD
    Inherits IServiceBase

    Function SetEnable(Enable As Boolean) As Boolean
    Function GetInfo(ByRef Info As WLANInfo) As Boolean
    Function SetConfig(Config As WLANInfo) As Boolean

    ''' <summary>
    ''' Since WEP is deprecated, the WEPKey variables are ignored.
    ''' </summary>
    Function SetSecurityKeys(PreSharedKey As String, KeyPassphrase As String) As Boolean

    ''' <summary>
    ''' Since WEP is deprecated, the WEPKey variables are always empty
    ''' </summary>
    ''' <returns></returns>
    Function GetSecurityKeys(ByRef PreSharedKey As String, ByRef KeyPassphrase As String) As Boolean
    Function SetBasBeaconSecurityProperties(BasicEncryptionModes As String, BasicAuthenticationMode As String) As Boolean
    Function GetBasBeaconSecurityProperties(ByRef BasicEncryptionModes As String, ByRef BasicAuthenticationMode As String) As Boolean
    Function GetBSSID(ByRef BSSID As String) As Boolean
    Function GetSSID(ByRef SSID As String) As Boolean
    Function SetSSID(SSID As String) As Boolean
    Function GetBeaconType(ByRef BeaconType As String, ByRef PossibleBeaconTypes As String()) As Boolean
    Function SetBeaconType(BeaconType As String) As Boolean
    Function GetChannelInfo(ByRef Channel As Integer, ByRef PossibleChannels As String) As Boolean
    Function SetChannel(Channel As Integer) As Boolean
    Function GetBeaconAdvertisement(ByRef BeaconAdvertisementEnabled As Boolean) As Boolean
    Function SetBeaconAdvertisement(BeaconAdvertisementEnabled As Boolean) As Boolean
    Function GetTotalAssociations(ByRef TotalAssociations As Integer) As Boolean
    Function GetGenericAssociatedDeviceInfo(DeviceIndex As Integer, ByRef DeviceInfo As AssociatedDevice) As Boolean
    Function GetSpecificAssociatedDeviceInfo(AssociatedDeviceMACAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean
    Function GetSpecificAssociatedDeviceInfoByIp(AssociatedDeviceIPAddress As String, ByRef DeviceInfo As AssociatedDevice) As Boolean
    ''' <summary>
    ''' Returns an URL path to fetch a WIFI device list of all connected devices as an XML document. 
    ''' </summary>
    ''' <remarks>Devices with AssociatedDeviceChannel set to 0 are not listed in this list, because these are not connected to any access point of this CPE.<br/>
    ''' The "TotalAssociations" list element shows the number of all connected WIFI devices which are listed in the file independent from the current access point or WLANConfiguration service.
    ''' </remarks>
    Function GetWLANDeviceListPath(ByRef WLANDeviceListPath As String) As Boolean

    ''' <summary>
    ''' Inoffizielle Action: X_AVM-DE_GetWLANDeviceListPath wird als <see cref="WLANDeviceList"/> deserialisiert zurückgegeben.
    ''' </summary>
    Function GetWLANDeviceList(AssociatedDevices As WLANDeviceList) As Boolean
    Function SetStickSurfEnable(StickSurfEnable As Boolean) As Boolean
    Function GetIPTVOptimized(ByRef IPTVoptimize As Boolean) As Boolean
    Function SetIPTVOptimized(IPTVoptimize As Boolean) As Boolean
    Function GetStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean
    Function GetPacketStatistics(ByRef TotalPacketsSent As Integer, ByRef TotalPacketsReceived As Integer) As Boolean
    Function GetNightControl(ByRef NightControl As String, ByRef NightTimeControlNoForcedOff As Boolean) As Boolean

    ''' <summary>
    ''' The action is listed in the WLAN configuration service but only supported on certain OEM devices.    
    ''' </summary>
    ''' <remarks>Please see WLAN configuration service 2 on FRITZ!Box 7390 for reading and writing 5 GHz WLAN settings.</remarks>
    Function SetHighFrequencyBand(EnableHighFrequency As Boolean) As Boolean

    ''' <summary>
    ''' This action delivers informations about the WLAN-Mode access.
    ''' </summary>
    Function GetWLANHybridMode(ByRef Info As WLANHybridMode) As Boolean

    ''' <summary>
    ''' This action sets informations about the WLAN-Mode access.
    ''' </summary>
    ''' <returns></returns>
    Function SetWLANHybridMode(Info As WLANHybridMode) As Boolean

    ''' <summary>
    ''' This action delivers informations about the WLAN-Guest access.
    ''' </summary>
    Function GetWLANExtInfo(ByRef Info As WLANExtInfo) As Boolean

    ''' <summary>
    ''' Enables “1” or disables “0” WLAN as if pushing the “WLAN” button on the CPE device.
    ''' </summary>
    Function SetWLANGlobalEnable(WLANGlobalEnable As Boolean) As Boolean

    Function GetWPSInfo(ByRef WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean

    ''' <summary>
    ''' WPS is supported with Push Button Configuration.<br/>
    ''' WPS is only supported for either access points or guest access points.<br/> 
    ''' WPS is not supported for both types of access points in parallel.
    ''' </summary>
    Function SetWPSConfig(WPSMode As WPSModeEnum, ByRef WPSStatus As WPSStatusEnum) As Boolean

    ''' <summary>
    ''' Enables “1” or disables “0” WPS.
    ''' </summary>
    ''' <remarks>If X_AVM-DE_WPSEnable is “1”, you may start the WPS mechanism with <see cref="SetWPSConfig"/></remarks>
    Function SetWPSEnable(WPSEnable As Boolean) As Boolean

    ''' <summary>
    ''' Get information about the Wi-Fi connection of the requesting Wi-Fi host device. 
    ''' If the device is not directly connected to any of the access points, the status code 714 (no such entry in array) will be returned.
    ''' </summary>
    Function GetWLANConnectionInfo(ByRef Info As WLANConnectionInfo) As Boolean
End Interface
