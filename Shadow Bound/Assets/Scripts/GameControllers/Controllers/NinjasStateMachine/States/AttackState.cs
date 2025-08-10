using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.StateMachineBasic;

namespace GameControllers.Controllers.NinjasStateMachine.States
{
    public class AttackState : IState
    {
        private readonly UnitAnimator _unitAnimator;
        
        public AttackState(UnitAnimator unitAnimator)
        {
            _unitAnimator = unitAnimator;
        }
        
        public void Enter()
        {
            _unitAnimator.AttackOneKatana(true);
        }

        public void Exit()
        {
            _unitAnimator.AttackOneKatana(false);
        }
    }
}
