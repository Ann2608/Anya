using System;
using UnityEngine;
using UnityEngine.UI;

public class SpeedItem : MonoBehaviour
{
    public static event Action<float> OnSpeedChange;
    public float speedBoost;
    public int ID;
    public string Name;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Collect();
        }
    }

    public void Collect()
    {
        OnSpeedChange?.Invoke(speedBoost);
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