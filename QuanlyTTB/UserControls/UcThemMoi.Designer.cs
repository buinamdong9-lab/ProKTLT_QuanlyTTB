namespace QuanlyTTB.UserControls;

partial class UcThemMoi
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle;
    private Label lblSubtitle;
    private TableLayoutPanel tableForm;
    private Label lblMaTTB;
    private Label lblSoHieu;
    private Label lblTenTTB;
    private Label lblNgaySanXuat;
    private Label lblNgaySuDung;
    private Label lblNguonCap;
    private Label lblSoLuong;
    private Label lblChungLoai;
    private Label lblCap;
    private TextBox txtMaTTB;
    private TextBox txtSoHieu;
    private TextBox txtTenTTB;
    private DateTimePicker dtpNgaySanXuat;
    private DateTimePicker dtpNgaySuDung;
    private TextBox txtNguonCap;
    private TextBox txtSoLuong;
    private TextBox txtChungLoai;
    private NumericUpDown nudCap;
    private Panel panelButtons;
    private Button btnLuu;
    private Button btnLamMoi;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        lblTitle = new Label();
        lblSubtitle = new Label();
        tableForm = new TableLayoutPanel();
        lblMaTTB = new Label();
        txtMaTTB = new TextBox();
        lblSoHieu = new Label();
        txtSoHieu = new TextBox();
        lblTenTTB = new Label();
        txtTenTTB = new TextBox();
        lblNgaySanXuat = new Label();
        dtpNgaySanXuat = new DateTimePicker();
        lblNgaySuDung = new Label();
        dtpNgaySuDung = new DateTimePicker();
        lblNguonCap = new Label();
        txtNguonCap = new TextBox();
        lblSoLuong = new Label();
        txtSoLuong = new TextBox();
        lblChungLoai = new Label();
        txtChungLoai = new TextBox();
        lblCap = new Label();
        nudCap = new NumericUpDown();
        panelButtons = new Panel();
        btnLamMoi = new Button();
        btnLuu = new Button();
        tableForm.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)nudCap).BeginInit();
        panelButtons.SuspendLayout();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.White;
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(27, 94, 60);
        lblTitle.Location = new Point(0, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Padding = new Padding(25, 18, 0, 0);
        lblTitle.Size = new Size(950, 58);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "M1 - THÊM MỚI HỒ SƠ THIẾT BỊ";
        // 
        // lblSubtitle
        // 
        lblSubtitle.BackColor = Color.White;
        lblSubtitle.Dock = DockStyle.Top;
        lblSubtitle.ForeColor = Color.DimGray;
        lblSubtitle.Location = new Point(0, 58);
        lblSubtitle.Name = "lblSubtitle";
        lblSubtitle.Padding = new Padding(28, 0, 0, 0);
        lblSubtitle.Size = new Size(950, 38);
        lblSubtitle.TabIndex = 1;
        lblSubtitle.Text = "Tất cả các trường đều bắt buộc nhập.";
        // 
        // tableForm
        // 
        tableForm.Anchor = AnchorStyles.Top;
        tableForm.BackColor = Color.White;
        tableForm.ColumnCount = 2;
        tableForm.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 210F));
        tableForm.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        tableForm.Controls.Add(lblMaTTB, 0, 0);
        tableForm.Controls.Add(txtMaTTB, 1, 0);
        tableForm.Controls.Add(lblSoHieu, 0, 1);
        tableForm.Controls.Add(txtSoHieu, 1, 1);
        tableForm.Controls.Add(lblTenTTB, 0, 2);
        tableForm.Controls.Add(txtTenTTB, 1, 2);
        tableForm.Controls.Add(lblNgaySanXuat, 0, 3);
        tableForm.Controls.Add(dtpNgaySanXuat, 1, 3);
        tableForm.Controls.Add(lblNgaySuDung, 0, 4);
        tableForm.Controls.Add(dtpNgaySuDung, 1, 4);
        tableForm.Controls.Add(lblNguonCap, 0, 5);
        tableForm.Controls.Add(txtNguonCap, 1, 5);
        tableForm.Controls.Add(lblSoLuong, 0, 6);
        tableForm.Controls.Add(txtSoLuong, 1, 6);
        tableForm.Controls.Add(lblChungLoai, 0, 7);
        tableForm.Controls.Add(txtChungLoai, 1, 7);
        tableForm.Controls.Add(lblCap, 0, 8);
        tableForm.Controls.Add(nudCap, 1, 8);
        tableForm.Location = new Point(95, 115);
        tableForm.Name = "tableForm";
        tableForm.Padding = new Padding(25, 12, 25, 12);
        tableForm.RowCount = 9;
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.RowStyles.Add(new RowStyle(SizeType.Absolute, 48F));
        tableForm.Size = new Size(760, 456);
        tableForm.TabIndex = 2;
        // 
        // labels
        // 
        lblMaTTB.Dock = DockStyle.Fill;
        lblMaTTB.Font = new Font("Segoe UI", 9.5F);
        lblMaTTB.Name = "lblMaTTB";
        lblMaTTB.TabIndex = 0;
        lblMaTTB.Text = "Mã trang thiết bị (*)";
        lblMaTTB.TextAlign = ContentAlignment.MiddleRight;
        lblSoHieu.Dock = DockStyle.Fill;
        lblSoHieu.Font = new Font("Segoe UI", 9.5F);
        lblSoHieu.Name = "lblSoHieu";
        lblSoHieu.TabIndex = 2;
        lblSoHieu.Text = "Số hiệu trang thiết bị (*)";
        lblSoHieu.TextAlign = ContentAlignment.MiddleRight;
        lblTenTTB.Dock = DockStyle.Fill;
        lblTenTTB.Font = new Font("Segoe UI", 9.5F);
        lblTenTTB.Name = "lblTenTTB";
        lblTenTTB.TabIndex = 4;
        lblTenTTB.Text = "Tên trang thiết bị (*)";
        lblTenTTB.TextAlign = ContentAlignment.MiddleRight;
        lblNgaySanXuat.Dock = DockStyle.Fill;
        lblNgaySanXuat.Font = new Font("Segoe UI", 9.5F);
        lblNgaySanXuat.Name = "lblNgaySanXuat";
        lblNgaySanXuat.TabIndex = 6;
        lblNgaySanXuat.Text = "Ngày sản xuất (*)";
        lblNgaySanXuat.TextAlign = ContentAlignment.MiddleRight;
        lblNgaySuDung.Dock = DockStyle.Fill;
        lblNgaySuDung.Font = new Font("Segoe UI", 9.5F);
        lblNgaySuDung.Name = "lblNgaySuDung";
        lblNgaySuDung.TabIndex = 8;
        lblNgaySuDung.Text = "Ngày đưa vào sử dụng (*)";
        lblNgaySuDung.TextAlign = ContentAlignment.MiddleRight;
        lblNguonCap.Dock = DockStyle.Fill;
        lblNguonCap.Font = new Font("Segoe UI", 9.5F);
        lblNguonCap.Name = "lblNguonCap";
        lblNguonCap.TabIndex = 10;
        lblNguonCap.Text = "Nguồn cấp (*)";
        lblNguonCap.TextAlign = ContentAlignment.MiddleRight;
        lblSoLuong.Dock = DockStyle.Fill;
        lblSoLuong.Font = new Font("Segoe UI", 9.5F);
        lblSoLuong.Name = "lblSoLuong";
        lblSoLuong.TabIndex = 12;
        lblSoLuong.Text = "Số lượng (*)";
        lblSoLuong.TextAlign = ContentAlignment.MiddleRight;
        lblChungLoai.Dock = DockStyle.Fill;
        lblChungLoai.Font = new Font("Segoe UI", 9.5F);
        lblChungLoai.Name = "lblChungLoai";
        lblChungLoai.TabIndex = 14;
        lblChungLoai.Text = "Chủng loại (*)";
        lblChungLoai.TextAlign = ContentAlignment.MiddleRight;
        lblCap.Dock = DockStyle.Fill;
        lblCap.Font = new Font("Segoe UI", 9.5F);
        lblCap.Name = "lblCap";
        lblCap.TabIndex = 16;
        lblCap.Text = "Cấp (1 - 5) (*)";
        lblCap.TextAlign = ContentAlignment.MiddleRight;
        // 
        // inputs
        // 
        txtMaTTB.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtMaTTB.Font = new Font("Segoe UI", 10F);
        txtMaTTB.Margin = new Padding(15, 8, 15, 8);
        txtMaTTB.Name = "txtMaTTB";
        txtMaTTB.Size = new Size(480, 25);
        txtMaTTB.TabIndex = 1;
        txtSoHieu.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtSoHieu.Font = new Font("Segoe UI", 10F);
        txtSoHieu.Margin = new Padding(15, 8, 15, 8);
        txtSoHieu.Name = "txtSoHieu";
        txtSoHieu.Size = new Size(480, 25);
        txtSoHieu.TabIndex = 3;
        txtTenTTB.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtTenTTB.Font = new Font("Segoe UI", 10F);
        txtTenTTB.Margin = new Padding(15, 8, 15, 8);
        txtTenTTB.Name = "txtTenTTB";
        txtTenTTB.Size = new Size(480, 25);
        txtTenTTB.TabIndex = 5;
        dtpNgaySanXuat.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        dtpNgaySanXuat.Font = new Font("Segoe UI", 10F);
        dtpNgaySanXuat.Format = DateTimePickerFormat.Short;
        dtpNgaySanXuat.Margin = new Padding(15, 8, 15, 8);
        dtpNgaySanXuat.Name = "dtpNgaySanXuat";
        dtpNgaySanXuat.Size = new Size(480, 25);
        dtpNgaySanXuat.TabIndex = 7;
        dtpNgaySuDung.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        dtpNgaySuDung.Font = new Font("Segoe UI", 10F);
        dtpNgaySuDung.Format = DateTimePickerFormat.Short;
        dtpNgaySuDung.Margin = new Padding(15, 8, 15, 8);
        dtpNgaySuDung.Name = "dtpNgaySuDung";
        dtpNgaySuDung.Size = new Size(480, 25);
        dtpNgaySuDung.TabIndex = 9;
        txtNguonCap.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtNguonCap.Font = new Font("Segoe UI", 10F);
        txtNguonCap.Margin = new Padding(15, 8, 15, 8);
        txtNguonCap.Name = "txtNguonCap";
        txtNguonCap.Size = new Size(480, 25);
        txtNguonCap.TabIndex = 11;
        txtSoLuong.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtSoLuong.Font = new Font("Segoe UI", 10F);
        txtSoLuong.Margin = new Padding(15, 8, 15, 8);
        txtSoLuong.Name = "txtSoLuong";
        txtSoLuong.Size = new Size(480, 25);
        txtSoLuong.TabIndex = 13;
        txtChungLoai.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        txtChungLoai.Font = new Font("Segoe UI", 10F);
        txtChungLoai.Margin = new Padding(15, 8, 15, 8);
        txtChungLoai.Name = "txtChungLoai";
        txtChungLoai.Size = new Size(480, 25);
        txtChungLoai.TabIndex = 15;
        nudCap.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        nudCap.Font = new Font("Segoe UI", 10F);
        nudCap.Location = new Point(250, 399);
        nudCap.Margin = new Padding(15, 8, 15, 8);
        nudCap.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
        nudCap.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
        nudCap.Name = "nudCap";
        nudCap.Size = new Size(480, 25);
        nudCap.TabIndex = 17;
        nudCap.Value = new decimal(new int[] { 1, 0, 0, 0 });
        // 
        // panelButtons
        // 
        panelButtons.Anchor = AnchorStyles.Top;
        panelButtons.Controls.Add(btnLamMoi);
        panelButtons.Controls.Add(btnLuu);
        panelButtons.Location = new Point(95, 584);
        panelButtons.Name = "panelButtons";
        panelButtons.Size = new Size(760, 55);
        panelButtons.TabIndex = 3;
        // 
        // btnLamMoi
        // 
        btnLamMoi.BackColor = Color.FromArgb(27, 94, 60);
        btnLamMoi.FlatStyle = FlatStyle.Flat;
        btnLamMoi.ForeColor = Color.White;
        btnLamMoi.Location = new Point(615, 8);
        btnLamMoi.Name = "btnLamMoi";
        btnLamMoi.Size = new Size(115, 38);
        btnLamMoi.TabIndex = 1;
        btnLamMoi.Text = "Làm mới";
        btnLamMoi.UseVisualStyleBackColor = false;
        btnLamMoi.Click += btnLamMoi_Click;
        // 
        // btnLuu
        // 
        btnLuu.BackColor = Color.FromArgb(27, 94, 60);
        btnLuu.FlatStyle = FlatStyle.Flat;
        btnLuu.ForeColor = Color.White;
        btnLuu.Location = new Point(490, 8);
        btnLuu.Name = "btnLuu";
        btnLuu.Size = new Size(115, 38);
        btnLuu.TabIndex = 0;
        btnLuu.Text = "Thêm";
        btnLuu.UseVisualStyleBackColor = false;
        btnLuu.Click += btnLuu_Click;
        // 
        // UcThemMoi
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(242, 247, 244);
        Controls.Add(panelButtons);
        Controls.Add(tableForm);
        Controls.Add(lblSubtitle);
        Controls.Add(lblTitle);
        Name = "UcThemMoi";
        Size = new Size(950, 680);
        tableForm.ResumeLayout(false);
        tableForm.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)nudCap).EndInit();
        panelButtons.ResumeLayout(false);
        ResumeLayout(false);
    }
}
