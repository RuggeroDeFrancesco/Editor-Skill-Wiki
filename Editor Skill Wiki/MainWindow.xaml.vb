Class MainWindow



    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'Dim path As String = "E:\Desktop\Miscellanea\Fractured\Spell\MonoBehaviour\spell_AbsorbElements.json"
        'Dim spellDataCreator As New SpellDataCreator()
        'spellDataCreator.parseData(path)
    End Sub

    Private Sub GenerateSpellData(sender As Object, e As RoutedEventArgs)
        Try
            Dim spellDataWindow As New SpellDataWindow
            spellDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Private Sub GenerateMonsterSpells(sender As Object, e As RoutedEventArgs)
        Try
            Dim monsterSpellDataWindow As New MonsterDataCreator()
            monsterSpellDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Private Sub GenerateItemData(sender As Object, e As RoutedEventArgs)
        Try
            Dim ItemDataWindow As New ItemDataWindow()
            ItemDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

End Class
