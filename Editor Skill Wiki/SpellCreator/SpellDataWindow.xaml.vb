Imports System.ComponentModel
Imports Newtonsoft.Json
Public Class SpellDataWindow

#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Public Sub New(commonFunctions As CommonFunctions)

        InitializeComponent()

        spellList = New List(Of MirrorClasses.SpellInfo)
        OutputBlock.DataContext = Me
        OutputText = ""
        Me.commonFunctions = commonFunctions
        commonFunctions.AddLogEntry("Initialized SpellData Window.")
        spellDataCreator = New SpellDataCreator(commonFunctions)
        CompileSpellList()
        GetSpellDataOutput(spellList)
    End Sub
    Dim commonFunctions As CommonFunctions
    Dim spellDataCreator As SpellDataCreator
    Dim spellName As String
    Dim spellList As List(Of MirrorClasses.SpellInfo)

#Region "Properties"

    Private Property _OutputText As String

    Public Property OutputText As String
        Get
            Return Me._OutputText
        End Get
        Set(value As String)
            _OutputText = value
            Me.OnInternalPropertyChanged("OutputText")
        End Set
    End Property

    Private Property _description As String

#End Region
    Private Sub CompileSpellList()
        CommonFunctions.AddLogEntry("Compiling Spell List.")
        For Each monster As MirrorClasses.MonsterClass In commonFunctions.MonsterList.FindAll(Function(p) p.champion = False And p.excludeFromKnowledgeSystem = False)
            For Each spell As MirrorClasses.KnowledgeUnlockableMonsterSpell In monster.MonsterSpellsList
                Dim assetPath As String = commonFunctions.GetAssetName(spell.value.m_PathID, "spell_")
                commonFunctions.AddLogEntry("Deserializing data from:" & assetPath)
                Dim spelldata = JsonConvert.DeserializeObject(Of MirrorClasses.SpellInfo)(commonFunctions.GetRawData(assetPath))
                AddSpellToSpellList(spelldata, True)
            Next
        Next
        For Each monster As MirrorClasses.MonsterClass In commonFunctions.MonsterList.FindAll(Function(p) p.champion = True)
            For Each spell As MirrorClasses.KnowledgeUnlockableMonsterSpell In monster.MonsterSpellsList
                Dim assetPath As String = commonFunctions.GetAssetName(spell.value.m_PathID)
                commonFunctions.AddLogEntry("Deserializing data from:" & assetPath)
                Dim spelldata = JsonConvert.DeserializeObject(Of MirrorClasses.SpellInfo)(commonFunctions.GetRawData(assetPath))
                AddSpellToSpellList(spelldata, False)
            Next
        Next
    End Sub

    Private Sub AddSpellToSpellList(spell As MirrorClasses.SpellInfo, playerSpell As Boolean)
        If spellList.FindAll(Function(p) p.SpellKey = spell.SpellKey).Count = 0 Then
            spell.PlayerSpell = playerSpell
            spellList.Add(spell)
            If playerSpell Then
                commonFunctions.AddLogEntry("Added skill " & spell.SpellKey & " to Spell List as a player skill.")
            Else
                commonFunctions.AddLogEntry("Added skill " & spell.SpellKey & " to Spell List as a monster only skill.")
            End If

        End If
    End Sub


    Private Sub GetSpellDataOutput(spells As List(Of MirrorClasses.SpellInfo))
        commonFunctions.AddLogEntry("Generating Spell Output.")
        For Each spell As MirrorClasses.SpellInfo In spells
            If spell.gameReady = False Then
                commonFunctions.AddLogEntry("Skill " & spell.SpellKey & " is not game ready. Skipping.")
            Else
                spellDataCreator.createOutput(spell)
                spellDataCreator.Description = createHyperlinks(spellDataCreator.Description)
                OutputText &= spellDataCreator.Output.Replace("descriptionValue", spellDataCreator.Description)
                OutputText &= vbCrLf
                OutputText &= vbCrLf
            End If
        Next


    End Sub



    Private Function createHyperlinks(description As String) As String
        Dim listOfHyperlinkTermsEN As List(Of hyperlinkTerm) = New List(Of hyperlinkTerm)
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" mana ", " [[Mana]] "))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" health ", " [[Health]] "))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("health regeneration", "[[Health Regeneration]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" armor ", " [[Armor]] "))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("resistance", "[[Resistances|Resistance]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("movement speed", "[[Move Speed|Movement Speed]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("attack speed", "[[Attack Speed]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("intelligence", "[[Intelligence]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("strength", "[[Strength]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("dexterity", "[[Dexterity]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("constitution", "[[Constitution]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("perception", "[[Perception]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("charisma", "[[Charisma]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("willpower", "[[Willpower]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("fortitude", "[[Fortitude]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("evasion", "[[Evasion]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("accuracy", "[[Accuracy]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("stealth", "[[Stealth]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("corrosion", "[[Corrosion]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("chilled", "[[Chilled]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("frozen", "[[Frozen]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("shocked", "[[Shocked]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" warm ", "[[Warm]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("burning", "[[Burning]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("poisoned", "[[Poisoned]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" bleed ", " [[Bleeding|Bleed]] "))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" bleeding ", "[[Bleeding]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("silenced", "[[Silenced]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("weakened", "[[Weakened]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("snared", "[[Snared]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("magnetized", "[[Magnetized]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("petrify", "[[Petrified|Petrify]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("slowed", "[[Slowed]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("slowing", "[[Slowed|Slowing]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("cripple", "[[Cripple]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("crippled", "[[Cripple|Crippled]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("blinding", "[[Blinded|Blinding]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" blinded ", " [[Blinded]] "))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" stun ", "[[Stunned|Stun]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" stunned ", "[[Stunned]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("frightened", "[[Frightened]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("dazed", "[[Dazed]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("confused", "[[Confused]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("confusing", "[[Confused|Confusing]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("hidden", "[[Hidden]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("paralyzed", "[[Paralyzed]]"))
        Dim listOfHyperlinkTermsDE As List(Of hyperlinkTerm) = New List(Of hyperlinkTerm)
        Dim listOfHyperlinkTermsFR As List(Of hyperlinkTerm) = New List(Of hyperlinkTerm)

        Return description
    End Function

    Private Class hyperlinkTerm
        Public term As String
        Public hyperlink As String
        Public Sub New(term As String, hyperlink As String)
            Me.term = term
            Me.hyperlink = hyperlink
        End Sub
    End Class

    Private Sub CopyToClipBoard()
        Clipboard.SetText(OutputText)
    End Sub

    Private Sub CopyToClipboard_Click(sender As Object, e As RoutedEventArgs)
        CopyToClipBoard()
    End Sub
End Class
