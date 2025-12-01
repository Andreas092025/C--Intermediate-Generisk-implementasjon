using DispatchGame.Models;
namespace DispatchGame.Missions;

// vet ikke helt om denne trengs eller ikke, men lager den for sikkerhets skyld

public class MultiMissionDispatcher
{
    /// <summary>
    /// Kjører flere missions parallelt for alle helter.
    /// </summary>
    public async Task<List<MissionResult>> DispatchMultipleAsync(
        IEnumerable<Hero> heroes,
        IEnumerable<IMission> missions,
        CancellationToken token = default)
    {
        var allTasks = new List<Task<MissionResult>>();

        foreach (var mission in missions)
        {
            foreach (var hero in heroes)
            {
                // Kjører mission X for hero Y som en egen Task
                allTasks.Add(mission.ExecuteAsync(hero, token));
            }
        }

        // Venter på at ALLE missions for ALLE helter blir ferdige
        return (await Task.WhenAll(allTasks)).ToList();
    }
}
