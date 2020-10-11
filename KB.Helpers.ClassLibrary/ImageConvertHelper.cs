using Syncfusion.Pdf.Parsing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using Image = System.Drawing.Image;

namespace KB.Helpers.ClassLibrary
{
    class ImageConvertHelper
    {
        public string Base64ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (var ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                Image image = Image.FromStream(ms, true);
                Bitmap bmp1 = new Bitmap(image);
                return image.ToString();
            }
        }
        public string ImageToBase64(HttpPostedFileBase FileUpload1)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap image = new Bitmap(FileUpload1.InputStream);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public string PdfImageToBase64(HttpPostedFileBase FileUpload1)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(FileUpload1.InputStream);
                Bitmap image = loadedDocument.ExportAsImage(0);
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public string ImagetToBase64(HttpPostedFileBase FileUpload1, int width, int Height)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Bitmap image = new Bitmap(FileUpload1.InputStream);
                Bitmap bmp1 = ImageResize(image, width, Height);
                bmp1.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public string PdfImagetToBase64(HttpPostedFileBase FileUpload1, int width, int Height)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                PdfLoadedDocument loadedDocument = new PdfLoadedDocument(FileUpload1.InputStream);
                Bitmap image = loadedDocument.ExportAsImage(0);
                Bitmap bmp1 = ImageResize(image, width, Height);
                bmp1.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                byte[] imageBytes = ms.ToArray();
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        private Bitmap ImageResize(Bitmap image, int width, int Height)
        {
            Bitmap originalImage = image;
            int newWidth = originalImage.Width;
            int newHeight = originalImage.Height;
            double aspectRatio = Convert.ToDouble(originalImage.Width) / Convert.ToDouble(originalImage.Height);
            if (aspectRatio <= 1 && originalImage.Width > width)
            {
                newWidth = width;
                newHeight = Convert.ToInt32(Math.Round(newWidth / aspectRatio));
            }
            else if (aspectRatio > 1 && originalImage.Height > Height)
            {
                newHeight = Height;
                newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio));
            }
            return new Bitmap(originalImage, newWidth, newHeight);
        }
    }
}
