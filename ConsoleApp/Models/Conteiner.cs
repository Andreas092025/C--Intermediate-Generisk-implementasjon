using System.Text.Json;
using DispatchGame.Core;
using DispatchGame.UI;
using DispatchGame.Services;

namespace DispatchGame.Models
{
    public class Container<T> : IStorable<T> where T : class
    {
        private List<T> items = new List<T>();

        public int Count => items.Count;

        public void Add(T item) => items.Add(item);
        public void Remove(T item) => items.Remove(item);
        public T Get(int index) => items[index];

        public IReadOnlyList<T> GetAll() => items.AsReadOnly();

        public void DisplayAll()
        {
            if (items.Count == 0)
            {
                Console.WriteLine(LanguageService.T("container_empty"));
                return;
            }

            foreach (var item in items)
                Console.WriteLine(item);
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
        public void ReplaceAll(IEnumerable<T> newItems)
        {
            items = newItems.ToList();
        }

        public void SortByPowerLevelDecending(Func<T, int> sortFunc)
        {
            /* if (typeof(T) == typeof(Hero))
            {
                items = items.Cast<Hero>()
                    .OrderByDescending(h => h.PowerLevel)
                    .Cast<T>()
                    .ToList();
            } */
            items = items.OrderByDescending(sortFunc).ToList();
        }

        public void SortByPowerLevelAscending(Func<T, int> sortFunc)
        {
            /* if (typeof(T) == typeof(Hero))
            {
                items = items.Cast<Hero>()
                    .OrderBy(h => h.PowerLevel)
                    .Cast<T>()
                    .ToList();
            } */
            items = items.OrderBy(sortFunc).ToList();
        }

        public void SaveToJson(string filePath)
        {
            var json = JsonSerializer.Serialize(
                items,
                new JsonSerializerOptions { WriteIndented = true }
            );

            File.WriteAllText(filePath, json);
            Console.WriteLine(string.Format(LanguageService.T("saved_to"), filePath));
        }

        public void LoadFromJson(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine(LanguageService.T("json_not_found"));
                return;
            }

            var json = File.ReadAllText(filePath);
            var loadedItems = JsonSerializer.Deserialize<List<T>>(json);

            if (loadedItems != null)
            {
                items = loadedItems;
                Console.WriteLine(LanguageService.T("json_loaded"));
            }
        }

        public void SortByPowerLevel()
        {
            throw new NotImplementedException();
        }
    }
}
