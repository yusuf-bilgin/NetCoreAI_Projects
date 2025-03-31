using System.Security.Policy;
using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using NetCoreAI.Project02_ApiConsumeUI.Dtos;
using Newtonsoft.Json;

namespace NetCoreAI.Project02_ApiConsumeUI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CustomerController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> CustomerList()
        {
            var client = _httpClientFactory.CreateClient(); //Bir tane istemci olusturduk
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Customers"); //istekte bulunacagimiz requst url
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync(); // Bu degisken ustte tanimlanan responseMessage degiskenine gelen degeri okuyacak ve jsonData degiskenine atayacak
                var values = JsonConvert.DeserializeObject<List<ResultCustomerDto>>(jsonData); // Json formati olarak gelen veriyi deserialize edecek yani normal texte (metine) dönüştürecek. Gelen veriyi json formatında kullanmak istemiyorum.

                return View(values);
            }
            return View();
        }

        [HttpGet]
        public IActionResult CreateCustomer() => View(); // Daha kisa yazilisa alis. Parantez acmaya gerek yok

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CreateCustomerDto createCustomerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(createCustomerDto); //Gelen stringi jsona dönüştüreceğim
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json"); // PutAsync metodu, bir HTTP gövdesi (body) göndermek için HttpContent türünde bir içerik bekler. Ancak jsonData değişkenimiz bir string türündedir.
            var responseMessage = await client.PostAsync("https://localhost:7066/api/Customers", stringContent); //Ekleme islemi yapilirken tam olarak nereye istekte bulunacagimizi belirler

            return responseMessage.IsSuccessStatusCode ? RedirectToAction("CustomerList") : View(); // 200'lu kod donerse CustomerListe yonlendir, donmezse geriye view dondur
        }

        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.DeleteAsync("https://localhost:7066/api/Customers?id=" + id);

            return responseMessage.IsSuccessStatusCode ? RedirectToAction("CustomerList") : View();
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCustomer(int id)
        {
            var client = _httpClientFactory.CreateClient();
            var responseMessage = await client.GetAsync("https://localhost:7066/api/Customers/GetCustomer?id=" + id);
            if (responseMessage.IsSuccessStatusCode)
            {
                var jsonData = await responseMessage.Content.ReadAsStringAsync();
                var values = JsonConvert.DeserializeObject<GetByIdCustomerDto>(jsonData);
                return View(values);
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCustomer(UpdateCustomerDto updateCustomerDto)
        {
            var client = _httpClientFactory.CreateClient();
            var jsonData = JsonConvert.SerializeObject(updateCustomerDto);
            StringContent stringContent = new StringContent(jsonData, Encoding.UTF8, "application/json");
            var responseMessage = await client.PutAsync("https://localhost:7066/api/Customers", stringContent);

            return responseMessage.IsSuccessStatusCode ? RedirectToAction("CustomerList") : View();
        }


    }
}
