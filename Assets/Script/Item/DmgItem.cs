using System;
using UnityEngine;
using static SpeedItem;

public class DmgItem : MonoBehaviour
{
    public static event Action<int> OnDmgChange;

    public int ID;
    public string Name;
    public int dmgBoost;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AnyaAtk anyaAtk = collision.GetComponent<AnyaAtk>();
            if (anyaAtk != null)
            {
                anyaAtk.AttackIncrease(dmgBoost);
            }
            Collect();
        }
    }

    public void Collect()
    {
        OnDmgChange?.Invoke(dmgBoost);
        PickUp();
        Destroy(gameObject);
    }

    public virtual void PickUp()
    {
        Sprite IconItem = GetComponent<SpriteRenderer>().sprite;
        if (ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(Name, IconItem);
        }
    }
}