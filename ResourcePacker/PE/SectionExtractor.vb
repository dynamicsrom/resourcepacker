Public Class SectionExtractor

	Public Shared Function GetSection(ByVal PE_sections As ArrayList, ByVal sectionName As String) As Section
		Dim vsize As UInt32 = 0
		Dim rsize As UInt32 = 0
		Dim RawAddr As UInt32 = 0
		Dim vaddr As UInt32 = 0
		For x = 0 To PE_sections.Count - 1
			Dim section1 As Section = PE_sections(x)
			If section1.Name = sectionName Then
				Return section1
			End If
        Next
        Return Nothing
    End Function

End Class
