using GameControllers.Models.Combat;

namespace Factories.Types
{
    public class ProjectilePoolFactory : PoolFactory<Projectile>
    {
        public ProjectilePoolFactory(
            Projectile entity, 
            int entityPreloadCount) : 
            base(entity, 
                entityPreloadCount)
        {
            
        }
    }
}
