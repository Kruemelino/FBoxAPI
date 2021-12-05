''' <summary>
''' TR-064 Support – DeviceInfo
''' Date: 2009-7-15
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_dectSCPD.pdf</see>
''' </summary>
Public Interface IDECT_SCPD
    Inherits IServiceBase

    ''' <summary>
    ''' Returns the number of dect devices.
    ''' </summary>
    ''' <remarks>Required rights: AppRight or PhoneRight or HomeautoRight</remarks>
    Function GetNumberOfDectEntries(ByRef NumberOfEntries As Integer) As Boolean

    ''' <summary>
    ''' Read values/states for dect devices by index. 
    ''' </summary>
    ''' <param name="NumberOfEntries">Index can have a value from 0 .. NumberOfEntries (from <see cref="GetNumberOfDectEntries"/>).</param>
    ''' <remarks>Required rights: AppRight or PhoneRight or HomeautoRight</remarks>
    Function GetGenericDectEntry(ByRef GenericDectEntry As DectEntry, NumberOfEntries As Integer) As Boolean

    ''' <summary>
    ''' Read values/states for dect devices by ID. 
    ''' </summary>
    ''' <param name="ID">ID can have a value from 1 .. 6 for DECT handsets or 16 .. 415 for DECT ULE devices</param>
    ''' <remarks>Required rights: AppRight or PhoneRight or HomeautoRight</remarks>
    Function GetSpecificDectEntry(ByRef SpecificDectEntry As DectEntry, ID As String) As Boolean

    ''' <summary>
    ''' Trigger to start an update for a dect devices by ID
    ''' </summary>
    ''' <param name="ID">ID can have a value from 1 .. 6 for DECT handsets or 16 .. 415 for DECT ULE devices</param>
    ''' <remarks>Required rights: AppRight</remarks>
    Function DectDoUpdate(ByRef ID As String) As Boolean
End Interface
