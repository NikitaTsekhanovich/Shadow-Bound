using SaveSystems;
using SaveSystems.DataTypes;

namespace GameControllers.Controllers
{
    public class ExperienceController
    {
        private readonly SaveSystem _saveSystem;

        private int _currentExperience;

        public int CurrentLevel { get; private set; }

        public const int MaxExperienceMultiplier = 10;
        
        public ExperienceController(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
            _currentExperience = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDExperience);
            CurrentLevel = _saveSystem.GetData<PlayerSaveData, int>(PlayerSaveData.GUIDLevel);
        }

        public void TakeExperience(int experience)
        {
            _currentExperience += experience;
            var maximumExperience = CurrentLevel * MaxExperienceMultiplier;

            while (_currentExperience >= maximumExperience)
            {
                _currentExperience -= maximumExperience;
                CurrentLevel++;
                maximumExperience = CurrentLevel * MaxExperienceMultiplier;
            }
            
            _saveSystem.SaveData<PlayerSaveData, int>(_currentExperience, PlayerSaveData.GUIDExperience);
            _saveSystem.SaveData<PlayerSaveData, int>(CurrentLevel, PlayerSaveData.GUIDLevel);
        }
    }
}
