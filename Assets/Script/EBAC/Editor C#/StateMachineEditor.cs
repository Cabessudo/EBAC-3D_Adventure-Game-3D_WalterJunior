using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEditor;

[CustomEditor(typeof(FSMExample))]
public class StateMachineEditor : Editor
{
    public bool showFoldout;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var fsm = (FSMExample)target;

        EditorGUILayout.Space(30);
        EditorGUILayout.LabelField("State Machine");

        if(fsm.stateMachine == null) return;

        if(fsm.stateMachine.currState != null)
            EditorGUILayout.LabelField("Current State: ", fsm.stateMachine.currState.ToString());

        showFoldout = EditorGUILayout.Foldout(showFoldout, "Available States");
        
        if(showFoldout)
        {
            if(fsm.stateMachine.dictionary != null)
            {
                var keys = fsm.stateMachine.dictionary.Keys.ToArray();
                var vals = fsm.stateMachine.dictionary.Values.ToArray();

                for(int i = 0; i < keys.Length; i++)
                {
                    EditorGUILayout.LabelField(string.Format("{0} :: {1}", keys[i], vals[i]));
                }
            }
        }
    }
}