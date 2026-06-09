using System.IO.Compression;
using System.Text;
using System.Text.Json;
using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

public partial class UcDanhSach : UserControl
{
    private static string BackupFolder =>
        Path.Combine(MainForm.DataFolder, "Backup");

    private const int KichThuocTrang = 20;
    private MainForm? mainForm;
    private List<ThietBi> duLieuPhanTrang = [];
    private int trangHienTai = 1;

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

    private void btnTaiLai_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;

        mainForm.XoaKetQuaTimKiem();
        TaiLai();
    }

    private void btnXoa_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        if (gridDanhSach.CurrentRow?.DataBoundItem is not ThietBi tb)
        {
            MessageBox.Show("Hãy chọn bản ghi cần xóa."); return;
        }
        if (MessageBox.Show($"Xóa thiết bị {tb.MaTTB}?", "Xác nhận",
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;

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
            mainForm.DanhSach.Insert(viTriCu, tb);
            TaiLai();
        }
    }

    private void btnSua_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        if (gridDanhSach.CurrentRow?.DataBoundItem is not ThietBi tb)
        {
            MessageBox.Show("Hãy chọn bản ghi cần sửa."); return;
        }
        mainForm.MoThemMoi(tb);
    }

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

    private void btnKhoiPhuc_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;

        using OpenFileDialog hopThoai = new()
        {
            Title = "Chọn file backup",
            Filter = "File backup ZIP (*.zip)|*.zip",
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

    private void TaiLai()
    {
        if (mainForm == null) return;
        duLieuPhanTrang = mainForm.KetQuaTimKiemM4 ?? mainForm.DanhSach;
        trangHienTai = 1;
        HienThiTrang();
        lblCount.Text = mainForm.KetQuaTimKiemM4 == null
            ? $"Tổng số hồ sơ: {mainForm.DanhSach.Count}"
            : $"Kết quả M4 ({mainForm.MoTaTimKiemM4}): {duLieuPhanTrang.Count} hồ sơ";
    }

    private static string SaoLuuDuLieu()
    {
        Directory.CreateDirectory(BackupFolder);
        if (!File.Exists(MainForm.DataFilePath))
            throw new FileNotFoundException("Không tìm thấy data.json để sao lưu.");

        string tenFile = $"backup_{DateTime.Now:yyyyMMdd_HHmmss}.zip";
        string fileBackup = Path.Combine(BackupFolder, tenFile);
        string fileTam = fileBackup + ".tmp";

        try
        {
            using ZipArchive zip = ZipFile.Open(fileTam, ZipArchiveMode.Create);
            zip.CreateEntryFromFile(MainForm.DataFilePath, "data.json",
                CompressionLevel.Optimal);
            zip.Dispose();
            File.Move(fileTam, fileBackup, true);
        }
        finally
        {
            if (File.Exists(fileTam))
                File.Delete(fileTam);
        }

        return fileBackup;
    }

    private static void KhoiPhucDuLieu(string fileBackup)
    {
        Directory.CreateDirectory(MainForm.DataFolder);
        string thuMucTam = Path.Combine(MainForm.DataFolder,
            $"Restore_{Guid.NewGuid():N}");

        try
        {
            ZipFile.ExtractToDirectory(fileBackup, thuMucTam);
            string dataTam = Path.Combine(thuMucTam, "data.json");
            if (!File.Exists(dataTam))
                throw new InvalidDataException("File backup không có data.json.");

            List<ThietBi?> danhSach =
                JsonSerializer.Deserialize<List<ThietBi?>>(
                    File.ReadAllText(dataTam))
                ?? throw new InvalidDataException(
                    "data.json trong backup không hợp lệ.");

            KiemTraDuLieuKhoiPhuc(danhSach);

            File.Copy(dataTam, MainForm.DataFilePath, true);
        }
        finally
        {
            if (Directory.Exists(thuMucTam))
                Directory.Delete(thuMucTam, true);
        }
    }

    private static void KiemTraDuLieuKhoiPhuc(List<ThietBi?> danhSach)
    {
        if (danhSach.Count == 0)
            throw new InvalidDataException(
                "File backup không chứa bản ghi thiết bị nào.");

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
        new($"Dữ liệu backup không hợp lệ tại bản ghi {chiSo + 1}: " +
            noiDung);

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

            writer.WriteLine(string.Join(',', giaTri.Select(ChuanHoaO)));
        }
    }

    private static string ChuanHoaO(string giaTri)
    {
        string sach = giaTri.Replace("\r", " ").Replace("\n", " ");
        return $"\"{sach.Replace("\"", "\"\"")}\"";
    }

    private void HienThiTrang()
    {
        int tongBanGhi = duLieuPhanTrang.Count;
        int tongTrang = Math.Max(1, (int)Math.Ceiling(tongBanGhi / (double)KichThuocTrang));
        trangHienTai = Math.Clamp(trangHienTai, 1, tongTrang);

        List<ThietBi> duLieuTrang = duLieuPhanTrang
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
            (int)Math.Ceiling(duLieuPhanTrang.Count / (double)KichThuocTrang));
        HienThiTrang();
    }
}
