using Xunit;

namespace Encoder.UnitTests
{
    public class JpegEncoderTest : EncoderTestBase
    {
        private static readonly string referenceImageFilePath = "pic.jpg";
        private static readonly string reencodedImageFilePath = "reencoded_output.jpg";

        [Fact]
        public void SaveToFile_WhenReencodedToJpeg_ThenEquivalentPicture()
        {
            Test(new JpegEncoder(75), referenceImageFilePath, reencodedImageFilePath, 0.01F);
        }
    }
}
