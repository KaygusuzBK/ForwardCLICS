using System;

namespace App.Utils
{
    public static class ConsoleHelper
    {
        // Kullanıcıdan integer değer alır
        public static int GetIntInput(string prompt)
        {
            int result;
            while (true)
            {
                Console.WriteLine(prompt);
                string? input = Console.ReadLine();

                if (int.TryParse(input, out result))
                {
                    return result;
                }

                PrintError("Invalid input. Please enter a valid integer.");
            }
        }

        // Kullanıcıdan string değer alır
        public static string GetStringInput(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string? input = Console.ReadLine();

                if (!string.IsNullOrWhiteSpace(input))
                {
                    return input;
                }

                PrintError("Input cannot be empty. Please try again.");
            }
        }

        // Hata mesajını konsola yazdırır
        public static void PrintError(string errorMessage)
        {
            Console.ForegroundColor = ConsoleColor.Red; // Hata mesajlarını kırmızı yapar
            Console.WriteLine($"Error: {errorMessage}");
            Console.ResetColor();
        }

        // Başarı mesajını konsola yazdırır
        public static void PrintSuccess(string successMessage)
        {
            Console.ForegroundColor = ConsoleColor.Green; // Başarı mesajlarını yeşil yapar
            Console.WriteLine($"Success: {successMessage}");
            Console.ResetColor();
        }
    }
}
