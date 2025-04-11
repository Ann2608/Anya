using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;

    public PlayerState playerState;

    [Header("Moving")]
    public float speedMove;

    [Header("Crouching")]
    public bool isCrouch;

    [Header("Jumping")]
    public bool isGround;
    public float jumpForce;

    [Header("Shooting")]
    public bool isShoot;
    public GameObject bulletPrefab;
    public Transform spawnPoint;
    public float forceBullet;

    [Header("Health Player")]
    public bool isDeath;
    public int health;
    public int healthMax;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        //Player moving
        Moving();

        //player jumping
        Jumping();

        //player crouching
        Crouching();

        //player shoot
        Shooting();

        //animator player changed state
        AnimationChanged();
    }
    void Moving()
    {
        //khi nguoi choi ban
        if (isShoot == true || isCrouch == true)
        {
            //player dungy en
            rb.linearVelocity = Vector2.zero;
        }
        //khi nguoi choi k ban
        else
        {
            //player di chuyen bth

            float moveX = Input.GetAxisRaw("Horizontal");
            Vector2 directionMove = new Vector2(moveX * speedMove, rb.linearVelocity.y);

            rb.linearVelocity = directionMove;

            if (moveX > 0)
            {
                transform.localScale = new Vector3(2, 2, 2);
            }
            if (moveX < 0)
            {
                transform.localScale = new Vector3(-2, 2, 2);
            }
        }
    }
    void Crouching()
    {
        // khi nguoi choi an nut S va nguoi choi khong ban, va dung tren mat dat
        if (Input.GetKeyDown(KeyCode.S) && isShoot == false && isGround == true)
        {
            isCrouch = true;
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            isCrouch = false;
        }
    }

    void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGround == true && isCrouch == false)
        {
            rb.linearVelocity = Vector2.zero;
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.J) && isGround == true)
        {
            isShoot = true;
        }
    }

    void AnimationChanged()
    {
        anim.SetInteger("state", (int)playerState);

        if (isDeath)
        {
            playerState = PlayerState.Death;
            return;
        }


        //khi nguoi choi ban sung
        if (isShoot == true)
        {
            playerState = PlayerState.Shoot;
        }
        //khi nguoi choi ngung ban
        else  //isShot = false
        {
            if (isGround == true)
            {
                if (isCrouch == true)
                {
                    //dang o trang thai ngoi
                    playerState = PlayerState.Crouch;
                }
                else
                {
                    // dang o tren mat dat
                    if (rb.linearVelocity.x == 0)
                    {
                        //nguoi choi dung yen
                        playerState = PlayerState.Idle;
                    }
                    else
                    {
                        //nguoi choi di chuyen
                        playerState = PlayerState.Run;
                    }
                }
            }
            else
            {
                //dang o tren khong
                if (rb.linearVelocity.y > 0)
                {
                    //nguoi choi dang nhay 
                    playerState = PlayerState.Jump;

                }
                else
                {
                    //nguoi choi dang roi
                    playerState = PlayerState.Fall;

                }
            }
        }
        //Animation Crouch

    }


    //Spawn Bullet
    public void SpawnBullet()
    {
        GameObject _bullet = Instantiate(bulletPrefab, spawnPoint.position, Quaternion.identity);

        Vector2 directionBullet = new Vector2(transform.localScale.x, 0);

        _bullet.GetComponent<Rigidbody2D>().AddForce(directionBullet * forceBullet, ForceMode2D.Impulse);
    }

    public void IdleShoot()
    {
        isShoot = false;
        playerState = PlayerState.Idle;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log(damage);
    }
    #region Save and load

    public void Save(ref PlayerSaveData saveData)
    {
        saveData.Position = transform.position;
    }
    public void Load(PlayerSaveData saveData)
    {
        transform.position = saveData.Position;
    }
    #endregion


    [System.Serializable]
    public struct PlayerSaveData
    {
        public Vector3 Position;
    }






}

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Fall,
    Shoot,
    Crouch,
    Death
}