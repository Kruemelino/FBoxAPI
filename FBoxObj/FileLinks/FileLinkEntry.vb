''' <summary>
''' TR-064 Support – X_AVM-DE_Filelinks
''' Date: 2016-07-07
''' <see href="link">https://avm.de/fileadmin/user_upload/Global/Service/Schnittstellen/x_filelinksSCPD.pdf</see>
''' </summary>
Imports System.Xml.Serialization
<Serializable()> Public Class FileLinkEntry
    ''' <summary>
    ''' sequential number for each file link. 
    ''' </summary>
    <XmlElement("Index")> Public Property Index As Integer

    ''' <summary>
    ''' Unique id for each file link
    ''' </summary>
    <XmlElement("ID")> Public Property ID As String

    ''' <summary>
    ''' Path to the file/directory in the storage hierarchy
    ''' </summary>
    <XmlElement("Path")> Public Property Path As String

    ''' <summary>
    ''' '1' file link is directory, '0' file link is file
    ''' </summary>
    <XmlElement("IsDirectory")> Public Property IsDirectory As Boolean

    ''' <summary>
    ''' file link
    ''' </summary>
    <XmlElement("Url")> Public Property Url As String

    ''' <summary>
    ''' Username of the creator of the file link
    ''' </summary>
    <XmlElement("Username")> Public Property Username As String

    ''' <summary>
    ''' Limit the hit count on a file 
    ''' </summary>
    <XmlElement("AccessCountLimit")> Public Property AccessCountLimit As Integer

    ''' <summary>
    ''' Counts the hits on a file
    ''' </summary>
    <XmlElement("AccessCount")> Public Property AccessCount As Integer

    ''' <summary>
    ''' Remaining days until expire date is reached
    ''' </summary>
    <XmlElement("Expire")> Public Property Expire As Integer

    ''' <summary>
    ''' Expiration date for the link. Formatted according to iso8601 : 0001-01-01T00:00:00
    ''' </summary>
    <XmlElement("ExpireDate")> Public Property ExpireDate As Date

    <XmlElement("Valid")> Public Property Valid As Boolean

End Class
