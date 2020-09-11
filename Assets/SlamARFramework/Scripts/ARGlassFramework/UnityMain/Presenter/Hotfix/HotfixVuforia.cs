using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HotfixVuforia : MonoBehaviour
{
    public string vuforiaCameraName;
    IEnumerator Start()
    {
        yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownVuforiaData);
        yield return new WaitUntil(DownloadAllPrepositionAssets.GetInstance.HasDownHotfixData);
        Addressables.InstantiateAsync(vuforiaCameraName).Completed += t => { t.Result.name = vuforiaCameraName; };
    }

}
