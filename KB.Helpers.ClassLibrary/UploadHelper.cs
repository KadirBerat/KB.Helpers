using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace KB.Helpers.ClassLibrary
{
    class UploadHelper
    {
        public const string FileMapPath = "/Content/FileStore";
        public const string ImageMapPath = "/Content/imgUpload";
        public static string ServerFileMapPath { get => HttpContext.Current.Server.MapPath(FileMapPath); }
        public static string ServerImgMapPath { get => HttpContext.Current.Server.MapPath(ImageMapPath); }
        public static string SaveFile(HttpPostedFileBase file)
        {
            CreatePath(true);
            string filePath = Path.GetFileName(file.FileName);
            var uploadPath = Path.Combine(ServerFileMapPath, filePath);
            file.SaveAs(uploadPath);
            return FileMapPath + "/" + filePath;
        }
        public static string SaveImage(HttpPostedFileBase file)
        {
            CreatePath(false);
            string date = DateTime.Now.ToString().Replace('/', '-').Replace('.', '-').Replace(@"\", "-").Replace(':', '-').Replace(' ', '-');
            string ImagePath = Path.GetFileName(date + file.FileName.ToLower().Trim());
            var uploadPath = Path.Combine(ServerImgMapPath, ImagePath);
            file.SaveAs(uploadPath);
            return ImageMapPath + "/" + ImagePath;
        }
        public static string SaveImage(HttpPostedFileBase file, int width, int height, bool preserveAspect = false)
        {
            CreatePath(false);
            string date = DateTime.Now.ToString().Replace('/', '-').Replace('.', '-').Replace(@"\", "-").Replace(':', '-').Replace(' ', '-');
            string ImagePath = Path.GetFileName(date + file.FileName.ToLower().Trim());
            var uploadPath = Path.Combine(ServerImgMapPath, ImagePath);
            var fileFormat = file.ContentType.Split('/')[1];
            Bitmap bmp = ImageResize(file.InputStream, width, height, preserveAspect);
            ImageCodecInfo jgpEncoder = GetEncoder(fileFormat == "png" ? ImageFormat.Png : ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
                50L);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            using (FileStream stream = File.Create(uploadPath))
            {
                bmp.Save(stream, jgpEncoder, myEncoderParameters);
            }
            return ImageMapPath + "/" + ImagePath;
        }
        public static string ImageToBase64(HttpPostedFileBase file, int width, int height, bool preserveAspect = false)
        {
            Bitmap bmp = ImageResize(file.InputStream, width, height, preserveAspect);
            ImageCodecInfo jgpEncoder = GetEncoder(bmp.RawFormat.Equals(ImageFormat.Png) ? ImageFormat.Png : ImageFormat.Jpeg);
            System.Drawing.Imaging.Encoder myEncoder =
                System.Drawing.Imaging.Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder,
                50L);
            myEncoderParameter = new EncoderParameter(myEncoder, 100L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            return ToBase64(file.FileName);
        }
        static void CreatePath(bool fileMapControl)
        {
            if (!Directory.Exists(ServerFileMapPath) && fileMapControl == true)
                Directory.CreateDirectory(ServerFileMapPath);
            if (!Directory.Exists(ServerImgMapPath) && fileMapControl == false)
                Directory.CreateDirectory(ServerImgMapPath);
        }
        public static bool? FileExtensionControl(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string[] extension = new string[] { ".jpg", ".jpeg", ".png", ".docx", ".pdf" };
                string fileExtension = Path.GetExtension(file.FileName).ToLower();
                if (!extension.Contains(fileExtension))
                {
                    return false;
                }
                return true;
            }
            return null;
        }
        static Bitmap ImageResize(Stream image, int width, int height, bool preserveAspect)
        {
            Bitmap orjinalimage = new Bitmap(image);
            int newWidth = 0;
            int newHeight = 0;
            if (orjinalimage.Width == width && orjinalimage.Height == height)
                return orjinalimage;
            else if (preserveAspect)
            {
                newWidth = orjinalimage.Width;
                newHeight = orjinalimage.Height;
                double enboyorani = Convert.ToDouble(orjinalimage.Width) / Convert.ToDouble(orjinalimage.Height);
                newWidth = width;
                newHeight = Convert.ToInt32(Math.Round(newWidth / enboyorani));
                newHeight = height;
                newWidth = Convert.ToInt32(Math.Round(newHeight * enboyorani));
            }
            else
            {
                newWidth = width;
                newHeight = height;
            }
            return new Bitmap(orjinalimage, newWidth, newHeight);
        }
        static ImageCodecInfo GetEncoder(ImageFormat format)
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
        public static string ToBase64(string uploadFileName)
        {
            using (Image img = Image.FromFile(uploadFileName))
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    img.Save(memoryStream, img.RawFormat);
                    byte[] imageBytes = memoryStream.ToArray();
                    return Convert.ToBase64String(imageBytes);
                }
            }
        }
        public static Image ToImage(string base64String)
        {
            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream memoryStream = new MemoryStream(imageBytes, 0, imageBytes.Length))
            {
                return Image.FromStream(memoryStream, true);
            }
        }
        public static bool DeleteFileOrImage(string path)
        {
            try
            {
                if (path != null || path != "")
                {
                    FileInfo fileInfo = new FileInfo(HttpContext.Current.Server.MapPath(path));
                    if (fileInfo.Exists)
                    {
                        fileInfo.Delete();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
