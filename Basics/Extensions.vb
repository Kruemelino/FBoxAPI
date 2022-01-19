Imports System.ComponentModel
Imports System.Runtime.CompilerServices

'
'<DebuggerStepThrough()>
Public Module Extensions

#Region "Extensions für Verarbeitung von Zahlen: Double, Integer, Long"

    ''' <summary>
    ''' Prüft, ob die übergebende Größe Null ist.
    ''' </summary>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsZero(Val1 As Integer) As Boolean
        Return Val1 = 0
    End Function

    ''' <summary>
    ''' Prüft, ob die übergebende Größe ungleich Null ist.
    ''' </summary>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsNotZero(Val1 As Integer) As Boolean
        Return Not Val1.IsZero
    End Function

    ''' <summary>
    ''' Prüft, ob die beiden übergebenen Größen gleich sind: <paramref name="Val1"/> == <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    <Extension()> Public Function AreEqual(Val1 As Integer, Val2 As Integer) As Boolean
        Return (Val1 - Val2).IsZero
    End Function


#End Region

#Region "Extensions für Verarbeitung von Zeichenfolgen: String"
    <Extension> Public Function AreEqual(Str1 As String, Str2 As String) As Boolean
        Return String.Compare(Str1, Str2).IsZero
    End Function
    <Extension> Public Function IsStringNothingOrEmpty(Str1 As String) As Boolean
        Return String.IsNullOrEmpty(Str1)
    End Function
    <Extension> Public Function IsNotStringNothingOrEmpty(Str1 As String) As Boolean
        Return Not Str1.IsStringNothingOrEmpty
    End Function

#End Region

#Region "Extensions für Verarbeitung von Boolean"
    ''' <summary>
    ''' Wandelt den Boolean-Wert in eine 1 für Wahr und eine 0 für Falsch
    ''' </summary>
    ''' <param name="Value">Umzuwandelnder Boolean-Wert</param>
    <Extension> Friend Function ToBoolStr(Value As Boolean) As String
        Return If(Value, "1", "0")
    End Function
#End Region

    ''' <summary>
    ''' Provides the Description Name for an enumeration based on the System.ComponentModel.Description Attribute on the enumeration value. if not found it returnes the defaultvalue passed.
    ''' Allows the description property of an enumeration to be exposed easily.
    ''' </summary>
    ''' <param name="EnumConstant">The Enumeration Item extended by the function.</param>
    <Extension()>
    Friend Function Description(EnumConstant As [Enum]) As String
        Dim fi As Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
        If aattr.Length = 0 OrElse aattr(0).Description.IsStringNothingOrEmpty Then
            Return String.Empty
        Else
            Return aattr(0).Description
        End If
    End Function

    <Extension()>
    Friend Function SerializeDictionary(dictionary As Dictionary(Of String, String), Name As String) As String
        Return $"{{ ""{Name}"" : {{ {String.Join(", ", dictionary.Select(Function(kvp) $"""{kvp.Key}"" : ""{kvp.Value}"""))} }}"
    End Function

    ''' <summary>
    ''' Eigene Funktion, die das GetKeyValue zumindest etwas typsicherer macht. Der Standardwert ist bei einem <see cref="Dictionary(Of String, String)"/> "". Dies kann jedoch nicht in Boolean umgewandelt werden.
    ''' </summary>
    <Extension()>
    Public Function TryGetValueEx(Of T)(dictionary As Dictionary(Of String, String), key As String, ByRef value As T) As Boolean
        ' Setze den Standardwert
        value = Nothing

        With dictionary
            ' prüfe, der Key vorhanden ist.
            If .ContainsKey(key) Then
                Try
                    ' Typumwandlung

                    ' Bei Boolean muss momentan ein Sonderweg gegangen werden.
                    ' "0" und "1" können nicht in Boolean umgewandelt werden.
                    If GetType(T) Is GetType(Boolean) AndAlso
                        (.Item(key).Equals("0") Or .Item(key).Equals("1")) Then
                        value = Convert.ChangeType(CInt(.Item(key)), GetType(T))
                    Else
                        value = Convert.ChangeType(.Item(key), GetType(T))
                    End If

                    ' Erfolg
                    Return True
                Catch ex As Exception
                    ' ToDo Log?
                End Try
            End If
        End With
        Return False
    End Function

End Module
