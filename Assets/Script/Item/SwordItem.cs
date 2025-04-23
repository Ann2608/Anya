using UnityEngine;
using static SpeedItem;

public class SwordItem : MonoBehaviour
{
    public int ID;
    public string Name;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            AnyaAtk player = collision.GetComponent<AnyaAtk>();
            if (player != null)
            {
                player.EquipSword();
            }
            Collect();
        }
    }

    public void Collect()
    {
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