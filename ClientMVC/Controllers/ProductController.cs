using ClientMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ClientMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly Uri _baseAddress = new Uri("https://localhost:7112/api");
        private readonly HttpClient _client;

        public ProductController()
        {
            _client = new HttpClient { BaseAddress = _baseAddress };
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ProductViewModel> productList = new List<ProductViewModel>();

            try
            {
                HttpResponseMessage response = await _client.GetAsync("/Products/Get");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    productList = JsonConvert.DeserializeObject<List<ProductViewModel>>(data);
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.ReasonPhrase}");
                }
            }
            catch (HttpRequestException httpRequestException)
            {
                Console.WriteLine($"HttpRequestException: {httpRequestException.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            return View(productList);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PostAsync("/Products/Post", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product Created.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            ProductViewModel product = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/Products/Get/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<ProductViewModel>(data);
                }
                else
                {
                    TempData["errorMessage"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(ProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                string data = JsonConvert.SerializeObject(model);
                StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await _client.PutAsync("/Products/Put", content);

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product details updated.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ProductViewModel product = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync($"/Products/Delete/{id}");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    product = JsonConvert.DeserializeObject<ProductViewModel>(data);
                }
                else
                {
                    TempData["errorMessage"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"/Products/Delete/{id}");

                if (response.IsSuccessStatusCode)
                {
                    TempData["successMessage"] = "Product deleted.";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["errorMessage"] = $"Error: {response.StatusCode} - {response.ReasonPhrase}";
                }
            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Delete", new { id });
        }
    }
}
