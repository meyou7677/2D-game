using Unity.VisualScripting.FullSerializer;
using UnityEditor;
using UnityEngine;

public static class DebugTools
{
    [MenuItem("Tools/deleteplayerprefs")]
    private static void deleteplayerprefs()
    {
        PlayerPrefs.DeleteAll();
        Debug.Log("playerprefsdeleted");
    }
}
