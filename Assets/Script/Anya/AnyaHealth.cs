using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnyaHealth : MonoBehaviour
{
    [Header("Health")]
    public int MaxHealth;
    public float CurrentHealth { get; private set; }
    private bool Dead;

    [Header("Iframe")]
    [SerializeField] private float Immortal;            // thời gian bất tử
    [SerializeField] private float ChangeColorImmortal;         // nhân vật đổi màu khi bất tử
    private SpriteRenderer SpriteRenderer;          // đồi màu nhân vật trong Sprite
    [Header("UI")]
    [SerializeField] private UIPauseMenu uiPauseMenu;

    //[SerializeField] private AudioClip DeadSound;
    //[SerializeField] private AudioClip HurtSound;
    Animator Anim;
    private void Awake()
    {
        Anim = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
        CurrentHealth = MaxHealth;
    }
    public void TakeDmg(int Dmg)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth - Dmg, 0, MaxHealth);
        if (CurrentHealth > 0)
        {
            StartCoroutine(immortal());
            //SoundManager.instance.PlaySound(HurtSound);
        }
        else
        {
            if (!Dead)
            {
                Anim.SetTrigger("Dead");

                if (GetComponentInParent<AnyaMv>() != null)
                    GetComponent<AnyaMv>().enabled = false;
                Dead = true;
                if (uiPauseMenu != null)
                {
                    uiPauseMenu.GameOVer();
                }
                //SoundManager.instance.PlaySound(DeadSound);
            }
        }
    }

    public void HealthPlus(float plus)
    {
        CurrentHealth = Mathf.Clamp(CurrentHealth + plus, 0, MaxHealth);
    }
    private IEnumerator immortal()
    {
        Physics2D.IgnoreLayerCollision(3, 6, true);         // hàm chỉ định hai vật thể không va chạm với nhau
        for (int i = 0; i < ChangeColorImmortal; i++)
        {
            SpriteRenderer.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(Immortal / (ChangeColorImmortal * 2));
            SpriteRenderer.color = Color.white;
            yield return new WaitForSeconds(Immortal / (ChangeColorImmortal * 2));      // thời gian nhấp nháy khi nhận St

        }

        Physics2D.IgnoreLayerCollision(3, 6, false);        //hàm để chỉ định hai vật thể va chạm lại với nhau
    }
    public void respawn()
    {
        Dead = false;
        Anim.ResetTrigger("Dead");
        Anim.Play("Idle");
        GetComponent<AnyaMv>().enabled = true;
    }
}
