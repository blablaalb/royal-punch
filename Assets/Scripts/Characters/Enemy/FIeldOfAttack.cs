using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;
using System;

namespace Characters.Enemy
{
    public class FIeldOfAttack : MonoBehaviour
    {
        private Material _material;
        private Tween _angleTween;
        private Tween _distanceTween;

        /// <summary>
        /// The value must be normalized i.e. betwen 0 and 1.
        /// </summary>
        /// <value></value>
        public float Distance
        {
            get { return _material.GetFloat("_FarPlane"); }
            set
            {
                var val = value;
                val = Mathf.Clamp01(val);
                _material.SetFloat("_FarPlane", val);
            }
        }

        /// <summary>
        /// 0 - 360deg
        /// </summary>
        /// <value></value>
        public float Range
        {
            get { return _material.GetFloat("_FieldOfView"); }
            set
            {
                var val = value;
                val = Mathf.Clamp(val, 0f, 360f);
                _material.SetFloat("_FieldOfView", val);
            }
        }



        internal void Awake()
        {
            _material = GetComponent<MeshRenderer>().material;
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 0 - 360deg
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="duration"></param>
        /// <param name="callback"></param>
        public void ScaleToRange(float angle, float duration = 1f, Action callback = null)
        {
            if (_angleTween != null) DOTween.Kill(_angleTween);
            _angleTween = DOTween.To(getter: () => Range, setter: x => Range = x, endValue: angle, duration: duration)
                .OnComplete(() =>
                {
                    _angleTween = null;
                    callback?.Invoke();
                });
        }

        /// <summary>
        /// The value must be normalized i.e. betwen 0 and 1.
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="callback"></param>
        public void ScaleToDistance(float distance, float duration = 1f, Action callback = null)
        {
            if (_distanceTween != null) DOTween.Kill(_distanceTween);
            _distanceTween = DOTween.To(getter: () => Distance, setter: x => Distance = x, endValue: distance, duration: duration)
                .OnComplete(() =>
                {
                    _distanceTween = null;
                    callback?.Invoke();
                });
        }

    }
}
