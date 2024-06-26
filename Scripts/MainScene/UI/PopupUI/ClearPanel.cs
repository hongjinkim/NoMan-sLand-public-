using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearPanel : MainSceneUI
{
    enum Buttons
    {
        GoBackBtn,
    }

    private void Start()
    {
        Managers.UI.UIlist.Add(this);
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