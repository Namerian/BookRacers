using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

using UnityEngine;

[CustomEditor(typeof(GameController))]
public class GameControllerEditor : Editor
{
#if UNITY_EDITOR
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        GameController gameController = (GameController)target;
        if (GUILayout.Button("Save Data"))
        {
            gameController.SaveData();
        }
    }
#endif
}
