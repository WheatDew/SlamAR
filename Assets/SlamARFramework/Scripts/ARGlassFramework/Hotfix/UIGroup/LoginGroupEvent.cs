using System.Collections;
using System.Collections.Generic;
using SC.InputSystem;
using UnityEngine;
using UnityEngine.UI;

namespace Unity.Hotfix
{
    public class LoginGroupEvent : HotfixEventBase
    {
        private GameObject step1;
        private GameObject step2;
        private Text testText;

        public override void OnInitState(GameObject obj)
        {
            step1 = obj.transform.Find("Step1").gameObject;
            step2 = obj.transform.Find("Step2").gameObject;
            testText = obj.transform.Find("testText").GetComponent<Text>();
            step1.SetActive(true);
            step2.SetActive(false);
            Debug.Log(obj.name);
        }

        public override void OnHandlerRayDown(GameObject obj, SCPointEventData eventData)
        {
            testText.text = Random.Range(0, 100).ToString();
        }
    }
}

