using SC;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Hotfix
{
    public class QRCodeTaskGroupEvent : HotfixEventBase
    {
        public override void OnInitState(GameObject obj)
        {
            //obj.transform.Find("Step1").parent = Transform.FindObjectOfType<ShadowSystem>().transform;
        }
    }
}

