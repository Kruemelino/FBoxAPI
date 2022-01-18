Imports System.Xml.Serialization

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
    ''' Call not from a VIP 
    ''' </summary>
    <XmlEnum> fromNotVIP

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

Public Enum AccessTypeEnum
    DSL
    Ethernet
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

Public Enum UpdateStateEnum
    Started
    Stopped
    [Error]
    NoUpdate
    UpdateAvailable
    Unknown
End Enum

Public Enum AutoUpdateModeEnum
    off
    all
    important
    check
End Enum

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
    Unconfigured
    IP_Routed
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