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
        private RagdollController _ragdollController;

        public string StateName => "Knockdown";

        public void Initialize(PlayerAnimations animations, PlayerBrain context, RagdollController ragdollController)
        {
            _animations = animations;
            _context = context;
            _ragdollController = ragdollController;
        }

        public void Enter()
        {
            _ragdollController.EnableRagdoll(() => _context.Idle());
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