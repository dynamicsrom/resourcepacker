Public Class Section

    Private _name As String
    Private _flags As UInt32
    Private _VSize As UInt32
    Private _VAddr As UInt32
    Private _RawAddr As UInt32

    Public Property Name() As String
        Get
            Return _name
        End Get
        Set(ByVal value As String)
            _name = value
        End Set
    End Property

    Public Property Flags() As UInt32
        Get
            Return _flags
        End Get
        Set(ByVal value As UInt32)
            _flags = value
        End Set
    End Property

    Public Property VSize() As UInt32
        Get
            Return _VSize
        End Get
        Set(ByVal value As UInt32)
            _VSize = value
        End Set
    End Property

    Public Property VAddr() As UInt32
        Get
            Return _VAddr
        End Get
        Set(ByVal value As UInt32)
            _VAddr = value
        End Set
    End Property

    Public Property RawAddr() As UInt32
        Get
            Return _RawAddr
        End Get
        Set(ByVal value As UInt32)
            _RawAddr = value
        End Set
    End Property

End Class