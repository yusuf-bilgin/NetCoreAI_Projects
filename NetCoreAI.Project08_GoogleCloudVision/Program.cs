using Google.Cloud.Vision.V1;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Görsel yolunu giriniz:");
        Console.WriteLine();
        string imagePath = Console.ReadLine();

        string credentialPath = "Your credential is here!!!";
        Environment.SetEnvironmentVariable("GOOGLE_APPLICATION_CREDENTIALS", credentialPath);

        GoogleCloudVision(imagePath, credentialPath);
    }
    static void GoogleCloudVision(string imagePath, string crendentialPath)
    {
        try
        {
            var client = ImageAnnotatorClient.Create();
            var image = Image.FromFile(imagePath);
            var response = client.DetectText(image);
            Console.WriteLine("Resimdeki metin:");
            Console.WriteLine();
            foreach (var annotation in response)
            {
                if (!string.IsNullOrEmpty(annotation.Description))
                {
                    Console.WriteLine(annotation.Description);
                }
            }
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Bir hata oluştu: {exception.Message}");
        }
    }
}