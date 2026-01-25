using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class FindMissingScripts
{
    [MenuItem("Tools/Find Missing Scripts In Scene")]
    public static void FindMissingScriptsInScene()
    {
        int goCount = 0;
        int missingCount = 0;
        var rootGOs = SceneManager.GetActiveScene().GetRootGameObjects();
        foreach (var root in rootGOs)
        {
            var transforms = root.GetComponentsInChildren<Transform>(true);
            foreach (var t in transforms)
            {
                goCount++;
                var components = t.gameObject.GetComponents<Component>();
                for (int i = 0; i < components.Length; i++)
                {
                    if (components[i] == null)
                    {
                        missingCount++;
                        Debug.Log($"Missing script on: {GetGameObjectPath(t.gameObject)}", t.gameObject);
                    }
                }
            }
        }
        Debug.Log($"Searched {goCount} GameObjects, found {missingCount} missing components.");
    }

    static string GetGameObjectPath(GameObject go)
    {
        string path = go.name;
        Transform t = go.transform;
        while (t.parent != null)
        {
            t = t.parent;
            path = t.name + "/" + path;
        }
        return path;
    }
}