Imports System.Collections.ObjectModel
Imports System.Globalization
Imports System.IO
Imports System.Linq



Public Class App
	Implements IDisposable

#Region "Create and Destroy"

	Public Sub New()
		Me.IsDisposed = False
		Me.theInternalCultureInfo = New CultureInfo("en-US", False)
		Me.theInternalNumberFormat = Me.theInternalCultureInfo.NumberFormat
		Me.theSmdFilesWritten = New List(Of String)()
	End Sub

#Region "IDisposable Support"

	Public Sub Dispose() Implements IDisposable.Dispose
		' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) below.
		Dispose(True)
		GC.SuppressFinalize(Me)
	End Sub

	Protected Overridable Sub Dispose(ByVal disposing As Boolean)
		If Not Me.IsDisposed Then
			If disposing Then
				Me.Free()
			End If
			'NOTE: free shared unmanaged resources
		End If
		Me.IsDisposed = True
	End Sub

#End Region

#End Region

#Region "Init and Free"

	Declare Function AttachConsole Lib "kernel32.dll" (ByVal dwProcessId As Int32) As Boolean

	Public Sub Init()
		Me.theAppPath = Application.StartupPath
		Me.LoadAppSettings()

		Dim documentsPath As String
		documentsPath = Path.Combine(Me.theAppPath, "Documents")
		Dim clArgs() As String = Environment.GetCommandLineArgs()
		Dim path_to_model As String = String.Empty
		Dim output_path As String = String.Empty
		If clArgs.Length() = 1 Then
			Console.WriteLine("Argument amount error!")
			Console.WriteLine("Usage: ""Path\To\Model.mdl"" ""Path\To\Output\Folder""")
			Application.Exit()
		End If

		If clArgs.Length() < 0 Then
			Console.WriteLine("Input args error!")
		End If


		If clArgs.Length() = 2 Then
			path_to_model = clArgs(1)
			If path_to_model.EndsWith(".mdl") And My.Computer.FileSystem.FileExists(path_to_model) Then
				output_path = path_to_model.Substring(0, path_to_model.LastIndexOf("\"))
				Me.theDecompiler = New Decompiler(path_to_model, output_path)
			End If

		End If



		If clArgs.Length() >= 3 Then
			path_to_model = clArgs(1)
			If path_to_model.EndsWith(".mdl") Then
				output_path = clArgs(2)
				If (My.Computer.FileSystem.FileExists(path_to_model)) Then
					Console.WriteLine("Working with model: " + path_to_model)
				Else
					Console.WriteLine("Error! File not found: " + path_to_model)
					Application.Exit()
				End If

				If path_to_model IsNot String.Empty And output_path IsNot String.Empty Then
					Me.theDecompiler = New Decompiler(path_to_model, output_path)
				Else
					Console.WriteLine("Usage: ""Path\To\Model.mdl"" ""Path\To\Output\Folder""")
					Console.WriteLine("Second using way (you'll find result in the model folder) ""Path\To\Model.mdl"" or just use drag-and-drop")
				End If
			End If
		End If


	End Sub

	Private Sub Free()
		If Me.theSettings IsNot Nothing Then
			Me.SaveAppSettings()
		End If
		'If Me.theCompiler IsNot Nothing Then
		'End If
	End Sub

#End Region

#Region "Properties"

	Public ReadOnly Property Settings() As AppSettings
		Get
			Return Me.theSettings
		End Get
	End Property

	Public ReadOnly Property ErrorPathFileName() As String
		Get
			Return Path.Combine(Me.GetCustomDataPath(), Me.ErrorFileName)
		End Get
	End Property

	Public ReadOnly Property InternalCultureInfo() As CultureInfo
		Get
			Return Me.theInternalCultureInfo
		End Get
	End Property

	Public ReadOnly Property InternalNumberFormat() As NumberFormatInfo
		Get
			Return Me.theInternalNumberFormat
		End Get
	End Property

	Public Property SmdFileNames() As List(Of String)
		Get
			Return Me.theSmdFilesWritten
		End Get
		Set(ByVal value As List(Of String))
			Me.theSmdFilesWritten = value
		End Set
	End Property

#End Region

#Region "Methods"

	Public Function GetDebugPath(ByVal outputPath As String, ByVal modelName As String) As String
		'Dim logsPath As String

		'logsPath = Path.Combine(outputPath, modelName + "_" + App.LogsSubFolderName)

		'Return logsPath
		Return outputPath
	End Function

	Public Sub SaveAppSettings()
		Dim appSettingsPath As String
		Dim appSettingsPathFileName As String

		appSettingsPathFileName = Me.GetAppSettingsPathFileName()
		appSettingsPath = FileManager.GetPath(appSettingsPathFileName)

		If FileManager.PathExistsAfterTryToCreate(appSettingsPath) Then
			FileManager.WriteXml(Me.theSettings, appSettingsPathFileName)
		End If
	End Sub



	'TODO: [GetCustomDataPath] Have location option where custom data and settings is saved.
	Public Function GetCustomDataPath() As String
		Dim customDataPath As String
		customDataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ZeqMacaw")
		customDataPath += Path.DirectorySeparatorChar
		'customDataPath += "Crowbar"
		customDataPath += My.Application.Info.ProductName
		customDataPath += " "
		customDataPath += My.Application.Info.Version.ToString(2)

		FileManager.CreatePath(customDataPath)

		Return customDataPath
	End Function

	Public Function GetAppSettingsPathFileName() As String
		Return Path.Combine(Me.GetCustomDataPath(), App.theAppSettingsFileName)
	End Function

#End Region

#Region "Private Methods"

	Private Sub LoadAppSettings()
		Dim appSettingsPathFileName As String
		appSettingsPathFileName = Me.GetAppSettingsPathFileName()

		Dim commandLineOption_Settings_IsEnabled As Boolean = False
		Dim commandLineValues As New ReadOnlyCollection(Of String)(System.Environment.GetCommandLineArgs())
		If commandLineValues.Count > 1 AndAlso commandLineValues(1) <> "" Then
			Dim command As String = commandLineValues(1)
			If command.StartsWith(App.SettingsParameter) Then
				commandLineOption_Settings_IsEnabled = True
				Dim oldAppSettingsPathFileName As String = command.Replace(App.SettingsParameter, "")
				oldAppSettingsPathFileName = oldAppSettingsPathFileName.Replace("""", "")
				If File.Exists(oldAppSettingsPathFileName) Then
					File.Copy(oldAppSettingsPathFileName, appSettingsPathFileName, True)
				End If
			End If
		End If

		If File.Exists(appSettingsPathFileName) Then
			Try
				VersionModule.ConvertSettingsFile(appSettingsPathFileName)
				Me.theSettings = CType(FileManager.ReadXml(GetType(AppSettings), appSettingsPathFileName), AppSettings)
			Catch
				Me.CreateAppSettings()
			End Try
		Else
			' File not found, so init default values.
			Me.CreateAppSettings()
		End If
	End Sub

	Private Sub CreateAppSettings()
		Me.theSettings = New AppSettings()

		Me.SaveAppSettings()
	End Sub

	Public Function GetHeaderComment() As String
		Dim line As String

		line = "Created by "
		line += Me.GetProductNameAndVersion()

		Return line
	End Function

	Public Function GetProductNameAndVersion() As String
		Dim result As String

		result = My.Application.Info.ProductName
		result += " "
		result += My.Application.Info.Version.ToString(2)

		Return result
	End Function


#End Region

#Region "Data"

	Private IsDisposed As Boolean
	Private theInternalCultureInfo As CultureInfo
	Private theInternalNumberFormat As NumberFormatInfo
	Private theSettings As AppSettings
	Public Const SettingsParameter As String = "/settings="
	Private theCommandLineOption_Settings_IsEnabled As Boolean
	Private theAppPath As String
	Private Const theCrowbarLauncherEXEFileName As String = "CrowbarLauncher.exe"
	Public SevenZrExePathFileName As String
	Public CrowbarLauncherExePathFileName As String
	Public LzmaExePathFileName As String
	Private Const PreviewsRelativePath As String = "previews"
	Private Const theAppSettingsFileName As String = "Crowbar Settings.xml"
	Public Const AnimsSubFolderName As String = "anims"
	Public Const LogsSubFolderName As String = "logs"
	Private ErrorFileName As String = "unhandled_exception_error.txt"
	Private theDecompiler As Decompiler
	Private theModelRelativePathFileName As String
	Private theSmdFilesWritten As List(Of String)

#End Region

End Class
