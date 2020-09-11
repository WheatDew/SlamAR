using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace SC.InputSystem {
    public class AutoClick : PointerHandlers {

        public float autoClickTime = 3;
        Coroutine coroutineAddKey;
        bool isAddKeyFinish = false;

        public override void OnSCPointerEnter(InputDevicePartBase part, SCPointEventData eventData) {
            part.inputDeviceUIBase.model.lineIndicate.focus.endOfPointWhenTarget.AutoClickAnimationStart(autoClickTime);
            coroutineAddKey = StartCoroutine(AddKey(part, eventData));
        }

        public override void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData) {
            part.inputDeviceUIBase.model.lineIndicate.focus.endOfPointWhenTarget.AutoClickAnimationStop();
            StopCoroutine(coroutineAddKey);
            if(isAddKeyFinish) {
                isAddKeyFinish = false;
                part.inputDataBase.InputDataAddKey(InputKeyCode.Enter, InputKeyState.UP);
            }
        }

        public override void OnSCPointerExit(InputDevicePartBase part, SCPointEventData eventData) {
            part.inputDeviceUIBase.model.lineIndicate.focus.endOfPointWhenTarget.AutoClickAnimationStop();

            StopCoroutine(coroutineAddKey);
            if(isAddKeyFinish) {
                isAddKeyFinish = false;
                part.inputDataBase.InputDataAddKey(InputKeyCode.Enter, InputKeyState.UP);
            }
        }

        IEnumerator AddKey(InputDevicePartBase part, SCPointEventData eventData) {
            yield return new WaitForSeconds(autoClickTime);
            part.inputDataBase.InputDataAddKey(InputKeyCode.Enter, InputKeyState.DOWN);
            isAddKeyFinish = true;
        }

    }
}