using DispatchGame.Models;
using DispatchGame.Core;
using DispatchGame.UI;

namespace DispatchGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IStorable<Hero> zTeam = new Container<Hero>();
            // Dette er Default listen over helter som er i programmet fra før av
            // Standardhelter fra Spillet Dispatch 
            zTeam.Add(new Hero("MechaMan", "Leader / Dispatcher", 85)); // Hovedkarakter fra spillet
        // Alle under er tidligere "super skurker" som rekvireres til å bli helter og ble en del av "Z-Team"
            zTeam.Add(new Hero("Coupe", "Allround DPS", 81));
            zTeam.Add(new Hero("Malevola", "Support / Healer", 79));
            zTeam.Add(new Hero("Invisigal", "Stealth Specialist", 84));
            zTeam.Add(new Hero("Punch Up", "Tank / Bruiser", 88));
            zTeam.Add(new Hero("Flambae", "Pyro Attacker", 83));
            zTeam.Add(new Hero("Prism", "Support / Duplicator", 76));
            zTeam.Add(new Hero("Golem", "Defender / Heavy Tank", 90));
            zTeam.Add(new Hero("Phenomaman", "Alien Powerhouse", 85));
            zTeam.Add(new Hero("Waterboy", "Support / Field Medic", 74));
            zTeam.Add(new Hero("Sonar", "Adaptive Hybrid", 80));

            // Start menyen
            Menu menu = new Menu(zTeam);
            menu.Start();
        }
    }
}
