using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndPanel : MainSceneUI
{
    enum Buttons
    {
        GoBackBtn,
    }
    private void Start()
    {
        Managers.UI.UIlist.Add(this);
        Managers.Game.GameOver += () => gameObject.SetActive(true);
        Init();
    }

    public override void Init()
    {
        Bind<Button>(typeof(Buttons));

        Get<Button>((int)Buttons.GoBackBtn).gameObject.BindEvent(GoBack);
    }

    private void GoBack(PointerEventData data)
    {
        Managers.Sound.Play(Sounds.Click);
        SceneManager.LoadScene((int)SceneType.StartScene);
    }
}