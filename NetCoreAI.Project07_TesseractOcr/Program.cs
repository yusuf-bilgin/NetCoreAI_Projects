using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        string tessDataPath = @"D:\TesseractOcrTraineddata";

        while (true)
        {
            Console.WriteLine("Karakter okuması yapılacak görsel yolu: ");
            string imagePath = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                Console.WriteLine("Geçersiz giriş! Lütfen geçerli bir görsel yolu girin.");
                continue;
            }

            try
            {
                using (var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default))
                {
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        using (var page = engine.Process(img))
                        {
                            string text = page.GetText();
                            Console.WriteLine("Görselden Okunan Metin: ");
                            Console.WriteLine(text);
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine($"Bir hata oluştu: {exception.Message}");
            }

            Console.WriteLine("\nYeni bir görsel yolu girmek için bir tuşa basın. Çıkmak için ESC tuşuna basın.");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Çıkış yapılıyor...");
                break;
            }
        }
    }
}