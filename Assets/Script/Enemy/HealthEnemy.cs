using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthEnemy : MonoBehaviour
{
    [Header("Loot")]
    public List<LootItem> lootItems = new List<LootItem>();
    public int MaxHealth;
    public int CurrentHealth;
    Rigidbody2D rg;
    public Animator Anim;
    public float delayBeforeDeath = 3f;
    [SerializeField] private AudioClip DeadSound;
    [SerializeField] private AudioClip HurtSound;

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
        AudioManager.instance.PlaySound(HurtSound);
        if (CurrentHealth <= 0)
        {
            Die();
            Invoke("DestroyEnemy", delayBeforeDeath);
        }
    }
    private void Die()
    {
        foreach (LootItem item in lootItems)
        {
            if(Random.Range(0f, 100f) <= item.DropChance)
            {
                InstantiateLoot(item.ItemPrefab);
                break;
            }
        }
        if (Anim != null)
        {
            Anim.SetBool("Dead", true);
            AudioManager.instance.PlaySound(DeadSound);
        }
    }
    private void DestroyEnemy()
    {
        if (gameObject != null)
        {
            Destroy(gameObject);
        }
    }
    void InstantiateLoot(GameObject loot)
    {
        if (loot)
        {
            GameObject Drop = Instantiate(loot, transform.position, Quaternion.identity);
            Drop.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
}
