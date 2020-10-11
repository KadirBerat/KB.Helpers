using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using Syncfusion.Pdf.Parsing;

namespace KB.Helpers.ClassLibrary
{
    class ImageUploadFromPdfHelper
    {
        string UploadedFileName;
        internal Tuple<string, string> ImageResize(HttpPostedFileBase FileUpload1, int width, int height, int bigWidth, int bigHeight)
        {
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(FileUpload1.InputStream);
            string fileName = FileUpload1.FileName.Replace(" ", "");
            UploadedFileName = HttpContext.Current.Server.MapPath("~/images/Upload/" + DateTime.Now.ToShortDateString().Trim().Replace(':', '_').Replace('.', '_') + DateTime.Now.ToShortTimeString().Trim().Replace(':', '_').Replace('.', '_') + fileName + ".png");
            Bitmap image = loadedDocument.ExportAsImage(0);
            Bitmap bmp1 = ImageResize(image, bigWidth, bigHeight);
            ImageCodecInfo jgpEncoder = GetEncoder(bmp1.RawFormat.Equals(ImageFormat.Png) ? ImageFormat.Png : ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
                50L);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (FileStream stream = File.Create(UploadedFileName))
            {
                bmp1.Save(stream, jgpEncoder, myEncoderParameters);
            }
            UploadedFileName = "~/images/Upload/" + UploadedFileName.Split('\\')[UploadedFileName.Split('\\').Length - 1].ToString();
            string imageUrlThumbnail = HttpContext.Current.Server.MapPath("~/images/Thumbnails/" + DateTime.Now.ToShortDateString().Trim().Replace(':', '_').Replace('.', '_') + DateTime.Now.ToShortTimeString().Trim().Replace(':', '_').Replace('.', '_') + fileName + ".png");
            System.Drawing.Image i = System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(UploadedFileName));
            System.Drawing.Image thumbnail = new System.Drawing.Bitmap(width, height);
            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(thumbnail);
            g.DrawImage(i, 0, 0, width, height);
            loadedDocument.Close(true);
            thumbnail.Save(imageUrlThumbnail);
            return new Tuple<string, string>("/images/Upload/" + DateTime.Now.ToShortDateString().Trim().Replace(':', '_').Replace('.', '_') + DateTime.Now.ToShortTimeString().Trim().Replace(':', '_').Replace('.', '_') + fileName, "/images/Thumbnails/" + DateTime.Now.ToShortDateString().Trim().Replace(':', '_').Replace('.', '_') + DateTime.Now.ToShortTimeString().Trim().Replace(':', '_').Replace('.', '_') + fileName + ".png");
        }
        internal string ImageResize(HttpPostedFileBase FileUpload1, int width, int height)
        {
            string fileName = FileUpload1.FileName.Replace(" ", "");
            UploadedFileName = HttpContext.Current.Server.MapPath("~/images/Thumbnails/" + DateTime.Now.ToShortDateString().Trim().Replace(':', '_').Replace('.', '_') + DateTime.Now.ToShortTimeString().Trim().Replace(':', '_').Replace('.', '_') + fileName + ".png");
            PdfLoadedDocument loadedDocument = new PdfLoadedDocument(FileUpload1.InputStream);
            Bitmap image = loadedDocument.ExportAsImage(0);
            Bitmap bmp1 = ImageResize(image, width, height);
            ImageCodecInfo jgpEncoder = GetEncoder(bmp1.RawFormat.Equals(ImageFormat.Png) ? ImageFormat.Png : ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
                50L);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (FileStream stream = File.Create(UploadedFileName))
            {
                bmp1.Save(stream, jgpEncoder, myEncoderParameters);
            }
            loadedDocument.Close(true);
            return "/images/Thumbnails/" + DateTime.Now.ToShortDateString().Trim().Replace(':', '_').Replace('.', '_') + DateTime.Now.ToShortTimeString().Trim().Replace(':', '_').Replace('.', '_') + fileName + ".png";
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
        private Bitmap ImageResize(Bitmap image, int width, int height)
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
            else if (aspectRatio > 1 && originalImage.Height > height)
            {
                newHeight = height;
                newWidth = Convert.ToInt32(Math.Round(newHeight * aspectRatio));
            }
            return new Bitmap(originalImage, newWidth, newHeight);
        }
    }
}
