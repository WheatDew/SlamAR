using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
namespace Unity.Hotfix {
    public class SphereEvent : HotfixEventBase
    {
        public override void OnInitState(GameObject obj)
        {

        }
        public override void OnHandlerRayDown(GameObject obj, SCPointEventData eventData)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public override void OnHandlerRayUp(GameObject obj, SCPointEventData eventData)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.green;
        }
        
    }
}