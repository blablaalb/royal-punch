using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player.FSM;

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
        [SerializeField]
        private JumpPunch1State _jumpPunchState;
        [SerializeField]
        private RestState _restState;
        [SerializeField]
        private IdleState _idleState;


        public int Health => _health;
        public int MaxHealth => _maxHealth;
        public Action<int> HealthChanged;

        internal void Awake()
        {
            _player=  FindObjectOfType<PlayerBrain>();
            _health = _maxHealth;
            _animations = GetComponent<EnemyAnimations>();
            _fieldOfAttack = FindObjectOfType<FIeldOfAttack>(true);
            _jumpPunchState.Initialize(_animations, this, _fieldOfAttack, _player);
            _restState.Initialize(_animations, this);
            _idleState.Initialize(_animations, this);
        }

        internal void Start()
        {
            Idle();
        }


        public void JumpPunch1()
        {
            EnterState(_jumpPunchState);
        }

        public void Rest()
        {
            EnterState(_restState);
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
        }
    }
}