using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Equipment
{
    public class Armor : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        
        public ArmorCharacteristicsData ArmorCharacteristicsData { get; private set; }
        
        public void Init(
            ArmorCharacteristicsData armorCharacteristicsData, 
            Transform ownerTransform,
            Material material)
        {
            ArmorCharacteristicsData = armorCharacteristicsData;
            _meshRenderer.material = material;
        }
    }
}
