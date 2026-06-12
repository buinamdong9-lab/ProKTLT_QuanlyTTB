// =============================================================================
// File: UserControls/UcThongKe.cs
// Mô tả: Chức năng M5 - Thống kê trang thiết bị.
//         Tổng số loại, tổng số lượng, bảng thống kê theo Cấp/Chủng loại/Nguồn cấp.
//         Dùng Dictionary để nhóm (grouping) và HashSet để đếm distinct.
// =============================================================================

using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

/// <summary>UcThongKe - hiển thị các bảng thống kê thiết bị.</summary>
public partial class  UcThongKe : UserControl
{
    /// <summary>Một dòng trong bảng thống kê, dùng làm DataSource cho DataGridView.</summary>
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

    /// <summary>Tính toán và gán dữ liệu thống kê cho các bảng.</summary>
    private void HienThiThongKe()
    {
        if (mainForm == null) return;
        lblTongLoaiValue.Text = TongSoLoai(mainForm.DanhSach).ToString();
        lblTongSoLuongValue.Text = TongSoLuong(mainForm.DanhSach).ToString();

        // Lambda "tb => $"Cấp {tb.Cap}"": hàm ẩn danh trích xuất khóa nhóm
        // Func<ThietBi, string>: delegate nhận ThietBi, trả về string
        capBindingSource.DataSource =
            ChuyenThanhBang(GomNhom(mainForm.DanhSach, tb => $"Cấp {tb.Cap}"));
        chungLoaiBindingSource.DataSource =
            ChuyenThanhBang(GomNhom(mainForm.DanhSach, tb => tb.ChungLoai));
        nguonCapBindingSource.DataSource =
            ChuyenThanhBang(GomNhom(mainForm.DanhSach, tb => tb.NguonCap));
    }

    // =========================================================================
    // CÁC HÀM TÍNH TOÁN
    // =========================================================================

    /// <summary>
    /// Đếm số chủng loại duy nhất (distinct).
    /// HashSet: tập hợp không trùng lặp, Add tự bỏ qua nếu đã tồn tại.
    /// O(n).
    /// </summary>
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

    /// <summary>Tổng cộng dồn SoLuong (không phải đếm bản ghi). O(n).</summary>
    private static int TongSoLuong(List<ThietBi> danhSach)
    {
        int tong = 0;
        foreach (ThietBi thietBi in danhSach)
            tong += thietBi.SoLuong;
        return tong;
    }

    /// <summary>
    /// Nhóm thiết bị theo tiêu chí và cộng dồn SoLuong.
    /// VD: layKhoa = tb => tb.ChungLoai → {"Điện tử": 7, "Cơ khí": 3}
    /// 
    /// Func&lt;ThietBi, string&gt; layKhoa: delegate nhận ThietBi trả về chuỗi khóa nhóm.
    /// Dictionary: ánh xạ khóa → giá trị, truy cập O(1).
    /// O(n).
    /// </summary>
    private static Dictionary<string, int> GomNhom(
        List<ThietBi> danhSach, Func<ThietBi, string> layKhoa)
    {
        Dictionary<string, int> ketQua =
            new(StringComparer.CurrentCultureIgnoreCase);
        foreach (ThietBi thietBi in danhSach)
        {
            string khoa = layKhoa(thietBi);
            // ContainsKey: kiểm tra khóa đã tồn tại chưa
            if (!ketQua.ContainsKey(khoa))
                ketQua[khoa] = 0;
            ketQua[khoa] += thietBi.SoLuong;
        }
        return ketQua;
    }

    /// <summary>
    /// Chuyển Dictionary → List&lt;ThongKeDong&gt; cho DataGridView.
    /// KeyValuePair&lt;K,V&gt;: cặp key-value khi duyệt Dictionary bằng foreach.
    /// </summary>
    private static List<ThongKeDong> ChuyenThanhBang(Dictionary<string, int> duLieu)
    {
        List<ThongKeDong> ketQua = [];
        foreach (KeyValuePair<string, int> dong in duLieu)
            ketQua.Add(new ThongKeDong { Nhom = dong.Key, SoLuong = dong.Value });
        return ketQua;
    }
}
