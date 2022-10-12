using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;

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

        public string StateName => "Idle Punch";

        public void Initialize(EnemyAnimations animations, EnemyBrain context)
        {
            _animations = animations;
            _context = context;
        }

        public void Enter()
        {
            if (_waitCoroutineHandler != null) _context.StopCoroutine(_waitCoroutineHandler);
            _animations.Round();
            _waitCoroutineHandler = _context.StartCoroutine(WaitCorotuine());
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

        private IEnumerator WaitCorotuine()
        {
            var wait = UnityEngine.Random.Range(_waitRange.Min, _waitRange.Max);
            yield return new WaitForSeconds(wait);
            _context.JumpPunch1();
        }

    }
}