namespace QuanlyTTB.UserControls;

partial class UcTimKiem
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle;
    private Panel panelOptions;
    private Label lblThuatToan;
    private Label lblKhoa;
    private Label lblTuKhoa;
    private Label lblStatus;
    private ComboBox cboThuatToan;
    private ComboBox cboKhoa;
    private TextBox txtTuKhoa;
    private Button btnTim;
    private Panel panelPhanTrang;
    private Label lblViTri;
    private Label lblTrang;
    private Button btnTrangDau;
    private Button btnTrangTruoc;
    private Button btnTrangSau;
    private Button btnTrangCuoi;
    private System.Windows.Forms.BindingSource thietBiBindingSource;
    private System.Windows.Forms.DataGridView gridKetQua;
    private System.Windows.Forms.DataGridViewTextBoxColumn colMaTTB;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSoHieu;
    private System.Windows.Forms.DataGridViewTextBoxColumn colTenTTB;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySanXuat;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNgaySuDung;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNguonCap;
    private System.Windows.Forms.DataGridViewTextBoxColumn colSoLuong;
    private System.Windows.Forms.DataGridViewTextBoxColumn colChungLoai;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCap;

    protected override void Dispose(bool disposing)
    {
        if (disposing && components != null)
            components.Dispose();
        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        lblTitle = new Label();
        panelOptions = new Panel();
        lblThuatToan = new Label();
        cboThuatToan = new ComboBox();
        lblKhoa = new Label();
        cboKhoa = new ComboBox();
        lblTuKhoa = new Label();
        txtTuKhoa = new TextBox();
        btnTim = new Button();
        lblStatus = new Label();
        panelPhanTrang = new Panel();
        lblViTri = new Label();
        lblTrang = new Label();
        btnTrangDau = new Button();
        btnTrangTruoc = new Button();
        btnTrangSau = new Button();
        btnTrangCuoi = new Button();
        gridKetQua = new System.Windows.Forms.DataGridView();
        thietBiBindingSource = new System.Windows.Forms.BindingSource(components);
        colMaTTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colSoHieu = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colTenTTB = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colNgaySanXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colNgaySuDung = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colNguonCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colChungLoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colCap = new System.Windows.Forms.DataGridViewTextBoxColumn();
        panelOptions.SuspendLayout();
        panelPhanTrang.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridKetQua).BeginInit();
        ((System.ComponentModel.ISupportInitialize)thietBiBindingSource).BeginInit();
        SuspendLayout();
        // 
        // lblTitle
        // 
        lblTitle.BackColor = Color.White;
        lblTitle.Dock = DockStyle.Top;
        lblTitle.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
        lblTitle.ForeColor = Color.FromArgb(27, 94, 60);
        lblTitle.Name = "lblTitle";
        lblTitle.Padding = new Padding(25, 18, 0, 0);
        lblTitle.Size = new Size(950, 66);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "M4 - TÌM KIẾM TRANG THIẾT BỊ";
        // 
        // panelOptions
        // 
        panelOptions.BackColor = Color.White;
        panelOptions.Controls.Add(lblThuatToan);
        panelOptions.Controls.Add(cboThuatToan);
        panelOptions.Controls.Add(lblKhoa);
        panelOptions.Controls.Add(cboKhoa);
        panelOptions.Controls.Add(lblTuKhoa);
        panelOptions.Controls.Add(txtTuKhoa);
        panelOptions.Controls.Add(btnTim);
        panelOptions.Controls.Add(lblStatus);
        panelOptions.Dock = DockStyle.Top;
        panelOptions.Location = new Point(0, 66);
        panelOptions.Name = "panelOptions";
        panelOptions.Size = new Size(950, 142);
        panelOptions.TabIndex = 1;
        // 
        // lblThuatToan
        // 
        lblThuatToan.Location = new Point(25, 18);
        lblThuatToan.Name = "lblThuatToan";
        lblThuatToan.Size = new Size(90, 25);
        lblThuatToan.TabIndex = 0;
        lblThuatToan.Text = "Thuật toán:";
        // 
        // cboThuatToan
        // 
        cboThuatToan.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThuatToan.FormattingEnabled = true;
        cboThuatToan.Items.AddRange(new object[] { "Tìm kiếm tuần tự", "Tìm kiếm nhị phân" });
        cboThuatToan.Location = new Point(115, 15);
        cboThuatToan.Name = "cboThuatToan";
        cboThuatToan.Size = new Size(190, 23);
        cboThuatToan.TabIndex = 1;
        cboThuatToan.SelectedIndexChanged += cboThuatToan_SelectedIndexChanged;
        // 
        // lblKhoa
        // 
        lblKhoa.Location = new Point(335, 18);
        lblKhoa.Name = "lblKhoa";
        lblKhoa.Size = new Size(45, 25);
        lblKhoa.TabIndex = 2;
        lblKhoa.Text = "Khóa:";
        // 
        // cboKhoa
        // 
        cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhoa.FormattingEnabled = true;
        cboKhoa.Items.AddRange(new object[] { "Mã TTB", "Tên TTB", "Ngày sản xuất", "Ngày đưa vào sử dụng", "Nguồn cấp", "Chủng loại", "Số lượng", "Cấp" });
        cboKhoa.Location = new Point(385, 15);
        cboKhoa.Name = "cboKhoa";
        cboKhoa.Size = new Size(170, 23);
        cboKhoa.TabIndex = 3;
        // 
        // lblTuKhoa
        // 
        lblTuKhoa.Location = new Point(25, 66);
        lblTuKhoa.Name = "lblTuKhoa";
        lblTuKhoa.Size = new Size(90, 25);
        lblTuKhoa.TabIndex = 4;
        lblTuKhoa.Text = "Từ khóa:";
        // 
        // txtTuKhoa
        // 
        txtTuKhoa.Location = new Point(115, 63);
        txtTuKhoa.Name = "txtTuKhoa";
        txtTuKhoa.Size = new Size(440, 23);
        txtTuKhoa.TabIndex = 5;
        // 
        // btnTim
        // 
        btnTim.BackColor = Color.FromArgb(27, 94, 60);
        btnTim.FlatStyle = FlatStyle.Flat;
        btnTim.ForeColor = Color.White;
        btnTim.Location = new Point(585, 58);
        btnTim.Name = "btnTim";
        btnTim.Size = new Size(120, 36);
        btnTim.TabIndex = 6;
        btnTim.Text = "Tìm kiếm";
        btnTim.UseVisualStyleBackColor = false;
        btnTim.Click += btnTim_Click;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.ForeColor = Color.DimGray;
        lblStatus.Location = new Point(25, 112);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(170, 15);
        lblStatus.TabIndex = 7;
        lblStatus.Text = "Nhập từ khóa để tìm kiếm.";
        // 
        // panelPhanTrang
        // 
        panelPhanTrang.BackColor = Color.White;
        panelPhanTrang.Controls.Add(lblViTri);
        panelPhanTrang.Controls.Add(lblTrang);
        panelPhanTrang.Controls.Add(btnTrangDau);
        panelPhanTrang.Controls.Add(btnTrangTruoc);
        panelPhanTrang.Controls.Add(btnTrangSau);
        panelPhanTrang.Controls.Add(btnTrangCuoi);
        panelPhanTrang.Dock = DockStyle.Bottom;
        panelPhanTrang.Location = new Point(0, 624);
        panelPhanTrang.Name = "panelPhanTrang";
        panelPhanTrang.Size = new Size(950, 56);
        panelPhanTrang.TabIndex = 3;
        // 
        // lblViTri
        // 
        lblViTri.AutoSize = true;
        lblViTri.Location = new Point(25, 20);
        lblViTri.Name = "lblViTri";
        lblViTri.Size = new Size(110, 15);
        lblViTri.TabIndex = 0;
        lblViTri.Text = "Hiển thị 0-0 / 0";
        // 
        // lblTrang
        // 
        lblTrang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTrang.Location = new Point(664, 16);
        lblTrang.Name = "lblTrang";
        lblTrang.Size = new Size(76, 25);
        lblTrang.TabIndex = 3;
        lblTrang.Text = "Trang 1 / 1";
        lblTrang.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnTrangDau
        // 
        btnTrangDau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTrangDau.BackColor = Color.FromArgb(27, 94, 60);
        btnTrangDau.FlatStyle = FlatStyle.Flat;
        btnTrangDau.ForeColor = Color.White;
        btnTrangDau.Location = new Point(474, 10);
        btnTrangDau.Name = "btnTrangDau";
        btnTrangDau.Size = new Size(88, 36);
        btnTrangDau.TabIndex = 1;
        btnTrangDau.Text = "Trang đầu";
        btnTrangDau.UseVisualStyleBackColor = false;
        btnTrangDau.Click += btnTrangDau_Click;
        // 
        // btnTrangTruoc
        // 
        btnTrangTruoc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTrangTruoc.BackColor = Color.FromArgb(27, 94, 60);
        btnTrangTruoc.FlatStyle = FlatStyle.Flat;
        btnTrangTruoc.ForeColor = Color.White;
        btnTrangTruoc.Location = new Point(568, 10);
        btnTrangTruoc.Name = "btnTrangTruoc";
        btnTrangTruoc.Size = new Size(90, 36);
        btnTrangTruoc.TabIndex = 2;
        btnTrangTruoc.Text = "Trang trước";
        btnTrangTruoc.UseVisualStyleBackColor = false;
        btnTrangTruoc.Click += btnTrangTruoc_Click;
        // 
        // btnTrangSau
        // 
        btnTrangSau.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTrangSau.BackColor = Color.FromArgb(27, 94, 60);
        btnTrangSau.FlatStyle = FlatStyle.Flat;
        btnTrangSau.ForeColor = Color.White;
        btnTrangSau.Location = new Point(746, 10);
        btnTrangSau.Name = "btnTrangSau";
        btnTrangSau.Size = new Size(85, 36);
        btnTrangSau.TabIndex = 4;
        btnTrangSau.Text = "Trang sau";
        btnTrangSau.UseVisualStyleBackColor = false;
        btnTrangSau.Click += btnTrangSau_Click;
        // 
        // btnTrangCuoi
        // 
        btnTrangCuoi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTrangCuoi.BackColor = Color.FromArgb(27, 94, 60);
        btnTrangCuoi.FlatStyle = FlatStyle.Flat;
        btnTrangCuoi.ForeColor = Color.White;
        btnTrangCuoi.Location = new Point(837, 10);
        btnTrangCuoi.Name = "btnTrangCuoi";
        btnTrangCuoi.Size = new Size(88, 36);
        btnTrangCuoi.TabIndex = 5;
        btnTrangCuoi.Text = "Trang cuối";
        btnTrangCuoi.UseVisualStyleBackColor = false;
        btnTrangCuoi.Click += btnTrangCuoi_Click;
        // 
        // gridKetQua
        // 
        gridKetQua.AllowUserToAddRows = false;
        gridKetQua.AutoGenerateColumns = false;
        gridKetQua.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridKetQua.BackgroundColor = Color.White;
        gridKetQua.BorderStyle = BorderStyle.None;
        gridKetQua.ColumnHeadersHeight = 38;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(27, 94, 60);
        dataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle3.SelectionForeColor = Color.FromArgb(27, 94, 60);
        gridKetQua.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
        gridKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridKetQua.ColumnHeadersVisible = true;
        gridKetQua.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colMaTTB, colSoHieu, colTenTTB, colNgaySanXuat, colNgaySuDung, colNguonCap, colSoLuong, colChungLoai, colCap });
        gridKetQua.DataSource = thietBiBindingSource;
        gridKetQua.Dock = DockStyle.Fill;
        gridKetQua.EnableHeadersVisualStyles = false;
        gridKetQua.Location = new Point(0, 208);
        gridKetQua.Name = "gridKetQua";
        gridKetQua.ReadOnly = true;
        gridKetQua.RowHeadersVisible = false;
        gridKetQua.RowTemplate.Height = 32;
        gridKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(18, 67, 42);
        gridKetQua.RowsDefaultCellStyle = dataGridViewCellStyle4;
        gridKetQua.Size = new Size(950, 416);
        gridKetQua.TabIndex = 2;
        // 
        // columns
        // 
        colMaTTB.DataPropertyName = "MaTTB";
        colMaTTB.FillWeight = 70F;
        colMaTTB.HeaderText = "Mã TTB";
        colMaTTB.Name = "colMaTTB";
        colMaTTB.ReadOnly = true;
        colSoHieu.DataPropertyName = "SoHieu";
        colSoHieu.FillWeight = 70F;
        colSoHieu.HeaderText = "Số hiệu";
        colSoHieu.Name = "colSoHieu";
        colSoHieu.ReadOnly = true;
        colTenTTB.DataPropertyName = "TenTTB";
        colTenTTB.FillWeight = 130F;
        colTenTTB.HeaderText = "Tên thiết bị";
        colTenTTB.Name = "colTenTTB";
        colTenTTB.ReadOnly = true;
        colNgaySanXuat.DataPropertyName = "NgaySanXuat";
        dataGridViewCellStyle1.Format = "dd/MM/yyyy";
        colNgaySanXuat.DefaultCellStyle = dataGridViewCellStyle1;
        colNgaySanXuat.FillWeight = 90F;
        colNgaySanXuat.HeaderText = "Ngày sản xuất";
        colNgaySanXuat.Name = "colNgaySanXuat";
        colNgaySanXuat.ReadOnly = true;
        colNgaySuDung.DataPropertyName = "NgayDuaVaoSuDung";
        dataGridViewCellStyle2.Format = "dd/MM/yyyy";
        colNgaySuDung.DefaultCellStyle = dataGridViewCellStyle2;
        colNgaySuDung.FillWeight = 90F;
        colNgaySuDung.HeaderText = "Ngày sử dụng";
        colNgaySuDung.Name = "colNgaySuDung";
        colNgaySuDung.ReadOnly = true;
        colNguonCap.DataPropertyName = "NguonCap";
        colNguonCap.FillWeight = 115F;
        colNguonCap.HeaderText = "Nguồn cấp";
        colNguonCap.Name = "colNguonCap";
        colNguonCap.ReadOnly = true;
        colSoLuong.DataPropertyName = "SoLuong";
        colSoLuong.FillWeight = 45F;
        colSoLuong.HeaderText = "SL";
        colSoLuong.Name = "colSoLuong";
        colSoLuong.ReadOnly = true;
        colChungLoai.DataPropertyName = "ChungLoai";
        colChungLoai.FillWeight = 105F;
        colChungLoai.HeaderText = "Chủng loại";
        colChungLoai.Name = "colChungLoai";
        colChungLoai.ReadOnly = true;
        colCap.DataPropertyName = "Cap";
        colCap.FillWeight = 40F;
        colCap.HeaderText = "Cấp";
        colCap.Name = "colCap";
        colCap.ReadOnly = true;
        // 
        // thietBiBindingSource
        // 
        thietBiBindingSource.DataSource = typeof(Models.ThietBi);
        // 
        // UcTimKiem
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(gridKetQua);
        Controls.Add(panelPhanTrang);
        Controls.Add(panelOptions);
        Controls.Add(lblTitle);
        Name = "UcTimKiem";
        Size = new Size(950, 680);
        panelOptions.ResumeLayout(false);
        panelOptions.PerformLayout();
        panelPhanTrang.ResumeLayout(false);
        panelPhanTrang.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridKetQua).EndInit();
        ((System.ComponentModel.ISupportInitialize)thietBiBindingSource).EndInit();
        ResumeLayout(false);
    }
}
