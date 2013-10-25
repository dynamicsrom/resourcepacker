
Public Class o32_RomClass

    Private _hasCopyEntry As Boolean
    Private _moduleName As String
    Private _o32_dataptr As UInteger
    Private _o32_flags As UInteger
    Private _o32_psize As UInteger
    Private _o32_realaddr As UInteger
    Private _o32_rva As UInteger
    Private _o32_vsize As UInteger
    Private _physOffset As UInteger
    Private _sectionNumber As Integer

    Public Property HasCopyEntry() As Boolean
        Get
            Return _hasCopyEntry
        End Get
        Set(ByVal value As Boolean)
            _hasCopyEntry = value
        End Set
    End Property

    Public Property ModuleName() As String
        Get
            Return _moduleName
        End Get
        Set(ByVal value As String)
            _moduleName = value
        End Set
    End Property

    Public Property o32_dataptr() As UInteger
        Get
            Return _o32_dataptr
        End Get
        Set(ByVal value As UInteger)
            _o32_dataptr = value
        End Set
    End Property

    Public Property o32_flags() As UInteger
        Get
            Return _o32_flags
        End Get
        Set(ByVal value As UInteger)
            _o32_flags = value
        End Set
    End Property

    Public Property o32_psize() As UInteger
        Get
            Return _o32_psize
        End Get
        Set(ByVal value As UInteger)
            _o32_psize = value
        End Set
    End Property

    Public Property o32_realaddr() As UInteger
        Get
            Return _o32_realaddr
        End Get
        Set(ByVal value As UInteger)
            _o32_realaddr = value
        End Set
    End Property

    Public Property o32_rva() As UInteger
        Get
            Return _o32_rva
        End Get
        Set(ByVal value As UInteger)
            _o32_rva = value
        End Set
    End Property

    Public Property o32_vsize() As UInteger
        Get
            Return _o32_vsize
        End Get
        Set(ByVal value As UInteger)
            _o32_vsize = value
        End Set
    End Property

    Public Property physOffset() As UInteger
        Get
            Return _physOffset
        End Get
        Set(ByVal value As UInteger)
            _physOffset = value
        End Set
    End Property

    Public Property SectionNumber() As Integer
        Get
            Return _sectionNumber
        End Get
        Set(ByVal value As Integer)
            _sectionNumber = value
        End Set
    End Property

    Friend Sub New()
        init()
    End Sub

    Friend Sub init()
        _o32_vsize = 0
        _o32_rva = 0
        _o32_psize = 0
        _o32_dataptr = 0
        _o32_realaddr = 0
        _o32_flags = 0
        _moduleName = ""
        _hasCopyEntry = False
        _physOffset = 0
    End Sub

    Friend Sub Write(ByRef binWriter As System.IO.BinaryWriter)
        binWriter.Write(_o32_vsize)
        binWriter.Write(_o32_rva)
        binWriter.Write(_o32_psize)
        binWriter.Write(_o32_dataptr)
        binWriter.Write(_o32_realaddr)
        binWriter.Write(_o32_flags)
    End Sub

    Public Shared Function Read(ByVal binReader As System.IO.BinaryReader, ByVal stOffset As Long, ByVal modName As String) As o32_RomClass
        Dim o32_RomClass2 As o32_RomClass = New o32_RomClass
        binReader.BaseStream.Position = stOffset
        o32_RomClass2._o32_vsize = binReader.ReadUInt32()
        o32_RomClass2._o32_rva = binReader.ReadUInt32()
        o32_RomClass2._o32_psize = binReader.ReadUInt32()
        o32_RomClass2._o32_dataptr = binReader.ReadUInt32()
        o32_RomClass2._o32_realaddr = binReader.ReadUInt32()
        o32_RomClass2._o32_flags = binReader.ReadUInt32()
        o32_RomClass2._moduleName = modName
        Return o32_RomClass2
    End Function

End Class ' class o32_RomClass
