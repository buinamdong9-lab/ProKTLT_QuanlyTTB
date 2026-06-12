// =============================================================================
// File: UserControls/UcThemMoi.cs
// Mô tả: Chức năng M1 - Thêm mới / Cập nhật hồ sơ thiết bị.
//         Bao gồm: nhập liệu, validation, kiểm tra trùng mã, lưu JSON.
//         Hỗ trợ 2 chế độ: thêm mới (thietBiDangSua == null) và
//         cập nhật (thietBiDangSua != null).
// =============================================================================

using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

/// <summary>UcThemMoi - form nhập/sửa thiết bị, partial class (logic + Designer).</summary>
public partial class UcThemMoi : UserControl
{
    /// <summary>Tham chiếu MainForm. Nullable vì constructor mặc định (Designer) không có.</summary>
    private MainForm? mainForm;

    /// <summary>Thiết bị đang sửa. null = thêm mới, != null = cập nhật.</summary>
    private ThietBi? thietBiDangSua;

    // =========================================================================
    // CONSTRUCTORS
    // =========================================================================

    /// <summary>Constructor mặc định - dùng cho Designer.</summary>
    public UcThemMoi()
    {
        InitializeComponent();
    }

    /// <summary>Constructor chính. ": this()" gọi constructor mặc định trước.</summary>
    public UcThemMoi(MainForm mainForm, ThietBi? thietBiCansua = null)
        : this()
    {
        this.mainForm = mainForm;
        thietBiDangSua =thietBiCansua ;
        if (thietBiCansua != null) NapDuLieu(thietBiCansua);
    }

    // =========================================================================
    // NÚT LƯU - LOGIC CHÍNH
    // =========================================================================

    /// <summary>Xử lý lưu/cập nhật thiết bị, bao gồm validate + rollback nếu thất bại.</summary>
    private void btnLuu_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        // KiemTraDuLieu trả về false nếu lỗi, trả soLuong và cap qua "out"
        if (!KiemTraDuLieu(out int soLuong, out int cap)) return;

        // Kiểm tra trùng mã TTB
        // Any(): LINQ - kiểm tra có phần tử nào thỏa điều kiện
        // ReferenceEquals: so sánh tham chiếu (bỏ qua chính thiết bị đang sửa)
        bool trungMa = mainForm.DanhSach.Any(tb =>
            !ReferenceEquals(tb, thietBiDangSua) &&
            tb.MaTTB.Equals(txtMaTTB.Text.Trim(), StringComparison.CurrentCultureIgnoreCase));
        if (trungMa)
        {
            MessageBox.Show("Mã TTB đã tồn tại.", "Dữ liệu không hợp lệ",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaTTB.Focus();
            return;
        }

        // Kiểm tra trùng số hiệu
        bool trungSoHieu = mainForm.DanhSach.Any(tb =>
            !ReferenceEquals(tb, thietBiDangSua) &&
            tb.SoHieu.Equals(txtSoHieu.Text.Trim(),
                StringComparison.CurrentCultureIgnoreCase));
        if (trungSoHieu)
        {
            MessageBox.Show("Số hiệu trang thiết bị đã tồn tại.",
                "Dữ liệu không hợp lệ", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            txtSoHieu.Focus();
            txtSoHieu.SelectAll(); // SelectAll(): bôi đen text để dễ sửa
            return;
        }

        bool laThemMoi = thietBiDangSua == null;
        // Toán tử "??": nếu null → tạo mới, ngược lại dùng đối tượng cũ
        ThietBi tb = thietBiDangSua ?? new ThietBi();
        // Backup dữ liệu cũ trước khi sửa (để rollback nếu lưu thất bại)
        ThietBi? duLieuCu = laThemMoi ? null : SaoChep(tb);

        // Gán dữ liệu từ form. Trim() loại bỏ khoảng trắng đầu/cuối.
        // .Date loại bỏ phần giờ, chỉ giữ ngày.
        tb.MaTTB = txtMaTTB.Text.Trim();
        tb.SoHieu = txtSoHieu.Text.Trim();
        tb.TenTTB = txtTenTTB.Text.Trim();
        tb.NgaySanXuat = dtpNgaySanXuat.Value.Date;
        tb.NgayDuaVaoSuDung = dtpNgaySuDung.Value.Date;
        tb.NguonCap = txtNguonCap.Text.Trim();
        tb.SoLuong = soLuong;
        tb.ChungLoai = txtChungLoai.Text.Trim();
        tb.Cap = cap;

        if (laThemMoi)
            mainForm.DanhSach.Add(tb);

        if (mainForm.LuuDuLieu())
        {
            // Toán tử 3 ngôi (? :): chọn thông báo phù hợp
            MessageBox.Show(laThemMoi
                    ? "Đã thêm thiết bị."
                    : "Đã cập nhật thiết bị.",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (laThemMoi)
                LamMoi();
        }
        // Rollback nếu lưu thất bại
        else if (laThemMoi)
        {
            mainForm.DanhSach.Remove(tb);
        }
        else if (duLieuCu != null)
        {
            GanDuLieu(tb, duLieuCu);
        }
    }

    // =========================================================================
    // SAO CHÉP DỮ LIỆU
    // =========================================================================

    /// <summary>Tạo bản sao (clone) để backup trước khi sửa.</summary>
    /// <remarks>Expression-bodied method + object initializer syntax.</remarks>
    private static ThietBi SaoChep(ThietBi nguon) =>
        new()
        {
            MaTTB = nguon.MaTTB,
            SoHieu = nguon.SoHieu,
            TenTTB = nguon.TenTTB,
            NgaySanXuat = nguon.NgaySanXuat,
            NgayDuaVaoSuDung = nguon.NgayDuaVaoSuDung,
            NguonCap = nguon.NguonCap,
            SoLuong = nguon.SoLuong,
            ChungLoai = nguon.ChungLoai,
            Cap = nguon.Cap
        };

    /// <summary>Gán dữ liệu từ nguồn sang đích (dùng khi rollback, không tạo đối tượng mới).</summary>
    private static void GanDuLieu(ThietBi dich, ThietBi nguon)
    {
        dich.MaTTB = nguon.MaTTB;
        dich.SoHieu = nguon.SoHieu;
        dich.TenTTB = nguon.TenTTB;
        dich.NgaySanXuat = nguon.NgaySanXuat;
        dich.NgayDuaVaoSuDung = nguon.NgayDuaVaoSuDung;
        dich.NguonCap = nguon.NguonCap;
        dich.SoLuong = nguon.SoLuong;
        dich.ChungLoai = nguon.ChungLoai;
        dich.Cap = nguon.Cap;
    }

    // =========================================================================
    // VALIDATION
    // =========================================================================

    /// <summary>Kiểm tra tất cả dữ liệu nhập: bắt buộc, định dạng mã, số lượng, ngày.</summary>
    private bool KiemTraDuLieu(out int soLuong, out int cap)
    {
        soLuong = 0;
        cap = (int)nudCap.Value; // NumericUpDown trả về decimal, ép sang int

        // Mảng tuple: mỗi phần tử gồm (TextBox, tên trường hiển thị lỗi)
        (TextBox ONhap, string TenTruong)[] truongBatBuoc =
        [
            (txtMaTTB, "Mã trang thiết bị"),
            (txtSoHieu, "Số hiệu trang thiết bị"),
            (txtTenTTB, "Tên trang thiết bị"),
            (txtNguonCap, "Nguồn cấp"),
            (txtSoLuong, "Số lượng"),
            (txtChungLoai, "Chủng loại")
        ];

        // Duyệt kiểm tra trường bắt buộc
        // Tuple deconstruction: tách (oNhap, tenTruong) từ tuple
        foreach ((TextBox oNhap, string tenTruong) in truongBatBuoc)
        {
            if (!string.IsNullOrWhiteSpace(oNhap.Text)) continue;

            MessageBox.Show($"{tenTruong} là trường bắt buộc.",
                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            oNhap.Focus();
            return false;
        }

        // Kiểm tra định dạng mã TTB: "TTB" + số dương
        // maTTB[3..]: range syntax, lấy từ vị trí 3 đến cuối
        string maTTB = txtMaTTB.Text.Trim();
        if ( !maTTB.StartsWith("TTB",StringComparison.OrdinalIgnoreCase)||
            !int.TryParse(maTTB[3..], out int soThuTu)|| soThuTu <= 0)
        {
            MessageBox.Show("Mã thiết bị phải đúng định dạng TTBxxx, ví dụ:TTB001.",
                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaTTB.Focus();
            txtMaTTB.SelectAll();
            return false;
        }

        // int.TryParse: cố parse chuỗi → số, trả false nếu không hợp lệ
        if (!int.TryParse(txtSoLuong.Text.Trim(), out soLuong) || soLuong <= 0)
        {
            MessageBox.Show("Số lượng phải là số nguyên lớn hơn 0.",
                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtSoLuong.Focus();
            txtSoLuong.SelectAll();
            return false;
        }

        if (dtpNgaySuDung.Value.Date < dtpNgaySanXuat.Value.Date)
        {
            MessageBox.Show("Ngày đưa vào sử dụng không được nhỏ hơn ngày sản xuất.",
                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtpNgaySuDung.Focus();
            return false;
        }

        if (dtpNgaySanXuat.Value.Date > DateTime.Today)
        {
            MessageBox.Show("Ngày sản xuất không được lớn hơn ngày hiện tại.",
                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtpNgaySanXuat.Focus();
            return false;
        }

        if (dtpNgaySuDung.Value.Date > DateTime.Today)
        {
            MessageBox.Show("Ngày đưa vào sử dụng không được lớn hơn ngày hiện tại.",
                "Dữ liệu không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            dtpNgaySuDung.Focus();
            return false;
        }
        return true;
    }

    // =========================================================================
    // LÀM MỚI VÀ NẠP DỮ LIỆU
    // =========================================================================

    /// <summary>Expression-bodied method: viết gọn khi chỉ có 1 dòng.</summary>
    private void btnLamMoi_Click(object sender, EventArgs e) => LamMoi();

    /// <summary>Reset tất cả ô nhập về giá trị mặc định.</summary>
    private void LamMoi()
    {
        txtMaTTB.Clear();
        txtSoHieu.Clear();
        txtTenTTB.Clear();
        txtNguonCap.Clear();
        txtSoLuong.Clear();
        txtChungLoai.Clear();
        dtpNgaySanXuat.Value = new DateTime(2020, 1, 1);
        dtpNgaySuDung.Value = DateTime.Today;
        nudCap.Value = 1;
        txtMaTTB.Focus();
    }

    /// <summary>Nạp dữ liệu thiết bị vào form + đổi tiêu đề sang chế độ "Cập nhật".</summary>
    private void NapDuLieu(ThietBi tb)
    {
        lblTitle.Text = "M1 - CẬP NHẬT HỒ SƠ THIẾT BỊ";
        btnLuu.Text = "Cập nhật";
        txtMaTTB.Text = tb.MaTTB;
        txtSoHieu.Text = tb.SoHieu;
        txtTenTTB.Text = tb.TenTTB;
        dtpNgaySanXuat.Value = tb.NgaySanXuat;
        dtpNgaySuDung.Value = tb.NgayDuaVaoSuDung;
        txtNguonCap.Text = tb.NguonCap;
        txtSoLuong.Text = tb.SoLuong.ToString();
        txtChungLoai.Text = tb.ChungLoai;
        nudCap.Value = tb.Cap;
    }
}
