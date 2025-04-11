using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoldierShieldAttack : MonoBehaviour
{
    [SerializeField] private float AtkCoolDown;
    [SerializeField] private int Dmg;
    [SerializeField] private float Range;
    [SerializeField] private float ColliderDistance;        //độ rộng của Collider
    [SerializeField] GameObject ProjectedPrefab;
    [SerializeField] private Transform ShootPoint;
    [SerializeField] private float bulletSpeed;
    //[SerializeField] private AudioClip SwordSound;
    private float CoolDownTimer = Mathf.Infinity;
    Rigidbody2D rg;
    Animator Anim;
    public BoxCollider2D Box;
    public LayerMask playerlayer;
    private AnyaHealth PlayerHealth;

    private SoldierShieldIdle EneMv;
    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        EneMv = GetComponentInParent<SoldierShieldIdle>();
    }
    void Update()
    {
        CoolDownTimer += Time.deltaTime;
        if (PlayerSight())
        {
            if (CoolDownTimer >= AtkCoolDown && PlayerHealth.CurrentHealth > 0)
            {
                CoolDownTimer = 0;
                Anim.SetTrigger("Atk");
                //SoundManager.instance.PlaySound(SwordSound);
            }
        }
        if (EneMv != null)
        {
            EneMv.enabled = !PlayerSight();         // di chuyển nếu người chơi không trong tầm nhìn
        }
    }
    private bool PlayerSight()      //tấn công khi người chơi trong tầm nhìn
    {
        RaycastHit2D hit = Physics2D.BoxCast(Box.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance,
            new Vector3(Box.bounds.size.x * Range, Box.bounds.size.y, Box.bounds.size.z), 0, Vector2.left, 0, playerlayer);
        if (hit.collider != null)
        {
            PlayerHealth = hit.transform.GetComponent<AnyaHealth>();
        }

        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(Box.bounds.center + transform.right * Range * transform.localScale.x * ColliderDistance,
            new Vector3(Box.bounds.size.x * Range, Box.bounds.size.y, Box.bounds.size.z));
    }
    private void ShootBullet()
    {
        if (PlayerSight()) // Chỉ bắn nếu người chơi trong tầm nhìn
        {
            // Tạo viên đạn tại ShootPoint
            GameObject bullet = Instantiate(ProjectedPrefab, ShootPoint.position, ShootPoint.rotation);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();

            float direction = Mathf.Sign(transform.localScale.x); // 1 nếu quay phải, -1 nếu quay trái

            // Bắn thẳng theo trục ngang (Vector2.right là trục x)
            bulletRb.linearVelocity = new Vector2(direction * bulletSpeed, 0f); // Bắn thẳng, không có thành phần y

            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x = Mathf.Abs(bulletScale.x) * direction; // Lật ngang dựa trên hướng
            bullet.transform.localScale = bulletScale;
        }
    }
}
