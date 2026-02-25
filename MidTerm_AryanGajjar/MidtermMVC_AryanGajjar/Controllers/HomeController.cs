using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace MidtermMVC_FirstNameLastName.Controllers
{
    public class HomeController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;

        public HomeController(IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _httpClientFactory = httpClientFactory;
            _config = config;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = new Dictionary<string, string>();
            await LoadUsage(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CheckUsage()
        {
            var model = new Dictionary<string, string>();
            await LoadUsage(model);
            return View("Index", model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(string name, int quantity)
        {
            var client = _httpClientFactory.CreateClient("MidtermApi");
            client.DefaultRequestHeaders.Remove("X-Api-Key");
            client.DefaultRequestHeaders.Add("X-Api-Key", _config["Api:ApiKey"]);

            var payload = new { name = name, quantity = quantity };
            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var resp = await client.PostAsync("Product", content);
            var body = await resp.Content.ReadAsStringAsync();

            var model = new Dictionary<string, string>
            {
                ["CreateStatus"] = $"{(int)resp.StatusCode} {resp.ReasonPhrase}",
                ["CreateBody"] = body
            };

            await LoadUsage(model);
            return View("Index", model);
        }

        private async Task LoadUsage(Dictionary<string, string> model)
        {
            var client = _httpClientFactory.CreateClient("MidtermApi");
            client.DefaultRequestHeaders.Remove("X-Api-Key");
            client.DefaultRequestHeaders.Add("X-Api-Key", _config["Api:ApiKey"]);

            var resp = await client.GetAsync("usage");
            var body = await resp.Content.ReadAsStringAsync();

            model["UsageStatus"] = $"{(int)resp.StatusCode} {resp.ReasonPhrase}";
            model["UsageBody"] = body;
            model["ApiBaseUrl"] = _config["Api:BaseUrl"] ?? "";
        }
    }
}