using NatSuite.Recorders;
using NatSuite.Recorders.Clocks;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoGroup : MonoBehaviour
{

    private WebCamTexture webCamTexture;
    private MP4Recorder recorder;
    private IClock clock;
    private bool recording;
    private Color32[] pixelBuffer;
    public TextMesh exLog;
    private bool initialized;

    public GameObject recordDirect;
    public MeshRenderer CameraDisplay;


    #region --Recording State--

    public void StartRecording()
    {
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
        var path = await recorder.FinishWriting();
        // Playback recording
        Debug.Log($"Saved recording to: {path}");
        //var prefix = Application.platform == RuntimePlatform.IPhonePlayer ? "file://" : "";
        //Handheld.PlayFullScreenMovie($"{prefix}{path}");
        Handheld.PlayFullScreenMovie(Application.persistentDataPath + "/MulPhotoes/");
    }
    #endregion


    #region --Operations--

    IEnumerator Start()
    {
        // Request camera permission
        yield return Application.RequestUserAuthorization(UserAuthorization.WebCam);
        if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
            yield break;
        // Start the WebCamTexture
        webCamTexture = new WebCamTexture(1280, 720, 30);
        CameraDisplay.material.mainTexture = webCamTexture;

        webCamTexture.Play();
        // Display webcam
        yield return new WaitUntil(() => webCamTexture.width != 16 && webCamTexture.height != 16); // Workaround for weird bug on macOS

        initialized = true;
    }

    void Update()
    {
        if (recording && webCamTexture.didUpdateThisFrame)
        {
            webCamTexture.GetPixels32(pixelBuffer);
            recorder.CommitFrame(pixelBuffer, clock.timestamp);
        }

        if (initialized && API_InputSystem_Head.IsHeadKeyDown(SC.InputSystem.InputKeyCode.Enter))
        {
            if (recordDirect.activeSelf)
            {
                StopRecording();
                recordDirect.SetActive(false);
            }
            else
            {
                StartRecording();
                recordDirect.SetActive(true);
            }
        }
    }
    #endregion

    private void OnDestroy()
    {
        webCamTexture.Stop();
        Destroy(webCamTexture);
        StopAllCoroutines();
    }
}
