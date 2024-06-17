using ClientWebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ClientWebAPI.Controllers
{
    public class ProductController : Controller
    {
        // Uri: go to the API launchSettings.json file and copy the URL from the applicationUrl key
        Uri baseAdress = new Uri("http://localhost:5242/api");
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient();
            _client.BaseAddress = baseAdress;
        }

        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/product/Get").Result;

            if(response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
            }
            return View(productList);
        }
    }
}
