using GameControllers.Controllers;
using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.Controllers.NinjasStateMachine;
using GameControllers.Controllers.NinjasStateMachine.States;
using UnityEngine;

namespace GameControllers.Models
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(Animator))]
    public abstract class Unit : MonoBehaviour
    {
        [SerializeField] protected GameEntityTypes _gameEntityType;
        
        private UnitStateMachine _unitStateMachine;
        
        private void Update()
        {
            _unitStateMachine?.UpdateSystem();
        }

        private void FixedUpdate()
        {
            _unitStateMachine?.FixedUpdateSystem();
        }

        protected void InitStateMachine(UnitAnimator unitAnimator, ForwardMovement forwardMovement)
        {
            _unitStateMachine = new UnitStateMachine(
                unitAnimator, 
                forwardMovement);
        }
        
        protected void DetectEnemy(bool isDetected)
        {
            if (isDetected)
                _unitStateMachine.EnterIn<AttackState>();
            else
                _unitStateMachine.EnterIn<RunState>();
        }

        protected virtual void GoToIdleState(Transform ninjaTransform)
        {
            _unitStateMachine.EnterIn<IdleState>();
        }

        protected virtual void Die()
        {
            _unitStateMachine.EnterIn<DeathState>();
        }
    }
}
