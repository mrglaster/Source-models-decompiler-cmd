Imports System.IO

Module Main

	' Entry point of application.
	Public Function Main() As Integer
		Dim anExceptionHandler As New AppExceptionHandler()
		AddHandler Application.ThreadException, AddressOf anExceptionHandler.Application_ThreadException
		Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException)
		Application.EnableVisualStyles()
		Application.SetCompatibleTextRenderingDefault(False)

		TheApp = New App()



		TheApp.Init()




		TheApp.Dispose()

		Return 0
	End Function

	'Public TheJob As WindowsJob
	Public TheApp As App

End Module
