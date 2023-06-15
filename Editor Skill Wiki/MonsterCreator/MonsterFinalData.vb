Public Class MonsterFinalData 'class which stores the monster data after being calculated from the raw data

    Public name As String
    Public description As String
    Public challengeRating As String
    Public difficulty As String
    Public halfKillsRequired As String
    Public monsterRace As String
    Public capturable As String
    Public skinAmount As String
    Public petItemType As String
    Public champion As String

    Public walkSpeed As String
    Public baseSpeed As String

    Public str As String
    Public dex As String
    Public int As String
    Public cos As String
    Public per As String
    Public cha As String
    Public health As String
    Public healthRegen As String
    Public manaRegen As String
    Public mana As String
    Public accuracy As String
    Public fortitude As String
    Public evasion As String
    Public willpower As String

    Public stealth As String
    Public detection As String
    Public spellDamageIncrease As String
    Public cooldownReduction As String
    Public luck As String
    Public criticalChance As String
    Public criticalDamage As String
    Public slashArmor As String
    Public slashArmorPerc As String
    Public pierceArmor As String
    Public pierceArmorPerc As String
    Public crushArmor As String
    Public crushArmorPerc As String

    Public fireResistance As String
    Public fireResistancePerc As String
    Public coldResistance As String
    Public coldResistancePerc As String
    Public shockResistance As String
    Public shockResistancePerc As String
    Public magicResistance As String
    Public magicResistancePerc As String
    Public poisonResistance As String
    Public poisonResistancePerc As String
    Public acidResistance As String
    Public acidResistancePerc As String

    Public magicalDamageReflection As String
    Public physicalDamageReflection As String
    Public slashDamageIncrease As String
    Public pierceDamageIncrease As String
    Public crushDamageIncrease As String
    Public fireDamageIncrease As String
    Public iceDamageIncrease As String
    Public shockDamageIncrease As String
    Public acidDamageIncrease As String
    Public poisonDamageIncrease As String
    Public energyDamageIncrease As String
    Public fireDamageConversion As String
    Public iceDamageConversion As String
    Public shockDamageConversion As String
    Public acidDamageConversion As String
    Public poisonDamageConversion As String
    Public energyDamageConversion As String
    Public monsterPoisonStack As String
    Public broadcastAggression As String

    Public immunityEffectGroup As String
    Public specialImmunity As String


    Public monsterAttacks As List(Of MonsterAttackFinalData)
    Public monsterSkills As List(Of MonsterSpellFinalData)
    Public immunityStatusEffectList As List(Of String)
    Public damageAbsorbed As List(Of String)

    Public playerAttitude As String
    Public combatStance As String
    Public category As String
    Public allowedPoi As List(Of String)

    Public Sub parseData(data As MonsterData, attackFolder As String, spellFolder As String, wpFolder As String, enchantFolder As String, languageData As LanguageData, languageEnum As Integer)
        Try
            Dim attackFolderPath As String
            Dim spellFolderPath As String
            Dim wpFolderPath As String
            Dim enchantFolderPath As String
            monsterAttacks = New List(Of MonsterAttackFinalData)
            monsterSkills = New List(Of MonsterSpellFinalData)
            immunityStatusEffectList = New List(Of String)
            damageAbsorbed = New List(Of String)
            allowedPoi = New List(Of String)


            If System.IO.Directory.Exists(attackFolder) Then
                attackFolderPath = attackFolder
            Else Throw New Exception("Attack folder does not exist.")
            End If

            If System.IO.Directory.Exists(spellFolder) Then
                spellFolderPath = spellFolder
            Else Throw New Exception("Spell folder does not exist.")
            End If

            If System.IO.Directory.Exists(wpFolder) Then
                wpFolderPath = wpFolder
            Else Throw New Exception("WP folder does not exist.")
            End If

            If System.IO.Directory.Exists(enchantFolder) Then
                enchantFolderPath = enchantFolder
            Else Throw New Exception("WP folder does not exist.")
            End If

            name = data.m_name
            challengeRating = data.challengeRating.ToString
            difficulty = [Enum].GetName(GetType(LegendDifficulty), data.difficulty)
            halfKillsRequired = data.halfKillsRequired.ToString
            monsterRace = [Enum].GetName(GetType(MonsterRace), data.monsterRace)
            If challengeRating = 10 Then
                category = "Legend"
            Else
                category = monsterRace
            End If
            playerAttitude = [Enum].GetName(GetType(PlayerAttitude), data.playerAttitude)
            combatStance = [Enum].GetName(GetType(CombatStance), data.combatStance)
            capturable = data.capturable.ToString
            skinAmount = data.skinAmount.ToString
            champion = data.champion.ToString
            broadcastAggression = data.BroadcastAggression.ToString

            walkSpeed = data.walkSpeed.ToString
            baseSpeed = data.baseSpeed.ToString

            str = data.str.value.ToString
            dex = data.dex.value.ToString
            int = data._int.value.ToString
            cos = data.cos.value.ToString
            per = data.per.value.ToString
            cha = data.cha.value.ToString

            health = (data.baseEndurance.value + data.str.value * 50 + data.cos.value * 75).ToString
            healthRegen = (data.baseEnduranceRegen.value + data.str.value).ToString
            mana = (data.baseEnergy.value + data._int.value * 75 + data.cha.value * 50).ToString
            manaRegen = (data.baseEnergyRegen.value + data._int.value * 2).ToString
            accuracy = (data.accuracy.value + data.per.value * 25 - 150).ToString
            fortitude = (data.fortitude.value + data.cos.value * 25 - 150).ToString
            evasion = (data.evasion.value + data.dex.value * 25 - 150).ToString
            willpower = (data.willpower.value + data._int.value * 25 - 150).ToString
            stealth = (data.Stealth.value + data.dex.value * 25 - 150).ToString
            detection = (data.Detection.value + data.per.value * 25 - 150).ToString
            luck = (data.baseLuck.value + data.cha.value * 25 - 250).ToString
            criticalChance = (data.criticalChance.value + data.per.value * 1 - 10).ToString & "%"
            criticalDamage = data.criticalDamage.value.ToString


            slashArmor = data.armorSlash.value.ToString
            slashArmorPerc = calculatePerc(data.armorSlash.value)
            pierceArmor = data.armorPierce.value.ToString
            pierceArmorPerc = calculatePerc(data.armorPierce.value)
            crushArmor = data.armorCrush.value.ToString
            crushArmorPerc = calculatePerc(data.armorCrush.value)
            fireResistance = data.fireResistance.value.ToString
            fireResistancePerc = calculatePerc(data.fireResistance.value)
            coldResistance = data.coldResistance.value.ToString
            coldResistancePerc = calculatePerc(data.coldResistance.value)
            shockResistance = data.shockResistance.value.ToString
            shockResistancePerc = calculatePerc(data.shockResistance.value)
            magicResistance = data.magicResistance.value.ToString
            magicResistancePerc = calculatePerc(data.magicResistance.value)
            poisonResistance = data.poisonResistance.value.ToString
            poisonResistancePerc = calculatePerc(data.poisonResistance.value)
            acidResistance = data.acidResistance.value.ToString
            acidResistancePerc = calculatePerc(data.acidResistance.value)
            immunityEffectGroup = data.immunityEffectGroup
            specialImmunity = combinedEnumeratorName(GetType(SpecialMonsterImmunity), data.specialImmunities)

            magicalDamageReflection = data.magicalDamageReflection.ToString
            physicalDamageReflection = data.physicalDamageReflection.ToString
            slashDamageIncrease = data.slashDamageIncrease.ToString
            pierceDamageIncrease = data.pierceDamageIncrease.ToString
            crushDamageIncrease = data.crushDamageIncrease.ToString
            fireDamageIncrease = data.fireDamageConversion.ToString
            iceDamageIncrease = data.iceDamageConversion.ToString
            shockDamageIncrease = data.shockDamageConversion.ToString
            acidDamageIncrease = data.acidDamageConversion.ToString
            poisonDamageIncrease = data.poisonDamageConversion.ToString
            energyDamageIncrease = data.energyDamageConversion.ToString
            fireDamageConversion = data.fireDamageConversion.ToString
            iceDamageConversion = data.iceDamageConversion.ToString
            shockDamageConversion = data.shockDamageConversion.ToString
            acidDamageConversion = data.acidDamageConversion.ToString
            poisonDamageConversion = data.poisonDamageConversion.ToString
            energyDamageConversion = data.energyDamageConversion.ToString
            monsterPoisonStack = data.monsterPoisonStack.ToString
            spellDamageIncrease = data.spellDamageIncrease.value.ToString
            cooldownReduction = data.cooldownReduction.value.ToString

            'If data.petItemType.m_PathID <> 0 Then
            '    Dim itemRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "LootCreator/itemsIndex.xml"))
            '    petItemType = LootCreator.getItem(data.petItemType.m_PathID, itemRawData)
            'Else petItemType = ""
            'End If
            petItemType = "" 'inseguire i dati dei petItem è troppo difficile, lascio il campo vuoto


            Dim statusRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/statusEffects.xml"))

            For Each StatusEffect As PropertyClass In data.immunityStatusEffectList
                If StatusEffect.m_PathID <> 0 Then
                    Dim newImmunity As String = getStatusEffect(StatusEffect.m_PathID, statusRawData)
                    If newImmunity <> "" Then
                        immunityStatusEffectList.Add(newImmunity)
                    End If

                End If
            Next

            For Each damage As Integer In data.damageAbsorbed
                damageAbsorbed.Add([Enum].GetName(GetType(DamageType), damage))
            Next


            Dim attackRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/attacks.xml"))
            Dim wpRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/weaponProperties.xml"))
            Dim enchantsRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/enchantments.xml"))

            For Each attack As MonsterAttack In data.MonsterAttacks
                monsterAttacks.Add(getAttackData(attack.value.m_PathID, attackRawData, wpRawData, enchantsRawData, statusRawData, attackFolderPath, wpFolderPath, enchantFolderPath, languageData, languageEnum))
            Next

            Dim spellRawData As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/SpellIndex.xml"))

            For Each spell As MonsterSpells In data.MonsterSpellsList
                Dim newskill As MonsterSpellFinalData = getSpellData(spell.value.m_PathID, spellRawData, spellFolderPath, spell)
                newskill.acquired = spell.targetToUnlock
                compileRestrictions(spell, newskill)
                compileSkillFullText(newskill)
                monsterSkills.Add(newskill)
            Next

            For Each poi As Integer In data.allowedPoi
                allowedPoi.Add([Enum].GetName(GetType(POIType), poi))
            Next
        Catch ex As Exception
            Throw New Exception("The file selected is not a creature.")
        End Try


    End Sub

    Private Function calculatePerc(number As Integer) As String
        Dim percentValue As Double

        percentValue = Math.Round(100 * number / (number + 500), 0)



        Return percentValue.ToString & "%"
    End Function

    Private Function getAttackData(attackRef As String, attackRawData As String, wpRawData As String, enchantRawData As String, statusRawData As String, attackFolder As String, wpFolder As String, enchantFolder As String, languageData As LanguageData, languageEnum As Integer) As MonsterAttackFinalData
        Dim newAttack As New MonsterAttackFinalData
        Dim parsedData As MonsterAttackData
        Dim index As Integer = attackRawData.IndexOf(attackRef)
        If index <> -1 Then
            Dim startName As Integer = attackRawData.LastIndexOf("<Name>", index)
            Dim endName As Integer = attackRawData.IndexOf("</Name>", startName)
            Dim name As String = attackRawData.Substring(startName + 6, endName - startName - 6)
            Dim filePath As String = attackFolder & "\" & name & ".json"
            If System.IO.File.Exists(filePath) Then
                Dim textData As String = System.IO.File.ReadAllText(filePath)
                parsedData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of MonsterAttackData)(textData)
                newAttack.name = parsedData.attackName
                newAttack.primaryDamage = [Enum].GetName(GetType(DamageType), parsedData.damage.dmgType1)
                newAttack.secondaryDamage = [Enum].GetName(GetType(DamageType), parsedData.damage.dmgType2)
                newAttack.tertiaryDamage = [Enum].GetName(GetType(DamageType), parsedData.damage.dmgType3)
                newAttack.damageModifier = parsedData.damage.damageModifier.ToString
                newAttack.primaryStat = [Enum].GetName(GetType(CharacterAttribute), parsedData.damage.dmgAttr1)
                newAttack.secondaryStat = [Enum].GetName(GetType(CharacterAttribute), parsedData.damage.dmgAttr2)
                newAttack.range = parsedData.range.ToString
                newAttack.speed = parsedData.attackSpeed.ToString
                newAttack.weaponDamageType = [Enum].GetName(GetType(WeaponDamageType), parsedData.weaponDamageType)
                newAttack.damageAmplitude = parsedData.damageAmplitude.ToString
                newAttack.damageLength = parsedData.damageLength.ToString
                newAttack.damageWidth = parsedData.damageWidth.ToString

                For Each attackProperty As WeaponProperty In parsedData.weaponProperties
                    If attackProperty.Property.m_PathID <> "0" Then
                        Dim newProperty As New AttackProperty
                        newProperty.value = attackProperty.baseAmount
                        newProperty.name = getAttackPropertyName(attackProperty.Property.m_PathID, wpFolder, enchantFolder, wpRawData, enchantRawData, statusRawData, languageData, languageEnum)
                        newAttack.attackProperties.Add(newProperty)
                    End If
                Next



                calculateAttackDamage(newAttack)
                compileAttackFullText(newAttack)
            Else Throw New Exception(filePath & " Not Found")
            End If
        End If
        Return newAttack
    End Function

    Private Function getAttackPropertyName(wpRef As String, wpFolder As String, enchantFolder As String, wpRawData As String, enchantRawData As String, statusRawData As String, languageData As LanguageData, languageEnum As Integer) As String
        Dim attackProperty As New AttackProperty
        Dim attackPropertyData As New WeaponPropertyData
        Dim index As Integer = wpRawData.IndexOf(wpRef)
        Dim startName As Integer = wpRawData.LastIndexOf("<Name>", index)
        Dim endName As Integer = wpRawData.IndexOf("</Name>", startName)
        Dim name = wpRawData.Substring(startName + 6, endName - startName - 6)
        Dim filePath As String = wpFolder & "\" & name & ".json"
        If System.IO.File.Exists(filePath) Then
            Dim textData As String = System.IO.File.ReadAllText(filePath)
            attackPropertyData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of WeaponPropertyData)(textData)
            If attackPropertyData.localizationString <> "" Then
                Return languageData.returnTerm(attackPropertyData.localizationString, languageEnum)
            ElseIf attackPropertyData.enchantment IsNot Nothing Then
                Return getAttackEnchantName(attackPropertyData.enchantment.m_PathID, enchantFolder, enchantRawData, languageData, languageEnum)
            ElseIf attackPropertyData.statusEffect IsNot Nothing Then
                Return getStatusEffect(attackPropertyData.statusEffect.m_PathID, statusRawData)
            ElseIf attackPropertyData.necroticStatus IsNot Nothing Then
                Return "Necrotic"
            Else Return ""
            End If
        Else
            Return ""
        End If
    End Function

    Private Function getAttackEnchantName(enchantRef As String, enchantFolder As String, enchantRawData As String, languageData As LanguageData, languageEnum As Integer) As String
        Dim enchantName As String = ""
        Dim enchantPropertyData As EnchantmentRecipe
        Dim name As String = ""
        Dim index As Integer = enchantRawData.IndexOf(enchantRef)
        Dim startName As Integer = enchantRawData.LastIndexOf("<Name>", index)
        Dim endName As Integer = enchantRawData.IndexOf("</Name>", startName)
        name = enchantRawData.Substring(startName + 6, endName - startName - 6)
        Dim filePath As String = enchantFolder & "\" & name & ".json"
        If System.IO.File.Exists(filePath) Then
            Dim textData As String = System.IO.File.ReadAllText(filePath)
            enchantPropertyData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of EnchantmentRecipe)(textData)
            Dim term As String = "enchantments/" & enchantPropertyData.recipeName & "_name"
            If term.IndexOf("DamageConversion_name") <> -1 Then
                term = term.Replace("DamageConversion_name", "DamageCovnersion_name") 'game bug
            End If
            enchantName = languageData.returnTerm(term, languageEnum)
        End If
        Return enchantName
    End Function

    Private Function getStatusEffect(statusRef As String, rawdata As String) As String
        Dim statusEffect As String = ""
        Dim index As Integer = rawdata.IndexOf(statusRef)
        Dim startName As Integer = rawdata.LastIndexOf("<Name>", index)
        Dim endName As Integer = rawdata.IndexOf("</Name>", startName)
        statusEffect = rawdata.Substring(startName + 6, endName - startName - 6)
        Return statusEffect
    End Function


    Private Function getSpellData(spellRef As String, rawData As String, spellFolder As String, spell As MonsterSpells)
        Dim newSpell As New MonsterSpellFinalData
        Dim parsedData As SpellClassMirror
        Dim index As Integer = rawData.IndexOf(spellRef)
        If index <> -1 Then
            Dim startName As Integer = rawData.LastIndexOf("<Name>", index)
            Dim endName As Integer = rawData.IndexOf("</Name>", startName)
            Dim name As String = rawData.Substring(startName + 6, endName - startName - 6)

            'eccezioni di skill specifiche dei mostri
            Select Case name
                Case "spell_ArborealDragonBreath_ArborealDragon"
                    name = "spell_ArborealDragonBreath"
                Case "spell_SkeletalDragonBreath_SkeletalDragon"
                    name = "spell_SkeletalDragonBreath"
                Case "spell_MountainDragonBreath_MountainDragon"
                    name = "spell_MountainDragonBreath"
                Case "spell_EmberDragonBreath_EmberDragon"
                    name = "spell_EmberDragonBreath"

                Case "spell_MagicReflection_ColossalHuskWorm"
                    name = "spell_MagicReflection"
                Case "spell_BattleJump_Ghoul"
                    name = "spell_BattleJump"

                Case "spell_MagicReflection_ColossalHuskWorm"
                    name = "spell_MagicReflection"
                Case "spell_LegendWOPSilence"
                    name = "spell_WordOfPower_Silence"
                Case "spell_LegendShivers"
                    name = "spell_Shivers"
                Case "spell_LegendMegaEruption"
                    name = "spell_Eruption"
                Case "spell_LegendLargeSlow"
                    name = "spell_Slow"
                Case "spell_LegendIceSpikesRing"
                    name = "spell_IceSpikes"
                Case "spell_LegendGrooveRam"
                    name = "spell_RamThrough"
                Case "spell_LegendGrooveQuake"
                    name = "spell_Earthquake"
                Case "spell_LegendAxfitmithissTouch"
                    name = "spell_ChillingTouch"

                    'skill con carattere speciale
                Case "spell_Hack&amp;Slash"
                    name = "spell_Hack&Slash"



            End Select

            Dim filePath As String = spellFolder & "\" & name & ".json"
            If System.IO.File.Exists(filePath) Then
                Dim textData As String = System.IO.File.ReadAllText(filePath)
                parsedData = Newtonsoft.Json.JsonConvert.DeserializeObject(Of SpellClassMirror)(textData)
                newSpell.name = parsedData.m_name
                newSpell.kp = ((parsedData.difficulty + 1) * 1000).ToString
                newSpell.override = spell.cooldownOverride.ToString
                newSpell.globalCd = spell.globalCooldown.ToString
                newSpell.castingChance = spell.castingChance.ToString
                newSpell.targetAlly = spell.targetAlly.ToString

            Else Throw New Exception(filePath & " Not Found")
            End If
        End If
        Return newSpell
    End Function

    Private Sub calculateAttackDamage(ByRef attack As MonsterAttackFinalData)

        Dim statValue As Integer
        Select Case attack.primaryStat
            Case "Strength"
                statValue = str
            Case "Dexterity"
                statValue = dex
            Case "Intelligence"
                statValue = int
            Case "Constitution"
                statValue = cos
            Case "Perception"
                statValue = per
            Case "Charisma"
                statValue = cha
            Case Else
                statValue = 0

        End Select
        Dim baseAttackDamage As Integer = (attack.damageModifier * statValue)
        Dim minimumAttackDamage As Integer = baseAttackDamage * 0.75
        Dim maximumAttackDamage As Integer = baseAttackDamage * 1.25

        attack.damage = minimumAttackDamage.ToString & " - " & maximumAttackDamage.ToString

    End Sub

    Private Sub compileAttackFullText(ByRef attack As MonsterAttackFinalData)
        Dim baseText As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterAttack.txt"))
        'the name will be compiled later by the MonsterDataCreator since it needs the language data
        baseText = baseText.Replace("textDamageValue", attack.damage)
        baseText = baseText.Replace("textType1", attack.primaryDamage)
        baseText = baseText.Replace("textType2", attack.secondaryDamage)
        baseText = baseText.Replace("textType3", attack.tertiaryDamage)
        baseText = baseText.Replace("textRange", attack.range)
        baseText = baseText.Replace("textSpeed", attack.speed)
        baseText = baseText.Replace("textWeaponType", attack.weaponDamageType)
        baseText = baseText.Replace("textDamageLength", attack.damageLength)
        baseText = baseText.Replace("textDamageAmplitude", attack.damageAmplitude)
        baseText = baseText.Replace("textDamageWidth", attack.damageWidth)

        Dim propertyText As String = ""
        For Each attackProperty As AttackProperty In attack.attackProperties
            Dim newPropertyText As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultAttackProperty.txt"))
            newPropertyText = newPropertyText.Replace("textName", attackProperty.name)
            newPropertyText = newPropertyText.Replace("textValue", attackProperty.value)
            propertyText &= newPropertyText
            propertyText &= vbCrLf
        Next
        baseText = baseText.Replace("propertySpace", propertyText)
        'baseText = baseText.Replace("poisonChanceText", attack.poisonChance)
        attack.fullText = baseText

    End Sub


    Private Function compileRestrictions(spell As MonsterSpells, finalSpell As MonsterSpellFinalData)
        Dim restrictionText As String = ""
        For Each restriction As MonsterCastingRestriction In spell.castingRestrictions
            Dim baseText As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterRestriction.txt"))
            baseText = baseText.Replace("typeText", [Enum].GetName(GetType(MonsterRestrictionType), restriction.type))
            baseText = baseText.Replace("valueText", restriction.value.ToString)
            baseText = baseText.Replace("statusEffectText", [Enum].GetName(GetType(RestrictionStatusEffect), restriction.statusEffect))
            baseText = baseText.Replace("statusGroupText", [Enum].GetName(GetType(EffectGroup), restriction.statusEffectGroup))
            restrictionText &= baseText
            restrictionText &= vbCrLf
        Next
        finalSpell.restrictionText = restrictionText
        Return finalSpell
    End Function

    Private Sub compileSkillFullText(ByRef spell As MonsterSpellFinalData)
        Dim baseText As String = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "MonsterCreator/defaultMonsterSkill.txt"))
        'the name will be compiled later by the MonsterDataCreator since it needs the language data
        baseText = baseText.Replace("kpText", spell.kp)
        baseText = baseText.Replace("acquiredText", spell.acquired)
        baseText = baseText.Replace("cooldownOverrideText", spell.override)
        baseText = baseText.Replace("globalCooldownText", spell.globalCd)
        baseText = baseText.Replace("castingChanceText", spell.castingChance)
        baseText = baseText.Replace("targetAllyText", spell.targetAlly)
        baseText = baseText.Replace("restrictionsSpace", spell.restrictionText)
        spell.fullText = baseText
    End Sub

End Class



Public Class MonsterAttackFinalData

    Public name As String
    Public damage As String
    Public damageModifier As Double
    Public range As String
    Public speed As String
    Public primaryDamage As String
    Public secondaryDamage As String
    Public tertiaryDamage As String
    Public primaryStat As String
    Public secondaryStat As String
    Public weaponDamageType As String
    Public damageWidth As String
    Public damageLength As String
    Public damageAmplitude As String
    Public fullText As String
    Public attackProperties As List(Of AttackProperty)

    Public Sub New()
        attackProperties = New List(Of AttackProperty)
    End Sub

End Class

Public Class AttackProperty
    Public name As String
    Public value As String
End Class

Public Class MonsterSpellFinalData

    Public name As String
    Public kp As String
    Public acquired As String
    Public override As String
    Public globalCd As String
    Public targetAlly As String
    Public castingChance As String
    Public restrictionText As String
    Public fullText As String

End Class