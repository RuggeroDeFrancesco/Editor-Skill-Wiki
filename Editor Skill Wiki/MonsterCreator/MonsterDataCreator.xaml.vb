Imports Newtonsoft.Json
Public Class MonsterDataCreator


    Dim languageEnum As Integer = 0
    Dim languageData As LanguageData
    Dim spellFolder As String
    Dim attackFolder As String


    Private Sub test_click(sender As Object, e As RoutedEventArgs)
        Try
            If spellFolder <> "" And attackFolder <> "" Then
                Dim rawData As String
                Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog
                OpenFileDialog1.Title = "Open File..."
                OpenFileDialog1.Multiselect = False
                OpenFileDialog1.Filter = "All Files|*.*"
                OpenFileDialog1.ShowDialog()
                Dim path As String = OpenFileDialog1.FileName
                If System.IO.File.Exists(path) Then
                    rawData = System.IO.File.ReadAllText(path)
                Else Throw New System.Exception("File does not exist.")
                End If

                'extract language data to get the correct monster name and description
                languageData = deserializeLanguageData(GetLanguageData())


                Dim parsedData = parseMonsterData(rawData)
                Dim finalData As New MonsterFinalData
                finalData.parseData(parsedData, attackFolder, spellFolder)
                Dim output = createModuleOutput(finalData)
                OutputBlock.Text = output
            Else
                MsgBox("Please select valid folders for Attacks and Spells.")
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try
    End Sub


    Private Function parseMonsterData(data As String)
        Dim parsedData As MonsterData
        parsedData = JsonConvert.DeserializeObject(Of MonsterData)(data)

        Return parsedData
    End Function



    Private Function createModuleOutput(data As MonsterFinalData)
        Dim output As String
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterData.txt"))

        output = output.Replace("nameText", getMonsterName(data.name, languageData))
        output = output.Replace("descriptionText", getMonsterDescription(data.name, languageData))
        output = output.Replace("strText", data.str)
        output = output.Replace("dexText", data.dex)
        output = output.Replace("intText", data.int)
        output = output.Replace("conText", data.cos)
        output = output.Replace("perText", data.per)
        output = output.Replace("chaText", data.cha)
        output = output.Replace("healthText", data.health)
        output = output.Replace("manaText", data.mana)
        output = output.Replace("accuracyText", data.accuracy)
        output = output.Replace("fortitudeText", data.fortitude)
        output = output.Replace("evasionText", data.evasion)
        output = output.Replace("willpowerText", data.willpower)

        output = output.Replace("slashText", data.slashArmor)
        output = output.Replace("slash%Text", data.slashArmorPerc)
        output = output.Replace("pierceText", data.pierceArmor)
        output = output.Replace("pierce%Text", data.pierceArmorPerc)
        output = output.Replace("crushText", data.crushArmor)
        output = output.Replace("crush%Text", data.crushArmorPerc)
        output = output.Replace("fireText", data.fireResistance)
        output = output.Replace("fire%Text", data.fireResistancePerc)
        output = output.Replace("iceText", data.coldResistance)
        output = output.Replace("ice%Text", data.coldResistancePerc)
        output = output.Replace("shockText", data.shockResistance)
        output = output.Replace("shock%Text", data.shockResistancePerc)
        output = output.Replace("magicText", data.magicResistance)
        output = output.Replace("magic%Text", data.magicResistancePerc)
        output = output.Replace("poisonText", data.poisonResistance)
        output = output.Replace("poison%Text", data.poisonResistancePerc)
        output = output.Replace("acidText", data.acidResistance)
        output = output.Replace("acid%Text", data.acidResistancePerc)


        Dim attackText As String = ""
        For Each attack As MonsterAttackFinalData In data.monsterAttacks
            'insert the right name into the fullText, using the language data
            attack.fullText = attack.fullText.Replace("textName", getAttackName(attack.name, languageData))
            attackText &= attack.fullText & vbCrLf
        Next

        output = output.Replace("attackSpace", attackText)

        Dim skillText As String = ""
        For Each skill As MonsterSpellFinalData In data.monsterSkills
            'insert the right name into the fullText, using the language data
            skill.fullText = skill.fullText.Replace("nameText", SpellDataCreator.GetTooltipName(skill.name, languageData, languageEnum))
            skillText &= skill.fullText & vbCrLf
        Next

        output = output.Replace("skillSpace", skillText)

        Return output
    End Function

    Private Function GetLanguageData()
        Dim rawLanguageData As String
        rawLanguageData = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "I2languages.json"))
        Return rawLanguageData
    End Function

    Private Function deserializeLanguageData(data As String) As LanguageData

        Dim parsedData As LanguageData
        Dim correctedData As String = data.Replace("\n", " ")
        parsedData = JsonConvert.DeserializeObject(Of LanguageData)(data)
        Return parsedData
    End Function

    Private Function getMonsterName(monsterName As String, deserializedData As LanguageData)
        Dim tooltipName As String
        Dim term As String

        term = monsterName.Replace("class_", "monsters/")
        term &= "Name"
        term = term.Remove(9, 1).Insert(9, Char.ToLower(term(9))) 'converts the 9th letter to lower case

        tooltipName = deserializedData.returnTerm(term, languageEnum)
        Return tooltipName

    End Function

    Private Function getMonsterDescription(monsterName As String, deserializedData As LanguageData)
        Dim description As String
        Dim term As String

        term = monsterName.Replace("class_", "monsters/")
        term &= "Description"
        term = term.Remove(10, 1).Insert(10, Char.ToLower(term(10))) 'converts the 10th letter to lower case

        description = deserializedData.returnTerm(term, languageEnum)
        Return description

    End Function

    Private Function getAttackName(attackName As String, deserializedData As LanguageData)
        Dim name As String
        Dim term As String

        term = attackName.Replace("key:", "")

        name = deserializedData.returnTerm(term, languageEnum)
        Return name
    End Function

    Private Sub selectAttacks_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            attackFolder = fbd.SelectedPath
            SelectedAttackFolder.Text = attackFolder
        End If
    End Sub

    Private Sub selectSpells_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            spellFolder = fbd.SelectedPath
            SelectedSpellFolder.Text = spellFolder
        End If
    End Sub
End Class
