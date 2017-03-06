Imports mysql.data.MySqlClient

Public Class Form1
    Dim tilkobling As MySqlConnection
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'tilkobling = New MySqlConnection("server=mysql.stud.iie.ntnu.no;database=evenmof;uid=evenmof;Pwd=gyzQJzDq")
        'tilkobling.Open
        'Dim filnummer = FreeFile()
        'FileOpen(filnummer, "eksport.txt", OpenMode.Input)
        'Dim linjenummer = 0
        'While Not EOF(filnummer)
        '    Dim linje = LineInput(filnummer)
        '    linjenummer += 1
        '    If linjenummer > 1 Then
        '        ' MsgBox(linje)
        '        Dim linjeOppdetlt = linje.Split(", ")
        '        Dim navn = linjeOppdetlt(0)
        '        'MsgBox(navn)
        '        Dim navnOppdelt = navn.Split(" ")
        '        Dim etternavn = navnOppdelt(navnOppdelt.Length - 1)
        '        'MsgBox(etternavn)
        '        Dim fornavn = navnOppdelt(0)
        '        For indeks = 1 To navnOppdelt.Length - 2
        '            fornavn &= " " & navnOppdelt(indeks)
        '        Next
        '        MsgBox(fornavn)
        '        Dim sql As New MySqlCommand("insert into ansatte (fornavn, etternavn) values (@fornavn, @etternavn)")
        '        sql.Parameters.AddWithValue("@fornavn", fornavn)
        '        sql.Parameters.AddWithValue("@etternavn", etternavn)

        '        Dim ansatt_id = sql.LastInsertedId
        '        For indeks = 1 To linjeOppdetlt.Length - 1
        '            Dim telefonnummer = linjeOppdetlt(indeks)
        '            Dim telefonnummerOppdelt = telefonnummer.Split(" ")
        '            Dim telefonnummerUtenMellomrom = ""
        '            For Each element In telefonnummerOppdelt
        '                telefonnummerUtenMellomrom &= element

        '            Next
        '            Dim sql2 As New MySqlCommand("insert into telefonnummere ( telefonnummer, ansatt_id) values (@telefonnummer, @ansatt_id)", tilkobling)
        '            sql2.Parameters.AddWithValue("@telefonnummer", telefonnummerUtenMellomrom)
        '            sql2.Parameters.AddWithValue("@ansattID", ansatt_id)
        '            sql2.ExecuteNonQuery()
        '        Next
        '    End If

        ' End While
        ' FileClose(filnummer)
        'database:
        'tabell: ansatte
        'ID ( primærnøkkel, auto-increment)
        'fornavn
        'etternavn
        'tabell: telefonnumre
        'telefonnummer
        'ansattID
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        tilkobling = New MySqlConnection("server=mysql.stud.iie.ntnu.no;database=evenmof;uid=evenmof;Pwd=gyzQJzDq")
        tilkobling.Open()
        Dim filnummer1 = FreeFile()
        FileOpen(filnummer1, "Postnummerregister_ansi.txt", OpenMode.Input)
        Dim linjenummer1 = 0
        While Not EOF(filnummer1)
            Dim linje1 = LineInput(filnummer1)
            Dim linjeoppdelt1 = linje1.Split(vbTab)
            Dim postnr = linjeoppdelt1(0)
            Dim poststed = linjeoppdelt1(1)
            Dim sql As New MySqlCommand("insert into postnummer (post_nr, poststed) values (@post_nr, @poststed)", tilkobling)
            sql.Parameters.AddWithValue("@post_nr", postnr)
            sql.Parameters.AddWithValue("@postSted", poststed)
            sql.ExecuteNonQuery()
            'tilkobling.Close()
            'Dim sql2 As New MySqlCommand("insert into poststed (poststed, postnummer) values (@poststed, @postnr)", tilkobling)
            'sql2.Parameters.AddWithValue("@postnr", postnr)
            'sql2.Parameters.AddWithValue("@postSted", poststed)

            'sql2.ExecuteNonQuery()


        End While
        tilkobling.Close()
        MsgBox("done!")
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim sok As Integer
        Try
            If Convert.ToDouble(TextBox1.Text) Then
                sok = TextBox1.Text
            End If
        Catch ex As Exception
            MsgBox("skriv tall!")
        End Try
        tilkobling = New MySqlConnection("server=mysql.stud.iie.ntnu.no;database=evenmof;uid=evenmof;Pwd=gyzQJzDq")
        tilkobling.Open()
        Dim sporring As New MySqlCommand("SELECT poststed FROM postnummer WHERE post_nr = @soktall", tilkobling)
        sporring.Parameters.AddWithValue("@soktall", sok)
        Dim leser = sporring.ExecuteReader()
        While leser.Read()
            ListBox1.Items.Add(leser("poststed"))
        End While
        leser.Close()
        tilkobling.Close()
    End Sub
End Class
