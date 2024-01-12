Class MainWindow



    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

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

    Private Sub GenerateLootData(sender As Object, e As RoutedEventArgs)
        Try
            Dim LootDataWindow As New LootDataWindow()
            LootDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Private Sub GenerateSeData(sender As Object, e As RoutedEventArgs)
        Try
            Dim SeDataWindow As New SeDataWindow()
            SeDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Private Sub GenerateImbueData(sender As Object, e As RoutedEventArgs)
        Try
            Dim ImbueDataWindow As New ImbueDataWindow()
            ImbueDataWindow.Show()
        Catch ex As Exception
            MsgBox(ex.Message)
            MsgBox(ex.StackTrace)
        End Try

    End Sub

    Public Shared Function GetAssets() As AssetItemsList
        Dim assets As New AssetItemsList
        Dim serializer As New System.Xml.Serialization.XmlSerializer(GetType(AssetItemsList))
        assets = serializer.Deserialize(New System.IO.FileStream(IO.Path.Combine(Environment.CurrentDirectory, "AssetIndices/unifiedIndex.xml"), IO.FileMode.Open))
        Return assets
    End Function

    Public Shared Function GetAssetFromIndex(assetIndex As String, assets As AssetItemsList) As assetItem
        Dim selectedAsset As assetItem = assets.Assets.Find(Function(p) p.PathID = assetIndex)
        If selectedAsset Is Nothing Then
            Return New assetItem
        End If
        Return selectedAsset
    End Function

End Class

<Serializable()>
Public Class assetItem
    <System.Xml.Serialization.XmlElement("PathID")> Public PathID As String
    <System.Xml.Serialization.XmlElement("Name")> Public Name As String
End Class

<Serializable(), System.Xml.Serialization.XmlRoot("Assets")>
Public Class AssetItemsList
    <System.Xml.Serialization.XmlElement("Asset")> Public Assets As List(Of assetItem)
End Class