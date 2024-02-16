using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        var gm = (GameManager)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(gm.stateMachine == null) return;

        if(gm.stateMachine.currState != null) 
            EditorGUILayout.LabelField("Current State: ", gm.stateMachine.currState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Parameters Availables");

        if(showFoldout)
        {
            if(gm.stateMachine.dictionary != null)
            {
                var keys = gm.stateMachine.dictionary.Keys.ToArray();
                var vals = gm.stateMachine.dictionary.Values.ToArray();

                for(int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0}, {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
