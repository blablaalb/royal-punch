using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using Managers;
#if UNITY_EDITOR
using NaughtyAttributes;
#endif

namespace Characters.Player.FSM
{
    public class PlayerBrain : Context<IState>
    {
        [SerializeField]
        private int _maxHealth;
        private int _health;
        [SerializeField]
        private KnockdownState _knockdownState;
        [SerializeField]
        private RunState _runState;
        [SerializeField]
        private IdleState _idleState;
        private PlayerAnimations _animations;
        private RagdollController _ragdollController;
        private CharacterController _characterController;
        private PunchAction _punchAction;
        [SerializeField]
        private float _punchDistance = 3.5f;
        [SerializeField]
        private DieState _dieState;

        public bool Dead => _health <= 0f;


        internal void Awake()
        {
            _health = _maxHealth;
            _punchAction = GetComponent<PunchAction>();
            _animations = GetComponent<PlayerAnimations>();
            _characterController = GetComponent<CharacterController>();
            _ragdollController = GetComponent<RagdollController>();
            _runState.Initialize(animations: _animations, characterController: _characterController, context: this);
            _idleState.Initialize(animations: _animations, characterController: _characterController, context: this);
            _knockdownState.Initialize(animations: _animations, this, _ragdollController);
            _dieState.Initialize(_animations, _characterController, this, _ragdollController);
        }

        internal void Start()
        {
            InputManager.Instance.Move += OnMove;
            InputManager.Instance.JoystickReleaed += OnJoystickReleased;

            Idle();
        }


        public void Move(float xSpeedScale = 1f, float ySpeedScale = 1f)
        {
            if (_currentState != _runState)
                EnterState(_runState);

            _runState.SpeedScale = new Vector2(xSpeedScale, ySpeedScale);
        }

        public void Idle()
        {
            if (_currentState != _idleState)
                EnterState(_idleState);
        }

        public void Knockdown()
        {
            if (_currentState != _knockdownState)
                EnterState(_knockdownState);
        }

        public void Die()
        {
            EnterState(_dieState);
        }


        public void TakeDamage(int damage, bool knockdown = false)
        {
            var health = _health - damage;
            _health = Mathf.Clamp(health, 0, _maxHealth);
            if (knockdown)
                Knockdown();

            if (Dead)
            {
                Die();
            }
        }



        private void OnMove(Vector2 speedScale)
        {
            if (Dead) return;
            if (_currentState != _knockdownState)
                Move(speedScale.x, speedScale.y);
        }

        private void OnJoystickReleased()
        {
            if (Dead) return;
            if (_currentState == _runState)
                Idle();
        }

        protected override void Update()
        {
            base.Update();
            if (Dead) return;

            var distance = Vector3.Distance(_characterController.transform.position, Vector3.zero);
            if (distance <= _punchDistance)
            {
                _punchAction.Punch();
            }

            GroundSelf();
        }

        private void GroundSelf()
        {
            var position = transform.position;
            position.y = 0;
            transform.position = position;
        }




#if UNITY_EDITOR
        [Button]
        public void PrintPlayerState()
        {
            Debug.Log($"Player: Current State is: {_currentState.StateName}");
        }
#endif

    }
}