Imports System.IO
Imports System.IO.Compression

Module Program
    Dim PROG_VERSION As String = "1.0"
    Dim args() As String = Environment.GetCommandLineArgs()

    Sub Main()
        If args.Length < 2 Then
            Call UsageDialog()
        Else
            If args(1) = "--help" Then
                Call HelpDialog()
            ElseIf args(1) = "--add" Then
                If args.Length < 3 Then
                    Console.WriteLine("No Package Selected!")
                Else
                    If My.Computer.FileSystem.FileExists(args(2)) Then
                        If Path.GetExtension(args(2)) = ".pkg" Then
                            Call InstallDialog()
                        Else
                            Call InvalidPKG()
                        End If
                    Else
                        Console.WriteLine("No Package Selected!")
                    End If
                End If
            ElseIf args(1) = "--info" Then
                Call PKGInfo()
            ElseIf args(1) = "--del" Then
                Call UninstallPKG()
            ElseIf args(1) = "--version" Then
                Call VersionDialog()
            Else
                Call UsageDialog()
            End If
        End If
    End Sub


    Public Sub InstallDialog()
        Console.Write("Install Package? (y/n):")
        Dim yndialog = Console.ReadLine()
        If yndialog = "y" Then
            Call InstallPKG()
        ElseIf yndialog = "Y" Then
            Call InstallPKG()
        ElseIf yndialog = "n" Then
            Environment.Exit(0)
        ElseIf yndialog = "N" Then
            Environment.Exit(0)
        Else
        End If
    End Sub
    Public Sub InstallPKG()
        Dim Abspath As String
        Dim installdir As String
        Abspath = IO.Path.GetFileNameWithoutExtension(args(2))
        installdir = "\rootfs\etc\packages\" + Path.GetFileNameWithoutExtension(Abspath)
        If Directory.Exists("\rootfs") Then
            Console.WriteLine("Installing " + args(2))
            Try
                Directory.CreateDirectory(installdir)
                ZipFile.ExtractToDirectory(Abspath + ".pkg", "\rootfs\etc\package_cache")
                If My.Computer.FileSystem.FileExists("\rootfs\etc\package_cache\install.bat") Then
                    Dim runscriptinfo As New ProcessStartInfo
                    runscriptinfo.FileName = "\rootfs\etc\package_cache\install.bat"
                    runscriptinfo.UseShellExecute = True
                    Process.Start(runscriptinfo).WaitForExit()
                    File.Copy("\rootfs\etc\package_cache\uninstall.bat", installdir + "\uninstall.bat")
                    File.Copy("\rootfs\etc\package_cache\package.info", installdir + "\package.info")
                    Directory.Delete("\rootfs\etc\package_cache", True)
                    Directory.CreateDirectory("\rootfs\etc\package_cache")
                Else
                    Call InvalidPKG()
                End If
            Catch ex As Exception
                Console.WriteLine("Error on installing Package: " + ex.Message)
            End Try

        Else
            Console.WriteLine("The command needs to be executed on the Lukindu root disk.")
        End If

    End Sub
    Public Sub PKGInfo()
        If File.Exists("\rootfs\etc\packages\" + args(2) + "\package.info") Then
            Dim filereader As String
            filereader = My.Computer.FileSystem.ReadAllText("\rootfs\etc\packages\" + args(2) + "\package.info")
            Console.WriteLine(filereader)
        Else
            Console.WriteLine("Package " + args(2) + " is not installed!")
        End If
    End Sub

    Public Sub UninstallPKG()

        If Directory.Exists("\rootfs\etc\packages\" + args(2)) Then
            Console.WriteLine("Uninstalling " + args(2))
            Try
                Dim runscriptinfo As New ProcessStartInfo
                runscriptinfo.FileName = "\rootfs\etc\packages\" + args(2) + "\uninstall.bat"
                runscriptinfo.UseShellExecute = True
                Process.Start(runscriptinfo).WaitForExit()
                Directory.Delete("\rootfs\etc\packages\" + args(2), True)
            Catch ex As Exception
                Console.WriteLine("Error on uninstalling Package: " + ex.Message)
            End Try
        Else
            Console.WriteLine("Package " + args(2) + " is not installed!")
        End If
    End Sub

    Public Sub VersionDialog()
        Console.WriteLine("Lukindu Package Utility " + PROG_VERSION)
    End Sub

    Public Sub InvalidPKG()
        Console.WriteLine("Invalid Package file!")
    End Sub

    Public Sub UsageDialog()
        Console.WriteLine("Invalid Args! Try pkgutil --help")
    End Sub

    Public Sub HelpDialog()
        Console.WriteLine("Lukindu Package utility")
        Console.WriteLine()
        Console.WriteLine("--add - install new package")
        Console.WriteLine("--info - get info of package")
        Console.WriteLine("--del - uninstall package")
        Console.WriteLine("--version - version of PKGUtil")
    End Sub
End Module
