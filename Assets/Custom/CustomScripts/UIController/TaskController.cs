using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Video;

public class TaskController : MonoBehaviour
{
    public List<List<TaskInfo>> taskList = new List<List<TaskInfo>>();
    public TextMesh currentStep, currentStepDescribe1, currentStepDescribe2, currentStepDescribe3, currentStepDescribe4, currentStepDescribe5;
    //public GameObject Tick1, Tick2, Tick3, Tick4, Tick5;
    public SCButton NextStepButton;
    public MeshRenderer NextStepButtonMeshRenderer;
    public Material SubmitMaterial;
    public UnityAction currentCommand;
    public int currentStepIndex=-1;
    public TextMesh exLog;

    private string commandString;
    private int currentTaskDetailedStepIndex = -1;
    private DocumentGroup documentGroup;
    private VideoPlayerGroup videoPlayerGroup;
    private CameraGroup cameraGroup;
    private VideoGroup videoGroup;
    private InputInfoGroup inputInfoGroup;
    private void Start()
    {
        //测试
        List<TaskInfo> step1 = new List<TaskInfo>();
        step1.Add(new TaskInfo("观看视频"));
        step1.Add(new TaskInfo("播放视频00000001","观看操作流程视频"));
        step1.Add(new TaskInfo("了解设备结构与原理"));

        List<TaskInfo> step2 = new List<TaskInfo>();
        step2.Add(new TaskInfo("查看文档"));
        step2.Add(new TaskInfo("查看文档","查看设备参数文档"));
        step2.Add(new TaskInfo("熟悉规范操作事项"));

        List<TaskInfo> step3 = new List<TaskInfo>();
        step3.Add(new TaskInfo("拍照"));
        step3.Add(new TaskInfo("拍摄照片","对检修设备进行拍照"));
        step3.Add(new TaskInfo("对故障处进行拍照"));
        List<TaskInfo> step4 = new List<TaskInfo>();
        step4.Add(new TaskInfo("录像"));
        step4.Add(new TaskInfo("录制影像","对设备进行翻转录像"));
        step4.Add(new TaskInfo("对检修过程进行录像"));
        List<TaskInfo> step5 = new List<TaskInfo>();
        step5.Add(new TaskInfo("录入信息"));
        step5.Add(new TaskInfo("提交任务","录入设备信息"));
        step5.Add(new TaskInfo("录入信息","录入故障信息"));
        step5.Add(new TaskInfo("录入检修结果"));
        taskList.Add(step1);
        taskList.Add(step2);
        taskList.Add(step3);
        taskList.Add(step4);
        taskList.Add(step5);
        NextStep();
    }

    private void Update()
    {
        currentCommand();
    }


    public void NextStep()
    {
        UIController uIController = FindObjectOfType<UIController>();
        currentCommand = delegate { };
        NextStepButton.gameObject.SetActive(false);
        currentStepIndex++;
        if (currentStepIndex >= taskList.Count)
        {
            uIController.ClearConflictedGroup();
            currentStep.text = "";
            currentStepDescribe1.text = "";
            currentStepDescribe2.text = "";
            currentStepDescribe3.text = "";
            currentStepDescribe4.text = "";
            currentStepDescribe5.text = "";
            return;
        }


        if (taskList[currentStepIndex].Count > 0)
            currentStep.text = taskList[currentStepIndex][0].Describe;
        if (taskList[currentStepIndex].Count > 1)
        {
            currentTaskDetailedStepIndex = 1;
            currentStepDescribe1.text = taskList[currentStepIndex][1].Describe;
            SwitchTask(ref currentCommand, taskList[currentStepIndex][1]);
        }
        if (taskList[currentStepIndex].Count > 2)
            currentStepDescribe2.text = taskList[currentStepIndex][2].Describe;
        if (taskList[currentStepIndex].Count > 3)
            currentStepDescribe3.text = taskList[currentStepIndex][3].Describe;
        if (taskList[currentStepIndex].Count > 4)
            currentStepDescribe4.text = taskList[currentStepIndex][4].Describe;
        if (taskList[currentStepIndex].Count > 5)
            currentStepDescribe5.text = taskList[currentStepIndex][5].Describe;
    }

    public void NextTask()
    {
        
        currentTaskDetailedStepIndex++;
        if (currentTaskDetailedStepIndex >= taskList[currentStepIndex].Count)
        {
            currentCommand = delegate { };
            NextStepButton.gameObject.SetActive(true);
            return;
        }
        SwitchTask(ref currentCommand, taskList[currentStepIndex][currentTaskDetailedStepIndex]);

    }

    private void SwitchTask(ref UnityAction currentCommand,TaskInfo taskInfo)
    {
        switch (taskInfo.Command.Substring(0, 4))
        {
            case "播放视频":
                PlayVideo();
                currentCommand = delegate
                {
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                Debug.Log("播放视频" + taskInfo.Command.Substring(4, 8));
                break;
            case "查看文档":
                DisplayDecument();
                currentCommand = delegate
                {
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                break;
            case "拍摄照片":
                Photograph();
                currentCommand = delegate
                {
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                break;
            case "录制影像":
                Recording();
                currentCommand = delegate
                {
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                break;
            case "录入信息":
                InputInfo();
                currentCommand = delegate
                {
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                break;
            case "提交任务":
                NextStepButtonMeshRenderer.material = SubmitMaterial;
                currentCommand = delegate {
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                break;
            case "空白指令":
                currentCommand = delegate { 
                    taskList[currentStepIndex][currentTaskDetailedStepIndex].IsCompleted = true;
                    NextTask();
                };
                break;
        }
    }

    public void PlayVideo()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateVideoPlayerGroup();
        videoPlayerGroup = uIController.GetVideoPlayerGroup();
    }

    public void DisplayDecument()
    {
        UIController uIController = FindObjectOfType<UIController>();
        //if (exLog == null)
        //    Debug.LogError("空");
        uIController.CreateDocumentGroup("ckk.pdf",exLog);
        documentGroup = uIController.GetDocumentGroup();
    }

    public void Photograph()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateCameraGroup(exLog);
        Debug.Log("创建拍摄照片页面");
        cameraGroup = uIController.GetCameraGroup();
    }

    public void Recording()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateVideoGroup(exLog);
        videoGroup = uIController.GetVideoGroup();
    }

    public void InputInfo()
    {
        UIController uIController = FindObjectOfType<UIController>();
        uIController.CreateInputInfoGroup();
        inputInfoGroup = uIController.GetInputInfoGroup();
    }
}

public class TaskInfo
{
    public string Command;
    public string Describe;
    public bool IsCompleted;
    public TaskInfo(string describe)
    {
        Command = "空白指令";
        Describe = describe;
        IsCompleted=false;
    }

    public TaskInfo(string command,string describe)
    {
        Command = command;
        Describe = describe;
        IsCompleted = false;
    }
}
