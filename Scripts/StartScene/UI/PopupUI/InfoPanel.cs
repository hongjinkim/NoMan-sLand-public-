using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InfoPanel : StartSceneUI
{
    enum Buttons
    {
        StartBtn,
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

        Get<Button>((int)Buttons.StartBtn).gameObject.BindEvent(StartBtn);
        Get<Button>((int)Buttons.GoBackBtn).gameObject.BindEvent(GoBack);
    }

    private void StartBtn(PointerEventData data)
    {
        Managers.UI.FindUI<StagePanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }
    private void GoBack(PointerEventData data)
    {
        Managers.UI.FindUI<StartPanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }

}
