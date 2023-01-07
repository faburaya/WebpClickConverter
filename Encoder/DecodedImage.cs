using System.Drawing;
using System.Runtime.InteropServices;

namespace Encoder
{
    /// <summary>
    /// Wickelt eine Instanz von <see cref="Image"/>, um zu vermeiden,
    /// dass ihre verbundene Ressourcen entsorgt werden.
    /// </summary>
    public class DecodedImage : IDisposable
    {
        /// <summary>
        /// Das Bild, dessen Ressourcen sichergestellt werden müssen.
        /// </summary>
        public Image Image { get; private init; }

        #region Entsorgung

        private GCHandle _pinnedDataHandle;

        private DecodedImage(byte[] bgraImageData, Image image)
        {
            _pinnedDataHandle = GCHandle.Alloc(bgraImageData, GCHandleType.Pinned);
            Image = image;
        }

        private bool _disposed = false;

        /// <inheritdoc/>
        public void Dispose()
        {
            if (!_disposed)
            {
                Dispose(true);
                GC.SuppressFinalize(this);
                _disposed = true;
            }
        }

        /// <summary>
        /// Entsorgt die Ressourcen während der Sammlung durch GC.
        /// </summary>
        ~DecodedImage()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                Image.Dispose();
            }
            _pinnedDataHandle.Free();
        }

        #endregion

        /// <summary>
        /// Erstellt ein Bild aus Bilddaten.
        /// </summary>
        /// <param name="width">Die Breite des Bildes in Pixels.</param>
        /// <param name="height">Die Höhe des Bildes in Pixels.</param>
        /// <param name="bgra">Die Bilddaten in BGRA-Format.</param>
        /// <returns>Eine neue Instanz von <see cref="DecodedImage"/>.</returns>
        public static DecodedImage CreateBgraImage(int width, int height, byte[] bgra)
        {
            Image image = new Bitmap(width, height,
                width * 4/*Bytes per BGRA-Pixel*/,
                System.Drawing.Imaging.PixelFormat.Format32bppArgb,
                Marshal.UnsafeAddrOfPinnedArrayElement(bgra, 0));

            return new DecodedImage(bgra, image);
        }
    }
}
