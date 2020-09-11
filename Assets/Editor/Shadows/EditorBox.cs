using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[CustomEditor(typeof(DragComponent),true)] //关联之前的脚本
public class EditorBox : Editor
{


    private SerializedObject obj; //序列化


    private SerializedProperty dm, editManagerType; //定义变量


    void OnEnable()
    {
        obj = new SerializedObject(target);

        editManagerType = obj.FindProperty("editManagerType");
        dm = obj.FindProperty("dm");

    }


    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        obj.Update();


    //    EditorGUILayout.PropertyField(editManagerType);


        if (editManagerType.enumValueIndex == 0)
        {
        }
        else if (editManagerType.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(dm);
        }


        obj.ApplyModifiedProperties();

    }


}