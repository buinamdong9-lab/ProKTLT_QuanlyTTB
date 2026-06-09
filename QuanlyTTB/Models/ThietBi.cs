namespace QuanlyTTB.Models;

public class ThietBi
{
    public string MaTTB { get; set; } = "";
    public string SoHieu { get; set; } = "";
    public string TenTTB { get; set; } = "";
    public DateTime NgaySanXuat { get; set; }
    public DateTime NgayDuaVaoSuDung { get; set; }
    public string NguonCap { get; set; } = "";
    public int SoLuong { get; set; }
    public string ChungLoai { get; set; } = "";
    public int Cap { get; set; }
}
