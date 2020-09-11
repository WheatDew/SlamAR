using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Unity.Hotfix {
    public class CubeEvent : HotfixEventBase
    {
        bool isHeadRayEnter;
        public override void OnInitState(GameObject obj)
        {
            base.OnInitState(obj);
        }

        public override void OnHandlerRayDown(GameObject obj, SCPointEventData eventData)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.yellow;
        }

        public override void OnHandlerRayUp(GameObject obj, SCPointEventData eventData)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.white;
        }

        public override void OnHandlerRayEnter(GameObject obj, SCPointEventData eventData)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.red;
        }

        public override void OnHandlerRayExit(GameObject obj, SCPointEventData eventData)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.black;
        }

        public override void OnHeadRayEnter(GameObject obj, SCPointEventData eventData)
        {
            if (!isHeadRayEnter)
            {
                isHeadRayEnter = true;
                obj.GetComponent<MeshRenderer>().material.color = Color.blue;
            }
        }

        public override void OnHeadRayExit(GameObject obj, SCPointEventData eventData)
        {
            if (isHeadRayEnter)
            {
                isHeadRayEnter = false;
                obj.GetComponent<MeshRenderer>().material.color = new Color(0.6f, 0.6f, 1, 1);
            }
        }

        public override void OnPlayerEnter(GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.green;
        }

        public override void OnPlayerExit(GameObject obj)
        {
            obj.GetComponent<MeshRenderer>().material.color = Color.cyan;
        }

        public override void OnDragTarget(GameObject obj, SCPointEventData eventData)
        {
           obj.transform.position = eventData.dragSlowPosition;
        }
    }
}