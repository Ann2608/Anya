using System;
using UnityEngine;
using static SpeedItem;

public class DmgItem : MonoBehaviour
{
    public static event Action<int> OnDmgChange;

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
    private int dmgBoost;
    [SerializeField]
    private ItemType itemType = ItemType.DmgItem;

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
        inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemType);
        OnDmgChange?.Invoke(dmgBoost);
        PickUp();
        Destroy(gameObject);
    }

    public virtual void PickUp()
    {
        Sprite itemIcon = sprite;
        if (ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(itemName, itemIcon);
        }
    }
}