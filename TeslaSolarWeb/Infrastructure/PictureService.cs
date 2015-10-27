using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace TeslaSolarWeb.Infrastructure
{
    public class PictureService
    { 
        public static Image ScaleByPercent(Image imgPhoto, int Percent)
        {
            float nPercent = ((float)Percent / 100);

            int sourceWidth = imgPhoto.Width;
            int sourceHeight = imgPhoto.Height;
            int sourceX = 0;
            int sourceY = 0;

            int destX = 0;
            int destY = 0;
            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap bmPhoto = new Bitmap(destWidth, destHeight,
                                     PixelFormat.Format24bppRgb);
            bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
                                    imgPhoto.VerticalResolution);


            Graphics grPhoto = Graphics.FromImage(bmPhoto);
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;
            grPhoto.DrawImage(imgPhoto,
                new Rectangle(destX, destY, destWidth, destHeight),
                new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
                GraphicsUnit.Pixel);
          

            grPhoto.Dispose();
            return bmPhoto;
        }

        public static ImageCodecInfo GetCodec(String mimeFormat)
        {
            ImageCodecInfo output = null;
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.MimeType == mimeFormat)
                {
                    output = codec;
                }
            }
            return output;
        }

        public static bool ZacuvajSlika(HttpPostedFileBase fileBase, string path)
        {
            string rootpath = Path.Combine(path, fileBase.FileName);

            if (fileBase != null && fileBase.ContentLength > 0)
            {
                fileBase.SaveAs(rootpath);
            }

            return true;
        }

        public static bool ZacuvajSlikaPlusMala(HttpPostedFileBase fileBase, string path,string osnovnoIme, ref string generIme)
        {
            if (!DaliEslikaFormat(fileBase))
                return false;
            //------------Slika GOLEMA
            Image slika = Image.FromStream(fileBase.InputStream);
            string tokenIme = GenerImeToken(osnovnoIme);
            string imeNaSlikaGolema = tokenIme  + Path.GetExtension(fileBase.FileName).ToLower();
            generIme = imeNaSlikaGolema;
            slika.Save(Path.Combine(path, imeNaSlikaGolema));

            ///-------- Slika MALA
            string imeNaSlikaMala = tokenIme + "_small" + Path.GetExtension(fileBase.FileName).ToLower();
            int faktor;
            // Za namaluvanje na slikata - Mala slika
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(Encoder.Quality, 70L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            faktor = (int)(300.0 / int.Parse(slika.Width.ToString()) * 100);
            slika = PictureService.ScaleByPercent(slika, faktor);
            slika.Save(Path.Combine(path, imeNaSlikaMala), PictureService.GetCodec("image/jpeg"), myEncoderParameters);
            
            return true;        
        }

        public static string GenerirajImeZaSlika(string osnova)
        {
            string imeFile = "";

            if (String.IsNullOrWhiteSpace(osnova)) { osnova = "File"; }

            imeFile = GenerImeToken(osnova);

            return imeFile;
        }
        public static string GenerImeToken(string ime)
        {
            string uique = "";
            Random rdm = new Random();
            rdm.Next();
            uique = ime + "_" + vratiDatumIVremeVoString() + "_" + rdm.Next().ToString();
            return uique;
        }
        private static string vratiDatumIVremeVoString()
        {
            return "_" + DateTime.Now.ToString("yyyyMMddhhmmss");
        }

        public static bool DaliEslikaFormat(HttpPostedFileBase fileBase)
        {
            if (fileBase == null)
                return false;
            //-------------------------------------------
            //  Check the image mime types
            //-------------------------------------------
            if (fileBase.ContentType.ToLower() != "image/jpg" &&
                        fileBase.ContentType.ToLower() != "image/jpeg" &&
                        fileBase.ContentType.ToLower() != "image/pjpeg" &&
                        fileBase.ContentType.ToLower() != "image/gif" &&
                        fileBase.ContentType.ToLower() != "image/x-png" &&
                        fileBase.ContentType.ToLower() != "image/png")
            {
                return false;
            }

            //-------------------------------------------
            //  Check the image extension
            //-------------------------------------------
            if (Path.GetExtension(fileBase.FileName).ToLower() != ".jpg"
                && Path.GetExtension(fileBase.FileName).ToLower() != ".png"
                && Path.GetExtension(fileBase.FileName).ToLower() != ".gif"
                && Path.GetExtension(fileBase.FileName).ToLower() != ".jpeg")
            {
                return false;
            }

            return true;
        }

        public static bool IzbrisiSlika(string patekaServer,string patekaDb,string patekaDbMala)
        {
            string filePath = "";
            string fileIme = "";
            string[] pomGolema = patekaDb.Split('/');
            string[] pomMala = patekaDbMala.Split('/'); ;
            if (pomGolema.Length == 4)
            {
                fileIme = pomGolema[3];                       
                filePath= Path.Combine(patekaServer, fileIme);
                if (File.Exists(filePath)) { File.Delete(filePath); }

            }

            if (pomMala.Length == 4)
            {
                fileIme = pomMala[3];          
                filePath = Path.Combine(patekaServer, fileIme);
                if (File.Exists(filePath)) { File.Delete(filePath); }
            }
            return true;


        }
    }
}