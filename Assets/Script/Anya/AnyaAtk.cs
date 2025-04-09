using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AnyaAtk : MonoBehaviour
{
    [SerializeField] private float AtkCoolDown;
    [SerializeField] private Transform Firepoint;
    [SerializeField] private GameObject[] Bullet;
    [SerializeField] private Transform SwordPoint;
    [SerializeField] private GameObject SlashEffect;
    Rigidbody2D rg;
    Animator Anim;



    public LayerMask Enemylayer;
    private AnyaMv Anya;

    [Header("Sword")]
    private bool HasSword;

    [Header("Atk")]
    public Transform AttackPoint;
    public float Range;
    public int AtkDmg;
    private int BaseAtk;
    private float CoolDownTimer = Mathf.Infinity;


    //[SerializeField] private AudioClip SwordSound;
    void Awake()
    {
        Anim = GetComponent<Animator>();
        Anya = GetComponent<AnyaMv>();
        
    }
    private void Start()
    {
        Anim.SetBool("HasSword", HasSword);
        BaseAtk = AtkDmg;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < 0) // Nhân vật quay sang trái
        {
            SwordPoint.localRotation = Quaternion.Euler(0, 180, 0); // Xoay 180 độ quanh trục Y
        }
        else // Nhân vật quay sang phải
        {
            SwordPoint.localRotation = Quaternion.Euler(0, 0, 0); // Không xoay
        }
        CoolDownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Z) && CoolDownTimer > AtkCoolDown && Anya.CanAtk())
        {
            SwordAttack();
        }
        CoolDownTimer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.X) && CoolDownTimer > AtkCoolDown && Anya.CanAtk())
            Attack3();
        CoolDownTimer += Time.deltaTime;
    }
    private void SwordAttack()
    {
        //SoundManager.instance.PlaySound(SwordSound);
        Anim.SetTrigger("SwordAttack");
        CoolDownTimer = 0;
        Collider2D[] HitEnemy = Physics2D.OverlapCircleAll(AttackPoint.position, Range, Enemylayer);
        if (HasSword)
        {
            Anim.SetTrigger("SwordAttack"); // Tấn công bằng kiếm
            Instantiate(SlashEffect, SwordPoint.position, SwordPoint.rotation);
        }
        else
        {
            Anim.SetTrigger("Attack"); // Đấm tay không
        }

        foreach (Collider2D hitEnemy in HitEnemy)
        {
            hitEnemy.GetComponent<HealthEnemy>().TakeDmg(AtkDmg);
        }
    }
    public void EquipSword()
    {
        HasSword = true;
        Anim.SetBool("HasSword", true); // Chuyển sang trạng thái cầm kiếm
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
    public void AttackIncrease(int DmgBoost)
    {
        AtkDmg += DmgBoost;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackPoint.position, Range);
    }
}
