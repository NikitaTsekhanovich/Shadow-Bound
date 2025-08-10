using GameControllers.Controllers.Properties;
using UnityEngine;

namespace GameControllers.Models.Combat
{
    public class Arrow : Projectile
    {
        [SerializeField] private MeshRenderer _meshRenderer;

        private bool _isInitMaterial;

        public void InitMaterial(Material material)
        {
            if (!_isInitMaterial)
                _meshRenderer.material = material;
        }

        public override void DealDamage(ICanTakeDamage takerDamage)
        {
            base.DealDamage(takerDamage);
            
            if (takerDamage.GameEntityType != OwnerType)
                ReturnToPool();
        }
    }
}
