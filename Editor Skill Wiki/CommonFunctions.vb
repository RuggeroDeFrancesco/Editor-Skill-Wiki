
Imports System.IO
Imports System.Threading
Imports System.Xml.Serialization
Imports Editor_Skill_Wiki.Enumerators
Imports Editor_Skill_Wiki.MirrorClasses
Imports Newtonsoft.Json

Public Class CommonFunctions



    Public Sub New()
        terms = New Languages
        CurrentLog = New Log
        MonsterList = New List(Of MirrorClasses.MonsterClass)
        AssetIndex = New AssetList
        IconIndex = New AssetList
        AddHandler terms.NewLogEntry, AddressOf CurrentLog.AddLogEntry

        GetAssetIndex()
        GetIconIndex()
        GetLanguageData()
        GetMonsterList()
    End Sub

    Public WithEvents terms As Languages

    Public WithEvents CurrentLog As Log

    Public MonsterList As List(Of MirrorClasses.MonsterClass)

    Public AssetIndex As AssetList

    Public IconIndex As AssetList


    Public Function GetRawData(path As String) As String
        CurrentLog.AddLogEntry("Loading data from:" & path)
        Dim assetFolder As String = IO.Path.Combine(Environment.CurrentDirectory, "Assets")
        path = IO.Path.Combine(assetFolder, path) & ".json"
        Return IO.File.ReadAllText(path)
    End Function

    'Public Function DeserializeData(path As String, type As class) As Object
    '    CurrentLog.AddLogEntry("Deserializing data from:" & path)
    '    Dim output = JsonConvert.DeserializeObject(Of MirrorClasses.SpellInfo)(GetRawData(path))
    '    Return output
    'End Function

    Public Sub GetMonsterList()
        'Dim list As String = IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/list_monsters.json"))
        'CurrentLog.AddLogEntry("Monster list found.")
        'Dim deserializedList As TypeList = JsonConvert.DeserializeObject(Of TypeList)(list)
        Dim di As New IO.DirectoryInfo(System.IO.Path.Combine(Environment.CurrentDirectory, "Assets"))
        Dim fi As IO.FileInfo() = di.GetFiles
        For Each file As FileInfo In fi
            If file.Name.StartsWith("class_") Then
                Dim monsterData As MirrorClasses.MonsterClass = JsonConvert.DeserializeObject(Of MirrorClasses.MonsterClass)(IO.File.ReadAllText(file.FullName))
                MonsterList.Add(monsterData)
            End If
        Next
        CurrentLog.AddLogEntry("Deserialized monster list created.")
    End Sub

    Private Sub GetAssetIndex()
        Dim serializer As XmlSerializer = New XmlSerializer(GetType(AssetList))
        Dim path As String = IO.Path.Combine(Environment.CurrentDirectory, "Assets/assets.xml")
        AssetIndex = serializer.Deserialize(IO.File.OpenRead(path))
        CurrentLog.AddLogEntry("Asset index created.")
    End Sub

    Private Sub GetIconIndex()
        Dim serializer As XmlSerializer = New XmlSerializer(GetType(AssetList))
        IconIndex = CType(serializer.Deserialize(IO.File.OpenRead(IO.Path.Combine(Environment.CurrentDirectory, "Assets/icons.xml"))), AssetList)
        CurrentLog.AddLogEntry("Icon index created.")
    End Sub

    Private Sub GetLanguageData()
        Dim languageData As String = IO.File.ReadAllText(System.IO.Path.Combine(Environment.CurrentDirectory, "Assets/I2languages.json"))
        terms = JsonConvert.DeserializeObject(Of Languages)(languageData)
    End Sub

    Public Function GetAssetName(id As Integer, Optional preference As String = "") As String
        Dim assetsFound As List(Of Asset) = AssetIndex.Assets.FindAll(Function(p) p.PathID = id)
        If assetsFound.Count = 0 Then
            CurrentLog.AddLogEntry("Cannot find Asset ID: " & id)
            Return ""
        ElseIf assetsFound.Count > 1 Then
            CurrentLog.AddLogEntry("Duplicate asset ID: " & id)
            Dim restrictedList As Asset = assetsFound.Find(Function(p) p.Name.StartsWith(preference))
            If restrictedList IsNot Nothing Then
                Return restrictedList.Name
            Else
                CurrentLog.AddLogEntry("Cannot Resolve Duplicate asset ID: " & id)
                Return ""
            End If
        Else
            Return assetsFound(0).Name
        End If
    End Function

    Public Function GetIconName(id As Integer) As String
        Dim iconFound As List(Of Asset) = IconIndex.Assets.FindAll(Function(p) p.PathID = id)
        If iconFound.Count = 0 Then
            CurrentLog.AddLogEntry("Cannot find Icon ID: " & id)
            Return ""
        ElseIf iconFound.Count > 1 Then
            CurrentLog.AddLogEntry("Duplicate Icon ID: " & id)
            Dim restrictedList As Asset = iconFound.Find(Function(p) p.Name.StartsWith("icon_"))
            If restrictedList IsNot Nothing Then
                Return restrictedList.Name
            Else
                CurrentLog.AddLogEntry("Cannot Resolve Duplicate asset ID: " & id)
                Return ""
            End If
        Else
            Return iconFound(0).Name
        End If
    End Function

    Public Sub AddLogEntry(entry As String)
        CurrentLog.AddLogEntry(entry)
    End Sub


    Public Class Log
        Public Property LogEntry As New List(Of String)
        Public Event LogUpdated(entry As String)
        Private Sub UpdateLog(entry As String)
            RaiseEvent LogUpdated(entry)
        End Sub

        Public Sub AddLogEntry(entry As String)
            LogEntry.Add(entry)
            UpdateLog(entry)
            Console.WriteLine(entry)
        End Sub

    End Class
    Public Class Languages
        Public Event NewLogEntry(entry As String)
        Public Sub New()
            RaiseEvent NewLogEntry("Localization Terms Initialized")
            mSource = New TermsList
        End Sub
        Public mSource As TermsList
        Public Function getTerm(term As String, language As String) As String
            Dim terms As List(Of Term) = mSource.mTerms.FindAll(Function(p) p.Term = term)
            If terms.Count = 0 Then
                RaiseEvent NewLogEntry("Term not found:" & term)
                Return ""
            ElseIf terms.Count = 1 Then
                Return terms(0).Languages(language)
            Else
                RaiseEvent NewLogEntry("Too many terms found:" & term)
                Return terms(0).Languages(language)
            End If
        End Function


    End Class
    Public Class TermsList
        Public Sub New()
            mTerms = New List(Of Term)
        End Sub
        Public mTerms As List(Of Term)
    End Class

    Public Class Term
        Public Sub New()
            Languages = New List(Of String)
        End Sub
        Public Term As String
        Public TermType As String
        Public Languages As List(Of String)
    End Class

    Public Class TypeList
        Public itemTypeList As New List(Of TypeListEntry)
    End Class

    Public Class TypeListEntry
        Public m_FileID As Integer = 0
        Public m_PathID As Integer = 0
    End Class
    <Serializable(), XmlRoot("Assets")>
    Public Class AssetList
        <XmlElement("Asset")> Public Assets As New List(Of Asset)
    End Class

    <Serializable()>
    Public Class Asset
        Public Name As String
        Public PathID As Double
    End Class


End Class



