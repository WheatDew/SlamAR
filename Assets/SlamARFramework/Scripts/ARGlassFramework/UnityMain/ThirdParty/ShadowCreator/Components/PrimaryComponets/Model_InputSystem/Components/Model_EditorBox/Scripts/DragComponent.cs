using SC.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using static SC.InputSystem.InputDeviceEventBase;

public abstract class DragComponent : MonoBehaviour, ISCPointerDragHandler, ISCPointerEnterHandler, ISCPointerExitHandler, ISCPointerDownHandler,ISCPointerUpHandler, ISCBothHandPointerDownHandler, ISCBothHandPointerDragHandler, ISCBothHandPointerUpHandler
{
    //空物体父体
    [HideInInspector]
    public  GameObject parentGame;
    //空物体子体
    [HideInInspector]
    public  GameObject childGame;
    //scale的模型集
    [HideInInspector]
    public  List<GameObject> scaleGameList;
    //euler的模型集
    [HideInInspector]
    public  List<GameObject> eulerGameList;
    [HideInInspector]
    public  GameObject editGame;

    [HideInInspector]
    public BoxCollider box;

    [HideInInspector]
    public bool isBoth;

    private float initDis;

    float xishu = 1.1f;

    [HideInInspector]
    public Vector3 initPos;


    [HideInInspector]
    public Vector3 scale3;

    [HideInInspector]
    public Vector3 Tscale3;

    Vector3 dragInitLocalPosition = Vector3.zero;
    [HideInInspector]
    public LineRenderer lr;
    private Material mat;
    [HideInInspector]
    public float totalx;
    [HideInInspector]
    public float totaly;
    [HideInInspector]
    public float totalz;
    [HideInInspector]
    public Vector3[] veces;
    [HideInInspector]
    public int testIndex = 0;
    [HideInInspector]
    public bool isFangda;
    [HideInInspector]
    public Vector3 sScale;
    [HideInInspector]
    public Vector3 rScale;
    [HideInInspector]
    public Vector3 rotEurl;
    Vector3[] v3;

    [HideInInspector]
    public Material boxMaterial;
    [HideInInspector]
    public Material boxClickMaterial;
    private GameObject boxDisplay;
    public bool isVertexScale;

    [HideInInspector]
    public DragManager dm;

    [HideInInspector]
    public GameObject eulerGame;
    [HideInInspector]
    public GameObject scaleGame;
    [HideInInspector]
    public Material LineMaterial;
    public float LineSize = 0.005f;

    // 定义枚举
    [Serializable]
    public enum EditTypeEnum
    {
        [EnumAttirbute("无边框拖拽")]
        DEFECT = 0,
        [EnumAttirbute("有边框拖拽")]
        NORMAL = 1,
        [EnumAttirbute("旋转放大拖拽")]
        ALLEDIT = 2
    }
    // 定义枚举属性
    [EnumAttirbute("编辑类型")]
    public EditTypeEnum editType;
    // 定义枚举
    [Serializable]
    public enum EditManagerEnum
    {
        [EnumAttirbute("默认框")]
        Default = 0,
        [EnumAttirbute("自定义框")]
        Custom = 1
    }
    // 定义枚举属性
    [EnumAttirbute("材质类型")]
    public EditManagerEnum editManagerType;

    private bool initRib;
    private Dictionary<string, bool> partEnterList;
    private Dictionary<string, bool> partDownList;
    public virtual void Start()
    {
        partEnterList = new Dictionary<string, bool>();
        partDownList = new Dictionary<string, bool>();
        if (editManagerType == EditManagerEnum.Default)
        {
            dm = ((GameObject)Resources.Load("ShadowEidtBox")).GetComponent<DragManager>();
        }

        parentGame = new GameObject();
        parentGame.name = "parentGame_"+this.name;
        parentGame.transform.parent = this.transform.parent;
        parentGame.transform.localEulerAngles = new Vector3(0,0,0);
        childGame = new GameObject();
        childGame.name = "childGame";
        childGame.transform.parent = parentGame.transform;
        childGame.transform.localEulerAngles = new Vector3(0, 0, 0);
        editGame = new GameObject();
        editGame.name = "editGame";
        editGame.transform.parent = parentGame.transform;
        editGame.transform.localEulerAngles = new Vector3(0, 0, 0);
        scaleGameList = new List<GameObject>();
        eulerGameList = new List<GameObject>();
        boxMaterial = dm.boxMaterial;
        boxClickMaterial = dm.boxClickMaterial;
        box = this.GetComponent<BoxCollider>();
        lr = this.gameObject.AddComponent<LineRenderer>();
        LineMaterial = dm.LineMaterial;
        if (LineMaterial == null)
        {
            lr.material = new Material(Shader.Find("Standard"));
        }
        else
        {
            lr.material = LineMaterial;
        }
        lr.widthMultiplier = LineSize;
        scale3 = this.transform.localScale;
        Tscale3 = scale3;
        veces = GetBoxColliderVertexPositions(box);
        lr.positionCount =16;
        eulerGame = dm.eulerGame;
        scaleGame = dm.scaleGame;
        if (scaleGame != null)
        {
            createSaleGame();
        }
        if (eulerGame != null)
        {
            createEulerGame();
        }

       // AddBoxDisplay();
        InitEulerManager();
        InitScaleManager();

        HideBoxEdit();
        lr.enabled = false;
    }

    public void OnSCPointerEnter(InputDevicePartBase part, SCPointEventData eventData)
    {
        changeEnterPart(part.name,true);
        ShowBoxEdit();
    }

    public void OnSCPointerExit(InputDevicePartBase part, SCPointEventData eventData)
    {
        if (eventData.hitOtherPointer != null)
        {
            if (eventData.hitOtherPointer.transform.parent != null && eventData.hitOtherPointer.transform.parent.name == editGame.name)
            {
                return;
            }
        }
        changeEnterPart(part.name, false);
        if (isEnterChoose())
        {
            return;
        }
        HideBoxEdit();
    }

    public bool isEnterChoose()
    {
        foreach (KeyValuePair<string, bool> kv in partEnterList)
        {
            if (kv.Value)
            {
                return true;
            }
        }
        return false;
    }

    public void changeEnterPart(string name, bool choose)
    {
        if (!partEnterList.ContainsKey(name))
        {
            partEnterList.Add(name, choose);
        }
        else
        {
            partEnterList[name] = choose;
        }

    }

    public bool isDownChoose()
    {
        foreach (KeyValuePair<string, bool> kv in partDownList)
        {
            if (kv.Value)
            {
                return true;
            }
        }
        return false;
    }

    public void changeDownPart(string name, bool choose)
    {
        if (!partDownList.ContainsKey(name))
        {
            partDownList.Add(name, choose);
        }
        else
        {
            partDownList[name] = choose;
        }

    }

    public virtual void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData)
    {
        try
        {
            changeDownPart(part.name,true);
            //dragInitLocalPosition = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.transform.InverseTransformPoint(transform.position);
            //doTweeningPos = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.transform.TransformPoint(dragInitLocalPosition);
            Rigidbody rb = this.GetComponent<Rigidbody>();
            if (rb!=null)
            {
                initRib = rb.isKinematic;
                rb.isKinematic = true;
            }
            switch (editType)
            {
                case EditTypeEnum.ALLEDIT:
                    lr.enabled = true;
                    //  boxDisplay.SetActive(true);
                    break;
                case EditTypeEnum.DEFECT:
                 //   boxDisplay.SetActive(false);
                    break;
                case EditTypeEnum.NORMAL:
                    lr.enabled = true;
                    //   boxDisplay.SetActive(true); ;
                    break;
            }
           // ApplyMaterialToAllRenderers(boxDisplay, boxClickMaterial);
            HideBoxEdit();
            isMove = true;
            //     InputDeviceEventBase.anyKeyUpDelegate += onUp;
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    public virtual void OnSCPointerUp(InputDevicePartBase part, SCPointEventData eventData)
    {

        changeDownPart(part.name, false);

        if(isDownChoose())
        {
            return;
        }

        Rigidbody rb = this.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = initRib;
        }
        ShowBoxEdit();
        lr.enabled = false;
        //     InputDeviceEventBase.anyKeyUpDelegate -= onUp;
        isMove = false;
    }

    public virtual void OnSCPointerDrag(InputDevicePartBase part, SCPointEventData eventData)
    {

        if (!isBoth&& isMove)
        transform.position = eventData.dragSlowPosition;
    }

    private void AddBoxDisplay()
    {
        if (boxMaterial != null)
        {

            boxDisplay = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(boxDisplay.GetComponent<Collider>());
            boxDisplay.SetActive(false);
            boxDisplay.name = "bounding box";

            ApplyMaterialToAllRenderers(boxDisplay, boxMaterial);
            boxDisplay.transform.parent = this.transform;
            boxDisplay.transform.localScale = box.size * xishu;
            boxDisplay.transform.localPosition = new Vector3(0,0,0);
        }
    }

    private static void ApplyMaterialToAllRenderers(GameObject root, Material material)
    {
        if (material != null)
        {
            Renderer[] renderers = root.GetComponentsInChildren<Renderer>();

            for (int i = 0; i < renderers.Length; ++i)
            {
                renderers[i].material = material;
            }
        }
    }

    public virtual void ShowBoxEdit()
    {
      //  ApplyMaterialToAllRenderers(boxDisplay, boxMaterial);
        switch (editType)
        {
            case EditTypeEnum.ALLEDIT:
                editGame.SetActive(true);
              //  boxDisplay.SetActive(true);
              //  lr.enabled = true;
                changeLine();
                break;
            case EditTypeEnum.DEFECT:
                editGame.SetActive(false);
            //    boxDisplay.SetActive(false);
            //    lr.enabled = false;
                changeLine();
                break;
            case EditTypeEnum.NORMAL:
                editGame.SetActive(false);
             //   boxDisplay.SetActive(true);
             //   lr.enabled = true;
                changeLine();
                break;
        }
    }

    public virtual void HideBoxEdit()
    {
        editGame.SetActive(false);
       // lr.enabled = false;
    }

    public virtual void InitEulerManager()
    {
        if(eulerGameList.Count>11)
        {

            for (int i = 0; i < 12; i++)
            {
                if (i == 1 || i == 3 || i == 5 || i == 10)
                {
                    eulerGameList[i].transform.localEulerAngles = new Vector3(0, 0, 90);
                }
                if (i == 0 || i == 2 || i == 6 || i == 7)
                {
                    eulerGameList[i].transform.localEulerAngles = new Vector3(90, 0, 0);
                }
            }
        }

    }

    public virtual void InitScaleManager()
    {
        if(scaleGameList.Count>7)
        {
            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(0, 90, 0);
                        break;
                    case 1:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(0, 180, 0);
                        break;
                    case 2:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(0, 0, 0);
                        break;
                    case 3:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(-90, 0, 0);
                        break;
                    case 4:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(90, 90, 0);
                        break;
                    case 5:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(90, 90, -90);
                        break;
                    case 6:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(90, 0, 0);
                        break;
                    case 7:
                        scaleGameList[i].transform.localEulerAngles = new Vector3(90, -90, 0);
                        break;
                }
            }
        }
    }

    private void createEulerGame()
    {
        GameObject go;
        RotManager rm;
        for (int i = 0; i <12; i++)
        {
            go = GameObject.Instantiate(eulerGame);
            go.SetActive(true);
            rm = go.AddComponent<RotManager>();
            rm.index = i;
            rm.dc = this;
         //   rm.model.transform.localEulerAngles = new Vector3() ;
            go.transform.parent = editGame.transform;
            rScale = go.transform.localScale;
            eulerGameList.Add(go);
        }
    }
    private Vector3 scInputPos;

    void createSaleGame()
    {
        GameObject go;
        for (int i = 0; i < veces.Length; i++)
        {
            go = GameObject.Instantiate(scaleGame);
            go.SetActive(true);
            go.AddComponent<ScaleManager>();
            go.GetComponent<ScaleManager>().index = i;
            go.GetComponent<ScaleManager>().dc = this;
        //    rm.model.transform.localEulerAngles = new Vector3();
            go.transform.parent = editGame.transform;

            sScale = go.transform.localScale;
            scaleGameList.Add(go);
        }
    }
    

    public void OnDrag( PointerEventData eventData ) {
        try
        {
        } catch(Exception e) {

        }
    }

    private void moveTW()
    {
   //     doTweeningPos = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.transform.TransformPoint(dragInitLocalPosition);
        tw = this.transform.DOMove(doTweeningPos, 0.2f).OnComplete(moveTW);

    }
    private Vector3 doTweeningPos;
    public bool isMove;
    Tweener tw;
    public virtual void OnPointerDown( PointerEventData eventData ) {


    }

    public virtual void onUp(InputKeyCode keyCode)
    {
      //  ApplyMaterialToAllRenderers(boxDisplay, boxMaterial);
        ShowBoxEdit();
        tw.Kill(false);
        //     InputDeviceEventBase.anyKeyUpDelegate -= onUp;
        isMove = false;
    }

    public void OnPointerUp( PointerEventData eventData ) {

    }
    public virtual void Update()
    {
        if (isMove)
        {
      //      doTweeningPos = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.transform.TransformPoint(dragInitLocalPosition);
      //      Debug.Log(Vector3.Lerp(doTweeningPos, this.transform.position,Time.deltaTime*10));
           // this.transform.position = InputSystem.InputDeviceCurrent.inputDeviceUIBase.model.transform.TransformPoint(dragInitLocalPosition);

        }
        if (isVertexScale)
        {
            changePos();
        }
    }

    private void changePos()
    {
        if(isFangda)
        {
            Vector3 position = -new Vector3((1) * (totalx * 0.5f - totalx * (box.transform.localScale.x) * 0.5f), (1) * (totaly * 0.5f - totaly * (box.transform.localScale.y) * 0.5f), (1) * (totalz * 0.5f - totalz * (box.transform.localScale.z) * 0.5f)) - box.center * box.transform.localScale.x; // left justified;
           
            this.transform.localPosition = initPos+ position + changeOfferSize() + changeOfferPos();

        }
    }

    public void changeLine()
    {
        veces = GetBoxColliderVertexPositions(box, xishu);
        lr.SetPosition(0, veces[0]);
        lr.SetPosition(1, veces[1]);
        lr.SetPosition(2, veces[3]);
        lr.SetPosition(3, veces[2]);
        lr.SetPosition(4, veces[0]);

        lr.SetPosition(5, veces[4]);
        lr.SetPosition(6, veces[6]);
        lr.SetPosition(7, veces[7]);
        lr.SetPosition(8, veces[5]);
        lr.SetPosition(9, veces[4]);
        lr.SetPosition(10, veces[6]);
        lr.SetPosition(11, veces[2]);
        lr.SetPosition(12, veces[3]);
        lr.SetPosition(13, veces[7]);
        lr.SetPosition(14, veces[5]);
        lr.SetPosition(15, veces[1]);
        for (int i = 0; i < veces.Length; i++)
        {
            if (scaleGameList.Count > i)
            {
                scaleGameList[i].transform.position = veces[i];
            }
        }
        eulerGameList[0].transform.position = (veces[0] + veces[1]) / 2;
        eulerGameList[1].transform.position = (veces[1] + veces[3]) / 2;
        eulerGameList[2].transform.position = (veces[3] + veces[2]) / 2;
        eulerGameList[3].transform.position = (veces[2] + veces[0]) / 2;

        eulerGameList[4].transform.position = (veces[0] + veces[4]) / 2;
        eulerGameList[5].transform.position = (veces[4] + veces[6]) / 2;
        eulerGameList[6].transform.position = (veces[6] + veces[7]) / 2;
        eulerGameList[7].transform.position = (veces[5] + veces[4]) / 2;

        eulerGameList[8].transform.position = (veces[6] + veces[2]) / 2;
        eulerGameList[9].transform.position = (veces[3] + veces[7]) / 2;
        eulerGameList[10].transform.position = (veces[5] + veces[7]) / 2;
        eulerGameList[11].transform.position = (veces[5] + veces[1]) / 2;
    }

    public Vector3 changeOfferPos()
    {
        Vector3 vOfferPos = new Vector3(0, 0, 0);
        vOfferPos = new Vector3((box.center.x) * this.scale3.x, (box.center.y) * this.scale3.y, (box.center.z) * this.scale3.z);
        return vOfferPos;
    }

    public Vector3 changeOfferSize()
    {
        Vector3 vOfferSize = new Vector3(0,0,0);
        float offX = box.size.x;
        float offY = box.size.y;
        float offZ = box.size.z;
        switch (testIndex)
        {
            case 0:
                vOfferSize = new Vector3((box.size.x / 2f * (1 - scale3.x)), (box.size.y / 2f * (1 - scale3.y)), (box.size.z / 2f * (1 - scale3.z)));
                break;
            case 1:
                vOfferSize = new Vector3((box.size.x / 2f * (1-scale3.x)), (box.size.y / 2f * (1 - scale3.y)), -(box.size.z / 2f * (1 - scale3.z)));
                break;
            case 2:
                vOfferSize = new Vector3(-(box.size.x / 2f * (1 - scale3.x)), (box.size.y / 2f * (1 - scale3.y)), (box.size.z / 2f * (1 - scale3.z)));
                break;
            case 3:
                vOfferSize = new Vector3(-(box.size.x / 2f * (1 - scale3.x)), (box.size.y / 2f * (1 - scale3.y)), -(box.size.z / 2f * (1 - scale3.z)));
                break;
            case 4:
                vOfferSize = new Vector3((box.size.x / 2f * (1 - scale3.x)), -(box.size.y / 2f * (1 - scale3.y)), (box.size.z / 2f * (1 - scale3.z)));
                break;
            case 5:
                vOfferSize = new Vector3((box.size.x / 2f * (1 - scale3.x)), -(box.size.y / 2f * (1 - scale3.y)), -(box.size.z / 2f * (1 - scale3.z)));
                break;
            case 6:
                vOfferSize = new Vector3(-(box.size.x / 2f * (1 - scale3.x)), -(box.size.y / 2f * (1 - scale3.y)), (box.size.z / 2f * (1 - scale3.z)));
                break;
            case 7:
                vOfferSize = new Vector3(-(box.size.x / 2f * (1 - scale3.x)), -(box.size.y / 2f * (1 - scale3.y)), -(box.size.z / 2f * (1 - scale3.z)));
                break;
        }
        return vOfferSize;
    }

    Vector3[] GetBoxColliderVertexPositions(BoxCollider boxcollider,float xishu=1)
    {
        var vertices = new Vector3[8];
        //上面4个点  
        vertices[0] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f * xishu);
        vertices[1] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f * xishu);
        vertices[2] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, boxcollider.size.z) * 0.5f * xishu);
        vertices[3] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, boxcollider.size.y, -boxcollider.size.z) * 0.5f * xishu);
        //下面4个点  
        vertices[4] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f * xishu);
        vertices[6] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, boxcollider.size.z) * 0.5f * xishu) ;
        vertices[7] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(-boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f * xishu) ;
        vertices[5] = boxcollider.transform.TransformPoint(boxcollider.center + new Vector3(boxcollider.size.x, -boxcollider.size.y, -boxcollider.size.z) * 0.5f * xishu) ;
        return vertices;
    }

    public void changeTotal(int indexType)
    {
        testIndex = indexType;
        v3 = GetBoxColliderVertexPositions(box);
        switch (indexType)
        {
            //右上1
            case 0:
                totalx = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //右上2
            case 1:
                totalx = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //左上1
            case 2:
                totalx = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //左上2
            case 3:
                totalx = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //右下1
            case 4:
                totalx = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //右下2
            case 5:
                totalx = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //左下1
            case 6:
                totalx = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
            //左下2
            case 7:
                totalx = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[2]));
                totaly = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[4]));
                totalz = -Vector3.Distance(box.transform.InverseTransformPoint(v3[0]), box.transform.InverseTransformPoint(v3[1]));
                break;
        }
    }
    private float initDisRot;
    private Vector3 initBothRot;
    public void OnSCBothHandPointerDown(InputDevicePartBase part1, SCPointEventData part1EventData, InputDevicePartBase part2, SCPointEventData part2EventData)
    {
        switch (editType)
        {
            case EditTypeEnum.ALLEDIT:
                isBoth = true;
                changeLine();
                break;
        }
        switch (editType)
        {
            case EditTypeEnum.ALLEDIT:
                break;
            case EditTypeEnum.DEFECT:
                break;
            case EditTypeEnum.NORMAL:
                break;
        }
        
        initDis = SvrManager.Instance.head.InverseTransformPoint(part1.inputDeviceUIBase.model.lineIndicate.StartPoint.position).x - SvrManager.Instance.head.InverseTransformPoint(part2.inputDeviceUIBase.model.lineIndicate.StartPoint.position).x;
        initDisRot = SvrManager.Instance.head.InverseTransformPoint(part1.inputDeviceUIBase.model.lineIndicate.StartPoint.position).z- SvrManager.Instance.head.InverseTransformPoint(part2.inputDeviceUIBase.model.lineIndicate.StartPoint.position).z;
        scale3 = this.transform.localScale;
        this.sScale = this.scaleGameList[0].transform.localScale;
        this.rScale = this.eulerGameList[0].transform.localScale;
        initBothRot = this.transform.eulerAngles;
    }

    public TextMesh text;
    public void OnSCBothHandPointerDrag(InputDevicePartBase part1, SCPointEventData part1EventData, InputDevicePartBase part2, SCPointEventData part2EventData)
    {
        if(isBoth)
        {
            float disRot = SvrManager.Instance.head.InverseTransformPoint(part1.inputDeviceUIBase.model.lineIndicate.StartPoint.position).z - SvrManager.Instance.head.InverseTransformPoint(part2.inputDeviceUIBase.model.lineIndicate.StartPoint.position).z;

            float dis = SvrManager.Instance.head.InverseTransformPoint(part1.inputDeviceUIBase.model.lineIndicate.StartPoint.position).x - SvrManager.Instance.head.InverseTransformPoint(part2.inputDeviceUIBase.model.lineIndicate.StartPoint.position).x;
            
            float aaa = Mathf.Abs(disRot - initDisRot);
            float bbb = Mathf.Abs(dis - initDis);
            if (aaa < bbb&& bbb>0.01f)
            {
                float perValue = dis / initDis;
                if ((scale3 * perValue).x > (Tscale3 / 5).x && (scale3 * perValue).x < (Tscale3 * 5).x)
                {
                    this.transform.localScale = scale3 * (perValue);
                    this.lr.widthMultiplier = this.LineSize * (perValue);
                    for (int i = 0; i < this.veces.Length; i++)
                    {
                        this.scaleGameList[i].transform.localScale = this.sScale * (perValue);
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        this.eulerGameList[i].transform.localScale = this.rScale * (perValue);
                    }
                }
                initDisRot = SvrManager.Instance.head.InverseTransformPoint(part1.inputDeviceUIBase.model.lineIndicate.StartPoint.position).z - SvrManager.Instance.head.InverseTransformPoint(part2.inputDeviceUIBase.model.lineIndicate.StartPoint.position).z;

            }
            else if(Mathf.Abs(disRot - initDisRot) * 200>5)
            {
                initDis = SvrManager.Instance.head.InverseTransformPoint(part1.inputDeviceUIBase.model.lineIndicate.StartPoint.position).x - SvrManager.Instance.head.InverseTransformPoint(part2.inputDeviceUIBase.model.lineIndicate.StartPoint.position).x;

                this.transform.eulerAngles = new Vector3(initBothRot.x, initBothRot.y + (disRot - initDisRot) * 200, initBothRot.z);
                this.changeLine();
            }
        }
    }

    public void OnSCBothHandPointerUp(InputDevicePartBase part1, SCPointEventData part1EventData, InputDevicePartBase part2, SCPointEventData part2EventData)
    {
        isBoth = false;
        this.parentGame.transform.eulerAngles = this.transform.eulerAngles;
        isMove = false;
        // this.changeLine();
    }


}
