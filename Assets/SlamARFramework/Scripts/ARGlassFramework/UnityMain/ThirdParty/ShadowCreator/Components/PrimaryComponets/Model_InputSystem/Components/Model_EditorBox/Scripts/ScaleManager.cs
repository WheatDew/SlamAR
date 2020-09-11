using SC.InputSystem;
using SC.InputSystem.InputDeviceHandShank;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScaleManager : ChangeFcous
{
    [HideInInspector]
    public DragComponent dc;
    //[HideInInspector]
    public int index;
    [HideInInspector]
    public float perValue;
    // Start is called before the first frame update
    SCButton scbt;
    
    void Start()
    {
        if(dc!=null)
        {
        }
        model = dc.dm.focusScalModel;
        //   scbt = this.gameObject.AddComponent<SCButton>();
        //   UnityEvent onSCDown = new UnityEvent();
        //   onSCDown.AddListener(onDown);
        //  scbt.onDown = onSCDown;
    }

    public override void OnSCPointerExit(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerExit(part, eventData);
        dc.changeEnterPart(part.name,false);
        if (!dc.isEnterChoose())
            dc.HideBoxEdit();
    }

    override public void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData)
    {
        onDown();
        dragInitLocalPosition = eventData.dragPosition;
        //Debug.Log("xlv3Down===========》" + dragInitLocalPosition);
    }

    override public void OnSCPointerUp(InputDevicePartBase part, SCPointEventData eventData)
    {
        onUp();
    }
    override public void OnSCPointerDrag(InputDevicePartBase part, SCPointEventData eventData)
    {
        if (!dc.isBoth && !dc.isMove && isDown)
        {
            float temp = 0;
            float temp2 = 0;
            float temp3 = 0;
            Vector3 poslocal = eventData.dragPosition;
            Vector3 xlv3 = GetNormal(dragInitLocalPosition, poslocal);
            //Debug.Log("xlv3===========》"+xlv3);
            //  xlv3 = new Vector3(xlv3.x, xlv3.y, 0);
            // xlv3 = dc.testRot2.transform.InverseTransformPoint(xlv3);
            temp = xlv3.x;
            temp2 = xlv3.y;
            temp3 = xlv3.z;
            float xishu = 0.7f;
            switch (index)
            {
                case 0:
                    perValue = (1 + (temp + temp2 + temp3) * xishu);
                    break;
                case 1:
                    perValue = (1 + (temp + temp2 - temp3) * xishu);
                    break;
                case 2:
                    perValue = (1 + (-temp + temp2 + temp3) * xishu);
                    break;
                case 3:
                    perValue = (1 + (-temp + temp2 - temp3) * xishu);
                    break;
                case 4:
                    perValue = (1 + (temp - temp2 - temp3) * xishu);
                    break;
                case 5:
                    perValue = (1 + (temp - temp2 - temp3) * xishu);
                    break;
                case 6:
                    perValue = (1 + (-temp - temp2 + temp3) * xishu);
                    break;
                case 7:
                    perValue = (1 + (-temp - temp2 - temp3) * xishu);
                    break;
            }
            if ((dc.scale3 * perValue).x > (dc.Tscale3 / 5).x && (dc.scale3 * perValue).x < (dc.Tscale3 * 5).x)
            {

                dc.transform.localScale = dc.scale3 * (perValue);
                dc.lr.widthMultiplier = dc.LineSize * (perValue);
                if (dc.transform != dc.editGame.transform.parent)
                {
                    for (int i = 0; i < dc.veces.Length; i++)
                    {
                        dc.scaleGameList[i].transform.localScale = dc.sScale * (perValue);
                    }
                    for (int i = 0; i < 12; i++)
                    {
                        dc.eulerGameList[i].transform.localScale = dc.rScale * (perValue);
                    }
                }
            }
            dc.changeLine();
        }
        else
        {
            dc.isFangda = false;
        }
    
    }

    private void onUp()
    {
     //   InputDeviceEventBase.anyKeyUpDelegate -= onUp;
        isDown = false;
        dc.isFangda = false;
        dc.box.enabled = true;
        dc.transform.parent = dc.parentGame.transform.parent;
        for (int i = 0; i < dc.veces.Length; i++)
        {
            dc.scaleGameList[i].GetComponent<BoxCollider>().enabled = true;
        }
        for (int i = 0; i < 12; i++)
        {
            dc.eulerGameList[i].GetComponent<BoxCollider>().enabled = true;
        }
        dc.ShowBoxEdit();
    }
    Vector3 GetNormal(Vector3 b, Vector3 c)
    {

        b = dc.childGame.transform.InverseTransformPoint(b);
        c = dc.childGame.transform.InverseTransformPoint(c);
        return c - b;
    }
    Vector3 dragInitLocalPosition;
    Vector3 dragInitLocalPosition2;
    // Update is called once per frame
    Vector3 nowxz = new Vector3(0, 0, 0);



    
    override public void Update()
    {
        this.model.transform.LookAt(SvrManager.Instance.head);
        this.model.transform.eulerAngles = new Vector3(this.model.transform.eulerAngles.x, this.model.transform.eulerAngles.y, this.model.transform.eulerAngles.z + this.transform.localEulerAngles.z);
      
        
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
  //      InputDeviceEventBase.anyKeyUpDelegate += onUp;
        dc.changeTotal(index);
        dc.isFangda = true;
        dc.parentGame.transform.localEulerAngles = dc.transform.localEulerAngles;
        dc.transform.parent = dc.parentGame.transform;
        dc.scale3 = dc.transform.localScale;
        dc.sScale = dc.scaleGameList[0].transform.localScale;
        dc.rScale = dc.eulerGameList[0].transform.localScale;
        dc.LineSize = dc.lr.widthMultiplier;
        dc.rotEurl = dc.transform.localEulerAngles;
        dc.childGame.transform.localEulerAngles = new Vector3(0, 0, 0);
        dc.childGame.transform.localScale = dc.transform.localScale;
        dc.initPos = dc.transform.localPosition;
     //   dc.box.enabled = false;
        isDown = true;
    }

}
