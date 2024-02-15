using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var myTarget = (Player)target;

        myTarget.speed = EditorGUILayout.FloatField("Walk Speed", myTarget.speed);
        myTarget.runSpeed = EditorGUILayout.FloatField("Run Speed", myTarget.runSpeed);

        EditorGUILayout.HelpBox("Calculate the Max Speed", MessageType.Info);
        EditorGUILayout.LabelField("MaxSpeed", myTarget.MaxSpeed.ToString());
        if(myTarget.MaxSpeed > 100)
        EditorGUILayout.HelpBox("To High", MessageType.Error);

        GUI.color = Color.black;
        if(GUILayout.Button("Button"))
        myTarget.speed = 50;
        
    }
}
