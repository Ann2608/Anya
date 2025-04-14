using System;
using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public static event Action<float> OnSpeedChange;

    [SerializeField]
    private string itemName;
    [SerializeField]
    private int quantity = 1;
    [SerializeField]
    private Sprite sprite;
    [TextArea]
    [SerializeField]
    private string itemDescription;
    [SerializeField]
    private float speedBoost;
    [SerializeField]
    private ItemType itemType = ItemType.SpeedItem; // Thêm itemType

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
        if (sprite == null)
        {
            SpriteRenderer sr = GetComponent<SpriteRenderer>();
            if (sr != null)
            {
                sprite = sr.sprite;
            }
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
        inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemType); // Truyền itemType
        OnSpeedChange?.Invoke(speedBoost);
        if (ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(itemName, sprite);
        }
        Destroy(gameObject);
    }

    public virtual void PickUp()
    {
        if (ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(itemName, sprite);
        }
    }
    public enum ItemType
    {
        None,
        SpeedItem,
        SwordItem,
        DmgItem
    }
}