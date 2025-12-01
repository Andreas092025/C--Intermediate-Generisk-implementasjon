using Spectre.Console;
using DispatchGame.Models;

namespace DispatchGame.Missions
{
    public class ProgressMissionDispatcher
    {
        public async Task<List<MissionResult>> DispatchWithProgressAsync(
            IEnumerable<Hero> heroes,
            IMission mission,
            CancellationToken token = default)
        {
            var heroList = heroes.ToList();
            var results = new List<MissionResult>(heroList.Count);

            await AnsiConsole.Progress()
                .AutoRefresh(true)
                .AutoClear(false)
                .Columns(new ProgressColumn[]
                {
                    new TaskDescriptionColumn(),
                    new ProgressBarColumn(),
                    new PercentageColumn(),
                    new RemainingTimeColumn(),
                    new SpinnerColumn()
                })
                .StartAsync(async ctx =>
                {
                    // Opprett én progress‑task per helt
                    var tasks = heroList.ToDictionary(
                        hero => hero,
                        hero => ctx.AddTask($"[yellow]{hero.Name}[/]", autoStart: true)
                    );

                    // Kjør alle missions parallelt
                    var running = heroList.Select(async hero =>
                    {
                        var progress = tasks[hero];

                        // Simulerer progress mens oppdraget kjører
                        var missionTask = mission.ExecuteAsync(hero, token);

                        while (!missionTask.IsCompleted)
                        {
                            await Task.Delay(100, token);
                            progress.Increment(2);  // 2% per tick
                        }

                        progress.Value = 100; // fullført

                        var result = await missionTask;
                        results.Add(result);
                    });

                    await Task.WhenAll(running);
                });

            return results;
        }
    }
}