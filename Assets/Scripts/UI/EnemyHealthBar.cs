using System;
using System.Collections;
using System.Collections.Generic;
using Characters.Enemy.FSM;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class EnemyHealthBar : MonoBehaviour
    {
        private Slider _slider;
        private EnemyBrain _enemyBrain;

        internal void Awake()
        {
            _slider = GetComponent<Slider>();
        }

        internal void Start()
        {
            _enemyBrain = FindObjectOfType<EnemyBrain>();
            _enemyBrain.HealthChanged += OnHealthChanged;
        }

        internal void OnDestroy()
        {
            _enemyBrain.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(int delta)
        {
            var val = _enemyBrain.Health / (float)_enemyBrain.MaxHealth;
            _slider.value = val;
        }

        internal void FixedUpdate()
        {
            transform.LookAt(Camera.main.transform.position);
        }
    }
}