using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyerAtk : MonoBehaviour
{
    [SerializeField] private float AtkCoolDown;
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


    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        if (Anim == null)
        {
            Debug.LogError("Animator component is missing!", this);
        }
    }
    void Update()
    {
        CoolDownTimer += Time.deltaTime;
        if (PlayerSight())
        {
            Debug.Log("Player detected!");
            if (CoolDownTimer >= AtkCoolDown && PlayerHealth.CurrentHealth > 0)
            {
                Debug.Log("Triggering Atk animation!");
                CoolDownTimer = 0;
                Anim.SetTrigger("Atk");
                //SoundManager.instance.PlaySound(SwordSound);
            }
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
            bulletRb.velocity = new Vector2(direction * bulletSpeed, 0f); // Bắn thẳng, không có thành phần y

            Vector3 bulletScale = bullet.transform.localScale;
            bulletScale.x = Mathf.Abs(bulletScale.x) * direction; // Lật ngang dựa trên hướng
            bullet.transform.localScale = bulletScale;
        }
    }
}
