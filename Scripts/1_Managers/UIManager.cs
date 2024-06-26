using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UIManager
{
    public List<UIBase> UIlist = new List<UIBase>();
    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("@UIRoot");
            if (root == null)
                root = new GameObject { name = "@UIRoot" };
            return root;
        }
    }
    public T ShowUI<T>(string name = null,string path = null) where T : UIBase
    {
        if (string.IsNullOrEmpty(name))
            name = nameof(T);
        GameObject go = Managers.Resource.Instantiate($"UI/{name}");
        T ui = Util.GetOrAddComponent<T> (go);
        UIlist.Add(ui);
        go.transform.SetParent(Root.transform);
        return ui;
    }
    public T ToggleUI<T>() where T : UIBase
    {
        T ui = FindUI<T>();
        if (ui != null)
        {
            FindUI<T>().gameObject.SetActive(!FindUI<T>().gameObject.activeSelf);
            return ui;
        }
        else
            return null;
    }
    public T FindUI<T>() where T : UIBase
    {
        return UIlist.Where(x => x.GetType() == typeof(T)).FirstOrDefault() as T;
    }
}
