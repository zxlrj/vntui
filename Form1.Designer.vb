<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(form1))
        TextBox1 = New TextBox()
        Button1 = New Button()
        Label1 = New Label()
        TextBox2 = New TextBox()
        Button2 = New Button()
        Label2 = New Label()
        TextBox3 = New TextBox()
        Button3 = New Button()
        Label3 = New Label()
        TextBox4 = New TextBox()
        Button4 = New Button()
        Button5 = New Button()
        chkAutoStart = New CheckBox()
        CheckBox2 = New CheckBox()
        SuspendLayout()
        ' 
        ' TextBox1
        ' 
        TextBox1.Location = New Point(12, 132)
        TextBox1.Multiline = True
        TextBox1.Name = "TextBox1"
        TextBox1.ScrollBars = ScrollBars.Both
        TextBox1.Size = New Size(481, 372)
        TextBox1.TabIndex = 0
        ' 
        ' Button1
        ' 
        Button1.Location = New Point(134, 84)
        Button1.Name = "Button1"
        Button1.Size = New Size(99, 32)
        Button1.TabIndex = 1
        Button1.Text = "开始连接"
        Button1.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(19, 12)
        Label1.Name = "Label1"
        Label1.Size = New Size(89, 17)
        Label1.TabIndex = 2
        Label1.Text = "vnt-cli.exe目录"
        ' 
        ' TextBox2
        ' 
        TextBox2.Location = New Point(114, 12)
        TextBox2.Name = "TextBox2"
        TextBox2.Size = New Size(178, 23)
        TextBox2.TabIndex = 3
        ' 
        ' Button2
        ' 
        Button2.Location = New Point(313, 12)
        Button2.Name = "Button2"
        Button2.Size = New Size(53, 22)
        Button2.TabIndex = 4
        Button2.Text = "查找"
        Button2.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(19, 44)
        Label2.Name = "Label2"
        Label2.Size = New Size(85, 17)
        Label2.TabIndex = 5
        Label2.Text = "vnt服务器地址"
        ' 
        ' TextBox3
        ' 
        TextBox3.Location = New Point(114, 44)
        TextBox3.Name = "TextBox3"
        TextBox3.Size = New Size(178, 23)
        TextBox3.TabIndex = 6
        TextBox3.Text = "vnt.wherewego.top:29872"
        ' 
        ' Button3
        ' 
        Button3.Location = New Point(250, 84)
        Button3.Name = "Button3"
        Button3.Size = New Size(96, 32)
        Button3.TabIndex = 7
        Button3.Text = "更新状态"
        Button3.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(372, 15)
        Label3.Name = "Label3"
        Label3.Size = New Size(56, 17)
        Label3.TabIndex = 8
        Label3.Text = "组网编号"
        ' 
        ' TextBox4
        ' 
        TextBox4.Location = New Point(436, 12)
        TextBox4.Name = "TextBox4"
        TextBox4.Size = New Size(48, 23)
        TextBox4.TabIndex = 9
        ' 
        ' Button4
        ' 
        Button4.Location = New Point(19, 84)
        Button4.Name = "Button4"
        Button4.Size = New Size(97, 32)
        Button4.TabIndex = 10
        Button4.Text = "上次配置"
        Button4.UseVisualStyleBackColor = True
        ' 
        ' Button5
        ' 
        Button5.Location = New Point(359, 84)
        Button5.Name = "Button5"
        Button5.Size = New Size(116, 32)
        Button5.TabIndex = 11
        Button5.Text = "最小化到托盘"
        Button5.UseVisualStyleBackColor = True
        ' 
        ' chkAutoStart
        ' 
        chkAutoStart.AutoSize = True
        chkAutoStart.Location = New Point(313, 44)
        chkAutoStart.Name = "chkAutoStart"
        chkAutoStart.Size = New Size(75, 21)
        chkAutoStart.TabIndex = 12
        chkAutoStart.Text = "自动启动"
        chkAutoStart.UseVisualStyleBackColor = True
        ' 
        ' CheckBox2
        ' 
        CheckBox2.AutoSize = True
        CheckBox2.Location = New Point(409, 46)
        CheckBox2.Name = "CheckBox2"
        CheckBox2.Size = New Size(75, 21)
        CheckBox2.TabIndex = 13
        CheckBox2.Text = "自动连接"
        CheckBox2.UseVisualStyleBackColor = True
        ' 
        ' form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 17F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(510, 519)
        Controls.Add(CheckBox2)
        Controls.Add(chkAutoStart)
        Controls.Add(Button5)
        Controls.Add(Button4)
        Controls.Add(TextBox4)
        Controls.Add(Label3)
        Controls.Add(Button3)
        Controls.Add(TextBox3)
        Controls.Add(Label2)
        Controls.Add(Button2)
        Controls.Add(TextBox2)
        Controls.Add(Label1)
        Controls.Add(Button1)
        Controls.Add(TextBox1)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Name = "form1"
        Text = "VntUi"
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents Button1 As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents Button2 As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents Button3 As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents Button4 As Button
    Friend WithEvents Button5 As Button
    Friend WithEvents chkAutoStart As CheckBox
    Friend WithEvents CheckBox2 As CheckBox

End Class
