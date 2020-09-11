using SC;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


public class WindowSlamSelect : EditorWindow {

    static EditorWindow window;

   [MenuItem("ShadowCreator/Slam Select")]
    static void Init() {
        if(window == null) {
            window = EditorWindow.GetWindow(typeof(WindowSlamSelect));
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
        if(SlamSettings.GetAssetDataBase()) {
            GUILayout.Label("Slam Select [Other]", styleNoticeText);
        } else {
            GUILayout.Label("Slam Select [SVR]", styleNoticeText);
        }

        EditorGUILayout.EndHorizontal();

        GUILayout.Space(10);

        GUIStyle styleSlide = new GUIStyle();
        styleSlide.fontSize = 16;
        styleSlide.normal.textColor = Color.red;
        styleSlide.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("Notice: Select SVR if you have no idea", styleSlide);

        GUILayout.Space(10);
        GUIStyle style1Slide = new GUIStyle();
        style1Slide.fontSize = 12;
        style1Slide.alignment = TextAnchor.MiddleCenter;
        style1Slide.normal.textColor = Color.black;
        style1Slide.alignment = TextAnchor.MiddleCenter;
        GUILayout.Label("SVR:default Slam", style1Slide);
        GUILayout.Label("Other: other slam like Vuforia,you must set you Camera tag 'MainCamera'", style1Slide);

    }

    void ApplyWindow() { 
        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("", GUILayout.Width(100));

        GUIStyle styleApply = new GUIStyle("LargeButton");
        styleApply.alignment = TextAnchor.MiddleCenter; 
        if(GUILayout.Button("SVR", styleApply, GUILayout.Width(100), GUILayout.Height(50))) {
            SlamSettings.SaveAssetDataBase(false);
            ShadowSystem ss = FindObjectOfType<ShadowSystem>();
            if(ss) {
                SvrManager svr = ss.transform.GetComponentInChildren<SvrManager>(true);
                if(svr) {
                    svr.gameObject.SetActive(true);
                }
            }
            window.Close();
            Close();
        }
        
        EditorGUILayout.LabelField("", GUILayout.Width(100));

        GUIStyle style1Apply = new GUIStyle("LargeButton");
        styleApply.alignment = TextAnchor.MiddleCenter;
        if(GUILayout.Button("Other", style1Apply, GUILayout.Width(100), GUILayout.Height(50))) {
            SlamSettings.SaveAssetDataBase(true);
            SvrManager svr = FindObjectOfType<SvrManager>();
            if(svr) {
                svr.gameObject.SetActive(false);
            }
            window.Close();
            Close();
        }

        EditorGUILayout.LabelField("", GUILayout.Width(100));

        EditorGUILayout.EndHorizontal();
    }

     

}
