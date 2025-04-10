﻿using Tesseract;

class Program
{
    static void Main(string[] args)
    {
        string tessDataPath = @"D:\TesseractOcrTraineddata";

        while (true)
        {
            Console.WriteLine("Yeni bir görsel yolu girin veya çıkmak için ESC tuşuna basın.");
            var key = Console.ReadKey(true);

            if (key.Key == ConsoleKey.Escape)
            {
                Console.WriteLine("Çıkış yapılıyor...");
                break;
            }

            string imagePath = Console.ReadLine();
            Console.WriteLine();

            if (string.IsNullOrWhiteSpace(imagePath))
            {
                Console.WriteLine("Geçersiz giriş! Lütfen geçerli bir görsel yolu girin.");
                continue;
            }

            ImageToText(tessDataPath, imagePath);
        }
    }
    static void ImageToText(string tessDataPath, string imagePath)
    {
        try
        {
            using var engine = new TesseractEngine(tessDataPath, "eng", EngineMode.Default);
            using var img = Pix.LoadFromFile(imagePath); //Gorselleri OCR icin uygun formata getirir.
            using var page = engine.Process(img); //OCR motorunu baslatana ana bilesendir.
            string text = page.GetText();
            Console.WriteLine("Görselden Okunan Metin: ");
            Console.WriteLine(text);
        }
        catch (Exception exception)
        {
            Console.WriteLine($"Bir hata oluştu: {exception.Message}");
        }
    }
}