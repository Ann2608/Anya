using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    public int MaxHealth;
    public int CurrentHealth;
    Rigidbody2D rg;
    public Animator Anim;
    public float delayBeforeDeath = 3f;
    //[SerializeField] private AudioClip DeadSound;
    //[SerializeField] private AudioClip HurtSound;
    private void Start()
    {
        rg = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        CurrentHealth = MaxHealth;
    }
    public void TakeDmg(int Dmg)
    {
        CurrentHealth -= Dmg;
        Anim.SetTrigger("Hit");
        //SoundManager.instance.PlaySound(HurtSound);
        if (CurrentHealth <= 0)
        {
            Die();
            Invoke("DestroyEnemy", delayBeforeDeath);
        }
    }
    private void Die()
    {
        if (Anim != null)
        {
            Anim.SetBool("Dead", true);
            //SoundManager.instance.PlaySound(DeadSound);
        }
    }
    private void DestroyEnemy()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
}
