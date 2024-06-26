using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

public class CallTankVisualEffect : SkillVisualEffect
{
    float dy = 3f;
    float dTime = 3f;
    public override void Apply(SkillEffect effect)
    {
        StartCoroutine(CallTank());
        Managers.Sound.Play(Sounds.TankOn);
    }

    public override float GetDuration()
    {
        throw new NotImplementedException();
    }

    private IEnumerator CallTank()
    {
        var playerPos = Managers.Game.Player.transform.position;
        transform.position = new UnityEngine.Vector3(playerPos.x, playerPos.y + dy, playerPos.z);
        yield return new WaitForSeconds(dTime);
        ObjectPool.instance.Despawn(gameObject);
        Managers.Sound.Stop(Sounds.TankOn);
    }
}