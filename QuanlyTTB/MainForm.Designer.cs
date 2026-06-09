namespace QuanlyTTB;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null!;
    private Panel panelSidebar;
    private Panel panelContent;
    private Label lblLogo;
    private Label lblSubtitle;
    private Button btnM1;
    private Button btnM2;
    private Button btnM3;
    private Button btnM4;
    private Button btnM5;
    private Button btnM6;
    private UserControls.UcThemMoi ucThemMoiPreview;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        panelSidebar = new Panel();
        btnM6 = new Button();
        btnM5 = new Button();
        btnM4 = new Button();
        btnM3 = new Button();
        btnM2 = new Button();
        btnM1 = new Button();
        lblSubtitle = new Label();
        lblLogo = new Label();
        panelContent = new Panel();
        ucThemMoiPreview = new QuanlyTTB.UserControls.UcThemMoi();
        panelSidebar.SuspendLayout();
        panelContent.SuspendLayout();
        SuspendLayout();
        // 
        // panelSidebar
        // 
        panelSidebar.BackColor = Color.FromArgb(18, 67, 42);
        panelSidebar.Controls.Add(btnM6);
        panelSidebar.Controls.Add(btnM5);
        panelSidebar.Controls.Add(btnM4);
        panelSidebar.Controls.Add(btnM3);
        panelSidebar.Controls.Add(btnM2);
        panelSidebar.Controls.Add(btnM1);
        panelSidebar.Controls.Add(lblSubtitle);
        panelSidebar.Controls.Add(lblLogo);
        panelSidebar.Dock = DockStyle.Left;
        panelSidebar.Location = new Point(0, 0);
        panelSidebar.Margin = new Padding(3, 4, 3, 4);
        panelSidebar.Name = "panelSidebar";
        panelSidebar.Size = new Size(240, 960);
        panelSidebar.TabIndex = 0;
        // 
        // btnM6
        // 
        btnM6.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
        btnM6.BackColor = Color.FromArgb(27, 94, 60);
        btnM6.Cursor = Cursors.Hand;
        btnM6.FlatAppearance.BorderSize = 0;
        btnM6.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 125, 82);
        btnM6.FlatStyle = FlatStyle.Flat;
        btnM6.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnM6.ForeColor = Color.White;
        btnM6.Location = new Point(0, 840);
        btnM6.Margin = new Padding(3, 4, 3, 4);
        btnM6.Name = "btnM6";
        btnM6.Padding = new Padding(21, 0, 0, 0);
        btnM6.Size = new Size(240, 64);
        btnM6.TabIndex = 7;
        btnM6.Text = "M6  -  Thoát";
        btnM6.TextAlign = ContentAlignment.MiddleLeft;
        btnM6.UseVisualStyleBackColor = false;
        btnM6.Click += btnM6_Click;
        // 
        // btnM5
        // 
        btnM5.BackColor = Color.FromArgb(27, 94, 60);
        btnM5.Cursor = Cursors.Hand;
        btnM5.FlatAppearance.BorderSize = 0;
        btnM5.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 125, 82);
        btnM5.FlatStyle = FlatStyle.Flat;
        btnM5.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnM5.ForeColor = Color.White;
        btnM5.Location = new Point(0, 455);
        btnM5.Margin = new Padding(3, 4, 3, 4);
        btnM5.Name = "btnM5";
        btnM5.Padding = new Padding(21, 0, 0, 0);
        btnM5.Size = new Size(240, 64);
        btnM5.TabIndex = 6;
        btnM5.Text = "M5  -  Thống kê";
        btnM5.TextAlign = ContentAlignment.MiddleLeft;
        btnM5.UseVisualStyleBackColor = false;
        btnM5.Click += btnM5_Click;
        // 
        // btnM4
        // 
        btnM4.BackColor = Color.FromArgb(27, 94, 60);
        btnM4.Cursor = Cursors.Hand;
        btnM4.FlatAppearance.BorderSize = 0;
        btnM4.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 125, 82);
        btnM4.FlatStyle = FlatStyle.Flat;
        btnM4.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnM4.ForeColor = Color.White;
        btnM4.Location = new Point(0, 383);
        btnM4.Margin = new Padding(3, 4, 3, 4);
        btnM4.Name = "btnM4";
        btnM4.Padding = new Padding(21, 0, 0, 0);
        btnM4.Size = new Size(240, 64);
        btnM4.TabIndex = 5;
        btnM4.Text = "M4  -  Tìm kiếm";
        btnM4.TextAlign = ContentAlignment.MiddleLeft;
        btnM4.UseVisualStyleBackColor = false;
        btnM4.Click += btnM4_Click;
        // 
        // btnM3
        // 
        btnM3.BackColor = Color.FromArgb(27, 94, 60);
        btnM3.Cursor = Cursors.Hand;
        btnM3.FlatAppearance.BorderSize = 0;
        btnM3.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 125, 82);
        btnM3.FlatStyle = FlatStyle.Flat;
        btnM3.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnM3.ForeColor = Color.White;
        btnM3.Location = new Point(0, 311);
        btnM3.Margin = new Padding(3, 4, 3, 4);
        btnM3.Name = "btnM3";
        btnM3.Padding = new Padding(21, 0, 0, 0);
        btnM3.Size = new Size(240, 64);
        btnM3.TabIndex = 4;
        btnM3.Text = "M3  -  Sắp xếp";
        btnM3.TextAlign = ContentAlignment.MiddleLeft;
        btnM3.UseVisualStyleBackColor = false;
        btnM3.Click += btnM3_Click;
        // 
        // btnM2
        // 
        btnM2.BackColor = Color.FromArgb(27, 94, 60);
        btnM2.Cursor = Cursors.Hand;
        btnM2.FlatAppearance.BorderSize = 0;
        btnM2.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 125, 82);
        btnM2.FlatStyle = FlatStyle.Flat;
        btnM2.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnM2.ForeColor = Color.White;
        btnM2.Location = new Point(0, 239);
        btnM2.Margin = new Padding(3, 4, 3, 4);
        btnM2.Name = "btnM2";
        btnM2.Padding = new Padding(21, 0, 0, 0);
        btnM2.Size = new Size(240, 64);
        btnM2.TabIndex = 3;
        btnM2.Text = "M2  -  In danh sách";
        btnM2.TextAlign = ContentAlignment.MiddleLeft;
        btnM2.UseVisualStyleBackColor = false;
        btnM2.Click += btnM2_Click;
        // 
        // btnM1
        // 
        btnM1.BackColor = Color.FromArgb(27, 94, 60);
        btnM1.Cursor = Cursors.Hand;
        btnM1.FlatAppearance.BorderColor = Color.White;
        btnM1.FlatAppearance.BorderSize = 0;
        btnM1.FlatAppearance.MouseOverBackColor = Color.FromArgb(39, 125, 82);
        btnM1.FlatStyle = FlatStyle.Flat;
        btnM1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        btnM1.ForeColor = Color.White;
        btnM1.Location = new Point(0, 167);
        btnM1.Margin = new Padding(3, 4, 3, 4);
        btnM1.Name = "btnM1";
        btnM1.Padding = new Padding(21, 0, 0, 0);
        btnM1.Size = new Size(240, 64);
        btnM1.TabIndex = 2;
        btnM1.Text = "M1  -  Thêm mới hồ sơ";
        btnM1.TextAlign = ContentAlignment.MiddleLeft;
        btnM1.UseVisualStyleBackColor = false;
        btnM1.Click += btnM1_Click;
        // 
        // lblSubtitle
        // 
        lblSubtitle.ForeColor = Color.FromArgb(196, 232, 211);
        lblSubtitle.Location = new Point(14, 83);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Size = new Size(213, 51);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "HỆ THỐNG QUẢN LÝ\r\nTRANG THIẾT BỊ";
        lblSubtitle.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // lblLogo
        // 
        lblLogo.Font = new Font("Segoe UI", 17F, FontStyle.Bold);
        lblLogo.ForeColor = Color.White;
        lblLogo.Location = new Point(14, 33);
        lblLogo.Name = "lblLogo";
        lblLogo.Size = new Size(213, 48);
        lblLogo.TabIndex = 0;
        lblLogo.Text = "QUẢN LÝ TTB";
        lblLogo.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // panelContent
        // 
        panelContent.BackColor = Color.FromArgb(242, 247, 244);
        panelContent.Controls.Add(ucThemMoiPreview);
        panelContent.Dock = DockStyle.Fill;
        panelContent.Location = new Point(240, 0);
        panelContent.Margin = new Padding(3, 4, 3, 4);
        panelContent.Name = "panelContent";
        panelContent.Size = new Size(1131, 960);
        panelContent.TabIndex = 1;
        // 
        // ucThemMoiPreview
        // 
        ucThemMoiPreview.BackColor = Color.FromArgb(242, 247, 244);
        ucThemMoiPreview.Dock = DockStyle.Fill;
        ucThemMoiPreview.Location = new Point(0, 0);
        ucThemMoiPreview.Margin = new Padding(3, 5, 3, 5);
        ucThemMoiPreview.MinimumSize = new Size(1176, 1209);
        ucThemMoiPreview.Name = "ucThemMoiPreview";
        ucThemMoiPreview.Size = new Size(1176, 1209);
        ucThemMoiPreview.TabIndex = 0;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        ClientSize = new Size(1371, 960);
        Controls.Add(panelContent);
        Controls.Add(panelSidebar);
        Font = new Font("Segoe UI", 9F);
        Margin = new Padding(3, 4, 3, 4);
        MinimumSize = new Size(1197, 851);
        Name = "MainForm";
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Hệ thống Quản lý Trang Thiết Bị";
        WindowState = FormWindowState.Maximized;
        panelSidebar.ResumeLayout(false);
        panelContent.ResumeLayout(false);
        ResumeLayout(false);
    }
}
