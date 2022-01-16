Public Class DSLLinkInfo
    ''' <summary>
    ''' Returns always "true"
    ''' </summary>
    Public Property Enable As Boolean
    Public Property LinkStatus As LinkStatusEnum
    Public Property LinkType As LinkTypeEnum
    Public Property DestinationAddress As String
    Public Property ATMEncapsulation As String
    Public Property AutoConfig As Boolean
    Public Property ATMQoS As String
    Public Property ATMPeakCellRate As Integer
    Public Property ATMSustainableCellRate As Integer
End Class
