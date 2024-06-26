using System;
using UnityEngine;

public class Bullets : MonoBehaviour, IAttackable
{
    Rigidbody rb;
    int opt = 0;
    float dy = 10f;
    float fallingSpeed = 2f;
    float fallingWidth = 2f;
    public string attackType { get;private set; }

    public float attackPower { get; private set; }
    private void Start()
    {
        Init();

    }
    private void OnEnable()
    {
        Init();
    }
    private void Init()
    {
        rb = GetComponent<Rigidbody>();
        CalcPosVel();
        attackType = nameof(AttackType.Normal);
        attackPower = 1;

        Managers.Sound.Play(Sounds.Bullet);
    }

    private void CalcPosVel()
    {
        Debug.Log(Managers.Game.Player == null);
        if (Managers.Game.Player.transform.position.y + dy > Util.FindChild<Transform>(Managers.Game.map, nameof(Tags.EndLine)).position.y)
            return;
        else
        {
            transform.position = new Vector3(
                Managers.Game.Player.transform.position.x + UnityEngine.Random.Range(-fallingWidth,fallingWidth),
                Managers.Game.Player.transform.position.y + dy,
                1);
        }
        Vector3 mapVel = Managers.Game.map.GetComponent<Rigidbody>().velocity;
        rb.velocity = new Vector3(mapVel.x,mapVel.y - fallingSpeed, mapVel.z);
    }
    
    public bool Attack(IDamagable target, float power, int numberOfAttacks)
    {
        return false;
    }
}