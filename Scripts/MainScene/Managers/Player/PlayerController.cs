using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Scripting.APIUpdating;

public class PlayerController : MonoBehaviour
{
    Rigidbody rb;
    public float speed { get; private set; }
    private Vector2 dir;

    public List<GameObject> skills;

    private bool leftTouch = false;
    private bool rightTouch = false;
    private void Awake()
    {
        Init();
    }
    private void FixedUpdate()
    {
        Move();
    }

    public void ChangeSpeed(float value)
    {
        speed = value;
    }
    private void Init()
    {
        speed = 2f;
        Managers.Game.controller = this;
        rb = GetComponent<Rigidbody>();

        Managers.Game.inputManager.OnStartTouch0 += TouchStart0;
        Managers.Game.inputManager.OnEndTouch0 += TouchEnd0;
        Managers.Game.inputManager.OnStartTouch1 += TouchStart1;
        Managers.Game.inputManager.OnEndTouch1 += TouchEnd1;
    }

    private void Move()
    {
        if(leftTouch && rightTouch) dir = Vector2.zero;
        else if(leftTouch)          dir = Vector2.left;
        else if(rightTouch)         dir = Vector2.right;
        else                        dir = Vector2.zero;       
        rb.velocity = dir * speed;
    }
    public void TouchStart0(Vector2 position, float time)
    {
        SetTouchState(position, true);
    }

    private void TouchEnd0(Vector2 position, float time)
    {
        SetTouchState(position, false);
    }

    private void TouchStart1(Vector2 position, float time)
    {
        SetTouchState(position, true);
    }

    private void TouchEnd1(Vector2 position, float time)
    {
        SetTouchState(position, false);
    }
    private void SetTouchState(Vector2 position, bool v)
    {
        if (position.x < Screen.width / 2)
            leftTouch = v;
        else
            rightTouch = v;
    }
    public Skill UseSkill(int skillNum)
    {
        var skill = skills[skillNum].GetComponent<Skill>();
        var effects = skill.effects;


        for (int i = 0; i < effects.Count; i++)
        {
            var ob = ObjectPool.instance.Spawn(effects[i].effectName);

            var visualEffect = ob.GetComponent<SkillVisualEffect>();
            visualEffect.Apply(effects[i]);
        }

        return skill;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))            leftTouch = true;
        else if (Input.GetKeyUp(KeyCode.A))         leftTouch = false;
        if (Input.GetKeyDown(KeyCode.D))            rightTouch = true;
        else if (Input.GetKeyUp(KeyCode.D))         rightTouch = false;

        // Skill shortcuts
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            UseSkill(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            UseSkill(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            UseSkill(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            UseSkill(3);
        }

    }
}
