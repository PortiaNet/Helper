using System.Drawing;
using System.Drawing.Imaging;
using PortiaNet.Helper.Ai.Vision;
using PortiaNet.Helper.Ai.Vision.BuiltInExtractors;

public class EmailAddressExtractorTests
{
    [Fact]
    public void Extract_ShouldReturnEmail_WhenEmailPresentInImage()
    {
        // Arrange: Create a test image with an email address
        string testEmail = "test.user@example.com";
        using var bmp = new Bitmap(300, 100);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.White);
            g.DrawString(testEmail, new Font("Arial", 20), Brushes.Black, new PointF(10, 40));
        }
        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);
        var imageBytes = ms.ToArray();

        var extractor = new EmailAddressExtractor();
        var options = new VisionProcessingOptions();

        // Act
        var result = extractor.Extract(imageBytes, options);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(testEmail, result!.Value, ignoreCase: true);
        Assert.InRange(result.Confidence, 0.0, 1.0);
    }

    [Fact]
    public void Extract_ShouldReturnNull_WhenNoEmailPresent()
    {
        // Arrange: Create a blank image
        using var bmp = new Bitmap(300, 100);
        using (var g = Graphics.FromImage(bmp))
        {
            g.Clear(Color.White);
            g.DrawString("No email here", new Font("Arial", 20), Brushes.Black, new PointF(10, 40));
        }
        using var ms = new MemoryStream();
        bmp.Save(ms, ImageFormat.Png);
        var imageBytes = ms.ToArray();

        var extractor = new EmailAddressExtractor();
        var options = new VisionProcessingOptions();

        // Act
        var result = extractor.Extract(imageBytes, options);

        // Assert
        Assert.Null(result);
    }
}