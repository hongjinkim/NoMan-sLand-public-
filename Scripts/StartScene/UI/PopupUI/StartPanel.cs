using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartPanel : StartSceneUI
{
    enum Buttons
    {
        StartBtn,
        InfoBtn,
        ExitBtn
    }

    void Start()
    {
        Init();
    }
    public override void Init()
    {
        Managers.UI.UIlist.Add(this);
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.StartBtn).gameObject.BindEvent(StartBtn);
        Get<Button>((int)Buttons.InfoBtn).gameObject.BindEvent(InfoBtn);
        Get<Button>((int)Buttons.ExitBtn).gameObject.BindEvent(ExitBtn);
    }

    private void StartBtn(PointerEventData data)
    {
        Managers.UI.FindUI<StagePanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }
    private void InfoBtn(PointerEventData data)
    {
        Managers.UI.FindUI<InfoPanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }
    private void ExitBtn(PointerEventData data)
    {
        Managers.UI.FindUI<ExitPanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }
}
