using System.Text;

namespace Flex.Utils
{
    internal static class ImageFileFormat
    {
        private static readonly IEnumerable<ImageFormat> Formats = new[]
        {
            new ImageFormat { ImageType = ImageType.Bmp, Extension = ".bmp", MimeType = "image/bmp", Header = Encoding.ASCII.GetBytes("BM") },
            new ImageFormat { ImageType = ImageType.Gif, Extension = ".gif", MimeType = "image/gif", Header = Encoding.ASCII.GetBytes("GIF") },
            new ImageFormat { ImageType = ImageType.Png, Extension = ".png", MimeType = "image/png", Header = new byte[] { 137, 80, 78, 71 } },
            new ImageFormat { ImageType = ImageType.Tiff, Extension = ".tiff", MimeType = "image/tiff", Header = new byte[] { 73, 73, 42 } },
            new ImageFormat { ImageType = ImageType.Tiff, Extension = ".tiff", MimeType = "image/tiff", Header = new byte[] { 77, 77, 42 } },
            new ImageFormat { ImageType = ImageType.Jpeg, Extension = ".jpg", MimeType = "image/jpeg", Header = new byte[] { 255, 216, 255 } }
        };

        public static readonly ImageFormat UnknownFormat = new() { ImageType = ImageType.Unknown };

        public static ImageFormat GetImageFormat(IEnumerable<byte> bytes) =>
            Formats.FirstOrDefault(p => p.Header.SequenceEqual(bytes.Take(p.Header.Length))) ?? UnknownFormat;

        public static async Task<ImageFormat> GetImageFormat(string filename)
        {
            if (string.IsNullOrEmpty(filename))
                throw new ArgumentNullException();

            if (!File.Exists(filename))
                throw new FileNotFoundException(filename);

            var max = Formats.Select(p => p.Header.Length).Max();

            var header = new byte[max];

            await using var fs = File.OpenRead(filename);
            await fs.ReadAsync(header, 0, max);
            fs.Close();

            return GetImageFormat(header);
        }
    }
}
