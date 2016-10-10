Public Module Program
    <STAThread>
    Sub Main()
        Using game = New CustomSpriteFontTest
            game.Run()
        End Using
    End Sub
End Module
