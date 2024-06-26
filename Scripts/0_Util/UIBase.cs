using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Object = UnityEngine.Object;

public abstract class UIBase : MonoBehaviour
{
    protected Dictionary<Type, Object[]> objects = new Dictionary<Type, Object[]>();
    public abstract void Init();
    void Start()
    {
        Init();
        Managers.UI.UIlist.Add(this);
    }

    protected void Bind<T>(Type type) where T : Object
    {
        string[] names = Enum.GetNames(type);
        Object[] objs = new Object[names.Length];
        objects.Add(typeof(T), objs);

        for(int i=0; i<names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objs[i] = Util.FindChild(gameObject, names[i]);
            else
                objs[i] = Util.FindChild<T> (gameObject, names[i]);
            if (objs[i] == null)
                Debug.Log($"Failed to bind {names[i]}");
        }
    }

    protected T Get<T>(int idx) where T : Object
    {
        Object[] objs = null;
        if(!objects.TryGetValue(typeof(T), out objs))
            return null;
        return objs[idx] as T;
    }
    public static void BindEvent(GameObject go, Action<PointerEventData> action, UIEvent type = UIEvent.Click)
    {
        UIEventHandler evt = Util.GetOrAddComponent<UIEventHandler>(go);
        evt.OnPointerHandler[(int)type] -= action;
        evt.OnPointerHandler[(int)type] += action;
    }
}

public class UIEventHandler : MonoBehaviour,
    IPointerClickHandler,
    IPointerDownHandler,
    IPointerUpHandler,
    IBeginDragHandler,
    IDragHandler,
    IEndDragHandler
{
    public Action<PointerEventData>[] OnPointerHandler = new Action<PointerEventData>[6] {null,null,null,null,null,null};
    public void OnPointerClick(PointerEventData eventData)
    {
        if(OnPointerHandler[(int)UIEvent.Click] != null) 
            OnPointerHandler[(int)UIEvent.Click].Invoke(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(OnPointerHandler[(int)UIEvent.PointerDown] != null) 
            OnPointerHandler[(int)UIEvent.PointerDown].Invoke(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(OnPointerHandler[(int)UIEvent.PointerUp] != null) 
            OnPointerHandler[(int)UIEvent.PointerUp].Invoke(eventData);
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(OnPointerHandler[(int)UIEvent.BeginDrag] != null) 
            OnPointerHandler[(int)UIEvent.BeginDrag].Invoke(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(OnPointerHandler[(int)UIEvent.Drag] != null) 
            OnPointerHandler[(int)UIEvent.Drag].Invoke(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if(OnPointerHandler[(int)UIEvent.EndDrag] != null) 
            OnPointerHandler[(int)UIEvent.EndDrag].Invoke(eventData);
    }

}
