using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Skill : MonoBehaviour
{
    public PointerEventData action;
    public Sprite image;
    public SkillData data;
    public List<SkillEffect> effects;

    public void SetCoolTime(float time)
    {
        data.coolTime = time;
    }

    private void SetSkill(int idx)
    {
        Managers.Game.skills[idx] = UseSkill;
    }
    private void UseSkill(PointerEventData data)
    {
        var effects = this.effects;
        for(int i=0; i<effects.Count; i++)
        {
            var ob = Managers.Pool.Pop(effects[i].gameObject,transform);

            var visualEffect = ob.GetComponent<SkillVisualEffect>();
            visualEffect.Apply(effects[i]);
//            return Managers.Game.skills[i];
        }
//        return null;
    }
}
