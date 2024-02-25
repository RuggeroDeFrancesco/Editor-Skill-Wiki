
Imports Editor_Skill_Wiki.Enumerators

Public Class MonsterDataCreator

    Dim commonFunctions As CommonFunctions

    Public Sub New(commonFunctions As CommonFunctions)

        ' La chiamata è richiesta dalla finestra di progettazione.
        InitializeComponent()

        ' Aggiungere le eventuali istruzioni di inizializzazione dopo la chiamata a InitializeComponent().
        Me.commonFunctions = commonFunctions
        Try
            OutputBlock.Text = ""
            For Each monster As MirrorClasses.MonsterClass In commonFunctions.MonsterList
                If monster.IsDummy Then
                    Continue For
                End If
                If monster.m_Name = "class_NPC" Then
                    Continue For
                End If
                Dim finalData As New MonsterFinalData(commonFunctions)
                finalData.parseData(monster)
                Dim output = createModuleOutput(finalData)
                OutputBlock.Text &= output
                OutputBlock.Text &= vbCrLf
            Next
            Clipboard.SetText(OutputBlock.Text)
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try
    End Sub

    Private Sub test_click(sender As Object, e As RoutedEventArgs)
        Clipboard.SetText(OutputBlock.Text)
    End Sub



    Private Function createModuleOutput(data As MonsterFinalData)
        Dim output As String
        If My.Settings.json Then
            output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/jsonMonsterData.txt"))
        Else
            output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "DefaultOutputs/defaultMonsterData.txt"))
        End If


        output = output.Replace("nameText", data.name)
        output = output.Replace("descriptionText", data.description)
        output = output.Replace("strText", data.str)
        output = output.Replace("dexText", data.dex)
        output = output.Replace("intText", data.int)
        output = output.Replace("conText", data.cos)
        output = output.Replace("perText", data.per)
        output = output.Replace("chaText", data.wis)
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
        output = output.Replace("criticalChanceText", data.criticalChance)
        output = output.Replace("criticalDamageText", data.criticalDamage)
        output = output.Replace("luckText", data.luck)


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

        output = output.Replace("cooldownReductionText", data.cooldownReduction)
        output = output.Replace("spellDamageIncreaseText", data.spellDamageIncrease)


        output = output.Replace("immunityGroupText", data.EffectGroupImmunities)
        output = output.Replace("specialImmunityText", data.SpecialImmunities)
        output = output.Replace("immunitySpace", data.StatusImmunities)
        output = output.Replace("absorbSpace", data.Absorb)
        output = output.Replace("magicalDamageReflectionText", data.magicalDamageRefection)
        output = output.Replace("physicalDamageReflectionText", data.physicalDamageReflection)

        output = output.Replace("challengeRatingText", data.ChallengeRating)
        output = output.Replace("difficultyText", data.Difficulty)
        output = output.Replace("halfKillsRequiredText", data.HalfKillsRequired)
        output = output.Replace("monsterRaceText", data.MonsterRace)
        output = output.Replace("capturableText", data.Capturable)
        output = output.Replace("skinAmountText", data.SkinAmount)
        output = output.Replace("championText", data.Champion)
        output = output.Replace("walkSpeedText", data.WalkSpeed)
        output = output.Replace("baseSpeedText", data.BaseSpeed)
        output = output.Replace("broadcastAggressionText", data.BroadCastAggression)
        output = output.Replace("playerAttitudeText", data.PlayerAttitude)
        output = output.Replace("combatStanceText", data.CombatStance)




        Dim attackText As String = ""
        For Each attack As MonsterAttackFinalData In data.monsterAttacks
            attackText &= attack.fullText & vbCrLf
        Next

        output = output.Replace("attackSpace", attackText)

        Dim skillText As String = ""
        For Each skill As MonsterSpellFinalData In data.monsterSkills
            skillText &= skill.fullText & vbCrLf
        Next

        output = output.Replace("skillSpace", skillText)

        output = output.Replace("categoryText", data.Category)


        Return output
    End Function



End Class
