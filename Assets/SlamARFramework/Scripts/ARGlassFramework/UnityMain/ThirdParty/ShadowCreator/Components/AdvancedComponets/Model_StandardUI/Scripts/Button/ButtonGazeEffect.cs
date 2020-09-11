using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;
using SC.InputSystem;

namespace SC
{
    public class ButtonGazeEffect : PointEffectBase
    {
        /// <summary>
        /// 初始值
        /// </summary>
        private float initLocalScaleValue = 0f;
        /// <summary>
        /// 目标值
        /// </summary>
        public float endLocalScaleValue = 0.1f;
        /// <summary>
        /// 初始是否显示Mesh
        /// </summary>
        private bool InitMeshRenderState=false;

        private MeshRenderer _meshRenderer;
        protected MeshRenderer meshRenderer {
            get {
                if (_meshRenderer == null) {
                    try {
                        _meshRenderer = GetComponent<MeshRenderer>();
                        InitMeshRenderState = _meshRenderer.enabled;
                    } catch (Exception e) {
                        Debug.Log(e);
                    }
                }
                return _meshRenderer;
            }
        }

        private AutoClick _autoClick;
        protected AutoClick autoClick {
            get {
                if (_autoClick == null) {
                    try {
                        _autoClick = GetComponentInParent<AutoClick>();
                    } catch (Exception e) {
                        Debug.Log(e);
                    }
                }
                return _autoClick;
            }
        }

        public override void OnPointerEnter(PointerEventData eventData) {
            base.OnPointerEnter(eventData);
            if(!autoClick)
                return;

            if (!InitMeshRenderState && meshRenderer) {
                meshRenderer.enabled = true;
            }

            transform.DOScaleZ(endLocalScaleValue, autoClick.autoClickTime).SetAutoKill(true).SetId("entertween");
        }

        public override void OnPointerClick(PointerEventData eventData) {
            base.OnPointerClick(eventData);
            if(!autoClick)
                return;

            if (!InitMeshRenderState && meshRenderer) {
                meshRenderer.enabled = false;
            }
            DOTween.Kill("entertween");
            transform.DOScaleZ(initLocalScaleValue, 0).SetAutoKill(true);
        }

        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            if(!autoClick)
                return;

            if (!InitMeshRenderState && meshRenderer) {
                meshRenderer.enabled = false;
            }
            DOTween.Kill("entertween");
            transform.DOScaleZ(initLocalScaleValue, 0).SetAutoKill(true);
        }

    }
}
