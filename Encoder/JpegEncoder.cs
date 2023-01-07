using System.Drawing;
using System.Drawing.Imaging;

namespace Encoder
{
    /// <summary>
    /// Implementiert die Encodierung zum JPEG-Format.
    /// </summary>
    public class JpegEncoder : IEncoder
    {
        private readonly EncoderParameters _qualityParameters;

        private readonly ImageCodecInfo _jpegEncoder;

        /// <summary>
        /// Erstellt eine neue Instanz von <see cref="JpegEncoder"/>.
        /// </summary>
        /// <param name="quality">Gibt die Qualität für die Encodierung im Bereich [0,100] an.</param>
        public JpegEncoder(int quality)
        {
            _jpegEncoder = (
                from encoder in ImageCodecInfo.GetImageEncoders()
                where encoder.FormatID == ImageFormat.Jpeg.Guid
                select encoder).First();

            _qualityParameters = new EncoderParameters(1);
            _qualityParameters.Param[0] =
                new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        }

        /// <inheritdoc/>
        public void SaveToFile(string filePath, Image image)
        {
            image.Save(filePath, _jpegEncoder, _qualityParameters);
        }
    }
}
