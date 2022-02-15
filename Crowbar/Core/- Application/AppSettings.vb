Imports System.ComponentModel
Imports System.Xml.Serialization

' Purpose: Stores application-related settings, such as UI widget locations and auto-recover data.
Public Class AppSettings
	Implements INotifyPropertyChanged

#Region "Create and Destroy"

	Public Sub New()
		'MyBase.New()

		Me.theAppIsSingleInstance = False
		Me.theWindowLocation = New Point(0, 0)
		Me.theWindowSize = New Size(800, 600)
		Me.theWindowState = FormWindowState.Normal
		'NOTE: 0 means the Set Up Games tab.
		Me.theMainWindowSelectedTabIndex = 0

		Me.thePreviewDataViewerIsRunning = False
		'Me.thePreviewerIsRunning = False
		Me.theDecompilerIsRunning = False
		Me.theCompilerIsRunning = False
		Me.theViewDataViewerIsRunning = False
		'Me.theViewerIsRunning = False
		Me.thePackerIsRunning = False
		Me.theSetUpGamesGameSetupSelectedIndex = 0

		Me.theDownloadItemIdOrLink = ""
		Me.theDownloadOutputFolderOption = DownloadOutputPathOptions.DocumentsFolder
		Me.theDownloadOutputWorkPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.SetDefaultDownloadOptions()

		Me.theUnpackContainerType = ContainerType.VPK
		Me.theUnpackPackagePathFolderOrFileName = ""
		'Me.theUnpackOutputFolderOption = OutputFolderOptions.SubfolderName
		Me.theUnpackOutputFolderOption = UnpackOutputPathOptions.SameFolder
		Me.SetDefaultUnpackOutputSubfolderName()
		Me.theUnpackOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.theUnpackGameSetupSelectedIndex = 0
		Me.theUnpackSearchField = UnpackSearchFieldOptions.Files
		Me.theUnpackSearchText = ""
		Me.SetDefaultUnpackOptions()
		Me.theUnpackMode = InputOptions.File

		Me.thePreviewMdlPathFileName = ""
		Me.thePreviewOverrideMdlVersion = SupportedMdlVersion.DoNotOverride
		Me.thePreviewGameSetupSelectedIndex = 0

		Me.theDecompileMdlPathFileName = ""
		'Me.theDecompileOutputFolderOption = OutputFolderOptions.SubfolderName
		Me.theDecompileOutputFolderOption = DecompileOutputPathOptions.WorkFolder
		Me.SetDefaultDecompileOutputSubfolderName()
		Me.theDecompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.SetDefaultDecompileReCreateFilesOptions()
		Me.theDecompileMode = InputOptions.File
		Me.theDecompileFolderForEachModelIsChecked = False
		Me.theDecompileStricterFormatIsChecked = False
		Me.theDecompileLogFileIsChecked = False
		Me.theDecompileDebugInfoFilesIsChecked = False
		Me.theDecompileOverrideMdlVersion = SupportedMdlVersion.DoNotOverride

		Me.theCompileQcPathFileName = ""
		Me.theCompileOutputFolderIsChecked = True
		'Me.theCompileOutputFolderOption = OutputFolderOptions.SubfolderName
		Me.theCompileOutputFolderOption = CompileOutputPathOptions.GameModelsFolder
		Me.SetDefaultCompileOutputSubfolderName()
		Me.theCompileOutputFullPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.theCompileGameSetupSelectedIndex = 0
		Me.SetDefaultCompileOptions()
		Me.theCompileMode = InputOptions.File

		Me.thePatchMdlPathFileName = ""
		Me.thePatchMode = InputOptions.File

		Me.theViewMdlPathFileName = ""
		Me.theViewOverrideMdlVersion = SupportedMdlVersion.DoNotOverride
		Me.theViewGameSetupSelectedIndex = 0

		Me.thePackInputPathFileName = ""
		Me.thePackOutputFolderOption = PackOutputPathOptions.ParentFolder
		Me.thePackOutputPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.thePackGameSetupSelectedIndex = 0
		Me.SetDefaultPackOptions()

		Me.thePublishGameSelectedIndex = 0

		Me.thePublishSearchField = PublishSearchFieldOptions.ID
		Me.thePublishSearchText = ""
		'Me.thePublishDragDroppedContentPath = ""

		Me.SetDefaultOptionsAutoOpenOptions()
		Me.SetDefaultOptionsDragAndDropOptions()
		Me.SetDefaultOptionsContextMenuOptions()

		Me.theUpdateDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.theUpdateUpdateToNewPathIsChecked = False
		Me.theUpdateUpdateDownloadPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
		Me.theUpdateCopySettingsIsChecked = True

		'Me.Init()
	End Sub

#End Region

#Region "Init and Free"

	'Public Sub Init()
	'End Sub

	'Private Sub Free()
	'End Sub

#End Region

#Region "Properties"

	Public Property AppIsSingleInstance() As Boolean
		Get
			Return theAppIsSingleInstance
		End Get
		Set(ByVal value As Boolean)
			theAppIsSingleInstance = value
			NotifyPropertyChanged("AppIsSingleInstance")
		End Set
	End Property

	Public Property WindowLocation() As Point
		Get
			Return theWindowLocation
		End Get
		Set(ByVal value As Point)
			theWindowLocation = value
		End Set
	End Property

	Public Property WindowSize() As Size
		Get
			Return theWindowSize
		End Get
		Set(ByVal value As Size)
			theWindowSize = value
		End Set
	End Property

	Public Property WindowState() As FormWindowState
		Get
			Return theWindowState
		End Get
		Set(ByVal value As FormWindowState)
			theWindowState = value
		End Set
	End Property

	Public Property MainWindowSelectedTabIndex() As Integer
		Get
			Return Me.theMainWindowSelectedTabIndex
		End Get
		Set(ByVal value As Integer)
			theMainWindowSelectedTabIndex = value
		End Set
	End Property

	Public Property DownloadUseItemIdIsChecked() As Boolean
		Get
			Return Me.theDownloadUseItemIdIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theDownloadUseItemIdIsChecked <> value Then
				Me.theDownloadUseItemIdIsChecked = value
				NotifyPropertyChanged("DownloadUseItemIdIsChecked")
			End If
		End Set
	End Property

	Public Property DownloadPrependItemTitleIsChecked() As Boolean
		Get
			Return Me.theDownloadPrependItemTitleIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theDownloadPrependItemTitleIsChecked <> value Then
				Me.theDownloadPrependItemTitleIsChecked = value
				NotifyPropertyChanged("DownloadPrependItemTitleIsChecked")
			End If
		End Set
	End Property

	Public Property DownloadAppendItemUpdateDateTimeIsChecked() As Boolean
		Get
			Return Me.theDownloadAppendItemUpdateDateTimeIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theDownloadAppendItemUpdateDateTimeIsChecked <> value Then
				Me.theDownloadAppendItemUpdateDateTimeIsChecked = value
				NotifyPropertyChanged("DownloadAppendItemUpdateDateTimeIsChecked")
			End If
		End Set
	End Property
	Public Property DownloadReplaceSpacesWithUnderscoresIsChecked() As Boolean
		Get
			Return Me.theDownloadReplaceSpacesWithUnderscoresIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theDownloadReplaceSpacesWithUnderscoresIsChecked <> value Then
				Me.theDownloadReplaceSpacesWithUnderscoresIsChecked = value
				NotifyPropertyChanged("DownloadReplaceSpacesWithUnderscoresIsChecked")
			End If
		End Set
	End Property

	Public Property DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked() As Boolean
		Get
			Return Me.theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked <> value Then
				Me.theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked = value
				NotifyPropertyChanged("DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked")
			End If
		End Set
	End Property

	Public Property UnpackPackagePathFolderOrFileName() As String
		Get
			Return Me.theUnpackPackagePathFolderOrFileName
		End Get
		Set(ByVal value As String)
			Me.theUnpackPackagePathFolderOrFileName = value
			NotifyPropertyChanged("UnpackPackagePathFolderOrFileName")
		End Set
	End Property

	Public Property UnpackOutputSubfolderName() As String
		Get
			Return Me.theUnpackOutputSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theUnpackOutputSubfolderName = value
			NotifyPropertyChanged("UnpackOutputSubfolderName")
		End Set
	End Property

	Public Property UnpackFolderForEachPackageIsChecked() As Boolean
		Get
			Return Me.theUnpackFolderForEachPackageIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theUnpackFolderForEachPackageIsChecked = value
			NotifyPropertyChanged("UnpackFolderForEachPackageIsChecked")
		End Set
	End Property

	Public Property UnpackKeepFullPathIsChecked() As Boolean
		Get
			Return Me.theUnpackKeepFullPathIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theUnpackKeepFullPathIsChecked = value
			NotifyPropertyChanged("UnpackKeepFullPathIsChecked")
		End Set
	End Property

	Public Property UnpackLogFileIsChecked() As Boolean
		Get
			Return Me.theUnpackLogFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theUnpackLogFileIsChecked = value
			NotifyPropertyChanged("UnpackLogFileIsChecked")
		End Set
	End Property

	Public Property PreviewMdlPathFileName() As String
		Get
			Return Me.thePreviewMdlPathFileName
		End Get
		Set(ByVal value As String)
			Me.thePreviewMdlPathFileName = value
			NotifyPropertyChanged("PreviewMdlPathFileName")
		End Set
	End Property


	Public Property DecompileMdlPathFileName() As String
		Get
			Return Me.theDecompileMdlPathFileName
		End Get
		Set(ByVal value As String)
			Me.theDecompileMdlPathFileName = value
			NotifyPropertyChanged("DecompileMdlPathFileName")
		End Set
	End Property

	Public Property DecompileOutputFolderOption() As DecompileOutputPathOptions
		Get
			Return Me.theDecompileOutputFolderOption
		End Get
		Set(ByVal value As DecompileOutputPathOptions)
			Me.theDecompileOutputFolderOption = value
			NotifyPropertyChanged("DecompileOutputFolderOption")
		End Set
	End Property

	Public Property DecompileOutputSubfolderName() As String
		Get
			Return Me.theDecompileOutputSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theDecompileOutputSubfolderName = value
			NotifyPropertyChanged("DecompileOutputSubfolderName")
		End Set
	End Property

	Public Property DecompileOutputFullPath() As String
		Get
			Return Me.theDecompileOutputFullPath
		End Get
		Set(ByVal value As String)
			Me.theDecompileOutputFullPath = value
			NotifyPropertyChanged("DecompileOutputFullPath")
		End Set
	End Property

	Public Property DecompileQcFileIsChecked() As Boolean
		Get
			Return Me.theDecompileQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcFileIsChecked = value
			NotifyPropertyChanged("DecompileQcFileIsChecked")
		End Set
	End Property
	Public Property DecompileGroupIntoQciFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileGroupIntoQciFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileGroupIntoQciFilesIsChecked = value
			NotifyPropertyChanged("DecompileGroupIntoQciFilesIsChecked")
		End Set
	End Property

	Public Property DecompileQcSkinFamilyOnSingleLineIsChecked() As Boolean
		Get
			Return Me.theDecompileQcSkinFamilyOnSingleLineIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcSkinFamilyOnSingleLineIsChecked = value
			NotifyPropertyChanged("DecompileQcSkinFamilyOnSingleLineIsChecked")
		End Set
	End Property

	Public Property DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked() As Boolean
		Get
			Return Me.theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked = value
			NotifyPropertyChanged("DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked")
		End Set
	End Property

	Public Property DecompileQcIncludeDefineBoneLinesIsChecked() As Boolean
		Get
			Return Me.theDecompileQcIncludeDefineBoneLinesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcIncludeDefineBoneLinesIsChecked = value
			NotifyPropertyChanged("DecompileQcIncludeDefineBoneLinesIsChecked")
		End Set
	End Property

	Public Property DecompileQcUseMixedCaseForKeywordsIsChecked() As Boolean
		Get
			Return Me.theDecompileQcUseMixedCaseForKeywordsIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileQcUseMixedCaseForKeywordsIsChecked = value
			NotifyPropertyChanged("DecompileQcUseMixedCaseForKeywordsIsChecked")
		End Set
	End Property

	Public Property DecompileReferenceMeshSmdFileIsChecked() As Boolean
		Get
			Return Me.theDecompileReferenceMeshSmdFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileReferenceMeshSmdFileIsChecked = value
			NotifyPropertyChanged("DecompileReferenceMeshSmdFileIsChecked")
		End Set
	End Property

	Public Property DecompileBoneAnimationSmdFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileBoneAnimationSmdFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileBoneAnimationSmdFilesIsChecked = value
			NotifyPropertyChanged("DecompileBoneAnimationSmdFilesIsChecked")
		End Set
	End Property

	Public Property DecompileBoneAnimationPlaceInSubfolderIsChecked() As Boolean
		Get
			Return Me.theDecompileBoneAnimationPlaceInSubfolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileBoneAnimationPlaceInSubfolderIsChecked = value
			NotifyPropertyChanged("DecompileBoneAnimationPlaceInSubfolderIsChecked")
		End Set
	End Property

	Public Property DecompileTextureBmpFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileTextureBmpFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileTextureBmpFileIsChecked = value
			NotifyPropertyChanged("DecompileTextureBmpFileIsChecked")
		End Set
	End Property

	Public Property DecompileLodMeshSmdFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileLodMeshSmdFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileLodMeshSmdFilesIsChecked = value
			NotifyPropertyChanged("DecompileLodMeshSmdFilesIsChecked")
		End Set
	End Property

	Public Property DecompilePhysicsMeshSmdFileIsChecked() As Boolean
		Get
			Return Me.theDecompilePhysicsMeshSmdFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompilePhysicsMeshSmdFileIsChecked = value
			NotifyPropertyChanged("DecompilePhysicsMeshSmdFileIsChecked")
		End Set
	End Property

	Public Property DecompileVertexAnimationVtaFileIsChecked() As Boolean
		Get
			Return Me.theDecompileVertexAnimationVtaFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileVertexAnimationVtaFileIsChecked = value
			NotifyPropertyChanged("DecompileVertexAnimationVtaFileIsChecked")
		End Set
	End Property

	Public Property DecompileProceduralBonesVrdFileIsChecked() As Boolean
		Get
			Return Me.theDecompileProceduralBonesVrdFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileProceduralBonesVrdFileIsChecked = value
			NotifyPropertyChanged("DecompileProceduralBonesVrdFileIsChecked")
		End Set
	End Property

	Public Property DecompileFolderForEachModelIsChecked() As Boolean
		Get
			Return Me.theDecompileFolderForEachModelIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileFolderForEachModelIsChecked = value
			NotifyPropertyChanged("DecompileFolderForEachModelIsChecked")
		End Set
	End Property

	Public Property DecompilePrefixFileNamesWithModelNameIsChecked() As Boolean
		Get
			Return Me.theDecompilePrefixFileNamesWithModelNameIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompilePrefixFileNamesWithModelNameIsChecked = value
			NotifyPropertyChanged("DecompilePrefixFileNamesWithModelNameIsChecked")
		End Set
	End Property

	Public Property DecompileStricterFormatIsChecked() As Boolean
		Get
			Return Me.theDecompileStricterFormatIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileStricterFormatIsChecked = value
			NotifyPropertyChanged("DecompileStricterFormatIsChecked")
		End Set
	End Property

	Public Property DecompileLogFileIsChecked() As Boolean
		Get
			Return Me.theDecompileLogFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileLogFileIsChecked = value
			NotifyPropertyChanged("DecompileLogFileIsChecked")
		End Set
	End Property

	Public Property DecompileDebugInfoFilesIsChecked() As Boolean
		Get
			Return Me.theDecompileDebugInfoFilesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileDebugInfoFilesIsChecked = value
			NotifyPropertyChanged("DecompileDebugInfoFilesIsChecked")
		End Set
	End Property

	Public Property DecompileDeclareSequenceQciFileIsChecked() As Boolean
		Get
			Return Me.theDecompileDeclareSequenceQciFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileDeclareSequenceQciFileIsChecked = value
			NotifyPropertyChanged("DecompileDeclareSequenceQciFileIsChecked")
		End Set
	End Property

	Public Property DecompileRemovePathFromSmdMaterialFileNamesIsChecked() As Boolean
		Get
			Return Me.theDecompileRemovePathFromSmdMaterialFileNamesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileRemovePathFromSmdMaterialFileNamesIsChecked = value
			NotifyPropertyChanged("DecompileRemovePathFromSmdMaterialFileNamesIsChecked")
		End Set
	End Property

	Public Property DecompileUseNonValveUvConversionIsChecked() As Boolean
		Get
			Return Me.theDecompileUseNonValveUvConversionIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompileUseNonValveUvConversionIsChecked = value
			NotifyPropertyChanged("DecompileUseNonValveUvConversionIsChecked")
		End Set
	End Property

	Public Property DecompileOverrideMdlVersion() As SupportedMdlVersion
		Get
			Return Me.theDecompileOverrideMdlVersion
		End Get
		Set(ByVal value As SupportedMdlVersion)
			Me.theDecompileOverrideMdlVersion = value
			NotifyPropertyChanged("DecompileOverrideMdlVersion")
		End Set
	End Property

	Public Property DecompileMode() As InputOptions
		Get
			Return Me.theDecompileMode
		End Get
		Set(ByVal value As InputOptions)
			Me.theDecompileMode = value
			NotifyPropertyChanged("DecompileMode")
		End Set
	End Property

	<XmlIgnore()>
	Public Property DecompilerIsRunning() As Boolean
		Get
			Return Me.theDecompilerIsRunning
		End Get
		Set(ByVal value As Boolean)
			Me.theDecompilerIsRunning = value
			NotifyPropertyChanged("DecompilerIsRunning")
		End Set
	End Property

	Public Property CompileQcPathFileName() As String
		Get
			Return Me.theCompileQcPathFileName
		End Get
		Set(ByVal value As String)
			Me.theCompileQcPathFileName = value
			NotifyPropertyChanged("CompileQcPathFileName")
		End Set
	End Property

	Public Property CompileOutputSubfolderName() As String
		Get
			Return Me.theCompileOutputSubfolderName
		End Get
		Set(ByVal value As String)
			Me.theCompileOutputSubfolderName = value
			NotifyPropertyChanged("CompileOutputSubfolderName")
		End Set
	End Property

	Public Property CompileOutputFullPath() As String
		Get
			Return Me.theCompileOutputFullPath
		End Get
		Set(ByVal value As String)
			Me.theCompileOutputFullPath = value
			NotifyPropertyChanged("CompileOutputFullPath")
		End Set
	End Property

	Public Property CompileGoldSourceLogFileIsChecked() As Boolean
		Get
			Return Me.theCompileGoldSourceLogFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileGoldSourceLogFileIsChecked = value
			NotifyPropertyChanged("CompileGoldSourceLogFileIsChecked")
		End Set
	End Property

	Public Property CompileSourceLogFileIsChecked() As Boolean
		Get
			Return Me.theCompileSourceLogFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileSourceLogFileIsChecked = value
			NotifyPropertyChanged("CompileSourceLogFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesCreateFileIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesCreateFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesCreateFileIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesCreateFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesQciFileName() As String
		Get
			Return Me.theCompileOptionDefineBonesQciFileName
		End Get
		Set(ByVal value As String)
			Me.theCompileOptionDefineBonesQciFileName = value
			NotifyPropertyChanged("CompileOptionDefineBonesQciFileName")
		End Set
	End Property

	Public Property CompileOptionDefineBonesOverwriteQciFileIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesOverwriteQciFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesOverwriteQciFileIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesOverwriteQciFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionDefineBonesModifyQcFileIsChecked() As Boolean
		Get
			Return Me.theCompileOptionDefineBonesModifyQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionDefineBonesModifyQcFileIsChecked = value
			NotifyPropertyChanged("CompileOptionDefineBonesModifyQcFileIsChecked")
		End Set
	End Property

	Public Property CompileOptionNoP4IsChecked() As Boolean
		Get
			Return Me.theCompileOptionNoP4IsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionNoP4IsChecked = value
			NotifyPropertyChanged("CompileOptionNoP4IsChecked")
		End Set
	End Property

	Public Property CompileOptionVerboseIsChecked() As Boolean
		Get
			Return Me.theCompileOptionVerboseIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theCompileOptionVerboseIsChecked = value
			NotifyPropertyChanged("CompileOptionVerboseIsChecked")
		End Set
	End Property

	<XmlIgnore()>
	Public Property CompileOptionsText() As String
		Get
			Return Me.theCompileOptionsText
		End Get
		Set(ByVal value As String)
			Me.theCompileOptionsText = value
			NotifyPropertyChanged("CompileOptionsText")
		End Set
	End Property

	Public Property CompileMode() As InputOptions
		Get
			Return Me.theCompileMode
		End Get
		Set(ByVal value As InputOptions)
			Me.theCompileMode = value
			NotifyPropertyChanged("CompileMode")
		End Set
	End Property

	<XmlIgnore()>
	Public Property CompilerIsRunning() As Boolean
		Get
			Return Me.theCompilerIsRunning
		End Get
		Set(ByVal value As Boolean)
			Me.theCompilerIsRunning = value
			NotifyPropertyChanged("CompilerIsRunning")
		End Set
	End Property



	Public Property ViewMdlPathFileName() As String
		Get
			Return Me.theViewMdlPathFileName
		End Get
		Set(ByVal value As String)
			Me.theViewMdlPathFileName = value
			NotifyPropertyChanged("ViewMdlPathFileName")
		End Set
	End Property

	Public Property PackInputPath() As String
		Get
			Return Me.thePackInputPathFileName
		End Get
		Set(ByVal value As String)
			Me.thePackInputPathFileName = value
			NotifyPropertyChanged("PackInputPath")
		End Set
	End Property




	Public Property PackLogFileIsChecked() As Boolean
		Get
			Return Me.thePackLogFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.thePackLogFileIsChecked = value
			NotifyPropertyChanged("PackLogFileIsChecked")
		End Set
	End Property

	Public Property PackOptionMultiFileVpkIsChecked() As Boolean
		Get
			Return Me.thePackOptionMultiFileVpkIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.thePackOptionMultiFileVpkIsChecked = value
			NotifyPropertyChanged("PackOptionMultiFileVpkIsChecked")
		End Set
	End Property

	Public Property PackGmaTitle() As String
		Get
			Return Me.thePackGmaTitle
		End Get
		Set(ByVal value As String)
			Me.thePackGmaTitle = value
			NotifyPropertyChanged("PackGmaTitle")
		End Set
	End Property

	Public Property PackGmaItemTags() As BindingListEx(Of String)
		Get
			Return Me.thePackGmaItemTags
		End Get
		Set(ByVal value As BindingListEx(Of String))
			Me.thePackGmaItemTags = value
			NotifyPropertyChanged("PackGmaItemTags")
		End Set
	End Property

	<XmlIgnore()>
	Public Property PackOptionsText() As String
		Get
			Return Me.thePackOptionsText
		End Get
		Set(ByVal value As String)
			Me.thePackOptionsText = value
			NotifyPropertyChanged("PackOptionsText")
		End Set
	End Property


	Public Property OptionsAutoOpenVpkFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenVpkFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenVpkFileIsChecked <> value Then
				Me.theOptionsAutoOpenVpkFileIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenVpkFileIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenVpkFileOption() As ActionType
		Get
			Return Me.theOptionsAutoOpenVpkFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsAutoOpenVpkFileOption = value
			NotifyPropertyChanged("OptionsAutoOpenVpkFileOption")
		End Set
	End Property

	Public Property OptionsAutoOpenGmaFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenGmaFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenGmaFileIsChecked <> value Then
				Me.theOptionsAutoOpenGmaFileIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenGmaFileIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenGmaFileOption() As ActionType
		Get
			Return Me.theOptionsAutoOpenGmaFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsAutoOpenGmaFileOption = value
			NotifyPropertyChanged("OptionsAutoOpenGmaFileOption")
		End Set
	End Property

	Public Property OptionsAutoOpenFpxFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenFpxFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenFpxFileIsChecked <> value Then
				Me.theOptionsAutoOpenFpxFileIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenFpxFileIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenMdlFileIsChecked <> value Then
				Me.theOptionsAutoOpenMdlFileIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenMdlFileIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileForPreviewIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenMdlFileForPreviewIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenMdlFileForPreviewIsChecked <> value Then
				Me.theOptionsAutoOpenMdlFileForPreviewIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenMdlFileForPreviewIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileForDecompileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenMdlFileForDecompileIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenMdlFileForDecompileIsChecked <> value Then
				Me.theOptionsAutoOpenMdlFileForDecompileIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenMdlFileForDecompileIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileForViewIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenMdlFileForViewIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsAutoOpenMdlFileForViewIsChecked <> value Then
				Me.theOptionsAutoOpenMdlFileForViewIsChecked = value
				NotifyPropertyChanged("OptionsAutoOpenMdlFileForViewIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsAutoOpenMdlFileOption() As ActionType
		Get
			Return Me.theOptionsAutoOpenMdlFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsAutoOpenMdlFileOption = value
			NotifyPropertyChanged("OptionsAutoOpenMdlFileOption")
		End Set
	End Property

	Public Property OptionsAutoOpenQcFileIsChecked() As Boolean
		Get
			Return Me.theOptionsAutoOpenQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsAutoOpenQcFileIsChecked = value
			NotifyPropertyChanged("OptionsAutoOpenQcFileIsChecked")
		End Set
	End Property

	Public Property OptionsAutoOpenFolderOption() As ActionType
		Get
			Return Me.theOptionsAutoOpenFolderOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsAutoOpenFolderOption = value
			NotifyPropertyChanged("OptionsAutoOpenFolderOption")
		End Set
	End Property

	Public Property OptionsDragAndDropVpkFileOption() As ActionType
		Get
			Return Me.theOptionsDragAndDropVpkFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsDragAndDropVpkFileOption = value
			NotifyPropertyChanged("OptionsDragAndDropVpkFileOption")
		End Set
	End Property

	Public Property OptionsDragAndDropGmaFileOption() As ActionType
		Get
			Return Me.theOptionsDragAndDropGmaFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsDragAndDropGmaFileOption = value
			NotifyPropertyChanged("OptionsDragAndDropGmaFileOption")
		End Set
	End Property

	Public Property OptionsDragAndDropMdlFileForPreviewIsChecked() As Boolean
		Get
			Return Me.theOptionsDragAndDropMdlFileForPreviewIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsDragAndDropMdlFileForPreviewIsChecked <> value Then
				Me.theOptionsDragAndDropMdlFileForPreviewIsChecked = value
				NotifyPropertyChanged("OptionsDragAndDropMdlFileForPreviewIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsDragAndDropMdlFileForDecompileIsChecked() As Boolean
		Get
			Return Me.theOptionsDragAndDropMdlFileForDecompileIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsDragAndDropMdlFileForDecompileIsChecked <> value Then
				Me.theOptionsDragAndDropMdlFileForDecompileIsChecked = value
				NotifyPropertyChanged("OptionsDragAndDropMdlFileForDecompileIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsDragAndDropMdlFileForViewIsChecked() As Boolean
		Get
			Return Me.theOptionsDragAndDropMdlFileForViewIsChecked
		End Get
		Set(ByVal value As Boolean)
			If Me.theOptionsDragAndDropMdlFileForViewIsChecked <> value Then
				Me.theOptionsDragAndDropMdlFileForViewIsChecked = value
				NotifyPropertyChanged("OptionsDragAndDropMdlFileForViewIsChecked")
			End If
		End Set
	End Property

	Public Property OptionsDragAndDropMdlFileOption() As ActionType
		Get
			Return Me.theOptionsDragAndDropMdlFileOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsDragAndDropMdlFileOption = value
			NotifyPropertyChanged("OptionsDragAndDropMdlFileOption")
		End Set
	End Property

	Public Property OptionsDragAndDropFolderOption() As ActionType
		Get
			Return Me.theOptionsDragAndDropFolderOption
		End Get
		Set(ByVal value As ActionType)
			Me.theOptionsDragAndDropFolderOption = value
			NotifyPropertyChanged("OptionsDragAndDropFolderOption")
		End Set
	End Property

	Public Property OptionsContextMenuIntegrateMenuItemsIsChecked() As Boolean
		Get
			Return Me.theOptionsContextMenuIntegrateMenuItemsIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsContextMenuIntegrateMenuItemsIsChecked = value
			NotifyPropertyChanged("OptionsContextMenuIntegrateMenuItemsIsChecked")
		End Set
	End Property

	Public Property OptionsContextMenuIntegrateSubMenuIsChecked() As Boolean
		Get
			Return Me.theOptionsContextMenuIntegrateSubMenuIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsContextMenuIntegrateSubMenuIsChecked = value
			NotifyPropertyChanged("OptionsContextMenuIntegrateSubMenuIsChecked")
		End Set
	End Property

	Public Property OptionsOpenWithCrowbarIsChecked() As Boolean
		Get
			Return Me.theOptionsOpenWithCrowbarIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsOpenWithCrowbarIsChecked = value
			NotifyPropertyChanged("OptionsOpenWithCrowbarIsChecked")
		End Set
	End Property

	Public Property OptionsViewMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsViewMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsViewMdlFileIsChecked = value
			NotifyPropertyChanged("OptionsViewMdlFileIsChecked")
		End Set
	End Property

	Public Property OptionsDecompileMdlFileIsChecked() As Boolean
		Get
			Return Me.theOptionsDecompileMdlFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDecompileMdlFileIsChecked = value
			NotifyPropertyChanged("OptionsDecompileMdlFileIsChecked")
		End Set
	End Property

	Public Property OptionsDecompileFolderIsChecked() As Boolean
		Get
			Return Me.theOptionsDecompileFolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDecompileFolderIsChecked = value
			NotifyPropertyChanged("OptionsDecompileFolderIsChecked")
		End Set
	End Property

	Public Property OptionsDecompileFolderAndSubfoldersIsChecked() As Boolean
		Get
			Return Me.theOptionsDecompileFolderAndSubfoldersIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsDecompileFolderAndSubfoldersIsChecked = value
			NotifyPropertyChanged("OptionsDecompileFolderAndSubfoldersIsChecked")
		End Set
	End Property

	Public Property OptionsCompileQcFileIsChecked() As Boolean
		Get
			Return Me.theOptionsCompileQcFileIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsCompileQcFileIsChecked = value
			NotifyPropertyChanged("OptionsCompileQcFileIsChecked")
		End Set
	End Property

	Public Property OptionsCompileFolderIsChecked() As Boolean
		Get
			Return Me.theOptionsCompileFolderIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsCompileFolderIsChecked = value
			NotifyPropertyChanged("OptionsCompileFolderIsChecked")
		End Set
	End Property

	Public Property OptionsCompileFolderAndSubfoldersIsChecked() As Boolean
		Get
			Return Me.theOptionsCompileFolderAndSubfoldersIsChecked
		End Get
		Set(ByVal value As Boolean)
			Me.theOptionsCompileFolderAndSubfoldersIsChecked = value
			NotifyPropertyChanged("OptionsCompileFolderAndSubfoldersIsChecked")
		End Set
	End Property




#End Region

#Region "Core Event Handlers"

#End Region

#Region "Methods"

	Public Sub SetDefaultDownloadOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.DownloadUseItemIdIsChecked = True
		Me.DownloadPrependItemTitleIsChecked = True
		Me.DownloadAppendItemUpdateDateTimeIsChecked = True
		Me.DownloadReplaceSpacesWithUnderscoresIsChecked = True
		Me.DownloadConvertToExpectedFileOrFolderCheckBoxIsChecked = True
	End Sub

	Public Sub SetDefaultUnpackOutputSubfolderName()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.UnpackOutputSubfolderName = "unpacked " + My.Application.Info.Version.ToString(2)
	End Sub

	Public Sub SetDefaultUnpackOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.UnpackFolderForEachPackageIsChecked = False
		Me.UnpackKeepFullPathIsChecked = False
		Me.UnpackLogFileIsChecked = False
	End Sub

	Public Sub SetDefaultDecompileOutputSubfolderName()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.DecompileOutputSubfolderName = "decompiled " + My.Application.Info.Version.ToString(2)
	End Sub

	Public Sub SetDefaultDecompileReCreateFilesOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.DecompileQcFileIsChecked = True
		Me.DecompileGroupIntoQciFilesIsChecked = False
		Me.DecompileQcSkinFamilyOnSingleLineIsChecked = True
		Me.DecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked = True
		Me.DecompileQcIncludeDefineBoneLinesIsChecked = True
		Me.DecompileQcUseMixedCaseForKeywordsIsChecked = False

		Me.DecompileReferenceMeshSmdFileIsChecked = True
		Me.DecompileRemovePathFromSmdMaterialFileNamesIsChecked = True
		Me.DecompileUseNonValveUvConversionIsChecked = False

		Me.DecompileBoneAnimationSmdFilesIsChecked = True
		Me.DecompileBoneAnimationPlaceInSubfolderIsChecked = True

		Me.DecompileTextureBmpFilesIsChecked = True
		Me.DecompileLodMeshSmdFilesIsChecked = True
		Me.DecompilePhysicsMeshSmdFileIsChecked = True
		Me.DecompileVertexAnimationVtaFileIsChecked = True
		Me.DecompileProceduralBonesVrdFileIsChecked = True
	End Sub

	Public Sub SetDefaultCompileOutputSubfolderName()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.CompileOutputSubfolderName = "compiled " + My.Application.Info.Version.ToString(2)
	End Sub

	Public Sub SetDefaultCompileOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.CompileGoldSourceLogFileIsChecked = False
		Me.CompileSourceLogFileIsChecked = False

		Me.CompileOptionNoP4IsChecked = True
		Me.CompileOptionVerboseIsChecked = True

		Me.CompileOptionDefineBonesIsChecked = False
		Me.CompileOptionDefineBonesCreateFileIsChecked = False
		Me.CompileOptionDefineBonesQciFileName = "DefineBones"
		Me.CompileOptionDefineBonesOverwriteQciFileIsChecked = False
		Me.CompileOptionDefineBonesModifyQcFileIsChecked = False

		Me.CompileOptionsText = ""
	End Sub

	Public Sub SetDefaultPackOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.PackLogFileIsChecked = False

		Me.PackOptionMultiFileVpkIsChecked = False

		Me.PackGmaTitle = ""
		Me.PackGmaItemTags = New BindingListEx(Of String)()

		Me.PackOptionsText = ""
	End Sub

	Public Sub SetDefaultOptionsAutoOpenOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.OptionsAutoOpenVpkFileIsChecked = True
		Me.OptionsAutoOpenVpkFileOption = ActionType.Unpack
		Me.OptionsAutoOpenGmaFileIsChecked = True
		Me.OptionsAutoOpenGmaFileOption = ActionType.Unpack
		Me.OptionsAutoOpenFpxFileIsChecked = True

		Me.OptionsAutoOpenMdlFileIsChecked = True
		Me.OptionsAutoOpenMdlFileForPreviewIsChecked = False
		Me.OptionsAutoOpenMdlFileForDecompileIsChecked = True
		Me.OptionsAutoOpenMdlFileForViewIsChecked = False
		Me.OptionsAutoOpenMdlFileOption = ActionType.Decompile

		Me.OptionsAutoOpenQcFileIsChecked = True

		Me.OptionsAutoOpenFolderOption = ActionType.Decompile
	End Sub

	Public Sub SetDefaultOptionsDragAndDropOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.

		Me.OptionsDragAndDropVpkFileOption = ActionType.Unpack
		Me.OptionsDragAndDropGmaFileOption = ActionType.Unpack

		Me.OptionsDragAndDropMdlFileForPreviewIsChecked = False
		Me.OptionsDragAndDropMdlFileForDecompileIsChecked = True
		Me.OptionsDragAndDropMdlFileForViewIsChecked = False
		Me.OptionsDragAndDropMdlFileOption = ActionType.Decompile

		Me.OptionsDragAndDropFolderOption = ActionType.Decompile
	End Sub

	Public Sub SetDefaultOptionsContextMenuOptions()
		'NOTE: Call the properties so the NotifyPropertyChanged events are raised.
		Me.OptionsContextMenuIntegrateMenuItemsIsChecked = True
		Me.OptionsContextMenuIntegrateSubMenuIsChecked = True

		Me.OptionsOpenWithCrowbarIsChecked = True
		Me.OptionsViewMdlFileIsChecked = True

		Me.OptionsDecompileMdlFileIsChecked = True
		Me.OptionsDecompileFolderIsChecked = True
		Me.OptionsDecompileFolderAndSubfoldersIsChecked = True

		Me.OptionsCompileQcFileIsChecked = True
		Me.OptionsCompileFolderIsChecked = True
		Me.OptionsCompileFolderAndSubfoldersIsChecked = True
	End Sub

#End Region

#Region "Events"

	Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

#End Region

#Region "Private Methods"

	Protected Sub NotifyPropertyChanged(ByVal info As String)
		RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(info))
	End Sub

#End Region

#Region "Data"

	' General
	Private theAppIsSingleInstance As Boolean
	Private theWindowLocation As Point
	Private theWindowSize As Size
	Private theWindowState As FormWindowState
	Private theAboutTabBackgroundColor As Color
	Private theMainWindowSelectedTabIndex As Integer

	' Set Up Games tab

	Private theSetUpGamesGameSetupSelectedIndex As Integer

	' Download tab

	Private theDownloadItemIdOrLink As String
	Private theDownloadOutputFolderOption As DownloadOutputPathOptions
	Private theDownloadOutputWorkPath As String
	Private theDownloadUseItemIdIsChecked As Boolean
	Private theDownloadPrependItemTitleIsChecked As Boolean
	Private theDownloadAppendItemUpdateDateTimeIsChecked As Boolean
	Private theDownloadReplaceSpacesWithUnderscoresIsChecked As Boolean
	Private theDownloadConvertToExpectedFileOrFolderCheckBoxIsChecked As Boolean

	' Unpack tab

	Private theUnpackContainerType As ContainerType
	Private theUnpackPackagePathFolderOrFileName As String
	'Private theUnpackOutputFolderOption As OutputFolderOptions
	Private theUnpackOutputFolderOption As UnpackOutputPathOptions
	Private theUnpackOutputSamePath As String
	Private theUnpackOutputSubfolderName As String
	Private theUnpackOutputFullPath As String
	Private theUnpackPackagePathFileName As String
	Private theUnpackGameSetupSelectedIndex As Integer
	Private theUnpackSearchField As UnpackSearchFieldOptions
	Private theUnpackSearchText As String

	Private theUnpackFolderForEachPackageIsChecked As Boolean
	Private theUnpackKeepFullPathIsChecked As Boolean
	Private theUnpackLogFileIsChecked As Boolean

	Private theUnpackMode As InputOptions
	Private theUnpackerIsRunning As Boolean

	' Preview tab

	Private thePreviewMdlPathFileName As String
	Private thePreviewOverrideMdlVersion As SupportedMdlVersion
	Private thePreviewGameSetupSelectedIndex As Integer

	Private thePreviewDataViewerIsRunning As Boolean
	Private thePreviewViewerIsRunning As Boolean

	' Decompile tab

	Private theDecompileMdlPathFileName As String
	'Private theDecompileOutputFolderOption As OutputFolderOptions
	Private theDecompileOutputFolderOption As DecompileOutputPathOptions
	Private theDecompileOutputSubfolderName As String
	Private theDecompileOutputFullPath As String
	'Private theDecompileOutputPathName As String

	Private theDecompileQcFileIsChecked As Boolean
	Private theDecompileGroupIntoQciFilesIsChecked As Boolean
	Private theDecompileQcSkinFamilyOnSingleLineIsChecked As Boolean
	Private theDecompileQcOnlyChangedMaterialsInTextureGroupLinesIsChecked As Boolean
	Private theDecompileQcIncludeDefineBoneLinesIsChecked As Boolean
	Private theDecompileQcUseMixedCaseForKeywordsIsChecked As Boolean

	Private theDecompileReferenceMeshSmdFileIsChecked As Boolean
	Private theDecompileRemovePathFromSmdMaterialFileNamesIsChecked As Boolean
	Private theDecompileUseNonValveUvConversionIsChecked As Boolean

	Private theDecompileBoneAnimationSmdFilesIsChecked As Boolean
	Private theDecompileBoneAnimationPlaceInSubfolderIsChecked As Boolean

	Private theDecompileTextureBmpFileIsChecked As Boolean
	Private theDecompileLodMeshSmdFilesIsChecked As Boolean
	Private theDecompilePhysicsMeshSmdFileIsChecked As Boolean
	Private theDecompileVertexAnimationVtaFileIsChecked As Boolean
	Private theDecompileProceduralBonesVrdFileIsChecked As Boolean

	Private theDecompileDeclareSequenceQciFileIsChecked As Boolean

	Private theDecompileFolderForEachModelIsChecked As Boolean
	Private theDecompilePrefixFileNamesWithModelNameIsChecked As Boolean
	Private theDecompileStricterFormatIsChecked As Boolean
	Private theDecompileLogFileIsChecked As Boolean
	Private theDecompileDebugInfoFilesIsChecked As Boolean

	Private theDecompileOverrideMdlVersion As SupportedMdlVersion

	Private theDecompileMode As InputOptions
	Private theDecompilerIsRunning As Boolean

	' Compile tab

	Private theCompileQcPathFileName As String
	Private theCompileMode As InputOptions

	Private theCompileOutputFolderIsChecked As Boolean
	'Private theCompileOutputFolderOption As OutputFolderOptions
	Private theCompileOutputFolderOption As CompileOutputPathOptions
	Private theCompileOutputSubfolderName As String
	Private theCompileOutputFullPath As String

	Private theCompileGameSetupSelectedIndex As Integer

	' GoldSource engine
	Private theCompileGoldSourceLogFileIsChecked As Boolean

	' Source engine
	Private theCompileSourceLogFileIsChecked As Boolean
	Private theCompileOptionDefineBonesIsChecked As Boolean
	Private theCompileOptionDefineBonesCreateFileIsChecked As Boolean
	Private theCompileOptionDefineBonesQciFileName As String
	Private theCompileOptionDefineBonesOverwriteQciFileIsChecked As Boolean
	Private theCompileOptionDefineBonesModifyQcFileIsChecked As Boolean
	Private theCompileOptionNoP4IsChecked As Boolean
	Private theCompileOptionVerboseIsChecked As Boolean

	Private theCompileOptionsText As String

	Private theCompilerIsRunning As Boolean

	' Patch tab

	Private thePatchMode As InputOptions
	Private thePatchMdlPathFileName As String

	' View tab

	Private theViewMdlPathFileName As String
	Private theViewOverrideMdlVersion As SupportedMdlVersion
	Private theViewGameSetupSelectedIndex As Integer

	Private theViewDataViewerIsRunning As Boolean
	Private theViewViewerIsRunning As Boolean

	' Pack tab

	Private thePackMode As PackInputOptions
	Private thePackInputPathFileName As String

	Private thePackOutputFolderOption As PackOutputPathOptions
	Private thePackOutputParentPath As String
	Private thePackOutputPath As String

	Private thePackGameSetupSelectedIndex As Integer

	Private thePackLogFileIsChecked As Boolean
	Private thePackOptionMultiFileVpkIsChecked As Boolean

	Private thePackGmaTitle As String
	Private thePackGmaItemTags As BindingListEx(Of String)

	Private thePackOptionsText As String

	Private thePackerIsRunning As Boolean

	' Publish tab

	Private thePublishGameSelectedIndex As Integer
	Private thePublishSearchField As PublishSearchFieldOptions
	Private thePublishSearchText As String
	'Private thePublishDragDroppedContentPath As String

	' Options tab

	Private theOptionsAutoOpenVpkFileIsChecked As Boolean
	Private theOptionsAutoOpenVpkFileOption As ActionType
	Private theOptionsAutoOpenGmaFileIsChecked As Boolean
	Private theOptionsAutoOpenGmaFileOption As ActionType
	Private theOptionsAutoOpenFpxFileIsChecked As Boolean

	Private theOptionsAutoOpenMdlFileIsChecked As Boolean
	Private theOptionsAutoOpenMdlFileForPreviewIsChecked As Boolean
	Private theOptionsAutoOpenMdlFileForDecompileIsChecked As Boolean
	Private theOptionsAutoOpenMdlFileForViewIsChecked As Boolean
	Private theOptionsAutoOpenMdlFileOption As ActionType

	Private theOptionsAutoOpenQcFileIsChecked As Boolean
	'Private theOptionsAutoOpenQcFileOption As ActionType

	Private theOptionsAutoOpenFolderOption As ActionType

	Private theOptionsDragAndDropVpkFileOption As ActionType
	Private theOptionsDragAndDropGmaFileOption As ActionType

	Private theOptionsDragAndDropMdlFileForPreviewIsChecked As Boolean
	Private theOptionsDragAndDropMdlFileForDecompileIsChecked As Boolean
	Private theOptionsDragAndDropMdlFileForViewIsChecked As Boolean
	Private theOptionsDragAndDropMdlFileOption As ActionType

	'Private theOptionsDragAndDropQcFileOption As ActionType

	Private theOptionsDragAndDropFolderOption As ActionType

	Private theOptionsContextMenuIntegrateMenuItemsIsChecked As Boolean
	Private theOptionsContextMenuIntegrateSubMenuIsChecked As Boolean

	Private theOptionsOpenWithCrowbarIsChecked As Boolean
	Private theOptionsViewMdlFileIsChecked As Boolean
	Private theOptionsDecompileMdlFileIsChecked As Boolean
	Private theOptionsDecompileFolderIsChecked As Boolean
	Private theOptionsDecompileFolderAndSubfoldersIsChecked As Boolean
	Private theOptionsCompileQcFileIsChecked As Boolean
	Private theOptionsCompileFolderIsChecked As Boolean
	Private theOptionsCompileFolderAndSubfoldersIsChecked As Boolean

	' Update tab

	Private theUpdateDownloadPath As String
	Private theUpdateUpdateToNewPathIsChecked As Boolean
	Private theUpdateUpdateDownloadPath As String
	Private theUpdateCopySettingsIsChecked As Boolean

#End Region

End Class
