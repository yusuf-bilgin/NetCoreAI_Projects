using System.Net.Http.Headers;
using System.Text.Json.Serialization;
using NetCoreAi.Project03_RapidApi.ViewModels;
using Newtonsoft.Json;
var client = new HttpClient();
List<ApiSeriesViewModel> apiSeriesViewModel = new List<ApiSeriesViewModel>();
var request = new HttpRequestMessage
{
    Method = HttpMethod.Get, // Metot turu
    RequestUri = new Uri("https://imdb-top-100-movies.p.rapidapi.com/series/"), // istek yapilacak adres
    Headers =
    {
        { "x-rapidapi-key", "75311abc15mshf3984e50ebbedafp1914bajsn1fdf3493b145" },
        { "x-rapidapi-host", "imdb-top-100-movies.p.rapidapi.com" },
    },
};
using (var response = await client.SendAsync(request))
{
    response.EnsureSuccessStatusCode();
    var body = await response.Content.ReadAsStringAsync();
    apiSeriesViewModel = JsonConvert.DeserializeObject<List<ApiSeriesViewModel>>(body);
    foreach (var series in apiSeriesViewModel)
    {
        Console.WriteLine(series.rank + "- " + series.title + " - Film Puanı: " + series.rating + " - Yapım Yılı: " + series.year);
    }
}
Console.ReadLine();