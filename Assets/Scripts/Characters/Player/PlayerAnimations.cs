using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Player
{
    public class PlayerAnimations : MonoBehaviour
    {
        private Animator _animator;


        internal void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
        }

        public void Run(Vector2 blend)
        {
            _animator.Play("Run");
            _animator.SetFloat("ZDirection", blend.y);
            _animator.SetFloat("XDirection", blend.x);
        }

        public void Punch1()
        {
            _animator.SetLayerWeight(1, 1f);
            _animator.SafePlay("Armature|GGPunch", 1, 0f);
        }

        public void Punch2()
        {
            _animator.SetLayerWeight(1, 1f);
            _animator.SafePlay("Armature|GGPunch2", 1, 0f);
        }

        public void StopPunch()
        {
            _animator.SetLayerWeight(1, 0f);
        }

        public void Idle1()
        {
            _animator.Play("Armature|Idle1", -1);
        }

        public void Idle2()
        {
            _animator.CrossFade("Armature|Idle2", 0.1f);
        }

        public void Round()
        {
            _animator.CrossFade("Armature|Round", 0.1f);
        }

        public bool IsPunchAnimationPlaying()
        {
            return _animator.IsPlaying("Armature|GGPunch", 1) || _animator.IsPlaying("Armature|GGPunch2", 1);
        }

    }
}