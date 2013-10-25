Public Class DataEntry

	Dim dwRVA, dwSize, dwCodePage, dwReserved As UInt32

	Public Property RVA() As UInt32
		Get
			Return dwRVA
		End Get
		Set(ByVal value As UInt32)
			dwRVA = value
		End Set
	End Property

	Public Property Size() As UInt32
		Get
			Return dwSize
		End Get
		Set(ByVal value As UInt32)
			dwSize = value
		End Set
	End Property

	Public Property CodePage() As UInt32
		Get
			Return dwCodePage
		End Get
		Set(ByVal value As UInt32)
			dwCodePage = value
		End Set
	End Property

	Public Property Reserved() As UInt32
		Get
			Return dwReserved
		End Get
		Set(ByVal value As UInt32)
			dwReserved = value
		End Set
	End Property

	Public Shared Function FromBytes(ByVal b() As Byte)
		Dim de As New DataEntry
		de.RVA = BitConverter.ToUInt32(b, 0)
		de.Size = BitConverter.ToUInt32(b, 4)
		de.dwCodePage = BitConverter.ToUInt32(b, 8)
		de.dwReserved = BitConverter.ToUInt32(b, 12)
		Return de
	End Function
End Class
