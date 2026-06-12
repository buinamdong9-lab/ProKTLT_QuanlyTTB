// =============================================================================
// File: MainForm.cs
// Mô tả: Form chính của ứng dụng "Hệ thống Quản lý Trang Thiết Bị".
//         - Quản lý dữ liệu thiết bị (đọc/ghi file JSON)
//         - Điều hướng giữa các chức năng M1-M6 qua sidebar
//         - Lưu trữ trạng thái ứng dụng (danh sách, kết quả tìm kiếm, sắp xếp)
//
//         Giao diện: [panelSidebar (trái)] | [panelContent (phải)]
// =============================================================================

using System.Text.Encodings.Web;
using System.Text.Json;
using QuanlyTTB.Models;
using QuanlyTTB.UserControls;

namespace QuanlyTTB;

/// <summary>
/// MainForm - Form chính, partial class: phần này chứa logic, Designer.cs chứa giao diện.
/// </summary>
public partial class MainForm : Form
{
    // =========================================================================
    // CẤU HÌNH
    // =========================================================================

    /// <summary>Tùy chọn serialize JSON.</summary>
    /// <remarks>
    /// WriteIndented: xuất JSON có thụt lề (dễ đọc).
    /// UnsafeRelaxedJsonEscaping: giữ nguyên ký tự Unicode tiếng Việt,
    /// không escape thành \uXXXX.
    /// "static readonly": khởi tạo 1 lần, dùng chung cho toàn bộ ứng dụng.
    /// </remarks>
    private static readonly JsonSerializerOptions TuyChonJson = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    /// <summary>Đường dẫn thư mục chứa data.json. "const" = cố định lúc biên dịch.</summary>
    public const string DataFolder =
        @"D:\ProKTLT\QuanlyTTb\QuanlyTTB\Data";

    /// <summary>Đường dẫn đầy đủ đến file data.json.</summary>
    /// <remarks>
    /// Expression-bodied property (=>) tính giá trị mỗi lần truy cập.
    /// Path.Combine nối thư mục + tên file an toàn (tự thêm dấu \).
    /// </remarks>
    public static string DataFilePath =>
        Path.Combine(DataFolder, "data.json");

    // =========================================================================
    // TRẠNG THÁI ỨNG DỤNG
    // =========================================================================

    /// <summary>Danh sách toàn bộ thiết bị, đọc từ JSON khi khởi động.</summary>
    /// <remarks>"private set": chỉ MainForm được gán lại.</remarks>
    public List<ThietBi> DanhSach { get; private set; }

    /// <summary>Khóa mà danh sách đã sắp xếp gần nhất (null = chưa sắp xếp).</summary>
    /// <remarks>
    /// Dùng để kiểm tra điều kiện tìm nhị phân (M4) — yêu cầu dữ liệu 
    /// đã sắp xếp theo cùng khóa.
    /// </remarks>
    public string? KhoaDaSapXep { get; set; }

    /// <summary>Kết quả tìm kiếm gần nhất từ M4 (null = chưa tìm).</summary>
    /// <remarks>UcDanhSach (M2) dùng để hiển thị kết quả lọc thay vì toàn bộ danh sách.</remarks>
    public List<ThietBi>? KetQuaTimKiemM4 { get; private set; }

    /// <summary>Mô tả điều kiện tìm kiếm (VD: 'Tên TTB: "máy tính"').</summary>
    public string? MoTaTimKiemM4 { get; private set; }

    /// <summary>Cờ cho phép ghi file JSON.</summary>
    /// <remarks>
    /// false khi đọc file lỗi → khóa chức năng lưu để tránh ghi đè file hỏng.
    /// Đây là cơ chế bảo vệ dữ liệu quan trọng.
    /// </remarks>
    private bool choPhepGhiDuLieu;

    // =========================================================================
    // CONSTRUCTOR
    // =========================================================================

    public MainForm()
    {
        // InitializeComponent(): do Designer.cs tạo tự động, khởi tạo các control
        InitializeComponent();
        DanhSach = new List<ThietBi>();

        // Bỏ qua đọc dữ liệu khi đang ở Design mode (Visual Studio),
        // tránh lỗi khi kéo thả giao diện
        if (System.ComponentModel.LicenseManager.UsageMode ==
            System.ComponentModel.LicenseUsageMode.Designtime)
            return;

        // Tham số "out": trả về nhiều giá trị cùng lúc từ phương thức
        if (ThuDocDuLieu(out List<ThietBi> danhSach, out string? loi))
        {
            DanhSach = danhSach;
            choPhepGhiDuLieu = true;
        }
        else
        {
            choPhepGhiDuLieu = false;
            ThongBaoLoiDocDuLieu(loi);
        }

        // Mặc định hiển thị M1 - Thêm mới khi khởi động
        HienThiThemMoi();
    }

    // =========================================================================
    // LƯU DỮ LIỆU
    // =========================================================================

    /// <summary>Lưu toàn bộ danh sách xuống data.json.</summary>
    /// <remarks>
    /// Sử dụng atomic write pattern (ghi file tạm → Move):
    /// đảm bảo data.json không bị hỏng nếu crash/mất điện giữa chừng.
    /// </remarks>
    /// <returns>true nếu lưu thành công.</returns>
    public bool LuuDuLieu()
    {
        if (!choPhepGhiDuLieu)
        {
            MessageBox.Show(
                "Dữ liệu ban đầu chưa được đọc thành công nên chức năng lưu " +
                "đã bị khóa để tránh ghi đè file hiện tại.\n" +
                "Hãy sửa hoặc khôi phục data.json rồi tải lại dữ liệu.",
                "Không thể lưu dữ liệu", MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return false;
        }

        string fileTam = DataFilePath + ".tmp";
        try
        {
            Directory.CreateDirectory(DataFolder);

            // Sắp xếp theo mã TTB trước khi lưu để file JSON nhất quán
            // LINQ Sắp xếp trước khi lưu JSON:
            // - OrderBy(): Sắp xếp chính theo phần số thứ tự được trích xuất từ Mã TTB (ví dụ "TTB005" -> 5) để thứ tự file JSON luôn nhất quán
            // - ThenBy(): Nếu phần số trùng nhau, sắp xếp phụ theo toàn bộ chuỗi Mã TTB (không phân biệt chữ hoa/thường)
            // - ToList(): Chuyển kết quả truy vấn thành danh sách List<ThietBi> mới để ghi file
            List<ThietBi> duLieuDeLuu = DanhSach
                .OrderBy(thietBi => LaySoThuTuMaTTB(thietBi.MaTTB))
                .ThenBy(thietBi => thietBi.MaTTB,
                    StringComparer.CurrentCultureIgnoreCase)
                .ToList();

            string json = JsonSerializer.Serialize(duLieuDeLuu, TuyChonJson);

            // Ghi file tạm trước, rồi Move → atomic write
            File.WriteAllText(fileTam, json);
            File.Move(fileTam, DataFilePath, true);

            // Reset trạng thái vì thứ tự dữ liệu đã thay đổi
            KhoaDaSapXep = null;
            XoaKetQuaTimKiem();
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể lưu data.json:\n{ex.Message}",
                "Lỗi lưu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
        finally
        {
            // finally: luôn chạy dù có lỗi hay không → dọn file tạm
            if (File.Exists(fileTam))
            {
                try { File.Delete(fileTam); } catch { }
            }
        }
    }

    /// <summary>Trích xuất số thứ tự từ mã TTB (VD: "TTB001" → 1).</summary>
    /// <remarks>
    /// Mã không hợp lệ → trả về int.MaxValue để xếp cuối.
    /// maTTB[tienTo.Length..]: range syntax, lấy chuỗi từ vị trí 3 trở đi.
    /// </remarks>
    private static int LaySoThuTuMaTTB(string maTTB)
    {
        const string tienTo = "TTB";
        // StartsWith: kiểm tra tiền tố, OrdinalIgnoreCase: không phân biệt hoa/thường
        // int.TryParse: cố parse chuỗi thành số, trả false nếu không parse được
        if (maTTB.StartsWith(tienTo, StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(maTTB[tienTo.Length..], out int soThuTu))
            return soThuTu;

        return int.MaxValue;
    }

    // =========================================================================
    // ĐỌC VÀ NẠP LẠI DỮ LIỆU
    // =========================================================================

    /// <summary>Nạp lại dữ liệu từ file data.json (gọi khi khôi phục backup).</summary>
    public void NapLaiDuLieu()
    {
        if (!ThuDocDuLieu(out List<ThietBi> danhSach, out string? loi))
        {
            choPhepGhiDuLieu = false;
            ThongBaoLoiDocDuLieu(loi);
            return;
        }

        // Clear() + AddRange() thay vì gán mới để giữ nguyên tham chiếu
        // (các UserControl khác đang giữ tham chiếu đến DanhSach)
        DanhSach.Clear();
        DanhSach.AddRange(danhSach);
        choPhepGhiDuLieu = true;
        KhoaDaSapXep = null;
        XoaKetQuaTimKiem();
    }

    /// <summary>Thử đọc dữ liệu từ data.json.</summary>
    /// <remarks>
    /// static: không phụ thuộc trạng thái đối tượng.
    /// Tham số "out": trả về danh sách + thông báo lỗi cùng lúc.
    /// </remarks>
    private static bool ThuDocDuLieu(
        out List<ThietBi> danhSach, out string? loi)
    {
        danhSach = [];
        loi = null;

        try
        {
            Directory.CreateDirectory(DataFolder);
            // File chưa tồn tại (lần chạy đầu) → trả danh sách rỗng, không lỗi
            if (!File.Exists(DataFilePath))
                return true;

            string json = File.ReadAllText(DataFilePath);
            // Toán tử "?? throw": nếu Deserialize trả null thì ném ngoại lệ
            List<ThietBi?> duLieuDoc =
                JsonSerializer.Deserialize<List<ThietBi?>>(json)
                ?? throw new InvalidDataException(
                    "Nội dung JSON không tạo được danh sách thiết bị.");

            KiemTraDuLieuDanhSach(duLieuDoc);
            danhSach = duLieuDoc.Select(thietBi => thietBi!).ToList();
            return true;
        }
        catch (Exception ex)
        {
            loi = ex.Message;
            return false;
        }
    }

    private static void ThongBaoLoiDocDuLieu(string? loi)
    {
        MessageBox.Show(
            $"Không thể đọc data.json:\n{loi}\n\n" +
            "Chức năng lưu đã bị khóa để bảo vệ dữ liệu hiện tại.",
            "Lỗi đọc dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }

    /// <summary>
    /// Kiểm tra tính hợp lệ của toàn bộ danh sách thiết bị khi đọc dữ liệu
    /// hoặc khôi phục backup.
    /// </summary>
    public static void KiemTraDuLieuDanhSach(
        IReadOnlyList<ThietBi?> danhSach,
        bool choPhepDanhSachRong = true)
    {
        if (!choPhepDanhSachRong && danhSach.Count == 0)
            throw new InvalidDataException(
                "Danh sách không chứa bản ghi thiết bị nào.");

        HashSet<string> cacMa =
            new(StringComparer.CurrentCultureIgnoreCase);
        HashSet<string> cacSoHieu =
            new(StringComparer.CurrentCultureIgnoreCase);

        for (int i = 0; i < danhSach.Count; i++)
        {
            ThietBi tb = danhSach[i]
                ?? throw LoiDuLieu(i, "Bản ghi có giá trị null.");

            KiemTraBatBuoc(i, "Mã TTB", tb.MaTTB);
            KiemTraBatBuoc(i, "Số hiệu", tb.SoHieu);
            KiemTraBatBuoc(i, "Tên thiết bị", tb.TenTTB);
            KiemTraBatBuoc(i, "Nguồn cấp", tb.NguonCap);
            KiemTraBatBuoc(i, "Chủng loại", tb.ChungLoai);

            string maTTB = tb.MaTTB.Trim();
            if (!maTTB.StartsWith("TTB",
                    StringComparison.OrdinalIgnoreCase) ||
                !int.TryParse(maTTB[3..], out int soThuTu) ||
                soThuTu <= 0)
                throw LoiDuLieu(i,
                    $"Mã TTB \"{tb.MaTTB}\" không đúng định dạng TTBxxx.");

            if (!cacMa.Add(maTTB))
                throw LoiDuLieu(i, $"Mã TTB \"{maTTB}\" bị trùng.");

            string soHieu = tb.SoHieu.Trim();
            if (!cacSoHieu.Add(soHieu))
                throw LoiDuLieu(i, $"Số hiệu \"{soHieu}\" bị trùng.");

            if (tb.SoLuong <= 0)
                throw LoiDuLieu(i, "Số lượng phải lớn hơn 0.");

            if (tb.Cap is < 1 or > 5)
                throw LoiDuLieu(i, "Cấp phải nằm trong khoảng từ 1 đến 5.");

            if (tb.NgaySanXuat == default)
                throw LoiDuLieu(i, "Ngày sản xuất không hợp lệ.");

            if (tb.NgayDuaVaoSuDung == default)
                throw LoiDuLieu(i, "Ngày đưa vào sử dụng không hợp lệ.");

            if (tb.NgayDuaVaoSuDung.Date < tb.NgaySanXuat.Date)
                throw LoiDuLieu(i,
                    "Ngày đưa vào sử dụng không được trước ngày sản xuất.");

            if (tb.NgaySanXuat.Date > DateTime.Today)
                throw LoiDuLieu(i,
                    "Ngày sản xuất không được lớn hơn ngày hiện tại.");

            if (tb.NgayDuaVaoSuDung.Date > DateTime.Today)
                throw LoiDuLieu(i,
                    "Ngày đưa vào sử dụng không được lớn hơn ngày hiện tại.");
        }
    }

    private static void KiemTraBatBuoc(
        int chiSo, string tenTruong, string? giaTri)
    {
        if (string.IsNullOrWhiteSpace(giaTri))
            throw LoiDuLieu(chiSo, $"{tenTruong} không được để trống.");
    }

    private static InvalidDataException LoiDuLieu(
        int chiSo, string noiDung) =>
        new($"Dữ liệu không hợp lệ tại bản ghi {chiSo + 1}: {noiDung}");

    // =========================================================================
    // QUẢN LÝ KẾT QUẢ TÌM KIẾM
    // =========================================================================

    /// <summary>Lưu kết quả tìm kiếm từ M4 để M2 hiển thị.</summary>
    public void LuuKetQuaTimKiem(List<ThietBi> ketQua,
        string khoa, string tuKhoa)
    {
        // LINQ ToList(): Tạo một bản sao danh sách kết quả tìm kiếm mới hoàn toàn (shallow copy) để tránh ảnh hưởng khi danh sách gốc bị chỉnh sửa hoặc sắp xếp lại
        KetQuaTimKiemM4 = ketQua.ToList();
        MoTaTimKiemM4 = $"{khoa}: \"{tuKhoa}\"";
    }

    /// <summary>Xóa kết quả tìm kiếm (gọi khi dữ liệu thay đổi).</summary>
    public void XoaKetQuaTimKiem()
    {
        KetQuaTimKiemM4 = null;
        MoTaTimKiemM4 = null;
    }

    // =========================================================================
    // ĐIỀU HƯỚNG GIAO DIỆN
    // =========================================================================

    /// <summary>Nạp UserControl vào vùng nội dung chính (panelContent).</summary>
    /// <remarks>
    /// Pattern "Single Page Application" trong WinForms:
    /// thay đổi nội dung panel thay vì mở nhiều form.
    /// Dock = Fill: UserControl tự chiếm toàn bộ diện tích panel.
    /// </remarks>
    public void LoadUserControl(UserControl uc)
    {
        panelContent.Controls.Clear();
        uc.Dock = DockStyle.Fill;
        panelContent.Controls.Add(uc);
    }

    /// <summary>Mở M1 - Thêm mới hoặc Cập nhật (nếu truyền thiết bị cần sửa).</summary>
    public void MoThemMoi(ThietBi? thietBiCansua = null)
    {
        ChonNut(btnM1);
        LoadUserControl(new UcThemMoi(this, thietBiCansua));
    }

    /// <summary>Expression-bodied method: viết gọn cho phương thức 1 dòng.</summary>
    private void HienThiThemMoi() => MoThemMoi();

    // =========================================================================
    // XỬ LÝ SỰ KIỆN CLICK SIDEBAR (M1 - M6)
    // =========================================================================
    private void btnM1_Click(object sender, EventArgs e) => MoThemMoi();

    private void btnM2_Click(object sender, EventArgs e)
    {
        ChonNut(btnM2);
        LoadUserControl(new UcDanhSach(this));
    }

    private void btnM3_Click(object sender, EventArgs e)
    {
        ChonNut(btnM3);
        LoadUserControl(new UcSapXep(this));
    }

    private void btnM4_Click(object sender, EventArgs e)
    {
        ChonNut(btnM4);
        LoadUserControl(new UcTimKiem(this));
    }

    private void btnM5_Click(object sender, EventArgs e)
    {
        ChonNut(btnM5);
        LoadUserControl(new UcThongKe(this));
    }

    /// <summary>M6 - Thoát: hỏi xác nhận trước khi đóng ứng dụng.</summary>
    private void btnM6_Click(object sender, EventArgs e)
    {
        DialogResult chon = MessageBox.Show("Bạn có chắc muốn thoát chương trình?",
            "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (chon == DialogResult.Yes) Close();
    }

    // =========================================================================
    // HIỆU ỨNG SIDEBAR
    // =========================================================================

    /// <summary>Tô sáng nút được chọn (viền trắng 2px), reset các nút khác.</summary>
    private void ChonNut(Button nutChon)
    {
        // Duyệt control con, "is Button button": pattern matching - kiểm tra kiểu + ép kiểu
        foreach (Control control in panelSidebar.Controls)
            if (control is Button button)
            {
                button.BackColor = Color.FromArgb(27, 94, 60);
                button.FlatAppearance.BorderSize = 0;
            }

        nutChon.BackColor = Color.FromArgb(27, 94, 60);
        nutChon.FlatAppearance.BorderColor = Color.White;
        nutChon.FlatAppearance.BorderSize = 2;
    }
}
