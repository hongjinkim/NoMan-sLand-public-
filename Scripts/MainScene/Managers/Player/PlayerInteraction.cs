using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class PlayerInteraction : MonoBehaviour,IDamagable
{
    Rigidbody rb;
    Image healthBar;
    public float Health { get; private set; }
    float maxHealth;
    bool invincibility;
    float invincibilityTime;

    public string armorTypeName { get; private set; }

    public float armorValue { get; private set; }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        rb = GetComponent<Rigidbody>();
        armorTypeName = nameof(ArmorType.Small);
        healthBar = Util.FindChild<Image>(gameObject, "Health");
        maxHealth = 5;
        Health = maxHealth;
        armorValue = 0;
        invincibility = false;
        invincibilityTime = 1f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(nameof(Tags.EndLine)))
            Managers.Game.CallGameClearLogic();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag(nameof(Tags.Bullets)))
        {
            StartCoroutine(ApplyDamage(collision.gameObject.GetComponent<Bullets>().attackPower));
            Managers.Pool.Push(collision.gameObject);
//            Destroy(collision.gameObject);
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        if (!invincibility && collision.gameObject.CompareTag(nameof(Tags.Wire)))
            StartCoroutine(ApplyDamage(collision.gameObject.GetComponent<Wire>().DMG));

    }
    public IEnumerator ApplyDamage(float dmg)
    {
        Health = Mathf.Max(Health - dmg, 0);
        switch (UnityEngine.Random.Range(0,3))
        {
            case 0:
                Managers.Sound.Play(Sounds.HitOn1,true);
                break;
            case 1:
                Managers.Sound.Play(Sounds.HitOn2,true);
                break;
            case 2:
                Managers.Sound.Play(Sounds.HitOn3,true);
                break;
        }
        if (Health == 0)
            Die();
        healthBar.fillAmount = Health / maxHealth;
        healthBar.color = Color.white;
        invincibility = true;
        yield return new WaitForSeconds(invincibilityTime);
        healthBar.color = new Color(255 / 255f, 73 / 255f, 73 / 255f, 255 / 255f);
        invincibility = false;
    }
    public void Die()
    {
        if (Health == 0)
            Managers.Game.CallGameOverLogic();
    }
}