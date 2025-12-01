using Spectre.Console;
using DispatchGame.Models;
using DispatchGame.Core;
using DispatchGame.Services;
using DispatchGame.Missions;

namespace DispatchGame.UI
{
    public class Menu
    {
        private readonly IStorable<Hero> _zTeam;

        private readonly CanvasImage _dispatchLogo = new CanvasImage("./Picture/Test_Logo.png"); // Ville ikke funke som håpet

        public Menu(IStorable<Hero> zTeam)
        {
            _zTeam = zTeam;
        }

        public void Start()
        {
            bool running = true;

            while (running)
            {
                AnsiConsole.Clear();
                AnsiConsole.Write(
                new FigletText("DISPATCH")
                .Color(Color.Yellow));
                AnsiConsole.Write(
                new FigletText("DataBase Menu")
                .Color(Color.Yellow));
               
               /* AnsiConsole.Clear();
                _dispatchLogo.MaxWidth(80);
                AnsiConsole.Write(_dispatchLogo); */

                // Localized menu choices
                var view = LanguageService.T("view_heroes");
                var missions = LanguageService.T("mission_menu_title");
                var add = LanguageService.T("add_hero");
                var find = LanguageService.T("find_hero");
                var sort = LanguageService.T("sort_heroes");
                var edit = LanguageService.T("edit_hero");
                var remove = LanguageService.T("remove_hero");
                var save = LanguageService.T("save_json");
                var load = LanguageService.T("load_json");
                var changeLang = LanguageService.T("change_language");
                var exit = LanguageService.T("exit");

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(new[] { view, missions, add, find, sort, edit, remove, save, load, changeLang, exit }));

                if (choice == view) DisplayHeroes();
                else if (choice == missions) MissionMenu();
                else if (choice == add) AddHero();
                else if (choice == find) FindHero();
                else if (choice == sort) SortHeroes();
                else if (choice == edit) EditHero();
                else if (choice == remove) RemoveHero();
                else if (choice == save)
                {
                    _zTeam.SaveToJson("heroes.json");
                    Pause();
                }
                else if (choice == load)
                {
                    _zTeam.LoadFromJson("heroes.json");
                    Pause();
                }
                else if (choice == changeLang) ChangeLanguage();
                else if (choice == exit) running = false;
            }
        }

        // --------------------------------------------------------
        //         VIS ALLE HELTER - SHOW ALL HEROES
        // --------------------------------------------------------
        private void DisplayHeroes()
        {
            var heroes = _zTeam.GetAll();

            if (heroes.Count == 0)
            {
                AnsiConsole.MarkupLine($"[red]{LanguageService.T("no_heroes")}[/]");
                Pause();
                return;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn($"[bold aqua]{LanguageService.T("col_name")}[/]")
                .AddColumn($"[bold aqua]{LanguageService.T("col_role")}[/]")
                .AddColumn($"[bold aqua]{LanguageService.T("col_power")}[/]");

            foreach (var hero in heroes)
            {
                table.AddRow(
                    hero.Name,
                    hero.Role,
                    hero.PowerLevel.ToString()
                );
            }

            AnsiConsole.Write(table);
            Pause();
        }

        // --------------------------------------------------------
        //         OPPDRAGS -  MISSION MENU
        // --------------------------------------------------------

        private async void MissionMenu()
        {
            AnsiConsole.Clear();

            var heroes = _zTeam.GetAll();

            if (heroes.Count == 0)
            {
                AnsiConsole.MarkupLine("[red]Ingen helter tilgjengelige for oppdrag![/]");
                Pause();
                return;
            }

            //  Her legger du til nye oppdrag i missions-listen
            var missions = new List<IMission>
            {
                new FightMonstersMission(),
                new RescueCiviliansMission(),
                new FireRescueMission(),
                new MedicalAidMission(),
                new AccidentResponseMission(),
                new AssultsAndOrginizedCrimeMission(),
                new TheftAndRobberyMission(),
            };

        var choice = AnsiConsole.Prompt(
        new SelectionPrompt<string>()
            .Title("[yellow]Velg oppdrag[/]")
            .AddChoices(missions.Select(m => m.Name))
    );

    var selected = missions.First(m => m.Name == choice);

    var dispatcher = new ProgressMissionDispatcher();

    var results = await dispatcher.DispatchWithProgressAsync(heroes, selected);

    // Etter progress-bars → Vis resultat-tabell
    ShowMissionResults(results);
}
    private void ShowMissionResults(List<MissionResult> results)
{
    var table = new Table()
        .AddColumn("Helt")
        .AddColumn("Oppdrag")
        .AddColumn("Status")
        .AddColumn("Detaljer");

    foreach (var r in results)
    {
        table.AddRow(
            r.Hero.Name,
            r.MissionName,
            r.Success ? "[green]Suksess[/]" : "[red]Feilet[/]",
            r.Message
        );
    }

    AnsiConsole.Write(table);
    Pause();
}



        // --------------------------------------------------------
        //         LEGG TIL HELT - ADD HERO
        // --------------------------------------------------------
        private void AddHero()
        {
            string name = AnsiConsole.Ask<string>(LanguageService.T("enter_name"));
            string role = AnsiConsole.Ask<string>(LanguageService.T("enter_role")).Replace(",", " / ");
            int power = AnsiConsole.Ask<int>(LanguageService.T("enter_power"));

            _zTeam.Add(new Hero(name, role, power));

            AnsiConsole.MarkupLine($"[green]{string.Format(LanguageService.T("hero_added"), name)}[/]");
            Pause();
        }

        // --------------------------------------------------------
        //         REDIGER HELT - EDIT HERO
        // --------------------------------------------------------

        private void EditHero()
        {
            // 1. Ask which hero to edit
            string name = AnsiConsole.Ask<string>(LanguageService.T("which_hero_edit"));
            var hero = _zTeam.FindByName(name);

            if (hero == null)
            {
                AnsiConsole.MarkupLine($"[red]{LanguageService.T("hero_not_found")}[/]");
                Pause();
                return;
            }

            // 2. Menu for what to edit
            var editChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(string.Format(LanguageService.T("what_edit_for"), hero.Name))
                    .AddChoices(new[] { LanguageService.T("edit_name"), LanguageService.T("edit_role"), LanguageService.T("edit_power"), LanguageService.T("cancel") }));

            if (editChoice == LanguageService.T("edit_name"))
            {
                string newName = AnsiConsole.Ask<string>(LanguageService.T("enter_name"));
                hero.Name = newName.Replace(",", " / ");
                AnsiConsole.MarkupLine($"[green]{LanguageService.T("updated")}[/]");
            }
            else if (editChoice == LanguageService.T("edit_role"))
            {
                string newRole = AnsiConsole.Ask<string>(LanguageService.T("enter_role"));
                hero.Role = newRole.Replace(",", "/");
                AnsiConsole.MarkupLine($"[green]{LanguageService.T("updated")}[/]");
            }
            else if (editChoice == LanguageService.T("edit_power"))
            {
                int newPower = AnsiConsole.Ask<int>(LanguageService.T("enter_power"));
                hero.PowerLevel = newPower;
                AnsiConsole.MarkupLine($"[green]{LanguageService.T("updated")}[/]");
            }
            else // cancel
            {
                AnsiConsole.MarkupLine($"[yellow]{LanguageService.T("editing_cancelled")}[/]");
                Pause();
                return;
            }

            Pause();
        }



        // --------------------------------------------------------
        //         FINN HELT - FIND HERO
        // --------------------------------------------------------
        private void FindHero()
        {
            string name = AnsiConsole.Ask<string>(LanguageService.T("search_name"));

            var found = _zTeam.FindByName(name);

            if (found != null)
                AnsiConsole.MarkupLine($"[green]{string.Format(LanguageService.T("found_hero"), found.Name)}[/]");
            else
                AnsiConsole.MarkupLine($"[red]{LanguageService.T("hero_not_found")}[/]");

            Pause();
        }

        // --------------------------------------------------------
        //         SORTER HELTER - SORT HEROES
        // --------------------------------------------------------

        private void SortHeroes()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(LanguageService.T("sort_title"))
                    .HighlightStyle("green")
                    .AddChoices(
                        LanguageService.T("sort_power_asc"),
                        LanguageService.T("sort_power_desc"),
                        LanguageService.T("sort_name_asc"),
                        LanguageService.T("sort_name_desc"),
                        LanguageService.T("sort_role_asc"),
                        LanguageService.T("sort_role_desc"),
                        LanguageService.T("cancel"))
            );

            var heroes = _zTeam.GetAll().ToList();

            if (choice == LanguageService.T("sort_power_asc")) heroes = heroes.OrderBy(h => h.PowerLevel).ToList();
            else if (choice == LanguageService.T("sort_power_desc")) heroes = heroes.OrderByDescending(h => h.PowerLevel).ToList();
            else if (choice == LanguageService.T("sort_name_asc")) heroes = heroes.OrderBy(h => h.Name).ToList();
            else if (choice == LanguageService.T("sort_name_desc")) heroes = heroes.OrderByDescending(h => h.Name).ToList();
            else if (choice == LanguageService.T("sort_role_asc")) heroes = heroes.OrderBy(h => h.Role).ToList();
            else if (choice == LanguageService.T("sort_role_desc")) heroes = heroes.OrderByDescending(h => h.Role).ToList();
            else return; // back/cancel

            _zTeam.ReplaceAll(heroes);
            AnsiConsole.MarkupLine($"[green]{LanguageService.T("sorted_success")}[/]");
            Pause();
           
        }
        
        // --------------------------------------------------------
        //         ENDRE SPRÅK - CHANGE LANGUAGE
        // --------------------------------------------------------
        private void ChangeLanguage()
        {
            var lang = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("Choose language / Velg språk")
                    .AddChoices("Norsk", "English"));

            if (lang == "Norsk") LanguageService.SetLanguage("no");
            else LanguageService.SetLanguage("en");

            AnsiConsole.MarkupLine($"[green]{LanguageService.T("language_updated")}[/]");
            Pause();
        }



        // --------------------------------------------------------
        //         FJERN HELT - REMOVE HERO
        // --------------------------------------------------------
        private void RemoveHero()
        {
            string name = AnsiConsole.Ask<string>(LanguageService.T("enter_name_remove"));

            var hero = _zTeam.FindByName(name);

            if (hero == null)
            {
                AnsiConsole.MarkupLine($"[red]{LanguageService.T("hero_not_found")}[/]");
                Pause();
                return;
            }

            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(string.Format(LanguageService.T("confirm_remove"), $"[red]{name}[/]"))
                .AddChoices(LanguageService.T("yes"), LanguageService.T("no_choice"))
            );

            bool confirmed = choice == LanguageService.T("yes");
            if (confirmed)
            {
                _zTeam.Remove(hero);
                AnsiConsole.MarkupLine($"[green]{string.Format(LanguageService.T("hero_removed"), name)}[/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[yellow]{LanguageService.T("remove_cancelled")}[/]");
            }

            Pause();
        }

        // --------------------------------------------------------
        private void Pause()
        {
            AnsiConsole.MarkupLine($"\n[grey]{LanguageService.T("press_any_key")}[/]");
            Console.ReadKey();
        }
    }
}
