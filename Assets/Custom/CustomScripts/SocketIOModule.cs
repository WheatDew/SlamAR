using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Dpoch.SocketIO;
using System.Linq;
using System.Threading.Tasks;

public class SocketIOModule : MonoBehaviour
{
    SocketIO socket;
    public TestHome videoTelephonyController;
    public PDFReader pDFReader;

    public TextMesh textMesh;
    private string url;
    private string surl;

    public void StartSocket()
    {
        url = "ws://" + textMesh.text + ":8080/socket.io/?EIO=4&transport=websocket";
        surl = textMesh.text;
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

        socket.On("call", (ev) => {
            //foreach(var item in testSystemGroup.hideList)
            //{
            //    item.SetActive(false);
            //}
            StartVideoTelephony();
            //socket.Emit("reply", "successful");
        });

        socket.On("file", (ev) => {
            string myString = ev.Data[0].ToObject<string>();
            string[] temp = myString.Split('.');
            switch (temp.Last())
            {
                case "jpg":
                case "png":
                    pDFReader.StartPrintImage(myString);
                    break;
                case "pdf":
                    pDFReader.StartPrintPDF(string.Format("http://{0}:8080/file/download/", surl),myString);
                    break;
            }
        });

        socket.Connect();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    socket.Emit("message", "{\"name\":\"aaa\"}");

        //}
    }

    public void EmitMessage()
    {
        socket.Emit("message", "{\"name\":\"aaa\"}");
        Debug.Log("发送消息");
    }

    public void StartVideoTelephony()
    {
        videoTelephonyController.onJoinButtonClicked();
    }
}
