using UnityEngine;

public class SoldierShieldIdle : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Transform Enemy;

    [Header("Anim")]
    [SerializeField] private Animator Anim;

    [Header("Flip")]
    private SpriteRenderer Sprite;
    public float flipTime = 5f; // Thời gian giữa mỗi lần flip (giây)
    private float timer;
    private bool facingRight = true;

    void Start()
    {
        timer = flipTime;
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            // Flip hướng
            facingRight = !facingRight;
            Vector3 scale = transform.localScale;
            scale.x = facingRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
            transform.localScale = scale;

            timer = flipTime; // Reset đồng hồ
        }
    }

}