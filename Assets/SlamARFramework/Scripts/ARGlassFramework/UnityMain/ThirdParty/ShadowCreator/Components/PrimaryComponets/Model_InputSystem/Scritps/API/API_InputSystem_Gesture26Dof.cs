using System;
using UnityEngine;
using SC.InputSystem;
using SC.InputSystem.InputDeviceHead;
using SC.InputSystem.InputDeviceHandShank;
using SC.InputSystem.InputDevice26DofGesture;
public class API_InputSystem_Gesture26Dof {

    ///API-No.200
    /// <summary>
    /// 获取InputSystem支持的自由手势输入设备,自由手势输入设备包含二个Part，名曰：GTLeft,GTRight,也就是左手/右手
    /// </summary>
    /// <returns>null表示不支持或者InputSystem未初始好</returns>
    public static InputDevice26DofGesture Gesture26Dof {
        get {
            if(InputSystem.Instant) {
                return InputSystem.Instant.Gesture26Dof;
            }
            return null;
        }
    }

    public enum GestureType {
        Left=0,
        Right=1,
    }

    ///API-No.201
    /// <summary>
    /// 自由手势输入设备左手
    /// </summary>
    public static InputDevice26DofGesturePart GTLeft {
        get {
            if(Gesture26Dof && Gesture26Dof.InputDevice26DofGesturePartList[0]) {
                //return Gesture26Dof.InputDevice26DofGesturePartList[0].inputDataBase.isVaild ? Gesture26Dof.InputDevice26DofGesturePartList[0] : null;
                return Gesture26Dof.InputDevice26DofGesturePartList[0];
            }
            return null;
        }
    }

    ///API-No.202
    /// <summary>
    /// 自由手势输入设备右手
    /// </summary>
    public static InputDevice26DofGesturePart GTRight {
        get {
            if(Gesture26Dof && Gesture26Dof.InputDevice26DofGesturePartList[1]) {
                //return Gesture26Dof.InputDevice26DofGesturePartList[1].inputDataBase.isVaild ? Gesture26Dof.InputDevice26DofGesturePartList[1] : null;
                return Gesture26Dof.InputDevice26DofGesturePartList[1];
            }
            return null;
        }
    }

    ///API-No.203
    /// <summary>
    /// GTLeft/GTRight的手势数据，具体数据见handInfo
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static handInfo GThandInfo(GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputData26DofGesture.handInfo;
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputData26DofGesture.handInfo;
        }
        return null;
    }

    ///API-No.204
    /// <summary>
    /// GTLeft/GTRight 检测到碰撞信息集合，包含碰撞到的物体，数据等
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static SCPointEventData GTPointerEventData(GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDataBase.pointerEventData;
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDataBase.pointerEventData;
        }
        return null;
    }

    ///API-No.205
    /// <summary>
    /// GTLeft/GTRight 碰撞到Collider，若不为null,可以通过GTHitInfo获取碰撞信息，
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static GameObject GTHitTarget(GestureType type = GestureType.Right) {
        if(GTPointerEventData(type) != null) {
            return GTPointerEventData(type).target;
        }
        return null;
    }

    ///API-No.206
    /// <summary>
    /// GTLeft/GTRight 碰撞信息
    /// </summary>
    /// <returns></returns>
    public static RaycastHit GTHitInfo(GestureType type = GestureType.Right) {
        if(GTPointerEventData(type) != null) {
            return GTPointerEventData(type).hitinfo;
        }
        return new RaycastHit();
    }

    ///API-No.207
    /// <summary>
    /// GTLeft/GTRight 拖拽的物体
    /// </summary>
    public static GameObject GTDragTarget(GestureType type = GestureType.Right) {
        if(GTPointerEventData(type) != null) {
            return GTPointerEventData(type).dragTarget;
        }
        return null;
    }

    ///API-No.208
    /// <summary>
    /// GTLeft/GTRight某个按键是否按下，当前帧有效，下帧复位，参考Input.GetKeyDown
    /// </summary>
    /// <param name="inputKeyCode">具体按键，GTLeft/GTRight支持Enter/Back/Function</param>
    /// <returns></returns>
    public static bool IsGTKeyDown(InputKeyCode inputKeyCode, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDataBase.GetKeyDown(inputKeyCode);
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDataBase.GetKeyDown(inputKeyCode);
        }
        return false;
    }

    ///API-No.209
    /// <summary>
    /// GTLeft/GTRight某个按键是否按下后松开，当前帧有效，下帧复位，参考Input.GetKeyUp
    /// </summary>
    /// <param name="inputKeyCode">具体按键，GTLeft/GTRight支持Enter/Back/Function</param>
    /// <returns></returns>
    public static bool IsGTKeyUp(InputKeyCode inputKeyCode, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDataBase.GetKeyUp(inputKeyCode);
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDataBase.GetKeyUp(inputKeyCode);
        }
        return false;
    }

    ///API-No.210
    /// <summary>
    /// GTLeft/GTRight某个按键的状态信息，当前帧有效，下帧复位
    /// </summary>
    /// <param name="inputKeyCode">具体按键，GTLeft/GTRight支持Enter/Back/Function</param>
    /// <returns></returns>
    public static InputKeyState GTKeyState(InputKeyCode inputKeyCode, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDataBase.GetKeyState(inputKeyCode);
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDataBase.GetKeyState(inputKeyCode);
        }
        return InputKeyState.Null;
    }

    ///API-No.211
    /// <summary>
    /// GTLeft/GTRight某个按键的实时状态，参考Input.GetKey
    /// </summary>
    /// <param name="inputKeyCode">具体按键，GTLeft/GTRight支持Enter/Back/Function</param>
    /// <returns></returns>
    public static InputKeyState GTKeyCurrentState(InputKeyCode inputKeyCode, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDataBase.GetKeyCurrentState(inputKeyCode);
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDataBase.GetKeyCurrentState(inputKeyCode);
        }
        return InputKeyState.Null;
    }

    ///API-No.212
    /// <summary>
    /// GTLeft/GTRight发送一个按键，注意，发送按键至少需发送一个Down，然后再发送一个Up，此API模拟按键按下动作
    /// </summary>
    /// <param name="inputKeyCode">具体按键</param>
    /// <param name="inputKeyState">按键的状态</param>
    public static void GTKeyAddKey(InputKeyCode inputKeyCode, InputKeyState inputKeyState, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            GTLeft.inputDataBase.InputDataAddKey(inputKeyCode, inputKeyState);
        } else if(GTRight && type == GestureType.Right) {
            GTRight.inputDataBase.InputDataAddKey(inputKeyCode, inputKeyState);
        }
    }

    ///API-No.213
    /// <summary>
    /// GTLeft/GTRight设置可检测Collider的范围半径 默认50米
    /// </summary>
    /// <param name="distance">单位米</param>
    public static void SetGTRayCastDistance(float distance, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            GTLeft.inputDeviceTargetDetecterBase.MaxRaycastRange = distance;
        } else if(GTRight && type == GestureType.Right) {
            GTRight.inputDeviceTargetDetecterBase.MaxRaycastRange = distance;
        }
    }

    ///API-No.214
    /// <summary>
    /// GTLeft/GTRight设置可见光束的长度 默认3米
    /// </summary>
    /// <param name="distance">单位米</param>
    public static void SetGTEndPointerDistance(float distance, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            GTLeft.inputDeviceTargetDetecterBase.MaxEndPointRange = distance;
        } else if(GTRight && type == GestureType.Right) {
            GTRight.inputDeviceTargetDetecterBase.MaxEndPointRange = distance;
        }
    }

    ///API-No.215
    /// <summary>
    /// GTLeft/GTRight获取光束终点的Focus对象
    /// </summary>
    /// <param name="distance">单位米</param>
    public static Focus GetGTFocus(GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDeviceUIBase.model.lineIndicate.focus;
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDeviceUIBase.model.lineIndicate.focus;
        }
        return null;
    }

    ///API-No.216
    /// <summary>
    /// GTLeft/GTRight获取光束
    /// </summary>
    /// <param name="distance">单位米</param>
    public static LineRenderer GetGTLine(GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDeviceUIBase.model.lineIndicate.line;
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDeviceUIBase.model.lineIndicate.line;
        }
        return null;
    }

    ///API-No.217
    /// <summary>
    /// 开启GTLeft
    /// </summary>
    /// <param name="type"></param>
    public static void EnableGT(GestureType type = GestureType.Right) {
        if(Gesture26Dof && type == GestureType.Left) {
            Gesture26Dof.LeftHandOpen = true;
        } else if(Gesture26Dof && type == GestureType.Right) {
            Gesture26Dof.RightHandOpen = true;
        }
    }

    ///API-No.218
    /// <summary>
    /// 关闭GTLeft
    /// </summary>
    /// <param name="type"></param>
    public static void DisableableGT(GestureType type = GestureType.Right) {
        if(Gesture26Dof && type == GestureType.Left) {
            Gesture26Dof.LeftHandOpen = false;
        } else if(Gesture26Dof && type == GestureType.Right) {
            Gesture26Dof.RightHandOpen = false;
        }
    }

    ///API-No.219
    /// <summary>
    /// 获取手势游戏对象，具体见fingerUI
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static Model26DofGesture.fingerUI[] GetFingerUI(GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            return GTLeft.inputDevice26DofGestureUI.model26DofGesture.fingers;
        } else if(GTRight && type == GestureType.Right) {
            return GTRight.inputDevice26DofGestureUI.model26DofGesture.fingers;
        }
        return null;
    }

    ///API-No.220
    /// <summary>
    /// 设置是否启用手势触摸抓取功能
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static void SetTouch(bool isOn,GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            GTLeft.SetTouch(isOn);
        } else if(GTRight && type == GestureType.Right) {
            GTRight.SetTouch(isOn);
        }
    }

    ///API-No.221
    /// <summary>
    /// 设置是否启用手势射线抓取功能
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public static void SetRay(bool isOn, GestureType type = GestureType.Right) {
        if(GTLeft && type == GestureType.Left) {
            GTLeft.SetRay(isOn);
        } else if(GTRight && type == GestureType.Right) {
            GTRight.SetRay(isOn);
        }
    }
}
