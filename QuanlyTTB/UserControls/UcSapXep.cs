// =============================================================================
// File: UserControls/UcSapXep.cs
// Mô tả: Chức năng M3 - Sắp xếp danh sách trang thiết bị.
//         5 thuật toán: Selection Sort, Insertion Sort, Bubble Sort,
//         Quick Sort, Merge Sort. Hỗ trợ chọn khóa + chiều sắp xếp.
//         Có Timer tạo hiệu ứng animation RadioButton.
// =============================================================================

using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;



/// <summary>UcSapXep - sắp xếp danh sách bằng 5 thuật toán kinh điển.</summary>
public partial class UcSapXep : UserControl
{
    // =========================================================================
    // BIẾN THÀNH VIÊN
    // =========================================================================
    private const int KichThuocTrang = 20;
    private MainForm? mainForm;
    private List<ThietBi> duLieuPhanTrang = new List<ThietBi>();
    private int trangHienTai = 1;

    // =========================================================================
    // CONSTRUCTORS
    // =========================================================================

    public UcSapXep()
    {
        InitializeComponent();
    }
   
    /// <summary>": this()" gọi constructor mặc định trước để khởi tạo giao diện.</summary>
    public UcSapXep(MainForm mainForm): this()
    {
        this.mainForm = mainForm;
        // SelectedIndex = 0: chọn mục đầu tiên trong ComboBox
        cboThuatToan.SelectedIndex = 0;
        cboKhoa.SelectedIndex = 0;
        HienThi();
    }

    // =========================================================================
    // NÚT SẮP XẾP - LOGIC CHÍNH
    // =========================================================================

    /// <summary>Sắp xếp danh sách, lưu file, rollback nếu thất bại.</summary>
    private void btnSapXep_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        string thuatToan = cboThuatToan.Text;
        string khoa = cboKhoa.Text;
        // LINQ ToList(): Tạo một bản sao danh sách mới hoàn toàn từ danh sách hiện tại (shallow copy) để dùng cho việc rollback (khôi phục lại thứ tự cũ nếu lưu file thất bại)
        List<ThietBi> thuTuCu = mainForm.DanhSach.ToList();

        try
        {
            bool tangDan = rdoTangDan.Checked || !rdoGiamDan.Checked;
            // Sắp xếp in-place (trực tiếp trên danh sách gốc)
            SapXep(mainForm.DanhSach, thuatToan, TaoHamSoSanh(khoa, tangDan));

            if (mainForm.LuuDuLieu())
            {
                // Ghi nhận khóa đã sắp xếp → cho phép tìm nhị phân (M4) theo khóa này
                mainForm.KhoaDaSapXep = khoa;
                HienThi();
                lblStatus.Text = $"Đã sắp xếp {khoa} theo chiều {(tangDan ? "Tăng dần" : "Giảm dần")} bằng {thuatToan}.";
            }

            else
            {
                // Rollback thứ tự cũ
                mainForm.DanhSach.Clear();
                mainForm.DanhSach.AddRange(thuTuCu);
                HienThi();
            }
        }



        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Không thể sắp xếp",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

    }

    // =========================================================================
    // PHÂN PHỐI THUẬT TOÁN
    // =========================================================================

    /// <summary>Chọn thuật toán sắp xếp theo tên. Generic &lt;T&gt; để dùng với mọi kiểu.</summary>
    /// <remarks>
    /// Comparison&lt;T&gt;: delegate so sánh 2 phần tử, trả về:
    ///   &lt; 0 nếu a &lt; b, 0 nếu a == b, &gt; 0 nếu a &gt; b.
    /// </remarks>
    private static void SapXep<T>(
        List<T> danhSach, string thuatToan, Comparison<T> soSanh)
    {
        switch (thuatToan)
        {
            case "Selection Sort":
                SelectionSort(danhSach, soSanh);
                break;
            case "Insertion Sort":
                InsertionSort(danhSach, soSanh);
                break;
            case "Bubble Sort":
                BubbleSort(danhSach, soSanh);
                break;
            case "Quick Sort":
                QuickSort(danhSach, 0, danhSach.Count - 1, soSanh);
                break;
            case "Merge Sort":
                MergeSort(danhSach, 0, danhSach.Count - 1, soSanh);
                break;
            default:
                throw new ArgumentException(
                    "Thuật toán sắp xếp không hợp lệ.");
        }
    }

    // =========================================================================
    // HÀM SO SÁNH
    // =========================================================================

    /// <summary>Tạo hàm so sánh theo khóa và chiều sắp xếp.</summary>
    /// <remarks>
    /// huong = 1 (tăng dần) giữ nguyên, -1 (giảm dần) đảo kết quả.
    /// Switch expression + lambda: trả về delegate so sánh phù hợp.
    /// </remarks>
    private static Comparison<ThietBi> TaoHamSoSanh(string khoa, bool tangDan)
    {

        int huong = tangDan ? 1 : -1;
        return khoa switch
        {
            "Mã TTB" => (a, b) => SoSanhChuoi(a.MaTTB, b.MaTTB) * huong,
            "Tên TTB" => (a, b) => SoSanhChuoi(a.TenTTB, b.TenTTB) * huong,
            "Ngày sản xuất" =>
                (a, b) => a.NgaySanXuat.CompareTo(b.NgaySanXuat) * huong,
            "Ngày đưa vào sử dụng" =>
                (a, b) => a.NgayDuaVaoSuDung.CompareTo(b.NgayDuaVaoSuDung) * huong,
            "Nguồn cấp" => (a, b) => SoSanhChuoi(a.NguonCap, b.NguonCap) * huong,
            "Chủng loại" => (a, b) => SoSanhChuoi(a.ChungLoai, b.ChungLoai) * huong,
            "Số lượng" => (a, b) => a.SoLuong.CompareTo(b.SoLuong) * huong,
            "Cấp" => (a, b) => a.Cap.CompareTo(b.Cap) * huong,
            // "_" là default case trong switch expression
            _ => throw new ArgumentException("Khóa sắp xếp không hợp lệ.")
        };
    }

    /// <summary>So sánh chuỗi không phân biệt hoa/thường (hỗ trợ tiếng Việt).</summary>
    private static int SoSanhChuoi(string a, string b) =>
        string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase);

    // =========================================================================
    // THUẬT TOÁN SẮP XẾP
    // =========================================================================

    /// <summary>
    /// Selection Sort (Sắp xếp chọn): tìm phần tử nhỏ nhất, đưa vào đầu.
    /// Độ phức tạp: O(n²) mọi trường hợp. Ưu điểm: ít hoán đổi (tối đa n-1).
    /// </summary>
    private static void SelectionSort<T>(List<T> danhSach, Comparison<T> soSanh)
    {
        for (int i = 0; i < danhSach.Count - 1; i++)
        {
            int viTriNhoNhat = i;
            // Tìm phần tử nhỏ nhất trong đoạn chưa sắp xếp [i+1..n-1]
            for (int j = i + 1; j < danhSach.Count; j++)
                if (soSanh(danhSach[j], danhSach[viTriNhoNhat]) < 0)
                    viTriNhoNhat = j;

            // Tuple deconstruction: (a, b) = (b, a) để hoán đổi giá trị
            if (viTriNhoNhat != i)
                (danhSach[i], danhSach[viTriNhoNhat]) =
                    (danhSach[viTriNhoNhat], danhSach[i]);
        }
    }

    /// <summary>
    /// Insertion Sort (Sắp xếp chèn): chèn phần tử vào vị trí đúng.
    /// Độ phức tạp: O(n²) xấu nhất, O(n) nếu đã gần sắp xếp.
    /// Giống cách sắp xếp bài trên tay.
    /// </summary>
    private static void InsertionSort<T>(List<T> danhSach, Comparison<T> soSanh)
    {
        for (int i = 1; i < danhSach.Count; i++)
        {
            T giaTriChen = danhSach[i];
            int j = i - 1;

            // Dịch các phần tử lớn hơn sang phải để tạo chỗ trống
            while (j >= 0 && soSanh(danhSach[j], giaTriChen) > 0)
            {
                danhSach[j + 1] = danhSach[j];
                j--;
            }

            danhSach[j + 1] = giaTriChen;
        }
    }

    /// <summary>
    /// Bubble Sort (Sắp xếp nổi bọt): hoán đổi cặp liền kề, phần tử lớn "nổi" lên cuối.
    /// Độ phức tạp: O(n²) xấu nhất, O(n) nếu đã sắp xếp (nhờ cờ daDoiCho).
    /// </summary>
    private static void BubbleSort<T>(List<T> danhSach, Comparison<T> soSanh)
    {
        for (int i = 0; i < danhSach.Count - 1; i++)
        {
            bool daDoiCho = false;
            // danhSach.Count - i - 1: i phần tử cuối đã đúng vị trí
            for (int j = 0; j < danhSach.Count - i - 1; j++)
            {
                if (soSanh(danhSach[j], danhSach[j + 1]) <= 0)
                    continue;

                (danhSach[j], danhSach[j + 1]) =
                    (danhSach[j + 1], danhSach[j]);
                daDoiCho = true;
            }

            // Không có hoán đổi → danh sách đã sắp xếp → dừng sớm
            if (!daDoiCho)
                break;
        }
    }

    /// <summary>
    /// Quick Sort (Sắp xếp nhanh) - đệ quy, phân hoạch Hoare.
    /// Chọn chốt (pivot) ở giữa, phân mảng thành 2 phần: nhỏ hơn và lớn hơn chốt.
    /// Độ phức tạp: O(n log n) trung bình, O(n²) xấu nhất.
    /// Chọn chốt ở giữa tránh worst case khi dữ liệu đã sắp xếp.
    /// </summary>
    private static void QuickSort<T>(
        List<T> danhSach, int left, int right, Comparison<T> soSanh)
    {
        // Điều kiện dừng đệ quy: đoạn có 0 hoặc 1 phần tử
        if (left >= right)
            return;

        int i = left;
        int j = right;
        // left + (right - left) / 2: tránh tràn số so với (left + right) / 2
        T pivot = danhSach[left + (right - left) / 2];

        // Phân hoạch: nhỏ sang trái, lớn sang phải
        while (i <= j)
        {
            while (soSanh(danhSach[i], pivot) < 0) i++;
            while (soSanh(danhSach[j], pivot) > 0) j--;

            if (i <= j)
            {
                (danhSach[i], danhSach[j]) = (danhSach[j], danhSach[i]);
                i++;
                j--;
            }
        }

        // Đệ quy sắp xếp 2 phần
        if (left < j) QuickSort(danhSach, left, j, soSanh);
        if (i < right) QuickSort(danhSach, i, right, soSanh);
    }


    /// <summary>
    /// Merge Sort (Sắp xếp trộn) - đệ quy.
    /// Chia mảng thành 2 nửa, đệ quy sắp xếp, rồi trộn lại.
    /// Độ phức tạp: O(n log n) mọi trường hợp. Stable sort (giữ thứ tự bằng nhau).
    /// Nhược điểm: cần thêm bộ nhớ O(n) cho mảng tạm.
    /// </summary>
    private static void MergeSort<T>(
        List<T> danhSach, int left, int right, Comparison<T> soSanh)
    {
        if (left >= right)
            return;
        int mid = left + (right - left) / 2;
        MergeSort(danhSach, left, mid, soSanh);
        MergeSort(danhSach, mid + 1, right, soSanh);
        Tron(danhSach, left, mid, right, soSanh);
    }

    /// <summary>Trộn 2 đoạn đã sắp xếp [trai..giua] và [giua+1..phai] thành 1 đoạn.</summary>
    private static void Tron<T>(
        List<T> danhSach, int trai, int giua, int phai,
        Comparison<T> soSanh)
    {
        List<T> tam = new(phai - trai + 1);
        int i = trai;       // Con trỏ đoạn trái
        int j = giua + 1;   // Con trỏ đoạn phải

        // So sánh đầu 2 đoạn, chọn phần tử nhỏ hơn
        while (i <= giua && j <= phai)
            tam.Add(soSanh(danhSach[i], danhSach[j]) <= 0
                ? danhSach[i++]
                : danhSach[j++]);

        // Sao chép phần còn lại
        while (i <= giua) tam.Add(danhSach[i++]);
        while (j <= phai) tam.Add(danhSach[j++]);

        // Ghi kết quả lại vào danh sách gốc
        for (int k = 0; k < tam.Count; k++)
            danhSach[trai + k] = tam[k];
    }

    // =========================================================================
    // HIỂN THỊ VÀ PHÂN TRANG
    // =========================================================================

    private void HienThi()
    {
        if (mainForm == null) return;
        duLieuPhanTrang = mainForm.DanhSach;
        trangHienTai = 1;
        HienThiTrang();
    }

    /// <summary>Logic phân trang: Skip/Take, cập nhật label và nút điều hướng.</summary>
    private void HienThiTrang()
    {
        // 1. Lấy tổng số bản ghi trong danh sách đã sắp xếp
        int tongBanGhi = duLieuPhanTrang.Count;

        // 2. Tính tổng số trang: Chia tổng bản ghi cho kích thước mỗi trang (20).
        // Phải ép kiểu sang (double) để thực hiện phép chia số thực, rồi dùng Math.Ceiling để làm tròn lên.
        // Math.Max(1, ...) đảm bảo tổng số trang luôn tối thiểu là 1 trang (ngay cả khi không có bản ghi nào).
        int tongTrang = Math.Max(1, (int)Math.Ceiling(tongBanGhi / (double)KichThuocTrang));

        // 3. Bảo vệ biến trangHienTai: Math.Clamp giới hạn trangHienTai luôn nằm trong đoạn [1, tongTrang],
        // ngăn chặn việc trang hiện tại bị nhỏ hơn 1 hoặc vượt quá tổng số trang hiện có.
        trangHienTai = Math.Clamp(trangHienTai, 1, tongTrang);

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
