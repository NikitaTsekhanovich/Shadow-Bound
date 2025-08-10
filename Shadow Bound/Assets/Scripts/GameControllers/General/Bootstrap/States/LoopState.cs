using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;

namespace GameControllers.General.Bootstrap.States
{
    public class LoopState : IState, IHaveUpdate, IHaveFixedUpdate
    {
        private readonly GameSystemsHandler _gameSystemsHandler;
        
        public LoopState(GameSystemsHandler gameSystemsHandler)
        {
            _gameSystemsHandler = gameSystemsHandler;
        }
        
        public void Enter()
        {
            
        }

        public void Exit()
        {
            
        }

        public void UpdateSystem()
        {
            _gameSystemsHandler?.UpdateSystem();
        }

        public void FixedUpdateSystem()
        {
            _gameSystemsHandler?.FixedUpdateSystem();
        }
    }
}
