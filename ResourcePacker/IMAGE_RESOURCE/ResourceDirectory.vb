Public Class ResourceDirectory


	Dim dwCharacteristics, dwTimeDateStamp As UInt32
	Dim dwMajorVersion, dwMinorVersion, dwNumberOfNamedEntries, dwNumberOfIDEntries As UInt16

	Public Property Characteristics() As UInt32
		Get
			Return dwCharacteristics
		End Get
		Set(ByVal value As UInt32)
			dwCharacteristics = value
		End Set
	End Property

	Public Property TimeDateStamp() As UInt32
		Get
			Return dwTimeDateStamp
		End Get
		Set(ByVal value As UInt32)
			dwTimeDateStamp = value
		End Set
	End Property

	Public Property MajorVersion() As UInt16
		Get
			Return dwMajorVersion
		End Get
		Set(ByVal value As UInt16)
			dwMajorVersion = value
		End Set
	End Property
	Public Property MinorVersion() As UInt16
		Get
			Return dwMinorVersion
		End Get
		Set(ByVal value As UInt16)
			dwMinorVersion = value
		End Set
	End Property

	Public Property NumberOfNamedEntries() As UInt16
		Get
			Return dwNumberOfNamedEntries
		End Get
		Set(ByVal value As UInt16)
			dwNumberOfNamedEntries = value
		End Set
	End Property

	Public Property NumberOfIDEntries() As UInt16
		Get
			Return dwNumberOfIDEntries
		End Get
		Set(ByVal value As UInt16)
			dwNumberOfIDEntries = value
		End Set
	End Property

	Public Shared Function FromBytes(ByVal b() As Byte) As ResourceDirectory
		Dim resdir As New ResourceDirectory

		resdir.Characteristics = BitConverter.ToUInt32(b, 0)
		resdir.TimeDateStamp = BitConverter.ToUInt32(b, 4)
		resdir.MajorVersion = BitConverter.ToUInt16(b, 8)
		resdir.MinorVersion = BitConverter.ToUInt16(b, 10)
		resdir.NumberOfNamedEntries = BitConverter.ToUInt16(b, 12)
		resdir.NumberOfIDEntries = BitConverter.ToUInt16(b, 14)

		'Form1.ListBox1.Items.Add(resdir.NumberOfIDEntries.ToString + ":" + resdir.NumberOfNamedEntries.ToString)
		Return resdir
	End Function

End Class
