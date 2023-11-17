Imports System.ComponentModel
Imports Newtonsoft.Json
Imports System.Data

Public Class StatusEffectsDataCreator

#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Dim language As Integer

    Dim deserializedlanguageData As LanguageData

    Dim SkillToSETable As DataTable

    Private Property _Output As String

    Public Property Output As String
        Get
            Return Me._Output
        End Get
        Set(value As String)
            _Output = value
            Me.OnInternalPropertyChanged("Output")
        End Set
    End Property

    Private skillsFolder As String

    Public Sub New(language As Integer, skillsFolder As String)
        Me.language = language
        Dim rawlanguagedata As String = GetLanguageData()
        deserializedlanguageData = deserializeLanguageData(rawlanguagedata)
        Me.skillsFolder = skillsFolder
        SkillToSETable = CompileSkillToSETable()
    End Sub

    Public Sub GetStatusEffectTable(statusEffectPath As String) 'uses the private methods to extract the data from an SE file, deserialize it into a StatusEffectClassMirror class and extracts the generic information
        Dim data As StatusEffectClassMirror = deserializeStatusEffectData(cutData(getRawData(statusEffectPath)))
        Output = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "SpecialEffectsCreator/defaultSERow.txt"))

        Dim beneficial As String
        If data.Beneficial Then
            beneficial = "Beneficial"
        Else
            beneficial = "Harmful"
        End If

        Dim removable As String
        If data.irremovable Then
            removable = "Not Removable"
        Else
            removable = "Removable"
        End If

        Dim mounted As String
        If data.KeepWhenMounted Then
            mounted = "Keep when mounted"
        Else
            mounted = "Disabled when mounted"
        End If

        Dim putsInCombat As String
        If data.DontSetCombatFlag Then
            putsInCombat = "Does not keep in combat"
        Else
            putsInCombat = "Keeps in combat"
        End If

        Dim statusEffectName As String = GetStatusEffectName(data.statusKey, deserializedlanguageData, language)
        If statusEffectName = "" Then
            statusEffectName = IO.Path.GetFileNameWithoutExtension(statusEffectPath).Replace("se_", "")
        End If

        Dim relatedSkills As String = GetRelatedSkills(data.statusKey)
        Dim Occurrences As Integer = (relatedSkills.Length - relatedSkills.Replace("</br>", String.Empty).Length) / "</br>".Length

        Output = Output.Replace("StatusEffectName", statusEffectName)
        Output = Output.Replace("StatusEffectType", [Enum].GetName(GetType(EffectGroup), data.effectGroup))
        Output = Output.Replace("StatusEffectBeneficial", beneficial)
        Output = Output.Replace("CanBeRemoved", removable)
        Output = Output.Replace("KeepWhenMounted", mounted)
        Output = Output.Replace("PutsInCombat", putsInCombat)
        Output = Output.Replace("statusEffectIcon", ItemDataCreator.GetIcon(data.icon.m_PathID))
        If data.statusKey <> "" Then
            Output = Output.Replace("RelatedToSkill", relatedSkills)
        Else
            Output = Output.Replace("RelatedToSkill", "")
        End If

        Dim description As String = GetStatusEffectDescription(data.statusKey, deserializedlanguageData, language)

        If description = "No description available." AndAlso relatedSkills <> "" AndAlso Occurrences = 1 Then
            description = "Check " & relatedSkills.Replace("</br>", "")
        ElseIf description = "No description available." Then
            Output = ""
            Exit Sub
        Else
            description = InsertParameters(description, data)
        End If



        Output = Output.Replace("StatusEffectDescription", description)



    End Sub


    Private Function getRawData(path As String) As String 'reads the file and extracts the content as text
        If System.IO.File.Exists(path) Then
            Dim rawData As String
            rawData = System.IO.File.ReadAllText(path)
            Return rawData
        Else Throw New System.Exception("File does not exist.")
            Return ""
        End If
    End Function

    Private Function cutData(data As String) As String 'removes the useless parts of the file
        Dim processedData As String
        Dim startingIndex As Integer
        startingIndex = data.IndexOf("m_Name") - 1
        processedData = "{"
        processedData &= data.Substring(startingIndex)
        Return processedData
    End Function

    Shared Function deserializeStatusEffectData(data As String) As StatusEffectClassMirror 'converts the json to fields of the ItemClassMirror, used also by the loot creator

        Dim parsedData As StatusEffectClassMirror
        parsedData = JsonConvert.DeserializeObject(Of StatusEffectClassMirror)(data)
        Return parsedData
    End Function

    Private Function deserializeLanguageData(data As String) As LanguageData

        Dim parsedData As LanguageData
        Dim correctedData As String = data.Replace("\n", " ")
        parsedData = JsonConvert.DeserializeObject(Of LanguageData)(data)
        Return parsedData
    End Function

    Private Function GetLanguageData() As String
        Dim rawLanguageData As String
        rawLanguageData = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "I2languages.json"))
        Return rawLanguageData
    End Function

    Private Function GetStatusEffectName(statusKeys As String, deserializedLanguageData As LanguageData, language As Integer) As String
        Dim term As String = ""
        Dim output As String = ""

        term = "status_effect/" & statusKeys & "_name"
        output = deserializedLanguageData.returnTerm(term, language)


        If output = "" Then
            Return statusKeys
        Else
            Return output
        End If
    End Function

    Private Function GetStatusEffectDescription(statusKeys As String, deserializedLanguageData As LanguageData, language As Integer) As String
        Dim term As String = ""
        Dim output As String = ""

        term = "status_effect/" & statusKeys & "_desc"
        output = deserializedLanguageData.returnTerm(term, language)


        If output = "" Then
            Return "No description available."
        Else
            Return output
        End If
    End Function

    Private Function CompileSkillToSETable() As DataTable 'creates a table which crosslinks skill names to spell tags
        Dim table As New DataTable
        table.Columns.Add("Skill")
        table.Columns.Add("Tag")

        Dim files As System.IO.FileInfo() = Nothing
        If skillsFolder IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(skillsFolder)
            files = dinfo.GetFiles("*.json")
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then

            For Each file As IO.FileInfo In files

                Dim data As SpellClassMirror = SpellDataCreator.deserializeSpellData(SpellDataCreator.cutData(SpellDataCreator.getRawData(file.FullName)))
                Dim skillName As String = SpellDataCreator.GetTooltipName(data.SpellKey, deserializedlanguageData, language)
                Dim skillNameRow As DataRow = table.Rows.Add()
                skillNameRow("Skill") = skillName 'each skill has at least its own name and key as tags
                skillNameRow("Tag") = skillName
                Dim skillNameKeyRow As DataRow = table.Rows.Add()
                skillNameKeyRow("Skill") = skillName
                skillNameKeyRow("Tag") = data.SpellKey
                Dim skillNameKeyBuffRow As DataRow = table.Rows.Add()
                skillNameKeyBuffRow("Skill") = skillName 'sometimes they add buff at the end
                skillNameKeyBuffRow("Tag") = data.SpellKey & "Buff"

                For Each tag As SpellTagClass In data.spellTags
                    Dim newrow As DataRow = table.Rows.Add()
                    newrow("Skill") = skillName
                    newrow("Tag") = [Enum].GetName(GetType(SpellTag), tag.dataType)
                Next


            Next
        End If

        Return table
    End Function

    Private Function GetRelatedSkills(tagName As String) As String
        Dim output As String = ""
        Dim rows As New List(Of DataRow)
        rows = SkillToSETable.Select("Tag = '" & tagName & "'").ToList
        Dim addedSkills As New HashSet(Of String)
        For Each row As DataRow In rows
            If addedSkills.Add(row("Skill")) And row("Skill") <> "" Then
                output &= "[["
                output &= (row("Skill"))
                output &= "]]"
                output &= "</br>"
            End If
        Next
        Return output
    End Function

    Private Function InsertParameters(description As String, data As StatusEffectClassMirror) As String
        Dim newDescription As String = description

        Select Case data.statusKey
            Case "Atrophied"
                newDescription = newDescription.Replace("{0}", data.dexHardCap)
            Case "Bleeding"
                newDescription = newDescription.Replace("{0}", data.maxStack)
                newDescription = newDescription.Replace("{1}", data.stackLossFrequency)
                newDescription = newDescription.Replace("{2}", data.minLossPerc)
                newDescription = newDescription.Replace("{3}", data.maxLossPerc)
                newDescription = newDescription.Replace("{4}", data.minTargetHeatlhClamp)
                newDescription = newDescription.Replace("{5}", data.maxTargetHealthClamp)
                newDescription = AddStacksDecayInfo(newDescription, data)
            Case "Blinded"
                newDescription = newDescription.Replace("{0}", data.perceptionHardCap)
            Case "Burning"
                newDescription = newDescription.Replace("{0}", data.fireResitDebuff)
                newDescription = newDescription.Replace("{1}", data.damagePerTick)
                newDescription = newDescription.Replace("{2}", data.warmStackOnExpiration)
            Case "Chilled"
                newDescription = newDescription.Replace("{0}", data.maxStack)
                newDescription = newDescription.Replace("{1}", data.iceResistDebuffPerStack)
                newDescription = newDescription.Replace("{2}", data.moveSpeedDebuffPerStackPerc)
                newDescription = AddStacksDecayInfo(newDescription, data)
            Case "Confused"
                newDescription = newDescription.Replace("{0}", data.spellFailurePerc)
                newDescription = newDescription.Replace("{1}", data.debuff)
            Case "Corrosion"
                newDescription = newDescription.Replace("{0}", data.maxStack)
                newDescription = newDescription.Replace("{1}", data.armorDebuffPerStack)
                newDescription = newDescription.Replace("{2}", data.dmgPerStack)
                newDescription = AddStacksDecayInfo(newDescription, data)
            Case "Crippled"
                newDescription = newDescription.Replace("{0}", data.dexterityHardCap)
            Case "Dazed"
                newDescription = newDescription.Replace("{0}", data.willpowerDebuff)
            Case "PavedRoad"
                newDescription = newDescription.Replace("{0}", data.BonusMoveSpeed)
            Case "Frightened"
                newDescription = newDescription.Replace("{0}", data.damageDebuffPerc)
                newDescription = newDescription.Replace("{1}", data.saveModifiersDebuff)
                newDescription = newDescription.Replace("{2}", data.charismaHardCap)
            Case "Frozen"
                newDescription = newDescription.Replace("{0}", data.iceResitDebuff)
                newDescription = newDescription.Replace("{1}", data.armorBuff)
                newDescription = newDescription.Replace("{2}", data.chilledStackOnExpiration)
            Case "Hungry"
                newDescription = newDescription.Replace("{0}", data.healthRegenDebuff)
            Case "Petrified"
                newDescription = newDescription.Replace("{0}", data.slashArmor)
                newDescription = newDescription.Replace("{1}", data.crushArmor)
                newDescription = newDescription.Replace("{2}", data.pierceArmor)
            Case "Poisoned"
                newDescription = newDescription.Replace("{0}", data.maxStack)
                newDescription = newDescription.Replace("{1}", data.stackLossFrequency)
                newDescription = newDescription.Replace("{2}", data.minLossPerc)
                newDescription = newDescription.Replace("{3}", data.maxLossPerc)
                newDescription = newDescription.Replace("{4}", data.minTargetHeatlhClamp)
                newDescription = newDescription.Replace("{5}", data.maxTargetHealthClamp)
                newDescription = AddStacksDecayInfo(newDescription, data)
            Case "Rested"
                newDescription = newDescription.Replace("{0}", data.movSpeedBuff)
            Case "Sated"
                newDescription = newDescription.Replace("{0}", data.healthRegenBuff)
            Case "Shocked"
                newDescription = newDescription.Replace("{0}", data.maxStack)
                newDescription = newDescription.Replace("{1}", data.shockResistDebuffPerStack)
                newDescription = newDescription.Replace("{2}", data.shockedParalysisChancePerStack)
                newDescription = newDescription.Replace("{3}", data.shockedParalysisDuration)
                newDescription = AddStacksDecayInfo(newDescription, data)
            Case "Silenced"
                newDescription = newDescription.Replace("{0}", data.charismaHardCap)
            Case "Slowed"
                newDescription = newDescription.Replace("{0}", data.moveSpeedDebuffPerc)
            Case "Starving"
                newDescription = newDescription.Replace("{0}", data.healthRegenDebuff)
            Case "Tired"
                newDescription = newDescription.Replace("{0}", data.movSpeedDebuff)
            Case "Warm"
                newDescription = newDescription.Replace("{0}", data.maxStack)
                newDescription = newDescription.Replace("{1}", data.fireResistDebuffPerStack)
                newDescription = AddStacksDecayInfo(newDescription, data)
            Case "Weakened"
                newDescription = newDescription.Replace("{0}", data.strengthHardCap)
            Case Else
                If newDescription.IndexOf("}") <> -1 Then
                    newDescription = newDescription.Replace("{0}", "X")
                    newDescription = newDescription.Replace("{1}", "X")
                    newDescription = newDescription.Replace("{2}", "X")
                    newDescription = newDescription.Replace("{3}", "X")
                    newDescription = newDescription.Replace("{4}", "X")
                    newDescription = newDescription.Replace("{5}", "X")
                    newDescription &= "</br>"
                    newDescription &= "(The effects indicated as X depend on the skill which caused the effect, check the related skills for the details)"
                End If

        End Select


        Return newDescription
    End Function

    Private Function AddStacksDecayInfo(description As String, data As StatusEffectClassMirror) As String
        Dim newDescription As String = description
        newDescription &= "</br>"
        newDescription &= "Stacks decay at a rate of " & data.stackLossAmount & " every " & data.stackLossFrequency & " seconds."
        Return newDescription
    End Function

End Class
