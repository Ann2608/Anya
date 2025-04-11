using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwordItem : MonoBehaviour
{

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
            AnyaAtk player = collision.GetComponent<AnyaAtk>();
            if (player != null)
            {
                player.EquipSword(); // Gọi hàm trang bị kiếm
                //Destroy(gameObject); // Hủy vật phẩm sau khi nhặt
            }
            Collect();
        }
    }
    public void Collect()
    {
        PickUp(); // Gọi PickUp để hiển thị popup
        //OnSpeedChange.Invoke(SpeedBoost);
        Destroy(gameObject);
    }
}
