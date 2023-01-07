using Xunit;

namespace Encoder.UnitTests
{
    public class PngEncoderTest : EncoderTestBase
    {
        private static readonly string referenceImageFilePath = "pic.jpg";
        private static readonly string transcodedImageFilePath = "transcoded_output.png";

        [Fact]
        public void SaveToFile_WhenTranscodedToPng_ThenEquivalentPicture()
        {
            Test(new PngEncoder(), referenceImageFilePath, transcodedImageFilePath, 0.01F);
        }
    }
}