using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Windows;
using zadanie_P_P.Models;

namespace zadanie_P_P.Services;
// Obsługuje zapis i odczyt danych
public class FileService
{
    private const string FilePath = "inventory.json";

    // Wczytuje listę towarów
    public List<Item> LoadItems()
    {
        try
        {
            //jak nnie ma pliku zwraca pusta liste
            if (!File.Exists(FilePath))
                return new List<Item>();

            //czyta tekst i zamienia json na liste c#
            string json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Item>>(json) ?? new List<Item>();
        }
        catch (Exception ex)
        {
            //klasyczne okienko ze nie trybi
            MessageBox.Show($"Nie udało się wczytać danych z pliku: {ex.Message}", "Błąd odczytu",
                MessageBoxButton.OK, MessageBoxImage.Error);
            return new List<Item>();
        }
    }

    public void SaveItems(IEnumerable<Item> items)
    {
        try
        {
            // writeindented uklada ladnie tekst w pliku
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(items, options);
            File.WriteAllText(FilePath, json);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Wystąpił błąd podczas zapisywania danych: {ex.Message}", "Błąd zapisu",
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
    }
}