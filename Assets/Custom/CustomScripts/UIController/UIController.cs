using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private LoginGroup loginGroup;
    private LoginGroup currentLoginGroup;

    [SerializeField] private QRCodeGroup qRCodeGroup;
    private QRCodeGroup currentQRCodeGroup;

    [SerializeField] private TaskGroup taskGroup;
    private TaskGroup currentTaskGroup;

    [SerializeField] private ExpertListGroup expertListGroup;
    private ExpertListGroup currentExpertListGroup;

    [SerializeField] private ExpertCallGroup expertCallGroup;
    private ExpertCallGroup currentExpertCallGroup;

    [SerializeField] private MainGroup mainGroup;
    private MainGroup currentMainGroup;

    [SerializeField] private CameraGroup cameraGroup;
    private CameraGroup currentCameraGroup;

    [SerializeField] private VideoGroup videoGroup;
    private VideoGroup currentVideoGroup;

    private void Start()
    {
        CreateLoginGroup();
    }

    //实例化函数
    
    public void CreateLoginGroup()
    {
        currentLoginGroup = Instantiate(loginGroup);
        Debug.Log("创建登录界面成功");
    }

    public void CreateQRCodeGroup()
    {
        currentQRCodeGroup = Instantiate(qRCodeGroup);
        Debug.Log("创建扫码界面成功");
    }

    public void CreateTaskGroup()
    {
        currentTaskGroup = Instantiate(taskGroup);
    }

    public void CreateExpertListGroup()
    {
        currentExpertListGroup = Instantiate(expertListGroup);
    }

    public void CreateExpertCallGroup()
    {
        currentExpertCallGroup = Instantiate(expertCallGroup);
    }

    public void CreateMainGroup()
    {
        currentMainGroup = Instantiate(mainGroup);
        Debug.Log("创建任务界面成功");
    }
    public void CreateCameraGroup(TextMesh exLog)
    {
        currentCameraGroup = Instantiate(cameraGroup);
        currentCameraGroup.exLog = exLog;
    }
    public CameraGroup GetCurrentCameraGroup()
    {
        return currentCameraGroup;
    }

    public void CreateVideoGroup(TextMesh exLog)
    {
        currentVideoGroup = Instantiate(videoGroup);
        currentCameraGroup.exLog = exLog;
    }

    //销毁函数

    public void DestroyLoginGroup()
    {
        if(currentLoginGroup!=null)
        Destroy(currentLoginGroup.gameObject);
        Debug.Log("删除登录界面成功");
    }

    public void DestroyQRCodeGroup()
    {
        if(currentQRCodeGroup!=null)
        Destroy(currentQRCodeGroup.gameObject);
        Debug.Log("删除二维码界面成功");
    }

    public void DestroyTaskGroup()
    {
        if(currentTaskGroup!=null)
        Destroy(currentTaskGroup.gameObject);
    }

    public void DestroyExpertListGroup()
    {
        if(currentExpertListGroup!=null)
        Destroy(currentExpertListGroup.gameObject);
    }

    public void DestroyExperCallGroup()
    {
        if(currentExpertCallGroup!=null)
        Destroy(currentExpertCallGroup.gameObject);
    }
    public void DestroyMainGroup()
    {
        if(currentMainGroup!=null)
        Destroy(currentMainGroup.gameObject);
    }
    public void DestroyCameraGroup()
    {
        if(currentCameraGroup!=null)
        Destroy(currentCameraGroup.gameObject);
    }

    public void DestroyVideoGroup()
    {
        if(currentVideoGroup!=null)
        Destroy(currentVideoGroup.gameObject);
    }
}
