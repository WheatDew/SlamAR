using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotfixDatabase : MonoBehaviour
{
    public TextAsset hotfixDll;
    public TextAsset hotfixPdb;

    private void Start()
    {
        FindObjectOfType<LoadHotfix>().hotfixDatabase = this;
    }
}