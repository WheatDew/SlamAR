using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


public class SlamSettings : EditorWindow {

    static string assetPath = "Assets/ShadowCreator/Resources/SlamParam.asset";
    public static void SaveAssetDataBase(bool isIgnor) {
        SlamParam asset;
        if(File.Exists(assetPath)) {
            asset = AssetDatabase.LoadAssetAtPath<SlamParam>(assetPath);
        } else {
            asset = (SlamParam)CreateInstance("SlamParam");
            AssetDatabase.CreateAsset(asset, assetPath);
        }
        asset.IsUseOtherSlam = isIgnor;
        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();//must Refresh
    }

    public static bool GetAssetDataBase() {
        SlamParam asset;
        if(File.Exists(assetPath)) {
            asset = AssetDatabase.LoadAssetAtPath<SlamParam>(assetPath);
            return asset.IsUseOtherSlam;
        }
        return false;
    }
}
