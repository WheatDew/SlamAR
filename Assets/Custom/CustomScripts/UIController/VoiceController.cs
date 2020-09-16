using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

public class VoiceController : MonoBehaviour
{
    public TextMesh text;
    //public InputField inpufile
    // Start is called before the first frame update
    AndroidJavaClass javaClass;
    void Start()
    {
        javaClass = new AndroidJavaClass("com.example.myapplication.MainActivity");
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
        }
        javaClass.CallStatic("初始化讯飞", new object[] { "5efbf083", name });//初始化讯飞,5c81de59为appid

        // javaClass.CallStatic("设置语音识别参数", new object[] { "language", "zh_cn" });//设置语音识别为中文
    }


    //public void 开始语音评测()
    //{
    //    javaClass.CallStatic("开始语音评测", new object[] { inpufile.text });
    //}
    //public void 停止语音评测()
    //{
    //    javaClass.CallStatic("停止语音评测");
    //}
    //public void 开始语音合成()
    //{
    //    javaClass.CallStatic("开始语音合成", new object[] { inpufile.text });
    //}

    public void 开始语音识别()
    {
        text.text = "";
        javaClass.CallStatic("开始语音识别");
    }
    public void 停止语音识别()
    {
        javaClass.CallStatic("停止语音识别");
    }
    public void 清空文字()
    {
        text.text = "";
    }

    public void 语音识别结果(string result)
    {
        text.text = result.Substring(0,result.Length-1);
    }

    //public void 语音评测结果(string result)
    //{
    //    text.text += "-语音评测结果:\n" + result;
    //    Debug.Log("-语音评测结果:\n" + result);
    //}

    //public void onEndOfSpeech(string s)//用户结束说话回调
    //{
    //    //text.text += "-用户结束说话:" + s;
    //}

    //public void onCompleted(string s)//语音合成结束说话回调
    //{

    //    text.text += "-语音合成结束说话:" + s;
    //}
    //public void onSpeakBegin(string s)//开始播放语音合成回调
    //{

    //    text.text += "-开始播放语音合成:" + s;
    //}
}

