from pathlib import Path
from xml.sax.saxutils import escape

from PIL import Image, ImageDraw, ImageFont


ROOT = Path(r"D:\ProKTLT\QuanlyTTB")
ASSETS = ROOT / "report_assets"
PNG_PATH = ASSETS / "use_case_quan_ly_ttb.png"
SVG_PATH = ASSETS / "use_case_quan_ly_ttb.svg"

WIDTH = 1800
HEIGHT = 1250
NAVY = "#17365D"
BLUE = "#2E74B5"
LINE = "#64748B"
GREEN_FILL = "#EAF4EE"
BLUE_FILL = "#E8EEF5"
GOLD_FILL = "#FFF6DF"
PURPLE_FILL = "#F2F1FF"
WHITE = "#FFFFFF"


def font(name: str, size: int):
    return ImageFont.truetype(str(Path(r"C:\Windows\Fonts") / name), size)


FONT_TITLE = font("arial.ttf", 42)
FONT_HEADING = font("arialbd.ttf", 25)
FONT_BODY = font("arial.ttf", 22)
FONT_SMALL = font("arial.ttf", 18)


def ellipse_text(draw, box, lines, fill):
    draw.ellipse(box, fill=fill, outline=BLUE, width=3)
    cx = (box[0] + box[2]) / 2
    cy = (box[1] + box[3]) / 2
    spacing = 6
    heights = [draw.textbbox((0, 0), line, font=FONT_BODY)[3] for line in lines]
    total = sum(heights) + spacing * (len(lines) - 1)
    y = cy - total / 2
    for line, height in zip(lines, heights):
        draw.text((cx, y), line, font=FONT_BODY, fill="black", anchor="ma")
        y += height + spacing


def line(draw, start, end, dashed=False, arrow=False):
    if dashed:
        x1, y1 = start
        x2, y2 = end
        length = ((x2 - x1) ** 2 + (y2 - y1) ** 2) ** 0.5
        dash = 14
        gap = 9
        steps = int(length / (dash + gap)) + 1
        for index in range(steps):
            a = min(1, index * (dash + gap) / length)
            b = min(1, (index * (dash + gap) + dash) / length)
            draw.line(
                (
                    x1 + (x2 - x1) * a,
                    y1 + (y2 - y1) * a,
                    x1 + (x2 - x1) * b,
                    y1 + (y2 - y1) * b,
                ),
                fill=LINE,
                width=3,
            )
    else:
        draw.line((*start, *end), fill=LINE, width=3)

    if arrow:
        import math

        angle = math.atan2(end[1] - start[1], end[0] - start[0])
        size = 15
        points = [
            end,
            (
                end[0] - size * math.cos(angle - 0.55),
                end[1] - size * math.sin(angle - 0.55),
            ),
            (
                end[0] - size * math.cos(angle + 0.55),
                end[1] - size * math.sin(angle + 0.55),
            ),
        ]
        draw.polygon(points, fill=LINE)


def draw_png():
    ASSETS.mkdir(parents=True, exist_ok=True)
    image = Image.new("RGB", (WIDTH, HEIGHT), "white")
    draw = ImageDraw.Draw(image)

    draw.text(
        (WIDTH / 2, 55),
        "SƠ ĐỒ USE CASE HỆ THỐNG QUẢN LÝ TRANG THIẾT BỊ",
        font=FONT_TITLE,
        fill=NAVY,
        anchor="ma",
    )

    boundary = (360, 125, 1735, 1185)
    draw.rounded_rectangle(
        boundary, radius=24, fill=WHITE, outline=BLUE, width=4
    )
    draw.text(
        (1047, 150),
        "HỆ THỐNG QUẢN LÝ TRANG THIẾT BỊ",
        font=FONT_HEADING,
        fill=NAVY,
        anchor="ma",
    )

    # Actor
    actor_x = 170
    draw.ellipse((130, 230, 210, 310), outline=NAVY, width=4)
    draw.line((170, 310, 170, 455), fill=NAVY, width=4)
    draw.line((90, 355, 250, 355), fill=NAVY, width=4)
    draw.line((170, 455, 105, 545), fill=NAVY, width=4)
    draw.line((170, 455, 235, 545), fill=NAVY, width=4)
    draw.text(
        (actor_x, 585),
        "Người quản lý",
        font=FONT_HEADING,
        fill=NAVY,
        anchor="ma",
    )

    cases = {
        "update": ((455, 220, 820, 325), ["M1 - Cập nhật hồ sơ", "thêm / sửa thiết bị"], GREEN_FILL),
        "list": ((455, 380, 820, 485), ["M2 - Xem danh sách", "phân trang và thao tác"], BLUE_FILL),
        "sort": ((455, 540, 820, 645), ["M3 - Sắp xếp", "5 thuật toán, nhiều khóa"], BLUE_FILL),
        "search": ((455, 700, 820, 805), ["M4 - Tìm kiếm", "tuần tự / nhị phân"], GREEN_FILL),
        "stats": ((455, 860, 820, 965), ["M5 - Xem thống kê"], PURPLE_FILL),
        "exit": ((455, 1020, 820, 1125), ["M6 - Thoát chương trình"], BLUE_FILL),
        "delete": ((1010, 220, 1330, 325), ["Xóa thiết bị"], GOLD_FILL),
        "csv": ((1390, 220, 1690, 325), ["Xuất dữ liệu CSV"], GOLD_FILL),
        "backup": ((1010, 430, 1330, 535), ["Sao lưu dữ liệu", "ra tệp ZIP"], BLUE_FILL),
        "restore": ((1390, 430, 1690, 535), ["Khôi phục dữ liệu", "từ tệp ZIP"], GREEN_FILL),
        "validate": ((1010, 700, 1330, 805), ["Kiểm tra dữ liệu", "hợp lệ và không trùng"], PURPLE_FILL),
        "save": ((1390, 700, 1690, 805), ["Đọc / ghi data.json", "qua tệp tạm"], GOLD_FILL),
    }

    for box, labels, fill in cases.values():
        ellipse_text(draw, box, labels, fill)

    actor_targets = [
        (455, 272), (455, 432), (455, 592),
        (455, 752), (455, 912), (455, 1072),
    ]
    origins = [
        (250, 330), (250, 355), (250, 380),
        (250, 405), (250, 430), (235, 520),
    ]
    for origin, target in zip(origins, actor_targets):
        line(draw, origin, target)

    # Include relationships.
    include_edges = [
        ((820, 272), (1010, 752), "<<include>>"),
        ((1390, 482), (1330, 752), "<<include>>"),
        ((1330, 752), (1390, 752), "<<include>>"),
    ]
    for start, end, label in include_edges:
        line(draw, start, end, dashed=True, arrow=True)
        mx = (start[0] + end[0]) / 2
        my = (start[1] + end[1]) / 2
        draw.text(
            (mx, my - 12),
            label,
            font=FONT_SMALL,
            fill=LINE,
            anchor="ms",
            stroke_width=3,
            stroke_fill="white",
        )

    draw.text(
        (1045, 1205),
        "M1: cập nhật  |  M2: danh sách  |  M3: sắp xếp  |  "
        "M4: tìm kiếm  |  M5: thống kê  |  M6: thoát",
        font=FONT_SMALL,
        fill=LINE,
        anchor="ma",
    )

    image.save(PNG_PATH, dpi=(180, 180))


def svg_ellipse(cx, cy, rx, ry, fill, lines):
    text = []
    start_y = cy - (len(lines) - 1) * 15
    for index, value in enumerate(lines):
        text.append(
            f'<text x="{cx}" y="{start_y + index * 30}" '
            f'class="body" text-anchor="middle">{escape(value)}</text>'
        )
    return (
        f'<ellipse cx="{cx}" cy="{cy}" rx="{rx}" ry="{ry}" '
        f'fill="{fill}" stroke="{BLUE}" stroke-width="3"/>'
        + "".join(text)
    )


def draw_svg():
    # Editable companion version. The PNG is the primary report asset.
    svg = [
        f'<svg xmlns="http://www.w3.org/2000/svg" width="{WIDTH}" '
        f'height="{HEIGHT}" viewBox="0 0 {WIDTH} {HEIGHT}">',
        "<defs>",
        '<style>.title{font:42px Arial;fill:#17365D}.head{font:bold 25px Arial;'
        'fill:#17365D}.body{font:22px Arial;fill:#111}.small{font:18px Arial;'
        'fill:#64748B}.rel{stroke:#64748B;stroke-width:3;fill:none}</style>',
        '<marker id="arrow" markerWidth="10" markerHeight="10" refX="8" '
        'refY="3" orient="auto"><path d="M0,0 L0,6 L9,3 z" '
        'fill="#64748B"/></marker>',
        "</defs>",
        '<rect width="100%" height="100%" fill="white"/>',
        '<text x="900" y="75" class="title" text-anchor="middle">'
        "SƠ ĐỒ USE CASE HỆ THỐNG QUẢN LÝ TRANG THIẾT BỊ</text>",
        '<rect x="360" y="125" width="1375" height="1060" rx="24" '
        f'fill="white" stroke="{BLUE}" stroke-width="4"/>',
        '<text x="1047" y="175" class="head" text-anchor="middle">'
        "HỆ THỐNG QUẢN LÝ TRANG THIẾT BỊ</text>",
        f'<circle cx="170" cy="270" r="40" fill="white" stroke="{NAVY}" '
        'stroke-width="4"/>',
        f'<path d="M170 310 V455 M90 355 H250 M170 455 L105 545 '
        f'M170 455 L235 545" fill="none" stroke="{NAVY}" stroke-width="4"/>',
        '<text x="170" y="590" class="head" text-anchor="middle">'
        "Người quản lý</text>",
    ]
    for box, labels, fill in cases_for_svg().values():
        cx = (box[0] + box[2]) / 2
        cy = (box[1] + box[3]) / 2
        svg.append(svg_ellipse(cx, cy, (box[2] - box[0]) / 2,
                               (box[3] - box[1]) / 2, fill, labels))
    svg.append("</svg>")
    SVG_PATH.write_text("\n".join(svg), encoding="utf-8")


def cases_for_svg():
    return {
        "update": ((455, 220, 820, 325), ["M1 - Cập nhật hồ sơ", "thêm / sửa thiết bị"], GREEN_FILL),
        "list": ((455, 380, 820, 485), ["M2 - Xem danh sách", "phân trang và thao tác"], BLUE_FILL),
        "sort": ((455, 540, 820, 645), ["M3 - Sắp xếp", "5 thuật toán, nhiều khóa"], BLUE_FILL),
        "search": ((455, 700, 820, 805), ["M4 - Tìm kiếm", "tuần tự / nhị phân"], GREEN_FILL),
        "stats": ((455, 860, 820, 965), ["M5 - Xem thống kê"], PURPLE_FILL),
        "exit": ((455, 1020, 820, 1125), ["M6 - Thoát chương trình"], BLUE_FILL),
        "delete": ((1010, 220, 1330, 325), ["Xóa thiết bị"], GOLD_FILL),
        "csv": ((1390, 220, 1690, 325), ["Xuất dữ liệu CSV"], GOLD_FILL),
        "backup": ((1010, 430, 1330, 535), ["Sao lưu dữ liệu", "ra tệp ZIP"], BLUE_FILL),
        "restore": ((1390, 430, 1690, 535), ["Khôi phục dữ liệu", "từ tệp ZIP"], GREEN_FILL),
        "validate": ((1010, 700, 1330, 805), ["Kiểm tra dữ liệu", "hợp lệ và không trùng"], PURPLE_FILL),
        "save": ((1390, 700, 1690, 805), ["Đọc / ghi data.json", "qua tệp tạm"], GOLD_FILL),
    }


if __name__ == "__main__":
    draw_png()
    draw_svg()
    print(PNG_PATH)
    print(SVG_PATH)
