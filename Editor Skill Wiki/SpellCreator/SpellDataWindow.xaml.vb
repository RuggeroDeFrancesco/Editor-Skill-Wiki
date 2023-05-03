Imports System.ComponentModel
Public Class SpellDataWindow

#Region "Property Changed"
    Implements INotifyPropertyChanged

    Public Event PropertyChanged(sender As Object, e As PropertyChangedEventArgs) _
        Implements INotifyPropertyChanged.PropertyChanged

    Private Sub OnInternalPropertyChanged(ByVal info As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
    End Sub
#End Region

    Public Sub New()

        InitializeComponent()


        OutputBlock.DataContext = Me
        DescriptionBlock.DataContext = Me
        ComboBox0.DataContext = Me
        ComboBox1.DataContext = Me
        ComboBox2.DataContext = Me
        ComboBox3.DataContext = Me
        ComboBox4.DataContext = Me
        ComboBox5.DataContext = Me
        LanguageCombo.ItemsSource = [Enum].GetValues(GetType(EnumLanguage))
        'I check if there is a file with some saved combinations, if not I create a new one.

        If System.IO.File.Exists(IO.Path.Combine(Environment.CurrentDirectory, "SpellCreator/customDataCombinations.txt")) Then
            Dim rawData As String
            rawData = System.IO.File.ReadAllText(IO.Path.Combine(Environment.CurrentDirectory, "SpellCreator/customDataCombinations.txt"))
            customDataCombinations = Newtonsoft.Json.JsonConvert.DeserializeObject(Of List(Of CustomDataCombination))(rawData)
            If customDataCombinations Is Nothing Then
                customDataCombinations = New List(Of CustomDataCombination)
            End If
        Else
            IO.File.Create(IO.Path.Combine(Environment.CurrentDirectory, "SpellCreator/customDataCombinations.txt")).Dispose()
            customDataCombinations = New List(Of CustomDataCombination)
        End If

    End Sub
    Dim languageSelected As EnumLanguage = EnumLanguage.English
    Dim spellDataCreator As SpellDataCreator
    Dim DescriptionResetValue As String
    Dim customDataCombinations As List(Of CustomDataCombination)
    Dim customDataCombinationsModifed As Boolean = False
    Dim spellName As String

#Region "Properties"
    Private Property _customDataNameList As ObjectModel.ObservableCollection(Of String)

    Public Property CustomDataNameList As ObjectModel.ObservableCollection(Of String)
        Get
            Return Me._customDataNameList
        End Get
        Set(value As ObjectModel.ObservableCollection(Of String))
            _customDataNameList = value
            Me.OnInternalPropertyChanged("CustomDataNameList")
        End Set
    End Property

    Private Property _customDataValueList As ObjectModel.ObservableCollection(Of String)

    Public Property CustomDataValueList As ObjectModel.ObservableCollection(Of String)
        Get
            Return Me._customDataValueList
        End Get
        Set(value As ObjectModel.ObservableCollection(Of String))
            _customDataValueList = value
            Me.OnInternalPropertyChanged("CustomDataValueList")
        End Set
    End Property

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

    Public Property Description As String
        Get
            Return Me._description
        End Get
        Set(value As String)
            _description = value
            Me.OnInternalPropertyChanged("Description")
        End Set
    End Property

#End Region



    Private Sub BrowseFile_Click(sender As Object, e As RoutedEventArgs)
        GetSingleSpellDataOutput()

    End Sub

    Private Sub CreateOutput_Click(sender As Object, e As RoutedEventArgs)
        GetSpellDataOutputFromFolder()
    End Sub

    Private Sub GetSingleSpellDataOutput()
        Try
            Dim OpenFileDialog1 As New Microsoft.Win32.OpenFileDialog
            OpenFileDialog1.Title = "Open File..."
            OpenFileDialog1.Multiselect = False
            OpenFileDialog1.Filter = "All Files|*.*"
            OpenFileDialog1.ShowDialog()
            Dim path As String = OpenFileDialog1.FileName
            spellDataCreator = New SpellDataCreator(Convert.ToInt32(languageSelected))
            spellDataCreator.parseData(path)
            spellName = spellDataCreator.SpellName
            Description = spellDataCreator.rawDescription
            'Description = CheckExistingCustomDataCombinations(spellDataCreator.rawDescription)
            'CustomDataNameList = spellDataCreator.customDataNameList
            'CustomDataValueList = spellDataCreator.customDataValueList
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try
    End Sub

    Private Sub GetSpellDataOutputFromFolder()
        Dim fbd As Ookii.Dialogs.Wpf.VistaFolderBrowserDialog = New Ookii.Dialogs.Wpf.VistaFolderBrowserDialog
        Dim path As String = Nothing
        Dim files As System.IO.FileInfo() = Nothing
        If fbd.ShowDialog() Then
            path = fbd.SelectedPath

        End If
        If path IsNot Nothing Then
            Dim dinfo As New System.IO.DirectoryInfo(path)
            files = dinfo.GetFiles("*.json")
        End If

        If files IsNot Nothing AndAlso files.Count <> 0 Then
            Dim spellsInserted As New List(Of String)
            For Each file As IO.FileInfo In files
                spellDataCreator = New SpellDataCreator(Convert.ToInt32(languageSelected))
                spellDataCreator.parseData(file.FullName)
                If True AndAlso Not spellDataCreator.removed AndAlso spellsInserted.IndexOf(spellDataCreator.SpellName) = -1 Then
                    spellName = spellDataCreator.SpellName
                    'DescriptionResetValue = spellDataCreator.rawDescription
                    'Description = CheckExistingCustomDataCombinations(spellDataCreator.rawDescription)
                    Description = spellDataCreator.rawDescription
                    Description = createHyperlinks(Description)
                    OutputText &= spellDataCreator.Output.Replace("descriptionValue", Description)
                    OutputText = OutputText.Replace("skillParametersValue", spellDataCreator.parameters)
                    OutputText &= vbCrLf
                    OutputText &= vbCrLf
                    spellsInserted.Add(spellDataCreator.SpellName)
                End If
            Next
        End If



    End Sub

#Region "Combo Boxes"
    Private Sub ComboBox0_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If ComboBox0.SelectedItem IsNot Nothing Then
            Description = Description.Replace("{0}", CustomDataValueList(CustomDataNameList.IndexOf(ComboBox0.SelectedItem)))
        End If
    End Sub

    Private Sub ComboBox1_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If ComboBox1.SelectedItem IsNot Nothing Then
            Description = Description.Replace("{1}", CustomDataValueList(CustomDataNameList.IndexOf(ComboBox1.SelectedItem)))
        End If
    End Sub

    Private Sub ComboBox2_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If ComboBox2.SelectedItem IsNot Nothing Then
            Description = Description.Replace("{2}", CustomDataValueList(CustomDataNameList.IndexOf(ComboBox2.SelectedItem)))
        End If
    End Sub

    Private Sub ComboBox3_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If ComboBox3.SelectedItem IsNot Nothing Then
            Description = Description.Replace("{3}", CustomDataValueList(CustomDataNameList.IndexOf(ComboBox3.SelectedItem)))
        End If
    End Sub

    Private Sub ComboBox4_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If ComboBox4.SelectedItem IsNot Nothing Then
            Description = Description.Replace("{4}", CustomDataValueList(CustomDataNameList.IndexOf(ComboBox4.SelectedItem)))
        End If
    End Sub

    Private Sub ComboBox5_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If ComboBox5.SelectedItem IsNot Nothing Then
            Description = Description.Replace("{5}", CustomDataValueList(CustomDataNameList.IndexOf(ComboBox5.SelectedItem)))
        End If
    End Sub

    Private Sub LanguageCombo_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)
        If LanguageCombo.SelectedItem IsNot Nothing Then
            languageSelected = LanguageCombo.SelectedItem
        End If
    End Sub

#End Region

    Private Function CheckExistingCustomDataCombinations(rawDescription As String) As String
        Dim Description As String
        Dim combinationFound As Integer = -1

        For Each combination As CustomDataCombination In customDataCombinations
            If combination.SpellName = Me.spellName Then
                combinationFound = customDataCombinations.IndexOf(combination)
            End If
        Next

        If combinationFound <> -1 Then
            Description = rawDescription
            If customDataCombinations(combinationFound).Combination0 IsNot Nothing Then
                Description = Description.Replace("{0}", customDataCombinations(combinationFound).Combination0)
            End If
            If customDataCombinations(combinationFound).Combination1 IsNot Nothing Then
                Description = Description.Replace("{1}", customDataCombinations(combinationFound).Combination1)
            End If
            If customDataCombinations(combinationFound).Combination2 IsNot Nothing Then
                Description = Description.Replace("{2}", customDataCombinations(combinationFound).Combination2)
            End If
            If customDataCombinations(combinationFound).Combination3 IsNot Nothing Then
                Description = Description.Replace("{3}", customDataCombinations(combinationFound).Combination3)
            End If
            If customDataCombinations(combinationFound).Combination4 IsNot Nothing Then
                Description = Description.Replace("{4}", customDataCombinations(combinationFound).Combination4)
            End If
            If customDataCombinations(combinationFound).Combination5 IsNot Nothing Then
                Description = Description.Replace("{5}", customDataCombinations(combinationFound).Combination5)
            End If

        Else
            Description = rawDescription
        End If

        Return Description
    End Function

    Private Function createHyperlinks(description As String) As String
        Dim listOfHyperlinkTermsEN As List(Of hyperlinkTerm) = New List(Of hyperlinkTerm)
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("mana", "[[Mana]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("health", "[[Health]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("health regeneration", "[[Health Regeneration]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("armor", "[[Armor]]"))
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
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("warm", "[[Warm]]"))
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
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("stun", "[[Stunned|Stun]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm(" stunned ", "[[Stunned]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("frightened", "[[Frightened]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("dazed", "[[Dazed]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("confused", "[[Confused]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("confusing", "[[Confused|Confusing]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("hidden", "[[Hidden]]"))
        listOfHyperlinkTermsEN.Add(New hyperlinkTerm("paralyzed", "[[Paralyzed]]"))
        Dim listOfHyperlinkTermsDE As List(Of hyperlinkTerm) = New List(Of hyperlinkTerm)
        Dim listOfHyperlinkTermsFR As List(Of hyperlinkTerm) = New List(Of hyperlinkTerm)
        Select Case languageSelected
            Case EnumLanguage.English
                For Each term As hyperlinkTerm In listOfHyperlinkTermsEN
                    description = description.Replace(term.term, term.hyperlink, StringComparison.OrdinalIgnoreCase)
                Next
            Case EnumLanguage.French
                For Each term As hyperlinkTerm In listOfHyperlinkTermsFR
                    description = description.Replace(term.term, term.hyperlink, StringComparison.OrdinalIgnoreCase)
                Next
            Case EnumLanguage.German
                For Each term As hyperlinkTerm In listOfHyperlinkTermsDE
                    description = description.Replace(term.term, term.hyperlink, StringComparison.OrdinalIgnoreCase)
                Next
        End Select

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

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        If Description Is Nothing Then
            Exit Sub
        End If
        OutputText = spellDataCreator.Output.Replace("descriptionValue", Description)

        Dim newCombination As New CustomDataCombination
        newCombination.SpellName = Me.spellName
        If ComboBox0.SelectedItem IsNot Nothing Then
            newCombination.Combination0 = CustomDataValueList(CustomDataNameList.IndexOf(ComboBox0.SelectedItem))
        End If
        If ComboBox1.SelectedItem IsNot Nothing Then
            newCombination.Combination1 = CustomDataValueList(CustomDataNameList.IndexOf(ComboBox1.SelectedItem))
        End If
        If ComboBox2.SelectedItem IsNot Nothing Then
            newCombination.Combination2 = CustomDataValueList(CustomDataNameList.IndexOf(ComboBox2.SelectedItem))
        End If
        If ComboBox3.SelectedItem IsNot Nothing Then
            newCombination.Combination3 = CustomDataValueList(CustomDataNameList.IndexOf(ComboBox3.SelectedItem))
        End If
        If ComboBox4.SelectedItem IsNot Nothing Then
            newCombination.Combination4 = CustomDataValueList(CustomDataNameList.IndexOf(ComboBox4.SelectedItem))
        End If
        If ComboBox5.SelectedItem IsNot Nothing Then
            newCombination.Combination5 = CustomDataValueList(CustomDataNameList.IndexOf(ComboBox5.SelectedItem))
        End If





        If customDataCombinations.Count = 0 Then
            customDataCombinations.Add(newCombination)
            customDataCombinationsModifed = True
        Else
            Dim combinationFound As Boolean = False
            For Each combination As CustomDataCombination In customDataCombinations
                If combination.SpellName = Me.spellName Then

                    customDataCombinations(customDataCombinations.IndexOf(combination)) = newCombination
                    customDataCombinationsModifed = True
                    combinationFound = True
                    Exit For
                End If
            Next
            If Not combinationFound Then
                customDataCombinations.Add(newCombination)
                customDataCombinationsModifed = True
            End If

        End If


    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        Description = DescriptionResetValue
        ComboBox0.SelectedItem = Nothing
        ComboBox1.SelectedItem = Nothing
        ComboBox2.SelectedItem = Nothing
        ComboBox3.SelectedItem = Nothing
        ComboBox4.SelectedItem = Nothing
        ComboBox5.SelectedItem = Nothing

        For Each combination As CustomDataCombination In customDataCombinations
            If combination.SpellName = Me.spellName Then
                customDataCombinations.Remove(combination)
                customDataCombinationsModifed = True
                Exit For
            End If
        Next


    End Sub

    Private Sub Window_Closed(sender As Object, e As EventArgs)
        If customDataCombinationsModifed Then
            Dim combinationData As String
            combinationData = System.Text.Json.JsonSerializer.Serialize(customDataCombinations)
            IO.File.WriteAllText(IO.Path.Combine(Environment.CurrentDirectory, "SpellCreator/customDataCombinations.txt"), combinationData)

        End If

    End Sub


End Class
