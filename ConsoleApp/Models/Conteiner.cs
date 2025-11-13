using System.Text.Json; // 👈 viktig for JSON
using DispatchGame.Core;

namespace DispatchGame.Models
{
    public class Container<T> : IStorable<T> where T : class
    {
        private List<T> items = new List<T>();
        public int Count => items.Count;

        public void Add(T item) => items.Add(item);
        public void Remove(T item) => items.Remove(item);
        public T Get(int index) => items[index];

        public void DisplayAll()
        {
            if (items.Count == 0)
            {
                Console.WriteLine("Containeren er tom.");
                return;
            }

            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        public T? FindByName(string name)
        {
            if (typeof(T) == typeof(Hero))
            {
                return items.Cast<Hero>()
                    .FirstOrDefault(h => h.Name.Equals(name, StringComparison.OrdinalIgnoreCase)) as T;
            }
            return null;
        }

        public void SortByPowerLevel()
        {
            if (typeof(T) == typeof(Hero))
            {
                var sorted = items.Cast<Hero>()
                    .OrderByDescending(h => h.PowerLevel)
                    .ToList();
                items = sorted.Cast<T>().ToList();
            }
        }

        // JSON-funksjonene:
        public void SaveToJson(string filePath)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(filePath, json);
            Console.WriteLine($"Helter ble lagret til {filePath}");
        }

        public void LoadFromJson(string filePath)
        {
            if (File.Exists(filePath))
            {
                var json = File.ReadAllText(filePath);
                var loadedItems = JsonSerializer.Deserialize<List<T>>(json);
                if (loadedItems != null)
                {
                    items = loadedItems;
                    Console.WriteLine($"Helter ble lastet fra {filePath}");
                }
            }
            else
            {
                Console.WriteLine("Fant ingen lagret fil å laste fra.");
            }
        }
    }
}
