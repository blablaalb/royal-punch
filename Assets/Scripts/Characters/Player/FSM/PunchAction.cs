using System.Collections;
using System.Collections.Generic;
using PER.Common.FSM;
using UnityEngine;
using System;
using Characters.Player;
using Characters.Enemy.FSM;

namespace Characters.Player.FSM
{
    [Serializable]
    public class PunchAction : MonoBehaviour
    {
        private PlayerAnimations _animations;
        [SerializeField]
        private int _damage;
        private EnemyBrain _enemyBrain;

        internal void Awake()
        {
            _animations = GetComponent<PlayerAnimations>();
            _enemyBrain = FindObjectOfType<EnemyBrain>();
        }

        public void Punch()
        {
            if (!_animations.IsPunchAnimationPlaying())
            {
                _animations.Punch1();
                _enemyBrain.TakeDamage(_damage);
            }
        }
    }
}