using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;

namespace PDYS.Helper
{
    public static class ImageHelper
    {
        public static byte[] ResizeImage(byte[] source, int with, int height)
        {

            Size size = new Size(with, height);

            Image imgToResize = null;
            using (MemoryStream inputBuffer = new MemoryStream(source))
            {
                imgToResize = Image.FromStream(inputBuffer);
            }


            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            if (nPercent > 1)
                return source;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            destWidth = (destWidth < 1) ? 1 : destWidth;
            destHeight = (destHeight < 1) ? 1 : destHeight;

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.High;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();


            byte[] resultBuffer = null;
            using (MemoryStream buffer = new MemoryStream())
            {
                saveJpeg(buffer, b, 75);
                resultBuffer = buffer.ToArray();
            }

            return resultBuffer;
        }

        public static void saveJpeg(Stream buffer, Bitmap img, long quality)
        {
            // Encoder parameter for image quality
            EncoderParameter qualityParam = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);

            // Jpeg image codec
            ImageCodecInfo jpegCodec = getEncoderInfo("image/jpeg");

            if (jpegCodec == null)
                return;

            EncoderParameters encoderParams = new EncoderParameters(1);
            encoderParams.Param[0] = qualityParam;

            img.Save(buffer, jpegCodec, encoderParams);
        }

        private static ImageCodecInfo getEncoderInfo(string mimeType)
        {
            // Get image codecs for all image formats
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();

            // Find the correct image codec
            for (int i = 0; i < codecs.Length; i++)
                if (codecs[i].MimeType == mimeType)
                    return codecs[i];
            return null;
        }
    }
}
