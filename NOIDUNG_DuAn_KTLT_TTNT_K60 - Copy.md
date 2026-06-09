

## 1

## THỰC TẬP
## KỸ THUẬT LẬP TRÌNH

## I. MỤC ĐÍCH, YÊU CẦU
Mục đích: Giúp học viên, sinh viên củng cố, hoàn thiện kiến thức, rèn luyện kỹ năng lập
trình; Tiếp cận giải quyết một bài toán thực tế với kỹ thuật lập trình.
Yêu cầu:
- Vận dụng các kiến thức về Kỹ thuật lập trình đã trang bị từ môn học Kỹ thuật
lập trình.
- Sử dụng công cụ lập trình: C, C++, C SHAP.
- Sử dụng một số cấu trúc như: mảng, danh sách liên kết để giải quyết bài toán, sử
dụng cấy trúc cây nhị phân tìm kiếm trong phần tìm kiếm, sử dụng một số cấu
trúc nâng cao.
- Input dữ liệu nhận được từ file.
- Kết quả thực hiện của mỗi tuần gồm: chương trình, báo cáo thu hoạch theo tuần
mô tả công việc đã làm (trên file word).
## II. THỜI GIAN VÀ ĐỊA ĐIỂM
- Thời gian : 6 Tuần
- Địa điểm: H9204, Phòng máy S6-523, Địa điểm tự học (ĐĐTH).
## III. NỘI DUNG THỰC TẬP
Xây dựng chương trình quản lý ĐIỂM.
- Tuần 1: Xây dựng khung chương trình và menu chọn
▪ Yêu cầu: Xây dựng khung chương trình và giao diện dạng menu với nội dung
như sau:
o Giao diện chính gồm các mục chọn:
- Thêm mới hồ sơ (M1)
- In danh sách (M2)
- Sắp xếp (M3)
- Tìm kiếm (M4)
- Thống kê (M5)
- Thoát (M6)
o Khi chọn M1, chương trình cho phép cập nhật vào thông tin gồm:
▪ Mã trang thiết bị (TTB)
▪ Số hiệu trang thiết bị
▪ Tên TTB

## 2

▪ Ngày sản xuất
▪ Ngày đưa vào sử dụng
▪ Nguồn cấp
▪ Số lượng
▪ Chủng loại
## ▪ Cấp (1,2,3,4,5)
Yêu cầu: Giao diện cập nhật thiết kế logic, có các tính năng kiểm tra tính
đúng đắn dữ liệu.
o Khi chọn M2 chương trình cho phép in ra danh sách TTB theo thứ tự đã
sắp xếp (khi chọn M3) và tìm kiếm (khi chọn M4)
o Khi chọn M3 chương trình cho phép chọn thuật toán sắp xếp (chọn,
chèn, nổi bọt, quicksort) và khóa để sắp xếp (Tên, ngày, chủng loại, đơn
vị cấp,...). Có thể xây dựng các mục chọn này dạng menu (cấp 2).
o Khi chọn M4 chương trình cho phép chọn thuật toán tìm kiếm (tuần tự,
nhị phân), khóa cần tìm kiếm được nhập vào. Có thể xây dựng các mục
chọn này dạng menu (cấp 2). (Chú ý trong trường hợp chưa sắp xếp
phải tìm kiếm thuần tự, trong trường hợp đã sắp xếp mới tìm kiếm nhị
phân. Các trường khóa sắp xếp cho riêng vào tập chỉ mục lưu)
o Khi chọn M5 chương trình cho phép thống kê, báo cáo theo các tiêu chí.
Có thể xây dựng các mục chọn này dạng menu (cấp 2).
o Khi chọn M6 chương trình kết thúc.
▪ Kết quả:
o Chương trình chạy và cho phép NSD chọn lựa được các chức năng theo
yêu cầu đặt ra;
o Tổ chức chương trình gồm: Chương  trình  chính, khung các chương
trình con (hàm) để thực hiện từng chức năng tương ứng với hệ thống
menu đặt ra.
- Tuần 2: Xây dựng cấu trúc dữ liệu và các hàm nhập/xuất dữ liệu
▪ Yêu cầu: Xây dựng các cấu trúc dữ liệu phù hợp để quản lý đối tượng của bài
toán, viết các hàm thực hiện việc cập nhật hồ sơ, đọc/ghi dữ liệu từ file.
▪ Kiến thức liên quan:
o Các cấu trúc dữ liệu do người dùng định nghĩa;
o Thao tác đọc/ghi file;
o Viết hàm thực hiện các chức năng.
▪ Kết quả:

## 3

o Chương trình chạy và cho phép NSD cập nhật được hồ sơ, in được danh
sách (toàn bộ);
o Hồ sơ đối tượng được định nghĩa theo cấu trúc và được lưu trữ trên file.
- Tuần 3: Thực hiện các thuật toán sắp xếp
▪ Yêu cầu: Thực hiện các thuật toán xử lý, sắp xếp danh sách đối tượng của bài
toán. Khóa được dùng để sắp xếp là số, xâu ký tự, ngày tháng ...
▪ Kiến thức liên quan: Xử lý xâu ký tự, các thuật toán sắp xếp.
▪ Kết quả:
o Chương trình chạy và cho phép NSD lựa chọn thuật toán sắp xếp.
o Chương trình chạy và cho phép NSD lựa chọn khóa để sắp xếp.
o In danh sách sau khi đã sắp xếp.
- Tuần 4: Thực hiện các thuật toán tìm kiếm
▪ Yêu cầu: Thực hiện các thuật toán tìm kiếm, in danh sách theo điều kiện được
đưa vào. Khóa được dùng để tìm kiếm là số, xâu ký tự, ngày tháng ...
▪ Kiến thức liên quan:
o Xử lý xâu ký tự.
o Các thuật toán tìm kiếm.
▪ Kết quả:
o Chương trình chạy và cho phép NSD lựa chọn thuật toán tìm kiếm.
o Chương trình chạy và cho phép NSD lựa chọn khóa cần tìm kiếm.
o In danh sách sau khi đã tìm kiếm.
- Tuần 5: Thực hiện các báo cáo thống kê
▪ Yêu cầu: Thực hiện các thuật toán tìm kiếm, in danh sách theo điều kiện nào
đó. Khóa được dùng để tìm kiếm là số, xâu ký tự, ngày tháng ...
▪ Kiến thức liên quan:
o Xử lý chuỗi
o Các thuật toán tìm kiếm.
▪ Kết quả:
o Chương trình chạy và cho phép NSD lựa chọn thuật toán tìm kiếm.
o Chương trình chạy và cho phép NSD lựa chọn khóa cần tìm kiếm.
o In danh sách sau khi đã tìm kiếm.
- Tuần 6: Hoàn thiện chương trình và báo cáo
▪ Yêu cầu: Hoàn thiện chương trình, viết báo cáo và trình bày kết quả
▪ Kết quả: Chương trình hoàn thiện các chức năng đặt ra.



## 4

## IV. KẾ HOẠCH THỰC TẬP
Tuần Nội dung
## Số
tiết
## Hình
thức
huấn
luyện
Địa điểm
## Ghi
chú
## 1
Phổ biến kế hoạc chung và trao đổi
Nội dung 1 - Xây dựng khung chương
trình và menu chọn
6 LT+THs H9204

Thực hiện Nội dung 1 - Xây dựng
khung chương trình và menu chọn
## 24
Tự học
Tự bố trí,
## S6-523


Thảo luận Nội dung 1 và triển khai Nội
dung 2 - Xây dựng cấu trúc dữ liệu và
các hàm nhập/xuất dữ liệu
## 6 LT+TH H9204
## 2
Thực hiện Nội dung 2 - Xây dựng cấu
trúc dữ liệu và các hàm nhập/xuất dữ
liệu
24 Tự học
Tự bố trí,
## S6-523


Thảo luận Nội dung 2 và triển khai Nội
dung 3 - Cài đặt các thuật toán sắp xếp
## 6
## LT+TH H9204
## 3
Thực hiện Nội dung 3 - Cài đặt các
thuật toán sắp xếp
24 Tự học
Tự bố trí,
## S6-523


Thảo luận Nội dung 3 và triển khai Nội
dung 4 - Cài đặt các thuật toán tìm
kiếm
## 6
## LT+TH H9204
## 4
Thực hiện Nội dung 4 - Cài đặt các
thuật toán tìm kiếm
24 Tự học S6-523, HT

Thảo luận Nội dung 4 và triển khai Nội
dung 5 - Xây dựng các báo cáo thống
kê
## 6
Thảo luận H9204
## 5
Thực hiện Nội dung 5 - Xây dựng các
báo cáo thống kê
24 Tự học S6-523, HT

Thảo luận ND5 về các báo cáo thống
kê, truy xuất thông tin, bố trí thông tin
## 6
Thảo luận H9204
## 6
Hoàn thiện báo cáo, kiểm tra

Hoàn thiện chương trình, báo cáo 18 Tự học S6-523, HT
Kiểm tra 6 Vấn đáp H9204

Kiểm tra 6
Vấn đáp H9204
