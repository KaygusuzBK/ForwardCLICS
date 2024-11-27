using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace App.Data
{
    public static class JsonFileHandler
    {
        /// <summary>
        /// Belirtilen dosyadan veri yükler. Dosya yoksa veya içerik geçersizse varsayılan bir liste döner.
        /// </summary>
        public static List<T> LoadFromFile<T>(string filePath)
        {
            try
            {
                if (!File.Exists(filePath))
                {
                    Console.WriteLine("Dosya bulunamadı, yeni bir dosya oluşturuluyor...");
                    File.Create(filePath).Dispose();
                    return new List<T>();
                }

                var jsonData = File.ReadAllText(filePath);
                return string.IsNullOrWhiteSpace(jsonData)
                    ? new List<T>()
                    : JsonConvert.DeserializeObject<List<T>>(jsonData) ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri yüklenirken bir hata oluştu: {ex.Message}");
                return new List<T>();
            }
        }

        /// <summary>
        /// Belirtilen dosyaya verileri JSON formatında kaydeder.
        /// </summary>
        public static void SaveToFile<T>(string filePath, List<T> data)
        {
            try
            {
                var jsonData = JsonConvert.SerializeObject(data, Formatting.Indented);
                File.WriteAllText(filePath, jsonData);
                Console.WriteLine("Veriler başarıyla kaydedildi.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Veri kaydedilirken bir hata oluştu: {ex.Message}");
            }
        }
    }
}
