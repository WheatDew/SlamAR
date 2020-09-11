using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RotManager : ChangeFcous
{ 
    [HideInInspector]
    public DragComponent dc;
   // [HideInInspector]
    public int index;
    [HideInInspector]
    public float perValue;
    // Start is called before the first frame update
    SCButton scbt;

    private GameObject AllowPrefab;
    GameObject rotgameclone;
    bool isOpen = false;
    Transform focusobj;
    void Start()
    {
        // scbt = this.gameObject.AddComponent<SCButton>();
        // UnityEvent onSCDown = new UnityEvent();
        // onSCDown.AddListener(onDown);
        //scbt.onDown = onSCDown;
        box = this.GetComponent<BoxCollider>();
        AllowPrefab = Resources.Load<GameObject>("ShadowEidtBox");
        rotgameclone = AllowPrefab.GetComponent<DragManager>().focusRotModel;
        //model = dc.dm.focusRotModel;
         focusobj = GameObject.Find("Focus").transform;
    }

    override public void OnSCPointerEnter(InputDevicePartBase part, SCPointEventData eventData)
    {
        isOpen = true;
    }

    public override void OnSCPointerExit(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerExit(part, eventData);
        isOpen = false;
        if (objClone != null)
        {
            Destroy(objClone);
        }
        dc.changeEnterPart(part.name, false);
        if (!dc.isEnterChoose())
            dc.HideBoxEdit();
    }
    override public void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData)
    {
        dragInitLocalPosition = eventData.dragPosition;
        Debug.Log("Down=======>" + dragInitLocalPosition.x.ToString() + dragInitLocalPosition.y.ToString() + dragInitLocalPosition.z.ToString());
        onDown();
    }

    override public void OnSCPointerUp(InputDevicePartBase part, SCPointEventData eventData)
    {
        onUp();
    }

    override public void OnSCPointerDrag(InputDevicePartBase part, SCPointEventData eventData)
    {
        if (!dc.isBoth&&!dc.isMove && isDown)
        {
            float temp = 0;
            float temp2 = 0;
            float temp3 = 0;
            // Vector3 poslocal = new Vector3(0, 0, 0);
            Vector3 poslocal = eventData.dragPosition;
            Vector3 xlv3 = GetNormal(dragInitLocalPosition, poslocal);
            temp = xlv3.x;
            temp2 = xlv3.y;
            temp3 = xlv3.z;
            float xishu = 40;
            if (index == 4 || index == 8 || index == 9 || index == 11)
            {

                if (index == 9 || index == 11)
                {
                    temp = -temp;
                }
                if (index == 4 || index == 11)
                {
                    temp3 = -temp3;
                }
                dc.transform.localEulerAngles = new Vector3(0, (temp + temp3 + temp2) * xishu, 0);
                //  dc.transform.Rotate(xxjd - nowxz);
                //   Debug.Log("temp========>"+ index + nowxz);
            }
            else if (index == 2 || index == 6 || index == 0 || index == 7)
            {
                if (index == 7 || index == 6)
                {
                    temp = -temp;
                }
                if (index == 7 || index == 0)
                {
                    temp3 = -temp3;
                }
                if (index == 2 || index == 6)
                {
                    temp2 = -temp2;
                }
                dc.transform.localEulerAngles = new Vector3(0, 0, -(temp + temp3 - temp2) * xishu);
                Debug.Log(" 绕Z轴旋转Z" + index);
            }
            else
            {
                if (index == 5 || index == 10)
                {
                    temp3 = -temp3;
                }
                if (index == 5 || index == 3)
                {
                    temp2 = -temp2;
                }
                dc.transform.localEulerAngles = new Vector3((temp + temp3 + temp2) * xishu, 0, 0);
                Debug.Log(" 绕X轴旋转Y" + index + xlv3);
            }
            dc.changeLine();
            if (focusobj.childCount>=4)
            {
                focusobj.GetChild(3).gameObject.SetActive(false);
            }                     
        }
        else
        {
            Debug.Log("关闭DOWN");
            isDown = false;
        }
    }

     private void onUp()
    {
        //    InputDeviceEventBase.anyKeyUpDelegate -= onUp;
        isOpen = false;
        if (objClone != null)
        {
            Destroy(objClone);
        }
        isDown = false;
        dc.box.enabled = true;
        dc.transform.parent = dc.parentGame.transform.parent;
        dc.parentGame.transform.rotation = dc.transform.rotation;
        nowxz = new Vector3(0,0,0);
        dc.editGame.transform.parent = dc.parentGame.transform;
        dc.ShowBoxEdit();
    }
    Vector3 GetNormal(Vector3 b, Vector3 c)
    {
        b = dc.childGame.transform.InverseTransformPoint(b);
        c = dc.childGame.transform.InverseTransformPoint(c);
        Debug.Log("b=======>" + b.x.ToString() + "," + b.y.ToString() + "," + b.z.ToString());
        Debug.Log("c=======>" + c.x.ToString()+","+ c.y.ToString() + "," + c.z.ToString());
        return c - b;

    }
    Vector3 dragInitLocalPosition;
    Vector3 nowxz =new Vector3(0,0,0);


    BoxCollider box;
    Vector3 objCenter;
    Vector3 objUp;
    private void GetObjUpPoint(BoxCollider boxCollider)
    {
        objCenter = this.transform.TransformPoint(boxCollider.center.x, boxCollider.center.y, boxCollider.center.z); ;
        objUp = this.transform.TransformPoint(boxCollider.center.x, boxCollider.center.y + boxCollider.size.y * 0.5f, boxCollider.center.z);

    }
   

    public  GameObject objClone;
    public void ShowModel2()
    {
        GetObjUpPoint(box);
        rotgameclone.SetActive(true);
        Vector3 a = new Vector3(objUp.x - objCenter.x, objUp.y - objCenter.y, objUp.z - objCenter.z);
        Vector3 b = new Vector3(objUp.x - objCenter.x, 0, objUp.z - objCenter.z);
        float angle = Vector3.Angle(a, b);
        if (a.x == 0 && a.z == 0)
        {
            rotgameclone.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            rotgameclone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        else if (a.y == 0)
        {
            rotgameclone.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            rotgameclone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }
        else if (angle < 45)
        {
            rotgameclone.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            rotgameclone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        }
        else if (angle >= 45)
        {
            rotgameclone.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            rotgameclone.transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        }
        if (objClone == null)
        {
            objClone = GameObject.Instantiate(rotgameclone);
            objClone.transform.parent =focusobj;
            objClone.transform.localPosition = Vector3.zero;
            objClone.transform.localEulerAngles = Vector3.zero;
        }

    }

    override public void Update()
    {
        /*
        this.model.transform.LookAt(SvrManager.Instance.head);
        this.model.transform.eulerAngles = new Vector3(this.model.transform.eulerAngles.x, this.model.transform.eulerAngles.y, this.model.transform.eulerAngles.z + this.transform.localEulerAngles.z);
  */
        if (isOpen)
        {
            ShowModel2();
        }
        else
        {
            if (objClone != null)
            {
                Destroy(objClone);
            }
        }

    }
    private bool isDown;

    public void onDown()
    {
        /*
        for (int i = 0; i < dc.veces.Length; i++)
        {
            dc.scaleGameList[i].GetComponent<BoxCollider>().enabled = false;
        }
        for (int i = 0; i < 12; i++)
        {
            dc.eulerGameList[i].GetComponent<BoxCollider>().enabled = false;
        }*/
     //   InputDeviceEventBase.anyKeyUpDelegate += onUp;
        dc.parentGame.transform.localEulerAngles = dc.transform.localEulerAngles;
        dc.parentGame.transform.position = dc.transform.position;
        dc.transform.parent = dc.parentGame.transform;
        dc.rotEurl = dc.transform.localEulerAngles;
        dc.editGame.transform.parent = dc.transform;
        dc.childGame.transform.localEulerAngles = new Vector3(0, 0, 0);
        dc.childGame.transform.localScale = dc.transform.localScale;

       // dc.box.enabled = false;
        dc.changeLine();
        Debug.Log("触发DOWN");
        isDown = true;
    }
}
