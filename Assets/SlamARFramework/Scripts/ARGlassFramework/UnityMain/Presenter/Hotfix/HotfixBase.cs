using SC.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

#region 交互事件委托
public delegate void OnHandlerEnter(GameObject obj, SCPointEventData eventData);
public delegate void OnHeadEnter(GameObject obj, SCPointEventData eventData);
public delegate void OnHandlerExit(GameObject obj, SCPointEventData eventData);
public delegate void OnHeadExit(GameObject obj, SCPointEventData eventData);
public delegate void OnHandlerDown(GameObject obj, SCPointEventData eventData);
public delegate void OnHandlerUp(GameObject obj, SCPointEventData eventData);
public delegate void OnHandlerDrag(GameObject obj, SCPointEventData eventData);
public delegate void OnPlayerEnter(GameObject obj);
public delegate void OnPlayerExit(GameObject obj);
public delegate void OnVuforiaFind(GameObject obj);
public delegate void OnVuforiaLost(GameObject obj);
#endregion

#region 事件委托
public delegate void UseTexture(GameObject obj);
public delegate void PlayAudio(GameObject obj);
public delegate void PlayVideo(GameObject obj);
public delegate void OnInitState(GameObject obj);
public delegate void MoveToTargetPosition(GameObject obj);
#endregion

public class HotfixBase : PointerHandlers, IHotfix
{
    #region 字段
    [SerializeField]
    int showGroupID;
    [SerializeField]
    int showID;
    [SerializeField]
    int selfID;
    [SerializeField]
    string _name;
    [SerializeField]
    string content;
    #endregion

    #region 事件

    public event OnHandlerEnter _onHandlerEnter;
    public event OnHeadEnter _onHeadEnter;
    public event OnHandlerExit _onHandlerExit;
    public event OnHeadExit _onHeadExit;
    public event OnPlayerEnter _onPlayerEnter;
    public event OnPlayerExit _onPlayerExit;

    public event OnHandlerDown _onHandlerDown;
    public event OnHandlerUp _onHandlerUp;
    public event OnHandlerDrag _onHandlerDrag;
    public event OnVuforiaFind _onVuforiaFind;
    public event OnVuforiaLost _onVuforiaLost;

    public event UseTexture _onUseTexture;
    public event PlayAudio _onPlayAudio;
    public event PlayVideo _onPlayVideo;
    public event OnInitState _onInitState;
    public event MoveToTargetPosition _onMoveToTargetPosition;

    #endregion

    public override void OnSCPointerDown(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerDown(part, eventData);
        GetComponent<IHotfix>().OnHandlerDownTrigger(eventData);
    }

    public override void OnSCPointerUp(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerUp(part, eventData);
        GetComponent<IHotfix>().OnHandlerUpTrigger(eventData);
    }

    public override void OnSCPointerEnter(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerEnter(part, eventData);
        GetComponent<IHotfix>().OnHandlerEnterTrigger(eventData);
    }

    public override void OnSCPointerExit(InputDevicePartBase part, SCPointEventData eventData)
    {
        base.OnSCPointerExit(part, eventData);
        GetComponent<IHotfix>().OnHandlerExitTrigger(eventData);
    }

    public override void OnSCPointerDrag(InputDevicePartBase part, SCPointEventData eventData)
    {
        GetComponent<IHotfix>().OnHandlerDragTrigger(eventData);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print(1);
            GetComponent<IHotfix>().OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            print(2);
            GetComponent<IHotfix>().OnPlayerExit();
        }
    }

    #region 接口实现
    public int ShowGroupID()
    {
        return showGroupID;
    }

    public int SelfID()
    {
        return selfID;
    }

    public void UseTexture()
    {
        if (_onUseTexture != null)
            _onUseTexture.Invoke(gameObject);
    }

    public void PlayAudio()
    {
        if (_onPlayAudio != null)
            _onPlayAudio.Invoke(gameObject);
    }

    public void PlayVideo()
    {
        if (_onPlayVideo != null)
            _onPlayVideo.Invoke(gameObject);
    }

    public virtual void OnHandlerDownTrigger(SCPointEventData eventData)
    {
        if (_onHandlerDown != null)
            _onHandlerDown.Invoke(gameObject, eventData);
    }

    public virtual void OnHandlerEnterTrigger(SCPointEventData eventData)
    {
        if (_onHandlerEnter != null)
            _onHandlerEnter.Invoke(gameObject, eventData);
    }

    public virtual void OnHandlerExitTrigger(SCPointEventData eventData)
    {
        if (_onHandlerExit != null)
            _onHandlerExit.Invoke(gameObject, eventData);
    }

    public virtual void OnHandlerUpTrigger(SCPointEventData eventData)
    {
        if (_onHandlerUp != null)
            _onHandlerUp.Invoke(gameObject, eventData);
    }

    public void OnVuforiaFind()
    {
        if (_onVuforiaFind != null)
            _onVuforiaFind.Invoke(gameObject);
    }

    public void OnVuforiaLost()
    {
        if (_onVuforiaLost != null)
            _onVuforiaLost.Invoke(gameObject);
    }

    public void OnMove()
    {
        if (_onMoveToTargetPosition != null)
            _onMoveToTargetPosition.Invoke(gameObject);
    }

    public void OnHandlerDragTrigger(SCPointEventData eventData)

    {
        if (_onHandlerDrag != null)
            _onHandlerDrag.Invoke(gameObject, eventData);
    }

    public void OnHeadEnterTrigger(SCPointEventData eventData)
    {
        if (_onHeadEnter != null)
            _onHeadEnter.Invoke(gameObject, eventData);
    }

    public void OnHeadExitTrigger(SCPointEventData eventData)
    {
        if (_onHeadExit != null)
            _onHeadExit.Invoke(gameObject, eventData);
    }

    public void InitState()
    {
        if (_onInitState != null)
            _onInitState.Invoke(gameObject);
    }

    public void SetName(string name)
    {
        _name = name;
    }

    public void SetContent(string content)
    {
        this.content = content;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetContent()
    {
        return content;
    }

    public int ShowID()
    {
        return showID;
    }

    public void OnPlayerEnter()
    {
        if (_onPlayerEnter != null)
            _onPlayerEnter(gameObject);
    }

    public void OnPlayerExit()
    {
        if (_onPlayerExit != null)
            _onPlayerExit(gameObject);
    }


    #endregion
}
