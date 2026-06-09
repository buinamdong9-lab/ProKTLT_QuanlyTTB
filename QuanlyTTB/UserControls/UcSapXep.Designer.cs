namespace QuanlyTTB.UserControls;

partial class UcSapXep
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle;
    private Panel panelOptions;
    private Label lblThuatToan;
    private Label lblKhoa;
    private Label lblStatus;
    private ComboBox cboThuatToan;
    private ComboBox cboKhoa;
    private Button btnSapXep;

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
        DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
        DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
        lblTitle = new Label();
        panelOptions = new Panel();
        lblThuatToan = new Label();
        cboThuatToan = new ComboBox();
        lblKhoa = new Label();
        cboKhoa = new ComboBox();
        btnSapXep = new Button();
        lblStatus = new Label();
        panelPhanTrang = new Panel();
        lblViTri = new Label();
        lblTrang = new Label();
        btnTrangDau = new Button();
        btnTrangTruoc = new Button();
        btnTrangSau = new Button();
        btnTrangCuoi = new Button();
        gridKetQua = new DataGridView();
        colMaTTB = new DataGridViewTextBoxColumn();
        colSoHieu = new DataGridViewTextBoxColumn();
        colTenTTB = new DataGridViewTextBoxColumn();
        colNgaySanXuat = new DataGridViewTextBoxColumn();
        colNgaySuDung = new DataGridViewTextBoxColumn();
        colNguonCap = new DataGridViewTextBoxColumn();
        colSoLuong = new DataGridViewTextBoxColumn();
        colChungLoai = new DataGridViewTextBoxColumn();
        colCap = new DataGridViewTextBoxColumn();
        thietBiBindingSource = new BindingSource(components);
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
        lblTitle.Location = new Point(0, 0);
        lblTitle.Name = "lblTitle";
        lblTitle.Padding = new Padding(29, 24, 0, 0);
        lblTitle.Size = new Size(1086, 88);
        lblTitle.TabIndex = 0;
        lblTitle.Text = "M3 - SẮP XẾP DANH SÁCH";
        // 
        // panelOptions
        // 
        panelOptions.BackColor = Color.White;
        panelOptions.Controls.Add(lblThuatToan);
        panelOptions.Controls.Add(cboThuatToan);
        panelOptions.Controls.Add(lblKhoa);
        panelOptions.Controls.Add(cboKhoa);
        panelOptions.Controls.Add(btnSapXep);
        panelOptions.Controls.Add(lblStatus);
        panelOptions.Dock = DockStyle.Top;
        panelOptions.Location = new Point(0, 88);
        panelOptions.Margin = new Padding(3, 4, 3, 4);
        panelOptions.Name = "panelOptions";
        panelOptions.Size = new Size(1086, 149);
        panelOptions.TabIndex = 1;
        // 
        // lblThuatToan
        // 
        lblThuatToan.Location = new Point(29, 24);
        lblThuatToan.Name = "lblThuatToan";
        lblThuatToan.Size = new Size(103, 33);
        lblThuatToan.TabIndex = 0;
        lblThuatToan.Text = "Thuật toán:";
        // 
        // cboThuatToan
        // 
        cboThuatToan.DropDownStyle = ComboBoxStyle.DropDownList;
        cboThuatToan.FormattingEnabled = true;
        cboThuatToan.Items.AddRange(new object[] { "Selection Sort", "Insertion Sort", "Bubble Sort", "Quick Sort", "Merge Sort" });
        cboThuatToan.Location = new Point(131, 20);
        cboThuatToan.Margin = new Padding(3, 4, 3, 4);
        cboThuatToan.Name = "cboThuatToan";
        cboThuatToan.Size = new Size(205, 28);
        cboThuatToan.TabIndex = 1;
        // 
        // lblKhoa
        // 
        lblKhoa.Location = new Point(371, 24);
        lblKhoa.Name = "lblKhoa";
        lblKhoa.Size = new Size(57, 33);
        lblKhoa.TabIndex = 2;
        lblKhoa.Text = "Khóa:";
        // 
        // cboKhoa
        // 
        cboKhoa.DropDownStyle = ComboBoxStyle.DropDownList;
        cboKhoa.FormattingEnabled = true;
        cboKhoa.Items.AddRange(new object[] { "Mã TTB", "Tên TTB", "Ngày sản xuất", "Ngày đưa vào sử dụng", "Nguồn cấp", "Chủng loại", "Số lượng", "Cấp" });
        cboKhoa.Location = new Point(429, 20);
        cboKhoa.Margin = new Padding(3, 4, 3, 4);
        cboKhoa.Name = "cboKhoa";
        cboKhoa.Size = new Size(239, 28);
        cboKhoa.TabIndex = 3;
        // 
        // btnSapXep
        // 
        btnSapXep.BackColor = Color.FromArgb(27, 94, 60);
        btnSapXep.FlatStyle = FlatStyle.Flat;
        btnSapXep.ForeColor = Color.White;
        btnSapXep.Location = new Point(703, 16);
        btnSapXep.Margin = new Padding(3, 4, 3, 4);
        btnSapXep.Name = "btnSapXep";
        btnSapXep.Size = new Size(160, 48);
        btnSapXep.TabIndex = 4;
        btnSapXep.Text = "Thực hiện sắp xếp";
        btnSapXep.UseVisualStyleBackColor = false;
        btnSapXep.Click += btnSapXep_Click;
        // 
        // lblStatus
        // 
        lblStatus.AutoSize = true;
        lblStatus.ForeColor = Color.FromArgb(27, 94, 60);
        lblStatus.Location = new Point(29, 96);
        lblStatus.Name = "lblStatus";
        lblStatus.Size = new Size(228, 20);
        lblStatus.TabIndex = 5;
        lblStatus.Text = "Chọn thuật toán và khóa sắp xếp.";
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
        panelPhanTrang.Location = new Point(0, 832);
        panelPhanTrang.Margin = new Padding(3, 4, 3, 4);
        panelPhanTrang.Name = "panelPhanTrang";
        panelPhanTrang.Size = new Size(1086, 75);
        panelPhanTrang.TabIndex = 3;
        // 
        // lblViTri
        // 
        lblViTri.AutoSize = true;
        lblViTri.Location = new Point(29, 27);
        lblViTri.Name = "lblViTri";
        lblViTri.Size = new Size(109, 20);
        lblViTri.TabIndex = 0;
        lblViTri.Text = "Hiển thị 0-0 / 0";
        // 
        // lblTrang
        // 
        lblTrang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTrang.Location = new Point(740, 21);
        lblTrang.Name = "lblTrang";
        lblTrang.Size = new Size(96, 33);
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
        btnTrangDau.Location = new Point(511, 13);
        btnTrangDau.Margin = new Padding(3, 4, 3, 4);
        btnTrangDau.Name = "btnTrangDau";
        btnTrangDau.Size = new Size(101, 48);
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
        btnTrangTruoc.Location = new Point(618, 13);
        btnTrangTruoc.Margin = new Padding(3, 4, 3, 4);
        btnTrangTruoc.Name = "btnTrangTruoc";
        btnTrangTruoc.Size = new Size(103, 48);
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
        btnTrangSau.Location = new Point(853, 13);
        btnTrangSau.Margin = new Padding(3, 4, 3, 4);
        btnTrangSau.Name = "btnTrangSau";
        btnTrangSau.Size = new Size(97, 48);
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
        btnTrangCuoi.Location = new Point(957, 13);
        btnTrangCuoi.Margin = new Padding(3, 4, 3, 4);
        btnTrangCuoi.Name = "btnTrangCuoi";
        btnTrangCuoi.Size = new Size(101, 48);
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
        dataGridViewCellStyle1.BackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(27, 94, 60);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(27, 94, 60);
        gridKetQua.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        gridKetQua.ColumnHeadersHeight = 38;
        gridKetQua.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridKetQua.Columns.AddRange(new DataGridViewColumn[] { colMaTTB, colSoHieu, colTenTTB, colNgaySanXuat, colNgaySuDung, colNguonCap, colSoLuong, colChungLoai, colCap });
        gridKetQua.DataSource = thietBiBindingSource;
        gridKetQua.Dock = DockStyle.Fill;
        gridKetQua.EnableHeadersVisualStyles = false;
        gridKetQua.Location = new Point(0, 237);
        gridKetQua.Margin = new Padding(3, 4, 3, 4);
        gridKetQua.Name = "gridKetQua";
        gridKetQua.ReadOnly = true;
        gridKetQua.RowHeadersVisible = false;
        gridKetQua.RowHeadersWidth = 51;
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(18, 67, 42);
        gridKetQua.RowsDefaultCellStyle = dataGridViewCellStyle4;
        gridKetQua.RowTemplate.Height = 32;
        gridKetQua.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridKetQua.Size = new Size(1086, 595);
        gridKetQua.TabIndex = 2;
        // 
        // colMaTTB
        // 
        colMaTTB.DataPropertyName = "MaTTB";
        colMaTTB.FillWeight = 70F;
        colMaTTB.HeaderText = "Mã TTB";
        colMaTTB.MinimumWidth = 6;
        colMaTTB.Name = "colMaTTB";
        colMaTTB.ReadOnly = true;
        // 
        // colSoHieu
        // 
        colSoHieu.DataPropertyName = "SoHieu";
        colSoHieu.FillWeight = 70F;
        colSoHieu.HeaderText = "Số hiệu";
        colSoHieu.MinimumWidth = 6;
        colSoHieu.Name = "colSoHieu";
        colSoHieu.ReadOnly = true;
        // 
        // colTenTTB
        // 
        colTenTTB.DataPropertyName = "TenTTB";
        colTenTTB.FillWeight = 130F;
        colTenTTB.HeaderText = "Tên thiết bị";
        colTenTTB.MinimumWidth = 6;
        colTenTTB.Name = "colTenTTB";
        colTenTTB.ReadOnly = true;
        // 
        // colNgaySanXuat
        // 
        colNgaySanXuat.DataPropertyName = "NgaySanXuat";
        dataGridViewCellStyle2.Format = "dd/MM/yyyy";
        colNgaySanXuat.DefaultCellStyle = dataGridViewCellStyle2;
        colNgaySanXuat.FillWeight = 90F;
        colNgaySanXuat.HeaderText = "Ngày sản xuất";
        colNgaySanXuat.MinimumWidth = 6;
        colNgaySanXuat.Name = "colNgaySanXuat";
        colNgaySanXuat.ReadOnly = true;
        // 
        // colNgaySuDung
        // 
        colNgaySuDung.DataPropertyName = "NgayDuaVaoSuDung";
        dataGridViewCellStyle3.Format = "dd/MM/yyyy";
        colNgaySuDung.DefaultCellStyle = dataGridViewCellStyle3;
        colNgaySuDung.FillWeight = 90F;
        colNgaySuDung.HeaderText = "Ngày sử dụng";
        colNgaySuDung.MinimumWidth = 6;
        colNgaySuDung.Name = "colNgaySuDung";
        colNgaySuDung.ReadOnly = true;
        // 
        // colNguonCap
        // 
        colNguonCap.DataPropertyName = "NguonCap";
        colNguonCap.FillWeight = 115F;
        colNguonCap.HeaderText = "Nguồn cấp";
        colNguonCap.MinimumWidth = 6;
        colNguonCap.Name = "colNguonCap";
        colNguonCap.ReadOnly = true;
        // 
        // colSoLuong
        // 
        colSoLuong.DataPropertyName = "SoLuong";
        colSoLuong.FillWeight = 45F;
        colSoLuong.HeaderText = "SL";
        colSoLuong.MinimumWidth = 6;
        colSoLuong.Name = "colSoLuong";
        colSoLuong.ReadOnly = true;
        // 
        // colChungLoai
        // 
        colChungLoai.DataPropertyName = "ChungLoai";
        colChungLoai.FillWeight = 105F;
        colChungLoai.HeaderText = "Chủng loại";
        colChungLoai.MinimumWidth = 6;
        colChungLoai.Name = "colChungLoai";
        colChungLoai.ReadOnly = true;
        // 
        // colCap
        // 
        colCap.DataPropertyName = "Cap";
        colCap.FillWeight = 40F;
        colCap.HeaderText = "Cấp";
        colCap.MinimumWidth = 6;
        colCap.Name = "colCap";
        colCap.ReadOnly = true;
        // 
        // thietBiBindingSource
        // 
        thietBiBindingSource.DataSource = typeof(Models.ThietBi);
        // 
        // UcSapXep
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(gridKetQua);
        Controls.Add(panelPhanTrang);
        Controls.Add(panelOptions);
        Controls.Add(lblTitle);
        Margin = new Padding(3, 4, 3, 4);
        Name = "UcSapXep";
        Size = new Size(1086, 907);
        panelOptions.ResumeLayout(false);
        panelOptions.PerformLayout();
        panelPhanTrang.ResumeLayout(false);
        panelPhanTrang.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridKetQua).EndInit();
        ((System.ComponentModel.ISupportInitialize)thietBiBindingSource).EndInit();
        ResumeLayout(false);
    }
}
