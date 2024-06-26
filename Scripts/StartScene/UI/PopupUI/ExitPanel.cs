using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ExitPanel : StartSceneUI
{
    enum Buttons
    {
        ExitBtn,
        GoBackBtn
    }
    private void Start()
    {
        Managers.UI.UIlist.Add(this);
        Init();
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.ExitBtn).gameObject.BindEvent(Exit);
        Get<Button>((int)Buttons.GoBackBtn).gameObject.BindEvent(GoBack);
    }

    private void Exit(PointerEventData data)
    {
        Managers.Sound.Play(Sounds.Click);
        Application.Quit();
    }

    private void GoBack(PointerEventData data)
    {
        Managers.UI.FindUI<StartPanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }
}
