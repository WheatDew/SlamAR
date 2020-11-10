using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerGroup : MonoBehaviour
{
    public SCButton PlayVideoButton;
    public MeshRenderer screen;
    public Material videoMaterial, originalMaterial;
    public VideoPlayer videoPlayer;

    public void Start()
    {
        screen.material = videoMaterial;
    }

    public void VideoButton()
    {
        TextMesh buttonText = PlayVideoButton.transform.GetChild(1).GetComponent<TextMesh>();
        if (buttonText.text == "播    放")
        {
            videoPlayer.Play();
            buttonText.text = "停    止";
        }
        else
        {
            videoPlayer.Stop();
            buttonText.text = "播    放";
        }
    }
}
