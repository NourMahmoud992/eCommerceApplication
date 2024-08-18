using eCommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eCommerceApplication.Controllers
{
    public class UseApiController : Controller
    {
        Uri baseAddress = new Uri("http://localhost:53370/api/product");

        private readonly HttpClient _httpClient;
        public UseApiController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress =baseAddress; // Adjust base URL as needed
        }
        [HttpGet]
        public IActionResult Index()
        {
            Product product = new Product();
            try
            {

                HttpResponseMessage response = _httpClient.GetAsync(baseAddress ).Result;
                response.EnsureSuccessStatusCode(); // Throws if response is not successful

                var data = response.Content.ReadAsStringAsync().Result;
                product = JsonConvert.DeserializeObject<Product>(data);

                return View(product);
            }
            catch (HttpRequestException)
            {
                return NotFound(); // Or handle the error appropriately
            }
        }
    }
}
