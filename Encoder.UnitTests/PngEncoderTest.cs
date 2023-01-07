using System.Drawing;

using Xunit;

namespace Encoder.UnitTests
{
    public class PngEncoderTest
    {
        private static readonly string referenceImageFilePath = "pic.jpg";
        private static readonly string transcodedImageFilePath = "out.png";

        [Fact]
        public void SaveToFile_WhenJpegToPng_ThenEquivalentPicture()
        {
            using Image referenceImage = Image.FromFile(referenceImageFilePath);
            PngEncoder encoder = new();
            encoder.SaveToFile(transcodedImageFilePath, referenceImage);

            using Image transcodedImage = Image.FromFile(transcodedImageFilePath);
            float percentualDifference =
                SimpleImageComparison.ImageTool.GetPercentageDifference(
                    referenceImage, transcodedImage);

            Assert.InRange(percentualDifference, 0.00F, 0.01F);
        }
    }
}