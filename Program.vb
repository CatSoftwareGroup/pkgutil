Module Program

    Dim args() As String = Environment.GetCommandLineArgs()

    Sub Main()
        If args.Length < 2 Then
            Call UsageDialog()
        Else
            If args(1) = "--help" Then
                Call HelpDialog()
            End If
        End If
    End Sub


    Public Sub InstallPKG()

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
