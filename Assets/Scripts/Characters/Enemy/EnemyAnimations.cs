using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Characters.Enemy
{
    public class EnemyAnimations : MonoBehaviour
    {
        private Animator _animator;

        public Action JumpPunch1Finihed;
        public Action JumpPunch1Landed;

        internal void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void JumpPunch1()
        {
            _animator.CrossFade("Armature|BossSuper5", 0.1f);
        }

        public void ClosePunch()
        {
            var animation = "Armature|GGPunch";
            if (!_animator.IsPlaying(animation, 0))
            {
                _animator.Play(animation, 0, 0f);
                Debug.Log("punch");
            }
        }

        public void Rest()
        {
            _animator.CrossFade("Armature|BossTired", 0.2f);
        }

        public void Idle1()
        {
            _animator.Play("Armature|Idle1", -1);
        }

        public void Idle2()
        {
            _animator.CrossFade("Armature|Idle2", 0.1f);
        }

        public void Punched()
        {
            _animator.SetLayerWeight(1, 1f);
            _animator.Play("Punched",  1, 0f);
        }

        public void Round()
        {
            _animator.CrossFade("Armature|Round", 0.1f);
        }

        public void OnJumpPunch1Finished()
        {
            JumpPunch1Finihed?.Invoke();
        }

        public void OnJumpPunch1Landed()
        {
            JumpPunch1Landed?.Invoke();
        }

        public void OnPunchedFinished()
        {
            _animator.SetLayerWeight(1, 0f);
        }
    }
}