using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DropBoomVisualEffect : SkillVisualEffect
{
    GameObject boom;
    public override void Apply(SkillEffect effect)
    {
        if(boom == null)
        {
            boom = Managers.Resource.Load<GameObject>("Skill/VisualEffect/Boom");
            Util.GetOrAddComponent<Poolable>(boom);
            Managers.Pool.CreatePool(boom);
        }
        StartCoroutine(Boom());
    }

    public override float GetDuration()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator Boom()
    {
        for (int i = 0; i < 5; i++)
        {
            var ob = ObjectPool.instance.Spawn("Boom");
            ob.transform.position = GetRandomSpawnPosition();

            seq = DOTween.Sequence()
                .Append(ob.transform.DOScale(1f, 0.5f).SetEase(Ease.OutExpo))
                .AppendInterval(0.3f)
                .AppendCallback(() =>
                {
                    ob.transform.localScale = Vector3.one * 0.1f;
                    ObjectPool.instance.Despawn(ob);
                    Managers.Sound.Play(Sounds.Boom);
                });

            yield return new WaitForSeconds(1.0f);
        }
    }


    private Vector3 GetRandomSpawnPosition()
    {
        float randomX = Random.Range(0, Screen.width);
        float randomY = Random.Range(400, Screen.height);

        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(new Vector3(randomX, randomY, Camera.main.nearClipPlane));
        worldPosition.z = 0;

        return worldPosition;
    }
}
