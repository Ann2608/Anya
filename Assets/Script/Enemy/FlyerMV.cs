using UnityEngine;

public class FlyerMV : MonoBehaviour
{
    [Header("Patrol Point")]
    [SerializeField] private Transform LeftMv;
    [SerializeField] private Transform RightMv;

    [Header("Enemy")]       //Header: cho đẹp
    [SerializeField] private Transform Enemy;

    [Header("Mv para")]
    [SerializeField] private float Speed;
    private bool MvLeft;

    [Header("Idle")]
    [SerializeField] private float IdleDuration;            //thời gian chờ
    private float IdleTime;

    [Header("Anim")]
    [SerializeField] private Animator Anim;

    private Vector3 initScale;      // hàm quay về vị trí ban đầu

    private void Awake()
    {
        initScale = Enemy.localScale;
    }

    //private void OnDisable()            // vô hiệu hoá hoặc trước khi bị Destroy
    //{
    //    Anim.SetBool("Moving", false);
    //}

    private void Update()
    {
        if (MvLeft)
        {
            if (Enemy.position.x >= LeftMv.position.x)      // đã đứng bên trái
                Mv(-1); // -1 quay sang trái  1 quay sang phải
            else
                ChangeDirection();
        }
        else
        {
            if (Enemy.position.x <= RightMv.position.x)       // đã đứng bên phải
                Mv(1); // -1 quay sang trái  1 quay sang phải
            else
                ChangeDirection();
        }

    }

    private void ChangeDirection()
    {

        Anim.SetBool("Moving", false);
        IdleTime += Time.deltaTime;
        if (IdleTime > IdleDuration)
        {
            MvLeft = !MvLeft;
        }
    }
    private void Mv(int Direction)
    {
        IdleTime = 0;
        Anim.SetBool("Moving", true);
        // di chuyển qua lại
        Enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * Direction, initScale.y, initScale.z);


        // di chuyển
        Enemy.position = new Vector3(Enemy.position.x + Time.deltaTime * Direction * Speed, Enemy.position.y, Enemy.position.z);
    }
    public void SetEnemy(Transform newEnemy)
    {
        Enemy = newEnemy;
        if (Enemy != null)
        {
            initScale = Enemy.localScale;
            enabled = true; // Bật lại script nếu có Enemy mới
        }
        else
        {
            enabled = false; // Tắt script nếu Enemy bị hủy
        }
    }
}
