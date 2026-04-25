using Microsoft.AspNetCore.Mvc;
using NorthwindCatalog.Services.DTOs;

public class SummaryController : Controller
{
    private readonly HttpClient _client;

    public SummaryController(IHttpClientFactory factory)
    {
        _client = factory.CreateClient("api");
    }

    public async Task<IActionResult> Index()
    {
        var summary = await _client.GetFromJsonAsync<List<CategorySummaryDto>>
            ("api/products/summary");

        return View(summary);
    }
}