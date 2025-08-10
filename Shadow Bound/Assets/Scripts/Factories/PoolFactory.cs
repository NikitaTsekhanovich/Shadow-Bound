using GameControllers.Controllers.Properties;
using GameControllers.General.PoolObjects;
using UnityEngine;

namespace Factories
{
    public abstract class PoolFactory<T> : ICanGetPoolEntity<T>
        where T : MonoBehaviour, IPoolEntity
    {
        private readonly T _entity;
        private readonly int _entityPreloadCount;
        
        private PoolBase<T> _entitiesPool;
        private bool _isDestroyed;
        
        protected PoolFactory(T entity, int entityPreloadCount)
        {
            _entity = entity;
            _entityPreloadCount = entityPreloadCount;

            CreatePoolFactory();
        }

        public void CreatePoolFactory()
        {
            _entitiesPool = new PoolBase<T>(Preload, GetEntityAction, ReturnEntityAction, _entityPreloadCount);
        }
    
        public virtual T GetPoolEntity(Transform positionAppearance)
        {
            var newEntity = _entitiesPool.Get();
            newEntity.ActiveInit(positionAppearance.position, positionAppearance.rotation);
    
            return newEntity;
        }

        public void ClearPoolEntity()
        {
            _isDestroyed = true;

            while (_entitiesPool.Count > 0)
            {
                var entity = _entitiesPool.GetItem();
                
                if (entity != null)
                    Object.Destroy(entity.gameObject);
            }
        }
        
        protected virtual T Preload()
        {
            var newEntity = Object.Instantiate(_entity, Vector3.zero, Quaternion.identity);
            newEntity.SpawnInit(ReturnEntity);
            
            return newEntity;
        }
        
        private void ReturnEntityAction(T entity) 
        {
            if (_isDestroyed)
            {
                Object.Destroy(entity.gameObject);
                return;
            }
            
            entity.ChangeStateEntity(false);
        }
        
        private void ReturnEntity(IPoolEntity entity) => _entitiesPool.Return((T)entity);
    
        private void GetEntityAction(T entity) => entity.ChangeStateEntity(true);
    }
}
