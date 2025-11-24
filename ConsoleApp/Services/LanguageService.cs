
namespace DispatchGame.UI

{
    public static class LanguageService
    {
        public static string CurrentLanguage { get; private set; } = "no"; // default

        private static readonly Dictionary<string, Dictionary<string, string>> Translations =
            new()
            {
                {
                    "no", new Dictionary<string, string>
                    {
                        // Menu
                        { "view_heroes", "Vis alle helter" },
                        { "add_hero", "Legg til ny helt" },
                        { "find_hero", "Finn helt etter navn" },
                        { "edit_hero", "Rediger helt" },
                        { "remove_hero", "Fjern en helt" },
                        { "sort_heroes", "Sorter helter" },
                        { "save_json", "Lagre helter (JSON)" },
                        { "load_json", "Last inn helter (JSON)" },
                        { "change_language", "Bytt språk" },
                        { "exit", "Avslutt" },

                        // Prompts
                        { "enter_name", "Skriv inn navn:" },
                        { "enter_role", "Skriv inn rolle:" },
                        { "enter_power", "Skriv inn powerlevel:" },
                        { "hero_not_found", "Ingen helt funnet med det navnet." },
                        
                        // Additional keys used by Menu
                        { "no_heroes", "Ingen helter i laget." },
                        { "col_name", "Navn" },
                        { "col_role", "Rolle" },
                        { "col_power", "Power Level" },
                        { "hero_added", "{0} ble lagt til!" },
                        { "which_hero_edit", "Hvilken helt vil du redigere?" },
                        { "what_edit_for", "Hva vil du redigere for {0}?" },
                        { "edit_name", "Navn" },
                        { "edit_role", "Rolle" },
                        { "edit_power", "PowerLevel" },
                        { "cancel", "Avbryt" },
                        { "search_name", "Søk etter navn:" },
                        { "found_hero", "Fant: {0}" },
                        { "sort_title", "Hvordan vil du sortere heltene?" },
                        { "sort_power_asc", "PowerLevel (stigende)" },
                        { "sort_power_desc", "PowerLevel (synkende)" },
                        { "sort_name_asc", "Navn (A–Å)" },
                        { "sort_name_desc", "Navn (Å–A)" },
                        { "sort_role_asc", "Rolle (A–Å)" },
                        { "sort_role_desc", "Rolle (Å–A)" },
                        { "sorted_success", "Heltene ble sortert!" },
                        { "language_updated", "Språk oppdatert!" },
                        { "enter_name_remove", "Navn på helt som skal fjernes:" },
                        { "confirm_remove", "Er du sikker du vil fjerne {0}?" },
                        { "yes", "Ja" },
                        { "no_choice", "Nei" },
                        { "hero_removed", "{0} ble fjernet fra laget!" },
                        { "remove_cancelled", "Fjerning avbrutt." },
                        { "editing_cancelled", "Redigering avbrutt." },
                        { "press_any_key", "Trykk en tast for å fortsette..." },
                        { "updated", "Oppdatert!" },

                        // Container / JSON messages
                        { "container_empty", "Containeren er tom." },
                        { "saved_to", "Helter ble lagret til {0}" },
                        { "json_not_found", "Fant ingen JSON-fil." },
                        { "json_loaded", "Helter ble lastet inn fra JSON." },
                    }
                },

                {
                    "en", new Dictionary<string, string>
                    {
                        // Menu
                        { "view_heroes", "Show all heroes" },
                        { "add_hero", "Add new hero" },
                        { "find_hero", "Find hero by name" },
                        { "edit_hero", "Edit hero" },
                        { "remove_hero", "Remove a hero" },
                        { "sort_heroes", "Sort heroes" },
                        { "save_json", "Save heroes (JSON)" },
                        { "load_json", "Load heroes (JSON)" },
                        { "change_language", "Change language" },
                        { "exit", "Exit" },

                        // Prompts
                        { "enter_name", "Enter name:" },
                        { "enter_role", "Enter role:" },
                        { "enter_power", "Enter power level:" },
                        { "hero_not_found", "No hero found with that name." },
                        
                        // Additional keys used by Menu
                        { "no_heroes", "No heroes in the team." },
                        { "col_name", "Name" },
                        { "col_role", "Role" },
                        { "col_power", "Power Level" },
                        { "hero_added", "{0} was added!" },
                        { "which_hero_edit", "Which hero would you like to edit?" },
                        { "what_edit_for", "What do you want to edit for {0}?" },
                        { "edit_name", "Name" },
                        { "edit_role", "Role" },
                        { "edit_power", "PowerLevel" },
                        { "cancel", "Cancel" },
                        { "search_name", "Search by name:" },
                        { "found_hero", "Found: {0}" },
                        { "sort_title", "How would you like to sort the heroes?" },
                        { "sort_power_asc", "PowerLevel (ascending)" },
                        { "sort_power_desc", "PowerLevel (descending)" },
                        { "sort_name_asc", "Name (A–Z)" },
                        { "sort_name_desc", "Name (Z–A)" },
                        { "sort_role_asc", "Role (A–Z)" },
                        { "sort_role_desc", "Role (Z–A)" },
                        { "sorted_success", "Heroes were sorted!" },
                        { "language_updated", "Language updated!" },
                        { "enter_name_remove", "Name of hero to remove:" },
                        { "confirm_remove", "Are you sure you want to remove {0}?" },
                        { "yes", "Yes" },
                        { "no_choice", "No" },
                        { "hero_removed", "{0} was removed from the team!" },
                        { "remove_cancelled", "Removal cancelled." },
                        { "editing_cancelled", "Editing cancelled." },
                        { "press_any_key", "Press any key to continue..." },
                        { "updated", "Updated!" },

                        // Container / JSON messages
                        { "container_empty", "The container is empty." },
                        { "saved_to", "Heroes were saved to {0}" },
                        { "json_not_found", "No JSON file found." },
                        { "json_loaded", "Heroes were loaded from JSON." },
                    }
                }
            };

        public static void SetLanguage(string lang)
        {
            if (Translations.ContainsKey(lang))
                CurrentLanguage = lang;
        }

        public static string T(string key)
        {
            return Translations[CurrentLanguage].ContainsKey(key)
                ? Translations[CurrentLanguage][key]
                : $"[{key}]";
        }
    }
}
