Public Class SectionReader

	Private Shared Function getfirstnull(ByVal b() As Byte) As Integer
		For x = 0 To 8
			If b(x) = Byte.Parse(Val("&H00")) Then
				Return x
			End If
		Next
		Return 8
	End Function

	Public Shared Sub Read(ByRef array As ArrayList, ByVal fs As IO.FileStream)
		Dim buffer(4) As Byte
		fs.Seek(60, IO.SeekOrigin.Begin)
		fs.Read(buffer, 0, 4)

		'getting PE header offset
		Dim PEheaderOffset As Integer = BitConverter.ToInt32(buffer, 0)
		fs.Seek(PEheaderOffset, IO.SeekOrigin.Begin)

		'getting common info about executable file
		Dim header(100) As Byte
		fs.Read(header, 0, 100)

		'getting info about sections
		Dim section_count As Integer = 0
		fs.Seek(PEheaderOffset + 248, IO.SeekOrigin.Begin)
		While True
			ReDim buffer(40)
			fs.Read(buffer, 0, 40)
			If buffer(0) = Byte.Parse(Val("&H2E")) Then

				Dim s As String = System.Text.Encoding.ASCII.GetString(buffer, 0, getfirstnull(buffer))
				If s.Length >= 8 Then
					s = s.Substring(0, 8)
				End If

				Dim sect As New Section
				sect.Name = s
				sect.Flags = BitConverter.ToUInt32(buffer, 36)
				sect.VSize = BitConverter.ToUInt32(buffer, 8)
				sect.VAddr = BitConverter.ToUInt32(buffer, 12)
				sect.RawAddr = BitConverter.ToUInt32(buffer, 20)

				array.Add(sect)
				section_count += 1
			Else
				Exit While
			End If
		End While
	End Sub
End Class
