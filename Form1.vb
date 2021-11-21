Imports System.Math
Imports System
Imports System.IO
Imports System.Runtime.Serialization.Formatters.Binary
Imports System.Collections.Generic

Public Class Form1
    Dim baza() As Double
    Dim baza1() As Double
    Private ReadOnly LOGIN_DATA_FILE As String = Application.StartupPath + "\" + "login.dat"
    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        'GroupBox1.Top = 0
        'GroupBox1.Left = 0
        'GroupBox1.Width = Me.Width
        'GroupBox1.Height = Me.Height

        'Label1.Left = GroupBox1.Top + 10
        'Label1.Top = GroupBox1.Left + 20

        'TextBox1.Top = Label1.Top + 50
        'TextBox1.Left = Label1.Left
        'TextBox1.Width = GroupBox1.Width - 20

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim n As Integer
        If NumericUpDown1.Value = 0 Then
            NumericUpDown1.Value = 3
        End If
        If TextBox4.Text = "0" Or TextBox7.Text = "0" Or TextBox10.Text = "0" Then
            MsgBox("Skok parametru nie mo¿e wynosiæ 0")
        Else
            If TextBox3.Text <> "0" Or CDbl(TextBox2.Text) < CDbl(TextBox3.Text) Then
                If TextBox6.Text <> "0" Or CDbl(TextBox5.Text) < CDbl(TextBox6.Text) Then
                    If TextBox9.Text <> "0" Or CDbl(TextBox8.Text) < CDbl(TextBox9.Text) Then

                        ' funkcja y=a+P+f+r+P^2+f^2+r^2+P*f+P*r+f*r+P*f*r
                        Dim constant As Double = CDbl(TextBox1.Text)
                        Dim promien As Double = CDbl(TextBox13.Text)
                        Dim sila As Double = CDbl(TextBox11.Text)
                        Dim posuw As Double = CDbl(TextBox12.Text)
                        ' value ^2
                        Dim sqr_promien As Double = CDbl(TextBox17.Text)
                        Dim sqr_sila As Double = CDbl(TextBox15.Text)
                        Dim sqr_posuw As Double = CDbl(TextBox16.Text)
                        '' exchangable value

                        Dim promien_sila As Double = CDbl(TextBox20.Text) 'Dim sila_promien As Double
                        Dim promien_posuw As Double = CDbl(TextBox21.Text) ' Dim posuw_promien As Double
                        Dim sila_posuw As Double = CDbl(TextBox19.Text)

                        'combain three elements
                        Dim promien_sila_posuw As Double = CDbl(TextBox22.Text)

                        'Parametry zmienne
                        Dim min_sila As Double = CDbl(TextBox2.Text)
                        Dim max_sila As Double = CDbl(TextBox3.Text)
                        Dim skok_sila As Double = CDbl(TextBox4.Text)


                        Dim min_posuw As Double = CDbl(TextBox5.Text)
                        Dim max_posuw As Double = CDbl(TextBox6.Text)
                        Dim skok_posuw As Double = CDbl(TextBox7.Text)


                        Dim min_promien As Double = CDbl(TextBox8.Text)
                        Dim max_promien As Double = CDbl(TextBox9.Text)
                        Dim skok_promien As Double = CDbl(TextBox10.Text)

                        ' ToolStripStatusLabel4.Visible = True
                        'ToolStripStatusLabel4.Text = "Obliczenia"
                        ToolStripProgressBar2.Visible = True
                        ToolStripStatusLabel6.Visible = True
                        ToolStripStatusLabel6.Text = "Liczba przypadków"

                        DataGridView1.Rows.Clear()
                        Dim silaFs As Double
                        Dim skokPs As Double
                        Dim promienRs As Double
                        Dim y As Integer = 0
                        '' An information are reached to save the number of the cases
                        ' Wygenerowanie liczby przypadków
                        'Wartoœæ skoku pêtli musi byæ wartoœæi¹ jednostkow¹ ca³kowit¹
                        For silaFs = min_sila To max_sila Step skok_sila
                            For skokPs = min_posuw / skok_posuw To max_posuw / skok_posuw Step skok_posuw / skok_posuw
                                For promienRs = min_promien / skok_promien To max_promien / skok_promien Step skok_promien / skok_promien
                                    ReDim Preserve baza1(y)
                                    y = y + 1

                                Next
                            Next
                        Next
                        Dim silaF As Double = 0
                        Dim skokP As Double = 0
                        Dim promienR As Double = 0


                        ToolStripProgressBar2.Maximum = baza1.GetUpperBound(0)
                        ' total number all scores
                        ToolStripStatusLabel7.Text = baza1.GetUpperBound(0) + 1

                        ' generating data for all cases
                        For silaF = min_sila To max_sila Step skok_sila
                            For skokP = min_posuw / skok_posuw To max_posuw / skok_posuw Step skok_posuw / skok_posuw
                                For promienR = min_promien / skok_promien To max_promien / skok_promien Step skok_promien / skok_promien


                                    n = DataGridView1.Rows.Add()
                                    ReDim Preserve baza(n)
                                    ' progress bar

                                    ToolStripProgressBar2.Value = n
                                    ToolStripStatusLabel6.Visible = True
                                    ' ToolStripStatusLabel5.Text = Round((100 * (n) / Round(baza1.GetUpperBound(0), 0)), 0) & "%"
                                    'Datagridview

                                    ' zaokr¹glenie liczb do 5 po przecinku
                                    'DataGridView1.Rows(n).Cells(5).Value = Math.Round(constant + (sila * silaF) + (posuw * skokP) + (promien * promienR) + (sqr_sila * silaF ^ 2) + (sqr_posuw * skokP ^ 2) + (sqr_promien * promienR ^ 2) + (promien_sila * promienR * silaF) + (promien_posuw * promienR * skokP) + (sila_posuw * silaF * skokP) + (promien_sila_posuw * silaF * skokP * promienR), 4)
                                    ' zaokr¹glenie do 5 liczb po przecinku do góry
                                    Select Case CInt(NumericUpDown1.Value)
                                        Case 0
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1) / 1
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1) / 1
                                        Case 1
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10) / 10
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10) / 10
                                        Case 2
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100) / 100
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100) / 100

                                        Case 3
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000) / 1000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000) / 1000
                                        Case 4
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000) / 10000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000) / 10000
                                        Case 5
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000) / 100000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000) / 100000
                                        Case 6
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000000) / 1000000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000000) / 1000000
                                        Case 7
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000000) / 10000000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000000) / 10000000
                                        Case 8
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000000) / 100000000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000000) / 100000000
                                    End Select

                                    DataGridView1.Rows(n).Cells(4).Value = promienR * skok_promien
                                    DataGridView1.Rows(n).Cells(3).Value = skokP * skok_posuw
                                    DataGridView1.Rows(n).Cells(2).Value = silaF
                                    DataGridView1.Rows(n).Cells(1).Value = TextBox14.Text
                                    DataGridView1.Rows(n).Cells(0).Value = n + 1

                                    DataGridView1.Rows(n).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                    DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                                    ' progress bar

                                    If n = ToolStripProgressBar2.Maximum Then
                                        ToolStripProgressBar2.Visible = False
                                        ToolStripStatusLabel5.Visible = True
                                        ToolStripStatusLabel3.Visible = True
                                        ToolStripStatusLabel7.Visible = True
                                        ToolStripStatusLabel2.Visible = False
                                        ToolStripStatusLabel4.Visible = True
                                        ToolStripStatusLabel4.Visible = True



                                        ' ToolStripStatusLabel4.Text = ""
                                    End If

                                Next
                            Next
                        Next
                    Else
                        MsgBox("Sprawdz wprowadzone parametry. Wartoœæ max nie mo¿e wynosic 0, a tak¿e wartoœæ max nie mo¿e byæ mniejsza ni¿ min")
                    End If
                Else
                    MsgBox("Sprawdz wprowadzone parametry. Wartoœæ max nie mo¿e wynosic 0, a tak¿e wartoœæ max nie mo¿e byæ mniejsza ni¿ min")
                End If
            Else
                MsgBox("Sprawdz wprowadzone parametry. Wartoœæ max nie mo¿e wynosic 0, a tak¿e wartoœæ max nie mo¿e byæ mniejsza ni¿ min")
            End If

        End If
    End Sub

    Public Function Szukaj(ByVal tp As String, ByVal szuk As String) As String
        Dim test As String
        Dim i As Integer
        i = 1
        Do Until i = Len(tp) + 1
            test = Mid(tp, i, 11)
            If Mid(tp, i, 11) = szuk Then
                Szukaj = Microsoft.VisualBasic.Left(tp, i - 1)
                Exit Do
            Else
            End If
            i += 1
        Loop
    End Function

    Private Sub txt_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox10.KeyPress, TextBox11.KeyPress, TextBox12.KeyPress, TextBox13.KeyPress, TextBox15.KeyPress, TextBox16.KeyPress, TextBox17.KeyPress, TextBox18.KeyPress, TextBox19.KeyPress, TextBox2.KeyPress, TextBox20.KeyPress, TextBox21.KeyPress, TextBox22.KeyPress, TextBox23.KeyPress, TextBox3.KeyPress, TextBox4.KeyPress, TextBox5.KeyPress, TextBox6.KeyPress, TextBox7.KeyPress, TextBox8.KeyPress, TextBox9.KeyPress
        Select Case Val(Asc(e.KeyChar))
            Case 48 To 57
                'Jeœli u¿ykownik wpisze liczbê nic nie rób
            Case 8, 13, 34, 45, 44
                'Zezwól na u¿ycie klawisza BACKSPACE i ENTER i przecinek oraz krótka linia
            Case Else
                'Gdy wykryjesz jakikolwiek inny klawisz, zablokuj
                e.Handled = True
        End Select
    End Sub

    Public Sub New()

        ' This call is required by the Windows Form Designer.

        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        Me.Height = 473
        Me.Width = 863
        TextBox1.Text = "0" 'default value
        TextBox2.Text = "0" 'default value
        TextBox3.Text = "0" 'default value
        TextBox4.Text = "0" 'default value
        TextBox5.Text = "0" 'default value
        TextBox6.Text = "0" 'default value
        TextBox7.Text = "0" 'default value
        TextBox8.Text = "0" 'default value
        TextBox9.Text = "0" 'default value
        TextBox10.Text = "0" 'default value
        TextBox11.Text = "0" 'default value
        TextBox12.Text = "0" 'default value
        TextBox13.Text = "0" 'default value
        TextBox14.Text = "y(Sa)" 'default value
        TextBox15.Text = "0" 'default value
        TextBox16.Text = "0" 'default value
        TextBox17.Text = "0" 'default value
        TextBox19.Text = "0" 'default value
        TextBox20.Text = "0" 'default value
        TextBox21.Text = "0" 'default value
        TextBox22.Text = "0" 'default value
        ToolStripProgressBar2.Visible = False
        DataGridView1.RowCount = 9 'default value
        DataGridView1.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter 'default value
        ' see if there is any login data

        Dim loginIN As TestSimpleObject '(CDbl(TextBox2.Text), CDbl(TextBox3.Text), CDbl(TextBox4.Text), CDbl(TextBox5.Text), CDbl(TextBox6.Text), CDbl(TextBox6.Text), CDbl(TextBox8.Text), CDbl(TextBox9.Text), CDbl(TextBox10.Text))
        If (File.Exists(LOGIN_DATA_FILE)) Then
            Dim fs As FileStream = New FileStream(LOGIN_DATA_FILE, FileMode.Open)
            Dim formatter As Runtime.Serialization.Formatters.Binary.BinaryFormatter = New Runtime.Serialization.Formatters.Binary.BinaryFormatter()


            Try
                ' loginIn = formatter.Deserialize(fs)
                'loginIn = CType(formatter.Deserialize(fs), TestSimpleObject)
                loginIN = DirectCast(formatter.Deserialize(fs), TestSimpleObject)
            


            Catch
                ' do nothing
            Finally

                fs.Close()
            End Try

            If (Not loginIN Is Nothing) Then
                Me.TextBox2.Text = loginIN.member1.ToString
                Me.TextBox3.Text = loginIN.member2.ToString
                Me.TextBox4.Text = loginIN.member3.ToString
                Me.TextBox5.Text = loginIN.member4.ToString
                Me.TextBox6.Text = loginIN.member5.ToString
                Me.TextBox7.Text = loginIN.member6.ToString
                Me.TextBox8.Text = loginIN.member7.ToString
                Me.TextBox9.Text = loginIN.member8.ToString
                Me.TextBox10.Text = loginIN.member9.ToString
            End If
        End If

    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged

        If CheckBox1.Checked = True Then
            If CStr(DataGridView1.Rows(0).Cells(0).Value) <> "" Then

                Dim c As Integer
                Dim count As Integer = DataGridView1.Rows.Count
                ToolStripProgressBar2.Visible = True
                Array.Sort(baza)
                For c = 0 To count - 1
                    'sortowanie tablicy wzrastaj¹co/ ascending table sort

                    Dim n As Double = baza(0)
                    ToolStripProgressBar2.Maximum = count - 1
                    ToolStripProgressBar2.Value = c

                    Dim s As Integer = CInt(NumericUpDown1.Value)
                    If Math.Round(n, s) = Math.Round(DataGridView1.Rows(c).Cells(5).Value, s) Then
                        DataGridView1.Rows(c).DefaultCellStyle.BackColor = Color.Aquamarine
                    End If
                    If c = ToolStripProgressBar2.Maximum Then
                        ToolStripProgressBar2.Visible = False
                        ' ToolStripStatusLabel4.Text = ""
                    End If
                Next
                'zwalnianie pamieci /clear rem
                Call SET_MEMORY()
            Else
                MsgBox("Zdefiniuj parametry")
            End If
        End If
        ' Dzia³anie poprawne ale przy 20 tys sie zawiesza
        ''If CheckBox1.Checked = True Then
        ''    If CStr(DataGridView1.Rows(0).Cells(0).Value) <> "" Then

        ''        Dim c As Integer
        ''        Dim count As Integer = DataGridView1.Rows.Count
        ''        ToolStripProgressBar1.Visible = True
        ''        For c = 0 To count - 1
        ''            Call SET_MEMORY()
        ''            Dim n As Double = MinValOfIntArray(baza)
        ''            'ToolStripProgressBar1.Maximum = count - 1
        ''            'ToolStripProgressBar1.Value = c
        ''            'ToolStripStatusLabel6.Text = n
        ''            'MsgBox(Round(n, 5))
        ''            'MsgBox(DataGridView1.Rows(c).Cells(5).Value)
        ''            If Round(n, 4) = DataGridView1.Rows(c).Cells(5).Value Then
        ''                DataGridView1.Rows(c).DefaultCellStyle.BackColor = Color.Aquamarine
        ''            End If
        ''            If c = ToolStripProgressBar1.Maximum Then
        ''                ToolStripProgressBar1.Visible = False
        ''                ' ToolStripStatusLabel4.Text = ""
        ''            End If
        ''        Next
        ''    Else
        ''        MsgBox("Zdefiniuj parametry")
        ''    End If

        ''End If
        'Dim ile As Integer = 0

        'Wyzerowanie zmiennej przechowuj¹cej informacjê o liczbie elementów w zbiorze.

        '        FileOpen(nrPliku, plik, OpenMode.Input)
        'Otwarcie pliku w trybie do odczytu.

        '        Do While Not EOF(nrPliku)
        'Otwarcie pêtli odczytuj¹cej kolejne elementy pliku.

        '            ReDim Preserve t(ile)
        'Zmiana rozmiaru tablicy.

        '            t(ile) = LineInput(nrPliku)
        '            Console.Write(t(ile) & " ")
        'Odczytanie elementu z pliku i zapamiêtanie go w tablicy t.

        '            If ile = 0 Then
        '                Min = t(ile)
        '                Max = t(ile)
        '            Else
        '                If t(ile) > Max() Then Max = t(ile)
        '                If t(ile) < Min() Then Min = t(ile)
        '            End If
        '	Szukanie wartoœci maksymalnej i minimalnej w zbiorze.

        '            ile += 1
        '        Loop
        '        Console.WriteLine("Liczby z pliku zosta³y odczytane")
        '        FileClose(nrPliku)


    End Sub

    Private Sub CheckBox2_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox2.CheckedChanged

        If CheckBox2.Checked = True Then
            If TextBox19.Text = "" Or TextBox23.Text = "" Then
                MsgBox("Podaj przedzia³ poszukiwanych wartoœci")
            Else

                If CStr(DataGridView1.Rows(0).Cells(0).Value) <> "" Then
                    Dim d As Integer
                    Dim count As Integer = DataGridView1.Rows.Count - 1

                    For d = 0 To count
                        ' MsgBox(CDbl(DataGridView1.Rows(d).Cells(5).Value) & Chr(16) & CDbl(TextBox18.Text))
                        If CDbl(DataGridView1.Rows(d).Cells(5).Value) >= CDbl(TextBox18.Text) And CDbl(DataGridView1.Rows(d).Cells(5).Value) <= CDbl(TextBox23.Text) Then
                            'select match rows

                            DataGridView1.Rows(d).DefaultCellStyle.BackColor = Color.CadetBlue

                        End If

                        ToolStripProgressBar2.Maximum = count
                        ToolStripProgressBar2.Visible = True
                        ToolStripProgressBar2.Value = d
                        If d = ToolStripProgressBar2.Maximum Then
                            ToolStripProgressBar2.Visible = False
                        End If
                    Next
                    'Dim ks As Double = AvarageValOfIntArray(baza)


                    '  ToolStripStatusLabel6.Text = d
                    'MsgBox(DataGridView1.Rows(d).Cells(5).Value)

                Else
                    MsgBox("Zdefinuj parametry")
                End If
            End If
        End If

    End Sub
    Public Function MinValOfIntArray(ByRef TheArray1 As Object) As Double
        'This function gives max value of int array without sorting an array
        Dim t() As Double
        Dim ile As Integer = 0
        Dim min, max, i As Integer

        For i = 0 To UBound(TheArray1)
            'Otwarcie pêtli odczytuj¹cej kolejne elementy pliku.

            ReDim Preserve t(ile)
            'Zmiana rozmiaru tablicy.

            t(ile) = (TheArray1(i))

            'Odczytanie elementu z pliku i zapamiêtanie go w tablicy t.

            If ile = 0 Then
                min = t(ile)
                max = t(ile)
            Else
                If t(ile) > max Then max = t(ile)
                If t(ile) < min Then min = t(ile)
            End If
            ile += 1
        Next

        'Dim i As Integer
        'Dim MinIntegersIndex As Integer
        'MinIntegersIndex = 0

        'For i = 0 To UBound(TheArray1)
        '    ' If IsNothing(TheArray1(i)) = False Then
        '    'If TheArray(i) = "" Then

        '    If CSng(TheArray1(i)) < CSng(TheArray1(MinIntegersIndex)) Then

        '        ' MsgBox " false"
        '        MinIntegersIndex = i
        '    Else
        '        ' MsgBox "true"
        '    End If
        '    'End If
        'Next
        ''index of max value is MaxValOfIntArray
        'MinValOfIntArray = CSng(TheArray1(MinIntegersIndex))


    End Function




    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged

        If TextBox1.Text = "" Then
            TextBox1.Tag = 0
            TextBox1.Text = TextBox1.Tag
        End If
    End Sub

    Private Sub TextBox11_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox11.TextChanged
        If TextBox11.Text = "" Then
            TextBox11.Tag = 0
            TextBox11.Text = TextBox11.Tag
        End If
    End Sub

    Private Sub TextBox12_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox12.TextChanged
        If TextBox12.Text = "" Then
            TextBox12.Tag = 0
            TextBox12.Text = TextBox12.Tag
        End If
    End Sub

    Private Sub TextBox13_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox13.TextChanged
        If TextBox13.Text = "" Then
            TextBox13.Tag = 0
            TextBox13.Text = TextBox13.Tag
        End If
    End Sub

    Private Sub TextBox15_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox15.TextChanged
        If TextBox15.Text = "" Then
            TextBox15.Tag = 0
            TextBox15.Text = TextBox15.Tag
        End If
    End Sub

    Private Sub TextBox16_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox16.TextChanged
        If TextBox16.Text = "" Then
            TextBox16.Tag = 0
            TextBox16.Text = TextBox16.Tag
        End If
    End Sub

    Private Sub TextBox17_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox17.TextChanged
        If TextBox17.Text = "" Then
            TextBox17.Tag = 0
            TextBox17.Text = TextBox17.Tag
        End If
    End Sub

    Private Sub TextBox19_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox19.TextChanged
        If TextBox19.Text = "" Then
            TextBox19.Tag = 0
            TextBox19.Text = TextBox19.Tag
        End If
    End Sub

    Private Sub TextBox20_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox20.TextChanged
        If TextBox20.Text = "" Then
            TextBox20.Tag = 0
            TextBox20.Text = TextBox20.Tag
        End If
    End Sub

    Private Sub TextBox21_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox21.TextChanged
        If TextBox21.Text = "" Then
            TextBox21.Tag = 0
            TextBox21.Text = TextBox21.Tag
        End If
    End Sub

    Private Sub TextBox22_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox22.TextChanged
        If TextBox22.Text = "" Then
            TextBox22.Tag = 0
            TextBox22.Text = TextBox22.Tag
        End If
    End Sub

    Private Sub TextBox2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox2.TextChanged
        If TextBox2.Text = "" Then
            TextBox2.Tag = 0
            TextBox2.Text = TextBox2.Tag
        End If
    End Sub

    Private Sub TextBox3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox3.TextChanged
        If TextBox3.Text = "" Then
            TextBox3.Tag = 0
            TextBox3.Text = TextBox3.Tag
        End If
        If CDbl(TextBox3.Text) < CDbl(TextBox2.Text) Then
            MsgBox("Wartoœæ max musi byæ wiêksza od min")
        End If
    End Sub

    Private Sub TextBox4_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox4.TextChanged
        If TextBox4.Text = "" Then
            TextBox4.Tag = 0
            TextBox4.Text = TextBox4.Tag
        End If
    End Sub

    Private Sub TextBox5_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox5.TextChanged
        If TextBox5.Text = "" Then
            TextBox5.Tag = 0
            TextBox5.Text = TextBox5.Tag
        End If
    End Sub

    Private Sub TextBox6_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox6.TextChanged
        If TextBox6.Text = "" Then
            TextBox6.Tag = 0
            TextBox6.Text = TextBox6.Tag
        End If
        If CDbl(TextBox6.Text) < CDbl(TextBox5.Text) Then
            MsgBox("Wartoœæ max musi byæ wiêksza od min")
        End If
    End Sub

    Private Sub TextBox7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox7.TextChanged
        If TextBox7.Text = "" Then
            TextBox7.Tag = 0
            TextBox7.Text = TextBox7.Tag
        End If
    End Sub

    Private Sub TextBox8_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox8.TextChanged
        If TextBox8.Text = "" Then
            TextBox8.Tag = 0
            TextBox8.Text = TextBox8.Tag
        End If
        If CDbl(TextBox8.Text) < CDbl(TextBox7.Text) Then
            MsgBox("Wartoœæ max musi byæ wiêksza od min")
        End If
    End Sub

    Private Sub TextBox9_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox9.TextChanged
        If TextBox9.Text = "" Then
            TextBox9.Tag = 0
            TextBox9.Text = TextBox9.Tag
        End If
    End Sub

    Private Sub TextBox10_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox10.TextChanged
        If TextBox10.Text = "" Then
            TextBox10.Tag = 0
            TextBox10.Text = TextBox10.Tag
        End If
    End Sub

    'Private Sub DataGridView1_ColumnHeaderMouseClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles DataGridView1.ColumnHeaderMouseClick
    '    Dim newColumn As DataGridViewColumn
    '    If DataGridView1.Columns.GetColumnCount(DataGridViewElementStates _
    '        .Selected) = 1 Then
    '        newColumn = DataGridView1.SelectedColumns(0)
    '    Else
    '        newColumn = Nothing
    '    End If

    '    Dim oldColumn As DataGridViewColumn = DataGridView1.SortedColumn
    '    Dim direction As ComponentModel.ListSortDirection

    '    ' If oldColumn is null, then the DataGridView is not currently sorted.
    '    If oldColumn IsNot Nothing Then

    '        ' Sort the same column again, reversing the SortOrder.
    '        If oldColumn Is newColumn AndAlso DataGridView1.SortOrder = _
    '            SortOrder.Ascending Then
    '            direction = ComponentModel.ListSortDirection.Descending
    '        Else

    '            ' Sort a new column and remove the old SortGlyph.
    '            direction = ComponentModel.ListSortDirection.Ascending
    '            oldColumn.HeaderCell.SortGlyphDirection = SortOrder.None
    '        End If
    '    Else
    '        direction = ComponentModel.ListSortDirection.Ascending
    '    End If


    '    ' If no column has been selected, display an error dialog  box.
    '    If newColumn Is Nothing Then
    '        'MessageBox.Show("Zaznacz pojedyncz¹ kolumnê .", _
    '        '  "B³¹d: niew³aœciwe zaznaczenie", MessageBoxButtons.OK, _
    '        ' MessageBoxIcon.Error)
    '    Else
    '        DataGridView1.Sort(newColumn, direction)
    '        If direction = ComponentModel.ListSortDirection.Ascending Then
    '            newColumn.HeaderCell.SortGlyphDirection = SortOrder.Ascending
    '        Else
    '            newColumn.HeaderCell.SortGlyphDirection = SortOrder.Descending
    '        End If
    '    End If


    'End Sub
#Region "REDUKTOR PAMIÊCI"
    REM deklaracja kernell do zwalniania pamiêci
    Private Declare Auto Function SetProcessWorkingSetSize Lib "kernel32.dll" _
   (ByVal procHandle As IntPtr, ByVal min As Int32, ByVal max As Int32) As Boolean

    Public Sub SET_MEMORY()
        Try
            Dim Mem As Process
            Mem = Process.GetCurrentProcess()
            SetProcessWorkingSetSize(CType(Mem.Handle.ToInt64, IntPtr), -1, -1)
        Catch ex As ExecutionEngineException
        End Try
    End Sub

#End Region


    Private Sub ZapisDoPlikuToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ZapisDoPlikuToolStripMenuItem.Click
        Dim Wiersz As Integer
        Dim d, h As Integer
        Dim NazwaZestawienia As String
        Dim LiczbaWierszy, LiczbaKolumn As Integer
        Dim fileName As String
        ' Tablica wielowymiarowa dla okreœlania wzoru
        Dim varColumn(10) As String
        ' sta³a
        If CDbl(TextBox1.Text) <> 0 And CDbl(TextBox1.Text) > 0 Then
            varColumn(0) = (TextBox1.Text)
        ElseIf CDbl(TextBox1.Text) < 0 Then
            varColumn(0) = (TextBox1.Text)
        Else
            varColumn(0) = Nothing
        End If
        'si³a P
        If CDbl(TextBox11.Text) <> 0 And CDbl(TextBox11.Text) > 0 Then
            varColumn(1) = "+" & (TextBox11.Text) & "P"
        ElseIf CDbl(TextBox11.Text) < 0 Then
            varColumn(1) = (TextBox11.Text) & "P"
        Else
            varColumn(1) = Nothing
        End If
        'Posuw f
        If CDbl(TextBox12.Text) <> 0 And CDbl(TextBox12.Text) > 0 Then
            varColumn(2) = "+" & TextBox12.Text & "f"
        ElseIf CDbl(TextBox12.Text) < 0 Then
            varColumn(2) = TextBox12.Text & "f"
        Else
            varColumn(2) = Nothing
        End If
        'promien r
        If CDbl(TextBox13.Text) <> 0 And CDbl(TextBox13.Text) > 0 Then
            varColumn(3) = "+" & TextBox13.Text & "r"
        ElseIf CDbl(TextBox13.Text) < 0 Then
            varColumn(3) = TextBox13.Text & "r"
        Else
            varColumn(3) = Nothing
        End If

        'si³a P^2

        If CDbl(TextBox15.Text) <> 0 And CDbl(TextBox15.Text) > 0 Then
            varColumn(4) = "+" & TextBox15.Text & "P^2"
        ElseIf CDbl(TextBox15.Text) < 0 Then
            varColumn(4) = TextBox15.Text & "P^2"
        Else
            varColumn(4) = Nothing
        End If
        'Posuw f^2

        If CDbl(TextBox16.Text) <> 0 And CDbl(TextBox16.Text) > 0 Then
            varColumn(5) = "+" & TextBox16.Text & "f^2"
        ElseIf CDbl(TextBox16.Text) < 0 Then
            varColumn(5) = TextBox16.Text & "f^2"
        Else
            varColumn(5) = Nothing
        End If
        'promien r^2

        If CDbl(TextBox17.Text) <> 0 And CDbl(TextBox17.Text) > 0 Then
            varColumn(6) = "+" & TextBox17.Text & "r^2"
        ElseIf CDbl(TextBox17.Text) < 0 Then
            varColumn(6) = TextBox17.Text & "r^2"
        Else
            varColumn(6) = Nothing
        End If

        ' P*f
        If CDbl(TextBox19.Text) <> 0 And CDbl(TextBox19.Text) > 0 Then
            varColumn(7) = "+" & TextBox19.Text & "P*f"
        ElseIf CDbl(TextBox19.Text) < 0 Then
            varColumn(7) = TextBox19.Text & "P*f"
        Else
            varColumn(7) = Nothing
        End If

        ' P*r

        If CDbl(TextBox20.Text) <> 0 And CDbl(TextBox20.Text) > 0 Then
            varColumn(8) = "+" & TextBox20.Text & "P*r"
        ElseIf CDbl(TextBox20.Text) <> 0 Then
            varColumn(8) = TextBox20.Text & "P*r"
        Else
            varColumn(8) = Nothing
        End If

        ' f*r
        If CDbl(TextBox21.Text) <> 0 And CDbl(TextBox21.Text) > 0 Then
            varColumn(9) = "+" & TextBox21.Text & "f*r"
        ElseIf CDbl(TextBox21.Text) < 0 Then
            varColumn(9) = TextBox21.Text & "f*r"
        Else
            varColumn(9) = Nothing
        End If
        ' P*f*r
        If CDbl(TextBox22.Text) <> 0 And CDbl(TextBox22.Text) > 0 Then
            varColumn(10) = "+" & TextBox22.Text & "P*f*r"
        ElseIf CDbl(TextBox22.Text) < 0 Then
            varColumn(10) = TextBox22.Text & "P*f*r"
        Else
            varColumn(10) = Nothing
        End If



        'MsgBox(DataGridView1.Rows(0).Cells(0).Value)
        If CStr(DataGridView1.Rows(0).Cells(0).Value) <> "" Then
            With SaveFileDialog1
                .FileName = "Wyniki" & Chr(160) & "funkcji" & Chr(160) & TextBox14.Text
                .Filter = "Text files (*.txt)|*.txt|" & "All files|*.*"

                If SaveFileDialog1.ShowDialog() = Windows.Forms.DialogResult.OK Then
                    fileName = .FileName

                    Using ZapiszDoPliku As New System.IO.StreamWriter(fileName)
                        NazwaZestawienia = "C:\text.txt"
                        LiczbaKolumn = DataGridView1.ColumnCount
                        LiczbaWierszy = DataGridView1.RowCount

                        'set up range of an area
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine("---------------------------------------------------------------")
                        ZapiszDoPliku.WriteLine("Obliczanie wartoœci parametrów chropowatoœci")
                        ZapiszDoPliku.WriteLine("           oraz ich optymalizacja")
                        ZapiszDoPliku.WriteLine("przy pomocy funkcji -min- oraz poszukiwanej wartoœci")
                        ZapiszDoPliku.WriteLine("---------------------------------------------------------------")
                        ZapiszDoPliku.WriteLine("(C)2011 mgr in¿. K.Konefa³  Uniwersytet Rzeszowski")
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine(" Wczytane dane i parametry")
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine(" Funckja ogólna y" & "=" & varColumn(0) & varColumn(1) & varColumn(2) & varColumn(3) & varColumn(4) & varColumn(5) & varColumn(6) & varColumn(7) & varColumn(8) & varColumn(9) & varColumn(10))
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine("----------------------------------------------------------------")
                        ZapiszDoPliku.WriteLine("|" & "Nr." & vbTab & "|" & "Param." & vbTab & "|" & "Si³a P" & vbTab & "|" & "Posuw f" & "|" & "Promien" & "|" & "Wynik" & vbTab & vbTab & vbTab & "|")
                        ZapiszDoPliku.WriteLine("|" & "iter.i" & vbTab & "|" & "" & vbTab & "|" & Chr(160) & "[N]" & vbTab & "|" & "mm/obr" & vbTab & "|" & Chr(160) & "r mm" & vbTab & "|" & "" & vbTab & vbTab & vbTab & "|")
                        'ZapiszDoPliku.WriteLine("|" & "Lp." & "|" & Chr(160) & "Paratemtr" & "|" & Chr(160) & "Si³a P" & "|" & Chr(160) & "Posuw f" & "|" & Chr(160) & "Promien r " & "|" & Chr(160) & "Wynik" & Chr(160) & "|")
                        'ZapiszDoPliku.WriteLine("|" & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & "[N]" & Chr(160) & Chr(160) & "|" & "[mm/obr]" & "|" & Chr(160) & Chr(160) & Chr(160) & "[mm]" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|")
                        ZapiszDoPliku.WriteLine("|" & "---------------------------------------------------------------" & "|")
                        For Wiersz = 0 To DataGridView1.RowCount - 1

                            ZapiszDoPliku.Write("|" & DataGridView1.Rows.Item(Wiersz).Cells(0).Value & vbTab & "|" & DataGridView1.Rows.Item(Wiersz).Cells(1).Value & vbTab & "|" & DataGridView1.Rows.Item(Wiersz).Cells(2).Value & vbTab & "|" & DataGridView1.Rows.Item(Wiersz).Cells(3).Value & Chr(160) & vbTab & "|" & DataGridView1.Rows.Item(Wiersz).Cells(4).Value & Chr(160) & vbTab & "|" & DataGridView1.Rows.Item(Wiersz).Cells(5).Value & (vbTab) & vbTab & vbTab) ' & "|")
                            ZapiszDoPliku.WriteLine()
                            ZapiszDoPliku.WriteLine("|" & "---------------------------------------------------------------" & "|")
                            'For Wiersz = 0 To DataGridView1.RowCount - 1
                            'For Kolumna = 0 To DataGridView1.ColumnCount - 1
                            'ZapiszDoPliku.WriteLine(DataGridView1.Rows.Item(Wiersz).Cells(Kolumna).Value)
                            'ZapiszDoPliku.Write, DataGridView1.Rows.Item(Wiersz).Cells(2).Value, DataGridView1.Rows.Item(Wiersz).Cells(1).Value
                            'Next Kolumna
                        Next Wiersz
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine("Wyniki otrzymane po optymalizacji funkcj¹ - min")
                        ZapiszDoPliku.WriteLine()
                        ZapiszDoPliku.WriteLine("----------------------------------------------------------------")
                        ZapiszDoPliku.WriteLine("|" & "Nr." & vbTab & "|" & "Param." & vbTab & "|" & "Si³a P" & vbTab & "|" & "Posuw f" & "|" & "Promien" & "|" & "Wynik" & vbTab & vbTab & vbTab & "|")
                        ZapiszDoPliku.WriteLine("|" & "iter.i" & vbTab & "|" & "" & vbTab & "|" & Chr(160) & "[N]" & vbTab & "|" & "mm/obr" & vbTab & "|" & Chr(160) & "r mm" & vbTab & "|" & "" & vbTab & vbTab & vbTab & "|")
                        'ZapiszDoPliku.WriteLine("|" & "Lp." & "|" & Chr(160) & "Paratemtr" & "|" & Chr(160) & "Si³a P" & "|" & Chr(160) & "Posuw f" & "|" & Chr(160) & "Promien r " & "|" & Chr(160) & "Wynik" & Chr(160) & "|")
                        'ZapiszDoPliku.WriteLine("|" & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & "[N]" & Chr(160) & Chr(160) & "|" & "[mm/obr]" & "|" & Chr(160) & Chr(160) & Chr(160) & "[mm]" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|")
                        ZapiszDoPliku.WriteLine("|" & "---------------------------------------------------------------" & "|")
                        Array.Sort(baza)
                        For d = 0 To DataGridView1.RowCount - 1
                            Dim k As Double = baza(0)
                            If k = DataGridView1.Rows(d).Cells(5).Value Then


                                ZapiszDoPliku.Write("|" & DataGridView1.Rows.Item(d).Cells(0).Value & vbTab & "|" & DataGridView1.Rows.Item(d).Cells(1).Value & vbTab & "|" & DataGridView1.Rows.Item(d).Cells(2).Value & vbTab & "|" & DataGridView1.Rows.Item(d).Cells(3).Value & Chr(160) & vbTab & "|" & DataGridView1.Rows.Item(d).Cells(4).Value & Chr(160) & vbTab & "|" & DataGridView1.Rows.Item(d).Cells(5).Value & vbTab & vbTab & vbTab) ' & "|")
                                ZapiszDoPliku.WriteLine()
                                ZapiszDoPliku.WriteLine("|" & "---------------------------------------------------------------" & "|")
                                'For Wiersz = 0 To DataGridView1.RowCount - 1
                                'For Kolumna = 0 To DataGridView1.ColumnCount - 1
                                'ZapiszDoPliku.WriteLine(DataGridView1.Rows.Item(Wiersz).Cells(Kolumna).Value)
                                'ZapiszDoPliku.Write, DataGridView1.Rows.Item(Wiersz).Cells(2).Value, DataGridView1.Rows.Item(Wiersz).Cells(1).Value
                                'Next Kolumna
                                'Next Wiersz
                            End If
                        Next
                        If TextBox18.Text <> "" And TextBox23.Text <> "" Then
                            ZapiszDoPliku.WriteLine()
                            ZapiszDoPliku.WriteLine()
                            ZapiszDoPliku.WriteLine()
                            ZapiszDoPliku.WriteLine()
                            ZapiszDoPliku.WriteLine("Wyniki otrzymane po optymalizacji funkcj¹  w zdefiniowanym przedziale")
                            ZapiszDoPliku.WriteLine()
                            ZapiszDoPliku.WriteLine("----------------------------------------------------------------")
                            ZapiszDoPliku.WriteLine("|" & "Nr." & vbTab & "|" & "Param." & vbTab & "|" & "Si³a P" & vbTab & "|" & "Posuw f" & "|" & "Promien" & "|" & "Wynik" & vbTab & vbTab & vbTab & "|")
                            ZapiszDoPliku.WriteLine("|" & "iter.i" & vbTab & "|" & "" & vbTab & "|" & Chr(160) & "[N]" & vbTab & "|" & "mm/obr" & vbTab & "|" & Chr(160) & "r mm" & vbTab & "|" & "" & vbTab & vbTab & vbTab & "|")
                            'ZapiszDoPliku.WriteLine("|" & "Lp." & "|" & Chr(160) & "Paratemtr" & "|" & Chr(160) & "Si³a P" & "|" & Chr(160) & "Posuw f" & "|" & Chr(160) & "Promien r " & "|" & Chr(160) & "Wynik" & Chr(160) & "|")
                            'ZapiszDoPliku.WriteLine("|" & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & "[N]" & Chr(160) & Chr(160) & "|" & "[mm/obr]" & "|" & Chr(160) & Chr(160) & Chr(160) & "[mm]" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|" & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & Chr(160) & "|")
                            ZapiszDoPliku.WriteLine("|" & "---------------------------------------------------------------" & "|")

                            For h = 0 To DataGridView1.RowCount - 1

                                If CDbl(DataGridView1.Rows(h).Cells(5).Value) >= CDbl(TextBox18.Text) And CDbl(DataGridView1.Rows(h).Cells(5).Value) <= CDbl(TextBox23.Text) Then
                                    Dim count As Integer = DataGridView1.Rows.Count
                                    For d = 0 To count - 1

                                    Next

                                    ZapiszDoPliku.Write("|" & DataGridView1.Rows.Item(h).Cells(0).Value & vbTab & "|" & DataGridView1.Rows.Item(h).Cells(1).Value & vbTab & "|" & DataGridView1.Rows.Item(h).Cells(2).Value & vbTab & "|" & DataGridView1.Rows.Item(h).Cells(3).Value & Chr(160) & vbTab & "|" & DataGridView1.Rows.Item(h).Cells(4).Value & Chr(160) & vbTab & "|" & DataGridView1.Rows.Item(h).Cells(5).Value & vbTab & vbTab & vbTab) ' & "|")
                                    ZapiszDoPliku.WriteLine()
                                    ZapiszDoPliku.WriteLine("|" & "---------------------------------------------------------------" & "|")
                                    'For Wiersz = 0 To DataGridView1.RowCount - 1
                                    'For Kolumna = 0 To DataGridView1.ColumnCount - 1
                                    'ZapiszDoPliku.WriteLine(DataGridView1.Rows.Item(Wiersz).Cells(Kolumna).Value)
                                    'ZapiszDoPliku.Write, DataGridView1.Rows.Item(Wiersz).Cells(2).Value, DataGridView1.Rows.Item(Wiersz).Cells(1).Value
                                    'Next Kolumna
                                    'Next Wiersz
                                End If
                            Next

                        End If
                    End Using
                End If
            End With
        Else
            MsgBox("Wpierw zdefiniuj parametry optmalizacji, aby otrzymaæ plik")
        End If
    End Sub

    Private Sub AutorToolStripMenuItem1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AutorToolStripMenuItem1.Click
        AboutBox1.ShowDialog()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        MinValOfIntArray(baza)


    End Sub


    Private Sub NumericUpDown1_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles NumericUpDown1.ValueChanged
        Dim n As Integer
        If TextBox4.Text = "0" Or TextBox7.Text = "0" Or TextBox10.Text = "0" Then
            MsgBox("Skok parametru nie mo¿e wynosiæ 0")
        Else
            If TextBox3.Text <> "0" Or CDbl(TextBox2.Text) < CDbl(TextBox3.Text) Then
                If TextBox6.Text <> "0" Or CDbl(TextBox5.Text) < CDbl(TextBox6.Text) Then
                    If TextBox9.Text <> "0" Or CDbl(TextBox8.Text) < CDbl(TextBox9.Text) Then

                        ' funkcja y=a+P+f+r+P^2+f^2+r^2+P*f+P*r+f*r+P*f*r
                        Dim constant As Double = CDbl(TextBox1.Text)
                        Dim promien As Double = CDbl(TextBox13.Text)
                        Dim sila As Double = CDbl(TextBox11.Text)
                        Dim posuw As Double = CDbl(TextBox12.Text)
                        ' value ^2
                        Dim sqr_promien As Double = CDbl(TextBox17.Text)
                        Dim sqr_sila As Double = CDbl(TextBox15.Text)
                        Dim sqr_posuw As Double = CDbl(TextBox16.Text)
                        '' exchangable value

                        Dim promien_sila As Double = CDbl(TextBox20.Text) 'Dim sila_promien As Double
                        Dim promien_posuw As Double = CDbl(TextBox21.Text) ' Dim posuw_promien As Double
                        Dim sila_posuw As Double = CDbl(TextBox19.Text)

                        'combain three elements
                        Dim promien_sila_posuw As Double = CDbl(TextBox22.Text)

                        'Parametry zmienne
                        Dim min_sila As Double = CDbl(TextBox2.Text)
                        Dim max_sila As Double = CDbl(TextBox3.Text)
                        Dim skok_sila As Double = CDbl(TextBox4.Text)


                        Dim min_posuw As Double = CDbl(TextBox5.Text)
                        Dim max_posuw As Double = CDbl(TextBox6.Text)
                        Dim skok_posuw As Double = CDbl(TextBox7.Text)


                        Dim min_promien As Double = CDbl(TextBox8.Text)
                        Dim max_promien As Double = CDbl(TextBox9.Text)
                        Dim skok_promien As Double = CDbl(TextBox10.Text)

                        ' ToolStripStatusLabel4.Visible = True
                        'ToolStripStatusLabel4.Text = "Obliczenia"
                        ToolStripProgressBar2.Visible = True
                        ToolStripStatusLabel6.Visible = True
                        ToolStripStatusLabel6.Text = "Liczba przypadków"

                        DataGridView1.Rows.Clear()
                        Dim silaFs As Double
                        Dim skokPs As Double
                        Dim promienRs As Double
                        Dim y As Integer = 0
                        '' An information are reached to save the number of the cases
                        ' Wygenerowanie liczby przypadków
                        'Wartoœæ skoku pêtli musi byæ wartoœæi¹ jednostkow¹ ca³kowit¹
                        For silaFs = min_sila To max_sila Step skok_sila
                            For skokPs = min_posuw / skok_posuw To max_posuw / skok_posuw Step skok_posuw / skok_posuw
                                For promienRs = min_promien / skok_promien To max_promien / skok_promien Step skok_promien / skok_promien
                                    ReDim Preserve baza1(y)
                                    y = y + 1

                                Next
                            Next
                        Next
                        Dim silaF As Double = 0
                        Dim skokP As Double = 0
                        Dim promienR As Double = 0


                        ToolStripProgressBar2.Maximum = baza1.GetUpperBound(0)
                        ' total number all scores
                        ToolStripStatusLabel7.Text = baza1.GetUpperBound(0) + 1

                        ' generating data for all cases
                        For silaF = min_sila To max_sila Step skok_sila
                            For skokP = min_posuw / skok_posuw To max_posuw / skok_posuw Step skok_posuw / skok_posuw
                                For promienR = min_promien / skok_promien To max_promien / skok_promien Step skok_promien / skok_promien


                                    n = DataGridView1.Rows.Add()
                                    ReDim Preserve baza(n)
                                    ' progress bar

                                    ToolStripProgressBar2.Value = n
                                    ToolStripStatusLabel6.Visible = True
                                    ' ToolStripStatusLabel5.Text = Round((100 * (n) / Round(baza1.GetUpperBound(0), 0)), 0) & "%"
                                    'Datagridview

                                    ' zaokr¹glenie liczb do 5 po przecinku
                                    'DataGridView1.Rows(n).Cells(5).Value = Math.Round(constant + (sila * silaF) + (posuw * skokP) + (promien * promienR) + (sqr_sila * silaF ^ 2) + (sqr_posuw * skokP ^ 2) + (sqr_promien * promienR ^ 2) + (promien_sila * promienR * silaF) + (promien_posuw * promienR * skokP) + (sila_posuw * silaF * skokP) + (promien_sila_posuw * silaF * skokP * promienR), 4)
                                    ' zaokr¹glenie do 5 liczb po przecinku do góry
                                    Select Case CInt(NumericUpDown1.Value)
                                        Case 0
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1) / 1
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1) / 1
                                        Case 1
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10) / 10
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10) / 10
                                        Case 2
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100) / 100
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100) / 100

                                        Case 3
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000) / 1000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000) / 1000
                                        Case 4
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000) / 10000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000) / 10000
                                        Case 5
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000) / 100000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000) / 100000
                                        Case 6
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000000) / 1000000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 1000000) / 1000000
                                        Case 7
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000000) / 10000000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 10000000) / 10000000
                                        Case 8
                                            DataGridView1.Rows(n).Cells(5).Value = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000000) / 100000000
                                            baza(n) = Math.Truncate((constant + (sila * silaF) + (posuw * skokP * skok_posuw) + (promien * promienR * skok_promien) + (sqr_sila * silaF ^ 2) + (sqr_posuw * (skokP * skok_posuw) ^ 2) + (sqr_promien * (promienR * skok_promien) ^ 2) + (promien_sila * promienR * silaF * skok_promien) + (promien_posuw * promienR * skokP * skok_posuw * skok_promien) + (sila_posuw * silaF * skokP * skok_posuw) + (promien_sila_posuw * silaF * skokP * promienR * skok_posuw * skok_promien)) * 100000000) / 100000000
                                    End Select

                                    DataGridView1.Rows(n).Cells(4).Value = promienR * skok_promien
                                    DataGridView1.Rows(n).Cells(3).Value = skokP * skok_posuw
                                    DataGridView1.Rows(n).Cells(2).Value = silaF
                                    DataGridView1.Rows(n).Cells(1).Value = TextBox14.Text
                                    DataGridView1.Rows(n).Cells(0).Value = n + 1

                                    DataGridView1.Rows(n).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter
                                    DataGridView1.Columns(0).DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter

                                    ' progress bar

                                    If n = ToolStripProgressBar2.Maximum Then
                                        ToolStripProgressBar2.Visible = False
                                        ToolStripStatusLabel5.Visible = True
                                        ToolStripStatusLabel3.Visible = True
                                        ToolStripStatusLabel7.Visible = True
                                        ToolStripStatusLabel2.Visible = False
                                        ToolStripStatusLabel4.Visible = True
                                        ToolStripStatusLabel4.Visible = True



                                        ' ToolStripStatusLabel4.Text = ""
                                    End If

                                Next
                            Next
                        Next
                    Else
                        MsgBox("Sprawdz wprowadzone parametry. Wartoœæ max nie mo¿e wynosic 0, a tak¿e wartoœæ max nie mo¿e byæ mniejsza ni¿ min")
                    End If
                Else
                    MsgBox("Sprawdz wprowadzone parametry. Wartoœæ max nie mo¿e wynosic 0, a tak¿e wartoœæ max nie mo¿e byæ mniejsza ni¿ min")
                End If
            Else
                MsgBox("Sprawdz wprowadzone parametry. Wartoœæ max nie mo¿e wynosic 0, a tak¿e wartoœæ max nie mo¿e byæ mniejsza ni¿ min")
            End If

        End If
    End Sub




    Private Sub CheckBox3_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox3.CheckedChanged


        '    If (Not errorFound) Then
        If (Me.CheckBox3.Checked = True) Then

            Dim fs As New FileStream(LOGIN_DATA_FILE, FileMode.Create)

            Try
                ' Construct a BinaryFormatter and use it 
                ' to serialize the data to the stream.
                Dim formatter As New BinaryFormatter

                ' Construct a Version1Type object and serialize it.
                Dim P As New TestSimpleObject((TextBox2.Text), (TextBox3.Text), (TextBox4.Text), (TextBox5.Text), (TextBox6.Text), (TextBox7.Text), (TextBox8.Text), (TextBox9.Text), (TextBox10.Text))
                'Dim Persons As New ArrayList

                formatter.Serialize(fs, p)
            Catch v As Runtime.Serialization.SerializationException
                Console.WriteLine("Failed to serialize. Reason: " & v.Message)
                Throw
            Finally
                fs.Close()
            End Try
        Else

            If (File.Exists(LOGIN_DATA_FILE)) Then
                ' delete saved login info if the user doesn't have the
                ' 'remember' box checked
                File.Delete(LOGIN_DATA_FILE)
            End If
            'End If
            '        

        End If
    End Sub

    Private Sub WyczyœæWartoœciFunkcjiToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WyczyœæWartoœciFunkcjiToolStripMenuItem.Click
        TextBox1.Text = "0" 'default value

        TextBox11.Text = "0" 'default value
        TextBox12.Text = "0" 'default value
        TextBox13.Text = "0" 'default value
        TextBox14.Text = "y(Sa)" 'default value
        TextBox15.Text = "0" 'default value
        TextBox16.Text = "0" 'default value
        TextBox17.Text = "0" 'default value
        TextBox19.Text = "0" 'default value
        TextBox20.Text = "0" 'default value
        TextBox21.Text = "0" 'default value
        TextBox22.Text = "0" 'default value
    End Sub



    Private Sub WyczyœæToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WyczyœæToolStripMenuItem.Click
        TextBox2.Text = "0" 'default value
        TextBox3.Text = "0" 'default value
        TextBox4.Text = "0" 'default value
        TextBox5.Text = "0" 'default value
        TextBox6.Text = "0" 'default value
        TextBox7.Text = "0" 'default value
        TextBox8.Text = "0" 'default value
        TextBox9.Text = "0" 'default value
        TextBox10.Text = "0" 'default value
    End Sub

    Private Sub GroupBox2_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox2.Enter

    End Sub
End Class
