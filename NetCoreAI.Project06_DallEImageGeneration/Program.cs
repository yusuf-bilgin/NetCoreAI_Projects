using System.Text;
using Newtonsoft.Json;

class Program
{
    public static async Task Main(string[] args)
    {
        string apiKey = "Your api key";
        Console.WriteLine("Example promapts: ");
        string propmt = Console.ReadLine();
        using (HttpClient client = new HttpClient())
        {
            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            var requestBody = new
            {
                prompt = propmt,
                n = 1,
                size = "1024x1024"
            };
            string jsonBody = JsonConvert.SerializeObject(requestBody);
            var content = new StringContent(jsonBody,Encoding.UTF8,"application/json");
    }
    }
}