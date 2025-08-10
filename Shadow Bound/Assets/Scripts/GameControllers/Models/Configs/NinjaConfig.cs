using UnityEngine;

namespace GameControllers.Models.Configs
{
    [CreateAssetMenu(fileName = "NinjaConfig", menuName = "Configs/NinjaConfig")]
    public class NinjaConfig : ScriptableObject
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public float MoveSpeed { get; private set; }
        [field: SerializeField] public float AttackSpeed  { get; private set; }
    }
}
