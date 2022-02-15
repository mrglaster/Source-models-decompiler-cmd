Imports System.IO

Public Class AccessedBytesDebugFile

#Region "Creation and Destruction"

	Public Sub New(ByVal outputFileStream As StreamWriter)
		Me.theOutputFileStreamWriter = outputFileStream
	End Sub

#End Region

#Region "Methods"

	Public Sub WriteHeaderComment()
		Dim line As String = ""

		line = "// "
		line += TheApp.GetHeaderComment()
		Me.theOutputFileStreamWriter.WriteLine(line)
	End Sub

	Public Sub WriteFileSeekLog(ByVal aFileSeekLog As FileSeekLog)
		Dim line As String
		line = "====== File Size ======"
		Me.WriteLogLine(0, line)
		line = aFileSeekLog.theFileSize.ToString("N0")
		Me.WriteLogLine(1, line)
		line = "====== File Seek Log ======"
		Me.WriteLogLine(0, line)
		Dim offsetStart As Long
		Dim offsetEnd As Long
		offsetEnd = -1
		For i As Integer = 0 To aFileSeekLog.theFileSeekList.Count - 1
			offsetStart = aFileSeekLog.theFileSeekList.Keys(i)
			offsetEnd = aFileSeekLog.theFileSeekList.Values(i)
			line = offsetStart.ToString("N0") + " - " + offsetEnd.ToString("N0") + " " + aFileSeekLog.theFileSeekDescriptionList.Values(i)
			Me.WriteLogLine(1, line)
		Next

		line = "========================"
		Me.WriteLogLine(0, line)
	End Sub

#End Region

#Region "Private Methods"


	Private Sub WriteLogLine(ByVal indentLevel As Integer, ByVal line As String)
		Dim indentedLine As String = ""
		For i As Integer = 1 To indentLevel
			indentedLine += vbTab
		Next
		indentedLine += line
		Me.theOutputFileStreamWriter.WriteLine(indentedLine)
		Me.theOutputFileStreamWriter.Flush()
	End Sub

#End Region

#Region "Data"

	Private theOutputFileStreamWriter As StreamWriter

#End Region

End Class
