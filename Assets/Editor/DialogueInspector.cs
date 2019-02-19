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
            Lines line = new Lines();
            dialogue.lines.Add(line);

            // Try to guess which speaker is next
            if(dialogue.lines.Count > 1)
            {
                int previousSpeaker = dialogue.lines.Count - 3;
                line.character = dialogue.lines[previousSpeaker].character;
                Debug.Log(line.character);
            }
            

            // EditorUtility.SetDirty(dialogue);
            // EditorApplication.MarkSceneDirty();
        }
    }
}
