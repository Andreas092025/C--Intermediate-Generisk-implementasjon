using DispatchGame.Models;

namespace DispatchGame.Missions
{
    public interface IMission
    {
        string Name { get; }

        /// <summary>
        /// Utfører oppdraget for en bestemt helt.
        /// </summary>
        Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default);
    }
}
