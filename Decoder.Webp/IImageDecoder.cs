namespace Reusable.MediaCodecs
{
    /// <summary>
    /// Schnittstelle zur Decodierung von Bilddaten.
    /// </summary>
    public interface IImageDecoder
    {
        /// <summary>
        /// Decodiert das Bild, indem dessen Daten in BGRA umgewandelt werden.
        /// </summary>
        /// <param name="imageFilePath">Der Pfad der Bilddatei.</param>
        /// <returns>Die BGRA-Daten, die Breite und die Höhe des Bildes.</returns>
        Task<(byte[] decodedData, int widthPixels, int heightPixels)>
            DecodeToBgraAsync(string imageFilePath);
    }
}