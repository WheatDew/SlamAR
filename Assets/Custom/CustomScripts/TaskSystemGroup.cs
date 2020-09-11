using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;



public class TaskSystemGroup : MonoBehaviour
{
    public GameObject[] hideList;
    public TextMesh exLog;

    //摄像头图像类，继承自texture
    WebCamTexture webCamTexture;
    public MeshRenderer ma;
    enum FuncType { Empty,PhotoGraph, Video ,StopVideo }

    FuncType type=FuncType.Empty;

    public RawImage bgimage_02;
    public MeshRenderer photoDisplay;
    public GameObject photoDisplayObj, cameraDisplayObj, cameraButtonObj;

    private MP4Recorder recorder;
    private IClock clock;
    private bool recording;
    private Color32[] pixelBuffer;

    private byte[] imageTytes;
    private bool isPreview=false;

    public SocketIOModule socketIO;



    public void StartRecording()
    {
        StopAllCoroutines();
        // Start recording
        clock = new RealtimeClock();
        recorder = new MP4Recorder(webCamTexture.width, webCamTexture.height, 30);
        pixelBuffer = webCamTexture.GetPixels32();
        recording = true;
    }

    public async void StopRecording()
    {
        // Stop recording
        recording = false;
        if (recorder != null)
        {
            var path = await recorder.FinishWriting();
            // Playback recording
            Debug.Log($"Saved recording to: {path}");
            exLog.text = $"Saved recording to: {path}";
            //var prefix = Application.platform == RuntimePlatform.IPhonePlayer ? "file://" : "";
            //Handheld.PlayFullScreenMovie($"{prefix}{path}");
            Handheld.PlayFullScreenMovie(Application.persistentDataPath + "/MulPhotoes/");
        }
    }

    public void SetStart()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(false);

        photoDisplayObj.SetActive(false);
        cameraDisplayObj.SetActive(false);

        transform.localScale = Vector3.one;

        Debug.Log("设置任务系统成功");
    }

    public void SetRemoteAssistance()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(1).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(5).gameObject.SetActive(true);

        transform.localScale = Vector3.one;

        Debug.Log("设置任务系统成功");
    }

    public void CallExpert(string ExpertNumber)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        transform.GetChild(2).gameObject.SetActive(true);
        transform.GetChild(3).gameObject.SetActive(true);
        transform.GetChild(4).gameObject.SetActive(true);
        transform.GetChild(5).gameObject.SetActive(true);
    }

    private void Start()
    {
        //t = new Texture2D(1920, 1080, TextureFormat.RGB24, true);
        DirectoryInfo di1 = new DirectoryInfo(Application.persistentDataPath + "/MulPhotoes/");
        di1.Create();
        DirectoryInfo di2 = new DirectoryInfo(Application.persistentDataPath + "/my/");
        di2.Create();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (cameraDisplayObj.activeSelf&&(API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Back) || Input.GetMouseButtonDown(2)))
        {
            StopRecording();
            StopCamera();
            StopAllCoroutines();
            cameraDisplayObj.SetActive(false);
            type = FuncType.Empty;
        }
        else if ((API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Back) || Input.GetMouseButtonDown(2)) && transform.GetChild(0).gameObject.activeSelf)
        {

            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
            StopAllCoroutines();
            transform.localScale = Vector3.zero;
            FindObjectOfType<QRCodeTaskGroup>().SetStart();
        }

        if (recording && webCamTexture.didUpdateThisFrame)
        {
            webCamTexture.GetPixels32(pixelBuffer);
            recorder.CommitFrame(pixelBuffer, clock.timestamp);
        }



        if (!isPreview&&(Input.GetMouseButtonDown(0)|| API_InputSystem_Bluetooth.IsBTKeyDown(SC.InputSystem.InputKeyCode.Enter, API_InputSystem_Bluetooth.BTType.Right)
        || API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Enter)))
        {
            switch (type)
            {
                case FuncType.PhotoGraph:
                    CameraPhotographEvent();
                    Debug.Log(isPreview);
                    isPreview = true;
                    break;
                case FuncType.Video:
                    CameraVideoEvent();
                    type = FuncType.StopVideo;
                    break;
                case FuncType.StopVideo:
                    StopCameraVideoEvent();
                    type = FuncType.Video;
                    break;
            }
        }
    }

    /*
     * 按钮事件
     */

    //开启照相功能事件
    IEnumerator PhotoGraphOpenEvent()
    {
        Debug.Log("延时前");
        type = FuncType.Empty;
        cameraDisplayObj.SetActive(true);
        //photoDisplayObj.SetActive(true);
        StartCoroutine(OpenCamera());
        cameraButtonObj.SetActive(true);

        yield return new  WaitForSeconds(1);
        type = FuncType.PhotoGraph;
        Debug.Log("延时后");
    }
    //开启录像功能事件
    IEnumerator VideoOpenEvent()
    {
        type = FuncType.Empty;
        cameraDisplayObj.SetActive(true);
        //photoDisplayObj.SetActive(true);
        StartCoroutine(OpenCamera());
        cameraButtonObj.SetActive(true);

        yield return new WaitForSeconds(1);
        type = FuncType.Video;
    }

    //照相功能按钮
    public void PhotographButton()
    {
        StopRecording();
        StopAllCoroutines();
        StartCoroutine(PhotoGraphOpenEvent());
    }

    //录像功能按钮
    public void VideoButton()
    {
        StopCamera();
        StopAllCoroutines();
        StartCoroutine(VideoOpenEvent());
    }

    //拍照事件
    public void CameraPhotographEvent()
    {
        try
        {
            SaveImage();
        }
        catch(Exception ex)
        {
            exLog.text = ex.ToString();
        }
    }

    //录像事件
    public void CameraVideoEvent()
    {
        try
        {
            StartRecording();
        }
        catch(Exception ex)
        {
            exLog.text = ex.ToString();
        }
    }

    //停止录像事件
    public void StopCameraVideoEvent()
    {
        try
        {
            StopRecording();
        }
        catch(Exception ex)
        {
            exLog.text = ex.ToString();
        }
    }



    IEnumerator OpenCamera()
    {
        Debug.Log("打开摄像头");
        //等待用户允许访问
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //如果用户允许访问，开始获取图像        
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //先获取设备
            WebCamDevice[] device = WebCamTexture.devices;

            string deviceName = device[0].name;
            //然后获取图像
            webCamTexture = new WebCamTexture(deviceName);
            //将获取的图像赋值
            ma.material.mainTexture = webCamTexture;
            //开始实施获取
            webCamTexture.Play();
        }
    }

    public void SaveImage()
    {
        Texture2D t2d = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, true);
        t2d.SetPixels(webCamTexture.GetPixels());
        t2d.Apply();

        imageTytes = t2d.EncodeToJPG();

        photoDisplay.gameObject.SetActive(true);

        cameraDisplayObj.gameObject.SetActive(true);

        photoDisplay.material.mainTexture = t2d;

    }

    public void CameraPreviewButtonConfirm(GameObject obj)
    {
        obj.SetActive(false);
        File.WriteAllBytes(Application.persistentDataPath + "/my/" + Time.time.ToString().Split('.')[0] + "_" + Time.time.ToString().Split('.')[1] + ".jpg", imageTytes);
        isPreview = false;
    }

    public void CameraPreviewButtonCancel(GameObject obj)
    {
        obj.SetActive(false);
        isPreview = false;
    }

    void StopCamera()
    {
        //等待用户允许访问
        //Application.RequestUserAuthorization(UserAuthorization.WebCam);
        //如果用户允许访问，开始获取图像        
        if (Application.HasUserAuthorization(UserAuthorization.WebCam))
        {
            //先获取设备
            WebCamDevice[] device = WebCamTexture.devices;

            string deviceName = device[0].name;
            //然后获取图像
            //  tex = new WebCamTexture(deviceName);
            //  //将获取的图像赋值
            // ma.material.mainTexture = tex;
            //开始实施获取
            if (webCamTexture)
                webCamTexture.Stop();


        }
    }
    //返回按钮
    public void Back()
    {
        //StopCamera();
        //UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }
}
