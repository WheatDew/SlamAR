using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public interface IHotfix
{

    int ShowGroupID();

    int ShowID();

    int SelfID();

    void SetName(string name);

    void SetContent(string content);

    string GetName();

    string GetContent();

    void UseTexture();

    void PlayAudio();

    void PlayVideo();

    void OnVuforiaFind();
    void OnVuforiaLost();

    /// <summary>
    /// 初始化
    /// </summary>
    void InitState();

    void OnHandlerEnterTrigger(SCPointEventData eventData);
    void OnHeadEnterTrigger(SCPointEventData eventData);

    void OnHandlerExitTrigger(SCPointEventData eventData);
    void OnHeadExitTrigger(SCPointEventData eventData);

    void OnHandlerDownTrigger(SCPointEventData eventData);

    void OnHandlerUpTrigger(SCPointEventData eventData);

    void OnHandlerDragTrigger(SCPointEventData eventData);

    void OnPlayerEnter();

    void OnPlayerExit();

    void OnMove();
}
