
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
[Route("api/[controller]")]
public class CaptchaController : ControllerBase
{
    private readonly CaptchaService _captchaService;
    private readonly IMemoryCache _cache;

    public CaptchaController(CaptchaService captchaService, IMemoryCache cache)
    {
        _captchaService = captchaService;
        _cache = cache;
    }

    [HttpGet("generate")]
    public IActionResult Generate()
    {
        var captcha = _captchaService.GenerateCaptcha();

        // Store the answer in memory for 2 minutes
        _cache.Set(captcha.CaptchaId, captcha.Answer, TimeSpan.FromMinutes(2));

        return File(captcha.ImageBytes, "image/png", enableRangeProcessing: false);
    }

    [HttpPost("validate")]
    public IActionResult Validate([FromQuery] string captchaId, [FromQuery] string userInput)
    {
        if (_cache.TryGetValue(captchaId, out string correctAnswer))
        {
            if (userInput.Equals(correctAnswer, StringComparison.OrdinalIgnoreCase))
            {
                _cache.Remove(captchaId); // Prevent reuse
                return Ok(new { success = true, message = "CAPTCHA valid" });
            }
            return BadRequest(new { success = false, message = "Incorrect CAPTCHA" });
        }

        return BadRequest(new { success = false, message = "CAPTCHA expired or invalid" });
    }
 

}
