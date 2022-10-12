using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;

namespace Characters.Player.FSM
{
    [Serializable]
    public class KnockdownState : IState
    {
        private PlayerAnimations _animations;
        private PlayerBrain _context;

        public string StateName => "Knockdown";

        public void Initialize(PlayerAnimations animations, PlayerBrain context)
        {
            _animations = animations;
            _context = context;
        }

        public void Enter()
        {
            // TODO: ragdoll
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