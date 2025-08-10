using UnityEngine;

namespace GameControllers.Controllers.Properties
{
    public interface ICanGetPoolEntity<T>
    {
        public void CreatePoolFactory();
        public T GetPoolEntity(Transform positionAppearance);
    }
}
