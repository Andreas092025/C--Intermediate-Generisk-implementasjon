using DispatchGame.Models;
namespace DispatchGame.Missions
{
    public class MissionResult
    {
        public Hero Hero { get; set; }
        public string MissionName { get; set; }
        public bool Success { get; set; }
        public string Message { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;
    }
}
