// =============================================================================
// File: UserControls/UcTimKiem.cs
// Mô tả: Chức năng M4 - Tìm kiếm trang thiết bị.
//         2 thuật toán:
//         - Tìm tuần tự: duyệt từng phần tử, hỗ trợ nhiều từ, không cần sắp xếp
//         - Tìm nhị phân: yêu cầu đã sắp xếp (M3), dùng suffix index cho chuỗi
//         Hỗ trợ tìm tiếng Việt không dấu (chuẩn hóa Unicode).
// =============================================================================

using System.Globalization;
using System.Text;
using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

/// <summary>UcTimKiem - tìm kiếm tuần tự và nhị phân (với suffix index cho chuỗi).</summary>
public partial class UcTimKiem : UserControl
{
    // =========================================================================
    // CẤU TRÚC DỮ LIỆU
    // =========================================================================

    private const int KichThuocTrang = 20;

    /// <summary>
    /// Phần tử chỉ mục hậu tố: lưu chuỗi hậu tố + chỉ số thiết bị trong danh sách gốc.
    /// "readonly record struct": cấu trúc bất biến, tự động tạo Equals/GetHashCode.
    /// VD: chuỗi "abc" → 3 hậu tố: ("abc", idx), ("bc", idx), ("c", idx)
    /// </summary>
    private readonly record struct HauToTimKiem(string GiaTri, int ChiSo);

    private MainForm? mainForm;
    private List<ThietBi> ketQuaTimKiem = [];
    private int trangHienTai = 1;

    // =========================================================================
    // CONSTRUCTORS
    // =========================================================================

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

    // =========================================================================
    // NÚT TÌM KIẾM - LOGIC CHÍNH
    // =========================================================================

    /// <summary>Tìm kiếm: validate từ khóa → chọn thuật toán → lưu kết quả vào MainForm.</summary>
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
            // Tìm nhị phân yêu cầu đã sắp xếp theo cùng khóa
            // string.Equals: so sánh chính xác (CurrentCulture)
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
        // Lưu vào MainForm để M2 hiển thị kết quả
        mainForm.LuuKetQuaTimKiem(ketQua, khoa, tuKhoa);
        trangHienTai = 1;
        HienThiTrang();
        lblStatus.Text = $"Tìm thấy {ketQua.Count} kết quả.";
    }

    // =========================================================================
    // TÌM KIẾM TUẦN TỰ (SEQUENTIAL SEARCH)
    // =========================================================================

    /// <summary>
    /// Tìm tuần tự: duyệt từng phần tử, hỗ trợ nhiều từ + không dấu.
    /// Độ phức tạp: O(n × m), n = số thiết bị, m = độ dài chuỗi.
    /// </summary>
    private static List<ThietBi> TimTuanTu(
        List<ThietBi> danhSach, string khoa, string tuKhoa)
    {
        // Tách từ khóa thành mảng, bỏ phần tử rỗng
        // StringSplitOptions.RemoveEmptyEntries: bỏ chuỗi rỗng khi tách
        string[] cacTuKhoa = ChuanHoa(tuKhoa)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<ThietBi> ketQua = [];

        foreach (ThietBi thietBi in danhSach)
        {
            string giaTri = ChuanHoa(LayGiaTri(thietBi, khoa));
            // LINQ All(): Trả về true chỉ khi MỌI từ khóa con (trong mảng cacTuKhoa sau khi tách khoảng trắng) đều xuất hiện trong chuỗi giaTri của thiết bị (phép tìm kiếm dạng AND)
            if (cacTuKhoa.All(giaTri.Contains))
                ketQua.Add(thietBi);
        }

        return ketQua;
    }

    // =========================================================================
    // TÌM KIẾM NHỊ PHÂN (BINARY SEARCH)
    // =========================================================================

    /// <summary>
    /// Tìm nhị phân: khóa chuỗi → dùng suffix index, khóa số/ngày → tìm chính xác.
    /// Yêu cầu: danh sách đã sắp xếp theo khóa tương ứng.
    /// </summary>
    private static List<ThietBi> TimNhiPhan(
        List<ThietBi> danhSach, string khoa, string tuKhoa)
    {
        if (LaKhoaChuoi(khoa))
            return TimNhiPhanMotPhan(danhSach, khoa, tuKhoa);

        // Tìm nhị phân chính xác cho khóa số/ngày
        Comparison<ThietBi> soSanh = TaoHamSoSanh(khoa);
        // Tạo ThietBi giả chỉ chứa giá trị cần tìm (để so sánh)
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
                phai = giua - 1; // Tiếp tục tìm bên trái để lấy vị trí đầu tiên
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

        // Thu thập tất cả phần tử cùng giá trị (duyệt phải từ vị trí tìm thấy)
        List<ThietBi> ketQua = [];
        for (int i = viTri; i < danhSach.Count &&
            soSanh(danhSach[i], giaTriCanTim) == 0; i++)
            ketQua.Add(danhSach[i]);
        return ketQua;
    }

    // =========================================================================
    // TÌM NHỊ PHÂN MỘT PHẦN VỚI CHỈ MỤC HẬU TỐ (SUFFIX INDEX)
    // =========================================================================

    /// <summary>
    /// Tìm substring bằng nhị phân trên chỉ mục hậu tố.
    ///
    /// Ý tưởng: với chuỗi "may tinh", tạo các hậu tố: "may tinh", "ay tinh", ...
    /// Sắp xếp tất cả hậu tố → tìm nhị phân → hậu tố StartsWith(từ khóa) = chứa từ khóa.
    /// Nhiều từ: lấy giao (intersect) kết quả của từng từ.
    /// Độ phức tạp: O(m × log(S)), S = tổng số hậu tố.
    /// </summary>
    private static List<ThietBi> TimNhiPhanMotPhan(
        List<ThietBi> danhSach, string khoa, string tuKhoa)
    {
        string[] cacTuKhoa = ChuanHoa(tuKhoa)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries);
        List<HauToTimKiem> chiMucHauTo = TaoChiMucHauTo(danhSach, khoa);
        // "HashSet<int>?": nullable, null = chưa tìm từ nào
        HashSet<int>? cacChiSoKhop = null;

        foreach (string motTuKhoa in cacTuKhoa)
        {
            HashSet<int> chiSoKhopTuKhoa =
                TimChiSoChuaTuKhoa(chiMucHauTo, motTuKhoa);

            if (cacChiSoKhop == null)
                cacChiSoKhop = chiSoKhopTuKhoa;
            else
                // IntersectWith: chỉ giữ phần tử có trong cả 2 tập (phép giao)
                cacChiSoKhop.IntersectWith(chiSoKhopTuKhoa);

            if (cacChiSoKhop.Count == 0)
                return [];
        }

        if (cacChiSoKhop == null)
            return [];

        // LINQ:
        // - OrderBy(): Sắp xếp các chỉ số khớp theo thứ tự tăng dần để hiển thị theo đúng thứ tự ban đầu trong danh sách gốc
        // - Select(): Ánh xạ từng chỉ số (chiSo) thành đối tượng thiết bị tương ứng (danhSach[chiSo])
        // - ToList(): Thực thi truy vấn LINQ và chuyển kết quả thành danh sách List<ThietBi> thực tế
        return cacChiSoKhop
            .OrderBy(chiSo => chiSo)
            .Select(chiSo => danhSach[chiSo])
            .ToList();
    }

    /// <summary>
    /// Xây dựng chỉ mục hậu tố: tạo mọi hậu tố của mỗi giá trị, rồi sắp xếp.
    /// VD: index 0, giá trị "abc" → ("abc",0), ("bc",0), ("c",0)
    /// </summary>
    private static List<HauToTimKiem> TaoChiMucHauTo(
        List<ThietBi> danhSach, string khoa)
    {
        List<HauToTimKiem> chiMuc = [];

        for (int chiSo = 0; chiSo < danhSach.Count; chiSo++)
        {
            string giaTri = ChuanHoa(LayGiaTri(danhSach[chiSo], khoa));
            for (int viTri = 0; viTri < giaTri.Length; viTri++)
                // giaTri[viTri..]: range syntax, chuỗi từ vị trí viTri đến cuối
                chiMuc.Add(new HauToTimKiem(giaTri[viTri..], chiSo));
        }

        // Sắp xếp: chính theo alphabet, phụ theo chỉ số (ổn định)
        chiMuc.Sort((a, b) =>
        {
            int ketQua = string.Compare(
                a.GiaTri, b.GiaTri, StringComparison.Ordinal);
            return ketQua != 0 ? ketQua : a.ChiSo.CompareTo(b.ChiSo);
        });
        return chiMuc;
    }

    /// <summary>
    /// Tìm nhị phân trên chỉ mục hậu tố: tìm vị trí đầu tiên >= từ khóa,
    /// rồi duyệt các hậu tố StartsWith(từ khóa) → thu thập chỉ số thiết bị.
    /// </summary>
    private static HashSet<int> TimChiSoChuaTuKhoa(
        List<HauToTimKiem> chiMucHauTo, string tuKhoa)
    {
        // Tìm nhị phân lower bound
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

        // Thu thập hậu tố bắt đầu bằng từ khóa
        HashSet<int> ketQua = [];
        for (int i = trai; i < chiMucHauTo.Count &&
            chiMucHauTo[i].GiaTri.StartsWith(
                tuKhoa, StringComparison.Ordinal); i++)
            ketQua.Add(chiMucHauTo[i].ChiSo);

        return ketQua;
    }

    // =========================================================================
    // HÀM TIỆN ÍCH
    // =========================================================================

    /// <summary>Khóa chuỗi dùng suffix index, khóa số/ngày dùng tìm chính xác.</summary>
    /// <remarks>"is ... or ...": pattern matching kiểm tra nhiều giá trị.</remarks>
    private static bool LaKhoaChuoi(string khoa) =>
        khoa is "Mã TTB" or "Tên TTB" or "Nguồn cấp" or "Chủng loại";

    /// <summary>Tạo hàm so sánh cho tìm nhị phân chính xác (không nhân hệ số hướng).</summary>
    private static Comparison<ThietBi> TaoHamSoSanh(string khoa) =>
        khoa switch
        {
            "Mã TTB" => (a, b) => SoSanhChuoi(a.MaTTB, b.MaTTB),
            "SoHieu" => (a, b) => SoSanhChuoi(a.SoHieu, b.SoHieu),
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

    /// <summary>Tạo ThietBi giả chỉ chứa giá trị cần tìm (dùng cho so sánh nhị phân).</summary>
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

    /// <summary>Parse chuỗi ngày. TryParseExact: parse theo định dạng cho sẵn.</summary>
    private static DateTime DocNgay(string giaTri)
    {
        // Mảng định dạng được chấp nhận
        string[] dinhDang = ["dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd"];
        // InvariantCulture: không phụ thuộc cài đặt ngôn ngữ máy
        if (DateTime.TryParseExact(giaTri, dinhDang,
            CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime ngay))
            return ngay;
        throw new ArgumentException(
            "Ngày phải có định dạng ngày/tháng/năm, ví dụ 08/06/2026.");
    }

    // =========================================================================
    // CHUẨN HÓA TIẾNG VIỆT (BỎ DẤU)
    // =========================================================================

    /// <summary>
    /// Chuẩn hóa chuỗi tiếng Việt: bỏ dấu + viết thường.
    /// VD: "Máy Tính" → "may tinh", "Đồng hồ" → "dong ho"
    ///
    /// Quy trình:
    ///   1. Normalize(FormD): tách "á" → "a" + dấu sắc (combining mark)
    ///   2. Bỏ các ký tự dấu (NonSpacingMark category)
    ///   3. Chuyển "đ" → "d" (đ không bị tách bởi FormD nên xử lý riêng)
    ///   4. Normalize(FormC): gộp lại dạng chuẩn
    /// </summary>
    private static string ChuanHoa(string giaTri)
    {
        string chuoiTachDau = giaTri.Trim().ToLowerInvariant()
            .Normalize(NormalizationForm.FormD);
        StringBuilder ketQua = new();

        foreach (char kyTu in chuoiTachDau)
        {
            // CharUnicodeInfo.GetUnicodeCategory: phân loại ký tự Unicode
            // NonSpacingMark: ký tự dấu phụ (sắc, huyền, hỏi, ngã, nặng)
            if (CharUnicodeInfo.GetUnicodeCategory(kyTu) !=
                UnicodeCategory.NonSpacingMark)
                ketQua.Append(kyTu == 'đ' ? 'd' : kyTu);
        }

        // Chuẩn hóa khoảng trắng: "a  b" → "a b"
        return string.Join(' ', ketQua.ToString()
            .Normalize(NormalizationForm.FormC)
            .Split(' ', StringSplitOptions.RemoveEmptyEntries));
    }

    /// <summary>Lấy giá trị chuỗi của trường theo tên khóa (switch expression).</summary>
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

    // =========================================================================
    // CẬP NHẬT TRẠNG THÁI
    // =========================================================================

    private void cboThuatToan_SelectedIndexChanged(object? sender, EventArgs e) =>
        CapNhatTrangThai();

    /// <summary>Cập nhật label hướng dẫn theo thuật toán và khóa đã chọn.</summary>
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

    // =========================================================================
    // PHÂN TRANG
    // =========================================================================

    private void HienThiTrang()
    {
        // 1. Lấy tổng số bản ghi trong danh sách kết quả tìm kiếm
        int tongBanGhi = ketQuaTimKiem.Count;

        // 2. Tính tổng số trang: Chia tổng bản ghi cho kích thước mỗi trang (20).
        // Phải ép kiểu sang (double) để thực hiện phép chia số thực, rồi dùng Math.Ceiling để làm tròn lên.
        // Math.Max(1, ...) đảm bảo tổng số trang luôn tối thiểu là 1 trang (ngay cả khi không có bản ghi nào).
        int tongTrang = Math.Max(1, (int)Math.Ceiling(tongBanGhi / (double)KichThuocTrang));

        // 3. Bảo vệ biến trangHienTai: Math.Clamp giới hạn trangHienTai luôn nằm trong đoạn [1, tongTrang],
        // ngăn chặn việc trang hiện tại bị nhỏ hơn 1 hoặc vượt quá tổng số trang hiện có.
        trangHienTai = Math.Clamp(trangHienTai, 1, tongTrang);

        // 4. Lọc dữ liệu của trang hiện tại bằng LINQ:
        // - Skip(): Bỏ qua (trangHienTai - 1) * 20 kết quả tìm kiếm của các trang trước đó
        // - Take(): Lấy tối đa 20 kết quả tìm kiếm cho trang hiện tại
        // - ToList(): Chuyển đổi kết quả truy vấn thành danh sách List<ThietBi> thực tế
        List<ThietBi> duLieuTrang = ketQuaTimKiem
            .Skip((trangHienTai - 1) * KichThuocTrang)
            .Take(KichThuocTrang)
            .ToList();

        // 5. Cập nhật dữ liệu cho DataGridView:
        // Đặt DataSource về null trước để xóa dữ liệu cũ và buộc Grid vẽ lại (refresh), tránh lỗi cache hiển thị
        thietBiBindingSource.DataSource = null;
        thietBiBindingSource.DataSource = duLieuTrang;

        // 6. Tính toán số thứ tự dòng để hiển thị thông tin label (Ví dụ: "Hiển thị 21-40 / 53"):
        // - batDau: dòng đầu tiên của trang (ví dụ trang 2: (2-1)*20 + 1 = dòng thứ 21). Nếu không có bản ghi nào thì là 0.
        // - ketThuc: dòng cuối cùng của trang (ví dụ trang 2: 2*20 = 40. Nếu trang cuối chỉ có 13 dòng thì Math.Min(60, 53) = dòng thứ 53).
        int batDau = tongBanGhi == 0 ? 0 : (trangHienTai - 1) * KichThuocTrang + 1;
        int ketThuc = Math.Min(trangHienTai * KichThuocTrang, tongBanGhi);
        lblViTri.Text = $"Hiển thị {batDau}-{ketThuc} / {tongBanGhi}";
        lblTrang.Text = $"Trang {trangHienTai} / {tongTrang}";

        // 7. Cập nhật trạng thái kích hoạt (Bật/Tắt) của các nút điều hướng:
        // - Nút Trang Đầu & Trang Trước: Chỉ bật khi trang hiện tại lớn hơn 1 (không ở trang đầu)
        btnTrangDau.Enabled = trangHienTai > 1;
        btnTrangTruoc.Enabled = trangHienTai > 1;
        // - Nút Trang Sau & Trang Cuối: Chỉ bật khi trang hiện tại nhỏ hơn tổng số trang (không ở trang cuối)
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
