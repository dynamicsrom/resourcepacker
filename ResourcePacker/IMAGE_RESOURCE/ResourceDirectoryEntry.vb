Public Class ResourceDirectoryEntry


	Dim bNameIsString As Boolean = False
	Dim dwNameOffset As UInt32
	Dim dwName As UInt16

	Dim bDataIsDirectory As Boolean = False
	Dim dwID, dwOffset As UInt32

	Public Property ID() As UInt32
		Get
			Return dwID
		End Get
		Set(ByVal value As UInt32)
			dwID = value
		End Set
	End Property

	Public Property Offset() As UInt32
		Get
			Return dwOffset
		End Get
		Set(ByVal value As UInt32)
			dwOffset = value
		End Set
	End Property

	Public Property isNameString() As Boolean
		Get
			Return bNameIsString
		End Get
		Set(ByVal value As Boolean)
			bNameIsString = value
		End Set
	End Property

	Public Property NameOffset() As UInt32
		Get
			Return dwNameOffset
		End Get
		Set(ByVal value As UInt32)
			dwNameOffset = value
		End Set
	End Property

	Public Property Name() As UInt32
		Get
			Return dwName
		End Get
		Set(ByVal value As UInt32)
			dwName = value
		End Set
	End Property

	Public Property isDataDirectory() As Boolean
		Get
			Return bDataIsDirectory
		End Get
		Set(ByVal value As Boolean)
			bDataIsDirectory = value
		End Set
	End Property

	Public Shared Function FromBytes(ByVal b() As Byte) As ResourceDirectoryEntry
		Dim rde As New ResourceDirectoryEntry
		Dim id As UInt32 = BitConverter.ToUInt32(b, 0)
		Dim offset As UInt32 = BitConverter.ToUInt32(b, 4)

		Dim isDir As UInt32 = offset And 2147483648
		If Not isDir = 0 Then
			rde.bDataIsDirectory = True
			offset -= 2147483648
		Else
			rde.bDataIsDirectory = False
		End If

		rde.Offset = offset
		Return rde
	End Function
End Class