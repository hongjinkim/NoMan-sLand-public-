using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BuffEffect : SkillEffect
{
    public BuffType buffType;
    public float speedChangeValue;
    public float speedChangeDurationValue;
    public override void Apply()
    {
        if(buffType == BuffType.SPEED_UP)
        {
            StartCoroutine(ChangeSpeed());
        }
    }

    private IEnumerator ChangeSpeed()
    {
        Managers.Game.controller.ChangeSpeed(3f);
        yield return new WaitForSeconds(speedChangeDurationValue);
        Managers.Game.controller.ChangeSpeed(1f);
    }
}