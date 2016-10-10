Imports Microsoft.Xna.Framework.Graphics
Imports Newtonsoft.Json
Public Class N2FontTile
    Implements IDisposable

    <JsonIgnore>
    Dim _textureCache As WeakReference(Of Texture2D)
    ''' <summary>
    ''' 从字体纹理缓存中获取字体纹理，或者加载纹理
    ''' </summary>
    Public ReadOnly Property Texture As Texture2D
        Get
            Dim tex As Texture2D = Nothing
            If _textureCache IsNot Nothing Then
                If Not _textureCache.TryGetTarget(tex) Then
                    tex = LoadTexture()
                    _textureCache.SetTarget(tex)
                End If
            Else
                tex = LoadTexture()
                _textureCache = New WeakReference(Of Texture2D)(tex)
            End If
            Return tex
        End Get
    End Property

    ''' <summary>
    ''' 打开字体文件，然后加载纹理
    ''' </summary>
    Protected Overridable Function LoadTexture() As Texture2D
        Using strm = Parent.GetTileData(PngOffset, PngLength)
            Return Texture2D.FromStream(SharedGraphicsDevice.Current, strm)
        End Using
    End Function

#Region "IDisposable Support"
    Private disposedValue As Boolean ' 要检测冗余调用

    ' IDisposable
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposedValue Then
            If disposing Then
                ' TODO: 释放托管状态(托管对象)。
                Dim tex As Texture2D = Nothing
                If (_textureCache?.TryGetTarget(tex)).GetValueOrDefault Then
                    tex.Dispose()
                End If
            End If

            ' TODO: 释放未托管资源(未托管对象)并在以下内容中替代 Finalize()。
            ' TODO: 将大型字段设置为 null。
        End If
        disposedValue = True
    End Sub

    ' Visual Basic 添加此代码以正确实现可释放模式。
    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub
#End Region
End Class
