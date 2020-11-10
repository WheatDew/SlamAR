using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dpoch.SocketIO;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine.Networking;
using LitJson;
using Newtonsoft.Json.Linq;
using agora_gaming_rtc;
using UnityEngine.Android;

public class SocketIOModule : MonoBehaviour
{
    public TextMesh log;
    SocketIO socket;
    //public TestHome videoTelephonyController;
    //public PDFReader pDFReader;

    private string url;
    private string InputIP;
    string username = "empty", password = "empty";
    public string token="";
    public ExpertListController expertListController;
    public TestHome videoTelephonyController;

    public IRtcEngine mmRtcEngine;
    public GameObject go;
    private IRtcEngine mRtcEngine;


    public void StartSocket()
    {
#if UNITY_EDITOR
        url = string.Format("ws://{0}:8080/socket.io/?EIO=4&transport=websocket&token={1}&model=testmodel&serial=testserial"
            , InputIP, token);
        Debug.Log(url);
#elif UNITY_ANDROID
        url = string.Format("ws://{0}:8080/socket.io/?EIO=4&transport=websocket&token={1}&model={2}&serial={3}"
            ,InputIP,token,DeviceInfo.MODEL,DeviceInfo.SN);
#endif

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

        socket.On("message", (ev) => {
            string myString = ev.Data[0].ToObject<string>();
            Debug.Log(myString);
        });

        socket.On("call", (ev) =>
        {
            bool test = false;
            Debug.Log("Call Event"+ev.Data.Count+ev.IsAcknowledgable);
            
            //log.text = ev.Data[0].ToObject<string>();
            foreach(var item in ev.Data)
            {
                Debug.Log(item);
            }
            ev.Acknowledge(test);
        });



        socket.On("onlinePros", (ev) => {

            UIController uIController = FindObjectOfType<UIController>();
            expertListController.experts.Clear();
            foreach (var item in ev.Data[0])
            {
                expertListController.experts.Add(
                    new Expert
                    {
                        key = item["key"].ToString(),
                        name = item["value"]["username"].ToString(),
                        status = item["value"]["busy"].ToString()=="True"?"忙碌中":""
                    });
            }
            
            if (uIController.GetExpertListGroup() != null)
            {
                foreach(var item in uIController.GetExpertListGroup().expertItems)
                {
                    item.gameObject.SetActive(false);
                }
                for (int i = 0; i < expertListController.experts.Count; i++)
                {
                    uIController.GetExpertListGroup().expertItems[i].StatusText.text = expertListController.experts[i].status;
                    uIController.GetExpertListGroup().expertItems[i].gameObject.SetActive(true);
                }
            }
        });



        socket.On("file", (ev) =>
        {
            string myString = ev.Data[0].ToObject<string>();
            string[] temp = myString.Split('.');
            UIController uIController = FindObjectOfType<UIController>();


            //switch (temp.Last())
            //{
            //    case "jpg":
            //    case "png":
            //        PDFReader pDFReader = uIController.CreateCameraGroup(log)
            //        pDFReader.StartPrintImage(myString);
            //        break;
            //    case "pdf":
            //        pDFReader.StartPrintPDF(string.Format("http://{0}:8080/file/download/", surl), myString);
            //        break;
            //}
        });

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
                if (webRequest.responseCode == 200)
                token= webRequest.downloadHandler.text;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    StartVideoTelephony();
        //    UIController uIController = FindObjectOfType<UIController>();
        //    //uIController.GetDocumentGroup().pdfReader.()
        //}
    }

    //public void EmitMessage()
    //{
    //    socket.Emit("message", "{\"name\":\"aaa\"}");
    //    Debug.Log("发送消息");
    //}

    public void StartVideoTelephony()
    {
        socket.Emit("call", (ev) =>
        {
            Debug.Log(ev[0].ToString());
        }, expertListController.experts[0].key);
        videoTelephonyController.onJoinButtonClicked();
    }

    public void SetInputIP(string inputIP)
    {
        InputIP = inputIP;
    }


    public void SendLoginRequest(string RequestURL,string username,string password)
    {
        WWWForm formdata = new WWWForm();
        formdata.AddField("username", username);
        formdata.AddField("password", password);

        StartCoroutine(SendHttpRequest(RequestURL,formdata));
    }

    public void TestSend()
    {
        SendLoginRequest("http://192.168.1.118:8080/auth/glass/login", "glass", "123456");
    }

    public void EndCall()
    {
        socket.Emit("endCall", expertListController.experts[0].key);
    }

    private void OnEnable()
    {
        EndCall();
    }
}
