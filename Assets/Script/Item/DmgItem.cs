using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgItem : MonoBehaviour
{
    public static event Action<int> OnDmgChange;        // khai bao su kien
    public int DmgBoost;

    public int ID;
    public string Name;
    public virtual void PickUp()
    {
        Sprite ItemIcon = null;
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            ItemIcon = sr.sprite; // Lấy sprite từ SpriteRenderer
        }
        if (ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(Name, ItemIcon);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AnyaAtk anyaAtk = collision.GetComponent<AnyaAtk>();
            if(anyaAtk != null)
            {
                anyaAtk.AttackIncrease((int)DmgBoost);
            }
            Collect();
        }
    }
    public void Collect()
    {
        PickUp(); // Gọi PickUp để hiển thị popup
        OnDmgChange?.Invoke((int)DmgBoost);
        Destroy(gameObject);
    }
}
