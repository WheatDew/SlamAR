using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using System;

namespace SC
{
    public class MaterialEffect : PointEffectBase {

        /// <summary>
        /// 初始是否显示Mesh
        /// </summary>
        private bool InitMeshRenderState;
        private MeshRenderer _meshRenderer;
        protected MeshRenderer meshRenderer {
            get {
                if(_meshRenderer == null) {
                    try {
                        _meshRenderer = GetComponent<MeshRenderer>();
                    } catch(Exception e) {
                        Debug.Log(e);
                    }
                }
                return _meshRenderer;
            }
        }

        public Material normalMaterial;
        public Material enterMaterial;
        public Material clickMaterial;

        protected override void Awake() {
            base.Awake();
            if(meshRenderer) {
                InitMeshRenderState = meshRenderer.enabled;
            }
        }

        protected override void OnEnable() {
            base.OnEnable();
            if(meshRenderer) {
                meshRenderer.enabled = InitMeshRenderState;
                if(normalMaterial) {
                    meshRenderer.material = normalMaterial;
                }
            }
        }


        public override void OnPointerEnter(PointerEventData eventData) {
            base.OnPointerEnter(eventData);
            if(!InitMeshRenderState && meshRenderer) {
                meshRenderer.enabled = true;
            }
            if (meshRenderer && enterMaterial) {
                meshRenderer.material = enterMaterial;
            }
        }

        public override void OnPointerClick(PointerEventData eventData) {
            base.OnPointerClick(eventData);
            if (meshRenderer && clickMaterial) {
                meshRenderer.material = clickMaterial;
            }
        }

        public override void OnPointerExit(PointerEventData eventData) {
            base.OnPointerExit(eventData);
            if(!InitMeshRenderState && meshRenderer) {
                meshRenderer.enabled = false;
            }
            if (meshRenderer && normalMaterial) {
                meshRenderer.material = normalMaterial;
            }
        }

        public override void ClickFinish() {
            base.ClickFinish();
            if(meshRenderer && enterMaterial) {
                meshRenderer.material = enterMaterial;
            }
        }


    }
}
