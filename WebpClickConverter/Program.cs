using Encoder;
using Decoder.Webp;

namespace WebpClickConverter
{
    internal enum Status
    {
        Success = 0,
        FailUsage,
        FailUsageArgFormat,
        FailUsageArgQuality,
        FailLogic,
    }

    internal enum SupportedFormat { Jpeg, Png }

    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            const string usageErrorMessage =
                "Error! Usage: executable (JPEG|PNG) (0-100) file1 file2 ...";

            if (args.Length < 3)
            {
                Console.WriteLine(usageErrorMessage);
                return (int)Status.FailUsage;
            }

            SupportedFormat format;
            switch (args[0].ToLower())
            {
                case "jpeg":
                    format = SupportedFormat.Jpeg;
                    break;

                case "png":
                    format = SupportedFormat.Png;
                    break;

                default:
                    Console.WriteLine(usageErrorMessage);
                    return (int)Status.FailUsageArgFormat;
            }

            string newExtension;
            IEncoder encoder;
            switch (format)
            {
                case SupportedFormat.Jpeg:
                case SupportedFormat.Png:
                    encoder = new PngEncoder();
                    newExtension = "png";
                    break;

                default:
                    Console.WriteLine(usageErrorMessage);
                    return (int)Status.FailLogic;
            }

            if (!int.TryParse(args[1], out int quality)
                || quality < 0
                || quality > 100)
            {
                Console.WriteLine(usageErrorMessage);
                return (int)Status.FailUsageArgQuality;
            }

            await foreach ((string filePath, byte[] bgra, int width, int height)
                in DecodeToBgraAsync(args.Skip(2)))
            {
                string newFileName =
                    $"{Path.GetFileNameWithoutExtension(filePath)}_x.{newExtension}";

                string newFilePath =
                    Path.Combine(Path.GetDirectoryName(filePath) ?? string.Empty, newFileName);

                using DecodedImage bgraImage = DecodedImage.CreateBgraImage(width, height, bgra);
                encoder.SaveToFile(newFilePath, bgraImage.Image);
            }

            return (int)Status.Success;
        }

        private static async IAsyncEnumerable<
            (string inputFilePath, byte[] bgra, int width, int height)>
            DecodeToBgraAsync(IEnumerable<string> filePaths)
        {
            WebpDecoder decoder = new();
            foreach (string path in filePaths)
            {
                var decoded = await decoder.DecodeToBgraAsync(path);
                yield return (path, decoded.decodedData, decoded.widthPixels, decoded.heightPixels);
            }
        }
    }
}