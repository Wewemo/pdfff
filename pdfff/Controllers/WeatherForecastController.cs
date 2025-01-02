using Microsoft.AspNetCore.Mvc;
using PdfSharpCore.Drawing;
using PdfSharpCore.Fonts;
using PdfSharpCore.Pdf;
using SkiaSharp;

namespace pdfff.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {

        [HttpGet("GeneratePdf")]
        public IActionResult GeneratePdf()
        {
            GlobalFontSettings.FontResolver = new CustomFontResolver();

            using (var ms = new MemoryStream())
            {
                // 初始化 PDF 文檔
                var pdf = new PdfDocument();
                var page = pdf.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                // 設定字型
                var fontRegular = new XFont("標楷體", 12, XFontStyle.Regular);
                var fontBold = new XFont("標楷體", 16, XFontStyle.Bold);
                var fontTitle = new XFont("標楷體", 20, XFontStyle.Bold);

                // 添加 Logo 圖片
                var logoPath = Path.Combine("wwwroot/images", "logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    var logoImage = XImage.FromFile(logoPath);
                    gfx.DrawImage(logoImage, 150, 30, 40, 40); // Logo 大小與位置
                }

                // 添加標題
                gfx.DrawString("啟富興業股份有限公司", fontTitle, XBrushes.Black, new XRect(50, 30, page.Width - 100, 20), XStringFormats.TopCenter);
                gfx.DrawString("詢價單", fontTitle, XBrushes.Black, new XRect(50, 60, page.Width - 100, 20), XStringFormats.TopCenter);

                // 添加詢價單號和日期
                gfx.DrawString("詢價單單號:IS20250102160301", fontRegular, XBrushes.Black, new XPoint(50, 90));
                gfx.DrawString("日期:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), fontRegular, XBrushes.Black, new XPoint(425, 90));

                // 添加公司資訊與供應商資訊
                gfx.DrawString("供應商資訊", fontBold, XBrushes.Black, new XPoint(50, 120));
                gfx.DrawString("名稱：測試2", fontRegular, XBrushes.Black, new XPoint(50, 140));
                gfx.DrawString("統一編號：00000000", fontRegular, XBrushes.Black, new XPoint(50, 160));
                gfx.DrawString("聯絡人：陳先生", fontRegular, XBrushes.Black, new XPoint(50, 180));
                gfx.DrawString("電話號碼：04-00000000#301", fontRegular, XBrushes.Black, new XPoint(50, 200));
                gfx.DrawString("傳真號碼：04-00000000", fontRegular, XBrushes.Black, new XPoint(50, 220));
                gfx.DrawString("地址：407台中市西屯區", fontRegular, XBrushes.Black, new XPoint(50, 240));

                gfx.DrawString("公司資訊", fontBold, XBrushes.Black, new XPoint(350, 120));
                gfx.DrawString("名稱：啟富興業股份有限公司", fontRegular, XBrushes.Black, new XPoint(350, 140));
                gfx.DrawString("統一編號：00153661", fontRegular, XBrushes.Black, new XPoint(350, 160));
                gfx.DrawString("聯絡人：李昱祺", fontRegular, XBrushes.Black, new XPoint(350, 180));
                gfx.DrawString("電話：04-25340972", fontRegular, XBrushes.Black, new XPoint(350, 200));
                gfx.DrawString("傳真號碼：-", fontRegular, XBrushes.Black, new XPoint(350, 220));
                gfx.DrawString("地址：台中市潭子區中山路三段493巷8號", fontRegular, XBrushes.Black, new XPoint(350, 240));

                // 添加詢價項目
                gfx.DrawString("詢價項目", fontBold, XBrushes.Black, new XPoint(50, 275));

                // 添加表格
                var tableStartY = 290;
                var colWidths = new[] { 40, 180, 50, 50, 50, 80 }; // 欄位寬度
                var headers = new[] { "項次", "品名規格", "厚度", "尺寸", "數量", "單價" };

                // 畫表頭
                double x = 50;
                foreach (var header in headers)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x, tableStartY, colWidths[Array.IndexOf(headers, header)], 20);
                    gfx.DrawString(header, fontRegular, XBrushes.Black, new XRect(x, tableStartY, colWidths[Array.IndexOf(headers, header)], 20), XStringFormats.Center);
                    x += colWidths[Array.IndexOf(headers, header)];
                }

                // 表格內容
                var items = new[]
                {
                    new { Index = 1, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 2, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 3, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 4, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 5, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 6, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 7, Name = "不鏽鋼/304/2B/黑色膜", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                };

                int rowIndex = 1;
                foreach (var item in items)
                {
                    x = 50;
                    double y = tableStartY + rowIndex * 20;

                    gfx.DrawRectangle(XPens.Black, x, y, colWidths[0], 20);
                    gfx.DrawString(item.Index.ToString(), fontRegular, XBrushes.Black, new XRect(x, y, colWidths[0], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0], y, colWidths[1], 20);
                    gfx.DrawString(item.Name, fontRegular, XBrushes.Black, new XRect(x + colWidths[0], y, colWidths[1], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1], y, colWidths[2], 20);
                    gfx.DrawString(item.Thickness, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1], y, colWidths[2], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2], y, colWidths[3], 20);
                    gfx.DrawString(item.Size, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2], y, colWidths[3], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3], y, colWidths[4], 20);
                    gfx.DrawString(item.Quantity, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3], y, colWidths[4], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4], y, colWidths[5], 20);
                    gfx.DrawString(item.Remark, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4], y, colWidths[5], 20), XStringFormats.Center);

                    rowIndex++;
                }

                // 添加交期、備註與注意事項的表格
                double notesTableY = tableStartY + (rowIndex + 0) * 20;
                var notes = new[]
                {
                    new { Title = "交期", Content = "2024-12-25", Height = 30.0 },
                    new { Title = "備註", Content = "請依標準規格處理", Height = 50.0 },
                    new { Title = "注意事項", Content = "出貨時請附檢驗報告", Height = 40.0 },
                };

                foreach (var note in notes)
                {
                    gfx.DrawRectangle(XPens.Black, 50, notesTableY, 80, note.Height);
                    gfx.DrawString(note.Title, fontRegular, XBrushes.Black,
                        new XRect(50, notesTableY, 80, note.Height), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 130, notesTableY, 370, note.Height);
                    gfx.DrawString(note.Content, fontRegular, XBrushes.Black,
                        new XRect(130, notesTableY, 400, note.Height), XStringFormats.CenterLeft);

                    notesTableY += note.Height; // 直接加上當前行的高度，確保下一行緊接著
                }

                // 添加頁碼（置中）
                gfx.DrawString($"-1-", fontRegular, XBrushes.Black, new XRect(0, page.Height - 50, page.Width, 20), XStringFormats.Center);


                // 保存 PDF
                pdf.Save(ms);
                return File(ms.ToArray(), "application/pdf", "Quote.pdf");
            }
        }
    }

    public class CustomFontResolver : IFontResolver
    {
        public string DefaultFontName => "標楷體"; // 預設字型為標楷體

        public byte[] GetFont(string faceName)
        {
            // 指定字型檔案路徑
            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts");

            if (faceName == "DFKai-SB")
            {
                return File.ReadAllBytes(Path.Combine(fontPath, "kaiu.ttf"));
            }
            throw new ArgumentException("字型名稱無效: " + faceName);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName == "標楷體")
            {
                // 標楷體無粗體和斜體版本
                return new FontResolverInfo("DFKai-SB");
            }
            throw new ArgumentException("字型無法解析: " + familyName);
        }
    }
}
