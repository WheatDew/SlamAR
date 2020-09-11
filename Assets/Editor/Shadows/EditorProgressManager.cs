using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(ProgressManager), true)] //关联之前的脚本
public class EditorProgressManager : Editor
{


    private SerializedObject obj; //序列化


    private SerializedProperty scrollbar, editManagerType, control, text, audio, vp, mr, editManangerCase; //定义变量


    void OnEnable()
    {
        obj = new SerializedObject(target);

        editManagerType = obj.FindProperty("editScroll");
        scrollbar = obj.FindProperty("scrollbar");
        control = obj.FindProperty("control");
        editManangerCase = obj.FindProperty("editCase");
        text = obj.FindProperty("text");
        audio = obj.FindProperty("audio");
        vp = obj.FindProperty("vp");
        mr = obj.FindProperty("mr");
    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        obj.Update();


        //    EditorGUILayout.PropertyField(editManagerType);


        if (editManagerType.enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField(control);
        }
        else if (editManagerType.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(scrollbar);
            EditorGUILayout.PropertyField(control);
        }
        else if (editManagerType.enumValueIndex == 2)
        {
        }
        if (editManangerCase.enumValueIndex == 0)
        {
            EditorGUILayout.PropertyField(vp);
        }
        else if (editManangerCase.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(text);
            EditorGUILayout.PropertyField(audio);
        }
        else if (editManangerCase.enumValueIndex == 2)
        {
            EditorGUILayout.PropertyField(mr);
        }


        obj.ApplyModifiedProperties();

    }


}