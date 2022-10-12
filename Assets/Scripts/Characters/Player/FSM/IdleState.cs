using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;

namespace Characters.Player.FSM
{
    [Serializable]
    public class IdleState : IState
    {
        public string StateName => "Idle  State";

        private PlayerAnimations _animations;
        private CharacterController _characterController;
        private PlayerBrain _context;

        public void Initialize(PlayerAnimations animations, CharacterController characterController, PlayerBrain context)
        {
            _animations = animations;
            _characterController = characterController;
            _context = context;
        }

        public void Enter()
        {
            _animations.Round();
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