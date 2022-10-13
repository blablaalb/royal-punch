using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;

namespace Characters.Player.FSM
{
    [Serializable]
    public class DieState : IState
    {
        public string StateName => "Die ";

        private PlayerAnimations _animations;
        private CharacterController _characterController;
        private PlayerBrain _context;
        private RagdollController _ragdollController;

        public void Initialize(PlayerAnimations animations, CharacterController characterController, PlayerBrain context, RagdollController ragdollController)
        {
            _animations = animations;
            _characterController = characterController;
            _context = context;
            _ragdollController = ragdollController;
        }

        public void Enter()
        {
            _ragdollController.EnableRagdoll(() => _context.Idle());
            GameManager.Instance.OnPlayerLost();
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