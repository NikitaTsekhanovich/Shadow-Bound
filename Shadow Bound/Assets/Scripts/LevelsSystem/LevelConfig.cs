using UnityEngine;

namespace LevelsSystem
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "Configs/LevelConfig")]
    public class LevelConfig : ScriptableObject
    {
        [field: SerializeField] public int Index { get; private set; }
        [field: SerializeField] public Sprite Background { get; private set; }
        [field: SerializeField] public LevelLoader LevelLoader { get; private set; }
    }
}
