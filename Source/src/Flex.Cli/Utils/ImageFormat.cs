
namespace Flex.Utils
{
    internal class ImageFormat
    {
        public ImageType ImageType
        {
            get;
            internal set;
        }

        public byte[] Header
        {
            get;
            internal set;
        }

        public string Extension
        {
            get;
            internal set;
        }

        public string MimeType
        {
            get;
            internal set;
        }
    }
}
