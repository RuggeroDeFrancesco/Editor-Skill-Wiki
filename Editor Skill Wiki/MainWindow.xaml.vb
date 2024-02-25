Class MainWindow

    Dim commonFunctions As CommonFunctions

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        System.Globalization.CultureInfo.CurrentCulture = New Globalization.CultureInfo("en-US", False)
        commonFunctions = New CommonFunctions
    End Sub

    Private Sub GenerateSpellData(sender As Object, e As RoutedEventArgs)
        Try
            Dim spellDataWindow As New SpellDataWindow(commonFunctions)
            spellDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Private Sub GenerateMonsterSpells(sender As Object, e As RoutedEventArgs)
        Try
            Dim monsterSpellDataWindow As New MonsterDataCreator(commonFunctions)
            monsterSpellDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Private Sub OpenLog()
        Dim logWindow As New Log(commonFunctions)
        logWindow.Show()
    End Sub

    Private Sub ExitApp()
        System.Windows.Application.Current.Shutdown()
    End Sub

    Private Sub CheckBox_Checked(sender As Object, e As RoutedEventArgs)
        My.Settings.json = sender.ischecked
    End Sub
End Class
