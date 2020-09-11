using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Unity.Hotfix
{
    public class TaskSystemGroupEvent : HotfixEventBase
    {
        public override void OnInitState(GameObject obj)
        {
            obj.transform.localScale = Vector3.zero;
        }
    }
}

