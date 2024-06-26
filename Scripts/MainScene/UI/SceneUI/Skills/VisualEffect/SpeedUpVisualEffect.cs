using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUpVisualEffect : SkillVisualEffect
{
    public override void Apply(SkillEffect effect)
    {
        StartCoroutine(SpeedUpApply(effect));
    }

    public override float GetDuration()
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator SpeedUpApply(SkillEffect effect)
    {
        effect.Apply();
        yield return new WaitForSeconds(3.0f);

        ObjectPool.instance.Despawn(gameObject);
    }
}
