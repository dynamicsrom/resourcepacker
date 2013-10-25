Public Class e32_RomClass

    Private _e32_entryrva As UInteger
    Private _e32_imageflags As UShort
    Private _e32_objcnt As UShort
    Private _e32_sect14rva As UInteger
    Private _e32_sect14size As UInteger
    Private _e32_stackmax As UInteger
    Private _e32_subsys As UInteger
    Private _e32_subsysmajor As UShort
    Private _e32_subsysminor As UShort
    Private _e32_timestamp As UInteger
    Private _e32_unit As e32_unitClass()
    Private _e32_vbase As UInteger
    Private _e32_vsize As UInteger
    Private _moduleName As String

    Public Property e32_entryrva() As UInteger
        Get

            Return _e32_entryrva
        End Get
        Set(ByVal value As UInteger)
            _e32_entryrva = value
        End Set
    End Property

    Public Property e32_imageflags() As UShort
        Get
            Return _e32_imageflags
        End Get
        Set(ByVal value As UShort)
            _e32_imageflags = value
        End Set
    End Property

    Public Property e32_objcnt() As UShort
        Get
            Return _e32_objcnt
        End Get
        Set(ByVal value As UShort)
            _e32_objcnt = value
        End Set
    End Property

    Public Property e32_sect14rva() As UInteger
        Get
            Return _e32_sect14rva
        End Get
        Set(ByVal value As UInteger)
            _e32_sect14rva = value
        End Set
    End Property

    Public Property e32_sect14size() As UInteger
        Get
            Return _e32_sect14size
        End Get
        Set(ByVal value As UInteger)
            _e32_sect14size = value
        End Set
    End Property

    Public Property e32_stackmax() As UInteger
        Get
            Return _e32_stackmax
        End Get
        Set(ByVal value As UInteger)
            _e32_stackmax = value
        End Set
    End Property

    Public Property e32_subsys() As UInteger
        Get
            Return _e32_subsys
        End Get
        Set(ByVal value As UInteger)
            _e32_subsys = value
        End Set
    End Property

    Public Property e32_subsysmajor() As UShort
        Get
            Return _e32_subsysmajor
        End Get
        Set(ByVal value As UShort)
            _e32_subsysmajor = value
        End Set
    End Property

    Public Property e32_subsysminor() As UShort
        Get
            Return _e32_subsysminor
        End Get
        Set(ByVal value As UShort)
            _e32_subsysminor = value
        End Set
    End Property

    Public Property e32_timestamp() As UInteger
        Get
            Return _e32_timestamp
        End Get
        Set(ByVal value As UInteger)
            _e32_timestamp = value
        End Set
    End Property

    Public Property e32_vbase() As UInteger
        Get
            Return _e32_vbase
        End Get
        Set(ByVal value As UInteger)
            _e32_vbase = value
        End Set
    End Property

    Public Property e32_vsize() As UInteger
        Get
            Return _e32_vsize
        End Get
        Set(ByVal value As UInteger)
            _e32_vsize = value
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

    Public ReadOnly Property Size() As UInteger
        Get
            Return 112
        End Get
    End Property

    Friend Sub New()
        Init()
    End Sub

    Public Function get_e32_unit(ByVal index As Integer) As e32_unitClass

        Return _e32_unit(index)
    End Function

    Friend Sub Init()
        _e32_objcnt = 0
        _e32_imageflags = 0
        _e32_entryrva = 0
        _e32_vbase = 0
        _e32_subsysmajor = 0
        _e32_subsysminor = 0
        _e32_stackmax = 0
        _e32_vsize = 0
        _e32_sect14rva = 0
        _e32_sect14size = 0
        _e32_timestamp = 0
        _e32_unit = Nothing
        _e32_subsys = 0
        _e32_unit = New e32_unitClass(9) {}
        Dim i1 As Integer = 0
        Do
            Dim e32_unitClass1 As e32_unitClass = New e32_unitClass
            _e32_unit(i1) = e32_unitClass1
            i1 = i1 + 1
        Loop While i1 <= 8
        _moduleName = ""
    End Sub

    Public Sub set_e32_unit(ByVal index As Integer, ByVal value As e32_unitClass)
        _e32_unit(index) = value
    End Sub

    Friend Function ToText() As String
        Return ""
    End Function

    Public Sub Write(ByRef binWriter As System.IO.BinaryWriter)
        binWriter.Write(_e32_objcnt)
        binWriter.Write(_e32_imageflags)
        binWriter.Write(_e32_entryrva)
        binWriter.Write(_e32_vbase)
        binWriter.Write(_e32_subsysmajor)
        binWriter.Write(_e32_subsysminor)
        binWriter.Write(_e32_stackmax)
        binWriter.Write(_e32_vsize)
        binWriter.Write(_e32_sect14rva)
        binWriter.Write(_e32_sect14size)
        binWriter.Write(_e32_timestamp)
        Dim i1 As Integer = 0
        Do
            binWriter.Write(_e32_unit(i1).rva)
            binWriter.Write(_e32_unit(i1).size)
            i1 = i1 + 1
        Loop While i1 <= 8
        binWriter.Write(_e32_subsys)
    End Sub

    Public Function IsKernelModule(ByVal border As UInt64)
        Return CULng(_e32_vbase) >= CULng(2147483648)
    End Function

    Public Shared Function Read(ByVal binReader As System.IO.BinaryReader, ByVal stOffset As Long, ByVal modName As String) As e32_RomClass

        Dim e32_RomClass1 As e32_RomClass = New e32_RomClass
        binReader.BaseStream.Position = stOffset
        e32_RomClass1.e32_objcnt = binReader.ReadUInt16()
        e32_RomClass1.e32_imageflags = binReader.ReadUInt16()
        e32_RomClass1.e32_entryrva = binReader.ReadUInt32()
        e32_RomClass1.e32_vbase = binReader.ReadUInt32()
        e32_RomClass1.e32_subsysmajor = binReader.ReadUInt16()
        e32_RomClass1.e32_subsysminor = binReader.ReadUInt16()
        e32_RomClass1.e32_stackmax = binReader.ReadUInt32()
        e32_RomClass1.e32_vsize = binReader.ReadUInt32()
        e32_RomClass1.e32_sect14rva = binReader.ReadUInt32()
        e32_RomClass1.e32_sect14size = binReader.ReadUInt32()
        e32_RomClass1.e32_timestamp = binReader.ReadUInt32()
        Dim i1 As Integer = 0
        Do
            e32_RomClass1.get_e32_unit(i1).rva = binReader.ReadUInt32()
            e32_RomClass1.get_e32_unit(i1).size = binReader.ReadUInt32()
            i1 = i1 + 1
        Loop While i1 <= 8
        e32_RomClass1.e32_subsys = binReader.ReadUInt32()
        e32_RomClass1._moduleName = modName
        Return e32_RomClass1
    End Function

End Class
