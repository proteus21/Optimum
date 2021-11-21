Imports System.Runtime.Serialization.Formatters.Binary
Imports System.SerializableAttribute
'Public Class Formatxt

'Public Class Person
'    Private m_valueTxt As String
'    Private m_sLastName As String
'    Public Sub New()
'    End Sub

'    Public Property FirstName() As String
'        Get
'            Return Me.m_valueTxt
'        End Get
'        Set(ByVal value As String)
'            Me.m_valueTxt = value
'        End Set
'    End Property

'    Class Customer
'        Public txt2, txt3, txt4, txt5, txt6, txt7, txt8, txt9, txt10 As String
'        Sub New(ByVal _text2 As String, ByVal _text3 As String, ByVal _text4 As String, ByVal _text5 As String, ByVal _text6 As String, ByVal _text7 As String, ByVal _text8 As String, ByVal _text9 As String, ByVal _text10 As String)
'            txt2 = _text2 : txt3 = _text3 : txt4 = _text4 : txt5 = _text5 : txt6 = _text6 : txt7 = _text7 : txt8 = _text8 : txt9 = _text9 : txt10 = _text10
'        End Sub
'    End Class
'End Class

<Serializable()> Public Class TestSimpleObject

    Public member1 As String
    Public member2 As String
    Public member3 As String
    Public member4 As String
    Public member5 As String
    Public member6 As String
    Public member7 As String
    Public member8 As String
    Public member9 As String




    Public Sub New(ByVal _text2 As String, ByVal _text3 As String, ByVal _text4 As String, ByVal _text5 As String, ByVal _text6 As String, ByVal _text7 As String, ByVal _text8 As String, ByVal _text9 As String, ByVal _text10 As String)
        member1 = _text2
        member2 = _text3
        member3 = _text4
        member4 = _text5
        member5 = _text6
        member6 = _text7
        member7 = _text8
        member8 = _text9
        member9 = _text10

    End Sub 'New


End Class 'TestSimpleObject

'End Class
