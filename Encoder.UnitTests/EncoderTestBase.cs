using System.Drawing;

using Xunit;

namespace Encoder.UnitTests
{
    public class EncoderTestBase
    {
        protected static void Test(
            IEncoder encoder,
            string referenceImageFilePath,
            string transcodedImageFilePath,
            float maxAllowedPercentualDifference)
        {
            try
            {
                using Image referenceImage = Image.FromFile(referenceImageFilePath);
                encoder.SaveToFile(transcodedImageFilePath, referenceImage);

                using Image transcodedImage = Image.FromFile(transcodedImageFilePath);
                float percentualDifference =
                    SimpleImageComparison.ImageTool.GetPercentageDifference(
                        referenceImage, transcodedImage);

                Assert.InRange(percentualDifference, 0.00F, maxAllowedPercentualDifference);
            }
            finally
            {
                if (File.Exists(transcodedImageFilePath))
                {
                    File.Delete(transcodedImageFilePath);
                }
            }
        }
    }
}
