Imports System.ComponentModel
Imports System.Runtime.CompilerServices

<DebuggerStepThrough()>
Public Module Extensions

#Region "Extensions für Verarbeitung von Zahlen: Double, Integer, Long"
    Private Const Epsilon As Single = Single.Epsilon
    ''' <summary>
    ''' Gibt den Absolutwert der Zahlengröße zurück
    ''' </summary>
    <Extension()> Public Function Absolute(Val1 As Double) As Double
        Return Math.Abs(Val1)
    End Function
    <Extension()> Public Function Absolute(Val1 As Integer) As Integer
        Absolute = Math.Abs(Val1)
    End Function
    <Extension()> Public Function Absolute(Val1 As Long) As Long
        Absolute = Math.Abs(Val1)
    End Function

    ''' <summary>
    ''' Prüft, ob die übergebende Größe Null ist.
    ''' </summary>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsZero(Val1 As Double) As Boolean
        Return Val1.Absolute < Epsilon
    End Function
    <Extension()> Public Function IsZero(Val1 As Integer) As Boolean
        Return Val1.Absolute < Epsilon
    End Function
    <Extension()> Public Function IsZero(Val1 As Long) As Boolean
        Return Val1.Absolute < Epsilon
    End Function

    ''' <summary>
    ''' Prüft, ob die übergebende Größe ungleich Null ist.
    ''' </summary>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsNotZero(Val1 As Double) As Boolean
        Return Not Val1.IsZero
    End Function
    <Extension()> Public Function IsNotZero(Val1 As Integer) As Boolean
        Return Not Val1.IsZero
    End Function

    ''' <summary>
    ''' Prüft, ob die beiden übergebenen Größen gleich sind: <paramref name="Val1"/> == <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function AreEqual(Val1 As Double, Val2 As Double) As Boolean
        Return (Val1 - Val2).Absolute < Epsilon
    End Function
    <Extension()> Public Function AreEqual(Val1 As Integer, Val2 As Integer) As Boolean
        Return (Val1 - Val2).Absolute < Epsilon
    End Function
    <Extension()> Public Function AreEqual(Val1 As Long, Val2 As Long) As Boolean
        Return (Val1 - Val2).Absolute < Epsilon
    End Function

    ''' <summary>
    ''' Prüft, ob die beiden übergebenen Größen gleich sind: <paramref name="Val1"/> == <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function AreDifferentTo(Val1 As Double, Val2 As Double) As Boolean
        Return Not Val1.AreEqual(Val2)
    End Function
    <Extension()> Public Function AreDifferentTo(Val1 As Integer, Val2 As Integer) As Boolean
        Return Not Val1.AreEqual(Val2)
    End Function

    ''' <summary>
    ''' Prüft, ob die erste übergebene Größe <paramref name="Val1"/> kleiner als die zweite übergebene Größe <paramref name="Val2"/> ist: <paramref name="Val1"/> &lt; <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsLess(Val1 As Double, Val2 As Double) As Boolean
        Return Val2 - Val1 > Epsilon
    End Function
    <Extension()> Public Function IsLess(Val1 As Integer, Val2 As Integer) As Boolean
        Return Val2 - Val1 > Epsilon
    End Function

    ''' <summary>
    ''' Prüft, ob die erste übergebene Größe <paramref name="Val1"/> kleiner oder gleich als die zweite übergebene Größe <paramref name="Val2"/> ist: <paramref name="Val1"/> &lt;= <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>

    <Extension()> Public Function IsLessOrEqual(Val1 As Integer, Val2 As Integer) As Boolean
        Return Val1 - Val2 <= Epsilon
    End Function

    ''' <summary>
    ''' Prüft, ob die erste übergebene Größe <paramref name="Val1"/> größer als die zweite übergebene Größe <paramref name="Val2"/> ist: <paramref name="Val1"/> &gt; <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsLarger(Val1 As Double, Val2 As Double) As Boolean
        Return Val1 - Val2 > Epsilon
    End Function
    <Extension()> Public Function IsLarger(Val1 As Integer, Val2 As Integer) As Boolean
        Return Val1 - Val2 > Epsilon
    End Function
    <Extension()> Public Function IsLarger(Val1 As Long, Val2 As Long) As Boolean
        Return Val1 - Val2 > Epsilon
    End Function

    ''' <summary>
    ''' Prüft, ob die erste übergebene Größe <paramref name="Val1"/> größer oder gleich als die zweite übergebene Größe <paramref name="Val2"/> ist: <paramref name="Val1"/> &gt;= <paramref name="Val2"/>
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsLargerOrEqual(Val1 As Double, Val2 As Double) As Boolean
        Return Val2 - Val1 <= Epsilon
    End Function
    <Extension()> Public Function IsLargerOrEqual(Val1 As Integer, Val2 As Integer) As Boolean
        Return Val2 - Val1 <= Epsilon
    End Function
    <Extension()> Public Function IsLargerOrEqual(Val1 As Single, Val2 As Single) As Boolean
        Return Val2 - Val1 <= Epsilon
    End Function

    ''' <summary>
    ''' Gibt den größeren von zwei Vergleichswerten zurück
    ''' </summary>
    ''' <param name="Val1">Erste zu prüfende Größe</param>
    ''' <param name="Val2">Zweite zu prüfende Größe</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function GetLarger(Val1 As Integer, Val2 As Integer) As Integer
        Return If(Val1.IsLargerOrEqual(Val2), Val1, Val2)
    End Function

    ''' <summary>
    ''' Prüft, ob die übergebene Größe <paramref name="Val1"/> sich innerhalb eines Bereiches befindet: <paramref name="LVal"/> &lt; <paramref name="Val1"/> &lt; <paramref name="UVal"/>.
    ''' </summary>
    ''' <param name="Val1">Zu prüfende Größe</param>
    ''' <param name="LVal">Untere Schwelle</param>
    ''' <param name="UVal">Obere schwelle</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Public Function IsInRange(Val1 As Integer, LVal As Integer, UVal As Integer) As Boolean
        Return Val1.IsLargerOrEqual(LVal) And Val1.IsLessOrEqual(UVal)
    End Function


    ''' <summary>
    ''' Prüft, ob der übergebende Wert negativ ist
    ''' </summary>
    ''' <param name="Value">Der zu überprüfende Wert.</param>
    ''' <returns>Es erfolgt ein Vergleich gegen die festgelegte Epsilonschwelle.</returns>
    <Extension()> Friend Function IsNegative(Value As Double) As Boolean
        Return IsLess(Value, 0)
    End Function

    <Extension()> Friend Function IsNegative(Value As Integer) As Boolean
        Return IsLess(Value, 0)
    End Function
    <Extension()> Friend Function IsPositive(Value As Double) As Boolean
        Return IsLarger(Value, 0)
    End Function

    <Extension()> Friend Function IsPositive(Value As Integer) As Boolean
        Return IsLarger(Value, 0)
    End Function
#End Region

#Region "Extensions für Verarbeitung von Zeichenfolgen: String"
    <Extension> Public Function AreEqual(Str1 As String, Str2 As String) As Boolean
        Return String.Compare(Str1, Str2).IsZero
    End Function
    <Extension> Public Function AreNotEqual(Str1 As String, Str2 As String) As Boolean
        Return String.Compare(Str1, Str2).IsNotZero
    End Function
    <Extension> Public Function IsStringEmpty(Str1 As String) As Boolean
        Return Str1.AreEqual(String.Empty)
    End Function
    <Extension> Public Function IsNotStringEmpty(Str1 As String) As Boolean
        Return Str1 IsNot Nothing AndAlso Not Str1.IsStringEmpty
    End Function
    <Extension> Public Function IsStringNothingOrEmpty(Str1 As String) As Boolean
        Return Str1 Is Nothing OrElse Str1.IsStringEmpty
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
    <Extension> Friend Function ToInt(Value As Boolean) As Integer
        Return If(Value, 1, 0)
    End Function
#End Region

    ''' <summary>
    ''' Provides the Description Name for an enumeration based on the System.ComponentModel.Description Attribute on the enumeration value. if not found it returnes the defaultvalue passed.
    ''' Allows the description property of an enumeration to be exposed easily.
    ''' </summary>
    ''' <param name="EnumConstant">The Enumeration Item extended by the function.</param>
    <Extension()>
    Public Function Description(EnumConstant As [Enum]) As String
        Dim fi As Reflection.FieldInfo = EnumConstant.GetType().GetField(EnumConstant.ToString())
        Dim aattr() As DescriptionAttribute = DirectCast(fi.GetCustomAttributes(GetType(DescriptionAttribute), False), DescriptionAttribute())
        If aattr.Length = 0 OrElse aattr(0).Description = "" Then
            Return ""
        Else
            Return aattr(0).Description
        End If
    End Function
End Module