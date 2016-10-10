Imports Microsoft.Xna.Framework.Graphics

Public Class SharedGraphicsDevice
    Shared _Current As GraphicsDevice
    Public Shared Property Current As GraphicsDevice
        Get
            Return _Current
        End Get
        Friend Set
            _Current = Value
        End Set
    End Property
End Class
