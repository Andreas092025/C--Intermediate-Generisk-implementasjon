using DispatchGame.Models;
using DispatchGame.Services;

namespace DispatchGame.Missions
{
    public class FightMonstersMission : IMission
    {
        public string Name => LanguageService.T("mission_fight_monsters");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(1500, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(20, 80);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                ? string.Format(LanguageService.T("fight_success"), hero.Name)
                : string.Format(LanguageService.T("fight_fail"), hero.Name)
            };
        }
    }
    public class RescueCiviliansMission : IMission
    {
        public string Name => LanguageService.T("mission_rescue_civilians");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(1200, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(10, 70);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                ? string.Format(LanguageService.T("rescue_success"), hero.Name)
                : string.Format(LanguageService.T("rescue_fail"), hero.Name)
            };
        }
    }
    public class FireRescueMission : IMission
    {
        public string Name => LanguageService.T("mission_fire_rescue");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(1000, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(15, 75);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                    ? string.Format(LanguageService.T("fire_success"), hero.Name)
                    : string.Format(LanguageService.T("fire_fail"), hero.Name)
            };
        }
    }
    public class MedicalAidMission : IMission
    {
        public string Name => LanguageService.T("mission_medical_aid");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(800, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(5, 60);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                    ? string.Format(LanguageService.T("medical_success"), hero.Name)
                    : string.Format(LanguageService.T("medical_fail"), hero.Name)
            };
        }
    }
    public class TheftAndRobberyMission : IMission
    {
        public string Name => LanguageService.T("mission_theft_and_robbery");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(1100, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(25, 85);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                    ? string.Format(LanguageService.T("theft_success"), hero.Name)
                    : string.Format(LanguageService.T("theft_fail"), hero.Name)
            };
        }   
    }
    public class AccidentResponseMission : IMission
    {
        public string Name => LanguageService.T("mission_accident_response");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(900, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(10, 65);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                    ? string.Format(LanguageService.T("accident_success"), hero.Name)
                    : string.Format(LanguageService.T("accident_fail"), hero.Name)
            };
        }
    }
    public class AssultsAndOrginizedCrimeMission : IMission
    {
        public string Name => LanguageService.T("mission_assults_and_organized_crime");

        public async Task<MissionResult> ExecuteAsync(Hero hero, CancellationToken token = default)
        {
            await Task.Delay(1300, token); // simulerer arbeid

            bool success = hero.PowerLevel > Random.Shared.Next(30, 90);

            return new MissionResult
            {
                Hero = hero,
                MissionName = Name,
                Success = success,
                Message = success
                    ? string.Format(LanguageService.T("assults_success"), hero.Name)
                    : string.Format(LanguageService.T("assults_fail"), hero.Name)
            };
        }
    }
}
