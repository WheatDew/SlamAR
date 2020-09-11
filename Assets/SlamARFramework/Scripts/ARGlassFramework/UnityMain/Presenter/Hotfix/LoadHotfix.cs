using ILRuntime.CLR.TypeSystem;
using ILRuntime.Mono.Cecil.Pdb;
using ILRuntime.Runtime.Intepreter;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

public class LoadHotfix : MonoBehaviour
{
    public HotfixDatabase hotfixDatabase;
    MemoryStream dll, pdb;
    ILRuntime.Runtime.Enviorment.AppDomain appDomain;
    IType type;
    ILTypeInstance obj;
    private void Start()
    {
        StartCoroutine(Load());
        ClientEventDispatcher.GetInstance().Manager.AddEventListener<int>(EventID.LoadTargetHotfixAssets, LoadHotfixAssets);
        ClientEventDispatcher.GetInstance().Manager.TriggerEvent<int>(EventID.LoadTargetHotfixAssets, 0);
    }

    private void OnDestroy()
    {
        ClientEventDispatcher.GetInstance().Manager.RemoveEventListener<int>(EventID.LoadTargetHotfixAssets, LoadHotfixAssets);
    }

    bool HasGetDll()
    {
        return hotfixDatabase && DownloadAllPrepositionAssets.GetInstance.HasDownJsonData();
    }

    IEnumerator Load()
    {
        yield return new WaitUntil(HasGetDll);
        byte[] dllByte = hotfixDatabase.hotfixDll.bytes;
        byte[] pdbByte = hotfixDatabase.hotfixPdb.bytes;
#if ILRuntime
        dll = new MemoryStream(dllByte);
        pdb = new MemoryStream(pdbByte);

        appDomain = new ILRuntime.Runtime.Enviorment.AppDomain();
        appDomain.LoadAssembly(dll, pdb, new PdbReaderProvider());

        //转换委托
        appDomain.DelegateManager.RegisterMethodDelegate<GameObject>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject>>();
        appDomain.RegisterCrossBindingAdaptor(new CoroutineAdapter());
        appDomain.DelegateManager.RegisterFunctionDelegate<bool>(); 
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<IList<GameObject>>>();
        appDomain.DelegateManager.RegisterMethodDelegate<GameObject, SC.InputSystem.SCPointEventData>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<System.Int64>>();
        appDomain.DelegateManager.RegisterMethodDelegate<AudioClip>();
        appDomain.DelegateManager.RegisterMethodDelegate<UnityEngine.Texture2D>();

        MainScriptsEventChange.ChangeHandlerDownFunc(appDomain);
        MainScriptsEventChange.ChangeHandlerUpFunc(appDomain);
        MainScriptsEventChange.ChangeHandlerEnterFunc(appDomain);
        MainScriptsEventChange.ChangeHeadEnterFunc(appDomain);
        MainScriptsEventChange.ChangeHandlerExitFunc(appDomain);
        MainScriptsEventChange.ChangeHeadExitFunc(appDomain);
        MainScriptsEventChange.ChangeFindFunc(appDomain);
        MainScriptsEventChange.ChangeLostFunc(appDomain);
        MainScriptsEventChange.ChangeUseTextureFunc(appDomain);
        MainScriptsEventChange.ChangePlayAudioFunc(appDomain);
        MainScriptsEventChange.ChangePlayVideoFunc(appDomain);
        MainScriptsEventChange.ChangeMoveFunc(appDomain);
        MainScriptsEventChange.ChangeVuforiaFindFunc(appDomain);
        MainScriptsEventChange.ChangeVuforiaLostFunc(appDomain);
        MainScriptsEventChange.ChangeDragFunc(appDomain);
        MainScriptsEventChange.ChangeInitStateFunc(appDomain);
        MainScriptsEventChange.ChangeDoTweenEventFunc(appDomain);
        MainScriptsEventChange.ChangePlayerEnterFunc(appDomain);
        MainScriptsEventChange.ChangePlayerExitFunc(appDomain);

#else
        Assembly.Load(dllByte, pdbByte);
#endif
        //StartCoroutine(OnMethodInvoke());
    }

    IEnumerator OnMethodInvoke(int index)
    {
        yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownHotfixData);
        yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownJsonData);
        //type = appDomain.LoadedTypes["Unity.Hotfix.LoadAssets"];
        //obj = ((ILType)type).Instantiate();
        //appDomain.Invoke("Unity.Hotfix.LoadAssets", "Func", obj, new object[] { "hellow", 123 });
        appDomain.Invoke("Unity.Hotfix.LoadAssets", "HotfixFuncEnter", null, null);
    }

    void LoadHotfixAssets(int index)
    {
        StartCoroutine(OnMethodInvoke(index));
    }
}