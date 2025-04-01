using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

class Program
{
    static async Task Main(string[] args)
    {
        var config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        var apiKey = config["ApiSettings:ApiKey"];

        if (string.IsNullOrEmpty(apiKey))
        {
            Console.WriteLine("API Key bulunamadı! Lütfen appsettings.json dosyanızı kontrol edin.");
            return;
        }

        Console.WriteLine("Lütfer sormak istediğiniz sorunuzu yazınız...");

        var prompt = Console.ReadLine();
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[] {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            },
            max_tokens = 500
        };

        var json = JsonSerializer.Serialize(requestBody);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        try
        {
            var response = await httpClient.PostAsync("https://platform.openai.com/v1/chat/completions", content);
            var responseString = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                var result = JsonSerializer.Deserialize<JsonElement>(responseString);
                var answer = result.GetProperty("choices")[0].GetProperty("message").GetProperty("content").GetString();
                Console.WriteLine("Open AI'ın Cevabı: ");
                Console.WriteLine(answer);
            }
            else
            {
                Console.WriteLine($"Bir hata oluştu: {response.StatusCode}");
                Console.WriteLine(responseString);
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Bir hata oluştu: {exception.Message}");
        }
    }
}