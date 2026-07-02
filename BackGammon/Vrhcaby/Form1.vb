Public Class Form1
    Dim plocha(24) As ListBox 'Hrací plocha
    Dim vybranySloupec 'Číslo vybraného sloupce
    Dim kostka1 As Integer 'Hodnota kostky 1
    Dim kostka2 As Integer 'Hodnota kostky 2 
    Dim hrac1Tah = True 'Hráč1 má táh
    Dim PocetTahu = 2 'Počet tahů hráče (pokud hráč hýbne jedním kamenem pomocí jedné kostky, tak se sebere jeden tah; pokud hráč hýbne jedním kamenem pomocí součtu obou kostek, tak se seberou oba tahy a může hrát druhý hráč)
    Dim vyhozeneHrac1 = 0 'kameny hráče 1, které byli vyhozeny
    Dim vyhozeneHrac2 = 0 'kameny hráče 2, které byli vyhozeny
    Dim hotoveHrac1 = 0 'kameny hráče 1, které se dostali do cíle
    Dim hotoveHrac2 = 0 'kameny hráče 2, které se dostali do cíle

    'Načtení listboxů do pole pro lepší přístup a načtení kamenů na správná místa
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        plocha(0) = ListBox1
        plocha(1) = ListBox2
        plocha(2) = ListBox3
        plocha(3) = ListBox4
        plocha(4) = ListBox5
        plocha(5) = ListBox6
        plocha(6) = ListBox7
        plocha(7) = ListBox8
        plocha(8) = ListBox9
        plocha(9) = ListBox10
        plocha(10) = ListBox11
        plocha(11) = ListBox12
        plocha(12) = ListBox13
        plocha(13) = ListBox14
        plocha(14) = ListBox15
        plocha(15) = ListBox16
        plocha(16) = ListBox17
        plocha(17) = ListBox18
        plocha(18) = ListBox19
        plocha(19) = ListBox20
        plocha(20) = ListBox21
        plocha(21) = ListBox22
        plocha(22) = ListBox23
        plocha(23) = ListBox24


        plocha(0).Items.Add("Kamen1") : plocha(0).Items.Add("Kamen1")
        plocha(11).Items.Add("Kamen1") : plocha(11).Items.Add("Kamen1") : plocha(11).Items.Add("Kamen1") : plocha(11).Items.Add("Kamen1") : plocha(11).Items.Add("Kamen1")
        plocha(16).Items.Add("Kamen1") : plocha(16).Items.Add("Kamen1") : plocha(16).Items.Add("Kamen1")
        plocha(18).Items.Add("Kamen1") : plocha(18).Items.Add("Kamen1") : plocha(18).Items.Add("Kamen1") : plocha(18).Items.Add("Kamen1") : plocha(18).Items.Add("Kamen1")

        plocha(23).Items.Add("Kamen2") : plocha(23).Items.Add("Kamen2")
        plocha(12).Items.Add("Kamen2") : plocha(12).Items.Add("Kamen2") : plocha(12).Items.Add("Kamen2") : plocha(12).Items.Add("Kamen2") : plocha(12).Items.Add("Kamen2")
        plocha(7).Items.Add("Kamen2") : plocha(7).Items.Add("Kamen2") : plocha(7).Items.Add("Kamen2")
        plocha(5).Items.Add("Kamen2") : plocha(5).Items.Add("Kamen2") : plocha(5).Items.Add("Kamen2") : plocha(5).Items.Add("Kamen2") : plocha(5).Items.Add("Kamen2")
    End Sub


    'Hození kostkou
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Randomize()
        kostka1 = Int(Rnd() * 6) + 1
        kostka2 = Int(Rnd() * 6) + 1

        Label1.Text = kostka1
        Label2.Text = kostka2

        GroupBox3.Enabled = False
        GroupBox4.Enabled = True

        If Label1.Text = Label2.Text Then
            PocetTahu = 4
            Button2.Enabled = False
            Button3.Enabled = False
            Button4.Enabled = False
            Button7.Enabled = True
        Else
            PocetTahu = 2
            Button2.Enabled = True
            Button3.Enabled = True
            Button4.Enabled = True
            Button7.Enabled = False
        End If


        Label14.Text = PocetTahu
        Button5.Enabled = True
    End Sub

    'Posun kostkou1
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim posunuto = False

        If hrac1Tah Then
            posunuto = MovePieceForward("Kamen1", kostka1)
        Else
            posunuto = MovePieceBackward("Kamen2", kostka1)
        End If

        If posunuto = True Then
            PocetTahu = PocetTahu - 1
            Button2.Enabled = False
            Button4.Enabled = False
        End If

        TurnCheck()
    End Sub

    'Posun kostkou2
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Dim posunuto

        If hrac1Tah Then
            posunuto = MovePieceForward("Kamen1", kostka2)
        Else
            posunuto = MovePieceBackward("Kamen2", kostka2)
        End If

        If posunuto = True Then
            PocetTahu = PocetTahu - 1
            Button3.Enabled = False
            Button4.Enabled = False
        End If

        TurnCheck()
    End Sub

    'Posun oběma kostkami
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Dim posunuto

        If hrac1Tah Then
            posunuto = MovePieceForward("Kamen1", kostka2 + kostka1)
        Else
            posunuto = MovePieceBackward("Kamen2", kostka2 + kostka1)
        End If

        If posunuto = True Then
            PocetTahu = PocetTahu - 2
        End If

        TurnCheck()
    End Sub

    'Obnovení kamene
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        Dim obnoveno = False

        If hrac1Tah Then
            If vyhozeneHrac1 = 0 Then Exit Sub
            obnoveno = RecoverPiece("Kamen1", 0)
        Else
            If vyhozeneHrac2 = 0 Then Exit Sub
            obnoveno = RecoverPiece("Kamen2", 23)
        End If

        If obnoveno Then
            PocetTahu = PocetTahu - 1
            RemoveBarPiece()
            Button5.Enabled = False
        End If

        TurnCheck()
    End Sub

    'Ukončení tahu předčasně
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        PlayerSwitch()
    End Sub

    'Posouvání kamenů, když jsou obě kostky stejné
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        Dim posunuto

        If hrac1Tah Then
            posunuto = MovePieceForward("Kamen1", kostka1)
        Else
            posunuto = MovePieceBackward("Kamen2", kostka1)
        End If

        If posunuto = True Then
            PocetTahu = PocetTahu - 1
        End If

        TurnCheck()
    End Sub

    Function RecoverPiece(Kamen As String, Index As Integer)
        Dim obnoveno = False

        'Přidá se 
        If plocha(Index).Items.Count = 0 Then
            plocha(Index).Items.Add(Kamen)
            obnoveno = True
            Return obnoveno
        End If

        'Neobnoví se protože tam je 2 a více nepřáteldkých kamenů
        If plocha(Index).Items.Count >= 2 And plocha(Index).Items(0) IsNot Kamen Then

            MsgBox("Není možné obnovit kámen, pokud je startovní sloupec okupován dvěmi a více kameny druhého hráče", , "Chyba - Nelze obnovit kámen")
            obnoveno = False
            Return obnoveno
        End If

        'Vyhození nepřátelského kamene, jestli je jeden a následné obnovení
        If plocha(obnoveno).Items.Count = 1 And plocha(Index).Items(0) IsNot Kamen Then
            plocha(Index).Items.Clear()
            AddBarPiece()
            plocha(Index).Items.Add(Kamen)
            obnoveno = True
            Return obnoveno
        End If

        'Přidá se 
        If plocha(Index).Items.Count > 0 And plocha(Index).Items(0) Is Kamen Then
            plocha(Index).Items.Add(Kamen)
            obnoveno = True
            Return obnoveno
        End If

        Return obnoveno
    End Function

    'Metoda na posunutí kamene
    Function MovePieceForward(Kamen As String, Kostka As Integer)
        Dim posunuto = False

        'Jestli je prázdný sloupec, metoda se ukončí
        If plocha(vybranySloupec).Items.Count() = 0 Then
            MsgBox("Je vybrán prázdný sloupec", , "Chyba - Špatně vybraný sloupec")
            Return posunuto
        End If

        'Jestli se jedná o nepřátelský kámen, tak se nebude kamenem hýbat
        If plocha(vybranySloupec).Items(0) IsNot Kamen Then
            MsgBox("Je vybrán sloupec s kameny druhého hráče", , "Chyba - Špatně vybraný sloupec")
            Return posunuto
        End If

        'Kontroluje se jestli je kámen dostane mimo hrací pole; to znamená výherní bod pro hráče
        If vybranySloupec + Kostka >= 24 Then
            RemovePiece(vybranySloupec)
            AddWinPiece()
            posunuto = True
            Return posunuto
        End If

        'Posune se na prázndé místo
        If plocha(vybranySloupec + Kostka).Items.Count = 0 Then
            RemovePiece(vybranySloupec)
            plocha(vybranySloupec + Kostka).Items.Add(Kamen)
            posunuto = True
            Return posunuto
        End If

        'Posune se na místo, kde je jeden společný kámen
        If plocha(vybranySloupec + Kostka).Items.Count >= 1 And plocha(vybranySloupec + Kostka).Items(0) Is Kamen Then
            RemovePiece(vybranySloupec)
            plocha(vybranySloupec + Kostka).Items.Add(Kamen)
            posunuto = True
            Return posunuto
        End If

        'Neposune se protože tam je 2 a více nepřáteldkých kamenů
        If plocha(vybranySloupec + Kostka).Items.Count >= 2 And plocha(vybranySloupec + Kostka).Items(0) IsNot Kamen Then
            MsgBox("Kámen nejde posunout, protože cílový sloupec obsahuje 2 a více nepřátelských kamenů", , "Chyba - Špatně vybraný sloupec")
            posunuto = False
            Return posunuto
        End If

        'Vyhození nepřátelského kamene, jestli je jeden a následné přesunutí
        If plocha(vybranySloupec + Kostka).Items.Count = 1 And plocha(vybranySloupec + Kostka).Items(0) IsNot Kamen Then
            plocha(vybranySloupec + Kostka).Items.Clear()
            AddBarPiece()
            RemovePiece(vybranySloupec)
            plocha(vybranySloupec + Kostka).Items.Add(Kamen)
            posunuto = True
            Return posunuto
        End If

        Return posunuto
    End Function

    'Metoda na posunutí kamene dozadu
    Function MovePieceBackward(Kamen As String, Kostka As Integer)
        Dim posunuto = False

        If plocha(vybranySloupec).Items.Count() = 0 Then
            MsgBox("Je vybrán prázdný sloupec", , "Chyba - Špatně vybraný sloupec")
            Return posunuto 'Jestli je prázdný sloupec, metoda se ukončí
        End If

        If plocha(vybranySloupec).Items(0) IsNot Kamen Then
            MsgBox("Je vybrán sloupec s kameny druhého hráče", , "Chyba - Špatně vybraný sloupec")
            Return posunuto
        End If

        'Kontroluje se jestli je kámen dostane mimo hrací pole; to znamená výherní bod pro hráče
        If vybranySloupec - Kostka < 0 Then
            RemovePiece(vybranySloupec)
            AddWinPiece()
            posunuto = True
            Return posunuto
        End If

        'Posune se na prázndé místo
        If plocha(vybranySloupec - Kostka).Items.Count = 0 Then
            RemovePiece(vybranySloupec)
            plocha(vybranySloupec - Kostka).Items.Add(Kamen)
            posunuto = True
            Return posunuto
        End If

        'Posune se na místo, kde je jeden společný kámen
        If plocha(vybranySloupec - Kostka).Items.Count >= 1 And plocha(vybranySloupec - Kostka).Items(0) Is Kamen Then
            RemovePiece(vybranySloupec)
            plocha(vybranySloupec - Kostka).Items.Add(Kamen)
            posunuto = True
            Return posunuto
        End If

        'Neposune se protože tam je 2 a více nepřáteldkých kamenů
        If plocha(vybranySloupec - Kostka).Items.Count >= 2 And plocha(vybranySloupec - Kostka).Items(0) IsNot Kamen Then
            MsgBox("Kámen nejde posunout, protože cílový sloupec obsahuje 2 a více nepřátelských kamenů", , "Chyba - Špatně vybraný sloupec")
            posunuto = False
            Return posunuto
        End If

        'Vyhození nepřátelskéhe kamene a následné přesunutí
        If plocha(vybranySloupec - Kostka).Items.Count = 1 And plocha(vybranySloupec - Kostka).Items(0) IsNot Kamen Then
            plocha(vybranySloupec - Kostka).Items.Clear()
            AddBarPiece()
            RemovePiece(vybranySloupec)
            plocha(vybranySloupec - Kostka).Items.Add(Kamen)
            posunuto = True
            Return posunuto
        End If

        Return posunuto
    End Function

    'Přepnutí hráčů
    Function PlayerSwitch()
        WinCheck()

        If hrac1Tah Then
            hrac1Tah = False
        Else
            hrac1Tah = True
        End If

        GroupBox3.Enabled = True
        GroupBox4.Enabled = False

        'Vyzuální prvky pro identifikaci kola
        If CheckBox1.Checked = True Then
            CheckBox1.Checked = False
            CheckBox2.Checked = True
            PictureBox1.Visible = False
            PictureBox26.Visible = True
        Else
            CheckBox1.Checked = True
            CheckBox2.Checked = False
            PictureBox1.Visible = True
            PictureBox26.Visible = False
        End If
    End Function

    'Kontroluje počet tahů
    Function TurnCheck()
        Label14.Text = PocetTahu
        If PocetTahu = 0 Then
            PlayerSwitch()
        End If
    End Function

    'Kontrola výhry
    Function WinCheck()
        Dim hotovo = False
        Dim save = 0
        If hotoveHrac1 >= 15 Then
            save = MsgBox("Chcete uložit záznam hry?", vbQuestion + vbYesNo + vbDefaultButton2, "Vyhrává hráč1")
            hotovo = True
        ElseIf hotoveHrac2 >= 15 Then
            save = MsgBox("Chcete uložit záznam hry?", vbQuestion + vbYesNo + vbDefaultButton2, "Vyhrává hráč2")
            hotovo = True
        End If

        If Str(save) = 6 Then SaveGame()

        If hotovo Then Application.Restart()
    End Function

    'Přidává +1 ke kamenům v cíly
    Function AddWinPiece()
        If hrac1Tah Then
            hotoveHrac1 = hotoveHrac1 + 1
            Label7.Text = hotoveHrac1
        Else
            hotoveHrac2 = hotoveHrac2 + 1
            Label11.Text = hotoveHrac2
        End If
    End Function

    'Přidává +1 k vyhozeným kamenům
    Function AddBarPiece()
        If hrac1Tah Then
            vyhozeneHrac2 = vyhozeneHrac2 + 1
            Label12.Text = vyhozeneHrac2
        Else
            vyhozeneHrac1 = vyhozeneHrac1 + 1
            Label8.Text = vyhozeneHrac1
        End If
    End Function

    'Přidává -1 k vyhozeným kamenům
    Function RemoveBarPiece()
        If hrac1Tah Then
            vyhozeneHrac1 = vyhozeneHrac1 - 1
            Label8.Text = vyhozeneHrac1
        Else
            vyhozeneHrac2 = vyhozeneHrac2 - 1
            Label12.Text = vyhozeneHrac2
        End If
    End Function

    'Odstraní kámen ve sloupci na pozici 0 
    Function RemovePiece(index)
        plocha(index).Items.RemoveAt(0)
    End Function

    Function SaveGame()
        SaveFileDialog1.ShowDialog()

        FileOpen(1, SaveFileDialog1.FileName, OpenMode.Output)
        WriteLine(1, "'Hráč1': {")
        WriteLine(1, "      'Kameny v cíly: '" + Str(hotoveHrac1))
        WriteLine(1, "      'Kameny na baru: '" + Str(vyhozeneHrac1))
        WriteLine(1, "}")
        WriteLine(1, "'Hráč2': {")
        WriteLine(1, "      'Kameny v cíly: '" + Str(hotoveHrac2))
        WriteLine(1, "      'Kameny na baru: '" + Str(vyhozeneHrac2))
        WriteLine(1, "}")
        WriteLine(1)
        WriteLine(1, "Hra byla hrána v: " + Now.ToString)
        WriteLine(1, "____________________________________________________________________________")
        WriteLine(1)
        FileClose(1)
    End Function

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Dim odpoved = MsgBox("Opravdu chcete resetovat hru?", vbQuestion + vbYesNo + vbDefaultButton2, "Restart hry")
        If Str(odpoved) = 6 Then Application.Restart()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        Dim save
        save = MsgBox("Chcete uložit záznam hry?", vbQuestion + vbYesNo + vbDefaultButton2, "Vyhrává hráč2")
        If Str(save) = 6 Then SaveGame()
    End Sub


    'Metody, když se změní radio button, pomocí kterého se vybírá sloupec
    Private Sub RadioButton1_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton1.CheckedChanged
        If RadioButton1.Checked Then
            vybranySloupec = 0
        End If
    End Sub

    Private Sub RadioButton2_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton2.CheckedChanged
        If RadioButton2.Checked Then
            vybranySloupec = 1
        End If
    End Sub

    Private Sub RadioButton3_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton3.CheckedChanged
        If RadioButton3.Checked Then
            vybranySloupec = 2
        End If
    End Sub

    Private Sub RadioButton4_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton4.CheckedChanged
        If RadioButton4.Checked Then
            vybranySloupec = 3
        End If
    End Sub

    Private Sub RadioButton5_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton5.CheckedChanged
        If RadioButton5.Checked Then
            vybranySloupec = 4
        End If
    End Sub

    Private Sub RadioButton6_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton6.CheckedChanged
        If RadioButton6.Checked Then
            vybranySloupec = 5
        End If
    End Sub

    Private Sub RadioButton7_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton7.CheckedChanged
        If RadioButton7.Checked Then
            vybranySloupec = 6
        End If
    End Sub

    Private Sub RadioButton8_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton8.CheckedChanged
        If RadioButton8.Checked Then
            vybranySloupec = 7
        End If
    End Sub

    Private Sub RadioButton9_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton9.CheckedChanged
        If RadioButton9.Checked Then
            vybranySloupec = 8
        End If
    End Sub

    Private Sub RadioButton10_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton10.CheckedChanged
        If RadioButton10.Checked Then
            vybranySloupec = 9
        End If
    End Sub

    Private Sub RadioButton11_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton11.CheckedChanged
        If RadioButton11.Checked Then
            vybranySloupec = 10
        End If
    End Sub

    Private Sub RadioButton12_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton12.CheckedChanged
        If RadioButton12.Checked Then
            vybranySloupec = 11
        End If
    End Sub

    Private Sub RadioButton13_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton13.CheckedChanged
        If RadioButton13.Checked Then
            vybranySloupec = 12
        End If
    End Sub

    Private Sub RadioButton14_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton14.CheckedChanged
        If RadioButton14.Checked Then
            vybranySloupec = 13
        End If
    End Sub

    Private Sub RadioButton15_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton15.CheckedChanged
        If RadioButton15.Checked Then
            vybranySloupec = 14
        End If
    End Sub

    Private Sub RadioButton16_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton16.CheckedChanged
        If RadioButton16.Checked Then
            vybranySloupec = 15
        End If
    End Sub

    Private Sub RadioButton17_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton17.CheckedChanged
        If RadioButton17.Checked Then
            vybranySloupec = 16
        End If
    End Sub

    Private Sub RadioButton18_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton18.CheckedChanged
        If RadioButton18.Checked Then
            vybranySloupec = 17
        End If
    End Sub

    Private Sub RadioButton19_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton19.CheckedChanged
        If RadioButton19.Checked Then
            vybranySloupec = 18
        End If
    End Sub

    Private Sub RadioButton20_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton20.CheckedChanged
        If RadioButton20.Checked Then
            vybranySloupec = 19
        End If
    End Sub

    Private Sub RadioButton21_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton21.CheckedChanged
        If RadioButton21.Checked Then
            vybranySloupec = 20
        End If
    End Sub

    Private Sub RadioButton22_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton22.CheckedChanged
        If RadioButton22.Checked Then
            vybranySloupec = 21
        End If
    End Sub

    Private Sub RadioButton23_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton23.CheckedChanged
        If RadioButton23.Checked Then
            vybranySloupec = 22
        End If
    End Sub

    Private Sub RadioButton24_CheckedChanged(sender As Object, e As EventArgs) Handles RadioButton24.CheckedChanged
        If RadioButton24.Checked Then
            vybranySloupec = 23
        End If
    End Sub
End Class
