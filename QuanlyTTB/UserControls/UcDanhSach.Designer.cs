namespace QuanlyTTB.UserControls;

partial class UcDanhSach
{
    private System.ComponentModel.IContainer components = null!;
    private System.Windows.Forms.BindingSource thietBiBindingSource;
    private Label lblTitle;
    private Label lblCount;
    private Panel panelActions;
    private Button btnTaiLai;
    private Button btnSua;
    private Button btnXoa;
    private Button btnSaoLuu;
    private Button btnKhoiPhuc;
    private Button btnXuatExcel;
    private Panel panelPhanTrang;
    private Label lblViTri;
    private Label lblTrang;
    private Button btnTrangDau;
    private Button btnTrangTruoc;
    private Button btnTrangSau;
    private Button btnTrangCuoi;
    private System.Windows.Forms.DataGridView gridDanhSach;
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
        panelActions = new Panel();
        lblCount = new Label();
        btnTaiLai = new Button();
        btnSua = new Button();
        btnXoa = new Button();
        btnSaoLuu = new Button();
        btnKhoiPhuc = new Button();
        btnXuatExcel = new Button();
        panelPhanTrang = new Panel();
        lblViTri = new Label();
        lblTrang = new Label();
        btnTrangDau = new Button();
        btnTrangTruoc = new Button();
        btnTrangSau = new Button();
        btnTrangCuoi = new Button();
        gridDanhSach = new DataGridView();
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
        panelActions.SuspendLayout();
        panelPhanTrang.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridDanhSach).BeginInit();
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
        lblTitle.Text = "M2 - DANH SÁCH TRANG THIẾT BỊ";
        // 
        // panelActions
        // 
        panelActions.BackColor = Color.White;
        panelActions.Controls.Add(lblCount);
        panelActions.Controls.Add(btnTaiLai);
        panelActions.Controls.Add(btnSua);
        panelActions.Controls.Add(btnXoa);
        panelActions.Controls.Add(btnSaoLuu);
        panelActions.Controls.Add(btnKhoiPhuc);
        panelActions.Controls.Add(btnXuatExcel);
        panelActions.Dock = DockStyle.Top;
        panelActions.Location = new Point(0, 88);
        panelActions.Margin = new Padding(3, 4, 3, 4);
        panelActions.Name = "panelActions";
        panelActions.Size = new Size(1086, 142);
        panelActions.TabIndex = 1;
        // 
        // lblCount
        // 
        lblCount.AutoEllipsis = true;
        lblCount.Location = new Point(29, 20);
        lblCount.Name = "lblCount";
        lblCount.Size = new Size(585, 45);
        lblCount.TabIndex = 0;
        lblCount.Text = "Tổng số hồ sơ: 0";
        lblCount.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // btnTaiLai
        // 
        btnTaiLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTaiLai.BackColor = Color.FromArgb(27, 94, 60);
        btnTaiLai.FlatStyle = FlatStyle.Flat;
        btnTaiLai.ForeColor = Color.White;
        btnTaiLai.Location = new Point(646, 17);
        btnTaiLai.Margin = new Padding(3, 4, 3, 4);
        btnTaiLai.Name = "btnTaiLai";
        btnTaiLai.Size = new Size(114, 48);
        btnTaiLai.TabIndex = 1;
        btnTaiLai.Text = "Tải lại";
        btnTaiLai.UseVisualStyleBackColor = false;
        btnTaiLai.Click += btnTaiLai_Click;
        // 
        // btnSua
        // 
        btnSua.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSua.BackColor = Color.Cornsilk;
        btnSua.FlatStyle = FlatStyle.Flat;
        btnSua.ForeColor = Color.Black;
        btnSua.Location = new Point(771, 17);
        btnSua.Margin = new Padding(3, 4, 3, 4);
        btnSua.Name = "btnSua";
        btnSua.Size = new Size(131, 48);
        btnSua.TabIndex = 2;
        btnSua.Text = "Sửa bản ghi";
        btnSua.UseVisualStyleBackColor = false;
        btnSua.Click += btnSua_Click;
        // 
        // btnXoa
        // 
        btnXoa.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnXoa.BackColor = Color.Red;
        btnXoa.FlatStyle = FlatStyle.Flat;
        btnXoa.ForeColor = Color.White;
        btnXoa.Location = new Point(908, 18);
        btnXoa.Margin = new Padding(3, 4, 3, 4);
        btnXoa.Name = "btnXoa";
        btnXoa.Size = new Size(143, 48);
        btnXoa.TabIndex = 3;
        btnXoa.Text = "Xóa bản ghi";
        btnXoa.UseVisualStyleBackColor = false;
        btnXoa.Click += btnXoa_Click;
        // 
        // btnSaoLuu
        // 
        btnSaoLuu.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnSaoLuu.BackColor = Color.FromArgb(27, 94, 60);
        btnSaoLuu.FlatStyle = FlatStyle.Flat;
        btnSaoLuu.ForeColor = Color.White;
        btnSaoLuu.Location = new Point(646, 78);
        btnSaoLuu.Margin = new Padding(3, 4, 3, 4);
        btnSaoLuu.Name = "btnSaoLuu";
        btnSaoLuu.Size = new Size(114, 48);
        btnSaoLuu.TabIndex = 4;
        btnSaoLuu.Text = "Sao lưu";
        btnSaoLuu.UseVisualStyleBackColor = false;
        btnSaoLuu.Click += btnSaoLuu_Click;
        // 
        // btnKhoiPhuc
        // 
        btnKhoiPhuc.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnKhoiPhuc.BackColor = Color.FromArgb(27, 94, 60);
        btnKhoiPhuc.FlatStyle = FlatStyle.Flat;
        btnKhoiPhuc.ForeColor = Color.White;
        btnKhoiPhuc.Location = new Point(771, 78);
        btnKhoiPhuc.Margin = new Padding(3, 4, 3, 4);
        btnKhoiPhuc.Name = "btnKhoiPhuc";
        btnKhoiPhuc.Size = new Size(131, 48);
        btnKhoiPhuc.TabIndex = 5;
        btnKhoiPhuc.Text = "Khôi phục";
        btnKhoiPhuc.UseVisualStyleBackColor = false;
        btnKhoiPhuc.Click += btnKhoiPhuc_Click;
        // 
        // btnXuatExcel
        // 
        btnXuatExcel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnXuatExcel.BackColor = Color.FromArgb(27, 94, 60);
        btnXuatExcel.FlatStyle = FlatStyle.Flat;
        btnXuatExcel.ForeColor = Color.White;
        btnXuatExcel.Location = new Point(908, 78);
        btnXuatExcel.Name = "btnXuatExcel";
        btnXuatExcel.Size = new Size(143, 48);
        btnXuatExcel.TabIndex = 6;
        btnXuatExcel.Text = "Xuất CSV";
        btnXuatExcel.UseVisualStyleBackColor = false;
        btnXuatExcel.Click += btnXuatExcel_Click;
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
        panelPhanTrang.Location = new Point(0, 847);
        panelPhanTrang.Name = "panelPhanTrang";
        panelPhanTrang.Size = new Size(1086, 60);
        panelPhanTrang.TabIndex = 3;
        // 
        // lblViTri
        // 
        lblViTri.AutoSize = true;
        lblViTri.Location = new Point(29, 20);
        lblViTri.Name = "lblViTri";
        lblViTri.Size = new Size(109, 20);
        lblViTri.TabIndex = 0;
        lblViTri.Text = "Hiển thị 0-0 / 0";
        // 
        // lblTrang
        // 
        lblTrang.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        lblTrang.Location = new Point(771, 18);
        lblTrang.Name = "lblTrang";
        lblTrang.Size = new Size(99, 25);
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
        btnTrangDau.Location = new Point(570, 12);
        btnTrangDau.Name = "btnTrangDau";
        btnTrangDau.Size = new Size(95, 36);
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
        btnTrangTruoc.Location = new Point(671, 12);
        btnTrangTruoc.Name = "btnTrangTruoc";
        btnTrangTruoc.Size = new Size(99, 36);
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
        btnTrangSau.Location = new Point(872, 12);
        btnTrangSau.Name = "btnTrangSau";
        btnTrangSau.Size = new Size(92, 36);
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
        btnTrangCuoi.Location = new Point(970, 12);
        btnTrangCuoi.Name = "btnTrangCuoi";
        btnTrangCuoi.Size = new Size(95, 36);
        btnTrangCuoi.TabIndex = 5;
        btnTrangCuoi.Text = "Trang cuối";
        btnTrangCuoi.UseVisualStyleBackColor = false;
        btnTrangCuoi.Click += btnTrangCuoi_Click;
        // 
        // gridDanhSach
        // 
        gridDanhSach.AllowUserToAddRows = false;
        gridDanhSach.AllowUserToDeleteRows = false;
        gridDanhSach.AutoGenerateColumns = false;
        gridDanhSach.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridDanhSach.BackgroundColor = Color.White;
        gridDanhSach.BorderStyle = BorderStyle.None;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(27, 94, 60);
        dataGridViewCellStyle1.SelectionBackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle1.SelectionForeColor = Color.FromArgb(27, 94, 60);
        gridDanhSach.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        gridDanhSach.ColumnHeadersHeight = 38;
        gridDanhSach.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridDanhSach.Columns.AddRange(new DataGridViewColumn[] { colMaTTB, colSoHieu, colTenTTB, colNgaySanXuat, colNgaySuDung, colNguonCap, colSoLuong, colChungLoai, colCap });
        gridDanhSach.DataSource = thietBiBindingSource;
        gridDanhSach.Dock = DockStyle.Fill;
        gridDanhSach.EnableHeadersVisualStyles = false;
        gridDanhSach.Location = new Point(0, 230);
        gridDanhSach.Margin = new Padding(3, 4, 3, 4);
        gridDanhSach.MultiSelect = false;
        gridDanhSach.Name = "gridDanhSach";
        gridDanhSach.ReadOnly = true;
        gridDanhSach.RowHeadersVisible = false;
        gridDanhSach.RowHeadersWidth = 51;
        dataGridViewCellStyle4.SelectionBackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle4.SelectionForeColor = Color.FromArgb(18, 67, 42);
        gridDanhSach.RowsDefaultCellStyle = dataGridViewCellStyle4;
        gridDanhSach.RowTemplate.Height = 32;
        gridDanhSach.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        gridDanhSach.Size = new Size(1086, 617);
        gridDanhSach.TabIndex = 2;
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
        colSoLuong.FillWeight = 55F;
        colSoLuong.HeaderText = "Số lượng";
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
        // UcDanhSach
        // 
        AutoScaleDimensions = new SizeF(8F, 20F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(gridDanhSach);
        Controls.Add(panelPhanTrang);
        Controls.Add(panelActions);
        Controls.Add(lblTitle);
        Margin = new Padding(3, 4, 3, 4);
        Name = "UcDanhSach";
        Size = new Size(1086, 907);
        panelActions.ResumeLayout(false);
        panelPhanTrang.ResumeLayout(false);
        panelPhanTrang.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)gridDanhSach).EndInit();
        ((System.ComponentModel.ISupportInitialize)thietBiBindingSource).EndInit();
        ResumeLayout(false);
    }
}
