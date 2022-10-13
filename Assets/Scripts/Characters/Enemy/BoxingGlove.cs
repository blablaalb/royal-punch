using System.Collections;
using System.Collections.Generic;
using Characters.Enemy.FSM;
using Characters.Player.FSM;
using UnityEngine;

namespace Characters.Enemy
{
    public class BoxingGlove : MonoBehaviour
    {
        private EnemyBrain _enemy;
        private PlayerBrain _player;
        [SerializeField]
        private int _power;

        internal void Awake()
        {
            _enemy = FindObjectOfType<EnemyBrain>();
            _player = FindObjectOfType<PlayerBrain>();
        }

        internal void OnTriggerEnter(Collider collider)
        {
            if (_enemy.CurrentState.StateName == "Close Punch")
            {
                _player.TakeDamage(_power, true);
                Debug.Log("Damage");
            }
        }
    }
}
