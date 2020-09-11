using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoGroup : MonoBehaviour
{
    private IClock clock;
    private MP4Recorder recorder;
    private WebCamTexture webCamTexture;
    private Color32[] pixelBuffer;
    private bool recording;

    public MeshRenderer CameraDisplay;
    public GameObject recordDirect;

    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(PhotoGraphOpenEvent());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0)
        || API_InputSystem_Bluetooth.IsBTKeyDown(SC.InputSystem.InputKeyCode.Enter, API_InputSystem_Bluetooth.BTType.Right)
        || API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Enter))
        {
            if (!recording)
            {
                StartRecording();
                recordDirect.SetActive(true);
                recording = true;
            }
            else
            {
                StopRecording();
                recordDirect.SetActive(false);
                recording = false;
            }
        }
    }

    IEnumerator PhotoGraphOpenEvent()
    {
        CameraDisplay.gameObject.SetActive(true);
        StartCoroutine(OpenCamera());
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
            //exLog.text = $"Saved recording to: {path}";
            //var prefix = Application.platform == RuntimePlatform.IPhonePlayer ? "file://" : "";
            //Handheld.PlayFullScreenMovie($"{prefix}{path}");
            Handheld.PlayFullScreenMovie(Application.persistentDataPath + "/MulPhotoes/");
        }
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
