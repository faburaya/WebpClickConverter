using System.Drawing;
using System.Drawing.Imaging;

namespace Encoder
{
    /// <summary>
    /// Implementiert die Encodierung zum PNG-Format.
    /// </summary>
    public class PngEncoder : IEncoder
    {
        /// <inheritdoc/>
        public void SaveToFile(string filePath, Image image)
        {
            image.Save(filePath, ImageFormat.Png);
        }
    }
}
