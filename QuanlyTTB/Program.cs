// =============================================================================
// File: Program.cs
// Mô tả: Điểm khởi đầu (entry point) của ứng dụng Windows Forms
//         "Hệ thống Quản lý Trang Thiết Bị".
// =============================================================================

namespace QuanlyTTB
{
    /// <summary>
    /// Lớp Program chứa phương thức Main() - điểm vào chính của ứng dụng.
    /// internal static: chỉ truy cập trong cùng assembly, không cần tạo đối tượng.
    /// </summary>
    internal static class Program
    {
        /// <summary>
        /// Phương thức Main - khởi chạy ứng dụng.
        /// [STAThread]: yêu cầu bắt buộc cho Windows Forms vì giao diện
        /// chỉ được thao tác từ một luồng duy nhất (Single Thread Apartment).
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Khởi tạo cấu hình ứng dụng (DPI cao, font mặc định, v.v.)
            ApplicationConfiguration.Initialize();

            // Application.Run() bắt đầu vòng lặp xử lý sự kiện (message loop)
            // và hiển thị MainForm. Ứng dụng kết thúc khi MainForm bị đóng.
            Application.Run(new MainForm());
        }
    }
}
