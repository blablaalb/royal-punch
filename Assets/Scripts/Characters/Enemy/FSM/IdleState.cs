using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player.FSM;

namespace Characters.Enemy.FSM
{
    [Serializable]
    public class IdleState : IState
    {
        private EnemyAnimations _animations;
        private EnemyBrain _context;
        [SerializeField]
        private FloatRange _waitRange;
        private Coroutine _waitCoroutineHandler;
        [SerializeField]
        private float _closePunchDistance;
        private PlayerBrain _player;

        public string StateName => "Idle";

        public void Initialize(EnemyAnimations animations, EnemyBrain context, PlayerBrain player)
        {
            _animations = animations;
            _context = context;
            _player = player;
        }

        public void Enter()
        {
            if (_waitCoroutineHandler != null) _context.StopCoroutine(_waitCoroutineHandler);
            _animations.Round();
            _waitCoroutineHandler = _context.StartCoroutine(WaitCorotuine());
        }

        public void Exit()
        {
            if (_waitCoroutineHandler != null) _context.StopCoroutine(_waitCoroutineHandler);
        }

        public void OnFixedUpdate()
        {
        }

        public void OnUpdate()
        {
            var distance = Vector3.Distance(_context.transform.position, _player.transform.position);
            if (distance <= _closePunchDistance)
            {
                var angle = Vector3.Angle(_context.transform.forward, _player.transform.position - _context.transform.position);
                if (angle <= 45f)
                    _context.ClosePunch();
            }
        }

        private IEnumerator WaitCorotuine()
        {
            var wait = UnityEngine.Random.Range(_waitRange.Min, _waitRange.Max);
            yield return new WaitForSeconds(wait);
            _context.JumpPunch1();
            _waitCoroutineHandler = null;
        }

    }
}