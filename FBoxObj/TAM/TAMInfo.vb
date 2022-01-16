Public Class TAMInfo
    Public Property Enable As Boolean

    ''' <summary>
    ''' Publicly Name for TAM
    ''' </summary>
    Public Property Name As String

    ''' <summary>
    ''' 1 running <br/> 0 Not running 
    ''' </summary>
    Public Property TAMRunning As Boolean
    ''' <summary>
    ''' <list type="bullet">
    ''' <item>0 no USB memory stick</item>
    ''' <item>1 TAM already using USB memory stick</item>
    ''' <item>2 USB memory stick available but folder avm_tam missing</item>
    ''' </list>
    ''' </summary>
    Public Property Stick As Integer

    '''  <summary>
    ''' <list type="bullet">
    ''' <item>Bit 0: busy</item>
    ''' <item>Bit 1: no space left</item>
    ''' <item>Bit 15: Display in WebUI</item>
    ''' </list>
    ''' </summary>
    Public Property Status As Integer

    Public Property Capacity As Integer

    ''' <summary>
    ''' play_announcement, record_message, timeprofile 
    ''' </summary>
    Public Property Mode As String

    ''' <summary>
    ''' 0…255 <br/>
    ''' 0 immediately, 255 automatic
    ''' </summary>
    Public Property RingSeconds As Integer

    ''' <summary>
    ''' Empty string represents all numbers. <br/>
    ''' Comma separated list represents specific phone numbers
    ''' </summary>
    Public Property PhoneNumbers As String()

End Class