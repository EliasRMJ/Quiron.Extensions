using System.Drawing;
using System.Drawing.Imaging;

namespace Quiron.Extensions
{
    public static class StreamExtension
    {
        public static async Task<byte[]> ReadAllBytes(this Stream instream)
        {
            if (instream is MemoryStream stream)
                return stream.ToArray();

            using var memoryStream = new MemoryStream();
            await instream.CopyToAsync(memoryStream);

            return memoryStream.ToArray();
        }

        public static byte[]? CreateThumbnail(this byte[] value, int large)
        {
            if (value == null || value.Length == 0)
                return null;

            byte[] ReturnedThumbnail;

            using (MemoryStream StartMemoryStream = new(), NewMemoryStream = new())
            {
                StartMemoryStream.Write(value, 0, value.Length);

                Bitmap startBitmap = new(StartMemoryStream);

                int newHeight;
                int newWidth;
                double HW_ratio;
                if (startBitmap.Height > startBitmap.Width)
                {
                    newHeight = large;
                    HW_ratio = (large / startBitmap.Height);
                    newWidth = (int)(HW_ratio * startBitmap.Width);
                }
                else
                {
                    newWidth = large;
                    HW_ratio = (large / startBitmap.Width);
                    newHeight = (int)(HW_ratio * startBitmap.Height);
                }

                var newBitmap = ResizeImage(startBitmap, newWidth, newHeight);

                newBitmap.Save(NewMemoryStream, ImageFormat.Jpeg);

                ReturnedThumbnail = NewMemoryStream.ToArray();
            }

            return ReturnedThumbnail;
        }

        static Bitmap ResizeImage(Bitmap image, int width, int height)
        {
            Bitmap resizedImage = new(width, height);

            using (var gfx = Graphics.FromImage(resizedImage))
            {
                gfx.DrawImage(image, new Rectangle(0, 0, width, height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }
            return resizedImage;
        }
    }
}
