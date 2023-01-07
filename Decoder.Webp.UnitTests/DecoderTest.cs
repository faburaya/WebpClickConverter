using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace Decoder.Webp.UnitTests
{
    public class DecoderTest
    {
        private static readonly string bitmapReferenceFilePath = "pic.jpg";
        private static readonly string webpImageFilePath = "pic.webp";

        [Fact]
        public void DecodeWebpToBgraAsync_WebpDecoded_IfComparedToBitmapRef_ThenVerySimilar()
        {
            using Image referenceImage = Image.FromFile(bitmapReferenceFilePath);

            IImageDecoder decoder = new WebpDecoder();
            (byte[] decodedBytes, int actualWidth, int actualHeight) =
                decoder.DecodeToBgraAsync(webpImageFilePath).Result;

            Assert.Equal(referenceImage.Height, actualHeight);
            Assert.Equal(referenceImage.Width, actualWidth);

            const int bytesPerPixel = 4; // BGRA
            int expectedByteCount = actualHeight * actualWidth * bytesPerPixel;
            Assert.Equal(expectedByteCount, decodedBytes.Length);

            GCHandle pinnedMemoryHandle = GCHandle.Alloc(decodedBytes);

            try
            {
                int stride = actualWidth * bytesPerPixel;
                Assert.Equal(0, stride % 4); // Bitmap.Ctor setzt es voraus

                using Bitmap actualImage =
                    new(actualWidth, actualHeight, stride,
                        PixelFormat.Format32bppArgb,// eigentlich BGRA
                        Marshal.UnsafeAddrOfPinnedArrayElement(decodedBytes, 0));

                float percentualDifference =
                    SimpleImageComparison.ImageTool.GetPercentageDifference(
                        referenceImage, actualImage);

                Assert.InRange(percentualDifference, 0.0F, 0.01F);
            }
            finally
            {
                pinnedMemoryHandle.Free();
            }
        }
    }
}