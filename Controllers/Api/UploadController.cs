using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Linq;
using System;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace PortfolioCV.Controllers.Api;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
public class UploadController : ControllerBase
{
    private readonly IWebHostEnvironment _environment;

    public UploadController(IWebHostEnvironment environment)
    {
        _environment = environment;
    }

    [HttpPost("avatar")]
    public async Task<IActionResult> UploadAvatar(IFormFile file, [FromForm] string? deleteOldPath = null)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Dosya seçilmedi.");

        var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension))
            return BadRequest("Sadece resim dosyaları (.jpg, .png, .gif, .webp) yüklenebilir.");

        // wwwroot ve uploads klasörlerini ayarla
        string webRootPath = _environment.WebRootPath;
        if (string.IsNullOrEmpty(webRootPath))
        {
            webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        }

        var uploadsFolder = Path.Combine(webRootPath, "uploads");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // 1. ESKİ DOSYAYI SİLME İŞLEMİ (Temizlik)
        if (!string.IsNullOrEmpty(deleteOldPath))
        {
            try
            {
                // Güvenlik Kontrolü: Sadece /uploads/ altındaki dosyalar silinebilir.
                // Path Traversal saldırısını önlemek için dosya adını alıp tekrar path oluşturuyoruz.
                var fileName = Path.GetFileName(deleteOldPath); 
                var hasExtension = Path.HasExtension(fileName);
                
                if (hasExtension && deleteOldPath.Contains("/uploads/"))
                {
                    var oldFilePath = Path.Combine(uploadsFolder, fileName);
                    if (System.IO.File.Exists(oldFilePath))
                    {
                        System.IO.File.Delete(oldFilePath);
                    }
                }
            }
            catch
            {
                // Silme hatası yüklemeyi durdurmamalı, loglanabilir.
            }
        }

        // 2. YENİ DOSYAYI OPTİMİZE EDİP KAYDETME
        // Uzantıyı .jpg yapıyoruz (optimizasyon sonrası standart çıktı)
        var uniqueFileName = $"{Guid.NewGuid()}.jpg";
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        try
        {
            // ImageSharp ile işlem
            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                // Resize: En uzun kenarı 800px olacak şekilde orantılı küçült
                // Eğer resim zaten küçükse büyütmez (ResizeMode.Max)
                image.Mutate(x => x.Resize(new ResizeOptions
                {
                    Size = new Size(800, 800),
                    Mode = ResizeMode.Max
                }));

                // Kaliteyi %75'e düşürerek JPEG olarak kaydet
                // Bu, 5MB'lık bir resmi görsel kalite kaybı olmadan 100-200KB'a düşürür.
                await image.SaveAsJpegAsync(filePath, new JpegEncoder { Quality = 75 });
            }

            var url = $"/uploads/{uniqueFileName}";
            return Ok(new { url });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Optimizasyon ve yükleme hatası: {ex.Message}");
        }
    }
}
