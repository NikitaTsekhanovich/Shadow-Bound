using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.StateMachineBasic;

namespace GameControllers.Controllers.NinjasStateMachine.States
{
    public class DeathState : IState
    {
        private readonly UnitAnimator _unitAnimator;
        
        public DeathState(UnitAnimator unitAnimator)
        {
            _unitAnimator = unitAnimator;
        }
        
        public void Enter()
        {
            _unitAnimator.DieAnimation();
        }

        public void Exit()
        {
            
        }
    }
}
