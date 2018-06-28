using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using TestProject.Utilities.CustomImageComparision;

namespace TestProject.Utilities
{
    public static  class ImageUtilities
    {
        public static void ResizeImage(string originalFullFileName, string newFullFileName, int maximumWidth, int maximumHeight, bool enforceRatio = false, bool addPadding= false)
        {
            var image = Image.FromFile(originalFullFileName);
            var imageEncoders = ImageCodecInfo.GetImageEncoders();
            EncoderParameters encoderParameters = new EncoderParameters(1);
            encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
            var canvasWidth = maximumWidth;
            var canvasHeight = maximumHeight;
            var newImageWidth = maximumWidth;
            var newImageHeight = maximumHeight;
            var xPosition = 0;
            var yPosition = 0;


            if (enforceRatio)
            {
                var ratioX = maximumWidth / (double)image.Width;
                var ratioY = maximumHeight / (double)image.Height;
                var ratio = ratioX < ratioY ? ratioX : ratioY;
                newImageHeight = (int)(image.Height * ratio);
                newImageWidth = (int)(image.Width * ratio);

                if (addPadding)
                {
                    xPosition = (int)((maximumWidth - image.Width * ratio) / 2);
                    yPosition = (int)((maximumHeight - image.Height * ratio) / 2);
                }
                else
                {
                    canvasWidth = newImageWidth;
                    canvasHeight = newImageHeight;
                }
            }

            var thumbnail = new Bitmap(canvasWidth, canvasHeight);
            var graphic = Graphics.FromImage(thumbnail);

            if (enforceRatio && addPadding)
            {
                graphic.Clear(Color.White);
            }

            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;
            graphic.DrawImage(image, xPosition, yPosition, newImageWidth, newImageHeight);

            thumbnail.Save(newFullFileName, imageEncoders[1], encoderParameters);
        }


        /// <summary>
        /// Resizes the source Image by mainiting aspect ratio
        /// </summary>
        /// <param name="src">Source Image</param>
        /// <param name="maxWidth">Max target width of the Image</param>
        /// <param name="maxHeight">Max target height of the Image</param>
        /// <returns></returns>
        public static Size ResizeKeepAspect(Size src, int maxWidth, int maxHeight)
        {
            decimal rnd = Math.Min(maxWidth / (decimal)src.Width, maxHeight / (decimal)src.Height);
            return new Size((int)Math.Round(src.Width * rnd), (int)Math.Round(src.Height * rnd));
        }

        /// <summary>
        /// Compares two images and returns the difference in percent as an errorlevel (0 to 100)
        /// </summary>
        /// <param name="image1Path"></param>
        /// <param name="image2Path"></param>
        /// <param name="threshold"></param>
        /// <returns></returns>
        public static float GetPercentageDifference(string image1Path, string image2Path, byte threshold = 3)
        {
            if (FileUtilities.CheckIfFileExists(image1Path) && FileUtilities.CheckIfFileExists(image2Path))
            {
                Image img1 = Image.FromFile(image1Path);
                Image img2 = Image.FromFile(image2Path);

                float difference = img1.PercentageDifference(img2, threshold);

                img1.Dispose();
                img2.Dispose();

                return difference;
            }

            return -1;
        }
    }
}

