# HƯỚNG DẪN ĐỌC HIỂU MÃ NGUỒN

## 1. Mục đích tài liệu

Tài liệu này hướng dẫn cách đọc dự án quản lý trang thiết bị theo luồng thực
thi của chương trình. Mục tiêu là hiểu:

- Dữ liệu được đọc, giữ trong bộ nhớ và ghi ra file như thế nào.
- Các màn hình M1-M6 liên kết với nhau ra sao.
- Năm thuật toán sắp xếp được cài đặt như thế nào.
- Tìm kiếm tuần tự, nhị phân và tìm kiếm một phần hoạt động ra sao.
- Chương trình bảo vệ dữ liệu khi lưu hoặc khôi phục thất bại như thế nào.

## 2. Công nghệ và kiến trúc

Dự án sử dụng:

- C#.
- .NET 8.
- Windows Forms.
- JSON để lưu dữ liệu.
- CSV để xuất dữ liệu cho Excel.
- ZIP để sao lưu và khôi phục.

Kiến trúc của dự án tương đối đơn giản:

```text
Program.cs
    |
    v
MainForm
    |
    +-- M1: UcThemMoi
    +-- M2: UcDanhSach
    +-- M3: UcSapXep
    +-- M4: UcTimKiem
    +-- M5: UcThongKe
    +-- M6: Thoát
```

`MainForm` giữ danh sách dữ liệu dùng chung. Các `UserControl` nhận tham
chiếu đến `MainForm` để đọc hoặc thay đổi danh sách đó.

## 3. Cấu trúc thư mục

```text
QuanlyTTB/
├── Program.cs
├── MainForm.cs
├── MainForm.Designer.cs
├── Models/
│   └── ThietBi.cs
├── UserControls/
│   ├── UcThemMoi.cs
│   ├── UcDanhSach.cs
│   ├── UcSapXep.cs
│   ├── UcTimKiem.cs
│   └── UcThongKe.cs
└── Data/
    ├── data.json
    └── Backup/
```

Quy ước đọc file:

- File `.cs` chứa xử lý nghiệp vụ.
- File `.Designer.cs` chứa mã tạo giao diện.
- File `.resx` chứa tài nguyên giao diện.
- Không nên sửa thủ công `.Designer.cs` nếu không cần thiết.

## 4. Thứ tự đọc đề xuất

Đọc theo thứ tự sau để tránh phải quay lại nhiều lần:

1. `Program.cs`.
2. `Models/ThietBi.cs`.
3. `MainForm.cs`.
4. `UserControls/UcThemMoi.cs`.
5. `UserControls/UcDanhSach.cs`.
6. `UserControls/UcSapXep.cs`.
7. `UserControls/UcTimKiem.cs`.
8. `UserControls/UcThongKe.cs`.

## 5. Điểm bắt đầu: `Program.cs`

Phương thức `Main()` là điểm vào của ứng dụng:

```csharp
ApplicationConfiguration.Initialize();
Application.Run(new MainForm());
```

Lệnh đầu khởi tạo cấu hình Windows Forms. Lệnh thứ hai tạo `MainForm` và
bắt đầu vòng lặp giao diện.

## 6. Mô hình dữ liệu: `ThietBi.cs`

Lớp `ThietBi` biểu diễn một hồ sơ thiết bị:

| Thuộc tính | Kiểu | Nội dung |
|---|---|---|
| `MaTTB` | `string` | Mã trang thiết bị |
| `SoHieu` | `string` | Số hiệu thiết bị |
| `TenTTB` | `string` | Tên thiết bị |
| `NgaySanXuat` | `DateTime` | Ngày sản xuất |
| `NgayDuaVaoSuDung` | `DateTime` | Ngày đưa vào sử dụng |
| `NguonCap` | `string` | Đơn vị hoặc nguồn cấp |
| `SoLuong` | `int` | Số lượng |
| `ChungLoai` | `string` | Chủng loại |
| `Cap` | `int` | Cấp từ 1 đến 5 |

Một phần tử trong `data.json` tương ứng với một đối tượng `ThietBi`.

## 7. Trung tâm chương trình: `MainForm.cs`

### 7.1. Danh sách dùng chung

```csharp
public List<ThietBi> DanhSach { get; private set; }
```

`DanhSach` là dữ liệu chính trong bộ nhớ. M1-M5 đều thao tác trên danh sách
này.

Các trạng thái khác:

- `KhoaDaSapXep`: khóa mà M3 vừa sắp xếp.
- `KetQuaTimKiemM4`: kết quả gần nhất của M4.
- `MoTaTimKiemM4`: mô tả từ khóa và khóa tìm kiếm.
- `choPhepGhiDuLieu`: cho biết dữ liệu ban đầu có đọc thành công hay không.

### 7.2. Luồng khởi động

Constructor gọi `ThuDocDuLieu()`:

```text
Đọc data.json
    |
    +-- Thành công -> gán DanhSach, cho phép ghi
    |
    +-- Thất bại -> giữ DanhSach rỗng, khóa chức năng ghi
```

Việc khóa ghi rất quan trọng. Nếu JSON bị hỏng mà chương trình vẫn lưu danh
sách rỗng, file dữ liệu cũ có thể bị ghi đè.

### 7.3. `ThuDocDuLieu()`

Hàm này:

1. Tạo thư mục dữ liệu nếu chưa tồn tại.
2. Kiểm tra `data.json`.
3. Đọc toàn bộ nội dung.
4. Dùng `JsonSerializer.Deserialize<List<ThietBi>>()`.
5. Trả `true` khi thành công.
6. Trả `false` và nội dung lỗi khi thất bại.

Hàm không tự hiển thị `MessageBox`. Việc hiển thị lỗi được tách sang
`ThongBaoLoiDocDuLieu()`.

### 7.4. `LuuDuLieu()`

Trước tiên, hàm kiểm tra:

```csharp
if (!choPhepGhiDuLieu)
    return false;
```

Khi được phép lưu, luồng xử lý là:

```text
DanhSach
    -> tạo bản sao sắp theo phần số của MaTTB
    -> serialize thành JSON
    -> ghi data.json.tmp
    -> thay data.json bằng file tạm
```

JSON dùng `UnsafeRelaxedJsonEscaping` để tiếng Việt được ghi trực tiếp,
không chuyển thành chuỗi `\uXXXX`.

Sau khi lưu thành công:

- `KhoaDaSapXep` bị xóa.
- Kết quả tìm kiếm cũ bị xóa.

M3 sẽ đặt lại `KhoaDaSapXep` sau khi sắp xếp và lưu thành công.

### 7.5. `NapLaiDuLieu()`

Hàm chỉ thay `DanhSach` nếu đọc file thành công. Nếu đọc thất bại:

- Dữ liệu đang có trong bộ nhớ không bị xóa.
- Chức năng ghi bị khóa.
- Người dùng nhận thông báo lỗi.

### 7.6. Chuyển màn hình

`LoadUserControl()` thay nội dung của `panelContent`.

```text
btnM1 -> UcThemMoi
btnM2 -> UcDanhSach
btnM3 -> UcSapXep
btnM4 -> UcTimKiem
btnM5 -> UcThongKe
btnM6 -> đóng chương trình
```

## 8. M1: `UcThemMoi.cs`

M1 dùng chung cho cả thêm mới và cập nhật.

### 8.1. Phân biệt thêm và sửa

```csharp
bool laThemMoi = thietBiDangSua == null;
```

- `null`: tạo đối tượng mới.
- Khác `null`: sửa đối tượng đã chọn từ M2.

### 8.2. Luồng nút Lưu

`btnLuu_Click()` thực hiện:

1. Kiểm tra dữ liệu nhập.
2. Kiểm tra trùng mã TTB.
3. Kiểm tra trùng số hiệu.
4. Sao chép dữ liệu cũ nếu đang sửa.
5. Gán dữ liệu từ giao diện vào đối tượng.
6. Thêm đối tượng vào danh sách nếu là bản ghi mới.
7. Gọi `mainForm.LuuDuLieu()`.
8. Hoàn tác nếu lưu thất bại.

Khi kiểm tra trùng mã hoặc số hiệu, bản ghi đang sửa được bỏ qua bằng:

```csharp
!ReferenceEquals(tb, thietBiDangSua)
```

### 8.3. Kiểm tra đầu vào

`KiemTraDuLieu()` kiểm tra:

- Các trường bắt buộc không được rỗng.
- Số lượng phải là số nguyên lớn hơn 0.
- Ngày sử dụng không được trước ngày sản xuất.
- Cấp được giới hạn bởi `NumericUpDown`.

### 8.4. Hoàn tác khi lưu lỗi

Nếu thêm mới thất bại:

```csharp
mainForm.DanhSach.Remove(tb);
```

Nếu cập nhật thất bại, `GanDuLieu()` trả đối tượng về trạng thái được lưu bởi
`SaoChep()`.

## 9. M2: `UcDanhSach.cs`

### 9.1. Chọn nguồn hiển thị

```csharp
duLieuPhanTrang =
    mainForm.KetQuaTimKiemM4 ?? mainForm.DanhSach;
```

Nếu M4 có kết quả, M2 hiển thị kết quả tìm kiếm. Nếu không, M2 hiển thị toàn
bộ danh sách.

### 9.2. Phân trang

Mỗi trang có 20 bản ghi:

```csharp
duLieuPhanTrang
    .Skip((trangHienTai - 1) * KichThuocTrang)
    .Take(KichThuocTrang)
```

### 9.3. Sửa bản ghi

M2 lấy đối tượng được chọn trong `DataGridView`, sau đó gọi:

```csharp
mainForm.MoThemMoi(tb);
```

M1 nhận chính tham chiếu đó và chuyển sang chế độ cập nhật.

### 9.4. Xóa bản ghi

Luồng xóa:

1. Ghi nhớ vị trí đối tượng.
2. Xóa khỏi `DanhSach`.
3. Gọi `LuuDuLieu()`.
4. Chèn lại đúng vị trí nếu lưu thất bại.

### 9.5. Sao lưu

`SaoLuuDuLieu()` nén `data.json` thành file ZIP trong thư mục `Backup`.
File tạm có đuôi `.tmp` được tạo trước khi đổi thành file ZIP chính thức.

### 9.6. Khôi phục

`KhoiPhucDuLieu()`:

1. Giải nén ZIP vào thư mục tạm.
2. Kiểm tra có `data.json`.
3. Deserialize thành `List<ThietBi?>`.
4. Gọi `KiemTraDuLieuKhoiPhuc()`.
5. Chỉ thay dữ liệu chính khi toàn bộ bản ghi hợp lệ.

Bộ kiểm tra khôi phục từ chối:

- Danh sách rỗng.
- Bản ghi `null`.
- Trường bắt buộc rỗng.
- Mã TTB sai định dạng hoặc bị trùng.
- Số hiệu bị trùng.
- Số lượng không lớn hơn 0.
- Cấp ngoài khoảng 1-5.
- Ngày mặc định hoặc ngày sử dụng trước ngày sản xuất.

Thông báo lỗi ghi rõ số thứ tự bản ghi bị lỗi.

### 9.7. Xuất CSV

CSV được ghi bằng UTF-8 có BOM để Excel hiển thị đúng tiếng Việt.
`ChuanHoaO()`:

- Loại bỏ xuống dòng trong ô.
- Nhân đôi dấu `"`.
- Đặt toàn bộ giá trị trong dấu ngoặc kép.

## 10. M3: `UcSapXep.cs`

### 10.1. Luồng sắp xếp

```text
btnSapXep_Click
    -> lưu thứ tự cũ
    -> TaoHamSoSanh
    -> SapXep
    -> LuuDuLieu
    -> đặt KhoaDaSapXep
```

Nếu lưu thất bại, thứ tự cũ được khôi phục.

### 10.2. Hàm so sánh

`TaoHamSoSanh()` chuyển tên khóa trên giao diện thành
`Comparison<ThietBi>`.

Ví dụ:

```csharp
"Số lượng" => (a, b) => a.SoLuong.CompareTo(b.SoLuong)
```

Các thuật toán chỉ biết gọi hàm so sánh, không cần biết đang so sánh tên,
ngày hay số.

### 10.3. Năm thuật toán

#### Selection Sort

Mỗi vòng tìm phần tử nhỏ nhất trong đoạn chưa sắp xếp rồi đổi về đầu đoạn.

#### Insertion Sort

Lấy từng phần tử và dịch các phần tử lớn hơn sang phải để chèn vào đúng vị
trí.

#### Bubble Sort

So sánh từng cặp liền kề và đổi chỗ nếu sai thứ tự. Dừng sớm nếu một vòng
không có lần đổi chỗ nào.

#### Quick Sort

Chọn phần tử giữa làm chốt, đưa phần tử nhỏ sang trái, lớn sang phải rồi gọi
đệ quy cho hai phần.

#### Merge Sort

Chia danh sách thành hai nửa, sắp xếp từng nửa rồi trộn bằng `Tron()`.

Các hàm dùng generic:

```csharp
private static void SapXep<T>(...)
```

Do đó có thể tái sử dụng cho nhiều kiểu dữ liệu.

## 11. M4: `UcTimKiem.cs`

### 11.1. Điều kiện tìm nhị phân

M4 chỉ cho tìm nhị phân nếu:

```csharp
mainForm.KhoaDaSapXep == khoa
```

Nghĩa là người dùng phải sang M3 và sắp xếp đúng khóa trước.

### 11.2. Chuẩn hóa chuỗi

`ChuanHoa()`:

- Chuyển thành chữ thường.
- Tách dấu Unicode.
- Loại bỏ dấu tiếng Việt.
- Đổi `đ` thành `d`.
- Loại bỏ khoảng trắng thừa.

Ví dụ:

```text
"Súng AK47" -> "sung ak47"
```

### 11.3. Tìm kiếm tuần tự

`TimTuanTu()` duyệt toàn bộ danh sách. Nếu nhập nhiều từ, giá trị phải chứa
tất cả từ khóa:

```csharp
cacTuKhoa.All(giaTri.Contains)
```

Tìm tuần tự hỗ trợ tìm một phần và không cần nhập dấu.

### 11.4. Tìm kiếm nhị phân chính xác

Áp dụng cho:

- Ngày sản xuất.
- Ngày sử dụng.
- Số lượng.
- Cấp.

Thuật toán thu hẹp khoảng `[trai, phai]` cho đến khi tìm được vị trí đầu tiên
có giá trị bằng khóa. Sau đó duyệt tiếp để lấy tất cả bản ghi trùng khóa.

### 11.5. Tìm một phần chuỗi bằng chỉ mục hậu tố

Tìm nhị phân thông thường chỉ tìm được giá trị chính xác. Để tìm chuỗi con,
chương trình tạo mọi hậu tố của giá trị.

Ví dụ:

```text
"may tinh"
-> "may tinh"
-> "ay tinh"
-> "y tinh"
-> ...
-> "tinh"
```

`TaoChiMucHauTo()` tạo và sắp xếp các hậu tố.
`TimChiSoChuaTuKhoa()` dùng tìm kiếm nhị phân để tìm hậu tố bắt đầu bằng từ
khóa.

Với nhiều từ khóa, chương trình lấy giao của các tập kết quả:

```csharp
cacChiSoKhop.IntersectWith(chiSoKhopTuKhoa);
```

## 12. M5: `UcThongKe.cs`

Các hàm chính:

- `TongSoLoai()`: đếm số chủng loại khác nhau bằng `HashSet`.
- `TongSoLuong()`: cộng số lượng của mọi bản ghi.
- `GomNhom()`: cộng số lượng theo một khóa bằng `Dictionary`.
- `ChuyenThanhBang()`: chuyển từ điển thành danh sách để gắn vào bảng.

`GomNhom()` nhận hàm lấy khóa:

```csharp
GomNhom(danhSach, tb => $"Cấp {tb.Cap}")
GomNhom(danhSach, tb => tb.ChungLoai)
GomNhom(danhSach, tb => tb.NguonCap)
```

Một hàm được tái sử dụng cho ba loại thống kê.

## 13. Luồng dữ liệu tổng quát

```text
Data/data.json
      |
      v
MainForm.ThuDocDuLieu()
      |
      v
MainForm.DanhSach
      |
      +-- M1: thêm hoặc sửa
      +-- M2: hiển thị, xóa, backup, restore, CSV
      +-- M3: thay đổi thứ tự trong bộ nhớ
      +-- M4: tạo KetQuaTimKiemM4
      +-- M5: đọc để thống kê
      |
      v
MainForm.LuuDuLieu()
      |
      v
Data/data.json
```

## 14. Cách lần theo một nút bấm

Ví dụ muốn hiểu nút Sắp xếp:

1. Mở `UcSapXep.Designer.cs`.
2. Tìm:

```csharp
btnSapXep.Click += btnSapXep_Click;
```

3. Mở `UcSapXep.cs`.
4. Tìm `btnSapXep_Click()`.
5. Lần theo `TaoHamSoSanh()`.
6. Lần theo `SapXep()`.
7. Mở thuật toán được chọn.
8. Theo dõi lời gọi `mainForm.LuuDuLieu()`.

Áp dụng cách tương tự cho mọi nút trong dự án.

## 15. Các khái niệm C# cần nắm

- `List<T>`: danh sách động.
- `HashSet<T>`: tập hợp không trùng.
- `Dictionary<TKey, TValue>`: ánh xạ khóa và giá trị.
- `Comparison<T>`: hàm so sánh truyền vào thuật toán.
- Generic `<T>`: thuật toán dùng được cho nhiều kiểu.
- Lambda `=>`: viết hàm ngắn.
- LINQ: `Any`, `All`, `OrderBy`, `Skip`, `Take`, `Select`.
- Đệ quy: dùng trong Quick Sort và Merge Sort.
- Serialization: chuyển đối tượng thành JSON và ngược lại.
- Event handler: xử lý nút bấm và thay đổi lựa chọn.
- Reference equality: nhận biết chính đối tượng đang sửa.
- Rollback: hoàn tác dữ liệu khi lưu thất bại.

## 16. Gợi ý thực hành đọc mã

1. Đặt breakpoint tại constructor `MainForm`.
2. Theo dõi `ThuDocDuLieu()` đọc `data.json`.
3. Thêm một thiết bị và theo dõi `btnLuu_Click()`.
4. Thử nhập mã hoặc số hiệu trùng.
5. Đặt breakpoint trong từng thuật toán sắp xếp.
6. Theo dõi `trai`, `phai`, `giua` trong tìm kiếm nhị phân.
7. Kiểm tra kết quả `ChuanHoa("Súng AK47")`.
8. Theo dõi các hậu tố được tạo trong `TaoChiMucHauTo()`.
9. Thử khôi phục một backup có số lượng âm hoặc mã trùng.
10. Theo dõi `GomNhom()` khi thống kê theo chủng loại.

