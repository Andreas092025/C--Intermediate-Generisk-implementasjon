using DispatchGame.Models;

namespace DispatchGame.Missions

{
    public class MissionDispatcher
    {
        public async Task<List<MissionResult>> DispatchAsync(
            IEnumerable<Hero> heroes,
            IMission mission,
            CancellationToken token = default)
        {
            var tasks = heroes.Select(h => mission.ExecuteAsync(h, token));
            return (await Task.WhenAll(tasks)).ToList();
        }
    }
}
