using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slider : UIBase
{
    Transform StartLine;
    Transform EndLine;
    [SerializeField] UnityEngine.UI.Slider slider;

    void Start()
    {
        Managers.UI.UIlist.Add(this);
        Init();
    }
    public override void Init()
    {
        StartLine = Util.FindChild<Transform>(Managers.Game.map, nameof(Tags.StartLine));
        EndLine = Util.FindChild<Transform>(Managers.Game.map, nameof(Tags.EndLine));
        slider = GetComponent<UnityEngine.UI.Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = 
            (Managers.Game.Player.transform.position.y - StartLine.position.y) /
            (EndLine.position.y - StartLine.position.y);
    }
}
