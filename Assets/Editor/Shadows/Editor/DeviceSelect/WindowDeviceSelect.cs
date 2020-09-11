using SC;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


public class WindowDeviceSelect : EditorWindow {

    static EditorWindow window;

   //[MenuItem("ShadowCreator/Device Select")]
    static void Init() {
        if(window == null) {
            window = EditorWindow.GetWindow(typeof(WindowDeviceSelect));
            window.autoRepaintOnSceneChange = true;
            window.minSize = new Vector2(500, 200);
            window.maxSize = new Vector2(500, 200);
        }
    }

    void OnGUI() {
        GUILayout.Space(10);
        NoticWindow();
        //isQualityApply = QualityWindow();
        //GUILayout.Space(10);
        //isPlayerApply = PlayerWindow();


        GUILayout.Space(20);
        ApplyWindow();

    }
    static void NoticWindow() {
        EditorGUILayout.BeginHorizontal(); 

        GUIStyle styleNoticeText = new GUIStyle();
        styleNoticeText.alignment = TextAnchor.MiddleCenter;
        styleNoticeText.fontSize = 20;
        styleNoticeText.fontStyle = FontStyle.Bold;
        DeviceParam.DeviceInfo deviceI = GetAssetDataBase();
        if(deviceI != null) {
            if(deviceI.type == DeviceParam.DeviceType.Action) {
                GUILayout.Label("Device Select [Action]", styleNoticeText);
            }else if(deviceI.type == DeviceParam.DeviceType.ActionPro) {
                GUILayout.Label("Device Select [ActionPro]", styleNoticeText);
            } else if(deviceI.type == DeviceParam.DeviceType.Jimo) {
                GUILayout.Label("Device Select [Jimo]", styleNoticeText);
            }
        } else {
            GUILayout.Label("Device Select [Null]", styleNoticeText);
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUIStyle styleSlide = new GUIStyle();
        styleSlide.fontSize = 16;
        styleSlide.normal.textColor = Color.red;
        styleSlide.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Notice: Select ActionPro if you have no idea", styleSlide);

        GUILayout.Space(10);
        GUIStyle style1Slide = new GUIStyle();
        style1Slide.fontSize = 12;
        style1Slide.alignment = TextAnchor.MiddleCenter;
        style1Slide.normal.textColor = Color.black;
        style1Slide.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Select Device According to The GlassType", style1Slide);

    }

    void ApplyWindow() { 
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(80));

        GUIStyle styleApply = new GUIStyle("LargeButton");
        styleApply.alignment = TextAnchor.MiddleCenter; 
        if(GUILayout.Button("Action", styleApply, GUILayout.Width(100), GUILayout.Height(50))) {
            SaveAssetDataBase(DeviceParam.DeviceType.Action);
            window.Close();
            Close();
        }
        
        EditorGUILayout.LabelField("", GUILayout.Width(10));
        GUIStyle styleApply2 = new GUIStyle("LargeButton");
        styleApply.alignment = TextAnchor.MiddleCenter;
        if(GUILayout.Button("ActionPro", styleApply2, GUILayout.Width(100), GUILayout.Height(50))) {
            SaveAssetDataBase(DeviceParam.DeviceType.ActionPro);
            window.Close();
            Close();
        }

        EditorGUILayout.LabelField("", GUILayout.Width(10));

        GUIStyle style1Apply = new GUIStyle("LargeButton");
        styleApply.alignment = TextAnchor.MiddleCenter;
        if(GUILayout.Button("Jimo", style1Apply, GUILayout.Width(100), GUILayout.Height(50))) {
            SaveAssetDataBase(DeviceParam.DeviceType.Jimo);
            window.Close();
            Close();
        }

        EditorGUILayout.LabelField("", GUILayout.Width(80));

        EditorGUILayout.EndHorizontal();
    }

    static string assetPath = "Assets/ShadowCreator/Resources/DevicesParam.asset";
    public static void SaveAssetDataBase(DeviceParam.DeviceType deviceT) {
        DeviceParam asset;
        if(File.Exists(assetPath)) {
            asset = AssetDatabase.LoadAssetAtPath<DeviceParam>(assetPath);
        } else {
            asset = (DeviceParam)CreateInstance("DeviceParam");
            AssetDatabase.CreateAsset(asset, assetPath);
        }
        if(deviceT == DeviceParam.DeviceType.Action) {
            asset.CurrentDevice = asset.Action;
        } else if(deviceT == DeviceParam.DeviceType.ActionPro) {
            asset.CurrentDevice = asset.ActionPro;
        } else if(deviceT == DeviceParam.DeviceType.Jimo) {
            asset.CurrentDevice = asset.Jimo;
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();//must Refresh
    }

    public static DeviceParam.DeviceInfo GetAssetDataBase() {
        DeviceParam asset;
        if(File.Exists(assetPath)) {
            asset = AssetDatabase.LoadAssetAtPath<DeviceParam>(assetPath);
            return asset.CurrentDevice;
        }
        return null;
    }

}
