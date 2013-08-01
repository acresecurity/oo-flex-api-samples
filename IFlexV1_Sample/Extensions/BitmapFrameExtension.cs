namespace System.Windows.Media.Imaging
{
    public static class BitmapFrameExtension
    {
        public static BitmapFrame Resize(this BitmapFrame self, int width, int height)
        {
            TransformedBitmap bitmap = new TransformedBitmap(self,
                new ScaleTransform(width / self.Width, height / self.Height, 0, 0));
            return BitmapFrame.Create(bitmap);
        }
    }
}
