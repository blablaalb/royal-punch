using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using DG;
using DG.Tweening;
using Characters.Player.FSM;

namespace Characters.Player
{
    public class RagdollController : MonoBehaviour
    {
        private Animator _animator;
        [SerializeField]
        private GameObject _model;
        [SerializeField]
        private GameObject _ragdoll;
        private Animator _ragdollAnimator;
        private CharacterController _characterController;
        private Action _callback;
        private PlayerBrain _player;


        internal void Awake()
        {
            _animator = GetComponentInChildren<Animator>();
            _ragdollAnimator = _ragdoll.GetComponent<Animator>();
            _characterController = GetComponent<CharacterController>();
            _player = FindObjectOfType<PlayerBrain>();
        }

        internal void Update()
        {
            if (!_ragdoll.activeSelf && _model.activeSelf)
            {
                _ragdoll.transform.position = _model.transform.position;
                _ragdoll.transform.rotation = _model.transform.rotation;
            }
            else if (_ragdoll.activeSelf && !_model.activeSelf)
            {
                var position = _ragdoll.transform.position;
                position.y = 0f;
                transform.position = position;
            }
        }


        public void EnableRagdoll(Action callback)
        {
            _callback = callback;
            _characterController.enabled = false;
            var animName = _animator.GetCurrentAnimatorStateInfo(0).shortNameHash;
            _ragdollAnimator.enabled = true;
            _model.SetActive(false);
            _ragdoll.SetActive(true);
            _ragdollAnimator.Play(animName);
            var force = -_ragdoll.transform.forward * 25f;
            _ragdollAnimator.enabled = false;
            foreach (var rb in _ragdollAnimator.transform.GetComponentsInChildren<Rigidbody>())
            {
                rb.isKinematic = false;
                rb.AddForce(force, ForceMode.Impulse);
            }

            StartCoroutine(CountdownCoroutine());
        }

        public void EnableModel()
        {
            var tweens = new List<Tween>();
            _characterController.enabled = true;

            foreach (var rb in _ragdollAnimator.transform.GetComponentsInChildren<Rigidbody>())
            {
                rb.velocity = Vector3.zero;
            }
            _animator.enabled = false;
            var pos = _ragdoll.transform.position;
            pos.y = 0f;
            gameObject.transform.position = pos;
            var ragdollChilren = _ragdoll.GetComponentsInChildren<Transform>(true);
            var children = _model.GetComponentsInChildren<Transform>(true);
            var zip = ragdollChilren.Zip(children, (ragdollChild, modelChild) => (ragdollChild, modelChild));
            foreach (var z in zip)
            {
                if (z.ragdollChild.GetComponent<Rigidbody>() is Rigidbody rb)
                {
                    if (rb)
                        rb.isKinematic = true;
                }

                tweens.Add(z.ragdollChild.DOMove(z.modelChild.position, 1.2f));
                tweens.Add(z.ragdollChild.DORotate(z.modelChild.rotation.eulerAngles, .8f));
            }

            StartCoroutine(WaitForTweensComlpetion(tweens, () =>
            {
                _ragdoll.SetActive(false);
                _model.SetActive(true);
                _animator.enabled = true;
                _callback?.Invoke();
                _callback = null;
            }));
        }

        private IEnumerator WaitForTweensComlpetion(List<Tween> tweens, Action action)
        {
            var run = true;
            while (run)
            {
                if (tweens.All(tween => tween.IsComplete() || !tween.IsPlaying()))
                {
                    run = false;
                    action?.Invoke();
                }
                yield return new WaitForEndOfFrame();
            }
        }

        private IEnumerator CountdownCoroutine()
        {
            yield return new WaitForSeconds(1.5f);
            if (!_player.Dead)
                EnableModel();
        }

    }
}