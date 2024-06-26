using DG.Tweening;
using UnityEngine;

public abstract class SkillVisualEffect : Poolable
{
    protected Sequence seq;

    public abstract void Apply(SkillEffect effect);

    public abstract float GetDuration();
}