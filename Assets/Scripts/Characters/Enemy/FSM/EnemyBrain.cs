using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player.FSM;
#if UNITY_EDITOR
using NaughtyAttributes;
#endif

namespace Characters.Enemy.FSM
{
    public class EnemyBrain : Context<IState>
    {
        private int _health;
        [SerializeField]
        private int _maxHealth;
        private PlayerBrain _player;
        private EnemyAnimations _animations;
        private FIeldOfAttack _fieldOfAttack;
        private SmoothLookAt _lookAt;
        [SerializeField]
        private JumpPunch1State _jumpPunchState;
        [SerializeField]
        private RestState _restState;
        [SerializeField]
        private IdleState _idleState;
        [SerializeField]
        private ClosePunchState _closePunchState;
        [SerializeField]
        private DanceState _danceState;


        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public Action<int> HealthChanged;

        internal void Awake()
        {
            _player = FindObjectOfType<PlayerBrain>();
            _health = _maxHealth;
            _animations = GetComponent<EnemyAnimations>();
            _fieldOfAttack = FindObjectOfType<FIeldOfAttack>(true);
            _lookAt = GetComponent<SmoothLookAt>();
            _jumpPunchState.Initialize(_animations, this, _fieldOfAttack, _player);
            _restState.Initialize(_animations, this);
            _idleState.Initialize(_animations, this, _player);
            _closePunchState.Initialize(_animations, this, _player);
            _danceState.Initialize(_animations);

            GameManager.Instance.PlayerLost += OnPlayerLost;
        }

        internal void Start()
        {
            Idle();
        }

        internal void OnDestroy()
        {
            try
            {

                GameManager.Instance.PlayerLost -= OnPlayerLost;
            }
            catch { }
        }

        override protected void Update()
        {
            if (_currentState == _restState)
            {
                _lookAt.Target = null;
            }
            else
            {
                _lookAt.Target = _player.transform;
            }
            base.Update();
        }


        public void JumpPunch1()
        {
            EnterState(_jumpPunchState);
        }

        public void ClosePunch()
        {
            EnterState(_closePunchState);
        }

        public void Rest()
        {
            EnterState(_restState);
        }

        public void Dance()
        {
            EnterState(_danceState);
        }

        public void Idle()
        {
            EnterState(_idleState);
        }

        public void TakeDamage(int damage)
        {
            var health = _health - damage;
            health = Mathf.Clamp(health, 0, _maxHealth);
            var delta = health - _health;
            _health = health;
            HealthChanged?.Invoke(delta);
            _animations.Punched();
        }

        private void OnPlayerLost()
        {
            Dance();
        }

#if UNITY_EDITOR
        [Button]
        public void PrintEnemyState()
        {
            Debug.Log($"Enemy: Current State is: {_currentState.StateName}");
        }
#endif

    }
}