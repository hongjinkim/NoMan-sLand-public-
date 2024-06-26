using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SmokeVisualEffect : SkillVisualEffect
{
    public GameObject rangeEffect;
    public GameObject shellEffect;
    public GameObject smokeEffect;

    GameObject smoke;

    public override void Apply(SkillEffect effect)
    {
        if(smoke == null)
        {
            smoke = Managers.Resource.Load<GameObject>("Skill/VisualEffect/Smoke");
            Util.GetOrAddComponent<Poolable>(smoke);
            Managers.Pool.CreatePool(smoke);
        }
        StartCoroutine(SmokeApply());
    }

    public override float GetDuration()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator SmokeApply()
    {
        smokeEffect.SetActive(false);
        shellEffect.transform.rotation = Quaternion.identity;

        var playerPos = Managers.Game.Player.transform.position;
        rangeEffect.transform.position = new Vector3(playerPos.x, playerPos.y + 3, playerPos.z);

        seq = DOTween.Sequence()
            .Append(shellEffect.transform.DOMove(rangeEffect.transform.position, 2.5f).From(playerPos).SetEase(Ease.OutCubic))
                .OnComplete(() =>
                {
                    smokeEffect.transform.position = rangeEffect.transform.position;
                    smokeEffect.SetActive(true);
                    Managers.Sound.Play(Sounds.Smoke);
                })
            .Join(shellEffect.transform.DOScale(2.5f, 0.5f))
            .Join(shellEffect.transform.DORotate(new Vector3(90, 330, 90), 2.5f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic))
            .Insert(0.5f, shellEffect.transform.DOScale(1f, 1.8f));

        yield return new WaitForSeconds(6.0f);

        ObjectPool.instance.Despawn(gameObject);
    }
}