using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;

namespace Characters.Player.FSM
{
    [Serializable]
    public class RunState : IState
    {
        public string StateName => "Run State";
        public Vector2 SpeedScale { get; set; }


        [SerializeField]
        private float _forwardSpeed;
        [SerializeField]
        private float _backSpeed;
        [SerializeField]
        private float _sideSpeed;
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
        }

        public void Exit()
        {
            SpeedScale = Vector2.zero;
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
            PerformRun();
        }

        private void PerformRun()
        {
            _animations.Run(SpeedScale);
            var x = SpeedScale.x * _sideSpeed;
            var z = SpeedScale.y > 0f ? SpeedScale.y * _forwardSpeed : SpeedScale.y * _backSpeed;
            Vector3 move = new Vector3(x, 0f, z) * Time.deltaTime;
            move = _characterController.transform.TransformDirection(move);
            _characterController.Move(move);
            _characterController.transform.LookAt(Vector3.zero);
        }
    }
}