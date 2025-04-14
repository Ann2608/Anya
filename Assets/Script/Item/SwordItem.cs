using UnityEngine;
using static SpeedItem;

public class SwordItem : MonoBehaviour
{
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
    private ItemType itemType = ItemType.SwordItem;

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
        inventoryManager.AddItem(itemName, quantity, sprite, itemDescription, itemType);
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