using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Unity.Hotfix
{

    public class LoadAssets
    {
        public static void HotfixFuncEnter()
        {
            //LoginGroupEvent();
            //QRCodeTaskGroupEvent();
            //TaskSystemGroupEvent();
        }

        //任务系统界面
        public static void TaskSystemGroupEvent()
        {
            string assetsName = "TaskSystemGroup";
            Addressables.InstantiateAsync(assetsName).Completed += t =>
            {
                RegisterEvent(t.Result.GetComponent<HotfixBase>(), new TaskSystemGroupEvent());
                t.Result.GetComponent<IHotfix>().InitState();
            };
        }


        public static void QRCodeTaskGroupEvent()
        {
            string assetsName = "QRCodeTaskGroup";
            Addressables.InstantiateAsync(assetsName).Completed += t =>
            {
                RegisterEvent(t.Result.GetComponent<HotfixBase>(), new QRCodeTaskGroupEvent());
                t.Result.GetComponent<IHotfix>().InitState();
            };
        }


        //登录界面
        public static void LoginGroupEvent()
        {
            string assetsName = "LoginGroup";
            Addressables.InstantiateAsync(assetsName).Completed += t =>
            {
                RegisterEvent(t.Result.GetComponent<HotfixBase>(), new LoginGroupEvent());
                t.Result.GetComponent<IHotfix>().InitState();
            };
        }


        public static void Cube()
        {
            string assetsName = "Cube";
            Addressables.InstantiateAsync(assetsName).Completed += t => {
                RegisterEvent(t.Result.GetComponent<HotfixBase>(), new CubeEvent());
                t.Result.GetComponent<IHotfix>().InitState();
                //DownloadAllPrepositionAssets.GetInstance.hasInitAddressables = true;
            };
            //Addressables.GetDownloadSizeAsync
            //Addressables.LoadAssetAsync<Texture>(assetsName).Completed
        }

        static void RegisterEvent(HotfixBase target, HotfixEventBase _event)
        {
            target._onHandlerEnter += _event.OnHandlerRayEnter;
            target._onHandlerExit += _event.OnHandlerRayExit;
            target._onHandlerDown += _event.OnHandlerRayDown;
            target._onHandlerUp += _event.OnHandlerRayUp;
            target._onVuforiaFind += _event.OnVuforiaFind;
            target._onVuforiaLost += _event.OnVuforiaLost;
            target._onUseTexture += _event.OnUseTexture;
            target._onPlayAudio += _event.OnPlayAudio;
            target._onPlayVideo += _event.OnPlayVideo;
            target._onMoveToTargetPosition += _event.OnMoveToTargetPosition;
            target._onHandlerDrag += _event.OnDragTarget;
            target._onHeadEnter += _event.OnHeadRayEnter;
            target._onHeadExit += _event.OnHeadRayExit;
            target._onInitState += _event.OnInitState;
            target._onPlayerEnter += _event.OnPlayerEnter;
            target._onPlayerExit += _event.OnPlayerExit;
        }

        static void UnregisterEvent(HotfixBase target, HotfixEventBase _event)
        {
            target._onHandlerEnter -= _event.OnHandlerRayEnter;
            target._onHandlerExit -= _event.OnHandlerRayExit;
            target._onHandlerDown -= _event.OnHandlerRayDown;
            target._onHandlerUp -= _event.OnHandlerRayUp;
            target._onVuforiaFind -= _event.OnVuforiaFind;
            target._onVuforiaLost -= _event.OnVuforiaLost;
            target._onUseTexture -= _event.OnUseTexture;
            target._onPlayAudio -= _event.OnPlayAudio;
            target._onPlayVideo -= _event.OnPlayVideo;
            target._onHandlerDrag -= _event.OnDragTarget;
            target._onHeadEnter -= _event.OnHeadRayEnter;
            target._onHeadExit -= _event.OnHeadRayExit;
            target._onInitState -= _event.OnInitState;
            target._onPlayerEnter -= _event.OnPlayerEnter;
            target._onPlayerExit -= _event.OnPlayerExit;
        }

  
        /// <summary>
        /// 卸载所有资源
        /// </summary>
        static void UnloadHotfixAssets()
        {
            Debug.Log("卸载资源");
            //for (int i = 0; i < namesMain.Length; i++)
            //{
            //    if (objMain[i])
            //        Addressables.ReleaseInstance(objMain[i]);
            //    if (handleMain[i].IsValid())
            //        Addressables.Release(handleMain[i]);
            //}
            //for (int i = 0; i < namesMessage.Length; i++)
            //{
            //    if (objMessage[i])
            //        Addressables.ReleaseInstance(objMessage[i]);
            //    if (handleMessage[i].IsValid())
            //        Addressables.Release(handleMessage[i]);
            //}
            //for (int i = 0; i < nameShow.Length; i++)
            //{
            //    if (objShow[i])
            //        Addressables.ReleaseInstance(objShow[i]);
            //    if (handleShow[i].IsValid())
            //        Addressables.Release(handleShow[i]);
            //}
            //for (int i = 0; i < nameShow1.Length; i++)
            //{
            //    if (objShow1[i])
            //        Addressables.ReleaseInstance(objShow1[i]);
            //    if (handleShow1[i].IsValid())
            //        Addressables.Release(handleShow1[i]);
            //}
            //for (int i = 0; i < nameShow2.Length; i++)
            //{
            //    if (objShow2[i])
            //        Addressables.ReleaseInstance(objShow2[i]);
            //    if (handleShow2[i].IsValid())
            //        Addressables.Release(handleShow2[i]);
            //}
            //for (int i = 0; i < nameShow3.Length; i++)
            //{
            //    if (objShow3[i])
            //        Addressables.ReleaseInstance(objShow3[i]);
            //    if (handleShow3[i].IsValid())
            //        Addressables.Release(handleShow3[i]);
            //}
            Resources.UnloadUnusedAssets();
            GC.Collect();
        }

    }
}