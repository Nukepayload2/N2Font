Imports Newtonsoft.Json
''' <summary>
''' 字体图块。包含一个缓存的纹理。<see cref="GC"/> 可以随时回收纹理。
''' </summary>
Partial Public Class N2FontTile
    ''' <summary>
    ''' 字体的形状信息。应当有256个。
    ''' </summary>
    Public Property Glyphs As N2FontGlyph()
    ''' <summary>
    ''' 字体纹理在字体文件数据部分中的偏移量 (字节)
    ''' </summary>
    Public Property PngOffset As Integer
    ''' <summary>
    ''' 字体纹理的大小 (字节)
    ''' </summary>
    Public Property PngLength As Integer
    ''' <summary>
    ''' 与这个图块对应的字体
    ''' </summary>
    <JsonIgnore>
    Public Property Parent As N2SpriteFont
End Class
