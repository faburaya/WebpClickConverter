using System.Drawing;

namespace Encoder
{
    /// <summary>
    /// Schinittstelle zur Encodierung der Bilddaten.
    /// </summary>
    public interface IEncoder
    {
        /// <summary>
        /// Encodiert und speichert die Bilddaten.
        /// </summary>
        /// <param name="filePath">Der Dateipfad.</param>
        /// <param name="image">Das zu speichernde Bild.</param>
        void SaveToFile(string filePath, Image image);
    }
}