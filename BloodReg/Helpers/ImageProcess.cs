using System.IO;
using System.Windows.Media.Imaging;

namespace BloodReg.Helpers
{
    public static class ImageProcess
    {
        public static BitmapImage? StringToBitmapImage(string path)
        {
            BitmapImage? bitmapImage = null;
            if (!string.IsNullOrEmpty(path))
            {
                if (path.StartsWith("pack"))
                {
                    bitmapImage = new(new Uri(path));
                }
                else
                {
                    using BinaryReader reader = new(File.Open(path, FileMode.Open));
                    FileInfo fi = new(path);
                    byte[] bytes = reader.ReadBytes((int)fi.Length);
                    reader.Close();
                    bitmapImage = new()
                    {
                        CacheOption = BitmapCacheOption.OnLoad
                    };
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = new MemoryStream(bytes);
                    bitmapImage.EndInit();
                }
                if (bitmapImage.CanFreeze)
                {
                    bitmapImage.Freeze();
                }
            }
            return bitmapImage;
        }
    }
}
