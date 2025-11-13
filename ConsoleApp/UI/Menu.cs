using DispatchGame.Models;
using DispatchGame.Core;

namespace DispatchGame.UI
{
    public class Menu
    {
        private readonly IStorable<Hero> _zTeam;
        public Menu(IStorable<Hero> zTeam)

        {
            _zTeam = zTeam;
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n=== Dispatch: Z-Team Menu ===");
                Console.WriteLine("1. Vis alle helter");
                Console.WriteLine("2. Legg til en ny helt");
                Console.WriteLine("3. Finn helt etter navn");
                Console.WriteLine("4. Sorter etter Power Level (Synkende)");
                Console.WriteLine("5. Fjern en helt");
                Console.WriteLine("6. Lagre helter (JSON)");
                Console.WriteLine("7. Last inn helter (JSON)");

                Console.WriteLine("0. Avslutt");
                Console.Write("Velg et alternativ: ");
                Console.ResetColor();

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("\n=== Z-Team ===");
                        _zTeam.DisplayAll();
                        break;

                    case "2":
                        AddHero();
                        break;

                    case "3":
                        FindHero();
                        break;

                    case "4":
                        _zTeam.SortByPowerLevel();
                        Console.WriteLine("Heltene er nå sortert etter Power Level (synkende).");
                        _zTeam.DisplayAll();
                        break;

                    case "5":
                        RemoveHero();
                        break;

                    case "6":
                        _zTeam.SaveToJson("heroes.json");
                        break;

                    case "7":
                        _zTeam.LoadFromJson("heroes.json");
                        break;
                    
                    case "0":
                        running = false;
                        Console.WriteLine("Avslutter programmet...");
                        break;

                    default:
                        Console.WriteLine("Ugyldig valg. Prøv igjen.");
                        break;
                }
            }
        }

        private void AddHero()
        {
            Console.WriteLine("\nLegg til en ny helt:");
            Console.Write("Navn: ");
            string name = (Console.ReadLine() ?? "Ukjent").Replace(",", " / ");

            Console.Write("Rolle: ");
            string role = (Console.ReadLine() ?? "Ukjent").Replace(",", "/");

            Console.Write("Power Level: ");
            int powerLevel = int.TryParse(Console.ReadLine(), out int pl) ? pl : 50;

            _zTeam.Add(new Hero(name, role, powerLevel));
            Console.WriteLine($"{name} ble lagt til i Z-Teamet!");
        }

        private void FindHero()
        {
            Console.Write("\nSøk etter helt-navn: ");
            string name = Console.ReadLine() ?? "";
            var found = _zTeam.FindByName(name);

            if (found != null)
                Console.WriteLine($"Fant: {found}");
            else
                Console.WriteLine("Ingen helt funnet med det navnet.");
        }

       private void RemoveHero()
        {
            Console.Write("\nSkriv navnet på helten du vil fjerne: ");
            string name = Console.ReadLine() ?? "";
            var hero = _zTeam.FindByName(name);

            if (hero != null)
            {
                Console.Write($"Er du sikker på at du vil fjerne {name}? (y/n): ");
                string confirmation = Console.ReadLine()?.Trim().ToLower() ?? "n";

                if (confirmation == "y" || confirmation == "yes")
                {
                    _zTeam.Remove(hero);
                    Console.WriteLine($"{name} ble fjernet fra laget.");
                }
                else
                {
                    Console.WriteLine($"{name} ble ikke fjernet.");
                }
            }
            else
            {
                Console.WriteLine("Ingen helt funnet med det navnet.");
            }
        }
    }
}
