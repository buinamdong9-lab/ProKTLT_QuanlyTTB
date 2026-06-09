using QuanlyTTB.Models;

namespace QuanlyTTB.UserControls;

public partial class UcSapXep : UserControl
{
    private const int KichThuocTrang = 20;
    private MainForm? mainForm;
    private List<ThietBi> duLieuPhanTrang = [];
    private int trangHienTai = 1;

    public UcSapXep()
    {
        InitializeComponent();
    }

    public UcSapXep(MainForm mainForm): this()
    {
        this.mainForm = mainForm;
        cboThuatToan.SelectedIndex = 0;
        cboKhoa.SelectedIndex = 0;
        HienThi();
    }

    private void btnSapXep_Click(object sender, EventArgs e)
    {
        if (mainForm == null) return;
        string thuatToan = cboThuatToan.Text;
        string khoa = cboKhoa.Text;
        List<ThietBi> thuTuCu = mainForm.DanhSach.ToList();

        try
        {
            SapXep(mainForm.DanhSach, thuatToan, TaoHamSoSanh(khoa));
        }
        catch (ArgumentException ex)
        {
            MessageBox.Show(ex.Message, "Không thể sắp xếp",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        if (mainForm.LuuDuLieu())
        {
            mainForm.KhoaDaSapXep = khoa;
            HienThi();
            lblStatus.Text = $"Đã sắp xếp theo {khoa} bằng {thuatToan}.";
        }
        else
        {
            mainForm.DanhSach.Clear();
            mainForm.DanhSach.AddRange(thuTuCu);
            HienThi();
        }
    }

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
            _ => throw new ArgumentException("Khóa sắp xếp không hợp lệ.")
        };

    private static int SoSanhChuoi(string a, string b) =>
        string.Compare(a, b, StringComparison.CurrentCultureIgnoreCase);

    private static void SelectionSort<T>(List<T> danhSach, Comparison<T> soSanh)
    {
        for (int i = 0; i < danhSach.Count - 1; i++)
        {
            int viTriNhoNhat = i;
            for (int j = i + 1; j < danhSach.Count; j++)
                if (soSanh(danhSach[j], danhSach[viTriNhoNhat]) < 0)
                    viTriNhoNhat = j;

            if (viTriNhoNhat != i)
                (danhSach[i], danhSach[viTriNhoNhat]) =
                    (danhSach[viTriNhoNhat], danhSach[i]);
        }
    }

    private static void InsertionSort<T>(List<T> danhSach, Comparison<T> soSanh)
    {
        for (int i = 1; i < danhSach.Count; i++)
        {
            T giaTriChen = danhSach[i];
            int j = i - 1;

            while (j >= 0 && soSanh(danhSach[j], giaTriChen) > 0)
            {
                danhSach[j + 1] = danhSach[j];
                j--;
            }

            danhSach[j + 1] = giaTriChen;
        }
    }

    private static void BubbleSort<T>(List<T> danhSach, Comparison<T> soSanh)
    {
        for (int i = 0; i < danhSach.Count - 1; i++)
        {
            bool daDoiCho = false;
            for (int j = 0; j < danhSach.Count - i - 1; j++)
            {
                if (soSanh(danhSach[j], danhSach[j + 1]) <= 0)
                    continue;

                (danhSach[j], danhSach[j + 1]) =
                    (danhSach[j + 1], danhSach[j]);
                daDoiCho = true;
            }

            if (!daDoiCho)
                break;
        }
    }

    private static void QuickSort<T>(
        List<T> danhSach, int trai, int phai, Comparison<T> soSanh)
    {
        if (trai >= phai)
            return;

        int i = trai;
        int j = phai;
        T chot = danhSach[trai + (phai - trai) / 2];

        while (i <= j)
        {
            while (soSanh(danhSach[i], chot) < 0) i++;
            while (soSanh(danhSach[j], chot) > 0) j--;

            if (i <= j)
            {
                (danhSach[i], danhSach[j]) = (danhSach[j], danhSach[i]);
                i++;
                j--;
            }
        }

        if (trai < j) QuickSort(danhSach, trai, j, soSanh);
        if (i < phai) QuickSort(danhSach, i, phai, soSanh);
    }

    private static void MergeSort<T>(
        List<T> danhSach, int trai, int phai, Comparison<T> soSanh)
    {
        if (trai >= phai)
            return;

        int giua = trai + (phai - trai) / 2;
        MergeSort(danhSach, trai, giua, soSanh);
        MergeSort(danhSach, giua + 1, phai, soSanh);
        Tron(danhSach, trai, giua, phai, soSanh);
    }

    private static void Tron<T>(
        List<T> danhSach, int trai, int giua, int phai,
        Comparison<T> soSanh)
    {
        List<T> tam = new(phai - trai + 1);
        int i = trai;
        int j = giua + 1;

        while (i <= giua && j <= phai)
            tam.Add(soSanh(danhSach[i], danhSach[j]) <= 0
                ? danhSach[i++]
                : danhSach[j++]);

        while (i <= giua) tam.Add(danhSach[i++]);
        while (j <= phai) tam.Add(danhSach[j++]);

        for (int k = 0; k < tam.Count; k++)
            danhSach[trai + k] = tam[k];
    }

    private void HienThi()
    {
        if (mainForm == null) return;
        duLieuPhanTrang = mainForm.DanhSach;
        trangHienTai = 1;
        HienThiTrang();
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
