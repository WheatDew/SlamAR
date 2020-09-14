using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CameraGroup : MonoBehaviour
{
    public MeshRenderer CameraDisplay;
    public MeshRenderer PhotoPreview;
    public TextMesh exLog;

    private WebCamTexture webCamTexture;
    private byte[] ImageBytes;
    private bool isPreview = true;
    private Texture2D t2d;

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine("PhotoGraphOpenEvent");
        Invoke("ResetPreview", 0.5f);
    }

    private void Update()
    {
        if(!isPreview && API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Enter))
        {
            CameraPhotographEvent();
            isPreview = true;
            Debug.Log("照片浏览模式");
        }
    }

    //开启照相功能事件
    IEnumerator PhotoGraphOpenEvent()
    {
        CameraDisplay.gameObject.SetActive(true);
        StartCoroutine("OpenCamera");
        yield return new WaitForSeconds(1);
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
            CameraDisplay.material.mainTexture = webCamTexture;
            //开始实施获取
            webCamTexture.Play();
        }
    }

    public void SaveImage()
    {
        webCamTexture.Play();
        t2d = new Texture2D(webCamTexture.width, webCamTexture.height, TextureFormat.ARGB32, true);
        t2d.SetPixels(webCamTexture.GetPixels());
        t2d.Apply();
        ImageBytes = t2d.EncodeToJPG();
        PhotoPreview.gameObject.SetActive(true);

        PhotoPreview.material.mainTexture = t2d;

    }

    public void CameraPreviewButtonConfirm(GameObject obj)
    {
        obj.SetActive(false);
        File.WriteAllBytes(Application.persistentDataPath + "/my/" + Time.time.ToString().Split('.')[0] + "_" + Time.time.ToString().Split('.')[1] + ".jpg", ImageBytes);
        Invoke("ResetPreview", 1f);
    }

    public void CameraPreviewButtonCancel(GameObject obj)
    {
        obj.SetActive(false);
        Invoke("ResetPreview", 1f);
    }

    public void ResetPreview()
    {
        isPreview = false;
    }

    public void CameraPhotographEvent()
    {
        try
        {
            SaveImage();
        }
        catch (Exception ex)
        {
            exLog.text = ex.ToString();
        }
    }

    private void OnDestroy()
    {
        webCamTexture.Stop();
        Destroy(webCamTexture);
        Destroy(t2d);
        StopAllCoroutines();
    }
}
