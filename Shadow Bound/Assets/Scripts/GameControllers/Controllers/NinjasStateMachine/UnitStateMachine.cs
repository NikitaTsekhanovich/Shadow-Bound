using System;
using System.Collections.Generic;
using GameControllers.Controllers.AnimatorsControllers;
using GameControllers.Controllers.NinjasStateMachine.States;
using GameControllers.General.StateMachineBasic;
using GameControllers.StateMachineBasic;

namespace GameControllers.Controllers.NinjasStateMachine
{
    public class UnitStateMachine : StateMachine
    {
        public UnitStateMachine(
            UnitAnimator unitAnimator,
            ForwardMovement forwardMovement)
        {
            States = new Dictionary<Type, IState>
            {
                [typeof(IdleState)] = new IdleState(),
                [typeof(RunState)] = new RunState(unitAnimator, forwardMovement),
                [typeof(AttackState)] = new AttackState(unitAnimator),
                [typeof(DeathState)] = new DeathState(unitAnimator)
            };
            
            EnterIn<RunState>();
        }
    }
}
