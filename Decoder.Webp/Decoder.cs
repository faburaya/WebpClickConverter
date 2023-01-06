namespace Decoder.Webp
{
    /// <summary>
    /// Decodiert Bilder im WebP-Format.
    /// </summary>
    public class WebpDecoder : IImageDecoder
    {
        /// <inheritdoc/>
        public async Task<(byte[] decodedData, int widthPixels, int heightPixels)>
            DecodeToBgraAsync(string imageFilePath)
        {
            var result = await Task.Run(() =>
            {
                byte[] decodedToBgra = NativeInterop.DecoderWrapper.DecodeWebpToBgra(
                    imageFilePath, out int widthPixels, out int heightPixels);

                return (decodedToBgra, widthPixels, heightPixels);
            });
            return result;
        }
    }
}
