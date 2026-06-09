using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

public partial class UcThemMoi : UserControl
{
    private MainForm? mainForm;
    private ThietBi? thietBiDangSua;

    public UcThemMoi()
    {
        InitializeComponent();
    }

    public UcThemMoi(MainForm mainForm, ThietBi? thietBiCansua = null)
        : this()
    {
        this.mainForm = mainForm;
        thietBiDangSua =thietBiCansua ;
        if (thietBiCansua != null) NapDuLieu(thietBiCansua);
    }

    private void btnLuu_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        if (!KiemTraDuLieu(out int soLuong, out int cap)) return;

        bool trungMa = mainForm.DanhSach.Any(tb =>
            !ReferenceEquals(tb, thietBiDangSua) &&
            tb.MaTTB.Equals(txtMaTTB.Text.Trim(), StringComparison.CurrentCultureIgnoreCase));
        if (trungMa)
        {
            MessageBox.Show("Mã TTB đã tồn tại.", "Dữ liệu không hợp lệ",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaTTB.Focus();
            return;
        }

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
            txtSoHieu.SelectAll();
            return;
        }

        bool laThemMoi = thietBiDangSua == null;
        ThietBi tb = thietBiDangSua ?? new ThietBi();
        ThietBi? duLieuCu = laThemMoi ? null : SaoChep(tb);

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
            MessageBox.Show(laThemMoi
                    ? "Đã thêm thiết bị."
                    : "Đã cập nhật thiết bị.",
                "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (laThemMoi)
                LamMoi();
        }
        else if (laThemMoi)
        {
            mainForm.DanhSach.Remove(tb);
        }
        else if (duLieuCu != null)
        {
            GanDuLieu(tb, duLieuCu);
        }
    }

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

    private bool KiemTraDuLieu(out int soLuong, out int cap)
    {
        soLuong = 0;
        cap = (int)nudCap.Value;

        (TextBox ONhap, string TenTruong)[] truongBatBuoc =
        [
            (txtMaTTB, "Mã trang thiết bị"),
            (txtSoHieu, "Số hiệu trang thiết bị"),
            (txtTenTTB, "Tên trang thiết bị"),
            (txtNguonCap, "Nguồn cấp"),
            (txtSoLuong, "Số lượng"),
            (txtChungLoai, "Chủng loại")
        ];

        foreach ((TextBox oNhap, string tenTruong) in truongBatBuoc)
        {
            if (!string.IsNullOrWhiteSpace(oNhap.Text)) continue;

            MessageBox.Show($"{tenTruong} là trường bắt buộc.",
                "Thiếu thông tin", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            oNhap.Focus();
            return false;
        }

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
        return true;
    }

    private void btnLamMoi_Click(object sender, EventArgs e) => LamMoi();

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
