using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnyaMv : MonoBehaviour
{
    public Animator Anim;
    public Rigidbody2D rg;
    private float Speed = 0;
    public float MvSpeed;
    public float JumpHigh;
    private float move;
    //public Text WinText;

    //private bool DangLeoTuong;
    //[SerializeField] private float SpeedLeoTuong;

    //[SerializeField] private Transform WallCheck;
    //[SerializeField] private LayerMask WallLayer;
    //[SerializeField] private AudioClip JumpSound;

    private bool IsFacingRight = true;
    public bool IsGround;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    public void Update()
    {
        Anim.SetFloat("Speed", Speed);
        Anim.SetBool("IsGround", IsGround);
        move = Input.GetAxisRaw("Horizontal");
        Movement(move);

        if (Input.GetButtonDown("Jump") && IsGround)
        {
            Jump();
        }
        //LeoTuong();
    }

    void Movement(float move)       // giá trị move từ -1 đến 1
    {
        rg.linearVelocity = new Vector2(MvSpeed * move, rg.linearVelocity.y);
        Speed = Mathf.Abs(MvSpeed * move);

        // Kiểm tra đổi hướng
        if (IsFacingRight && move < 0 || !IsFacingRight && move > 0)
        {
            Flip();
        }
    }

    void Jump()
    {
        rg.linearVelocity = new Vector2(rg.linearVelocity.x, JumpHigh);
        Anim.SetBool("IsJumping", true);
        //SoundManager.instance.PlaySound(JumpSound);
    }
    public void JumpOff()
    {
        Anim.SetBool("IsJumping", false);
    }


    //private bool IsLeoTuong()
    //{
    //    return Physics2D.OverlapCircle(WallCheck.position, 0.2f, WallLayer);
    //}

    //private void LeoTuong()
    //{
    //    if (IsLeoTuong() && !IsGround && move != 0f)
    //    {
    //        DangLeoTuong = true;
    //        rg.velocity = new Vector2(rg.velocity.x, Mathf.Clamp(rg.velocity.y, -SpeedLeoTuong, float.MaxValue));
    //    }
    //}
    void Flip()
    {
        IsFacingRight = !IsFacingRight;
        Vector3 X = transform.localScale;
        X.x *= -1;
        transform.localScale = X;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        IsGround = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        IsGround = false;
    }
    public bool CanAtk()
    {
        return move == 0 && IsGround == true;
    }
    //private void OnTriggerEnter2D(Collider2D collision)

    //{
    //    if (collision.CompareTag("Win"))
    //    {
    //        WinText.gameObject.SetActive(true);
    //        Time.timeScale = 0;
    //    }
    //}
}
