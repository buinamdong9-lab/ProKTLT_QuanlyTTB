using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

public partial class UcThongKe : UserControl
{
    public class ThongKeDong
    {
        public string Nhom { get; set; } = "";
        public int SoLuong { get; set; }
    }

    private MainForm? mainForm;

    public UcThongKe()
    {
        InitializeComponent();
    }

    public UcThongKe(MainForm mainForm)
        : this()
    {
        this.mainForm = mainForm;
        HienThiThongKe();
    }

    private void btnTaiLai_Click(object sender, EventArgs e) => HienThiThongKe();

    private void HienThiThongKe()
    {
        if (mainForm == null) return;
        lblTongLoaiValue.Text = TongSoLoai(mainForm.DanhSach).ToString();
        lblTongSoLuongValue.Text = TongSoLuong(mainForm.DanhSach).ToString();
        capBindingSource.DataSource =
            ChuyenThanhBang(GomNhom(mainForm.DanhSach, tb => $"Cấp {tb.Cap}"));
        chungLoaiBindingSource.DataSource =
            ChuyenThanhBang(GomNhom(mainForm.DanhSach, tb => tb.ChungLoai));
        nguonCapBindingSource.DataSource =
            ChuyenThanhBang(GomNhom(mainForm.DanhSach, tb => tb.NguonCap));
    }

    private static int TongSoLoai(List<ThietBi> danhSach)
    {
        HashSet<string> cacLoai =
            new(StringComparer.CurrentCultureIgnoreCase);
        foreach (ThietBi thietBi in danhSach)
        {
            string chungLoai = thietBi.ChungLoai.Trim();
            if (chungLoai.Length > 0)
                cacLoai.Add(chungLoai);
        }
        return cacLoai.Count;
    }

    private static int TongSoLuong(List<ThietBi> danhSach)
    {
        int tong = 0;
        foreach (ThietBi thietBi in danhSach)
            tong += thietBi.SoLuong;
        return tong;
    }

    private static Dictionary<string, int> GomNhom(
        List<ThietBi> danhSach, Func<ThietBi, string> layKhoa)
    {
        Dictionary<string, int> ketQua =
            new(StringComparer.CurrentCultureIgnoreCase);
        foreach (ThietBi thietBi in danhSach)
        {
            string khoa = layKhoa(thietBi);
            if (!ketQua.ContainsKey(khoa))
                ketQua[khoa] = 0;
            ketQua[khoa] += thietBi.SoLuong;
        }
        return ketQua;
    }

    private static List<ThongKeDong> ChuyenThanhBang(Dictionary<string, int> duLieu)
    {
        List<ThongKeDong> ketQua = [];
        foreach (KeyValuePair<string, int> dong in duLieu)
            ketQua.Add(new ThongKeDong { Nhom = dong.Key, SoLuong = dong.Value });
        return ketQua;
    }
}
