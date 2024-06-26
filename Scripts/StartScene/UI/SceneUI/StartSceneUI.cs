using System.Collections;
using UnityEngine;

public class StartSceneUI : UIBase
{
    protected enum Panels
    {
        StartPanel,
        StagePanel,
        InfoPanel,
        ExitPanel,
    }
    private void Start()
    {
        Init();
    }
    public override void Init()
    {
        Managers.Sound.Play(Sounds.BGM);
        Bind<GameObject>(typeof(Panels));
        StartCoroutine(Initialize());
    }

    IEnumerator Initialize()
    {
        Util.GetOrAddComponent<CanvasGroup>(gameObject).alpha = 0f;
        yield return new WaitForEndOfFrame();
        GetComponent<CanvasGroup>().alpha = 1f;
        Get<GameObject>((int)Panels.StagePanel).SetActive(false);
        Get<GameObject>((int)Panels.InfoPanel).SetActive(false);
        Get<GameObject>((int)Panels.ExitPanel).SetActive(false);
    }
}
