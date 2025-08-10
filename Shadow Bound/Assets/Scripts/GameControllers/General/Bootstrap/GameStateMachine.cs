using System;
using System.Collections.Generic;
using Factories.Properties;
using GameControllers.General.Bootstrap.Data;
using GameControllers.General.Bootstrap.States;
using GameControllers.General.StateMachineBasic;
using GameControllers.StateMachineBasic;
using GlobalSystems;
using LevelsSystem;
using SaveSystems;
using Zenject;

namespace GameControllers.General.Bootstrap
{
    public class GameStateMachine : StateMachine
    {
        public GameStateMachine(
            GameLoadData gameLoadData,
            DiContainer container,
            SaveSystem saveSystem,
            LevelConfig levelConfig,
            CharacterGameTypes characterGameType,
            ICanGetItemSlot itemSlotFactory)
        {
            var gameSystemsHandler = new GameSystemsHandler();
            
            States = new Dictionary<Type, IState>
            {
                [typeof(LoadState)] = new LoadState(this, gameSystemsHandler, gameLoadData, container, saveSystem, levelConfig, characterGameType, itemSlotFactory),
                [typeof(LoopState)] = new LoopState(gameSystemsHandler)
            };
            
            EnterIn<LoadState>();
        }
    }
}
