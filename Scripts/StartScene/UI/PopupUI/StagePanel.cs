using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StagePanel : StartSceneUI
{
    enum Buttons
    {
        GoBackBtn,
        Lvl0Btn,
        Lvl1Btn,
        Lvl2Btn,
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
        Get<Button>((int)Buttons.Lvl0Btn).gameObject.BindEvent(LoadLvl0Scene);
        Get<Button>((int)Buttons.Lvl1Btn).gameObject.BindEvent(LoadLvl1Scene);
        Get<Button>((int)Buttons.Lvl2Btn).gameObject.BindEvent(LoadLvl2Scene);
    }


    private void GoBack(PointerEventData data)
    {
        Managers.UI.FindUI<StartPanel>().gameObject.SetActive(true);
        Managers.Sound.Play(Sounds.Click);
        gameObject.SetActive(false);
    }
    private void LoadLvl0Scene(PointerEventData data)
    {
        Managers.Data.SetLvl(0);
        Managers.Sound.Play(Sounds.Click);
        SceneManager.LoadScene((int)SceneType.MainScene);
    }

    private void LoadLvl1Scene(PointerEventData data)
    {
        Managers.Data.SetLvl(1);
        Managers.Sound.Play(Sounds.Click);
        SceneManager.LoadScene((int)SceneType.MainScene);
    }

    private void LoadLvl2Scene(PointerEventData data)
    {
        Managers.Data.SetLvl(2);
        Managers.Sound.Play(Sounds.Click);
        SceneManager.LoadScene((int)SceneType.MainScene);
    }
}
