Module Program

    Dim args() As String = Environment.GetCommandLineArgs()

    Sub Main()
        If args.Length < 2 Then
            Call UsageDialog()
        Else
            If args(1) = "--help" Then
                Call HelpDialog()
            ElseIf args(1) = "--add" Then
                Call InstallPKG()
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


    Public Sub InstallPKG()

    End Sub

    Public Sub PKGInfo()

    End Sub

    Public Sub UninstallPKG()

    End Sub

    Public Sub VersionDialog()

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
