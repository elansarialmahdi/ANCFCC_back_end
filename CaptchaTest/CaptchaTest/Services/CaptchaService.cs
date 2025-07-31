
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text;

public class CaptchaResult
{
    [Required(ErrorMessage = "Please enter the CAPTCHA text.")]
    [Display(Name = "Enter CAPTCHA")]
    [StringLength(5, MinimumLength = 5, ErrorMessage = "The CAPTCHA code must be 5 alphanumeric characters.")]
    [RegularExpression("^[A-Za-z0-9]{5}$", ErrorMessage = "CAPTCHA must be exactly 5 alphanumeric characters.")]
    public string CaptchaId { get; set; } = Guid.NewGuid().ToString();
    public byte[] ImageBytes { get; set; }
    public string Answer { get; set; }
}

public class CaptchaService
{
    private static readonly Random _rand = new Random();

    public CaptchaResult GenerateCaptcha()
    {
        var text = GenerateRandomText(5);
        var imageBytes = GenerateCaptchaImage(text);

        return new CaptchaResult
        {
            ImageBytes = imageBytes,
            Answer = text
        };
    }

    private string GenerateRandomText(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        var sb = new StringBuilder();
        for (int i = 0; i < length; i++)
            sb.Append(chars[_rand.Next(chars.Length)]);
        return sb.ToString();
    }

    //    private byte[] GenerateCaptchaImage(string text)
    //    {
    //        var bitmap = new Bitmap(150, 50);
    //        var font = new Font("Arial", 24, FontStyle.Bold);
    //        var graphics = Graphics.FromImage(bitmap);
    //        graphics.Clear(Color.White);
    //        graphics.DrawString(text, font, Brushes.Black, new PointF(10, 10));

    //        using var ms = new MemoryStream();
    //        bitmap.Save(ms, ImageFormat.Png);
    //        return ms.ToArray();
    //    }
    //}
    private byte[] GenerateCaptchaImage(string text)
    {
        //    var bitmap = new Bitmap(150, 50);
        //    var font = new Font("Arial", 24, FontStyle.Bold);
        //    var graphics = Graphics.FromImage(bitmap);
        //    graphics.Clear(Color.White);

        //    // Draw the CAPTCHA text
        //    graphics.DrawString(text, font, Brushes.Black, new PointF(10, 10));

        //    // Add noise - random dots
        //    var rand = new Random();
        //    for (int i = 0; i < 100; i++)
        //    {
        //        int x = rand.Next(bitmap.Width);
        //        int y = rand.Next(bitmap.Height);
        //        bitmap.SetPixel(x, y, Color.FromArgb(rand.Next(256), rand.Next(256), rand.Next(256)));
        //    }

        //    // Add random lines
        //    for (int i = 0; i < 5; i++)
        //    {
        //        var pen = new Pen(Color.Gray, 1);
        //        int x1 = rand.Next(bitmap.Width);
        //        int y1 = rand.Next(bitmap.Height);
        //        int x2 = rand.Next(bitmap.Width);
        //        int y2 = rand.Next(bitmap.Height);
        //        graphics.DrawLine(pen, x1, y1, x2, y2);
        //    }

        //    using var ms = new MemoryStream();
        //    bitmap.Save(ms, ImageFormat.Png);
        //    return ms.ToArray();
        //}
        
        int width = 150;
        int height = 50;
        using var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.SmoothingMode = SmoothingMode.AntiAlias;
        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        graphics.Clear(Color.White);
        

        var random = new Random();
        using var font = new Font("Arial", 20, FontStyle.Bold);
        using var brush = new SolidBrush(Color.Black);
       

        float x = 10f;
        for (int i = 0; i < text.Length; i++)
        {
          

            graphics.ResetTransform();
            var angle = random.Next(-10, 10);
            graphics.RotateTransform(angle);
          

            brush.Color = Color.FromArgb(
                random.Next(50, 150),
                random.Next(50, 150),
                random.Next(50, 150));
            

            graphics.DrawString(text[i].ToString(), font, brush, x, 10);
            x += 20;  // shift x a bit for the next character
        }
    
        for (int i = 0; i < 10; i++)
        {
            var pen = new Pen(Color.FromArgb(
                random.Next(100, 255),
                random.Next(100, 255),
                random.Next(100, 255)));
            var startPoint = new Point(random.Next(width), random.Next(height));
            var endPoint = new Point(random.Next(width), random.Next(height));
            graphics.DrawLine(pen, startPoint, endPoint);
        }
        // Saves the bitmap as a PNG image and converts it to a byte array for transmission.
        using var ms = new System.IO.MemoryStream();
        bitmap.Save(ms, ImageFormat.Png);
        return ms.ToArray();
    }
}

