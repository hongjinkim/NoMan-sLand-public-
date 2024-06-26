using UnityEngine;

public class Util
{
    public static T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T comp = go.GetComponent<T>();
        if(comp == null)
            comp = go.AddComponent<T>();
        return comp;
    }

    public static GameObject FindChild(GameObject go, string name = null)
    {
        Transform tf = FindChild<Transform>(go, name);
        if (tf == null)
            return null;
        return tf.gameObject;
    }
    public static T FindChild<T>(GameObject go, string name = null) where T : Object
    {
        if(go==null) return null;
        foreach(T comp in go.GetComponentsInChildren<T>())
        {
            if(string.IsNullOrEmpty(name) || comp.name == name)
                return comp;
        }
        return null;
    }
}
