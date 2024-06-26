using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoryPanel : MainSceneUI
{
    enum Buttons
    {
        EnterBtn,
    }
    private void Start()
    {
        Managers.UI.UIlist.Add(this);
        Init();
    }
    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.EnterBtn).gameObject.BindEvent(EnterGame);
    }

    private void EnterGame(PointerEventData data)
    {
        Managers.Sound.Play(Sounds.Click);
        Managers.Game.StartGame();
        gameObject.SetActive(false);
    }
}
