using System.Globalization;
using System.Text;
using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

public partial class UcTimKiem : UserControl
{
    private const int KichThuocTrang = 20;
    private readonly record struct HauToTimKiem(string GiaTri, int ChiSo);

    private MainForm? mainForm;
    private List<ThietBi> ketQuaTimKiem = [];
    private int trangHienTai = 1;

    public UcTimKiem()
    {
        InitializeComponent();
    }

    public UcTimKiem(MainForm mainForm)
        : this()
    {
        this.mainForm = mainForm;
        cboThuatToan.SelectedIndex = 0;
        cboKhoa.SelectedIndex = 0;
        CapNhatTrangThai();
        HienThiTrang();
    }

    private void btnTim_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        string tuKhoa = txtTuKhoa.Text.Trim();
        if (tuKhoa.Length == 0)
        {
            MessageBox.Show("Hãy nhập từ khóa tìm kiếm."); txtTuKhoa.Focus(); return;
        }

        string khoa = cboKhoa.Text;
        List<ThietBi> ketQua;
        if (cboThuatToan.Text == "Tìm kiếm nhị phân")
        {
            if (!string.Equals(mainForm.KhoaDaSapXep, khoa, StringComparison.CurrentCulture))
            {
                MessageBox.Show($"Danh sách chưa được sắp xếp theo khóa \"{khoa}\".\n" +
                    "Hãy dùng tìm kiếm tuần tự hoặc sang M3 để sắp xếp trước.",
                    "Không thể tìm nhị phân", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                ketQua = TimNhiPhan(mainForm.DanhSach, khoa, tuKhoa);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Không thể tìm kiếm",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTuKhoa.Focus();
                return;
            }
        }
        else
        {
            ketQua = TimTuanTu(mainForm.DanhSach, khoa, tuKhoa);
        }

        ketQuaTimKiem = ketQua;
        mainForm.LuuKetQuaTimKiem(ketQua, khoa, tuKhoa);
        trangHienTai = 1;
        HienThiTrang();
        lblStatus.Text = $"Tìm thấy {ketQua.Count} kết quả.";
    }

    private static List<ThietBi> TimTuanTu(
        List<ThietBi> danhSach, string khoa, string tuKhoa)
    {
        string[] cacTuKhoa = ChuanHoa(tuKhoa)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<ThietBi> ketQua = [];

        foreach (ThietBi thietBi in danhSach)
        {
            string giaTri = ChuanHoa(LayGiaTri(thietBi, khoa));
            if (cacTuKhoa.All(giaTri.Contains))
                ketQua.Add(thietBi);
        }

        return ketQua;
    }

    private static List<ThietBi> TimNhiPhan(
        List<ThietBi> danhSach, string khoa, string tuKhoa)
    {
        if (LaKhoaChuoi(khoa))
            return TimNhiPhanMotPhan(danhSach, khoa, tuKhoa);

        Comparison<ThietBi> soSanh = TaoHamSoSanh(khoa);
        ThietBi giaTriCanTim = TaoGiaTriTimKiem(khoa, tuKhoa);
        int trai = 0;
        int phai = danhSach.Count - 1;
        int viTri = -1;

        while (trai <= phai)
        {
            int giua = (trai + phai) / 2;
            int ketQuaSoSanh = soSanh(danhSach[giua], giaTriCanTim);

            if (ketQuaSoSanh == 0)
            {
                viTri = giua;
                phai = giua - 1;
            }
            else if (ketQuaSoSanh < 0)
            {
                trai = giua + 1;
            }
            else
            {
                phai = giua - 1;
            }
        }

        if (viTri < 0) return [];

        List<ThietBi> ketQua = [];
        for (int i = viTri; i < danhSach.Count &&
            soSanh(danhSach[i], giaTriCanTim) == 0; i++)
            ketQua.Add(danhSach[i]);
        return ketQua;
    }

    private static List<ThietBi> TimNhiPhanMotPhan(
        List<ThietBi> danhSach, string khoa, string tuKhoa)
    {
        string[] cacTuKhoa = ChuanHoa(tuKhoa)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<HauToTimKiem> chiMucHauTo = TaoChiMucHauTo(danhSach, khoa);
        HashSet<int>? cacChiSoKhop = null;

        foreach (string motTuKhoa in cacTuKhoa)
        {
            HashSet<int> chiSoKhopTuKhoa =
                TimChiSoChuaTuKhoa(chiMucHauTo, motTuKhoa);

            if (cacChiSoKhop == null)
                cacChiSoKhop = chiSoKhopTuKhoa;
            else
                cacChiSoKhop.IntersectWith(chiSoKhopTuKhoa);

            if (cacChiSoKhop.Count == 0)
                return [];
        }

        if (cacChiSoKhop == null)
            return [];

        return cacChiSoKhop
            .OrderBy(chiSo => chiSo)
            .Select(chiSo => danhSach[chiSo])
            .ToList();
    }

    private static List<HauToTimKiem> TaoChiMucHauTo(
        List<ThietBi> danhSach, string khoa)
    {
        List<HauToTimKiem> chiMuc = [];

        for (int chiSo = 0; chiSo < danhSach.Count; chiSo++)
        {
            string giaTri = ChuanHoa(LayGiaTri(danhSach[chiSo], khoa));
            for (int viTri = 0; viTri < giaTri.Length; viTri++)
                chiMuc.Add(new HauToTimKiem(giaTri[viTri..], chiSo));
        }

        chiMuc.Sort((a, b) =>
        {
            int ketQua = string.Compare(
                a.GiaTri, b.GiaTri, StringComparison.Ordinal);
            return ketQua != 0 ? ketQua : a.ChiSo.CompareTo(b.ChiSo);
        });
        return chiMuc;
    }

    private static HashSet<int> TimChiSoChuaTuKhoa(
        List<HauToTimKiem> chiMucHauTo, string tuKhoa)
    {
        int trai = 0;
        int phai = chiMucHauTo.Count;

        while (trai < phai)
        {
            int giua = trai + (phai - trai) / 2;
            if (string.Compare(chiMucHauTo[giua].GiaTri, tuKhoa,
                StringComparison.Ordinal) < 0)
                trai = giua + 1;
            else
                phai = giua;
        }

        HashSet<int> ketQua = [];
        for (int i = trai; i < chiMucHauTo.Count &&
            chiMucHauTo[i].GiaTri.StartsWith(
                tuKhoa, StringComparison.Ordinal); i++)
            ketQua.Add(chiMucHauTo[i].ChiSo);

        return ketQua;
    }

    private static bool LaKhoaChuoi(string khoa) =>
        khoa is "Mã TTB" or "Tên TTB" or "Nguồn cấp" or "Chủng loại";

    private static Comparison<ThietBi> TaoHamSoSanh(string khoa) =>
        khoa switch
        {
            "Mã TTB" => (a, b) => SoSanhChuoi(a.MaTTB, b.MaTTB),
            "Tên TTB" => (a, b) => SoSanhChuoi(a.TenTTB, b.TenTTB),
            "Ngày sản xuất" =>
                (a, b) => a.NgaySanXuat.CompareTo(b.NgaySanXuat),
            "Ngày đưa vào sử dụng" =>
                (a, b) => a.NgayDuaVaoSuDung.CompareTo(b.NgayDuaVaoSuDung),
            "Nguồn cấp" => (a, b) => SoSanhChuoi(a.NguonCap, b.NguonCap),
            "Chủng loại" => (a, b) => SoSanhChuoi(a.ChungLoai, b.ChungLoai),
            "Số lượng" => (a, b) => a.SoLuong.CompareTo(b.SoLuong),
            "Cấp" => (a, b) => a.Cap.CompareTo(b.Cap),
            _ => throw new ArgumentException("Khóa tìm kiếm không hợp lệ.")
        };

    private static int SoSanhChuoi(string a, string b) =>
        string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase);

    private static ThietBi TaoGiaTriTimKiem(string khoa, string tuKhoa)
    {
        ThietBi giaTri = new();
        string chuoi = tuKhoa.Trim();

        switch (khoa)
        {
            case "Mã TTB":
                giaTri.MaTTB = chuoi;
                break;
            case "Tên TTB":
                giaTri.TenTTB = chuoi;
                break;
            case "Ngày sản xuất":
                giaTri.NgaySanXuat = DocNgay(chuoi);
                break;
            case "Ngày đưa vào sử dụng":
                giaTri.NgayDuaVaoSuDung = DocNgay(chuoi);
                break;
            case "Nguồn cấp":
                giaTri.NguonCap = chuoi;
                break;
            case "Chủng loại":
                giaTri.ChungLoai = chuoi;
                break;
            case "Số lượng":
                if (!int.TryParse(chuoi, out int soLuong))
                    throw new ArgumentException("Số lượng phải là số nguyên.");
                giaTri.SoLuong = soLuong;
                break;
            case "Cấp":
                if (!int.TryParse(chuoi, out int cap))
                    throw new ArgumentException("Cấp phải là số nguyên.");
                giaTri.Cap = cap;
                break;
            default:
                throw new ArgumentException("Khóa tìm kiếm không hợp lệ.");
        }

        return giaTri;
    }

    private static DateTime DocNgay(string giaTri)
    {
        string[] dinhDang = ["dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"];
        if (DateTime.TryParseExact(giaTri, dinhDang,
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
            return ngay;
        throw new ArgumentException(
            "Ngày phải có định dạng ngày/tháng/năm, ví dụ 08/06/2026.");
    }

    private static string ChuanHoa(string giaTri)
    {
        string chuoiTachDau = giaTri.Trim().ToLowerInvariant()
            .Normalize(NormalizationForm.FormD);
        StringBuilder ketQua = new();

        foreach (char kyTu in chuoiTachDau)
        {
            if (CharUnicodeInfo.GetUnicodeCategory(kyTu) !=
                UnicodeCategory.NonSpacingMark)
                ketQua.Append(kyTu == 'đ' ? 'd' : kyTu);
        }

        return string.Join(' ', ketQua.ToString()
            .Normalize(NormalizationForm.FormC)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }

    private static string LayGiaTri(ThietBi thietBi, string khoa) =>
        khoa switch
        {
            "Mã TTB" => thietBi.MaTTB,
            "Tên TTB" => thietBi.TenTTB,
            "Ngày sản xuất" => thietBi.NgaySanXuat.ToString("dd/MM/yyyy"),
            "Ngày đưa vào sử dụng" =>
                thietBi.NgayDuaVaoSuDung.ToString("dd/MM/yyyy"),
            "Nguồn cấp" => thietBi.NguonCap,
            "Chủng loại" => thietBi.ChungLoai,
            "Số lượng" => thietBi.SoLuong.ToString(),
            "Cấp" => thietBi.Cap.ToString(),
            _ => ""
        };

    private void cboThuatToan_SelectedIndexChanged(object? sender, EventArgs e) =>
        CapNhatTrangThai();

    private void CapNhatTrangThai()
    {
        if (mainForm == null)
        {
            lblStatus.Text = "Nhập từ khóa để tìm kiếm.";
            return;
        }

        if (cboThuatToan.Text == "Tìm kiếm nhị phân")
            lblStatus.Text = mainForm.KhoaDaSapXep == null
                ? "Chưa có khóa đã sắp xếp; tìm nhị phân sẽ không được thực hiện."
                : LaKhoaChuoi(mainForm.KhoaDaSapXep)
                    ? $"Tìm nhị phân theo một phần chuỗi: {mainForm.KhoaDaSapXep}."
                    : $"Tìm nhị phân chính xác theo khóa: {mainForm.KhoaDaSapXep}.";
        else
            lblStatus.Text = "Có thể nhập một phần hoặc nhiều từ, không cần gõ dấu.";
    }

    private void HienThiTrang()
    {
        int tongBanGhi = ketQuaTimKiem.Count;
        int tongTrang = Math.Max(1, (int)Math.Ceiling(tongBanGhi / (double)KichThuocTrang));
        trangHienTai = Math.Clamp(trangHienTai, 1, tongTrang);

        List<ThietBi> duLieuTrang = ketQuaTimKiem
            .Skip((trangHienTai - 1) * KichThuocTrang)
            .Take(KichThuocTrang)
            .ToList();

        thietBiBindingSource.DataSource = null;
        thietBiBindingSource.DataSource = duLieuTrang;

        int batDau = tongBanGhi == 0 ? 0 : (trangHienTai - 1) * KichThuocTrang + 1;
        int ketThuc = Math.Min(trangHienTai * KichThuocTrang, tongBanGhi);
        lblViTri.Text = $"Hiển thị {batDau}-{ketThuc} / {tongBanGhi}";
        lblTrang.Text = $"Trang {trangHienTai} / {tongTrang}";

        btnTrangDau.Enabled = trangHienTai > 1;
        btnTrangTruoc.Enabled = trangHienTai > 1;
        btnTrangSau.Enabled = trangHienTai < tongTrang;
        btnTrangCuoi.Enabled = trangHienTai < tongTrang;
    }

    private void btnTrangDau_Click(object sender, EventArgs e)
    {
        trangHienTai = 1;
        HienThiTrang();
    }

    private void btnTrangTruoc_Click(object sender, EventArgs e)
    {
        trangHienTai--;
        HienThiTrang();
    }

    private void btnTrangSau_Click(object sender, EventArgs e)
    {
        trangHienTai++;
        HienThiTrang();
    }

    private void btnTrangCuoi_Click(object sender, EventArgs e)
    {
        trangHienTai = Math.Max(1,
            (int)Math.Ceiling(ketQuaTimKiem.Count / (double)KichThuocTrang));
        HienThiTrang();
    }
}
