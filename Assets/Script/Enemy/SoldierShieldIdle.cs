using UnityEngine;

public class SoldierShieldIdle : MonoBehaviour
{
    [Header("Enemy")]
    [SerializeField] private Transform Enemy;

    [Header("Anim")]
    [SerializeField] private Animator Anim;

    private Vector3 initScale;

    private void Awake()
    {
        initScale = Enemy.localScale;
    }

    //private void OnDisable()
    //{
    //    Anim.SetBool("Moving", false);
    //}

    private void Update()
    {
        // Đặt trạng thái không di chuyển
        //Anim.SetBool("Moving", false);
        // Giữ nguyên vị trí và hướng ban đầu của Enemy
        Enemy.localScale = initScale;
    }
}