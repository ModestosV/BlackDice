﻿using UnityEngine;
using UnityEditor;
/// <summary>
/// Allows users to navigate in the Unity Editor to Window -> FindMissingScriptsRecursively to help with tracking
/// GameObjects that reference missing scripts.
/// Code found at: http://wiki.unity3d.com/index.php?title=FindMissingScripts
/// </summary>
public class FindMissingScriptsRecursively : EditorWindow
{
    static int go_count = 0, components_count = 0, missing_count = 0;

    [MenuItem("Window/FindMissingScriptsRecursively")]
    public static void ShowWindow()
    {
        GetWindow(typeof(FindMissingScriptsRecursively));
    }

    public void OnGUI()
    {
        if (GUILayout.Button("Find Missing Scripts in selected GameObjects"))
        {
            FindInSelected();
        }
    }
    private static void FindInSelected()
    {
        GameObject[] go = Selection.gameObjects;
        go_count = 0;
        components_count = 0;
        missing_count = 0;
        foreach (GameObject g in go)
        {
            FindInGO(g);
        }
        Debug.Log($"Searched {go_count} GameObjects, {components_count} components, found {missing_count} missing");
    }

    private static void FindInGO(GameObject g)
    {
        go_count++;
        Component[] components = g.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            components_count++;
            if (components[i] == null)
            {
                missing_count++;
                string s = g.name;
                Transform t = g.transform;
                while (t.parent != null)
                {
                    var parent = t.parent;
                    s = parent.name + "/" + s;
                    t = parent;
                }
                Debug.Log(s + " has an empty script attached in position: " + i, g);
            }
        }
        // Now recurse through each child GO (if there are any):
        foreach (Transform childT in g.transform)
        {
            //Debug.Log("Searching " + childT.name  + " " );
            FindInGO(childT.gameObject);
        }
    }
}