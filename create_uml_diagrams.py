from pathlib import Path
import subprocess
from PIL import Image, ImageDraw, ImageFont


ROOT = Path(r"D:\ProKTLT\QuanlyTTB")
OUT = ROOT / "report_assets" / "uml"
DOT = Path(r"C:\Program Files\Graphviz\bin\dot.exe")

COMMON = r'''
graph [bgcolor="white", pad="0.25", nodesep="0.45", ranksep="0.65",
       fontname="Arial", labelloc="t", fontsize="25", fontcolor="#17365D"];
node [fontname="Arial", fontsize="12", color="#2E74B5", penwidth="1.8"];
edge [fontname="Arial", fontsize="10", color="#64748B", penwidth="1.5",
      arrowsize="0.8"];
'''

DIAGRAMS = {
    "01_use_case": r'''
digraph G {
''' + COMMON + r'''
label="USE CASE - HỆ THỐNG QUẢN LÝ TRANG THIẾT BỊ";
rankdir=LR;
actor [shape=box, style="rounded,filled", fillcolor="#EAF4EE",
       label="<<actor>>\nNgười quản lý", width=1.8, height=0.8];
subgraph cluster_system {
  label="HỆ THỐNG QUẢN LÝ TRANG THIẾT BỊ";
  color="#2E74B5"; penwidth=2; style="rounded";
  node [shape=ellipse, style="filled", width=2.65, height=0.72];
  m1 [label="M1 - Thêm / sửa\nhồ sơ thiết bị", fillcolor="#EAF4EE"];
  m2 [label="M2 - Xem danh sách\nvà phân trang", fillcolor="#E8EEF5"];
  m3 [label="M3 - Sắp xếp\n5 thuật toán", fillcolor="#E8EEF5"];
  m4 [label="M4 - Tìm kiếm\ntuần tự / nhị phân", fillcolor="#EAF4EE"];
  m5 [label="M5 - Thống kê", fillcolor="#F2F1FF"];
  m6 [label="M6 - Thoát", fillcolor="#E8EEF5"];
  del [label="Xóa thiết bị", fillcolor="#FFF6DF"];
  csv [label="Xuất CSV", fillcolor="#FFF6DF"];
  backup [label="Sao lưu JSON", fillcolor="#E8EEF5"];
  restore [label="Khôi phục JSON", fillcolor="#EAF4EE"];
  validate [label="Kiểm tra dữ liệu\nvà chống trùng", fillcolor="#F2F1FF"];
  save [label="Đọc / ghi data.json\nqua tệp tạm", fillcolor="#FFF6DF"];

  {rank=same; m1; del; validate}
  {rank=same; m2; csv; backup; restore}
  {rank=same; m3; m4; m5; m6}
  m1 -> validate [style=dashed, label="<<include>>"];
  validate -> save [style=dashed, label="<<include>>"];
  restore -> validate [style=dashed, label="<<include>>"];
}
actor -> m1 [arrowhead=none];
actor -> m2 [arrowhead=none];
actor -> m3 [arrowhead=none];
actor -> m4 [arrowhead=none];
actor -> m5 [arrowhead=none];
actor -> m6 [arrowhead=none];
actor -> del [arrowhead=none];
actor -> csv [arrowhead=none];
actor -> backup [arrowhead=none];
actor -> restore [arrowhead=none];
}
''',
    "02_class_diagram": r'''
digraph G {
''' + COMMON + r'''
label="CLASS DIAGRAM - CÁC LỚP CHÍNH";
rankdir=TB;
node [shape=plain];

ThietBi [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#EAF4EE"><B>ThietBi</B></TD></TR>
<TR><TD ALIGN="LEFT">+ MaTTB: string<BR/>+ SoHieu: string<BR/>+ TenTTB: string<BR/>
+ NgaySanXuat: DateTime<BR/>+ NgayDuaVaoSuDung: DateTime<BR/>
+ NguonCap: string<BR/>+ SoLuong: int<BR/>+ ChungLoai: string<BR/>+ Cap: int</TD></TR>
</TABLE>>];

MainForm [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#E8EEF5"><B>MainForm : Form</B></TD></TR>
<TR><TD ALIGN="LEFT">+ DanhSach: List&lt;ThietBi&gt;<BR/>+ KhoaDaSapXep: string?<BR/>
+ KetQuaTimKiemM4: List&lt;ThietBi&gt;?</TD></TR>
<TR><TD ALIGN="LEFT">+ LuuDuLieu(): bool<BR/>+ NapLaiDuLieu()<BR/>
+ LuuKetQuaTimKiem(...)<BR/>+ LoadUserControl(...)<BR/>+ MoThemMoi(...)</TD></TR>
</TABLE>>];

Them [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#EAF4EE"><B>UcThemMoi : UserControl</B></TD></TR>
<TR><TD ALIGN="LEFT">- mainForm: MainForm?<BR/>- thietBiDangSua: ThietBi?</TD></TR>
<TR><TD ALIGN="LEFT">- KiemTraDuLieu(): bool<BR/>- SaoChep(...): ThietBi<BR/>
- GanDuLieu(...)<BR/>- NapDuLieu(...)</TD></TR>
</TABLE>>];

DanhSach [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#FFF6DF"><B>UcDanhSach : UserControl</B></TD></TR>
<TR><TD ALIGN="LEFT">- duLieuPhanTrang: List&lt;ThietBi&gt;<BR/>- trangHienTai: int</TD></TR>
<TR><TD ALIGN="LEFT">- SaoLuuDuLieu(): string<BR/>- KhoiPhucDuLieu(...)<BR/>
- KiemTraDuLieuKhoiPhuc(...)<BR/>- XuatDanhSachCsv(...)</TD></TR>
</TABLE>>];

SapXep [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#E8EEF5"><B>UcSapXep : UserControl</B></TD></TR>
<TR><TD ALIGN="LEFT">- SapXep&lt;T&gt;(...)<BR/>- TaoHamSoSanh(...)</TD></TR>
<TR><TD ALIGN="LEFT">- SelectionSort&lt;T&gt;(...)<BR/>- InsertionSort&lt;T&gt;(...)<BR/>
- BubbleSort&lt;T&gt;(...)<BR/>- QuickSort&lt;T&gt;(...)<BR/>- MergeSort&lt;T&gt;(...)</TD></TR>
</TABLE>>];

TimKiem [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#EAF4EE"><B>UcTimKiem : UserControl</B></TD></TR>
<TR><TD ALIGN="LEFT">- ketQuaTimKiem: List&lt;ThietBi&gt;<BR/>- HauToTimKiem</TD></TR>
<TR><TD ALIGN="LEFT">- TimTuanTu(...)<BR/>- TimNhiPhan(...)<BR/>
- TimNhiPhanMotPhan(...)<BR/>- TaoChiMucHauTo(...)<BR/>- ChuanHoa(...)</TD></TR>
</TABLE>>];

ThongKe [label=<
<TABLE BORDER="1" CELLBORDER="0" CELLSPACING="0" CELLPADDING="7" COLOR="#2E74B5">
<TR><TD BGCOLOR="#F2F1FF"><B>UcThongKe : UserControl</B></TD></TR>
<TR><TD ALIGN="LEFT">+ ThongKeDong<BR/>- mainForm: MainForm?</TD></TR>
<TR><TD ALIGN="LEFT">- TongSoLoai(...)<BR/>- TongSoLuong(...)<BR/>
- GomNhom(...)<BR/>- ChuyenThanhBang(...)</TD></TR>
</TABLE>>];

MainForm -> ThietBi [label="quản lý 0..*", arrowhead=diamond];
MainForm -> Them [label="tạo / điều hướng", arrowhead=vee];
MainForm -> DanhSach [label="tạo / điều hướng", arrowhead=vee];
MainForm -> SapXep [label="tạo / điều hướng", arrowhead=vee];
MainForm -> TimKiem [label="tạo / điều hướng", arrowhead=vee];
MainForm -> ThongKe [label="tạo / điều hướng", arrowhead=vee];
Them -> ThietBi [label="thêm / sửa", arrowhead=vee];
DanhSach -> ThietBi [label="hiển thị / xóa", arrowhead=vee];
SapXep -> ThietBi [label="so sánh", arrowhead=vee];
TimKiem -> ThietBi [label="tìm", arrowhead=vee];
ThongKe -> ThietBi [label="tổng hợp", arrowhead=vee];
}
''',
    "03_activity_update": r'''
digraph G {
''' + COMMON + r'''
label="ACTIVITY DIAGRAM - THÊM / CẬP NHẬT HỒ SƠ";
rankdir=TB;
node [shape=box, style="rounded,filled", fillcolor="#E8EEF5",
      width=3.1, height=0.55];
start [shape=circle, label="", width=0.25, fillcolor="#17365D"];
input [label="Người dùng nhập thông tin", fillcolor="#EAF4EE"];
validate [label="Kiểm tra trường bắt buộc,\nđịnh dạng mã, số lượng và ngày"];
valid [shape=diamond, label="Hợp lệ?", fillcolor="#FFF6DF"];
duplicate [label="Kiểm tra trùng mã TTB\nvà số hiệu"];
dup [shape=diamond, label="Bị trùng?", fillcolor="#FFF6DF"];
copy [label="Tạo thiết bị mới hoặc\nsao chép dữ liệu cũ"];
update [label="Cập nhật List<ThietBi>", fillcolor="#EAF4EE"];
save [label="LuuDuLieu(): ghi data.json.tmp\nrồi thay data.json"];
saved [shape=diamond, label="Lưu thành công?", fillcolor="#FFF6DF"];
success [label="Thông báo thành công\nvà làm mới giao diện", fillcolor="#EAF4EE"];
rollback [label="Hoàn tác thêm / sửa\ntrong bộ nhớ", fillcolor="#FDECEC", color="#C62828"];
error [label="Hiển thị lỗi và đưa con trỏ\nvề trường cần sửa", fillcolor="#FDECEC", color="#C62828"];
end [shape=doublecircle, label="", width=0.28, fillcolor="#17365D"];

start -> input -> validate -> valid;
valid -> error [label="Không"];
error -> input [label="Sửa dữ liệu"];
valid -> duplicate [label="Có"];
duplicate -> dup;
dup -> error [label="Có"];
dup -> copy [label="Không"];
copy -> update -> save -> saved;
saved -> success [label="Có"];
success -> end;
saved -> rollback [label="Không"];
rollback -> end;
}
''',
    "04_sequence_search": r'''
digraph G {
''' + COMMON + r'''
label="SEQUENCE DIAGRAM - TÌM KIẾM THIẾT BỊ";
rankdir=TB;
node [shape=box, style="rounded,filled", fillcolor="#E8EEF5",
      width=2.2, height=0.55];
edge [arrowhead=vee];
u [label="Người dùng", fillcolor="#EAF4EE"];
ui [label="UcTimKiem"];
main [label="MainForm"];
list [label="List<ThietBi>", fillcolor="#FFF6DF"];
grid [label="DataGridView", fillcolor="#F2F1FF"];
{rank=same; u; ui; main; list; grid}

u -> ui [label="1. Chọn thuật toán, khóa,\nnhập từ khóa và bấm Tìm"];
ui -> ui [label="2. Kiểm tra từ khóa"];
ui -> main [label="3. Đọc KhoaDaSapXep"];
main -> ui [label="4. Trả trạng thái sắp xếp"];
ui -> list [label="5a. TimTuanTu(): duyệt tất cả\nhoặc\n5b. TimNhiPhan(): chia đôi"];
list -> ui [label="6. List<ThietBi> kết quả"];
ui -> main [label="7. LuuKetQuaTimKiem(...)"];
main -> ui [label="8. Đã lưu trạng thái M4"];
ui -> grid [label="9. Phân trang và gán DataSource"];
grid -> u [label="10. Hiển thị số lượng và kết quả"];

note [shape=note, label="Với khóa chuỗi:\n- ChuanHoa bỏ dấu, hạ chữ thường\n- Tìm tuần tự dùng Contains\n- Tìm nhị phân một phần dùng chỉ mục hậu tố",
      fillcolor="#FFF6DF"];
note -> ui [style=dashed, arrowhead=none];
}
''',
    "05_component": r'''
digraph G {
''' + COMMON + r'''
label="COMPONENT DIAGRAM - KIẾN TRÚC ỨNG DỤNG";
rankdir=TB;
node [shape=component, style="filled", width=3.0, height=0.8];

user [shape=plaintext, label="Người quản lý"];
main [label="MainForm\nĐiều hướng và trạng thái chung", fillcolor="#E8EEF5"];
controls [label="UserControls\nM1 · M2 · M3 · M4 · M5", fillcolor="#EAF4EE"];
model [label="Models\nThietBi", fillcolor="#F2F1FF"];
algorithms [label="Thuật toán\n5 sort · tuần tự · nhị phân · hậu tố", fillcolor="#E8EEF5"];
storage [label="Lưu trữ tệp\nSystem.Text.Json · File I/O", fillcolor="#FFF6DF"];
json [shape=folder, label="Data/data.json", fillcolor="#FFF6DF"];
backup [shape=folder, label="Data/Backup/*.json", fillcolor="#EAF4EE"];
csv [shape=note, label="DanhSachTrangThietBi_*.csv", fillcolor="#F2F1FF"];

user -> main [label="thao tác"];
main -> controls [label="LoadUserControl"];
main -> model [label="giữ List<ThietBi>"];
controls -> model [label="đọc / thay đổi"];
controls -> algorithms [label="gọi"];
main -> storage [label="đọc / ghi"];
controls -> storage [label="CSV / backup / restore"];
storage -> json [label="JSON"];
storage -> backup [label="sao lưu / khôi phục"];
storage -> csv [label="xuất"];
}
''',
}


def main():
    OUT.mkdir(parents=True, exist_ok=True)
    for name, source in DIAGRAMS.items():
        dot_path = OUT / f"{name}.dot"
        svg_path = OUT / f"{name}.svg"
        png_path = OUT / f"{name}.png"
        dot_path.write_text(source, encoding="utf-8")
        subprocess.run(
            [str(DOT), "-Tsvg", str(dot_path), "-o", str(svg_path)],
            check=True,
        )
        subprocess.run(
            [str(DOT), "-Tpng", "-Gdpi=180", str(dot_path), "-o", str(png_path)],
            check=True,
        )
        print(png_path)
    draw_sequence_diagram()


def draw_sequence_diagram():
    width, height = 1900, 1200
    image = Image.new("RGB", (width, height), "white")
    draw = ImageDraw.Draw(image)
    fonts = Path(r"C:\Windows\Fonts")
    title_font = ImageFont.truetype(str(fonts / "arial.ttf"), 43)
    head_font = ImageFont.truetype(str(fonts / "arialbd.ttf"), 23)
    body_font = ImageFont.truetype(str(fonts / "arial.ttf"), 19)
    small_font = ImageFont.truetype(str(fonts / "arial.ttf"), 17)
    navy = "#17365D"
    blue = "#2E74B5"
    line_color = "#64748B"
    fills = ["#EAF4EE", "#E8EEF5", "#E8EEF5", "#FFF6DF", "#F2F1FF"]

    draw.text(
        (width / 2, 45),
        "SEQUENCE DIAGRAM - TÌM KIẾM THIẾT BỊ",
        font=title_font,
        fill=navy,
        anchor="ma",
    )
    names = ["Người dùng", "UcTimKiem", "MainForm", "List<ThietBi>", "DataGridView"]
    xs = [160, 520, 900, 1280, 1660]
    top = 130
    for x, name, fill in zip(xs, names, fills):
        draw.rounded_rectangle(
            (x - 135, top, x + 135, top + 70),
            radius=13,
            fill=fill,
            outline=blue,
            width=3,
        )
        draw.text((x, top + 35), name, font=head_font, fill="black", anchor="mm")
        draw.line(
            (x, top + 70, x, height - 100),
            fill=line_color,
            width=2,
        )

    messages = [
        (0, 1, "1. Chọn thuật toán, khóa và nhập từ khóa"),
        (1, 1, "2. Kiểm tra từ khóa"),
        (1, 2, "3. Đọc KhoaDaSapXep"),
        (2, 1, "4. Trả trạng thái sắp xếp"),
        (1, 3, "5. Gọi TimTuanTu hoặc TimNhiPhan"),
        (3, 1, "6. Trả danh sách kết quả"),
        (1, 2, "7. LuuKetQuaTimKiem(...)"),
        (2, 1, "8. Xác nhận lưu trạng thái M4"),
        (1, 4, "9. Phân trang và gán DataSource"),
        (4, 0, "10. Hiển thị kết quả"),
    ]
    y = 270
    for source, target, label in messages:
        x1, x2 = xs[source], xs[target]
        if source == target:
            draw.line((x1, y, x1 + 105, y), fill=line_color, width=3)
            draw.line((x1 + 105, y, x1 + 105, y + 42), fill=line_color, width=3)
            draw.line((x1 + 105, y + 42, x1 + 8, y + 42), fill=line_color, width=3)
            draw.polygon(
                [(x1 + 8, y + 42), (x1 + 24, y + 34), (x1 + 24, y + 50)],
                fill=line_color,
            )
            draw.text((x1 + 120, y + 18), label, font=body_font, fill="black")
            y += 82
            continue
        draw.line((x1, y, x2, y), fill=line_color, width=3)
        direction = 1 if x2 > x1 else -1
        draw.polygon(
            [(x2, y), (x2 - 17 * direction, y - 8),
             (x2 - 17 * direction, y + 8)],
            fill=line_color,
        )
        draw.text(
            ((x1 + x2) / 2, y - 12),
            label,
            font=body_font,
            fill="black",
            anchor="ms",
            stroke_width=3,
            stroke_fill="white",
        )
        y += 82

    draw.rounded_rectangle(
        (1080, 1010, 1810, 1145),
        radius=14,
        fill="#FFF6DF",
        outline=blue,
        width=3,
    )
    note = (
        "Ghi chú với khóa chuỗi:\n"
        "ChuanHoa bỏ dấu và hạ chữ thường; tìm tuần tự dùng Contains;\n"
        "tìm nhị phân một phần sử dụng chỉ mục hậu tố."
    )
    draw.multiline_text(
        (1445, 1078),
        note,
        font=small_font,
        fill="black",
        anchor="mm",
        align="center",
        spacing=6,
    )
    png_path = OUT / "04_sequence_search.png"
    image.save(png_path, dpi=(180, 180))

    # Keep an editable SVG companion with the same clean layout.
    svg = [
        f'<svg xmlns="http://www.w3.org/2000/svg" width="{width}" height="{height}">',
        '<rect width="100%" height="100%" fill="white"/>',
        '<style>text{font-family:Arial}.title{font-size:43px;fill:#17365D}'
        '.head{font-size:23px;font-weight:bold}.body{font-size:19px}'
        '.small{font-size:17px}</style>',
        f'<text x="{width/2}" y="72" text-anchor="middle" class="title">'
        'SEQUENCE DIAGRAM - TÌM KIẾM THIẾT BỊ</text>',
    ]
    for x, name, fill in zip(xs, names, fills):
        svg.append(
            f'<rect x="{x-135}" y="{top}" width="270" height="70" rx="13" '
            f'fill="{fill}" stroke="{blue}" stroke-width="3"/>'
        )
        svg.append(
            f'<text x="{x}" y="{top+43}" text-anchor="middle" class="head">{name}</text>'
        )
        svg.append(
            f'<line x1="{x}" y1="{top+70}" x2="{x}" y2="{height-100}" '
            f'stroke="{line_color}" stroke-width="2"/>'
        )
    y = 270
    for source, target, label in messages:
        x1, x2 = xs[source], xs[target]
        if source == target:
            svg.append(
                f'<path d="M{x1} {y} H{x1+105} V{y+42} H{x1+8}" '
                f'fill="none" stroke="{line_color}" stroke-width="3"/>'
            )
            svg.append(
                f'<polygon points="{x1+8},{y+42} {x1+24},{y+34} '
                f'{x1+24},{y+50}" fill="{line_color}"/>'
            )
            svg.append(
                f'<text x="{x1+120}" y="{y+22}" class="body">{label}</text>'
            )
            y += 82
            continue
        direction = 1 if x2 > x1 else -1
        svg.append(
            f'<line x1="{x1}" y1="{y}" x2="{x2}" y2="{y}" '
            f'stroke="{line_color}" stroke-width="3"/>'
        )
        svg.append(
            f'<polygon points="{x2},{y} {x2-17*direction},{y-8} '
            f'{x2-17*direction},{y+8}" fill="{line_color}"/>'
        )
        svg.append(
            f'<text x="{(x1+x2)/2}" y="{y-12}" text-anchor="middle" '
            f'class="body">{label}</text>'
        )
        y += 82
    svg.append("</svg>")
    (OUT / "04_sequence_search.svg").write_text("\n".join(svg), encoding="utf-8")


if __name__ == "__main__":
    main()
