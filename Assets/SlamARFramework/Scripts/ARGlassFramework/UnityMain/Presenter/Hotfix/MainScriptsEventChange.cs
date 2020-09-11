using SC.InputSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MainScriptsEventChange
{
    public static void ChangePlayerEnterFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnPlayerEnter>((act) =>
        {
            return new OnPlayerEnter((obj) =>
            {
                ((Action<GameObject>)act)(obj);
            });
        });
    }
    public static void ChangePlayerExitFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnPlayerExit>((act) =>
        {
            return new OnPlayerExit((obj) =>
            {
                ((Action<GameObject>)act)(obj);
            });
        });
    }
    public static void ChangeDoTweenEventFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<DG.Tweening.TweenCallback>((act) =>
        {
             return new DG.Tweening.TweenCallback(() =>
            {
                ((Action)act)();
            });
        });
    }
    public static void ChangeInitStateFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnInitState>((action) =>
        {
            return new OnInitState((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }
    public static void ChangeHandlerDownFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHandlerDown>((action) =>
        {
            return new OnHandlerDown((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }

    public static void ChangeHandlerUpFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHandlerUp>((action) =>
        {
            return new OnHandlerUp((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }

    public static void ChangeHandlerEnterFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHandlerEnter>((action) =>
        {
            return new OnHandlerEnter((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }

    public static void ChangeHeadEnterFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHeadEnter>((action) =>
        {
            return new OnHeadEnter((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }

    public static void ChangeHandlerExitFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHandlerExit>((action) =>
        {
            return new OnHandlerExit((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }


    public static void ChangeHeadExitFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHeadExit>((action) =>
        {
            return new OnHeadExit((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }

    public static void ChangeFindFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnVuforiaFind>((action) =>
        {
            return new OnVuforiaFind((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangeLostFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnVuforiaLost>((action) =>
        {
            return new OnVuforiaLost((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangeUseTextureFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<UseTexture>((action) =>
        {
            return new UseTexture((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangePlayAudioFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<PlayAudio>((action) =>
        {
            return new PlayAudio((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangePlayVideoFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<PlayVideo>((action) =>
        {
            return new PlayVideo((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangeMoveFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<MoveToTargetPosition>((action) =>
        {
            return new MoveToTargetPosition((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangeVuforiaFindFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<VuforiaFind>((action) =>
        {
            return new VuforiaFind((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }

    public static void ChangeVuforiaLostFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<VuforiaLost>((action) =>
        {
            return new VuforiaLost((obj) =>
            {
                ((Action<GameObject>)action)(obj);
            });
        });
    }


    public static void ChangeDragFunc(ILRuntime.Runtime.Enviorment.AppDomain appDomain)
    {
        appDomain.DelegateManager.RegisterDelegateConvertor<OnHandlerDrag>((action) =>
        {
            return new OnHandlerDrag((obj, data) =>
            {
                ((Action<GameObject, SCPointEventData>)action)(obj, data);
            });
        });
    }
}
