using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class AddreeableLoadHotfix : MonoBehaviour
{
    public string label;
    public string _name;
    void Start()
    {
        Addressables.InstantiateAsync(_name).Completed += t => { t.Result.name = _name; DownloadAllPrepositionAssets.GetInstance.hasDownHotfixData = true; };
    }
}
