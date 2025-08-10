using GameControllers.Models;

namespace Factories.Types
{
    public class DamageViewPoolFactory : PoolFactory<StatusAttackText>
    {
        public DamageViewPoolFactory(
            StatusAttackText entity,
            int entityPreloadCount) : 
            base(entity, 
                entityPreloadCount)
        {
            
        }
    }
}
