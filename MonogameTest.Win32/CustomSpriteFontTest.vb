Imports Microsoft.Xna.Framework
Imports Microsoft.Xna.Framework.Graphics
Imports Microsoft.Xna.Framework.Input
Imports System.Reflection

''' <summary>
''' This is the main type for your game.
''' </summary>
Public Class CustomSpriteFontTest
    Inherits Game
    Private graphics As GraphicsDeviceManager
    Private spriteBatch As SpriteBatch

    Dim legacySpriteFont As New SegoeWP1632To127BasicLatinColorFF000000
    Dim SegoeWP16 As Texture2D
    Dim textTexture, textTextureClip As RenderTarget2D
    Dim n2SegoeUI As New N2SpriteFont
    Private asm As Assembly

    Public Sub New()
        graphics = New GraphicsDeviceManager(Me)
        Content.RootDirectory = "Content"
        asm = [GetType]().Assembly
    End Sub

    ''' <summary>
    ''' Allows the game to perform any initialization it needs to before starting to run.
    ''' This is where it can query for any required services and load any non-graphic
    ''' related content.  Calling base.Initialize will enumerate through any components
    ''' and initialize them as well.
    ''' </summary>
    Protected Overrides Sub Initialize()
        ' TODO: Add your initialization logic here

        MyBase.Initialize()
    End Sub

    ''' <summary>
    ''' LoadContent will be called once per game and is the place to load
    ''' all of your content.
    ''' </summary>
    Protected Overrides Sub LoadContent()
        SharedGraphicsDevice.Current = GraphicsDevice
        ' Create a new SpriteBatch, which can be used to draw textures.
        spriteBatch = New SpriteBatch(GraphicsDevice)

        ' TODO: use this.Content to load your game content here

        n2SegoeUI.Load(Function() asm.GetManifestResourceStream("MonogameTest.Win32.SegoeUI14.n2fnt"))
        SegoeWP16 = Texture2D.FromStream(GraphicsDevice, asm.GetManifestResourceStream("MonogameTest.Win32.SegoeWP16.png"))

        Dim pp = GraphicsDevice.PresentationParameters
        textTexture = New RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight)
        textTextureClip = New RenderTarget2D(GraphicsDevice, pp.BackBufferWidth, pp.BackBufferHeight)
    End Sub

    ''' <summary>
    ''' UnloadContent will be called once per game and is the place to unload
    ''' game-specific content.
    ''' </summary>
    Protected Overrides Sub UnloadContent()
        ' TODO: Unload any non ContentManager content here
        n2SegoeUI.Dispose()
        SharedGraphicsDevice.Current = Nothing
    End Sub

    ''' <summary>
    ''' Allows the game to run logic such as updating the world,
    ''' checking for collisions, gathering input, and playing audio.
    ''' </summary>
    ''' <param name="gameTime">Provides a snapshot of timing values.</param>
    Protected Overrides Sub Update(gameTime As GameTime)
        If GamePad.GetState(PlayerIndex.One).Buttons.Back = ButtonState.Pressed OrElse Keyboard.GetState().IsKeyDown(Keys.Escape) Then
            [Exit]()
        End If

        ' TODO: Add your update logic here

        MyBase.Update(gameTime)
    End Sub

    Private drawCount As Integer = 0
    Private rnd As New Random()
    ''' <summary>
    ''' This is called when the game should draw itself.
    ''' </summary>
    ''' <param name="gameTime">Provides a snapshot of timing values.</param>
    Protected Overrides Sub Draw(gameTime As GameTime)
        ' TODO: Add your drawing code here
        'DrawTextOnDefaultRenderTarget()
        DrawTextOnCustomRenderTarget()
        MyBase.Draw(gameTime)
    End Sub
    Private Sub DrawTextOnCustomRenderTarget()
        Dim spacing = 19
        Dim mp = Mouse.GetState().Position
        Dim pp = GraphicsDevice.PresentationParameters

        GraphicsDevice.SetRenderTarget(textTexture)
        GraphicsDevice.Clear(Color.Transparent)
        spriteBatch.Begin()
        DrawTemmieText(spacing)
        DrawSinText(spacing)
        spriteBatch.End()

        GraphicsDevice.SetRenderTarget(textTextureClip)
        GraphicsDevice.Clear(Color.Transparent)
        spriteBatch.Begin()
        spriteBatch.Draw(textTexture, New Rectangle(mp.X, mp.Y, pp.BackBufferWidth, pp.BackBufferHeight), Color.Black)
        spriteBatch.End()

        GraphicsDevice.SetRenderTarget(Nothing)
        GraphicsDevice.Clear(Color.White)
        spriteBatch.Begin()
        spriteBatch.Draw(textTextureClip, New Rectangle(mp.X, mp.Y, pp.BackBufferWidth, pp.BackBufferHeight), Color.White)
        spriteBatch.End()
    End Sub
    Private Sub DrawTextOnDefaultRenderTarget()
        GraphicsDevice.SetRenderTarget(Nothing)
        spriteBatch.Begin()

        Dim spacing = 19
        DrawTemmieText(spacing)
        DrawSinText(spacing)

        spriteBatch.End()
    End Sub

    Private Sub DrawTemmieText(spacing As Integer)
        drawCount += 1
        Dim str =
"中国智造,
惠及全球。
123Abcgj+=-_^
hOI!! i'M tEMMIE!
Draw count: " & drawCount
        Dim mp = Mouse.GetState().Position
        Dim i = mp.X
        Dim j = mp.Y

        For Each ch In str
            If ch = vbLf Then
                j += spacing
                i = mp.X
            Else
                Dim charCode = AscW(ch)
                Dim q1 = charCode And &HFF
                Dim q2 = charCode >> 8
                Dim tile = n2SegoeUI.Tiles(q2)
                Dim glyph = tile.Glyphs(q1)
                Dim tex = tile.Texture
                Dim src = New Rectangle(glyph.Left, glyph.Top, glyph.Width, glyph.Height)
                Dim dest = New Rectangle(i + rnd.Next(-1, 2), j + rnd.Next(-1, 2), glyph.Width, glyph.Height)
                spriteBatch.Draw(tex, dest, src, Color.White)
                i += glyph.Width + 1
            End If
        Next
    End Sub

    Private Sub DrawSinText(spacing As Integer)
        Dim leagacyStr = "Nukepayload2UIAbg123Drawing"
        Dim i = 0, j = 0
        For Each ch In leagacyStr
            If ch = vbLf Then
                j += spacing
                i = 0
            Else
                Dim glyph = legacySpriteFont.TryGetGlyph(ch).Value
                Dim src = New Rectangle(glyph.Left, glyph.Top, glyph.Width, glyph.Height)
                Dim dest = New Rectangle(i, j + Math.Sin((drawCount - i / 5) / 2), glyph.Width, glyph.Height)
                spriteBatch.Draw(SegoeWP16, dest, src, Color.White)
                i += glyph.Width
            End If
        Next
    End Sub
End Class
