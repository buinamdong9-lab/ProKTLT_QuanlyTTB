namespace QuanlyTTB.UserControls;

partial class UcThongKe
{
    private System.ComponentModel.IContainer components = null!;
    private Label lblTitle;
    private Panel panelSummary;
    private Label lblTongLoaiCaption;
    private Label lblTongLoaiValue;
    private Label lblTongSoLuongCaption;
    private Label lblTongSoLuongValue;
    private Button btnTaiLai;
    private TableLayoutPanel tableStats;
    private GroupBox groupCap;
    private GroupBox groupChungLoai;
    private GroupBox groupNguonCap;
    private System.Windows.Forms.BindingSource capBindingSource;
    private System.Windows.Forms.BindingSource chungLoaiBindingSource;
    private System.Windows.Forms.BindingSource nguonCapBindingSource;
    private System.Windows.Forms.DataGridView gridCap;
    private System.Windows.Forms.DataGridView gridChungLoai;
    private System.Windows.Forms.DataGridView gridNguonCap;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCapNhom;
    private System.Windows.Forms.DataGridViewTextBoxColumn colCapSoLuong;
    private System.Windows.Forms.DataGridViewTextBoxColumn colChungLoaiNhom;
    private System.Windows.Forms.DataGridViewTextBoxColumn colChungLoaiSoLuong;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNguonCapNhom;
    private System.Windows.Forms.DataGridViewTextBoxColumn colNguonCapSoLuong;

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
        lblTitle = new Label();
        panelSummary = new Panel();
        lblTongLoaiCaption = new Label();
        lblTongLoaiValue = new Label();
        lblTongSoLuongCaption = new Label();
        lblTongSoLuongValue = new Label();
        btnTaiLai = new Button();
        tableStats = new TableLayoutPanel();
        groupCap = new GroupBox();
        gridCap = new System.Windows.Forms.DataGridView();
        capBindingSource = new System.Windows.Forms.BindingSource(components);
        colCapNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colCapSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
        groupChungLoai = new GroupBox();
        gridChungLoai = new System.Windows.Forms.DataGridView();
        chungLoaiBindingSource = new System.Windows.Forms.BindingSource(components);
        colChungLoaiNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colChungLoaiSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
        groupNguonCap = new GroupBox();
        gridNguonCap = new System.Windows.Forms.DataGridView();
        nguonCapBindingSource = new System.Windows.Forms.BindingSource(components);
        colNguonCapNhom = new System.Windows.Forms.DataGridViewTextBoxColumn();
        colNguonCapSoLuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
        panelSummary.SuspendLayout();
        tableStats.SuspendLayout();
        groupCap.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridCap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)capBindingSource).BeginInit();
        groupChungLoai.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridChungLoai).BeginInit();
        ((System.ComponentModel.ISupportInitialize)chungLoaiBindingSource).BeginInit();
        groupNguonCap.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)gridNguonCap).BeginInit();
        ((System.ComponentModel.ISupportInitialize)nguonCapBindingSource).BeginInit();
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
        lblTitle.Text = "M5 - THỐNG KÊ TRANG THIẾT BỊ";
        // 
        // panelSummary
        // 
        panelSummary.BackColor = Color.White;
        panelSummary.Controls.Add(lblTongLoaiCaption);
        panelSummary.Controls.Add(lblTongLoaiValue);
        panelSummary.Controls.Add(lblTongSoLuongCaption);
        panelSummary.Controls.Add(lblTongSoLuongValue);
        panelSummary.Controls.Add(btnTaiLai);
        panelSummary.Dock = DockStyle.Top;
        panelSummary.Location = new Point(0, 66);
        panelSummary.Name = "panelSummary";
        panelSummary.Size = new Size(950, 115);
        panelSummary.TabIndex = 1;
        // 
        // lblTongLoaiCaption
        // 
        lblTongLoaiCaption.Location = new Point(40, 24);
        lblTongLoaiCaption.Name = "lblTongLoaiCaption";
        lblTongLoaiCaption.Size = new Size(150, 24);
        lblTongLoaiCaption.TabIndex = 0;
        lblTongLoaiCaption.Text = "Tổng số loại thiết bị";
        // 
        // lblTongLoaiValue
        // 
        lblTongLoaiValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTongLoaiValue.ForeColor = Color.FromArgb(27, 94, 60);
        lblTongLoaiValue.Location = new Point(40, 48);
        lblTongLoaiValue.Name = "lblTongLoaiValue";
        lblTongLoaiValue.Size = new Size(150, 48);
        lblTongLoaiValue.TabIndex = 1;
        lblTongLoaiValue.Text = "0";
        // 
        // lblTongSoLuongCaption
        // 
        lblTongSoLuongCaption.Location = new Point(260, 24);
        lblTongSoLuongCaption.Name = "lblTongSoLuongCaption";
        lblTongSoLuongCaption.Size = new Size(170, 24);
        lblTongSoLuongCaption.TabIndex = 2;
        lblTongSoLuongCaption.Text = "Tổng số lượng thiết bị";
        // 
        // lblTongSoLuongValue
        // 
        lblTongSoLuongValue.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
        lblTongSoLuongValue.ForeColor = Color.FromArgb(27, 94, 60);
        lblTongSoLuongValue.Location = new Point(260, 48);
        lblTongSoLuongValue.Name = "lblTongSoLuongValue";
        lblTongSoLuongValue.Size = new Size(170, 48);
        lblTongSoLuongValue.TabIndex = 3;
        lblTongSoLuongValue.Text = "0";
        // 
        // btnTaiLai
        // 
        btnTaiLai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        btnTaiLai.BackColor = Color.FromArgb(27, 94, 60);
        btnTaiLai.FlatStyle = FlatStyle.Flat;
        btnTaiLai.ForeColor = Color.White;
        btnTaiLai.Location = new Point(800, 38);
        btnTaiLai.Name = "btnTaiLai";
        btnTaiLai.Size = new Size(110, 38);
        btnTaiLai.TabIndex = 4;
        btnTaiLai.Text = "Tải lại";
        btnTaiLai.UseVisualStyleBackColor = false;
        btnTaiLai.Click += btnTaiLai_Click;
        // 
        // tableStats
        // 
        tableStats.BackColor = Color.FromArgb(242, 247, 244);
        tableStats.ColumnCount = 3;
        tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
        tableStats.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
        tableStats.Controls.Add(groupCap, 0, 0);
        tableStats.Controls.Add(groupChungLoai, 1, 0);
        tableStats.Controls.Add(groupNguonCap, 2, 0);
        tableStats.Dock = DockStyle.Fill;
        tableStats.Location = new Point(0, 181);
        tableStats.Name = "tableStats";
        tableStats.Padding = new Padding(20);
        tableStats.RowCount = 1;
        tableStats.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        tableStats.Size = new Size(950, 499);
        tableStats.TabIndex = 2;
        // 
        // groupCap
        // 
        groupCap.Controls.Add(gridCap);
        groupCap.Dock = DockStyle.Fill;
        groupCap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupCap.Location = new Point(28, 28);
        groupCap.Margin = new Padding(8);
        groupCap.Name = "groupCap";
        groupCap.Padding = new Padding(3);
        groupCap.Size = new Size(287, 443);
        groupCap.TabIndex = 0;
        groupCap.TabStop = false;
        groupCap.Text = "Số lượng theo cấp";
        // 
        // gridCap
        // 
        gridCap.AllowUserToAddRows = false;
        gridCap.AutoGenerateColumns = false;
        gridCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridCap.BackgroundColor = Color.White;
        gridCap.BorderStyle = BorderStyle.None;
        gridCap.ColumnHeadersHeight = 36;
        dataGridViewCellStyle1.BackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle1.ForeColor = Color.FromArgb(27, 94, 60);
        gridCap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
        gridCap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridCap.ColumnHeadersVisible = true;
        gridCap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colCapNhom, colCapSoLuong });
        gridCap.DataSource = capBindingSource;
        gridCap.Dock = DockStyle.Fill;
        gridCap.EnableHeadersVisualStyles = false;
        gridCap.Font = new Font("Segoe UI", 9F);
        gridCap.Location = new Point(3, 21);
        gridCap.Name = "gridCap";
        gridCap.ReadOnly = true;
        gridCap.RowHeadersVisible = false;
        gridCap.RowTemplate.Height = 30;
        gridCap.Size = new Size(281, 419);
        gridCap.TabIndex = 0;
        colCapNhom.DataPropertyName = "Nhom";
        colCapNhom.FillWeight = 70F;
        colCapNhom.HeaderText = "Nhóm";
        colCapNhom.Name = "colCapNhom";
        colCapNhom.ReadOnly = true;
        colCapSoLuong.DataPropertyName = "SoLuong";
        colCapSoLuong.FillWeight = 30F;
        colCapSoLuong.HeaderText = "Số lượng";
        colCapSoLuong.Name = "colCapSoLuong";
        colCapSoLuong.ReadOnly = true;
        // 
        // capBindingSource
        // 
        capBindingSource.DataSource = typeof(UcThongKe.ThongKeDong);
        // 
        // groupChungLoai
        // 
        groupChungLoai.Controls.Add(gridChungLoai);
        groupChungLoai.Dock = DockStyle.Fill;
        groupChungLoai.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupChungLoai.Location = new Point(331, 28);
        groupChungLoai.Margin = new Padding(8);
        groupChungLoai.Name = "groupChungLoai";
        groupChungLoai.Padding = new Padding(3);
        groupChungLoai.Size = new Size(287, 443);
        groupChungLoai.TabIndex = 1;
        groupChungLoai.TabStop = false;
        groupChungLoai.Text = "Số lượng theo chủng loại";
        // 
        // gridChungLoai
        // 
        gridChungLoai.AllowUserToAddRows = false;
        gridChungLoai.AutoGenerateColumns = false;
        gridChungLoai.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridChungLoai.BackgroundColor = Color.White;
        gridChungLoai.BorderStyle = BorderStyle.None;
        gridChungLoai.ColumnHeadersHeight = 36;
        dataGridViewCellStyle2.BackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle2.ForeColor = Color.FromArgb(27, 94, 60);
        gridChungLoai.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
        gridChungLoai.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridChungLoai.ColumnHeadersVisible = true;
        gridChungLoai.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colChungLoaiNhom, colChungLoaiSoLuong });
        gridChungLoai.DataSource = chungLoaiBindingSource;
        gridChungLoai.Dock = DockStyle.Fill;
        gridChungLoai.EnableHeadersVisualStyles = false;
        gridChungLoai.Font = new Font("Segoe UI", 9F);
        gridChungLoai.Location = new Point(3, 21);
        gridChungLoai.Name = "gridChungLoai";
        gridChungLoai.ReadOnly = true;
        gridChungLoai.RowHeadersVisible = false;
        gridChungLoai.RowTemplate.Height = 30;
        gridChungLoai.Size = new Size(281, 419);
        gridChungLoai.TabIndex = 0;
        colChungLoaiNhom.DataPropertyName = "Nhom";
        colChungLoaiNhom.FillWeight = 70F;
        colChungLoaiNhom.HeaderText = "Nhóm";
        colChungLoaiNhom.Name = "colChungLoaiNhom";
        colChungLoaiNhom.ReadOnly = true;
        colChungLoaiSoLuong.DataPropertyName = "SoLuong";
        colChungLoaiSoLuong.FillWeight = 30F;
        colChungLoaiSoLuong.HeaderText = "Số lượng";
        colChungLoaiSoLuong.Name = "colChungLoaiSoLuong";
        colChungLoaiSoLuong.ReadOnly = true;
        // 
        // chungLoaiBindingSource
        // 
        chungLoaiBindingSource.DataSource = typeof(UcThongKe.ThongKeDong);
        // 
        // groupNguonCap
        // 
        groupNguonCap.Controls.Add(gridNguonCap);
        groupNguonCap.Dock = DockStyle.Fill;
        groupNguonCap.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        groupNguonCap.Location = new Point(634, 28);
        groupNguonCap.Margin = new Padding(8);
        groupNguonCap.Name = "groupNguonCap";
        groupNguonCap.Padding = new Padding(3);
        groupNguonCap.Size = new Size(288, 443);
        groupNguonCap.TabIndex = 2;
        groupNguonCap.TabStop = false;
        groupNguonCap.Text = "Số lượng theo nguồn cấp";
        // 
        // gridNguonCap
        // 
        gridNguonCap.AllowUserToAddRows = false;
        gridNguonCap.AutoGenerateColumns = false;
        gridNguonCap.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        gridNguonCap.BackgroundColor = Color.White;
        gridNguonCap.BorderStyle = BorderStyle.None;
        gridNguonCap.ColumnHeadersHeight = 36;
        dataGridViewCellStyle3.BackColor = Color.FromArgb(196, 232, 211);
        dataGridViewCellStyle3.ForeColor = Color.FromArgb(27, 94, 60);
        gridNguonCap.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle3;
        gridNguonCap.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
        gridNguonCap.ColumnHeadersVisible = true;
        gridNguonCap.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] { colNguonCapNhom, colNguonCapSoLuong });
        gridNguonCap.DataSource = nguonCapBindingSource;
        gridNguonCap.Dock = DockStyle.Fill;
        gridNguonCap.EnableHeadersVisualStyles = false;
        gridNguonCap.Font = new Font("Segoe UI", 9F);
        gridNguonCap.Location = new Point(3, 21);
        gridNguonCap.Name = "gridNguonCap";
        gridNguonCap.ReadOnly = true;
        gridNguonCap.RowHeadersVisible = false;
        gridNguonCap.RowTemplate.Height = 30;
        gridNguonCap.Size = new Size(282, 419);
        gridNguonCap.TabIndex = 0;
        colNguonCapNhom.DataPropertyName = "Nhom";
        colNguonCapNhom.FillWeight = 70F;
        colNguonCapNhom.HeaderText = "Nhóm";
        colNguonCapNhom.Name = "colNguonCapNhom";
        colNguonCapNhom.ReadOnly = true;
        colNguonCapSoLuong.DataPropertyName = "SoLuong";
        colNguonCapSoLuong.FillWeight = 30F;
        colNguonCapSoLuong.HeaderText = "Số lượng";
        colNguonCapSoLuong.Name = "colNguonCapSoLuong";
        colNguonCapSoLuong.ReadOnly = true;
        // 
        // nguonCapBindingSource
        // 
        nguonCapBindingSource.DataSource = typeof(UcThongKe.ThongKeDong);
        // 
        // UcThongKe
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.White;
        Controls.Add(tableStats);
        Controls.Add(panelSummary);
        Controls.Add(lblTitle);
        Name = "UcThongKe";
        Size = new Size(950, 680);
        panelSummary.ResumeLayout(false);
        tableStats.ResumeLayout(false);
        groupCap.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridCap).EndInit();
        ((System.ComponentModel.ISupportInitialize)capBindingSource).EndInit();
        groupChungLoai.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridChungLoai).EndInit();
        ((System.ComponentModel.ISupportInitialize)chungLoaiBindingSource).EndInit();
        groupNguonCap.ResumeLayout(false);
        ((System.ComponentModel.ISupportInitialize)gridNguonCap).EndInit();
        ((System.ComponentModel.ISupportInitialize)nguonCapBindingSource).EndInit();
        ResumeLayout(false);
    }
}
