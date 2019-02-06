using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Dialogue))]
public class DialogueInspector : Editor
{
    public override void OnInspectorGUI() {
        Dialogue dialogue = (Dialogue)target;       

        DrawDefaultInspector();

        // for(int i = 0; i < dialogue.lines.Count; i++) {
        //     Lines l = dialogue.lines[i];
        //     l.character = (Character) EditorGUILayout.ObjectField("Character", l.character, typeof(Character), false);
        //     l.line = EditorGUILayout.TextField("Line", l.line, GUILayout.MaxHeight(20));
        // }

        if(GUILayout.Button("Add Line")) {
            
            dialogue.lines.Add(new Lines());
            // EditorUtility.SetDirty(dialogue);
            // EditorApplication.MarkSceneDirty();
        }
    }
}
