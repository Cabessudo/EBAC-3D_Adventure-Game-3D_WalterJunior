using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Player))]
public class PlayerEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var player = (Player)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(player.stateMachine == null) return;

        if(player.stateMachine.currState != null)
            EditorGUILayout.LabelField("Current State: ", player.stateMachine.currState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Parameters Available");

        if(showFoldout)
        {
            if(player.stateMachine.dictionary != null)
            {
                var keys = player.stateMachine.dictionary.Keys.ToArray();
                var vals = player.stateMachine.dictionary.Values.ToArray();

                for(int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} : {1}", keys[i], vals[i]));
                }
            }
        }
    }
}
