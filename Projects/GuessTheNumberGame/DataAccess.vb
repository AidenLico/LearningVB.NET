'File for accessing Json Game Data and Storing Game Data

Imports System.IO
Imports System.Text.Json

'Class cobtaining game data properties
Public Class GameData
    Public Property LastNumber As Integer
    Public Property LastGuessCount As Integer
    Public Property BestScore As Integer
    Public Property LastPlayed As DateOnly
End Class

'Data Access module
Module DataAccess

    'Sub takes game data and file path as inputs, stores them ikn json
    Sub SaveGameData(data As GameData, filePath As String)
        Dim options As New JsonSerializerOptions With {
            .WriteIndented = True
        }
        Dim json As String = JsonSerializer.Serialize(data, options)
        File.WriteAllText(filePath, json)
    End Sub

    'Load Game Data takes file path of gamedata and extracts it
    Function LoadGameData(filePath As String) As GameData
        If File.Exists(filePath) Then
            Dim json As String = File.ReadAllText(filePath)
            Return JsonSerializer.Deserialize(Of GameData)(json)
        Else
            Return New GameData()
        End If
    End Function

End Module

