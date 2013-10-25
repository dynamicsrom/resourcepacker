Public Class e32_unitClass

    Private _rva As UInteger
    Private _size As UInteger

    Public Property rva() As UInteger
        Get
            Return _rva
        End Get
        Set(ByVal value As UInteger)
            _rva = value
        End Set
    End Property

    Public Property size() As UInteger
        Get
            Return _size
        End Get
        Set(ByVal value As UInteger)
            _size = value
        End Set
    End Property

End Class
