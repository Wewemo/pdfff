using Microsoft.AspNetCore.Mvc;
using PdfSharp.Drawing;
using PdfSharp.Fonts;
using PdfSharp.Pdf;

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
                var fontRegular = new XFont("�з���", 12);
                var fontBold = new XFont("�з���", 16);
                var fontTitle = new XFont("�з���", 20);

                // �K�[ Logo �Ϥ�
                var logoPath = Path.Combine("wwwroot/images", "logo.png");
                if (System.IO.File.Exists(logoPath))
                {
                    var logoImage = XImage.FromFile(logoPath);
                    gfx.DrawImage(logoImage, 150, 30, 40, 40); // Logo �j�p�P��m
                }

                // �K�[���D
                gfx.DrawString("�ҴI���~�ѥ��������q", fontTitle, XBrushes.Black, new XRect(50, 30, page.Width - 100, 20), XStringFormats.TopCenter);
                gfx.DrawString("�i�f(���ʳ�)", fontTitle, XBrushes.Black, new XRect(50, 60, page.Width - 100, 20), XStringFormats.TopCenter);

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

                // �K�[����
                gfx.DrawString("����", fontBold, XBrushes.Black, new XPoint(30, 275));

                // �K�[���
                var tableStartY = 290;
                var colWidths = new[] { 40, 140, 40, 50, 50, 40, 50, 70, 70 }; // ���e��
                var headers = new[] { "����", "�~�W�W��", "�p��", "�ؤo", "���q", "�ƶq", "���", "���", "���B" };

                // �e���Y
                double x = 30;
                foreach (var header in headers)
                {
                    gfx.DrawRectangle(XPens.Black, XBrushes.LightGray, x, tableStartY, colWidths[Array.IndexOf(headers, header)], 20);
                    gfx.DrawString(header, fontRegular, XBrushes.Black, new XRect(x, tableStartY, colWidths[Array.IndexOf(headers, header)], 20), XStringFormats.Center);
                    x += colWidths[Array.IndexOf(headers, header)];
                }

                // ��椺�e
                var items = new[]
                {
                    new { Index = 1, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                    new { Index = 2, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                    new { Index = 3, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                    new { Index = 4, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                    new { Index = 5, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                    new { Index = 6, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                    new { Index = 7, Name = "���ÿ�/304/2B/�¦⽤", Thickness = "0.1", Size = "4'x8'", Weight = "2.4", Quantity = "1", Unit = "��", Uprice = "100", Money = "100" },
                };

                int rowIndex = 1;
                foreach (var item in items)
                {
                    x = 30;
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
                    gfx.DrawString(item.Weight, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3], y, colWidths[4], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4],y, colWidths[5], 20);
                    gfx.DrawString(item.Quantity, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4], y, colWidths[5], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4] + colWidths[5], y, colWidths[6], 20);
                    gfx.DrawString(item.Unit, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4] + colWidths[5], y, colWidths[6], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4] + colWidths[5] + colWidths[6], y, colWidths[7], 20);
                    gfx.DrawString(item.Uprice, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4] + colWidths[5] + colWidths[6], y, colWidths[7], 20), XStringFormats.Center);

                    gfx.DrawRectangle(XPens.Black, x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4] + colWidths[5] + colWidths[6] + colWidths[7], y, colWidths[8], 20);
                    gfx.DrawString(item.Money, fontRegular, XBrushes.Black, new XRect(x + colWidths[0] + colWidths[1] + colWidths[2] + colWidths[3] + colWidths[4] + colWidths[5] + colWidths[6] + colWidths[7], y, colWidths[8], 20), XStringFormats.Center);

                    rowIndex++;
                }

                // �K�[����B�Ƶ��P�`�N�ƶ������
                double notesTableY = tableStartY + (rowIndex + 0) * 20;
                double totalHeight = 0; // �Ω�l�ܥ���M�Ƶ����`����

                var notes = new[]
                {
                    new { Title = "���", Content = "2024-12-25", Height = 30.0 },
                    new { Title = "�Ƶ�", Content = "�Ш̼зǳW��B�z", Height = 90.0 },
                    new { Title = "�`�N�ƶ�", Content = "1.�q�п�u�H�W���ơB�~��B�ƶq�æp����f�C\n2.��f�t�ӽЩ�C��5��e�N�дڳ��H�o���e�F�|�p��b�C\n3.�����q���b�鬰�C��25��C\n4.�H�f���o��&�����ҩ��C", Height = 70.0 },
                };

                // �Ĥ@�B�Gø�s��������
                foreach (var note in notes)
                {
                    // ø�s���D��
                    gfx.DrawRectangle(XPens.Black, 30, notesTableY, 80, note.Height);
                    gfx.DrawString(note.Title, fontRegular, XBrushes.Black,
                        new XRect(30, notesTableY, 80, note.Height), XStringFormats.Center);

                    // ø�s���e��
                    gfx.DrawRectangle(XPens.Black, 110, notesTableY, 350, note.Height);

                    // �B�z�h���r
                    var lines = note.Content.Split('\n');
                    double lineHeight = 12;
                    double contentStartY = notesTableY + 10;

                    foreach (var line in lines)
                    {
                        gfx.DrawString(line, fontRegular, XBrushes.Black,
                            new XRect(115, contentStartY, 340, lineHeight), XStringFormats.TopLeft);
                        contentStartY += lineHeight;
                    }

                    if (note.Title == "���" || note.Title == "�Ƶ�")
                    {
                        totalHeight += note.Height;
                    }
                    else
                    {
                        // �u���`�N�ƶ�ø�s�k����쪺���
                        gfx.DrawRectangle(XPens.Black, 460, notesTableY, 120, note.Height);
                    }

                    notesTableY += note.Height;
                }

                // �ĤG�B�G���X�p��ø�s�~��
                double startY = tableStartY + (rowIndex + 0) * 20;
                // ø�s�X�p�檺�W�U�~�ةM�k�����
                gfx.DrawLine(XPens.Black, 460, startY, 580, startY); // �W���
                gfx.DrawLine(XPens.Black, 460, startY + totalHeight, 580, startY + totalHeight); // �U���
                gfx.DrawLine(XPens.Black, 580, startY, 580, startY + totalHeight); // �k���
                gfx.DrawLine(XPens.Black, 460, startY, 460, startY + totalHeight); // �����

                // �ĤT�B�G�K�[�X�p��T
                // �p���m
                double amountStartY = startY + 30; // �d�X�@�ǤW�趡�Z
                double textX = 470; // ��r�_�l��m�]�a���^
                double numberX = 560; // �Ʀr������m�]�a�k�^

                // ø�s�X�p���e
                gfx.DrawString("�X�p(���|):", fontRegular, XBrushes.Black,
                    new XRect(textX, amountStartY, 60, 12), XStringFormats.CenterLeft);
                gfx.DrawString("700", fontRegular, XBrushes.Black,
                    new XRect(textX, amountStartY, 90, 12), XStringFormats.CenterRight);

                gfx.DrawString("�|��(5%):", fontRegular, XBrushes.Black,
                    new XRect(textX, amountStartY + 25, 60, 12), XStringFormats.CenterLeft);
                gfx.DrawString("35", fontRegular, XBrushes.Black,
                    new XRect(textX, amountStartY + 25, 90, 12), XStringFormats.CenterRight);

                gfx.DrawString("�`��(�t�|):", fontRegular, XBrushes.Black,
                    new XRect(textX, amountStartY + 50, 60, 12), XStringFormats.CenterLeft);
                gfx.DrawString("735", fontRegular, XBrushes.Black,
                    new XRect(textX, amountStartY + 50, 90, 12), XStringFormats.CenterRight);

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
