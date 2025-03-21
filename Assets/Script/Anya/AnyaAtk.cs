using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnyaAtk : MonoBehaviour
{
    [SerializeField] private float AtkCoolDown;
    [SerializeField] private Transform Firepoint;
    [SerializeField] private GameObject[] Bullet;
    public Transform AttackPoint;
    public float Range;
    public int AtkDmg;
    public LayerMask Enemylayer;
    Rigidbody2D rg;
    Animator Anim;
    private AnyaMv Anya;
    private float CoolDownTimer = Mathf.Infinity;
    //[SerializeField] private AudioClip SwordSound;
    void Awake()
    {
        Anim = GetComponent<Animator>();
        Anya = GetComponent<AnyaMv>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) && CoolDownTimer > AtkCoolDown && Anya.CanAtk())
            Attack();
        CoolDownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) && CoolDownTimer > AtkCoolDown && Anya.CanAtk())
            Attack2();
        CoolDownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.C) && CoolDownTimer > AtkCoolDown && Anya.CanAtk())
            Attack3();
        CoolDownTimer += Time.deltaTime;
    }
    private void Attack()
    {
        //SoundManager.instance.PlaySound(SwordSound);
        Anim.SetTrigger("Attack");
        CoolDownTimer = 0;
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, Range, Enemylayer);
        foreach (Collider2D hitEnemy in HitEnemy)
        {
            hitEnemy.GetComponent<HealthEnemy>().TakeDmg(AtkDmg);
        }
    }
    private void Attack2()
    {
        //SoundManager.instance.PlaySound(SwordSound);
        Anim.SetTrigger("Attack2");
        CoolDownTimer = 0;
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, Range, Enemylayer);
        foreach (Collider2D hitEnemy in HitEnemy)
        {
            hitEnemy.GetComponent<HealthEnemy>().TakeDmg(AtkDmg);
        }
    }
    private void Attack3()
    {
        //SoundManager.instance.PlaySound(SwordSound);
        Anim.SetTrigger("GunAttack");
        CoolDownTimer = 0;
        Bullet[Continuousshooting()].transform.position = Firepoint.position;
        Bullet[Continuousshooting()].GetComponent<Bullet>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int Continuousshooting()
    {
        for(int i = 0; i < Bullet.Length; i++)
        {
            if (!Bullet[i].activeInHierarchy)
                return i;
        }
        return 0;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }
}
