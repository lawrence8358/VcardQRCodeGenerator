using QRCoder;
using System;
using System.Drawing.Drawing2D;
using VcardQRCodeGenerator.Model;

namespace VcardQRCodeGenerator.Utility
{
    internal class QRCodeObj
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();

        public void Generator(CardModel data, string saveFolderPath)
        {
            var content = @$"BEGIN:VCARD
VERSION:3.0
{data.Content}
END:VCARD";

            var qrCodeData = qrGenerator.CreateQrCode(content, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);

            int padding = 30;
            int fontSize = 20;
            int fontPadding = 10;
            int width = 500;  // 圖片寬
            int height = width + padding + fontSize + fontPadding; // 圖片高
            int qrCodeSize = width - padding * 2; // 設定 QRCode 的大小
            int logoSize = 100; // 設定 Logo 的大小
            int logoX = (width - logoSize) / 2;
            int logoY = (qrCodeSize - logoSize) / 2 + padding;

            #region 使用另外一張大張一點的圖片當作黑色底圖

            Bitmap bgImage = new Bitmap(width, height);
            Graphics newGraphics = Graphics.FromImage(bgImage);

            // 設定底圖的背景顏色
            // Color color = ColorTranslator.FromHtml("#272727");
            Color color = ColorTranslator.FromHtml("#641F91");
            SolidBrush brush = new SolidBrush(color);
            newGraphics.FillRectangle(brush, 0, 0, width, height);

            #endregion

            #region 底圖下方要寫 姓名，並且要文字要在正中央

            string text = data.FileName;
            Font font = new Font("微軟正黑體", fontSize);
            SizeF textSize = newGraphics.MeasureString(text, font);

            float fontX = (bgImage.Width - textSize.Width) / 2;
            float fontY = qrCodeSize + padding + fontPadding;

            newGraphics.DrawString(text, font, Brushes.White, new PointF(fontX, fontY));

            #endregion

            #region 在QRCode周圍創建一個有圓角的GraphicsPath

            GraphicsPath path = new GraphicsPath();
            Rectangle qrCodeRect = new Rectangle(padding, padding, qrCodeSize, qrCodeSize);
            int borderRadius = 20; // 設定圓角的半徑

            path.AddArc(qrCodeRect.X, qrCodeRect.Y, borderRadius, borderRadius, 180, 90); // 左上角
            path.AddArc(qrCodeRect.X + qrCodeRect.Width - borderRadius, qrCodeRect.Y, borderRadius, borderRadius, 270, 90); // 右上角
            path.AddArc(qrCodeRect.X + qrCodeRect.Width - borderRadius, qrCodeRect.Y + qrCodeRect.Height - borderRadius, borderRadius, borderRadius, 0, 90); // 右下角
            path.AddArc(qrCodeRect.X, qrCodeRect.Y + qrCodeRect.Height - borderRadius, borderRadius, borderRadius, 90, 90); // 左下角
            path.CloseFigure();

            #endregion

            #region 在QRCode區域內繪製QRCode圖片

            Region oldClip = newGraphics.Clip; // 保存原有的Clip
            newGraphics.SetClip(path, CombineMode.Intersect);
            newGraphics.DrawImage(qrCodeImage, qrCodeRect);

            // 還原Clip
            newGraphics.Clip = oldClip;

            #endregion

            #region 讀取 Logo 圖片

            Bitmap logoImage = new Bitmap("logo.png");

            // 繪製 Logo
            newGraphics.DrawImage(logoImage, logoX, logoY, logoSize, logoSize);

            #endregion

            var filePath = Path.Combine(saveFolderPath, $"{data.FileName}_{data.Lang}.png");
            bgImage.Save(filePath); 

        }
    }
}
