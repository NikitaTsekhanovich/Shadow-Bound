namespace LevelsSystem
{
    public class LevelConfigsContainer
    {
        private int _currentIndexLevel;
        
        public LevelConfig[] LevelConfigs { get; private set; }

        public LevelConfigsContainer(LevelConfig[] levelConfigs)
        {
            LevelConfigs = levelConfigs;
        }

        public void SetLevelIndex(int levelIndex)
        {
            _currentIndexLevel = levelIndex;
        }

        public LevelConfig GetCurrentLevelConfig()
        {
            return LevelConfigs[_currentIndexLevel];
        }
    }
}
