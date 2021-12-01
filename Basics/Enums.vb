Imports System.Xml.Serialization

Public Enum TelNrTyp
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

Public Enum EMailTyp

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
Public Enum DeflectionMode
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
Public Enum DeflectionType
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

Public Enum SIPType
    eAllCalls
    eGSM
    eISDN
    eNone
    ePOTS
    eVoIP
End Enum

Public Enum SupportDataMode
    normal
    mesh
    unknown
End Enum

Public Enum SupportDataStatus
    unknown
    ok
    preparing
    [error]
    creating
End Enum

Public Enum VoiceCoding
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

Public Enum PhysicalLinkStatus
    Unavailable
    Down
    Initializing
    Up
End Enum

Public Enum AccessType
    DSL
    Ethernet
End Enum