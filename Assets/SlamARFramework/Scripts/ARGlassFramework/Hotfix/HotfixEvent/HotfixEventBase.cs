using SC.InputSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Unity.Hotfix
{
    public abstract class HotfixEventBase
    {
        internal bool isEnter;

        /// <summary>
        /// Vuforia识别图识别到了
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnVuforiaFind(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  Find");
            }
        }

        /// <summary>
        /// Vuforia识别图丢失了
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnVuforiaLost(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  Lost");
            }
        }

        /// <summary>
        /// 光标按下事件
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnHandlerRayDown(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  Down");
            }
        }

        /// <summary>
        /// 光标松开事件
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnHandlerRayUp(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  Up");
            }
        }

        /// <summary>
        /// 光标进入事件
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnHandlerRayEnter(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null && !isEnter)
            {
                isEnter = true;
                Debug.Log(obj + "  OnHandlerEnter");
            }
        }

        public virtual void OnHeadRayEnter(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null && !isEnter)
            {
                isEnter = true;
                Debug.Log(obj + "  OnHeadEnter");
            }
        }

        /// <summary>
        /// 光标移出事件
        /// </summary>
        /// <param name="obj"></param>
        public virtual void OnHandlerRayExit(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                isEnter = false;
                Debug.Log(obj + "  OnHandlerExit");
            }
        }

        public virtual void OnHeadRayExit(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                isEnter = false;
                Debug.Log(obj + "  OnHeadExit");
            }
        }

        /// <summary>
        /// 需要使用某个对象上的纹理贴图
        /// </summary>
        /// <param name="obj">目标对象</param>
        public virtual void OnUseTexture(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnUseTexture");
            }
        }

        /// <summary>
        /// 需要播放某个对象上的音频
        /// </summary>
        /// <param name="obj">目标对象</param>
        public virtual void OnPlayAudio(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnPlayAudio");
            }
        }

        /// <summary>
        /// 需要播放某个对象上的视频
        /// </summary>
        /// <param name="obj">目标对象</param>
        public virtual void OnPlayVideo(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnPlayVideo");
            }
        }

        /// <summary>
        /// 移动到目标点
        /// </summary>
        /// <param name="obj">目标对象</param>
        public virtual void OnMoveToTargetPosition(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnMoveToTargetPosition");
            }
        }

        public virtual void OnDragTarget(GameObject obj, SCPointEventData eventData)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnDragTarget");
            }
        }

        public virtual void OnInitState(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnInitState");
            }
        }

        public virtual void OnPlayerEnter(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnPlayerEnter");
            }
        }

        public virtual void OnPlayerExit(GameObject obj)
        {
            if (obj.GetComponent<IHotfix>() != null)
            {
                Debug.Log(obj + "  OnPlayerExit");
            }
        }
    }
}