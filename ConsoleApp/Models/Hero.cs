namespace DispatchGame.Models
{
    public class Hero
    {
        public string Name { get; set; } // Heltenes navn
        public string Role { get; set; } // Heltenes rolle eller roller
        public int PowerLevel { get; set; } // Heltenes power level

        public Hero(string name, string role, int powerLevel)
        {
            Name = name;
            Role = role;
            PowerLevel = powerLevel;
        }

        public override string ToString()
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            return $"{Name} - ({Role}) - Power level score: {PowerLevel}";
        }
    }
}
