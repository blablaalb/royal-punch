using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player.FSM;

namespace Characters.Enemy.FSM
{
    [Serializable]
    public class JumpPunch1State : IState
    {
        private EnemyAnimations _animations;
        private FIeldOfAttack _fieldOfAttack;
        private EnemyBrain _context;
        private float _attackRange = 360f;
        [SerializeField]
        private float _maxDistance = 9f;
        private PlayerBrain _player;
        [SerializeField]
        private int _damage;

        public string StateName => "Jump Punch 1";

        public void Initialize(EnemyAnimations animations, EnemyBrain context, FIeldOfAttack fieldOfAttack, PlayerBrain player)
        {
            _animations = animations;
            _context = context;
            _fieldOfAttack = fieldOfAttack;
            _player = player;
        }

        public void Enter()
        {
            _animations.JumpPunch1Finihed -= OnJumpAnimationFinished;
            _animations.JumpPunch1Finihed += OnJumpAnimationFinished;
            _animations.JumpPunch1Landed -= OnJumpLanded;
            _animations.JumpPunch1Landed += OnJumpLanded;
            _animations.JumpPunch1();

            _fieldOfAttack.Show();
            _fieldOfAttack.Range = 0f;
            _fieldOfAttack.Distance = 0.1f;
            _fieldOfAttack.ScaleToDistance(1, 0.5f);
            _fieldOfAttack.ScaleToRange(360, 0.25f);
        }

        public void Exit()
        {
            _animations.JumpPunch1Finihed -= OnJumpAnimationFinished;
            _animations.JumpPunch1Landed -= OnJumpLanded;
        }



        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
        }

        private void OnJumpAnimationFinished()
        {
            _context.Rest();
            _fieldOfAttack.Hide();
        }

        private void OnJumpLanded()
        {
            _fieldOfAttack.Hide();
            var pp = _player.transform.position;
            var ep = _context.transform.position;
            if (Vector2.Distance(new Vector2(pp.x, pp.z), new Vector2(ep.x, ep.z)) <= _maxDistance)
            {
                _player.TakeDamage(_damage);
            }
        }


    }
}