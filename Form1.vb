Imports System.Diagnostics
Imports System.Threading
Imports System.Windows.Forms.VisualStyles.VisualStyleElement
Imports System.Windows.Forms
Imports System.Security.Principal
Imports System.IO
Imports System.Configuration


Public Class form1
    Inherits Form
    'Dim process As Process
    Dim outputThread As Thread

    Private settingsPath As String = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.txt")
    Private stdInWriter As StreamWriter
    ' 创建 NotifyIcon 控件
    Private WithEvents process As Process
    Private trayIcon As New NotifyIcon()
    Private contextMenuStrip As New ContextMenuStrip()






    Private Sub TrayIcon_DoubleClick(sender As Object, e As EventArgs)
        ' 双击托盘图标时显示主窗口
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    ' 其他代码...

    Function IsRunningAsAdmin() As Boolean
        Dim identity As WindowsIdentity = WindowsIdentity.GetCurrent()
        Dim principal As New WindowsPrincipal(identity)
        Return principal.IsInRole(WindowsBuiltInRole.Administrator)
    End Function

    Private Sub InitializeContextMenu()
        ' 创建“显示窗口”菜单项
        Dim showMenuItem As New ToolStripMenuItem("显示窗口")
        AddHandler showMenuItem.Click, AddressOf ShowMenuItem_Click

        ' 创建“退出”菜单项
        Dim exitMenuItem As New ToolStripMenuItem("退出")
        AddHandler exitMenuItem.Click, AddressOf ExitMenuItem_Click

        ' 将菜单项添加到上下文菜单
        contextMenuStrip.Items.Add(showMenuItem)
        contextMenuStrip.Items.Add(exitMenuItem)

        ' 将上下文菜单关联到托盘图标
        trayIcon.ContextMenuStrip = contextMenuStrip
    End Sub


    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing

        SaveSettings()
    End Sub


    Private Sub ShowMenuItem_Click(sender As Object, e As EventArgs)
        ' 显示或激活主窗体
        Me.Show()
        Me.WindowState = FormWindowState.Normal
    End Sub

    Private Sub ExitMenuItem_Click(sender As Object, e As EventArgs)
        ' 退出应用程序
        trayIcon.Visible = False
        ' Close a.exe when the form is closing

        Application.Exit()
    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        InitializeContextMenu()

        LoadSettings()

        ' 设置窗体无边框样式，这样用户就不能调整大小了
        Me.FormBorderStyle = FormBorderStyle.FixedSingle

        ' 禁用最大化按钮和最小化按钮
        Me.MaximizeBox = False
        Me.MinimizeBox = False

        ' 初始化 NotifyIcon
        trayIcon.Icon = My.Forms.form1.Icon ' 设置托盘图标
        trayIcon.Text = "VntUi" ' 设置托盘图标的文本
        trayIcon.Visible = True ' 设置托盘图标可见
        TextBox2.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "vnt\vnt-cli.exe")

        CheckAndCreateConfigFile()
        ' 添加双击事件处理程序
        AddHandler trayIcon.DoubleClick, AddressOf TrayIcon_DoubleClick

        ' 添加关闭事件处理程序
        '  AddHandler Me.FormClosing, AddressOf MainForm_FormClosing

        ' 检查当前是否以管理员权限运行
        If Not IsRunningAsAdmin() Then
            ' 如果不是，则以管理员权限重新启动程序
            Dim processInfo As New ProcessStartInfo With {
                .UseShellExecute = True,
                .WorkingDirectory = Environment.CurrentDirectory,
                .FileName = Application.ExecutablePath,
                .Verb = "runas"
            }

            Try
                Process.Start(processInfo)
            Catch ex As Exception
                ' 处理用户点击“取消”的情况
                MessageBox.Show("无法以管理员权限运行程序。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try

            ' 退出当前实例
            Application.Exit()
        Else
            ' 程序以管理员权限运行，执行其他代码

            ' 检查注册表中是否设置了自动启动



            LoadSettings()
            ' ...
        End If
        Dim autoStart As Boolean = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", False).GetValue("vntui") IsNot Nothing
        ' 根据自动启动设置复选框状态
        chkAutoStart.Checked = autoStart

        ' 检查配置文件是否存在，如果不存在则创建
        CheckAndCreateConfigFile()


        Me.CheckBox2.Checked = ReadAutoClickSetting()


        LoadSettings()
    End Sub

    Sub ReadOutput()
        While Not process.StandardOutput.EndOfStream
            Dim line As String = process.StandardOutput.ReadLine()
            Console.WriteLine(line)
            Me.Invoke(Sub() TextBox1.AppendText(line + vbCrLf))
        End While
    End Sub

    Sub SendCommand(command As String)
        If process IsNot Nothing AndAlso Not process.HasExited Then
            ' 写入命令到标准输入
            process.StandardInput.WriteLine(command)
            ' 刷新输入流，确保命令被发送
            process.StandardInput.Flush()
        End If
    End Sub

    Sub OnProcessExit(sender As Object, e As EventArgs)
        ' 确保进程已启动
        If Not process Is Nothing AndAlso Not process.HasExited Then
            ' 尝试正常关闭进程
            process.CloseMainWindow()
            ' 等待进程退出
            If Not process.WaitForExit(1000) Then
                ' 如果进程没有在指定时间内退出，则强制终止
                process.Kill()
            End If
            ' 释放资源
            process.Dispose()
        End If
    End Sub
    Private Sub LoadSettings()

        settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.txt")
        If File.Exists(settingsPath) Then
            Dim lines As String() = File.ReadAllLines(settingsPath)
            If lines.Length >= 3 Then
                TextBox2.Text = lines(0)
                TextBox3.Text = lines(1)
                TextBox4.Text = lines(2)
            End If
        End If
    End Sub

    Private Sub SaveSettings()
        settingsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "settings.txt")
        Dim lines As String() = {TextBox2.Text, TextBox3.Text, TextBox4.Text}
        File.WriteAllLines(settingsPath, lines)
    End Sub
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SaveSettings()
        If process Is Nothing OrElse process.HasExited Then
            ' Start a.exe with -aaa argument
            process = New Process()
            With process.StartInfo
                .FileName = TextBox2.Text
                .Arguments = "-s  " & TextBox3.Text & " -k " & TextBox4.Text & " --cmd" ' "-s  " & TextBox3.Text & 
                .UseShellExecute = False
                .CreateNoWindow = True
                .RedirectStandardOutput = True
                .RedirectStandardInput = True
                .RedirectStandardError = True
                .StandardOutputEncoding = System.Text.Encoding.UTF8
            End With

            process.Start()

            ' Start a thread to read the output
            outputThread = New Thread(AddressOf ReadOutput)
            outputThread.Start()

            ' Keep the StreamWriter to send commands later
            stdInWriter = process.StandardInput
        End If





        SaveSettings()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If Not process Is Nothing AndAlso Not process.HasExited Then
            ' Send "list" command to a.exe
            stdInWriter.WriteLine("list")
            stdInWriter.Flush()
        End If
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Using openFileDialog As New OpenFileDialog()
            openFileDialog.Filter = “vnt-cli.exe|vnt-cli.exe”
            openFileDialog.Title = “请选择 vnt-cli.exe 文件”

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                TextBox2.Text = openFileDialog.FileName
            End If
        End Using

    End Sub

    Private Sub NotifyIcon1_MouseDoubleClick(sender As Object, e As MouseEventArgs)

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        LoadSettings()
    End Sub

    Private Sub form1_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed

        SaveSettings()

        ' 确保在窗体关闭时，通知区域的图标也被移除
        ' 确保在窗体关闭时，通知区域的图标也被移除
        If Not trayIcon Is Nothing Then
            trayIcon.Visible = False
            trayIcon.Dispose()
        End If


        ' Close a.exe when the form is closing
        If Not process Is Nothing AndAlso Not process.HasExited Then
            process.Kill()
            process.WaitForExit()
            process.Close()
        End If


    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        Me.Hide() ' 隐藏主窗口
        trayIcon.ShowBalloonTip(1000, "VntUi", "vntui已经最小化到系统托盘.", ToolTipIcon.Info) ' 显示气球提示

    End Sub

    Private Sub chkAutoStart_CheckedChanged(sender As Object, e As EventArgs) Handles chkAutoStart.CheckedChanged
        Dim autoStart As Boolean = chkAutoStart.Checked
        ' 打开或创建注册表项
        Dim regKey As Microsoft.Win32.RegistryKey = My.Computer.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Windows\CurrentVersion\Run", True)
        If autoStart Then
            ' 如果复选框被选中，设置程序自动启动
            regKey.SetValue("vntui", Application.ExecutablePath)
        Else
            ' 如果复选框未被选中，取消程序自动启动
            regKey.DeleteValue("vntui", False)
        End If
    End Sub

    Private Sub SaveAutoClickSetting(checked As Boolean)
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        config.AppSettings.Settings("AutoClickEnabled").Value = checked.ToString()
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
    End Sub

    Private Function ReadAutoClickSetting() As Boolean
        On Error GoTo cuowu
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        Dim setting As String = config.AppSettings.Settings("AutoClickEnabled").Value
        Return Boolean.Parse(setting)
        Exit Function
cuowu:
        Return False
    End Function

    Private Sub CheckAndCreateConfigFile()
        Dim configFile As String = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).FilePath
        If Not File.Exists(configFile) Then
            ' 配置文件不存在，创建默认配置
            Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
            config.AppSettings.Settings.Add("AutoClickEnabled", "False")
            config.Save(ConfigurationSaveMode.Full)
        End If
    End Sub


    Private Sub CheckBox2_CheckedChanged(sender As Object, e As EventArgs) Handles CheckBox2.CheckedChanged
        ' 更新配置文件
        SaveAutoClickSetting(Me.CheckBox2.Checked)

        ' 如果复选框被选中，设置定时器
        If Me.CheckBox2.Checked Then
            Me.Invoke(New MethodInvoker(Sub() Button1_Click(Nothing, Nothing)))
        Else
            ' 如果复选框未选中，取消定时器

        End If
    End Sub

    Private Sub form1_LostFocus(sender As Object, e As EventArgs) Handles Me.LostFocus

    End Sub
End Class
