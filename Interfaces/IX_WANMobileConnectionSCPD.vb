''' <summary>
''' TR-064 Support – X_AVM-DE_WANMobileConnection
''' Date: 2022-11-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_wanmobileconnSCPD.pdf</see>
''' </summary>
Public Interface IX_WANMobileConnectionSCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Returns information about status from entering PIN/PUK and remaining tries. 
    ''' After entering PIN and/or PUK the status needs a few seconds to get updated.
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetInfo(ByRef Info As WANMobileConnectionInfo) As Boolean

    ''' <summary>
    ''' Returns additional information about mobile status
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetInfoEx(ByRef Info As WANMobileConnectionInfoEx) As Boolean

    ''' <summary>
    ''' Sets SIM card PIN. The status has to be "factory default", "unconfigured" or "enter PIN".
    ''' </summary>
    ''' <remarks>Success can be checked with GetInfo<br/>Required rights: App</remarks>
    Function SetPIN(PIN As String) As Boolean

    ''' <summary>
    ''' Sets SIM card PUK and PIN. The status has to be "factory default", "unconfigured" or "enter PUK", 
    ''' which appears after using all PIN tries (PINFailureCount = 0). The entered PIN will overwrite the existing PIN.
    ''' </summary>
    ''' <remarks>Success can be checked with GetInfo<br/>Required rights: App</remarks>
    Function SetPUK(PUK As String, PIN As String) As Boolean

    ''' <summary>
    ''' Sets the Radio Access Technologies (RATs), which should be enabled.
    ''' </summary>
    ''' <param name="AccessTechnology">"AUTO" or a comma separated list of all to enable RATs</param>
    ''' <remarks>Required rights: App</remarks>
    Function SetAccessTechnology(AccessTechnology As String) As Boolean

    ''' <summary>
    ''' Returns all enabled RATs, all possible RATs and the currently used RAT.
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetAccessTechnology(ByRef AccessTechnology As String,
                                 ByRef PossibleAccessTechnology As String,
                                 ByRef CurrentAccessTechnology As String) As Boolean

    ''' <summary>
    ''' Sets any number of bands for LTE, 5G-NSA and 5G-SA enabled. BandCapabilities(LTE, 5GNSA or 5GSA) is "0" for automatic mode or a comma separated list for specific bands
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function SetEnabledBandCapabilities(BandCapabilitiesLTE As String,
                                        BandCapabilities5GNSA As String,
                                        BandCapabilities5GSA As String) As Boolean

    ''' <summary>
    ''' Returns the currently enabled bands for LTE, 5G-NSA and 5G-SA as comma separated list. "0" is the automatic mode.
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetEnabledBandCapabilities(ByRef BandCapabilitiesLTE As String,
                                        ByRef BandCapabilities5GNSA As String,
                                        ByRef BandCapabilities5GSA As String) As Boolean

    ''' <summary>
    ''' Returns all available bands for LTE, 5G-NSA and 5G-SA as comma separated list. 
    ''' The value depends on the FRITZ!Box mobile module. "unknown" is returned, if the corresponding technology is not supported by the module.
    ''' </summary>
    ''' <remarks>Required rights: App</remarks>
    Function GetBandCapabilities(ByRef BandCapabilitiesLTE As String,
                                 ByRef BandCapabilities5GNSA As String,
                                 ByRef BandCapabilities5GSA As String) As Boolean
End Interface
