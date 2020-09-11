using System;
using UnityEngine;
using SC.InputSystem;
using SC.InputSystem.InputDeviceHead;
using SC.InputSystem.InputDeviceHandShank;
using SC.InputSystem.InputDevice26DofGesture;
public class API_InputSystem_Bluetooth {

    ///API-No.150
    /// <summary>
    /// 获取InputSystem支持的手柄输入设备,手柄输入设备包含二个Part，名曰：BTRight,BTLeft,也就是第一个手柄和第二个手柄
    /// </summary>
    /// <returns>null表示不支持或者InputSystem未初始好</returns>
    public static InputDeviceHandShank BTDevice {
        get {
            if(InputSystem.Instant) {
                return InputSystem.Instant.HandShank;
            }
            return null;
        }
    }

    ///API-No.151
    /// <summary>
    /// 手柄输入设备连接的第一个手柄
    /// </summary>
    public static InputDeviceHandShankPart BTRight {
        get {
            if(BTDevice && BTDevice.inputDeviceHandShankPartList[0]) {
                //return BTDevice.inputDeviceHandShankPartList[0].inputDataBase.isVaild ? BTDevice.inputDeviceHandShankPartList[0] : null;
                return BTDevice.inputDeviceHandShankPartList[0];
            }
            return null;
        }
    }

    ///API-No.152
    /// <summary>
    /// 手柄输入设备连接的第二个手柄
    /// </summary>
    public static InputDeviceHandShankPart BTLeft {
        get {
            if(BTDevice && BTDevice.inputDeviceHandShankPartList[1]) {
                //return BTDevice.inputDeviceHandShankPartList[1].inputDataBase.isVaild ? BTDevice.inputDeviceHandShankPartList[1] : null;
                return BTDevice.inputDeviceHandShankPartList[1];
            }
            return null;
        }
    }

    public enum BTType { 
        Left,
        Right
    }

    ///API-No.153
    /// <summary>
    /// BTRight/BTLeft的四元素，全局坐标
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns></returns>
    public static Quaternion BTRotation(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataHandShank.rotation;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataHandShank.rotation;
        }
        return Quaternion.identity;
    }

    ///API-No.154
    /// <summary>
    /// BTRight/BTLeft的四元素，全局坐标
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns></returns>
    public static Vector3 BTPosition(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataHandShank.position;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataHandShank.position;
        }
        return Vector3.zero;
    }

    ///API-No.155
    /// <summary>
    /// BTRight/BTLeft 的触摸板是否触摸
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns>ture表示被触摸，反之</returns>
    public static bool IsBTTpTouch(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataHandShank.isTpTouch;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataHandShank.isTpTouch;
        }
        return false;
    }

    ///API-No.156
    /// <summary>
    /// BTRight/BTLeft 的触摸板触摸数据
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns></returns>
    public static Vector2 BTTpTouchInfo(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataHandShank.tpPosition;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataHandShank.tpPosition;
        }
        return Vector2.zero;
    }

    ///API-No.157
    /// <summary>
    /// BTRight/BTLeft 的触摸板触摸数据
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns></returns>
    public static string BTName(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataHandShank.blueToothName;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataHandShank.blueToothName;
        }
        return "";
    }

    ///API-No.158
    /// <summary>
    /// BTRight/BTLeft 检测到碰撞信息集合，包含碰撞到的物体，数据等
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns></returns>
    public static SCPointEventData BTPointerEventData(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataBase.pointerEventData;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataBase.pointerEventData;
        }
        return null;
    }

    ///API-No.159
    /// <summary>
    /// BTRight/BTLeft 碰撞到Collider，若不为null,可以通过BTHitInfo获取碰撞信息，
    /// </summary>
    /// <param name="type">右手柄 BTRight /  左手柄 BTLeft</param>
    /// <returns></returns>
    public static GameObject BTHitTarget(BTType type = BTType.Right) {
        if(BTPointerEventData(type) != null) {
            return BTPointerEventData(type).target;
        }
        return null;
    }

    ///API-No.160
    /// <summary>
    /// BTRight/BTLeft 碰撞信息
    /// </summary>
    /// <returns></returns>
    public static RaycastHit BTHitInfo(BTType type = BTType.Right) {
        if(BTPointerEventData(type) != null) {
            return BTPointerEventData(type).hitinfo;
        }
        return new RaycastHit();
    }

    ///API-No.161
    /// <summary>
    /// BTRight/BTLeft 拖拽的物体
    /// </summary>
    public static GameObject BTDragTarget(BTType type = BTType.Right) {
        if(BTPointerEventData(type) != null) {
            return BTPointerEventData(type).dragTarget;
        }
        return null;
    }

    ///API-No.162
    /// <summary>
    /// BTRight/BTLeft某个按键是否按下，当前帧有效，下帧复位，参考Input.GetKeyDown
    /// </summary>
    /// <param name="inputKeyCode">具体按键，BTRight/BTLeft支持Enter/Back/Function</param>
    /// <returns></returns>
    public static bool IsBTKeyDown(InputKeyCode inputKeyCode, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataBase.GetKeyDown(inputKeyCode);
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataBase.GetKeyDown(inputKeyCode);
        }
        return false;
    }

    ///API-No.163
    /// <summary>
    /// BTRight/BTLeft某个按键是否按下后松开，当前帧有效，下帧复位，参考Input.GetKeyUp
    /// </summary>
    /// <param name="inputKeyCode">具体按键，BTRight/BTLeft支持Enter/Back/Function</param>
    /// <returns></returns>
    public static bool IsBTKeyUp(InputKeyCode inputKeyCode, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataBase.GetKeyUp(inputKeyCode);
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataBase.GetKeyUp(inputKeyCode);
        }
        return false;
    }

    ///API-No.164
    /// <summary>
    /// BTRight/BTLeft某个按键的状态信息，当前帧有效，下帧复位
    /// </summary>
    /// <param name="inputKeyCode">具体按键，BTRight/BTLeft支持Enter/Back/Function</param>
    /// <returns></returns>
    public static InputKeyState BTKeyState(InputKeyCode inputKeyCode, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataBase.GetKeyState(inputKeyCode);
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataBase.GetKeyState(inputKeyCode);
        }
        return InputKeyState.Null;
    }

    ///API-No.165
    /// <summary>
    /// BTRight/BTLeft某个按键的实时状态，参考Input.GetKey
    /// </summary>
    /// <param name="inputKeyCode">具体按键，BTRight/BTLeft支持Enter/Back/Function</param>
    /// <returns></returns>
    public static InputKeyState BTKeyCurrentState(InputKeyCode inputKeyCode, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDataBase.GetKeyCurrentState(inputKeyCode);
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDataBase.GetKeyCurrentState(inputKeyCode);
        }
        return InputKeyState.Null;
    }

    ///API-No.166
    /// <summary>
    /// BTRight/BTLeft发送一个按键，注意，发送按键至少需发送一个Down，然后再发送一个Up，此API模拟按键按下动作
    /// </summary>
    /// <param name="inputKeyCode">具体按键</param>
    /// <param name="inputKeyState">按键的状态</param>
    public static void BTKeyAddKey(InputKeyCode inputKeyCode, InputKeyState inputKeyState, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            BTRight.inputDataBase.InputDataAddKey(inputKeyCode, inputKeyState);
        } else if(BTLeft && type == BTType.Left) {
            BTLeft.inputDataBase.InputDataAddKey(inputKeyCode, inputKeyState);
        }
    }

    ///API-No.167
    /// <summary>
    /// BTRight/BTLeft设置可检测Collider的范围半径 默认50米
    /// </summary>
    /// <param name="distance">单位米</param>
    public static void SetBTRayCastDistance(float distance, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            BTRight.inputDeviceTargetDetecterBase.MaxRaycastRange = distance;
        } else if(BTLeft && type == BTType.Left) {
            BTLeft.inputDeviceTargetDetecterBase.MaxRaycastRange = distance;
        }
    }

    ///API-No.168
    /// <summary>
    /// BTRight/BTLeft设置可见光束的长度 默认3米
    /// </summary>
    /// <param name="distance">单位米</param>
    public static void SetBTEndPointerDistance(float distance, BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            BTRight.inputDeviceTargetDetecterBase.MaxEndPointRange = distance;
        } else if(BTLeft && type == BTType.Left) {
            BTLeft.inputDeviceTargetDetecterBase.MaxEndPointRange = distance;
        }
    }

    ///API-No.169
    /// <summary>
    /// BTRight/BTLeft获取光束终点的Focus对象
    /// </summary>
    /// <param name="distance">单位米</param>
    public static Focus GetBTFocus(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDeviceUIBase.model.lineIndicate.focus;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDeviceUIBase.model.lineIndicate.focus;
        }
        return null;
    }

    ///API-No.170
    /// <summary>
    /// BTRight/BTLeft获取光束
    /// </summary>
    /// <param name="distance">单位米</param>
    public static LineRenderer GetBTLine(BTType type = BTType.Right) {
        if(BTRight && type == BTType.Right) {
            return BTRight.inputDeviceUIBase.model.lineIndicate.line;
        } else if(BTLeft && type == BTType.Left) {
            return BTLeft.inputDeviceUIBase.model.lineIndicate.line;
        }
        return null;
    }

    ///API-No.171
    /// <summary>
    /// 开启BTRight/BTLeft
    /// </summary>
    /// <param name="type"></param>
    public static void EnableBT(BTType type = BTType.Right) {
        if(BTDevice && type == BTType.Right) {
            BTDevice.OneHandShankOpen = true;
        } else if(BTDevice && type == BTType.Left) {
            BTDevice.TwoHandShankOpen = true;
        }
    }

    ///API-No.172
    /// <summary>
    /// 关闭BTRight/BTLeft
    /// </summary>
    /// <param name="type"></param>
    public static void DisableableBT(BTType type = BTType.Right) {
        if(BTDevice && type == BTType.Right) {
            BTDevice.OneHandShankOpen = false;
        } else if(BTDevice && type == BTType.Left) {
            BTDevice.TwoHandShankOpen = false;
        }
    }


}
