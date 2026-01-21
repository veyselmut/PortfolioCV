using Microsoft.AspNetCore.Mvc;
using PortfolioCV.Services;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace PortfolioCV.Controllers.Api;

[Route("api/cv")]
[ApiController]
public class CvController : ControllerBase
{
    private readonly CvService _cvService;

    public CvController(CvService cvService)
    {
        _cvService = cvService;
    }

    [HttpGet("download")]
    public async Task<IActionResult> DownloadCv()
    {
        try
        {
            var data = await _cvService.GetCvDataAsync();
            
            var pdfBytes = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(50);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(11));

                    page.Header()
                        .Text(data.PersonalInfo?.FullName ?? "CV")
                        .SemiBold().FontSize(20).FontColor(Colors.Blue.Medium);

                    page.Content().PaddingVertical(1, Unit.Centimetre).Column(col =>
                    {
                        col.Spacing(5);

                        col.Item().Text(data.PersonalInfo?.Title ?? "").FontSize(14);
                        col.Item().Text(data.PersonalInfo?.Email ?? "");
                        col.Item().Text(data.PersonalInfo?.Phone ?? "");

                        if (data.Experiences.Any())
                        {
                            col.Item().PaddingTop(10).Text("Experience").SemiBold().FontSize(14);
                            foreach (var exp in data.Experiences.Take(5))
                            {
                                col.Item().Text(exp.Position).SemiBold();
                                col.Item().Text(exp.CompanyName);
                            }
                        }

                        if (data.Educations.Any())
                        {
                            col.Item().PaddingTop(10).Text("Education").SemiBold().FontSize(14);
                            foreach (var edu in data.Educations.Take(3))
                            {
                                col.Item().Text(edu.School).SemiBold();
                                col.Item().Text(edu.EducationType);
                            }
                        }

                        if (data.Skills.Any())
                        {
                            col.Item().PaddingTop(10).Text("Skills").SemiBold().FontSize(14);
                            foreach (var skill in data.Skills.Take(10))
                            {
                                col.Item().Text($"{skill.Name} - {skill.Level}%");
                            }
                        }
                    });

                    page.Footer()
                        .AlignCenter()
                        .Text(x =>
                        {
                            x.Span("Page ");
                            x.CurrentPageNumber();
                        });
                });
            }).GeneratePdf();

            var fileName = $"CV_{data.PersonalInfo?.FullName?.Replace(" ", "_") ?? "Portfolio"}_{DateTime.Now:yyyyMMdd}.pdf";
            
            return File(pdfBytes, "application/pdf", fileName);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { error = "Failed to generate CV", details = ex.Message });
        }
    }
}
