// =============================================================================
// File: UserControls/UcDanhSach.cs
// Mô tả: Chức năng M2 - Hiển thị danh sách trang thiết bị.
//         Phân trang (20/trang), xóa, sửa, sao lưu, khôi phục, xuất CSV.
// =============================================================================

using System.Text;
using System.Text.Json;
using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

/// <summary>UcDanhSach - hiển thị và quản lý danh sách thiết bị với phân trang.</summary>
public partial class UcDanhSach : UserControl
{
    // =========================================================================
    // BIẾN THÀNH VIÊN
    // =========================================================================

    /// <summary>Thư mục backup. Expression-bodied property: tính giá trị mỗi lần truy cập.</summary>
    private static string BackupFolder =>
        Path.Combine(MainForm.DataFolder, "Backup");

    private const int KichThuocTrang = 20;
    private MainForm? mainForm;
    /// <summary>Dữ liệu đang hiển thị (toàn bộ hoặc kết quả tìm kiếm M4).</summary>
    private List<ThietBi> duLieuPhanTrang = new List<ThietBi>();
    private int trangHienTai = 1;

    // =========================================================================
    // CONSTRUCTORS
    // =========================================================================

    public UcDanhSach()
    {
        InitializeComponent();
    }

    public UcDanhSach(MainForm mainForm)
        : this()
    {
        this.mainForm = mainForm;
        TaiLai();
    }

    // =========================================================================
    // XỬ LÝ SỰ KIỆN
    // =========================================================================

    /// <summary>Tải lại: xóa kết quả tìm kiếm M4 để hiển thị toàn bộ danh sách.</summary>
    private void btnTaiLai_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        mainForm.XoaKetQuaTimKiem();
        TaiLai();
    }

    /// <summary>Xóa thiết bị được chọn, rollback nếu lưu thất bại.</summary>
    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        // Pattern matching "is not ThietBi tb": kiểm tra kiểu + ép kiểu cùng lúc
        // "?.": null-conditional operator, tránh lỗi NullReferenceException
        if (gridDanhSach.CurrentRow?.DataBoundItem is not ThietBi tb)
        {
            MessageBox.Show("Hãy chọn bản ghi cần xóa."); return;
        }
        if (MessageBox.Show($"Xóa thiết bị {tb.MaTTB}?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

        // Lưu vị trí cũ để rollback
        int viTriCu = mainForm.DanhSach.IndexOf(tb);
        if (viTriCu < 0)
            return;

        mainForm.DanhSach.RemoveAt(viTriCu);
        if (mainForm.LuuDuLieu())
        {
            TaiLai();
        }
        else
        {
            // Rollback: chèn lại vào đúng vị trí cũ
            mainForm.DanhSach.Insert(viTriCu, tb);
            TaiLai();
        }
    }

    /// <summary>Mở M1 chế độ cập nhật với thiết bị được chọn.</summary>
    private void btnSua_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        if (gridDanhSach.CurrentRow?.DataBoundItem is not ThietBi tb)
        {
            MessageBox.Show("Hãy chọn bản ghi cần sửa."); return;
        }
        mainForm.MoThemMoi(tb);
    }

    // =========================================================================
    // SAO LƯU VÀ KHÔI PHỤC
    // =========================================================================

    private void btnSaoLuu_Click(object sender, EventArgs e)
    {
        try
        {
            string fileBackup = SaoLuuDuLieu();
            MessageBox.Show($"Đã sao lưu dữ liệu:\n{fileBackup}", "Sao lưu thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể sao lưu dữ liệu:\n{ex.Message}", "Lỗi sao lưu",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    /// <summary>Khôi phục: chọn file backup → validate → ghi đè data.json → nạp lại.</summary>
    private void btnKhoiPhuc_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;

        // "using": tự động giải phóng tài nguyên (Dispose) khi ra khỏi block
        using OpenFileDialog hopThoai = new()
        {
            Title = "Chọn file backup",
            Filter = "File backup JSON (*.json)|*.json",
            InitialDirectory = BackupFolder
        };

        if (hopThoai.ShowDialog() != DialogResult.OK) return;

        DialogResult xacNhan = MessageBox.Show(
            "Dữ liệu hiện tại sẽ được thay bằng file backup. Bạn có tiếp tục?",
            "Xác nhận khôi phục", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (xacNhan != DialogResult.Yes) return;

        try
        {
            KhoiPhucDuLieu(hopThoai.FileName);
            mainForm.NapLaiDuLieu();
            TaiLai();
            MessageBox.Show("Đã khôi phục dữ liệu.", "Thành công",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể khôi phục dữ liệu:\n{ex.Message}",
                "Lỗi khôi phục", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // =========================================================================
    // XUẤT CSV
    // =========================================================================

    private void btnXuatExcel_Click(object sender, EventArgs e)
    {
        if (mainForm == null || duLieuPhanTrang.Count == 0)
        {
            MessageBox.Show("Không có dữ liệu để xuất.", "Xuất CSV",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
        }

        using SaveFileDialog hopThoai = new()
        {
            Title = "Xuất danh sách trang thiết bị",
            Filter = "File CSV mở bằng Excel (*.csv)|*.csv",
            // DateTime.Now:yyyyMMdd_HHmmss: string interpolation format
            FileName = $"DanhSachTrangThietBi_{DateTime.Now:yyyyMMdd_HHmmss}.csv",
            AddExtension = true,
            DefaultExt = "csv"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK) return;

        try
        {
            XuatDanhSachCsv(hopThoai.FileName, duLieuPhanTrang);
            MessageBox.Show($"Đã xuất file CSV:\n{hopThoai.FileName}",
                "Xuất CSV thành công", MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Không thể xuất file CSV:\n{ex.Message}",
                "Lỗi xuất CSV", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // =========================================================================
    // XỬ LÝ DỮ LIỆU
    // =========================================================================

    /// <summary>Tải dữ liệu: dùng kết quả M4 nếu có, ngược lại dùng toàn bộ.</summary>
    private void TaiLai()
    {
        if (mainForm == null) return;
        // Toán tử "??": dùng kết quả tìm kiếm nếu có, ngược lại dùng danh sách đầy đủ
        duLieuPhanTrang = mainForm.KetQuaTimKiemM4 ?? mainForm.DanhSach;
        trangHienTai = 1;
        HienThiTrang();
        lblCount.Text = mainForm.KetQuaTimKiemM4 == null
            ? $"Tổng số hồ sơ: {mainForm.DanhSach.Count}"
            : $"Kết quả M4 ({mainForm.MoTaTimKiemM4}): {duLieuPhanTrang.Count} hồ sơ";
    }

    /// <summary>Sao lưu data.json vào thư mục Backup (atomic write).</summary>
    private static string SaoLuuDuLieu()
    {
        Directory.CreateDirectory(BackupFolder);
        if (!File.Exists(MainForm.DataFilePath))
            throw new FileNotFoundException("Không tìm thấy data.json để sao lưu.");

        string tenFile = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.json";
        string fileBackup = Path.Combine(BackupFolder, tenFile);
        string fileTam = fileBackup + ".tmp";

        try
        {
            File.Copy(MainForm.DataFilePath, fileTam, true);
            File.Move(fileTam, fileBackup,true);
        }
        finally
        {
            if (File.Exists(fileTam))
                File.Delete(fileTam);
        }
        return fileBackup;
    }

    /// <summary>Khôi phục: đọc backup → validate → ghi đè data.json.</summary>
    private static void KhoiPhucDuLieu(string fileBackup)
    {
        Directory.CreateDirectory(MainForm.DataFolder);

        List<ThietBi?> danhSach =
            JsonSerializer.Deserialize<List<ThietBi?>>(
                File.ReadAllText(fileBackup))
            ?? throw new InvalidDataException(
                "File backup JSON không hợp lệ.");

        MainForm.KiemTraDuLieuDanhSach(
            danhSach, choPhepDanhSachRong: false);
        File.Copy(fileBackup, MainForm.DataFilePath, true);
    }

    // =========================================================================
    // XUẤT FILE CSV
    // =========================================================================

    /// <summary>Ghi danh sách ra CSV. UTF-8 có BOM để Excel nhận diện tiếng Việt.</summary>
    /// <remarks>"new UTF8Encoding(true)": tham số true = thêm BOM (Byte Order Mark).</remarks>
    private static void XuatDanhSachCsv(
        string filePath, List<ThietBi> danhSach)
    {
        using StreamWriter writer =
            new(filePath, false, new UTF8Encoding(true));
        writer.WriteLine(
            "Mã TTB,Số hiệu,Tên thiết bị,Ngày sản xuất,Ngày sử dụng,Nguồn cấp,Số lượng,Chủng loại,Cấp");

        foreach (ThietBi tb in danhSach)
        {
            string[] giaTri =
            [
                tb.MaTTB,
                tb.SoHieu,
                tb.TenTTB,
                tb.NgaySanXuat.ToString("dd/MM/yyyy"),
                tb.NgayDuaVaoSuDung.ToString("dd/MM/yyyy"),
                tb.NguonCap,
                tb.SoLuong.ToString(),
                tb.ChungLoai,
                tb.Cap.ToString()
            ];

            // LINQ Select(): Áp dụng hàm ChuanHoaO cho từng chuỗi trong mảng giaTri để chuẩn hóa (bọc dấu ngoặc kép, xử lý dấu nháy kép) trước khi nối lại bằng dấu phẩy
            writer.WriteLine(string.Join(',', giaTri.Select(ChuanHoaO)));
        }
    }

    /// <summary>Chuẩn hóa giá trị cho CSV: bọc ngoặc kép, escape " thành "" (RFC 4180).</summary>
    private static string ChuanHoaO(string giaTri)
    {
        string sach = giaTri.Replace("\r", " ").Replace("\n", " ");
        return $"\"{sach.Replace("\"", "\"\"")}\"";
    }

    // =========================================================================
    // PHÂN TRANG
    // =========================================================================

    /// <summary>Hiển thị trang hiện tại: tính toán dữ liệu, cập nhật label và nút.</summary>
    private void HienThiTrang()
    {
        // 1. Lấy tổng số bản ghi trong danh sách cần phân trang
        int tongBanGhi = duLieuPhanTrang.Count;

        // 2. Tính tổng số trang: Chia tổng bản ghi cho kích thước mỗi trang (20).
        // Phải ép kiểu sang (double) để thực hiện phép chia số thực, rồi dùng Math.Ceiling để làm tròn lên.
        // Math.Max(1, ...) đảm bảo tổng số trang luôn tối thiểu là 1 trang (ngay cả khi không có bản ghi nào).
        int tongTrang = Math.Max(1, (int)Math.Ceiling(tongBanGhi / (double)KichThuocTrang));

        // 3. Bảo vệ biến trangHienTai: Math.Clamp giới hạn trangHienTai luôn nằm trong đoạn [1, tongTrang],
        // ngăn chặn việc trang hiện tại bị nhỏ hơn 1 hoặc vượt quá tổng số trang hiện có.
        trangHienTai = Math.Clamp(trangHienTai,1, tongTrang);

        // 4. Lọc dữ liệu của trang hiện tại bằng LINQ:
        // - Skip(): Bỏ qua (trangHienTai - 1) * 20 bản ghi của các trang trước đó
        // - Take(): Lấy ra tối đa 20 bản ghi tiếp theo để hiển thị trên trang hiện tại
        // - ToList(): Thực thi truy vấn LINQ và chuyển kết quả thành danh sách List<ThietBi> thực tế
        List<ThietBi> duLieuTrang = duLieuPhanTrang
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
            (int)Math.Ceiling(duLieuPhanTrang.Count / (double)KichThuocTrang));
        HienThiTrang();
    }
}
