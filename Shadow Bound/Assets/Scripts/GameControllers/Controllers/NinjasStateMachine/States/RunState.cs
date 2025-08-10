using DG.Tweening;
using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.Controllers.Properties;
using GameControllers.StateMachineBasic;

namespace GameControllers.Controllers.NinjasStateMachine.States
{
    public class RunState : IState, IHaveFixedUpdate
    {
        private readonly UnitAnimator _unitAnimator;
        private readonly ForwardMovement _forwardMovement;
        
        private const int DelayRun = 1;

        private bool _canRun;
        private Sequence _delaySequence;
        
        public RunState(
            UnitAnimator unitAnimator, 
            ForwardMovement forwardMovement)
        {
            _unitAnimator = unitAnimator;
            _forwardMovement = forwardMovement;
            _canRun = true;
        }
        
        public void Enter()
        {
            _delaySequence = DOTween.Sequence()
                .AppendInterval(DelayRun)
                .AppendCallback(() => _canRun = true);
            
            _unitAnimator.Run(true);
        }

        public void Exit()
        {
            _canRun = false;
            _delaySequence?.Kill();
            _unitAnimator.Run(false);
            _forwardMovement.Stop();
        }

        public void FixedUpdateSystem()
        {
            if (!_canRun) return;
            
            _forwardMovement.Run();
        }
    }
}
