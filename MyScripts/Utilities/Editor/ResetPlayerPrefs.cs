using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ResetPlayerPrefs : EditorWindow
{
    [MenuItem("Edit/Reset Playerprefs")]
    public static void DeletePlayerPrefs()
    {
        Debug.Log("Deleted all PlayerPrefs");
        PlayerPrefs.DeleteAll();
    }
}
