Imports System.ComponentModel
Imports System.Xml.Serialization

Public Enum AVMErrorCodes
    InvalidArguments = 402
    ActionFailed = 501
    ArgumentValueIsInvalid = 600
    DeviceIsOutOfMemory = 603
    NotAuthorized = 606
    InvalidArrayIndex = 713
    NoSuchArrayEntryInArray = 714
    StringTooShort = 801
    StringTooLong = 802
    ArgumentContainsInvalidCharacters = 803
    InternalError = 820
    AppIDIsTooShort = 821
    AppIDIsTooLong = 822
    AppIDContainsInvalidCharacters = 823
    AppDisplayIsTooShort = 824
    AppDisplayIsTooLong = 825
    AppDisplayContainsInvalidCharacters = 826
    InvalidMACAddress = 827
    AAppUserNameIsTooShort = 828
    AAppUserNameIsTooLong = 829
    AppUserNameContainsInvalidCharacters = 830
    AppUserNameAlreadyExists = 831
    AppPasswordIsTooShort = 832
    AppPasswordIsTooLong = 833
    AppPasswordContainsInvalidCharacters = 834
    MaximumNumbersOfAppsHasBeenReached = 835
    NoAllowedRightValue = 836
    SecurityContext = 837
    AppMayNotRegisterApp = 838
    AppMayNotRegisterAppFromInternet = 839
    AppNoRights = 840
    ''' <summary>
    ''' Does not have to be interpreted as an error. The app instance is created. But HTTPS is inactive.
    ''' </summary>
    EnableHTTPSFailedByRegisterApp = 841
    AuthenticationRequired = 866
    AuthenticationBlocked = 867
    AuthenticationBusy = 868
End Enum

''' <summary>
''' ServiceControlProtocolDefinitions 
''' </summary>
Public Enum SCPDFiles

    '<Description("/any.xml")> any

    <Description("/aura.xml")> auradesc

    <Description("/aura-scpd.xml")> auraSCPD

    <Description("/deviceconfigSCPD.xml")> deviceconfigSCPD

    <Description("/deviceinfoSCPD.xml")> deviceinfoSCPD

    <Description("/ethifconfigSCPD.xml")> ethifconfigSCPD

    <Description("/hostsSCPD.xml")> hostsSCPD

    '<Description("/igdconnSCPD.xml")> igdconnSCPD

    '<Description("/igddesc.xml")> igddesc

    '<Description("/igddslSCPD.xml")> igddslSCPD

    '<Description("/igdicfgSCPD.xml")> igdicfgSCPD

    <Description("/lanconfigsecuritySCPD.xml")> lanconfigsecuritySCPD

    <Description("/lanhostconfigmgmSCPD.xml")> lanhostconfigmgmSCPD

    <Description("/lanifconfigSCPD.xml")> lanifconfigSCPD

    <Description("/layer3forwardingSCPD.xml")> layer3forwardingSCPD

    <Description("/mgmsrvSCPD.xml")> mgmsrvSCPD

    <Description("/timeSCPD.xml")> timeSCPD

    <Description("/tr64desc.xml")> tr64desc

    <Description("/userifSCPD.xml")> userifSCPD

    <Description("/wancommonifconfigSCPD.xml")> wancommonifconfigSCPD

    <Description("/wanethlinkconfigSCPD.xml")> wanethlinkconfigSCPD

    <Description("/wandslifconfigSCPD.xml")> wandslifconfigSCPD

    <Description("/wandsllinkconfigSCPD.xml")> wandsllinkconfigSCPD

    <Description("/wanipconnSCPD.xml")> wanipconnSCPD

    <Description("/wanpppconnSCPD.xml")> wanpppconnSCPD

    <Description("/wlanconfigSCPD.xml")> wlanconfigSCPD

    <Description("/x_appsetupSCPD.xml")> x_appsetupSCPD

    <Description("/x_authSCPD.xml")> x_authSCPD

    ''' <summary>
    ''' X_AVM-DE_OnTel
    ''' </summary>
    <Description("/x_contactSCPD.xml")> x_contactSCPD

    <Description("/x_dectSCPD.xml")> x_dectSCPD

    <Description("/x_filelinksSCPD.xml")> x_filelinksSCPD

    <Description("/x_homeautoSCPD.xml")> x_homeautoSCPD

    <Description("/x_homeplugSCPD.xml")> x_homeplugSCPD

    <Description("/x_hostfilterSCPD.xml")> x_hostfilterSCPD

    <Description("/x_mediaSCPD.xml")> x_mediaSCPD

    <Description("/x_myfritzSCPD.xml")> x_myfritzSCPD

    <Description("/x_remoteSCPD.xml")> x_remoteSCPD

    <Description("/x_storageSCPD.xml")> x_storageSCPD

    <Description("/x_speedtestSCPD.xml")> x_speedtestSCPD

    <Description("/x_tamSCPD.xml")> x_tamSCPD

    <Description("/x_upnpSCPD.xml")> x_upnpSCPD

    <Description("/x_uspcontrollerSCPD.xml")> x_uspcontroller

    <Description("/x_voipSCPD.xml")> x_voipSCPD

    <Description("/x_wanmobileconnSCPD.xml")> x_wanmobileconn

    <Description("/x_webdavSCPD.xml")> x_webdavSCPD

End Enum

#Region "Telefonbuch"
Public Enum TelNrTypEnum
    <XmlEnum("")> notset

    <XmlEnum("intern")> intern

    <XmlEnum("work")> work

    <XmlEnum("home")> home

    <XmlEnum("mobile")> mobile

    <XmlEnum("fax_work")> fax_work

    <XmlEnum("memo")> memo

    <XmlEnum("other")> other

    ' Das AVM Telefonbuch nimmt es mit der Groß- und Kleinschreibung nicht so genau.
    ' Für die XML - Deserialsierung ist dies aber extrem wichtig.

End Enum

Public Enum EMailTypEnum

    <XmlEnum("")> notset

    <XmlEnum("private")> [private]

    <XmlEnum("work")> work

    <XmlEnum("other")> other

    ' Das AVM Telefonbuch nimmt es mit der Groß- und Kleinschreibung nicht so genau.
    ' Für die XML - Deserialsierung ist dies aber extrem wichtig.

End Enum
#End Region

''' <summary>
''' Fritz!Box Deflection
''' </summary>
Public Enum DeflectionModeEnum
    ''' <summary>
    ''' Deflect if a bell blockade is activ
    ''' </summary>
    <XmlEnum> eBellBlockade

    ''' <summary>
    ''' Busy
    ''' </summary>
    <XmlEnum> eBusy

    ''' <summary>
    ''' Deflect with a delay
    ''' </summary>
    <XmlEnum> eDelayed

    ''' <summary>
    ''' Deflect if busy or with a delay
    ''' </summary>
    <XmlEnum> eDelayedOrBusy

    ''' <summary>
    ''' Direct call
    ''' </summary>
    <XmlEnum> eDirectCall

    ''' <summary>
    ''' Deflect immediately
    ''' </summary>
    <XmlEnum> eImmediately

    ''' <summary>
    ''' Deflect with a long delay
    ''' </summary>
    <XmlEnum> eLongDelayed

    ''' <summary>
    ''' Do not signal this call
    ''' </summary>
    <XmlEnum> eNoSignal

    ''' <summary>
    ''' Deflect disabled
    ''' </summary>
    <XmlEnum> eOff

    ''' <summary>
    ''' Parallel call
    ''' </summary>
    <XmlEnum> eParallelCall

    ''' <summary>
    ''' Deflect with a short delay
    ''' </summary>
    <XmlEnum> eShortDelayed

    ''' <summary>
    ''' Mode unknown
    ''' </summary>
    <XmlEnum> eUnknown

    ''' <summary>
    ''' VIP
    ''' </summary>
    <XmlEnum> eVIP
End Enum

''' <summary>
''' Fritz!Box Deflection
''' </summary>
Public Enum DeflectionTypeEnum
    ''' <summary>
    ''' Phone port 1 is selected
    ''' </summary>
    <XmlEnum> fon1

    ''' <summary>
    ''' Phone port 2 is selected
    ''' </summary>
    <XmlEnum> fon2

    ''' <summary>
    ''' Phone port 3 is selected
    ''' </summary>
    <XmlEnum> fon3

    ''' <summary>
    ''' Phone port 4 is selected
    ''' </summary>
    <XmlEnum> fon4

    ''' <summary>
    ''' From all
    ''' </summary>
    <XmlEnum> fromAll

    ''' <summary>
    ''' From a anonymous call 
    ''' </summary>
    <XmlEnum> fromAnonymous

    ''' <summary>
    ''' The caller is not in the phonebook 
    ''' </summary>
    <XmlEnum> fromNotInPhonebook

    ''' <summary>
    ''' Call not from a VIP (obsolate from Version 37)
    ''' </summary>
    <Obsolete("Obsolate from Version 37")> <XmlEnum> fromNotVIP

    ''' <summary>
    ''' Specific Number 
    ''' </summary>
    <XmlEnum> fromNumber

    ''' <summary>
    ''' The caller is in the phonebook
    ''' </summary>
    <XmlEnum> fromPB

    ''' <summary>
    ''' Call from a VIP
    ''' </summary>
    <XmlEnum> fromVIP

    ''' <summary>
    ''' To Any
    ''' </summary>
    <XmlEnum> toAny

    ''' <summary>
    ''' To MSN
    ''' </summary>
    <XmlEnum> toMSN

    ''' <summary>
    ''' To POTS
    ''' </summary>
    <XmlEnum> toPOTS

    ''' <summary>
    ''' To VoIP
    ''' </summary>
    <XmlEnum> toVoIP

    ''' <summary>
    ''' Type unknown
    ''' </summary>
    <XmlEnum> unknown
End Enum

Public Enum SIPTypeEnum
    eAllCalls
    eGSM
    eISDN
    eNone
    ePOTS
    eVoIP
End Enum

Public Enum SupportDataModeEnum
    normal
    mesh
    unknown
End Enum

Public Enum SupportDataStatusEnum
    unknown
    ok
    preparing
    [error]
    creating
End Enum

Public Enum VoiceCodingEnum
    ''' <summary>
    ''' always use POTS quality (default value) 
    ''' </summary>
    fixed
    ''' <summary>
    ''' automatic audio codec selection
    ''' </summary>
    auto
    ''' <summary>
    ''' always use audio codec with compression
    ''' </summary>
    compressed
    ''' <summary>
    ''' automatic use of compressed audio codec
    ''' </summary>
    autocompressed
End Enum

Public Enum PhysicalLinkStatusEnum
    Unavailable
    Down
    Initializing
    Up
End Enum

Public Enum ATMEncapsulationEnum
    LLC
    VCMUX
End Enum

Public Enum UpdateEnum
    ''' <summary>
    ''' The update state is unknown
    ''' </summary>
    unknown

    ''' <summary>
    ''' Update for the device failed
    ''' </summary>
    failed

    ''' <summary>
    ''' Update for the device was successful 
    ''' </summary>
    succeeded
End Enum

#Region "HomeAuto"
Public Enum EnabledEnum
    ''' <summary>
    ''' Feature not supported
    ''' </summary> 
    DISABLED

    ''' <summary>
    ''' Feature supported
    ''' </summary>
    ENABLED

    ''' <summary>
    ''' Feature undefined
    ''' </summary>
    UNDEFINED
End Enum
Public Enum PresentEnum
    ''' <summary>
    ''' Device is disconnected
    ''' </summary> 
    DISCONNECTED

    ''' <summary>
    ''' Device is registered
    ''' </summary>
    REGISTERED

    ''' <summary>
    ''' Device is connected
    ''' </summary>
    CONNECTED

    ''' <summary>
    ''' unknown
    ''' </summary>
    UNKNOWN
End Enum
Public Enum SwModeEnum
    ''' <summary>
    ''' Automatic timer
    ''' </summary>
    AUTO
    ''' <summary>
    ''' Undefined timer
    ''' </summary>
    MANUAL
    ''' <summary>
    ''' Undefined timer
    ''' </summary>
    UNDEFINED
End Enum
Public Enum SwStateEnum
    ''' <summary>
    ''' Switch o
    ''' </summary>
    OFF
    ''' <summary>
    ''' Switch On
    ''' </summary>
    [ON]
    ''' <summary>
    ''' Toggle switch state
    ''' </summary>
    TOGGLE
    ''' <summary>
    ''' Undefined switch state
    ''' </summary>
    UNDEFINED
End Enum
Public Enum ValidEnum
    ''' <summary>
    ''' Invalid value
    ''' </summary>
    INVALID
    ''' <summary>
    ''' Valid value
    ''' </summary>
    VALID
    ''' <summary>
    ''' Undefined value
    ''' </summary>
    UNDEFINED
End Enum
Public Enum VentilEnum
    ''' <summary>
    ''' Valve closed
    ''' </summary>
    CLOSED
    ''' <summary>
    ''' Valve opened
    ''' </summary>
    OPEN
    ''' <summary>
    ''' Valve temperature controlled
    ''' </summary>
    TEMP
End Enum
#End Region

#Region "WLANConfiguration"
Public Enum WPSModeEnum
    ''' <summary>
    ''' Push Button Configuration
    ''' </summary>
    pbc

    ''' <summary>
    ''' Stop running WPS session
    ''' </summary>
    [stop]

    ''' <summary>
    ''' WPS disabled or unknown WPS mode 
    ''' </summary>
    other
End Enum

Public Enum WPSStatusEnum
    off
    inactive
    active
    success
    err_common
    err_timeout
    err_reconfig
    err_internal
    err_abort
End Enum

#End Region

#Region "Hostfilter"
Public Enum TicketIDStatusEnum
    ''' <summary>
    ''' The TicketID may be already used or was never valid.
    ''' </summary>
    invalid

    ''' <summary>
    ''' The TicketID is not retrieved via action MarkTicket nor marked via WebGUI.
    ''' </summary>
    unmarked

    ''' <summary>
    ''' The TicketID is retrieved via action MarkTicket or marked via WebGUI and not used.
    ''' </summary>
    marked
End Enum

Public Enum WANAccessEnum
    ''' <summary>
    ''' The LAN device has access to WAN.
    ''' </summary>
    granted

    ''' <summary>
    ''' The LAN device has no access to WAN.
    ''' </summary>
    denied

    ''' <summary>
    ''' Something went wrong, the state could not yet be retrieved.
    ''' </summary>
    [error]
End Enum

#End Region

#Region "UserInterface"
Public Enum AutoUpdateModeEnum
    off
    all
    important
    check
End Enum

Public Enum UpdateStateEnum
    Started
    Stopped
    [Error]
    NoUpdate
    UpdateAvailable
    Unknown
End Enum

Public Enum BuildTypeEnum
    Release
    Intern
    Work
    Personal
    Modified
    Inhaus
    Labor_Beta
    Labor_RC
    Labor_DSL
    Labor_Phone
    Labor
    Labor_Test
    Labor_Plus
End Enum

Public Enum UpdateSuccessfulEnum
    unknown
    failed
    succeeded
End Enum
#End Region

#Region "WAN"
Public Enum ConnectionStatusEnum
    Unconfigured
    Connecting
    Authenticating
    PendingDisconnect
    Disconnecting
    Disconnected
    Connected
End Enum

Public Enum ConnectionTypeEnum

    ''' <summary>
    ''' Valid connection types cannot be identified. This may be due to the fact that the LinkType variable (if specified in the WAN*LinkConfig service) is uninitialized. 
    ''' </summary>
    Unconfigured

    ''' <summary>
    ''' The Internet Gateway is an IP router between the LAN and the WAN connection.
    ''' </summary>
    IP_Routed

    ''' <summary>
    ''' The Internet Gateway is an Ethernet bridge between the LAN and the WAN connection. A router at the other end of the WAN connection from the IGD routes IP packets.
    ''' </summary>
    IP_Bridged
End Enum
Public Enum PortMappingProtocolEnum
    UDP
    TCP
End Enum

Public Enum LastConnectionErrorEnum
    ERROR_NONE
    ERROR_ISP_TIME_OUT
    ERROR_COMMAND_ABORTED
    ERROR_NOT_ENABLED_FOR_INTERNET
    ERROR_BAD_PHONE_NUMBER
    ERROR_USER_DISCONNECT
    ERROR_ISP_DISCONNECT
    ERROR_IDLE_DISCONNECT
    ERROR_FORCED_DISCONNECT
    ERROR_SERVER_OUT_OF_RESOURCES
    ERROR_RESTRICTED_LOGON_HOURS
    ERROR_ACCOUNT_DISABLED
    ERROR_ACCOUNT_EXPIRED
    ERROR_PASSWORD_EXPIRED
    ERROR_AUTHENTICATION_FAILURE
    ERROR_NO_DIALTONE
    ERROR_NO_CARRIER
    ERROR_NO_ANSWER
    ERROR_LINE_BUSY
    ERROR_UNSUPPORTED_BITSPERSECOND
    ERROR_TOO_MANY_LINE_ERRORS
    ERROR_IP_CONFIGURATION
    ERROR_UNKNOWN
End Enum

Public Enum EthernetLinkStatusEnum
    Up
    Down
    Unavailable
End Enum

Public Enum LinkTypeEnum
    EoA
    PPPoA
    PPPoE
    Unconfigured
End Enum

Public Enum LinkStatusEnum
    Up
    Down
    Initializing
    Unavailable
End Enum

Public Enum DigagnoseStateEnum
    NONE
    NO_CALIB
    RUNNING
    DONE
    DONE_CABLE_NOK
    DONE_CABLE_OK
End Enum

#End Region

Public Enum EnableEnum
    Enable
    Disable
    [Error]
End Enum

#Region "Remote"
Public Enum RemoteModeEnum
    ''' <summary>
    ''' Update only IPv4 address 
    ''' </summary>
    ddns_v4

    ''' <summary>
    ''' Update only IPv6 address
    ''' </summary>
    ddns_v6

    ''' <summary>
    ''' Update IPv4 and IPv6 address with separate HTTP requests 
    ''' </summary>
    ddns_both

    ''' <summary>
    ''' Update IPv4 and IPv6 address with one request 
    ''' </summary>
    ddns_both_together
End Enum

#End Region

#Region "AppSetup"
Public Enum RightEnum
    ''' <summary>
    ''' Read and write access 
    ''' </summary>
    RW

    ''' <summary>
    ''' Read only access 
    ''' </summary>
    RO

    ''' <summary>
    ''' No access allowed 
    ''' </summary>
    NO

    ''' <summary>
    ''' No specific right defined. 
    ''' </summary>
    UNDEFINED
End Enum
#End Region

#Region "Authentication"
Public Enum AuthStateEnum
    ''' <summary>
    ''' second factor authentication disabled by configuration
    ''' </summary>
    disabled

    ''' <summary>
    ''' second factor authentication waiting for user interaction to authenticate
    ''' </summary>
    waitingforauth

    ''' <summary>
    ''' second factor authentication running for another user 
    ''' </summary>
    anotherauthprocess

    ''' <summary>
    ''' second factor authentication granted for current user
    ''' </summary>
    authenticated

    ''' <summary>
    ''' second factor authentication stopped and not authenticated
    ''' </summary>
    stopped

    ''' <summary>
    ''' too many tries (limit reached)
    ''' </summary>
    blocked

    ''' <summary>
    ''' internal error occurred 
    ''' </summary>
    failure
End Enum

Public Enum AuthActionEnum
    ''' <summary>
    ''' Start second factor authentication process 
    ''' </summary>
    start

    ''' <summary>
    ''' Stop a started second factor authentication process
    ''' Possible if State has value waitingforauth or authenticated 
    ''' </summary>
    [stop]
End Enum
#End Region

#Region "AHA"
Public Enum HAN_FUN_UnitType
    <XmlEnum("SIMPLE_BUTTON")> SIMPLE_BUTTON = 273
    <XmlEnum("SIMPLE_ON_OFF_SWITCHABLE")> SIMPLE_ON_OFF_SWITCHABLE = 256
    <XmlEnum("SIMPLE_ON_OFF_SWITCH")> SIMPLE_ON_OFF_SWITCH = 257
    <XmlEnum("AC_OUTLET")> AC_OUTLET = 262
    <XmlEnum("AC_OUTLET_SIMPLE_POWER_METERING")> AC_OUTLET_SIMPLE_POWER_METERING = 263
    <XmlEnum("SIMPLE_LIGHT")> SIMPLE_LIGHT = 264
    <XmlEnum("DIMMABLE_LIGHT")> DIMMABLE_LIGHT = 265
    <XmlEnum("DIMMER_SWITCH")> DIMMER_SWITCH = 266
    <XmlEnum("COLOR_BULB")> COLOR_BULB = 277
    <XmlEnum("DIMMABLE_COLOR_BULB")> DIMMABLE_COLOR_BULB = 278
    <XmlEnum("BLIND")> BLIND = 281
    <XmlEnum("LAMELLAR")> LAMELLAR = 282
    <XmlEnum("SIMPLE_DETECTOR")> SIMPLE_DETECTOR = 512
    <XmlEnum("DOOR_OPEN_CLOSE_DETECTOR")> DOOR_OPEN_CLOSE_DETECTOR = 513
    <XmlEnum("WINDOW_OPEN_CLOSE_DETECTOR")> WINDOW_OPEN_CLOSE_DETECTOR = 514
    <XmlEnum("MOTION_DETECTOR")> MOTION_DETECTOR = 515
    <XmlEnum("FLOOD_DETECTOR")> FLOOD_DETECTOR = 518
    <XmlEnum("GLAS_BREAK_DETECTOR")> GLAS_BREAK_DETECTOR = 519
    <XmlEnum("VIBRATION_DETECTOR")> VIBRATION_DETECTOR = 520
    <XmlEnum("SIREN")> SIREN = 640
End Enum

Public Enum HAN_FUN_Interfaces
    <XmlEnum("KEEP_ALIVE")> KEEP_ALIVE = 277
    <XmlEnum("ALERT")> ALERT = 256
    <XmlEnum("ON_OFF")> ON_OFF = 512
    <XmlEnum("LEVEL_CTRL")> LEVEL_CTRL = 513
    <XmlEnum("COLOR_CTRL")> COLOR_CTRL = 514
    <XmlEnum("OPEN_CLOSE")> OPEN_CLOSE = 516
    <XmlEnum("OPEN_CLOSE_CONFIG")> OPEN_CLOSE_CONFIG = 517
    <XmlEnum("SIMPLE_BUTTON")> SIMPLE_BUTTON = 772
    <XmlEnum("SUOTA-Update")> SUOTAUpdate = 1024
End Enum
#End Region

#Region "MyFritz"
Public Enum MyFritzStateEnum
    ''' <summary>
    ''' MyFritz is disabled (Enabled is 0) or the Fritz!Box is not registered
    ''' </summary>
    myfritz_disabled

    ''' <summary>
    ''' Failed to register Fritz!Box. Also the Fritz!Box retries to register
    ''' </summary>
    register_failed

    ''' <summary>
    ''' Fritz!Box will be unregistered
    ''' </summary>
    unregister

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS is unknown
    ''' </summary>
    dyndns_unknown

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS is active
    ''' </summary>
    dyndns_active

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update failed
    ''' </summary>
    dyndns_update_failed

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update failed (authentication error)
    ''' </summary>
    dyndns_auth_error

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update failed (server not reachable)
    ''' </summary>
    dyndns_server_unreachable

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update failed (server responded with error)
    ''' </summary>
    dyndns_server_error

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update failed (server responded with no adress update possible)
    ''' </summary>
    dyndns_server_update

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update succeed, not verified
    ''' </summary>
    dyndns_not_verified

    ''' <summary>
    ''' Fritz!Box is registered. DynDNS update succedd, verified
    ''' </summary>
    dyndns_verified

    ''' <summary>
    ''' This state is reserved but not in use
    ''' </summary>
    reserved

    ''' <summary>
    ''' Unknown state was responded
    ''' </summary>
    unknown
End Enum
#End Region

#Region "Media"
Public Enum StationSearchModeEnum
    start
    [stop]
End Enum

Public Enum StationSearchStatusEnum
    active
    inactive
End Enum
#End Region