namespace DispatchGame.Core
{
    public interface IStorable<T>
    {
        void Add(T item);
        void Remove(T item);
        T Get(int index);
        void DisplayAll();
        int Count { get; }

        T? FindByName(string name);
        void SortByPowerLevel();

        void SaveToJson(string filePath);
        void LoadFromJson(string filePath);
    }
}
