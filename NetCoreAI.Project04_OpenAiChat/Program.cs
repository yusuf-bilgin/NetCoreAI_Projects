class Program
{
    static async Task Main(string[] args)
    {
        var apiKey = "Api key is here";
        Console.WriteLine("Lütfer sormak istediğiniz sorunuzu yazınız...");

        var prompt = Console.ReadLine();
        using var httpClient = new HttpClient();
        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer{apiKey}");

        var requestBody = new
        {
            model = "gpt-3.5-turbo",
            messages = new[] {
                new { role = "system", content = "You are a helpful assistant." },
                new { role = "user", content = prompt }
            },
            max_tokens = 100
        };
    }
}