Module Main

    Function RoundUp(ByVal val As Integer, ByVal roundval As Integer) As UInt32
        Dim res As Integer = 1
        While val > roundval
            val = val - roundval
            res += 1
        End While
        Return roundval * res
    End Function

    Sub Log(ByVal text As String, ByVal color As ConsoleColor)
        Console.ForegroundColor = color
        Console.WriteLine(text)
    End Sub

    Function ProcessDataEntry(ByVal memoryStream As IO.MemoryStream, ByVal start As UInt32, ByVal addr As UInt32, ByVal vaddr As Int32) As Boolean
        Dim savedPosition As Integer = memoryStream.Position

        If Not addr = 0 Then
            memoryStream.Seek(start + addr, IO.SeekOrigin.Begin)
        End If
        Dim dataEntryBuffer(16) As Byte
        memoryStream.Read(dataEntryBuffer, 0, 16)
        Dim de As DataEntry = DataEntry.FromBytes(dataEntryBuffer)


        memoryStream.Seek(start + addr, IO.SeekOrigin.Begin)

        Dim bw As New IO.BinaryWriter(memoryStream)
        Dim rva As Int32 = de.RVA + vaddr '4096
        bw.Write(rva)

        memoryStream.Seek(savedPosition, IO.SeekOrigin.Begin)
        'bw.Close()
    End Function

    Dim dirCount As Integer = 0
    Function ProcessDataDirectory(ByVal memoryStream As IO.MemoryStream, ByVal start As UInt32, ByVal addr As UInt32, ByVal vaddr As Int32) As Boolean
        dirCount += 1
        Dim savedPosition As Integer = memoryStream.Position

        If Not addr = 0 Then
            memoryStream.Seek(start + addr, IO.SeekOrigin.Begin)
        End If

        Dim datadir(16) As Byte
        memoryStream.Read(datadir, 0, 16)

        Dim ResourceDirectory As ResourceDirectory = ResourceDirectory.FromBytes(datadir)


        For x = 0 To ResourceDirectory.NumberOfNamedEntries - 1
            Dim datadirentry(8) As Byte
            memoryStream.Read(datadirentry, 0, 8)

            Dim current_offset As Integer = memoryStream.Position
            Dim rde As ResourceDirectoryEntry = ResourceDirectoryEntry.FromBytes(datadirentry)
            If rde.isDataDirectory = True Then
                ProcessDataDirectory(memoryStream, start, rde.Offset, vaddr)
                'dirsCount += 1
            Else
                ProcessDataEntry(memoryStream, start, rde.Offset, vaddr)
            End If
            memoryStream.Seek(current_offset, IO.SeekOrigin.Begin)
        Next

        For x = 0 To ResourceDirectory.NumberOfIDEntries - 1
            Dim datadirentry(8) As Byte
            memoryStream.Read(datadirentry, 0, 8)

            Dim current_offset As Integer = memoryStream.Position
            Dim rde As ResourceDirectoryEntry = ResourceDirectoryEntry.FromBytes(datadirentry)
            If rde.isDataDirectory = True Then
                ProcessDataDirectory(memoryStream, start, rde.Offset, vaddr)
                'dirsCount += 1
            Else
                ProcessDataEntry(memoryStream, start, rde.Offset, vaddr)
            End If
            memoryStream.Seek(current_offset, IO.SeekOrigin.Begin)
        Next

        memoryStream.Seek(savedPosition, IO.SeekOrigin.Begin)
    End Function

    Sub ProcessFile(ByVal outputfile As String)
        Try
            Dim fileInfo As New IO.FileInfo(outputfile)
            fileInfo.Attributes = IO.FileAttributes.Normal
            'MessageBox.Show("round 3350 to 500: " + roundup(3350, 500).ToString)
            'Exit Sub
            Dim filename As String = outputfile 'System.AppDomain.CurrentDomain.BaseDirectory + "\test.mui"	'"\resourcetemplate.dll"
            Dim fs As New IO.FileStream(filename, IO.FileMode.Open)

            Dim PE_sections As New ArrayList

            SectionReader.Read(PE_sections, fs)

            Dim bytes() As Byte
            Dim rsrcSection As Section = SectionExtractor.GetSection(PE_sections, ".rsrc")

            Dim cutname As String = outputfile
            If outputfile.Contains("\") Then
                cutname = outputfile.Substring(outputfile.LastIndexOf("\") + 1)
            End If

            If rsrcSection Is Nothing Then
                Log(cutname + ": no rsrc section! skipping", ConsoleColor.Red)
                fs.Close()
                Exit Sub
            End If

            Log("File " + cutname + ": .rsrc found! patching", ConsoleColor.Green)

            Dim sectionSize As UInt32 = RoundUp(rsrcSection.VSize, 512)
            ReDim bytes(sectionSize)
            fs.Seek(rsrcSection.RawAddr, IO.SeekOrigin.Begin)
            fs.Read(bytes, 0, rsrcSection.VSize)
            fs.Close()

            Dim memoryStream As New IO.MemoryStream(bytes)


            ProcessDataDirectory(memoryStream, 0, 0, 4096 - rsrcSection.VAddr)

            Dim resultMemoryStream As New IO.MemoryStream(512 + sectionSize - 1)

            resultMemoryStream.Write(My.Resources.resourcetemplate, 0, 512)
            resultMemoryStream.Write(memoryStream.ToArray, 0, memoryStream.Length)

            memoryStream.Close()

            Dim binaryWriter1 As New IO.BinaryWriter(resultMemoryStream)

            Dim size As UInt32 = 4096 + rsrcSection.VSize
            Dim buffer(4) As Byte
            resultMemoryStream.Seek(60, IO.SeekOrigin.Begin)
            resultMemoryStream.Read(buffer, 0, 4)

            'getting PE header offset
            Dim PEheaderOffset As Integer = BitConverter.ToInt32(buffer, 0)
            binaryWriter1.Seek(PEheaderOffset, IO.SeekOrigin.Begin)
            binaryWriter1.Seek(80, IO.SeekOrigin.Current)
            binaryWriter1.Write(size)

            binaryWriter1.Seek(448, IO.SeekOrigin.Begin)
            binaryWriter1.Write(rsrcSection.VSize)
            binaryWriter1.Seek(456, IO.SeekOrigin.Begin)
            binaryWriter1.Write(sectionSize)


            Dim resultname As String = outputfile
            fs = New IO.FileStream(resultname, IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None)
            fs.Write(resultMemoryStream.ToArray, 0, resultMemoryStream.Length)
            fs.Close()
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Console.ReadKey()
        End Try
    End Sub

    Public Sub Main(ByVal sArgs() As String)
        Log("Resource Packer v1.0", ConsoleColor.Gray)
        Log("(C) UltraShot", ConsoleColor.Gray)

        If sArgs.Count > 0 Then
            Log("Got " + sArgs.Count.ToString + " arguments, trying to parse them", ConsoleColor.White)
            For x = 0 To sArgs.Length - 1
                Dim outputfile As String = sArgs(x)
                If IO.File.Exists(outputfile) = True Then
                    ProcessFile(outputfile)
                End If
            Next
        Else
            Log("[ERROR] No arguments!", ConsoleColor.Red)
            Log("Usage: rpack.exe file1 file2 file3 ...", ConsoleColor.Gray)
            Console.ReadKey()
        End If
    End Sub

End Module
