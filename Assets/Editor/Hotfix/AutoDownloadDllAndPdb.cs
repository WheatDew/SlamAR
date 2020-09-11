using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[InitializeOnLoad]
public class AutoDownloadDllAndPdb 
{
    const string scriptsAssembliesDir = "Library/ScriptAssemblies";
    const string hotfixDir = "Assets/SlamARFramework/Resource/Hotfix";
    const string hotfixDll = "Unity.Hotfix.dll";
    const string hotfixPdb = "Unity.Hotfix.pdb";

    static AutoDownloadDllAndPdb()
    {
        File.Copy(Path.Combine(scriptsAssembliesDir, hotfixDll), Path.Combine(hotfixDir, hotfixDll + ".bytes"), true);
        File.Copy(Path.Combine(scriptsAssembliesDir, hotfixPdb), Path.Combine(hotfixDir, hotfixPdb + ".bytes"), true);
        Debug.Log("文件拷贝成功");
    }
}
