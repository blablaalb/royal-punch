using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;

namespace Characters.Enemy.FSM
{
    [Serializable]
    public class RestState : IState
    {
        private EnemyAnimations _animations;
        private EnemyBrain _context;
        [SerializeField]
        private float _restDuration;
        private Coroutine _restCoroutineHandler;

        public string StateName => "Rest State";

        public void Initialize(EnemyAnimations animations, EnemyBrain context)
        {
            _animations = animations;
            _context = context;
        }

        public void Enter()
        {
            _animations.Rest();

            if (_restCoroutineHandler != null) _context.StopCoroutine(_restCoroutineHandler);
            _restCoroutineHandler = _context.StartCoroutine(RestCountdownCoroutine());
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

        private void OnRestFinished()
        {
            _context.Idle();
        }

        private IEnumerator RestCountdownCoroutine()
        {
            yield return new WaitForSeconds(_restDuration);
            OnRestFinished();
        }
    }
}