using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

namespace SC
{
    public class LocalPositionEffect : PointEffectBase {
        /// <summary>
        /// 初始值
        /// </summary>
        float initLocalPositionValue;
        /// <summary>
        /// 开始值
        /// </summary>
        public float LocalPositionValue;

        protected override void Awake() {
            base.Awake();
            initLocalPositionValue = transform.localPosition.z;
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            base.OnPointerEnter(eventData);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, LocalPositionValue);
        }
        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, initLocalPositionValue);
        }


    }
}
