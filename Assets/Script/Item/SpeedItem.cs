using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedItem : MonoBehaviour
{
    public static event Action<float> OnSpeedChange;        // khai bao su kien
    public float SpeedBoost;

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
        if(ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(Name, ItemIcon);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }
    public void Collect()
    {
        PickUp(); // Gọi PickUp để hiển thị popup
        OnSpeedChange.Invoke(SpeedBoost);
        Destroy(gameObject);
    }
}
