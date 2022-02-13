Imports System.Xml

''' <summary>
''' <see href="link">https://stackoverflow.com/a/13538191</see>
''' </summary>
Public Class IgnoreNameSpaceXmlNodeReader
    Inherits XmlNodeReader

    Public Sub New(node As XmlNode)
        MyBase.New(node)
    End Sub

    Public Overrides ReadOnly Property NamespaceURI As String
        Get
            Return String.Empty
        End Get
    End Property
End Class
