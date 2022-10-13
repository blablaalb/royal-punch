using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player.FSM;

namespace Characters.Enemy.FSM
{
    [Serializable]
    public class ClosePunchState : IState
    {
        private EnemyAnimations _animations;
        private EnemyBrain _context;
        [SerializeField]
        private float _maxDistance = 3f;
        private PlayerBrain _player;

        public string StateName => "Close Punch";

        public void Initialize(EnemyAnimations animations, EnemyBrain context, PlayerBrain player)
        {
            _animations = animations;
            _context = context;
            _player = player;
        }

        public void Enter()
        {
            _animations.ClosePunch();
        }

        public void Exit()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
            var distance = Vector3.Distance(_context.transform.position, _player.transform.position);

            if (distance > _maxDistance)
            {
                _context.Idle();
            }
        }


    }
}