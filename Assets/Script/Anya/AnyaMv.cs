using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class AnyaMv : MonoBehaviour
{
    public Animator Anim;
    public Rigidbody2D rg;
    public BoxCollider2D Box;
    public ParticleSystem SpeedFX;      // ParticleSystem: hiệu ứng hạt

    private float Speed = 0;

    [Header("Speed")]
    public float MvSpeed;
    private float Speedboost = 1f;

    [Header("Interact")]
    [SerializeField] private GameObject NoteUI;
    [SerializeField] private Text NoteText;
    private bool CanInteract = false;       // kiểm tra xem người chơi có ở gần không
    private string NoteContent;

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
        Box = GetComponent<BoxCollider2D>();
        SpeedItem.OnSpeedChange += Startspeedboost;
        NoteUI.SetActive(false);
    }

    void Startspeedboost(float mutiplyer)       // mutiplyer là giá trị của Speedboost tức là giá trị tăng tốc
    {
        StartCoroutine(SpeedboostCouroutine(mutiplyer));
    }

    private IEnumerator SpeedboostCouroutine(float mutiplyer)
    {
        Speedboost = mutiplyer;
        SpeedFX.Play();
        yield return new WaitForSeconds(2f);
        Speedboost = 1f;
        SpeedFX.Stop();
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
        if (Input.GetKeyDown(KeyCode.E) && CanInteract)
        {
            StartCoroutine(ShowNote());
        }
        //LeoTuong();
    }
    IEnumerator ShowNote()
    {
        NoteText.text = NoteContent;
        NoteUI.SetActive(true);
        yield return new WaitForSeconds(5f);
        NoteUI.SetActive(false);
    }

    public void SetNoteContent(string content)
    {
        NoteContent = content;          // nội dung của Note
        CanInteract = true;
    }

    void Movement(float move)       // giá trị move từ -1 đến 1
    {
        rg.linearVelocity = new Vector2(MvSpeed * move * Speedboost, rg.linearVelocity.y);
        Speed = Mathf.Abs(MvSpeed * move * Speedboost);

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
        if (SpeedFX != null)
        {
            Vector3 fxScale = SpeedFX.transform.localScale;
            fxScale.x = Mathf.Abs(fxScale.x) * (IsFacingRight ? 1 : -1); // (IsFacingRight ? 1 : -1) xác định dấu của scale X dựa trên hướng nhân vật (dương nếu sang phải, âm nếu sang trái).
            // Mathf.Abs(fxScale.x) : lấy giá trị tuyệt đối của X, không thay đổi kích thước
            // dấu ? là toán tử 3 ngôi xác định true hoặc false
            SpeedFX.transform.localScale = fxScale;
        }
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
