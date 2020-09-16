using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dpoch.SocketIO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Networking;

public class SocketIOModule : MonoBehaviour
{
    SocketIO socket;
    //public TestHome videoTelephonyController;
    //public PDFReader pDFReader;

    private string url;
    private string InputIP;
    string username = "empty", password = "empty";

    private void StartSocket()
    {
        url = "ws://" + InputIP + ":8080/socket.io/?EIO=4&transport=websocket";
        socket = new SocketIO(url);

        socket.OnOpen += () => Debug.Log("Socket open!");
        socket.OnConnectFailed += () => Debug.Log("Socket failed to connect!");
        socket.OnClose += () => Debug.Log("Socket closed!");
        socket.OnError += (err) => Debug.Log("Socket Error: " + err);
        socket.OnOpen += () => {
            socket.Emit("join");
            Debug.Log("Sent client handshake");
        };

        socket.On("server-handshake", (ev) => {
            Debug.Log("Received server handshake");
        });

        //var myData = new MyCustomJsonClass()
        //{
        //    myMember = "This member will get serialized"
        //};

        socket.On("message", (ev) => {
            string myString = ev.Data[0].ToObject<string>();
            Debug.Log(myString);
        });

        //socket.On("call", (ev) => {
        //    //foreach(var item in testSystemGroup.hideList)
        //    //{
        //    //    item.SetActive(false);
        //    //}
        //    StartVideoTelephony();
        //    //socket.Emit("reply", "successful");
        //});

        //socket.On("file", (ev) => {
        //    string myString = ev.Data[0].ToObject<string>();
        //    string[] temp = myString.Split('.');
        //    switch (temp.Last())
        //    {
        //        case "jpg":
        //        case "png":
        //            pDFReader.StartPrintImage(myString);
        //            break;
        //        case "pdf":
        //            pDFReader.StartPrintPDF(string.Format("http://{0}:8080/file/download/", surl),myString);
        //            break;
        //    }
        //});

        socket.Connect();
    }

    public IEnumerator SendHttpRequest(string RequestURL,WWWForm formdata)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Post(RequestURL, formdata))
        {
            yield return webRequest.SendWebRequest();
            if (webRequest.isNetworkError)
            {
                Debug.LogError(webRequest.error);
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    socket.Emit("message", "{\"name\":\"aaa\"}");

        //}
    }

    //public void EmitMessage()
    //{
    //    socket.Emit("message", "{\"name\":\"aaa\"}");
    //    Debug.Log("发送消息");
    //}

    //public void StartVideoTelephony()
    //{
    //    videoTelephonyController.onJoinButtonClicked();
    //}

    public void SetInputIP(string inputIP)
    {
        InputIP = inputIP;
    }

    public void SetStart()
    {
        StartSocket();
    }

    public void SendLoginRequest(string RequestURL,string username,string password)
    {
        WWWForm formdata = new WWWForm();
        formdata.AddField("username", username);
        formdata.AddField("password", password);

        StartCoroutine(SendHttpRequest(RequestURL,formdata));
    }
}
