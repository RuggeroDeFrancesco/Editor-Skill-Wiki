Imports Newtonsoft.Json
Public Class SpellDataCreator

    'class that parses a json file of a spell and extracts all the fields in a usable format.

    Dim language As Integer

    Dim deserializedlanguageData As LanguageData
    Public Property Output As String 'full list of the properties, to be used for the datamodule on the wiki
    Public Property rawDescription As String 'description of the spell

    Public Property parameters As String 'numerical parameters that follow the description

    Public Property customDataNameList As ObjectModel.ObservableCollection(Of String)

    Public Property customDataValueList As ObjectModel.ObservableCollection(Of String)

    Public Property SpellName As String

    Public Property gameReady As Boolean

    Public Property removed As Boolean

    Public Property TooltipSpellName As String

    Shared Function getRawData(path As String) As String 'reads the file and extracts the content as text, used also by StatusEffectDataCreator
        If System.IO.File.Exists(path) Then
            Dim rawData As String
            rawData = System.IO.File.ReadAllText(path)
            Return rawData
        Else Throw New System.Exception("File does not exist.")
            Return ""
        End If
    End Function

    Shared Function cutData(data As String) As String 'removes the useless parts of the file, used also by StatusEffectDataCreator
        Dim processedData As String
        Dim startingIndex As Integer
        startingIndex = data.IndexOf("m_Name") - 1
        Dim secondIndex As Integer
        secondIndex = data.IndexOf("Icon") - 1
        Dim thirdIndex As Integer
        thirdIndex = data.IndexOf("spellcastingRestrictions") - 1
        'Dim fourthIndex As Integer
        'fourthIndex = data.IndexOf("customData") - 1
        processedData = "{"
        processedData &= data.Substring(startingIndex, secondIndex - startingIndex)
        processedData &= data.Substring(thirdIndex)
        'processedData &= data.Substring(thirdIndex, fourthIndex - thirdIndex)
        'processedData &= "}"
        Return processedData
    End Function

    Shared Function deserializeSpellData(data As String) As SpellClassMirror 'converts the json to fields of the SpellData class, , used also by StatusEffectDataCreator

        Dim parsedData As SpellClassMirror
        parsedData = JsonConvert.DeserializeObject(Of SpellClassMirror)(data)
        Return parsedData
    End Function

    Public Sub parseData(path As String, deserializedLanguageData As LanguageData) 'main method of the class, uses the private methods to extract the data from a spell file, deserialize it into a spellClassMirror class and extracts information from SpellClassMirror

        Dim data As SpellClassMirror = deserializeSpellData(cutData(getRawData(path)))
        Me.deserializedlanguageData = deserializedLanguageData
        Output = createOutput(data)
        rawDescription = GetDescription(data)
        parameters = GetSkillParameters(data)
        SpellName = data.m_name
        removed = data.removed
        gameReady = data.gameReady
        Output = Output.Replace("SpellName", GetTooltipName(data.SpellKey, deserializedLanguageData, language).Replace("'", "\'"))

        'Dim customDataLists As filteredCustomData
        'customDataLists = data.GetCustomData()
        'customDataNameList = customDataLists.name
        'customDataValueList = customDataLists.value
        'customDataNameList.Add("None")
        'customDataNameList.Add("Linear")
        'customDataValueList.Add("0") 'adding the 0 and 1 values to correct the bugged tooltips.
        'customDataValueList.Add("1")
    End Sub

    Private Function createOutput(data As SpellClassMirror) As String 'converts the enumerators into clear text and adds the categories
        Dim output As String

        output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "SpellCreator/defaultSpellData.txt"))
        'output = output.Replace("SpellName", data.m_name)
        If data.m_name = "spell_Charge" Then 'exceptions
            output = output.Replace("imageValue", data.m_name.Replace("spell_", "Icon_") & "_Ability.png")
        ElseIf data.m_name = "spell_LighitningRush" Then
            output = output.Replace("imageValue", "Icon_LightningRush.png")
        ElseIf data.m_name = "spell_SkeletalDragonBreath_SkeletalDragon" Then
            output = output.Replace("imageValue", "Icon_SkeletalDragonBreath.png")
        Else
            output = output.Replace("imageValue", data.m_name.Replace("spell_", "Icon_") & ".png")
        End If
        output = output.Replace("schoolValue", [Enum].GetName(GetType(SpellSchool), data.SpellSchool))

        output = output.Replace("friendlyFireValue", [Enum].GetName(GetType(FriendlyFire), data.friendlyFire))

        output = output.Replace("canCritValue", [Enum].GetName(GetType(canBeCritical), data.canBeCritical))
        output = output.Replace("preCastTimeValue", data.preCastTime.ToString)
        output = output.Replace("memoryCostValue", data.difficulty + 1)
        output = output.Replace("spellGroupValue", [Enum].GetName(GetType(SpellGroup), data.spellGroup))

        Dim foundCooldown As Boolean = False

        For Each dataType As spellDataInfo In data.spellData

            If dataType.dataType = SpellDataType.Cooldown Then
                If dataType.level0 <> dataType.level10 Then
                    Dim cooldownValue As String = dataType.level0 & " to " & dataType.level10
                    output = output.Replace("cooldownValue", """" & cooldownValue & """")
                Else
                    output = output.Replace("cooldownValue", dataType.level0)
                End If

                foundCooldown = True
                End If
        Next

        If Not foundCooldown Then
            output = output.Replace("cooldownValue", data.cooldown)
        End If



        Dim abilityType As String = ""
        If data.tooltipActivationType = tooltipActivationType.Sustained Then
            abilityType = "Sustained"

        ElseIf data.tooltipActivationType = tooltipActivationType.Ready Then
            abilityType = "Ready"
        ElseIf data.tooltipActivationType = tooltipActivationType.Directional Then
            abilityType = "Directional"
        ElseIf data.tooltipActivationType = tooltipActivationType.Self Then
            abilityType = "Self"
        ElseIf data.tooltipActivationType = tooltipActivationType.Targeted Then
            If data.tooltipTargetType = tooltipTargetType.Terrain Then
                abilityType = "Targeted (Terrain)"
            End If
            If data.tooltipTargetType = tooltipTargetType.Enemy Then
                abilityType = "Targeted (Enemy)"
            End If
            If data.tooltipTargetType = tooltipTargetType.FrozenEnemy Then
                abilityType = "Targeted (Frozen Enemy)"
            End If
            If data.tooltipTargetType = tooltipTargetType.RootedEnemy Then
                abilityType = "Targeted (Rooted Enemy)"
            End If
            If data.tooltipTargetType = tooltipTargetType.Ally Then
                abilityType = "Targeted (Ally)"
            End If
            If data.tooltipTargetType = tooltipTargetType.None Then
                abilityType = "Targeted"
            End If
        End If

        output = output.Replace("abilityTypeValue", abilityType)


        'output = output.Replace("ChannelingWeaponValue", [Enum].GetName(GetType(ChannelingWeapon), data.spellcastingRestrictions.spellChannelingWeapon))
        'output = output.Replace("weaponClassValue", [Enum].GetName(GetType(RestrictedWeaponClass), data.spellcastingRestrictions.restrictedWeaponClass))
        'output = output.Replace("weaponWeightValue", correctWeightEnumerator([Enum].GetName(GetType(RestrictedWeaponWeight), data.spellcastingRestrictions.restrictedWeaponWeight)))
        'output = output.Replace("weaponDamageValue", correctDamageEnumerator([Enum].GetName(GetType(RestrictedWeaponDamage), data.spellcastingRestrictions.restrictedWeaponDamage)))

        Dim weaponRestriction As String = ""
        If data.spellcastingRestrictions.weaponRestriction <> RestrictedWeaponClass.None Then
            weaponRestriction = GetSpellDataTypeName([Enum].GetName(GetType(RestrictedWeaponClass), data.spellcastingRestrictions.weaponRestriction))
        End If
        If data.spellcastingRestrictions.specialWeaponRestriction <> SpellSpecialWeaponRestriction.None Then
            If weaponRestriction = "" Then
                weaponRestriction = GetSpellDataTypeName([Enum].GetName(GetType(SpellSpecialWeaponRestriction), data.spellcastingRestrictions.specialWeaponRestriction))
            Else
                weaponRestriction &= ", " & GetSpellDataTypeName([Enum].GetName(GetType(SpellSpecialWeaponRestriction), data.spellcastingRestrictions.specialWeaponRestriction))
            End If
        End If
        If weaponRestriction = "" Then
            weaponRestriction = "N/A"
        End If

        output = output.Replace("SpecialWeaponRestictionValue", weaponRestriction)
        Dim armorRestriction As String
        Select Case data.spellcastingRestrictions.restrictedArmorWeight
            Case 1
                armorRestriction = GetSpellDataTypeName([Enum].GetName(GetType(RestrictedArmorWeight), 1))
            Case 2
                armorRestriction = GetSpellDataTypeName([Enum].GetName(GetType(RestrictedArmorWeight), 1)) & " / " & GetSpellDataTypeName([Enum].GetName(GetType(RestrictedArmorWeight), 2))
            Case Else
                armorRestriction = "Any"
        End Select
        output = output.Replace("armorWeightValue", armorRestriction)


        Dim foundManaCost As Boolean = False
        For Each dataType As spellDataInfo In data.spellData

            If dataType.dataType = SpellDataType.ManaCost Then
                If dataType.level0 <> dataType.level10 Then
                    Dim manacostValue As String = dataType.level0 & " to " & dataType.level10
                    output = output.Replace("manaCostValue", """" & manacostValue & """")
                Else
                    output = output.Replace("manaCostValue", dataType.level0)
                    output = output.Replace("manaCostPersValue", 0)
                    output = output.Replace("maxManaCostValue", 0)
                End If

                foundManaCost = True
            End If

            If dataType.dataType = SpellDataType.ManaCostPerSecond Then
                output = output.Replace("manaCostPersValue", dataType.level0)
                output = output.Replace("manaCostValue", 0)
                output = output.Replace("maxManaCostValue", 0)
                foundManaCost = True
            End If

            If dataType.dataType = SpellDataType.MaxManaCost Then
                output = output.Replace("manaCostPersValue", 0)
                output = output.Replace("manaCostValue", 0)
                output = output.Replace("maxManaCostValue", dataType.level0)
                foundManaCost = True
            End If

        Next

        If foundManaCost = False Then
            output = output.Replace("manaCostValue", 0)
            output = output.Replace("manaCostPersValue", 0)
            output = output.Replace("maxManaCostValue", 0)
        End If

        If isMonsterSkill(data.m_name) Then
            output = output.Replace("monsterOnlyValue", "Yes")
        Else
            output = output.Replace("monsterOnlyValue", "No")
        End If

        Dim SpellTags As String = ""
        'Dim skillType As String = "Magical Skill"

        For Each tag As SpellTagClass In data.spellTags
            If tag.dataType = 2 Then
                'skillType = "Physical Skill"
            End If
            If tag.tooltipHidden = False Then
                SpellTags &= GetSpellTagName([Enum].GetName(GetType(SpellTag), tag.dataType))
                SpellTags &= ", "
            End If
        Next
        If SpellTags.Count <> 0 Then
            SpellTags = SpellTags.Substring(0, SpellTags.Count - 2)
        End If
        output = output.Replace("spellTagsValue", SpellTags)

        output = output.Replace("spellTypeValue", [Enum].GetName(GetType(SpellType), data.spellType))
        output = output.Replace("toggleSpellGroupValue", [Enum].GetName(GetType(ToggleGroup), data.toggleGroup))
        output = output.Replace("effectGroupValue", Enumeratori.combinedEnumeratorName(GetType(EffectGroup), data.effectgroup))
        output = output.Replace("irremovableValue", [Enum].GetName(GetType(Irremovable), data.irremovable))
        output = output.Replace("spellUnicityGroupValue", [Enum].GetName(GetType(SpellUnicityGroup), data.unicityGroup))
        output = output.Replace("spellTooltipActivationTypeValue", [Enum].GetName(GetType(tooltipActivationType), data.tooltipActivationType))
        output = output.Replace("spellTooltipTargetTypeValue", [Enum].GetName(GetType(tooltipTargetType), data.tooltipTargetType))
        output = output.Replace("spellPathfindingValue", [Enum].GetName(GetType(pathfinding), data.pathfinding))
        output = output.Replace("readyTriggerValue", [Enum].GetName(GetType(ReadyTrigger), data.readyTrigger))
        output = output.Replace("secureHitValue", [Enum].GetName(GetType(SecureHit), data.SecureHit))
        output = output.Replace("removeStealthValue", [Enum].GetName(GetType(dontRemoveStealth), Not data.dontRemoveStealth))
        output = output.Replace("blockCheckValue", [Enum].GetName(GetType(avoidBlockControllerCheck), data.avoidBlockControllerCheck))
        output = output.Replace("activationTypeValue", [Enum].GetName(GetType(ActivationType), data.activationType))
        output = output.Replace("targetTypeValue", [Enum].GetName(GetType(TargetType), data.targetType))
        output = output.Replace("targetEntityValue", Enumeratori.combinedEnumeratorName(GetType(TargetEntityType), data.targetEntityType))
        output = output.Replace("targetingRangeValue", data.TargetingRange)
        output = output.Replace("castingRangeValue", data.castingRange)
        output = output.Replace("checkLosValue", [Enum].GetName(GetType(checkLos), data.checkLos))
        output = output.Replace("statusOnTargetValue", [Enum].GetName(GetType(RequireStatusOnTarget), data.requireStatusOnTarget))



        Return output
    End Function


    Private Function GetDescription(data As SpellClassMirror) As String 'extracts the description of the spell from the I2Language file


        Dim rawDescription As String
        Dim term As String

        term = "spells/" + data.SpellKey + "_description"
        rawDescription = deserializedlanguageData.returnTerm(term, language)
        While rawDescription.IndexOf("({") <> -1
            rawDescription = rawDescription.Remove(rawDescription.IndexOf("({"), rawDescription.IndexOf(")", rawDescription.IndexOf("({")) - rawDescription.IndexOf("({") + 1)
        End While
        rawDescription = rawDescription.Replace(vbCr, " ").Replace(vbLf, " ")




        Return rawDescription
    End Function

    Private Function GetSkillParameters(data As SpellClassMirror) As String
        Dim term As String
        Dim parameters As String = ""

        For Each dataType As spellDataInfo In data.spellData
            If dataType.dataType <> SpellDataType.ManaCost And dataType.dataType <> SpellDataType.ManaCostPerSecond And dataType.dataType <> SpellDataType.Cooldown And dataType.dataType <> SpellDataType.MaxManaCost Then
                Dim dataDescription As String = ""
                term = GetSpellDataTypeName([Enum].GetName(GetType(SpellDataType), dataType.dataType))
                If dataType.attributeType <> CharacterAttribute.None Then
                    If dataType.level0 <> dataType.level10 Then
                        dataDescription = term & ": (" & dataType.level0 & " to " & dataType.level10 & ") x " & [Enum].GetName(GetType(SpellCoefficientAttributeType), dataType.attributeType)
                    Else
                        dataDescription = term & ": " & dataType.level0 & " x " & [Enum].GetName(GetType(SpellCoefficientAttributeType), dataType.attributeType)
                    End If
                Else
                    If dataType.level0 <> dataType.level10 Then
                        dataDescription = term & ": " & dataType.level0 & " to " & dataType.level10
                    Else
                        dataDescription = term & ": " & dataType.level0
                    End If

                End If
                If dataType.modifierType <> SaveModifierType.None Then
                    dataDescription &= " (Reduced by: " & [Enum].GetName(GetType(SaveModifierType), dataType.modifierType) & ")"
                End If
                parameters &= dataDescription
                parameters &= "</br>"
            End If
        Next
        If parameters <> "" Then
            parameters = parameters.Substring(0, parameters.Count() - 5) 'tolgo il vbCrlf di troppo
        End If
        Return parameters

    End Function

    Public Shared Function GetTooltipName(spellname As String, deserializedlanguageData As LanguageData, language As Integer) 'used also by monsterDataCreator and StatusEffectDataCreator
        Dim tooltipName As String
        Dim term As String

        term = "spells/" + spellname + "_name"
        tooltipName = deserializedlanguageData.returnTerm(term, language)
        If tooltipName = "Counterstrike" Then
            tooltipName = "Counter Strike"
        End If
        If tooltipName = "DoubleStrike" Then
            tooltipName = "Double Strike"
        End If

        Return tooltipName


    End Function

    Private Function GetSpellDataTypeName(dataType As String) As String
        Dim term As String = "spells/spelldata_" & dataType
        Return deserializedlanguageData.returnTerm(term, language)
    End Function

    Private Function GetSpellTagName(spellTag As String) As String
        Dim term As String = "spells/spelltag_" & spellTag
        Return deserializedlanguageData.returnTerm(term, language)
    End Function


    Public Sub New(language As Integer)

        Me.language = language

    End Sub

    Private Function correctWeightEnumerator(Value As String) As String
        Dim output As String

        Select Case Value
            Case "Medium"
                output = "Light/Medium"
                Exit Select
            Case "Heavy"
                output = "Light, Heavy"
                Exit Select
            Case "Medium_Heavy"
                output = "Medium, Heavy"
                Exit Select
            Case Else
                output = Value

        End Select
        Return output
    End Function

    Private Function correctDamageEnumerator(Value As String) As String
        Dim output As String

        Select Case Value
            Case "Slash_Pierce"
                output = "Slash or Pierce"
                Exit Select
            Case "Crush_Slash"
                output = "Crush or Slash"
                Exit Select
            Case "Crush_Pierce"
                output = "Crush or Pierce"
                Exit Select
            Case Else
                output = Value

        End Select
        Return output
    End Function

    Private Function isMonsterSkill(spell As String) As Boolean
        Select Case spell
            Case "spell_ArborealDragonBreath_ArborealDragon"
                Return True
            Case "spell_BattleJump_Ghoul"
                Return True
            Case "spell_EmberDragonBreath_EmberDragon"
                Return True
            Case "spell_Firebolt_Totem"
                Return True
            Case "spell_FrostBlast_Totem"
                Return True
            Case "spell_LegendAxfitmithissTouch"
                Return True
            Case "spell_LegendGrooveQuake"
                Return True
            Case "spell_LegendGrooveRam"
                Return True
            Case "spell_LegendIceSpikesRing"
                Return True
            Case "spell_LegendLargeSlow"
                Return True
            Case "spell_LegendMegaEruption"
                Return True
            Case "spell_LegendShivers"
                Return True
            Case "spell_LegendWOPSilence"
                Return True
            Case "spell_MagicReflection_ColossalHuskWorm"
                Return True
            Case "spell_MountainDragonBreath_MountainDragon"
                Return True
            Case "spell_ShockingLash_Totem"
                Return True
            Case "spell_SkeletalDragonBreath_SkeletalDragon"
                Return True
            Case "spell_SpikeTrapGrokoton"
                Return True
            Case "spell_Argadr_Execute"
                Return True
            Case "spell_Argadr_LightningStorm"
                Return True
            Case "spell_Argadr_Rage"
                Return True
            Case "spell_Argadr_StaticPull"
                Return True
            Case "spell_Argadr_StormBlade"
                Return True
            Case "spell_Axfitmithiss_AuraOfTerror"
                Return True
            Case "spell_Axfitmithiss_ChillingTouch"
                Return True
            Case "spell_Axfitmithiss_IceSpikesBarrage"
                Return True
            Case "spell_Axfitmithiss_MassSilenceWOP"
                Return True
            Case "spell_Axfitmithiss_Shivers"
                Return True
            Case "spell_Groove_ConcussiveQuake"
                Return True
            Case "spell_Groove_ConcussiveStrike"
                Return True
            Case "spell_Groove_Slow"
                Return True
            Case "spell_LordEoronn_BramblePrison"
                Return True
            Case "spell_LordEoronn_EntanglingTouch"
                Return True
            Case "spell_LordEoronn_VerdantRegrowth"
                Return True
            Case "spell_Malakar_AuraOfTerror"
                Return True
            Case "spell_Malakar_BattleJump"
                Return True
            Case "spell_Malakar_ChillingTouch"
                Return True
            Case "spell_Malakar_DisruptingStrike"
                Return True
            Case "spell_Malakar_RendArmor"
                Return True
            Case "spell_MuragGul_BattleJump"
                Return True
            Case "spell_MuragGul_ConcussiveStrike"
                Return True
            Case "spell_MuragGul_Overpower"
                Return True
            Case "spell_MuragGul_Petrify"
                Return True
            Case "spell_Syr_Decimate"
                Return True
            Case "spell_Syr_Eviscerate"
                Return True
            Case "spell_Syr_HeavyBlow"
                Return True
            Case "spell_Zephyr_HealingOrb"
                Return True
            Case "spell_Zephyr_MassCureWounds"
                Return True
            Case "spell_Zephyr_MassShockingLash"
                Return True
            Case "spell_Zephyr_Relocate"
                Return True
            Case "spell_Zephyr_StaticDischarge"
                Return True
            Case Else
                Return False



        End Select
    End Function


End Class




