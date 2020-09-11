using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableTools
{
    /// <summary>
    /// 异步得到需要下载资源包的大小
    /// </summary>
    /// <param name="labels">标签</param>
    /// <param name="action">得到大小之后需要触发的事件</param>
    /// <returns></returns>
    public static IEnumerator GetDownloadSizeAsync(string labels, Action action)
    {
        var handler = Addressables.GetDownloadSizeAsync(labels);
        yield return handler;
        handler.Completed += t =>
        {
            if (action != null)
                action.Invoke();
        };
    }

    /// <summary>
    /// 获得下载进度
    /// </summary>
    /// <param name="name">Addressable目标对象名字</param>
    /// <returns></returns>
    public static IEnumerator DownloadAssetsAsync(string name, Action action)
    {
        var handler = Addressables.DownloadDependenciesAsync(name);
        var temp = handler.PercentComplete;
        yield return handler;
        while (!handler.IsDone)
        {
            if (temp == 0)
                temp = handler.PercentComplete;
            yield return null;
        }
        handler.Completed += t =>
        {
            if (action != null)
                action.Invoke();
        };
    }

    /// <summary>
    /// 异步加载单个资源
    /// </summary>
    /// <param name="name">Addressable目标对象名字</param>
    /// <returns></returns>
    public static IEnumerator LoadAssetAsync(string name, Action<GameObject> action)
    {
        var handler = Addressables.LoadAssetAsync<GameObject>(name);
        yield return handler;
        handler.Completed += t =>
        {
            if (action != null)
                action.Invoke(handler.Result);
        };
    }


    /// <summary>
    /// 通过地址和标签加载
    /// </summary>
    /// <param name="key"></param>
    /// <param name="label"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator LoadAssetsAsync(string key, string label, Action<GameObject> action)
    {
        var handler = Addressables.LoadAssetsAsync<GameObject>(new List<object> { key, label }, null, Addressables.MergeMode.Intersection);
        yield return handler;
        handler.Completed += t =>
        {
            if (action != null)
            {
                for (int i = 0; i < t.Result.Count; i++)
                {
                    action(t.Result[i]);
                }
            }
        };
    }

    /// <summary>
    /// 通过标签或者地址加载
    /// </summary>
    /// <param name="key"></param>
    /// <param name="action"></param>
    /// <returns></returns>
    public static IEnumerator LoadAssetsAsync(string key, Action<GameObject> action)
    {
        var handler = Addressables.LoadAssetsAsync<GameObject>(new List<object> { key }, null, Addressables.MergeMode.Intersection);
        yield return handler;
        handler.Completed += t =>
        {
            if (action != null)
            {
                for (int i = 0; i < t.Result.Count; i++)
                {
                    
                    action(t.Result[i]);
                }
            }
        };
    }

    public static IEnumerator InstantiateAsync(string key, Action<GameObject> action)
    {
        var handler = Addressables.InstantiateAsync(key);
        yield return handler;
        handler.Completed += t => {
            if (action != null)
                action.Invoke(t.Result);
        };
    }

    public static IEnumerator InstantiateAsync(string key,string label, Action<GameObject> action)
    {
        var handler = Addressables.InstantiateAsync(new List<object> { key, label });
        yield return handler;
        handler.Completed += t => {
            if (action != null)
                action.Invoke(t.Result);
        };
    }
}
