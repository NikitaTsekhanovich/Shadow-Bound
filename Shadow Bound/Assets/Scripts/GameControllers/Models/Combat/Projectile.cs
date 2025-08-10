using GameControllers.Controllers;
using GameControllers.Controllers.Properties;
using GameControllers.Models.ItemCharacteristicsData;
using UnityEngine;

namespace GameControllers.Models.Combat
{
    public class Projectile : PoolEntity, IDealDamage
    {
        [SerializeField] private DamageTakerCollision _damageTakerCollision;
        [SerializeField] private float _speed;
        
        private const float LifeTime = 4f;
        
        private DamageDealer _damageDealer;
        private Vector3 _direction;
        private float _currentLifeTime;
        
        protected GameEntityTypes OwnerType;

        public virtual void Init(
            ICanTakeDamage ownerTakerDamage, 
            WeaponCharacteristicsData weaponCharacteristicsData,
            Health health,
            Vector3 direction,
            AudioSource damageSound)
        {
            _currentLifeTime = 0;
            OwnerType = ownerTakerDamage.GameEntityType;
            
            _damageDealer ??= new DamageDealer(
                ownerTakerDamage,
                weaponCharacteristicsData,
                health,
                damageSound);
            
            _direction = direction;
            if (OwnerType == GameEntityTypes.Player)
                transform.localRotation = Quaternion.Euler(90f, 0f, -90f);
            
            transform.localPosition = new Vector3(
                transform.localPosition.x,
                transform.localPosition.y,
                0f);
            
            _damageTakerCollision.OnDamageTakerCollision += DealDamage;
        }

        private void Update()
        {
            transform.position += _direction * Time.deltaTime * _speed;
            
            _currentLifeTime += Time.deltaTime;

            if (_currentLifeTime >= LifeTime)
            {
                _currentLifeTime = 0;
                ReturnToPool();
            }
        }

        public virtual void DealDamage(ICanTakeDamage takerDamage)
        {
            _damageDealer.DealDamage(takerDamage);
        }

        protected override void ReturnToPool()
        {
            _damageTakerCollision.OnDamageTakerCollision -= DealDamage;
            base.ReturnToPool();
        }
    }
}
