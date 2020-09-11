using System;
using UnityEngine;
using SC.InputSystem;
using SC.InputSystem.InputDeviceHead;
using SC.InputSystem.InputDeviceHandShank;
using SC.InputSystem.InputDevice26DofGesture;
public class API_InputSystem {

    ///API-No.50
    /// <summary>
    /// 获取InputSystem的单例
    /// </summary>
    /// <returns></returns>
    public static InputSystem GetInstant() {
        return InputSystem.Instant;
    }

    ///API-No.51
    /// <summary>
    /// Inputsystem是否初始化完成
    /// </summary>
    /// <returns>true 表示初始化完成，反之</returns>
    public static bool IsISInitialized() {
        if(InputSystem.Instant) {
            return InputSystem.Instant.Initialize;
        }
        return false;
    }

    ///API-No.52
    /// <summary>
    /// 设置InputSystem初始化完成时的回调
    /// </summary>
    /// <param name="action">委托的方法</param>
    public static void AddInitializedCallBack(Action action) {
        InputSystem.ISInitializeCallBack += action;
    }

    ///API-No.53
    public static void RemoveInitializedCallBack(Action action) {
        InputSystem.ISInitializeCallBack -= action;
    }

    ///API-No.54
    /// <summary>
    /// 使能某个输入设备，支持的输入设备见InputDeviceType
    /// </summary>
    /// <param name="inputDevice">输入设备</param>
    public static void EnableInputDeivce(InputDeviceType inputDevice) {
        if(InputSystem.Instant) {
            InputSystem.Instant.RegisterInputDevice(inputDevice);
        }
    }

    ///API-No.55
    /// <summary>
    /// 关闭某个输入设备，支持的输入设备见InputDeviceType
    /// </summary>
    /// <param name="inputDevice">输入设备</param>
    public static void DisableInputDeivce(InputDeviceType inputDevice) {
        if(InputSystem.Instant) {
            InputSystem.Instant.UnRegisterInputDevice(inputDevice);
        }
    }

    ///API-No.56
    ///监听某个按键事件方式1
    ///using SC.InputSystem ; 然后继承PointerHandlers类并重写需要的方法
    ///支持的事件有：
    ///OnSCPointerDown OnSCPointerEnter OnSCPointerExit OnSCPointerDrag OnSCPointerUp
    ///OnSCBothHandPointerDown OnSCBothHandPointerDrag OnSCBothHandPointerUp


    ///API-No.57
    ///监听某个按键事件方式2
    ///using SC.InputSystem ; 然后继承PointerDelegate类然后重写对应的事件
    ///支持的事件有：
    ///partAnyKeyDownDelegate partAnyKeyLongDelegate partAnyKeyUpDelegate partEnterKeyDownDelegate partEnterKeyDragDelegate partEnterKeyUpDelegate


    ///API-No.58
    /// <summary>
    /// 输入设备检测的目标，优先级为Head/BTRight/BTLeft/GTRight/GTLeft
    /// </summary>
    public static GameObject Target {
        get {
            if(API_InputSystem_Head.Head != null) {
                return API_InputSystem_Head.HeadHitTarget;
            } else if(API_InputSystem_Bluetooth.BTRight != null) {
                return API_InputSystem_Bluetooth.BTHitTarget(API_InputSystem_Bluetooth.BTType.Right);
            } else if(API_InputSystem_Bluetooth.BTLeft != null) {
                return API_InputSystem_Bluetooth.BTHitTarget(API_InputSystem_Bluetooth.BTType.Left);
            } else if(API_InputSystem_Gesture26Dof.GTRight != null) {
                return API_InputSystem_Gesture26Dof.GTHitTarget(API_InputSystem_Gesture26Dof.GestureType.Right);
            } else if(API_InputSystem_Gesture26Dof.GTLeft != null) {
                return API_InputSystem_Gesture26Dof.GTHitTarget(API_InputSystem_Gesture26Dof.GestureType.Left);
            }
            return null;
        }
    }

    ///API-No.59
    /// <summary>
    /// 输入设备的发射射线起点，优先级为Head/BTRight/BTLeft/GTRight/GTLeft
    /// </summary>
    public static GameObject Gazer {
        get {
            if(API_InputSystem_Head.Head != null) {
                return API_InputSystem_Head.Head.inputDeviceUIBase.model.lineIndicate.StartPoint.gameObject;
            } else if(API_InputSystem_Bluetooth.BTRight != null) {
                return API_InputSystem_Bluetooth.BTRight.inputDeviceUIBase.model.lineIndicate.StartPoint.gameObject;
            } else if(API_InputSystem_Bluetooth.BTLeft != null) {
                return API_InputSystem_Bluetooth.BTLeft.inputDeviceUIBase.model.lineIndicate.StartPoint.gameObject;
            } else if(API_InputSystem_Gesture26Dof.GTRight != null) {
                return API_InputSystem_Gesture26Dof.GTRight.inputDeviceUIBase.model.lineIndicate.StartPoint.gameObject;
            } else if(API_InputSystem_Gesture26Dof.GTLeft != null) {
                return API_InputSystem_Gesture26Dof.GTLeft.inputDeviceUIBase.model.lineIndicate.StartPoint.gameObject;
            }
            return null;
        }
    }


    ///API-No.60
    /// <summary>
    /// 输入设备的发射射线方向，优先级为Head/BTRight/BTLeft/GTRight/GTLeft
    /// </summary>
    public static Vector3 Normal {
        get {
            if(Gazer!=null) {
                return Gazer.transform.forward;
            }
            return Vector3.zero;
        }
    }

    ///API-No.61
    /// <summary>
    /// 输入设备Focus的位置，优先级为Head/BTRight/BTLeft/GTRight/GTLeft
    /// </summary>
    public static Vector3 Position {
        get {
            if(API_InputSystem_Head.Head != null) {
                return API_InputSystem_Head.GetHeadFocus.transform.position;
            } else if(API_InputSystem_Bluetooth.BTRight != null) {
                return API_InputSystem_Bluetooth.GetBTFocus(API_InputSystem_Bluetooth.BTType.Right).transform.position;
            } else if(API_InputSystem_Bluetooth.BTLeft != null) {
                return API_InputSystem_Bluetooth.GetBTFocus(API_InputSystem_Bluetooth.BTType.Left).transform.position;
            } else if(API_InputSystem_Gesture26Dof.GTRight != null) {
                return API_InputSystem_Gesture26Dof.GetGTFocus(API_InputSystem_Gesture26Dof.GestureType.Right).transform.position;
            } else if(API_InputSystem_Gesture26Dof.GTLeft != null) {
                return API_InputSystem_Gesture26Dof.GetGTFocus(API_InputSystem_Gesture26Dof.GestureType.Left).transform.position;
            }
            return Vector3.zero;
        }
    }

    ///API-No.62
    /// <summary>
    /// 获取当前的具体输入设备，优先级为Head/BTRight/BTLeft/GTRight/GTLeft
    /// </summary>
    public static InputDevicePartBase InputDeviceCurrent {
        get {
            if(API_InputSystem_Head.Head != null) {
                return API_InputSystem_Head.Head;
            } else if(API_InputSystem_Bluetooth.BTRight != null) {
                return API_InputSystem_Bluetooth.BTRight;
            } else if(API_InputSystem_Bluetooth.BTLeft != null) {
                return API_InputSystem_Bluetooth.BTLeft;
            } else if(API_InputSystem_Gesture26Dof.GTRight != null) {
                return API_InputSystem_Gesture26Dof.GTRight;
            } else if(API_InputSystem_Gesture26Dof.GTLeft != null) {
                return API_InputSystem_Gesture26Dof.GTLeft;
            }
            return null;
        }
    }

}
