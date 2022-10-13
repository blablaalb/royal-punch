using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player.FSM;

namespace Characters.Enemy.FSM
{
    [Serializable]
    public class DanceState : IState
    {
        private EnemyAnimations _animations;
        public string StateName => "Dance";

        public void Initialize(EnemyAnimations animations)
        {
            _animations = animations;
        }

        public void Enter()
        {
            _animations.Celebrate();
        }

        public void Exit()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
        }

    }
}