Public Class AlarmClock
    Friend Property AlarmClockEnable As Boolean
    Friend Property AlarmClockName As String

    ''' <summary>
    ''' "HHMM" H=hours, M= minutes, "0000"
    ''' </summary>
    Friend Property AlarmClockTime As String
    ''' <summary>
    ''' "MO", "TU”, "WE", "TH", "FR", "SA", "SU", "HO"
    ''' "MO,TU,WE,TH,FR,SA,SU,HO"
    ''' </summary>
    ''' <returns>Comma separated string of the possible values</returns>
    Friend Property AlarmClockWeekdays As String()
    Friend Property AlarmClockPhoneName As String
End Class
