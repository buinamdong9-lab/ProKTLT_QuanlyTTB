// =============================================================================
// File: Models/ThietBi.cs
// Mô tả: Lớp mô hình dữ liệu (Model) đại diện cho một bản ghi trang thiết bị.
//         Được serialize/deserialize từ file JSON (data.json).
// =============================================================================

namespace QuanlyTTB.Models;

/// <summary>
/// Lớp ThietBi: mô hình dữ liệu cho một trang thiết bị.
/// Sử dụng auto-property với giá trị mặc định (chuỗi rỗng cho string, 
/// tránh null khi deserialize từ JSON).
/// </summary>
public class ThietBi
{
    /// <summary>Mã thiết bị duy nhất, định dạng TTBxxx (VD: TTB001).</summary>
    public string MaTTB { get; set; } = "";

    /// <summary>Số hiệu nội bộ, cũng phải duy nhất.</summary>
    public string SoHieu { get; set; } = "";

    /// <summary>Tên gọi mô tả thiết bị (VD: "Máy tính xách tay Dell").</summary>
    public string TenTTB { get; set; } = "";

    /// <summary>Ngày sản xuất, phải &lt;= NgayDuaVaoSuDung.</summary>
    public DateTime NgaySanXuat { get; set; }

    /// <summary>Ngày đưa vào sử dụng thực tế, phải &gt;= NgaySanXuat.</summary>
    public DateTime NgayDuaVaoSuDung { get; set; }

    /// <summary>Nơi cung cấp/mua thiết bị.</summary>
    public string NguonCap { get; set; } = "";

    /// <summary>Số lượng thiết bị, phải &gt; 0.</summary>
    public int SoLuong { get; set; }

    /// <summary>Phân loại nhóm thiết bị (VD: "Điện tử", "Cơ khí").</summary>
    public string ChungLoai { get; set; } = "";

    /// <summary>Cấp thiết bị từ 1 đến 5, thể hiện mức độ ưu tiên.</summary>
    public int Cap { get; set; }
}
