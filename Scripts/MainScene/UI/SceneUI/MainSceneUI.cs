using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MainSceneUI : UIBase
{
    enum Panels
    {
        StoryPanel,
        ClearPanel,
        EndPanel,
    }
    enum Buttons
    {
        Skill0,
        Skill1,
        Skill2,
        Skill3,
    }
    enum Images
    {
        GasMask
    }
    enum Sliders
    {
        Slider
    }
    private void Start()
    {
        Managers.UI.UIlist.Add(this);
        Init();
    }
    public override void Init()
    {
        Bind<GameObject>(typeof(Panels));
        Bind<Button>(typeof(Buttons));
        Bind<Image>(typeof(Images));
        Bind<Slider>(typeof(Sliders));

        for (int i = 0; i < Enum.GetNames(typeof(Buttons)).Length; i++)
        {
            Get<Button>(i).gameObject.BindEvent(Managers.Game.skills[i]);
        }

        StartCoroutine(Initialize());
        Time.timeScale = 0f;
    }

    IEnumerator Initialize()
    {
        Util.GetOrAddComponent<CanvasGroup>(gameObject).alpha = 0f;
        yield return new WaitForEndOfFrame();
        GetComponent<CanvasGroup>().alpha = 1f;
        Get<GameObject>((int)Panels.ClearPanel).SetActive(false);
        Get<GameObject>((int)Panels.EndPanel).SetActive(false);
        Get<Image>((int)Images.GasMask).gameObject.SetActive(false);
    }
}