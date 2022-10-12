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
        [SerializeField]
        private int _health;
        [SerializeField]
        private KnockdownState _knockdownState;
        [SerializeField]
        private RunState _runState;
        [SerializeField]
        private IdleState _idleState;
        private PlayerAnimations _animations;
        private CharacterController _characterController;
        private PunchAction _punchAction;
        [SerializeField]
        private float _punchDistance = 3.5f;


        internal void Awake()
        {
            _health = _maxHealth;
            _punchAction = GetComponent<PunchAction>();
            _animations = GetComponent<PlayerAnimations>();
            _characterController = GetComponent<CharacterController>();
            _runState.Initialize(animations: _animations, characterController: _characterController, context: this);
            _idleState.Initialize(animations: _animations, characterController: _characterController, context: this);
            _knockdownState.Initialize(animations: _animations, this);
        }

        internal void Start()
        {
            InputManager.Instance.Move += OnMove;
            InputManager.Instance.JoystickReleaed += OnJoystickReleased;
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

        public void TakeDamage(int damage, bool knockdown = false)
        {

        }



        private void OnMove(Vector2 speedScale)
        {
            Move(speedScale.x, speedScale.y);
        }

        private void OnJoystickReleased()
        {
            Idle();
        }

        protected override void Update()
        {
            base.Update();

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
            transform.position= position;
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