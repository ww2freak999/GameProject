Imports System.Drawing

Public Class Form1
    ' GRAPHICS VARIABLES
    Dim G As Graphics
    Dim BBG As Graphics
    Dim BB As Bitmap
    Dim r As Rectangle

    ' FPS COUNTER
    Dim tSec As Integer = TimeOfDay.Second
    Dim tTicks As Integer = 0
    Dim MaxTicks As Integer = 0

    ' GAME RUNNING ?
    Dim IsRunning As Boolean = True

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Show()
        Me.Focus()

        ' INITIALIZE GRAPHICS OBJECTS
        G = Me.CreateGraphics
        BB = New Bitmap(Me.Width, Me.Height)

        StartGameLoop()
    End Sub

    Private Sub StartGameLoop()
        Do While IsRunning = True
            ' KEEP APP RESPONSIVE
            Application.DoEvents()

            '1.) Check user input
            '2.) Run AI
            '3.) Update Object Data (Object Positions, Status, etc.)
            '4.) Check Triggers and Conditions
            '5.) Draw Graphics
            DrawGraphics()
            '6.) Playing Sound Effects & Music

            ' UPDATE TICK COUNTER
            TickCounter()
        Loop
    End Sub

    Private Sub DrawGraphics()
        'FILL THE BACKBUFFER
        'DRAW TILES
        For X = 0 To 19
            For Y = 0 To 14
                r = New Rectangle(X * 32, Y * 32, 32, 32)

                G.FillRectangle(Brushes.BurlyWood, r)
                G.DrawRectangle(Pens.Black, r)
            Next
        Next

        ' DRAW FINAL LAYERS
        'GUYS, MENUS, ETC.
        G.DrawString("Ticks: " & tTicks & vbCrLf & _
                     "TPS: " & MaxTicks, Me.Font, Brushes.Black, 650, 0)

        ' COPY BACKBUFFER TO GRAPHICS OBJECT
        G = Graphics.FromImage(BB)

        ' DRAW BACKBUFFER TO SCREEN
        BBG = Me.CreateGraphics
        BBG.DrawImage(BB, 0, 0, Me.Width, Me.Height)

        ' FIX OVERDRAW
        G.Clear(Color.Wheat)
    End Sub

    Private Sub TickCounter()
        If tSec = TimeOfDay.Second And IsRunning = True Then
            tTicks = tTicks + 1
        Else
            MaxTicks = tTicks
            tTicks = 0
            tSec = TimeOfDay.Second
        End If
    End Sub
End Class
