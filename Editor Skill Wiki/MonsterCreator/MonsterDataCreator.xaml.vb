Imports Newtonsoft.Json
Public Class MonsterDataCreator


    Dim languageEnum As Integer = 1
    Dim languageData As LanguageData
    Dim spellFolder As String
    Dim attackFolder As String
    Dim wpFolder As String
    Dim enchantFolder As String


    Private Sub test_click(sender As Object, e As RoutedEventArgs)
        Try
            If spellFolder <> "" And attackFolder <> "" Then
                Dim rawData As String
                Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog
                OpenFileDialog1.Title = "Open File..."
                OpenFileDialog1.Multiselect = True
                OpenFileDialog1.Filter = "All Files|*.*"
                OpenFileDialog1.ShowDialog()

                'extract language data to get the correct monster name and description
                languageData = deserializeLanguageData(GetLanguageData())
                Dim output As String = ""
                Dim assets As AssetItemsList = MainWindow.GetAssets
                For Each file As String In OpenFileDialog1.FileNames
                    Dim Path As String = file
                    If System.IO.File.Exists(Path) Then
                        rawData = System.IO.File.ReadAllText(Path)
                        Dim parsedData = parseMonsterData(rawData)
                        Dim finalData As New MonsterFinalData(assets)
                        finalData.parseData(parsedData, attackFolder, spellFolder, wpFolder, enchantFolder, languageData, languageEnum)
                        output &= createModuleOutput(finalData)
                        output &= vbCrLf
                        output &= vbCrLf
                    Else Throw New System.Exception("File does not exist.")
                    End If
                Next

                OutputBlock.Text = output

            Else
                MsgBox("Please select valid folders for Attacks and Spells.")
            End If
        Catch ex As Exception
            If ex.Message = "The file selected is not a creature." Then
                MsgBox(ex.Message)
            Else
                MsgBox(ex.Message)
                MsgBox(ex.StackTrace)
            End If
        End Try
    End Sub


    Shared Function parseMonsterData(data As String) 'used also by the loot creator
        Dim parsedData As MonsterData
        parsedData = JsonConvert.DeserializeObject(Of MonsterData)(data)

        Return parsedData
    End Function



    Private Function createModuleOutput(data As MonsterFinalData)
        If data.name = "Dummy" Or
            data.name = "class_NPC " Or
            data.name = "TotemDecay" Or data.name = "TotemThunder" Or data.name = "TotemWildfire" Or data.name = "TotemWinter" Then
            Return ""
        End If
        Dim output As String
        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterData.txt"))
        Dim monsterName As String = ""
        Try
            monsterName = getMonsterName(data.name, languageData, languageEnum)
            output = output.Replace("nameText", monsterName)
        Catch ex As Exception
            If ex.Message = "No Match" And data.challengeRating = 10 Then
                'this is an old Myr legend, skip
                Return ""
            End If
        End Try
        output = output.Replace("descriptionText", getMonsterDescription(data.name, languageData))

        output = output.Replace("imageText", "Env " & monsterName & ".png")

        output = output.Replace("challengeRatingText", data.challengeRating)
        output = output.Replace("difficultyText", data.difficulty)
        output = output.Replace("halfKillsRequiredText", data.halfKillsRequired)
        output = output.Replace("monsterRaceText", data.monsterRace)
        output = output.Replace("capturableText", data.capturable)
        output = output.Replace("skinAmountText", data.skinAmount)
        output = output.Replace("petItemTypeText", data.petItemType)
        output = output.Replace("championText", data.champion)

        output = output.Replace("walkSpeedText", data.walkSpeed)
        output = output.Replace("baseSpeedText", data.baseSpeed)

        output = output.Replace("strText", data.str)
        output = output.Replace("dexText", data.dex)
        output = output.Replace("intText", data.int)
        output = output.Replace("conText", data.cos)
        output = output.Replace("perText", data.per)
        output = output.Replace("chaText", data.cha)
        output = output.Replace("healthText", data.health)
        output = output.Replace("healthRegenText", data.healthRegen)
        output = output.Replace("manaText", data.mana)
        output = output.Replace("manaRegenText", data.manaRegen)
        output = output.Replace("accuracyText", data.accuracy)
        output = output.Replace("fortitudeText", data.fortitude)
        output = output.Replace("evasionText", data.evasion)
        output = output.Replace("willpowerText", data.willpower)
        output = output.Replace("stealthText", data.stealth)
        output = output.Replace("detectionText", data.detection)
        output = output.Replace("luckText", data.luck)
        output = output.Replace("criticalChanceText", data.criticalChance)
        output = output.Replace("criticalDamageText", data.criticalDamage)

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
        output = output.Replace("immunityGroupText", getEffectGroupName(data.immunityEffectGroup, languageData))
        output = output.Replace("specialImmunityText", data.specialImmunity)
        output = output.Replace("magicalDamageReflectionText", data.magicalDamageReflection)
        output = output.Replace("physicalDamageReflectionText", data.physicalDamageReflection)

        output = output.Replace("slashDamageIncreaseText", data.slashDamageIncrease)
        output = output.Replace("pierceDamageIncreaseText", data.pierceDamageIncrease)
        output = output.Replace("crushDamageIncreaseText", data.crushDamageIncrease)
        output = output.Replace("fireDamageIncreaseText", data.fireDamageConversion)
        output = output.Replace("iceDamageIncreaseText", data.iceDamageConversion)
        output = output.Replace("shockDamageIncreaseText", data.shockDamageConversion)
        output = output.Replace("acidDamageIncreaseText", data.acidDamageConversion)
        output = output.Replace("energyDamageIncreaseText", data.energyDamageConversion)
        output = output.Replace("poisonDamageIncreaseText", data.poisonDamageConversion)
        output = output.Replace("acidDamageConversionText", data.acidDamageConversion)
        output = output.Replace("poisonDamageConversionText", data.poisonDamageConversion)
        output = output.Replace("energyDamageConversionText", data.energyDamageConversion)
        output = output.Replace("fireDamageConversionText", data.fireDamageConversion)
        output = output.Replace("iceDamageConversionText", data.iceDamageConversion)
        output = output.Replace("shockDamageConversionText", data.shockDamageConversion)
        output = output.Replace("acidDamageConversionText", data.acidDamageConversion)
        output = output.Replace("poisonDamageConversionText", data.poisonDamageConversion)
        output = output.Replace("energyDamageConversionText", data.energyDamageConversion)
        output = output.Replace("monsterPoisonStacksText", data.monsterPoisonStack)
        output = output.Replace("cooldownReductionText", data.cooldownReduction)
        output = output.Replace("spellDamageIncreaseText", data.spellDamageIncrease)

        output = output.Replace("broadcastAggressionText", data.broadcastAggression)
        output = output.Replace("playerAttitudeText", data.playerAttitude)
        output = output.Replace("combatStanceText", data.combatStance)
        output = output.Replace("categoryText", data.category)

        Dim statusImmunityText As String = ""

        For Each statusImmunity As String In data.immunityStatusEffectList
            statusImmunityText &= getStatusEffectName(statusImmunity, languageData)
            statusImmunityText &= ", "
        Next

        If statusImmunityText.Count <> 0 Then
            statusImmunityText = statusImmunityText.Substring(0, (statusImmunityText.Count - 2))
        End If

        output = output.Replace("immunitySpace", statusImmunityText)

        Dim damageAbsorbedText As String = ""

        For Each damage As String In data.damageAbsorbed
            damageAbsorbedText &= damage
            damageAbsorbedText &= ", "
        Next

        If data.damageAbsorbed.Count <> 0 Then
            damageAbsorbedText = damageAbsorbedText.Substring(0, (damageAbsorbedText.Count - 2))
        End If

        output = output.Replace("absorbSpace", damageAbsorbedText)

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
            skill.fullText = skill.fullText.Replace("nameText", SpellDataCreator.GetTooltipName(skill.spellKey, languageData, languageEnum))
            skillText &= skill.fullText & vbCrLf
        Next

        output = output.Replace("skillSpace", skillText)

        Dim allowedPoiText As String = ""

        For Each poi As String In data.allowedPoi
            allowedPoiText &= getPoiName(poi, languageData)
            allowedPoiText &= ", "
        Next

        If allowedPoiText.Count <> 0 Then
            allowedPoiText = allowedPoiText.Substring(0, (allowedPoiText.Count - 2))
        End If

        output = output.Replace("allowedPoiSpace", allowedPoiText)

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

    Shared Function getMonsterName(monsterName As String, deserializedData As LanguageData, language As Integer) 'used by loot creator
        Dim tooltipName As String
        Dim term As String

        term = monsterName.Replace("class_", "monsters/")
        term &= "Name"
        term = term.Replace("_Tutorial_", "") 'the name of tutorial monsters does not cointain the "tutorial" part
        term = term.Replace("_", "") 'some monster names have a _ in the name, which isn't found in the l2language terms. It has to be removed

        'list of exceptions
        If term.IndexOf("EarthElementalGreater") <> -1 Then
            term = term.Replace("EarthElementalGreater", "GreaterEarthElemental")
        End If

        If term.IndexOf("ForestTroll") <> -1 Then
            If term.IndexOf("Legend") = -1 Then
                term = term.Replace("ForestTroll", "TrollForest")
            Else
                term = term.Replace("Legend", "Champion")
            End If

        End If
        If term.IndexOf("MountainTroll") <> -1 Then
            If term.IndexOf("Legend") = -1 Then
                term = term.Replace("MountainTroll", "TrollMountain")
            Else
                term = term.Replace("Legend", "Champion")
            End If
        End If

        If term.IndexOf("SeaTroll") <> -1 Then
            If term.IndexOf("Legend") = -1 Then
                term = term.Replace("SeaTroll", "TrollSea")
            Else
                term = term.Replace("Legend", "Champion")
            End If
        End If

        If term.IndexOf("Swordman") <> -1 Then
            term = term.Replace("Swordman", "Swordsman")
        End If
        If term.IndexOf("FrostShot") <> -1 Then
            term = term.Replace("FrostShot", "Archer")
        End If
        If term.IndexOf("ShamanDeath") <> -1 Then
            term = term.Replace("ShamanDeath", "DeathShaman")
        End If
        If term.IndexOf("ShamanFire") <> -1 Then
            term = term.Replace("ShamanFire", "FireShaman")
        End If
        If term.IndexOf("ShamanStorm") <> -1 Then
            term = term.Replace("ShamanStorm", "StormShaman")
        End If
        If term.IndexOf("ShamanFrost") <> -1 Then
            term = term.Replace("ShamanFrost", "FrostShaman")
        End If
        If term.IndexOf("GrizzlyBear") <> -1 Then
            term = term.Replace("GrizzlyBear", "Bear")
        End If
        If term.IndexOf("WoodlandWispLegend") <> -1 Then
            term = term.Replace("WoodlandWispLegend", "wispChampion")
        End If
        If term.IndexOf("Totem") <> -1 Then
            term = term.Replace("monsters/", "")
            term = term.Replace("Name", "")
            Return term
        End If
        If term.IndexOf("Dummy") <> -1 Then
            term = term.Replace("monsters/", "")
            term = term.Replace("Name", "")
            Return term
        End If


        tooltipName = deserializedData.returnTerm(term, language)
        If tooltipName = "" Then
            If monsterName.IndexOf("Legend") <> -1 Then 'could be an old legend, which used champion instead of legend
                term = term.Replace("Legend", "Champion")
                tooltipName = deserializedData.returnTerm(term, language)
                If tooltipName = "" Then
                    Throw New Exception("No Match")
                End If
            Else
                Throw New Exception("No Match")
            End If
        End If
        Return tooltipName

    End Function

    Private Function getEffectGroupName(effectGroupName As Integer, deserializedData As LanguageData)
        Dim term As String = "spells/statusType_" & [Enum].GetName(GetType(EffectGroup), effectGroupName)
        Return deserializedData.returnTerm(term, languageEnum)
    End Function

    Private Function getStatusEffectName(statusEffectName As String, deserializedData As LanguageData)
        Dim name As String

        Dim term As String = statusEffectName.Replace("se_", "status_effect/")
        term &= "_name"

        name = deserializedData.returnTerm(term, languageEnum)
        Return name
    End Function

    Private Function getPoiName(poiName As String, deserializedData As LanguageData)
        Dim name As String

        Dim term As String = "ui/worldMap_POI_" & poiName

        name = deserializedData.returnTerm(term, languageEnum)
        Return name
    End Function

    Private Function getMonsterDescription(monsterName As String, deserializedData As LanguageData)
        Dim description As String
        Dim term As String

        term = monsterName.Replace("class_", "monsters/")
        term &= "Description"
        term = term.Replace("_", "") 'some monster names have a _ in the name, which isn't found in the l2language terms. It has to be removed
        term = term.Remove(10, 1).Insert(10, Char.ToLower(term(10))) 'converts the 10th letter to lower case

        description = deserializedData.returnTerm(term, languageEnum)
        Return description

    End Function

    Private Function getAttackName(attackName As String, deserializedData As LanguageData)
        Dim name As String
        Dim term As String

        term = attackName.Replace("key:", "")

        name = deserializedData.returnTerm(term, languageEnum)
        If name <> "" Then
            Return name
        Else
            Return "Attack"
        End If

    End Function

    Private Sub selectAttacks_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            attackFolder = fbd.SelectedPath
            SelectedAttackFolder.Text = attackFolder
        End If
    End Sub

    Private Sub selectWP_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            WPFolder = fbd.SelectedPath
            SelectedWPFolder.Text = attackFolder
        End If
    End Sub

    Private Sub selectEnchant_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            enchantFolder = fbd.SelectedPath
            SelectedEnchantFolder.Text = enchantFolder
        End If
    End Sub

    Private Sub selectSpells_Click(sender As Object, e As RoutedEventArgs)
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        If fbd.ShowDialog() Then
            spellFolder = fbd.SelectedPath
            SelectedSpellFolder.Text = spellFolder
        End If
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Attacks")) Then
            attackFolder = IO.Path.Combine(Environment.CurrentDirectory, "Attacks")
            SelectedAttackFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Attacks")
        End If
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Spells")) Then
            spellFolder = IO.Path.Combine(Environment.CurrentDirectory, "Spells")
            SelectedSpellFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Spells")
        End If
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "WP")) Then
            wpFolder = IO.Path.Combine(Environment.CurrentDirectory, "WP")
            SelectedWPFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "WP")
        End If
        If IO.Directory.Exists(IO.Path.Combine(Environment.CurrentDirectory, "Enchantment")) Then
            enchantFolder = IO.Path.Combine(Environment.CurrentDirectory, "Enchantment")
            SelectedEnchantFolder.Text = IO.Path.Combine(Environment.CurrentDirectory, "Enchantment")
        End If
    End Sub
End Class
