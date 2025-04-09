using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class SlashEffect : MonoBehaviour
{
    [SerializeField] private float DmgSword;
    private AnyaAtk anyaatk;
    private Rigidbody2D rg;
    private Animator Anim;

    private void Start()
    {
        anyaatk = GameObject.FindObjectOfType<AnyaAtk>().GetComponent<AnyaAtk>();
        rg = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HealthEnemy enemyHealth = collision.GetComponent<HealthEnemy>();
        if (enemyHealth != null)
        {
            enemyHealth.TakeDmg((int)DmgSword);
        }
    }
    public void DestroySeft()
    {
        Destroy(gameObject);
    }
}
