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
                // ��l�� PDF ����
                var pdf = new PdfDocument();
                var page = pdf.AddPage();
                var gfx = XGraphics.FromPdfPage(page);

                // �]�w�r��
                var fontRegular = new XFont("�з���", 12, XFontStyle.Regular);
                var fontBold = new XFont("�з���", 16, XFontStyle.Bold);
                var fontTitle = new XFont("�з���", 20, XFontStyle.Bold);

                // �K�[ Logo �Ϥ�
                var logoPath = Path.Combine("wwwroot/images", "logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    var logoImage = XImage.FromFile(logoPath);
                    gfx.DrawImage(logoImage, 150, 30, 40, 40); // Logo �j�p�P��m
                }

                // �K�[���D
                gfx.DrawString("�ҴI���~�ѥ��������q", fontTitle, XBrushes.Black, new XRect(50, 30, page.Width - 100, 20), XStringFormats.TopCenter);
                gfx.DrawString("�߻���", fontTitle, XBrushes.Black, new XRect(50, 60, page.Width - 100, 20), XStringFormats.TopCenter);

                // �K�[�߻��渹�M���
                gfx.DrawString("�߻���渹:IS20250102160301", fontRegular, XBrushes.Black, new XPoint(50, 90));
                gfx.DrawString("���:"+DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), fontRegular, XBrushes.Black, new XPoint(425, 90));

                // �K�[���q��T�P�����Ӹ�T
                gfx.DrawString("�����Ӹ�T", fontBold, XBrushes.Black, new XPoint(50, 120));
                gfx.DrawString("�W�١G����2", fontRegular, XBrushes.Black, new XPoint(50, 140));
                gfx.DrawString("�Τ@�s���G00000000", fontRegular, XBrushes.Black, new XPoint(50, 160));
                gfx.DrawString("�p���H�G������", fontRegular, XBrushes.Black, new XPoint(50, 180));
                gfx.DrawString("�q�ܸ��X�G04-00000000#301", fontRegular, XBrushes.Black, new XPoint(50, 200));
                gfx.DrawString("�ǯu���X�G04-00000000", fontRegular, XBrushes.Black, new XPoint(50, 220));
                gfx.DrawString("�a�}�G407�x������ٰ�", fontRegular, XBrushes.Black, new XPoint(50, 240));

                gfx.DrawString("���q��T", fontBold, XBrushes.Black, new XPoint(350, 120));
                gfx.DrawString("�W�١G�ҴI���~�ѥ��������q", fontRegular, XBrushes.Black, new XPoint(350, 140));
                gfx.DrawString("�Τ@�s���G00153661", fontRegular, XBrushes.Black, new XPoint(350, 160));
                gfx.DrawString("�p���H�G���R�R", fontRegular, XBrushes.Black, new XPoint(350, 180));
                gfx.DrawString("�q�ܡG04-25340972", fontRegular, XBrushes.Black, new XPoint(350, 200));
                gfx.DrawString("�ǯu���X�G-", fontRegular, XBrushes.Black, new XPoint(350, 220));
                gfx.DrawString("�a�}�G�x������l�Ϥ��s���T�q493��8��", fontRegular, XBrushes.Black, new XPoint(350, 240));

                // �K�[�߻�����
                gfx.DrawString("�߻�����", fontBold, XBrushes.Black, new XPoint(50, 275));

                // �K�[���
                var tableStartY = 290;
                var colWidths = new[] { 40, 180, 50, 50, 50, 80 }; // ���e��
                var headers = new[] { "����", "�~�W�W��", "�p��", "�ؤo", "�ƶq", "���" };

                // �e���Y
                double x = 50;
                foreach (var header in headers)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x, tableStartY, colWidths[Array.IndexOf(headers, header)], 20);
                    gfx.DrawString(header, fontRegular, XBrushes.Black, new XRect(x, tableStartY, colWidths[Array.IndexOf(headers, header)], 20), XStringFormats.Center);
                    x += colWidths[Array.IndexOf(headers, header)];
                }

                // ��椺�e
                var items = new[]
                {
                    new { Index = 1, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 2, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 3, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 4, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 5, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 6, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
                    new { Index = 7, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Quantity = "1", Remark = "" },
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

                // �K�[����B�Ƶ��P�`�N�ƶ������
                double notesTableY = tableStartY + (rowIndex + 0) * 20;
                var notes = new[]
                {
                    new { Title = "���", Content = "2024-12-25", Height = 30.0 },
                    new { Title = "�Ƶ�", Content = "�Ш̼зǳW��B�z", Height = 50.0 },
                    new { Title = "�`�N�ƶ�", Content = "�X�f�ɽЪ�������i", Height = 40.0 },
                };

                foreach (var note in notes)
                {
                    gfx.DrawRectangle(XPens.Black, 50, notesTableY, 80, note.Height);
                    gfx.DrawString(note.Title, fontRegular, XBrushes.Black,
                        new XRect(50, notesTableY, 80, note.Height), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, 130, notesTableY, 370, note.Height);
                    gfx.DrawString(note.Content, fontRegular, XBrushes.Black,
                        new XRect(130, notesTableY, 400, note.Height), XStringFormats.CenterLeft);

                    notesTableY += note.Height; // �����[�W��e�檺���סA�T�O�U�@��򱵵�
                }

                // �K�[���X�]�m���^
                gfx.DrawString($"-1-", fontRegular, XBrushes.Black, new XRect(0, page.Height - 50, page.Width, 20), XStringFormats.Center);


                // �O�s PDF
                pdf.Save(ms);
                return File(ms.ToArray(), "application/pdf", "Quote.pdf");
            }
        }
    }

    public class CustomFontResolver : IFontResolver
    {
        public string DefaultFontName => "�з���"; // �w�]�r�����з���

        public byte[] GetFont(string faceName)
        {
            // ���w�r���ɮ׸��|
            var fontPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "fonts");

            if (faceName == "DFKai-SB")
            {
                return File.ReadAllBytes(Path.Combine(fontPath, "kaiu.ttf"));
            }
            throw new ArgumentException("�r���W�ٵL��: " + faceName);
        }

        public FontResolverInfo ResolveTypeface(string familyName, bool isBold, bool isItalic)
        {
            if (familyName == "�з���")
            {
                // �з���L����M���骩��
                return new FontResolverInfo("DFKai-SB");
            }
            throw new ArgumentException("�r���L�k�ѪR: " + familyName);
        }
    }
}
