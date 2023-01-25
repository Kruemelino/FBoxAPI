Public Class OnlineMonitor

    ''' <summary>
    ''' 0 .. TotalNumberSyncGroups-1
    ''' </summary>
    Public Property SyncGroupIndex As Integer

    ''' <summary>
    ''' Number of sync groups
    ''' </summary>
    Public Property TotalNumberSyncGroups As Integer

    ''' <summary>
    ''' Name of sync group
    ''' </summary>
    Public Property SyncGroupName As String

    ''' <summary>
    ''' Connection mod of sync group
    ''' </summary>
    Public Property SyncGroupMode As String

    ''' <summary>
    ''' Max number of bytes per second in downstream direction
    ''' </summary>
    Public Property Max_ds As Integer

    ''' <summary>
    ''' Max number of bytes per second in upstream direction
    ''' </summary>
    Public Property Max_us As Integer

    ''' <summary>
    ''' current number of bytes per second in downstream direction of multicast traffic
    ''' </summary>
    Public Property Ds_current_bps As String

    ''' <summary>
    ''' current number of bytes per second in downstream direction of home, guest and multicast traffic
    ''' </summary>
    Public Property Mc_current_bps As String

    ''' <summary>
    ''' current number of bytes per second in upstream direction
    ''' </summary>
    Public Property Us_current_bps As String

    ''' <summary>
    ''' current number of bytes per second in upstream direction of real-time home traffic
    ''' </summary>
    Public Property Prio_realtime_bps As String

    ''' <summary>
    ''' current number of bytes per second in upstream direction of important home traffic
    ''' </summary>
    Public Property Prio_high_bps As String

    ''' <summary>
    ''' current number of bytes per second in upstream direction of default home traffic
    ''' </summary>
    Public Property Prio_default_bps As String

    ''' <summary>
    ''' current number of bytes per second in upstream direction of background home traffic
    ''' </summary>
    Public Property Prio_low_bps As String

End Class
