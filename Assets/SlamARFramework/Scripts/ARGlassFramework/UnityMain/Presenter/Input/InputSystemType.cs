using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputSystemType : MonoBehaviour
{
    public InputType inputType = new InputType();
    public InputButtonType inputButtonType = new InputButtonType();
    public enum InputType
    { 
        PC,
        Android,
        IOS,
        ShadowsARHeadWithHandler,
    }

    public enum InputButtonType
    { 
        None,
        MouseEnter,
        MouseExit,
        MouseLeftDown,
        MouseLeftUp,
        MouseLeftDoubleClick,
        MouseRightDown,
        MouseRightUp,
        MouseRightDoubleClick,
        MouseMiddleUp,
        MouseMiddleDown,
        MouseMiddleClick,
        MouseMiddleDoubleClick
    }
}
