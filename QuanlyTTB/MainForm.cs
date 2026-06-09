using System.Text.Encodings.Web;
using System.Text.Json;
using QuanlyTTB.Models;
using QuanlyTTB.UserControls;

namespace QuanlyTTB;

public partial class MainForm : Form
{
    private static readonly JsonSerializerOptions TuyChonJson = new()
    {
        WriteIndented = true,
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    public const string DataFolder =
        @"D:\ProKTLT\QuanlyTTb\QuanlyTTB\Data";

    public static string DataFilePath =>
        Path.Combine(DataFolder, "data.json");

    public List<ThietBi> DanhSach { get; private set; }
    public string? KhoaDaSapXep { get; set; }
    public List<ThietBi>? KetQuaTimKiemM4 { get; private set; }
    public string? MoTaTimKiemM4 { get; private set; }
    private bool choPhepGhiDuLieu;

    public MainForm()
    {
        InitializeComponent();
        DanhSach = [];

        if (System.ComponentModel.LicenseManager.UsageMode ==
            System.ComponentModel.LicenseUsageMode.Designtime)
            return;

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

        HienThiThemMoi();
    }

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
            List<ThietBi> duLieuDeLuu = DanhSach
                .OrderBy(thietBi => LaySoThuTuMaTTB(thietBi.MaTTB))
                .ThenBy(thietBi => thietBi.MaTTB,
                    StringComparer.CurrentCultureIgnoreCase)
                .ToList();
            string json = JsonSerializer.Serialize(duLieuDeLuu, TuyChonJson);
            File.WriteAllText(fileTam, json);
            File.Move(fileTam, DataFilePath, true);
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
            if (File.Exists(fileTam))
            {
                try { File.Delete(fileTam); } catch { }
            }
        }
    }

    private static int LaySoThuTuMaTTB(string maTTB)
    {
        const string tienTo = "TTB";
        if (maTTB.StartsWith(tienTo, StringComparison.OrdinalIgnoreCase) &&
            int.TryParse(maTTB[tienTo.Length..], out int soThuTu))
            return soThuTu;

        return int.MaxValue;
    }

    public void NapLaiDuLieu()
    {
        if (!ThuDocDuLieu(out List<ThietBi> danhSach, out string? loi))
        {
            choPhepGhiDuLieu = false;
            ThongBaoLoiDocDuLieu(loi);
            return;
        }

        DanhSach.Clear();
        DanhSach.AddRange(danhSach);
        choPhepGhiDuLieu = true;
        KhoaDaSapXep = null;
        XoaKetQuaTimKiem();
    }

    private static bool ThuDocDuLieu(
        out List<ThietBi> danhSach, out string? loi)
    {
        danhSach = [];
        loi = null;

        try
        {
            Directory.CreateDirectory(DataFolder);
            if (!File.Exists(DataFilePath))
                return true;

            string json = File.ReadAllText(DataFilePath);
            danhSach = JsonSerializer.Deserialize<List<ThietBi>>(json)
                ?? throw new InvalidDataException(
                    "Nội dung JSON không tạo được danh sách thiết bị.");
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

    public void LuuKetQuaTimKiem(List<ThietBi> ketQua,
        string khoa, string tuKhoa)
    {
        KetQuaTimKiemM4 = ketQua.ToList();
        MoTaTimKiemM4 = $"{khoa}: \"{tuKhoa}\"";
    }

    public void XoaKetQuaTimKiem()
    {
        KetQuaTimKiemM4 = null;
        MoTaTimKiemM4 = null;
    }

    public void LoadUserControl(UserControl uc)
    {
        panelContent.Controls.Clear();
        uc.Dock = DockStyle.Fill;
        panelContent.Controls.Add(uc);
    }

    public void MoThemMoi(ThietBi? thietBiCansua = null)
    {
        ChonNut(btnM1);
        LoadUserControl(new UcThemMoi(this, thietBiCansua));
    }

    private void HienThiThemMoi() => MoThemMoi();

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

    private void btnM6_Click(object sender, EventArgs e)
    {
        DialogResult chon = MessageBox.Show("Bạn có chắc muốn thoát chương trình?",
            "Xác nhận thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (chon == DialogResult.Yes) Close();
    }

    private void ChonNut(Button nutChon)
    {
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
