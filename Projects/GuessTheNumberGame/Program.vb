'Program main file, handles game logic
Imports System

Module Program
    Sub Main()
        Dim filePath As String = "gamedata.json"
        Dim data As GameData = LoadGameData(filePath)

        If data.LastGuessCount = 0 AndAlso data.BestScore = 0 Then
            Console.WriteLine("Welcome!")
        Else
            Console.WriteLine("______ STATS ______")
            Console.WriteLine($"Last Number Guessed: {data.LastNumber}")
            Console.WriteLine($"Guesses Last Game: {data.LastGuessCount}")
            Console.WriteLine($"Best Game (Lowest Guesses): {data.BestScore}")
            Console.WriteLine($"Last Played: {data.LastPlayed}")
        End If

        Console.WriteLine(vbCrLf & "Press 'q' to quit, or any other key to start the game...")

        Dim key As ConsoleKeyInfo = Console.ReadKey(intercept:=True)
        Dim keyPressed As Char = Char.ToLower(key.KeyChar)

        If keyPressed = "q"c Then
            Console.WriteLine(vbCrLf & "Quitting the Game!")
            Return
        Else
            Console.WriteLine(vbCrLf & "Starting the Game!")
            Dim newData As GameData = GameLogic.GameStart(data.LastGuessCount)

            Console.WriteLine("Good Game! Saving Data...")
            SaveGameData(newData, filePath)
            Console.Write("Play Again (y/n): ")
            Dim playAgain As ConsoleKeyInfo = Console.ReadKey(intercept:=True)
            Dim playAgainPressed As Char = Char.ToLower(playAgain.KeyChar)

            If playAgainPressed = "y"c Then
                Main()
            Else
                Console.WriteLine("Thanks for playing!")
                Exit Sub
            End If

        End If
    End Sub
End Module

Module GameLogic
    Function GameStart(Optional previousBestScore As Integer = 0) As GameData
        Dim rnd As New Random()
        Dim number As Integer = rnd.Next(1, 101)
        Console.WriteLine("I have thought of a number between 1 and 100. Try to guess it!")

        Dim guesses As Integer = GuessLoop(number)

        Console.WriteLine($"You guessed correctly in {guesses}!")

        Dim newBest As Integer

        If previousBestScore = 0 Or previousBestScore > guesses Then
            newBest = guesses
        Else
            newBest = previousBestScore
        End If

        Return New GameData With {
            .LastNumber = number,
            .LastGuessCount = guesses,
            .BestScore = newBest,
            .LastPlayed = DateOnly.FromDateTime(DateTime.Now)
        }

    End Function

    Function GuessLoop(number As Integer) As Integer
        Dim currentGuesses As Integer = 0

        While True
            Console.Write($"Enter guess {currentGuesses + 1}: ")
            Dim input As String = Console.ReadLine()

            Dim guess As Integer

            If Not Integer.TryParse(input, guess) Or guess > 100 Then
                Console.WriteLine("Invalid input, try again (1-100)!")
                Continue While
            End If

            currentGuesses += 1

            If guess = number Then
                Console.WriteLine("Correct!")
                Exit While
            ElseIf guess < number Then
                Console.WriteLine("Too Low!")
            Else
                Console.WriteLine("Too High!")
            End If
        End While

        Return currentGuesses
    End Function
End Module
