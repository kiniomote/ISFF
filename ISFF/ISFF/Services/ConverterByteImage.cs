using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

namespace ISFF
{
    public static class ConverterByteImage
    {
        public static byte[] ImageToByte(BitmapImage bitmap)
        {
            var encoder = new JpegBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bitmap));
            using (var memStream = new MemoryStream())
            {
                encoder.Save(memStream);
                return memStream.ToArray();
            }
        }

        public static BitmapImage ByteToImage(byte[] array)
        {
            using (var memStream = new MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.StreamSource = memStream;
                image.EndInit();
                return image;
            }
        }

        public static BitmapImage ToBitmapImage(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, ImageFormat.Jpeg);
                memory.Position = 0;

                var bitmapImage = new BitmapImage();
                bitmapImage.BeginInit();
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapImage.StreamSource = memory;
                bitmapImage.EndInit();
                bitmapImage.Freeze();

                return bitmapImage;
            }
        }
    }
}
