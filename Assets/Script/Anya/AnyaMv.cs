using UnityEngine;
using static PlayerController;

public class AnyaMv : MonoBehaviour, IPlayerSavable
{
    public Animator Anim;
    public Rigidbody2D rg;
    private float Speed = 0;
    public float MvSpeed;
    public float JumpHigh;
    private float move;
    private bool IsFacingRight = true;
    public bool IsGround;

    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        Anim.SetFloat("Speed", Speed);
        Anim.SetBool("IsGround", IsGround);
        move = Input.GetAxisRaw("Horizontal");
        Movement(move);

        if (Input.GetButtonDown("Jump") && IsGround)
        {
            Jump();
        }
    }

    void Movement(float move)
    {
        rg.linearVelocity = new Vector2(MvSpeed * move, rg.linearVelocity.y);
        Speed = Mathf.Abs(MvSpeed * move);

        if (IsFacingRight && move < 0 || !IsFacingRight && move > 0)
        {
            Flip();
        }
    }

    void Jump()
    {
        rg.linearVelocity = new Vector2(rg.linearVelocity.x, JumpHigh);
        Anim.SetBool("IsJumping", true);
    }

    public void JumpOff()
    {
        Anim.SetBool("IsJumping", false);
    }

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

    // --- Save / Load ---
    public void Save(ref PlayerSaveData data)
    {
        data.Position = transform.position;
    }

    public void Load(PlayerSaveData data)
    {
        transform.position = data.Position;
    }
}
