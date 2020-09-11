using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    float x, y;
    [SerializeField]
    float speed = 0;
    [SerializeField]
    bool isQuaternion = false;
    [SerializeField]
    bool isDir = false;

    private void OnMouseDown()
    {
        StartRotate();
    }
    private void OnMouseDrag()
    {
        HoldonRotate();
    }

    /// <summary>
    /// 开始旋转
    /// </summary>
    public void StartRotate()
    {
        if (Mathf.Abs(ShowRotationLikeInspector(transform).y % 360) > 90 && Mathf.Abs(ShowRotationLikeInspector(transform).y % 360) < 270)
        {
            isDir = false;
        }
        else
        {
            isDir = true;
        }
    }

    /// <summary>
    /// 持续旋转
    /// </summary>
    public void HoldonRotate()
    {
        x = Input.GetAxis("Mouse X");
        y = Input.GetAxis("Mouse Y");
        Quaternion target = Quaternion.Euler(y * speed, -x * speed, 0);
        if (Mathf.Abs(ShowRotationLikeInspector(transform).y) % 360 >= 90 && Mathf.Abs(ShowRotationLikeInspector(transform).y % 360) < 270)
        {
            isDir = false;
        }
        else
        {
            isDir = true;
        }
        if (isQuaternion)
            transform.rotation = Quaternion.Slerp(transform.rotation, transform.rotation * target, 1);
        else
        {
            if (!isDir)
            {
                SetRotationLikeInspector(transform, ShowRotationLikeInspector(transform) + new Vector3(-y * speed, -x * speed, 0));
            }
            else
            {
                SetRotationLikeInspector(transform, ShowRotationLikeInspector(transform) + new Vector3(y * speed, -x * speed, 0));
            }
        }
    }

    /// <summary>
    /// 显示unity编辑器下的角度
    /// </summary>
    /// <param name="t">目标物体</param>
    Vector3 ShowRotationLikeInspector(Transform t)
    {
        var type = t.GetType();
        var mi = type.GetMethod("GetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrder = rotationOrderPro.GetValue(t, null);
        var EulerAnglesInspector = mi.Invoke(t, new[] { rotationOrder });
        return (Vector3)EulerAnglesInspector;
    }

    /// <summary>
    /// 设置unity编辑器下的角度
    /// </summary>
    /// <param name="t">目标物体</param>
    /// <param name="v">目标角度</param>
    void SetRotationLikeInspector(Transform t, Vector3 v)
    {
        var type = t.GetType();
        var mi = type.GetMethod("SetLocalEulerAngles", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrderPro = type.GetProperty("rotationOrder", BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
        var rotationOrder = rotationOrderPro.GetValue(t, null);
        mi.Invoke(t, new[] { v, rotationOrder });
    }
}
