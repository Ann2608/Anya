using UnityEngine;

public abstract class ItemSo : ScriptableObject
{
    public string itemName;
    public Sprite itemIcon;

    public abstract void UseItem(GameObject player, InventoryManager inventoryManager);
}

[CreateAssetMenu(fileName = "HealthItem", menuName = "Inventory/Items/HealthItem")]
public class HealthItem : ItemSo
{
    public int healAmount;

    public override void UseItem(GameObject player, InventoryManager inventoryManager)
    {
        //PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
       // if (playerHealth != null)
        //{
        //    playerHealth.ChangeHealth(healAmount);
       //     inventoryManager.RemoveItem(this);
       // }
    }
}

[CreateAssetMenu(fileName = "SpeedItem", menuName = "Inventory/Items/SpeedItem")]
public class SpeedItem : ItemSo
{
    public float speedBoost;
    public float duration;

    public override void UseItem(GameObject player, InventoryManager inventoryManager)
    {
       // PlayerStats playerStats = player.GetComponent<PlayerStats>();
       // if (playerStats != null)
       // {
       //     playerStats.ApplySpeedBoost(speedBoost, duration);
       //     inventoryManager.RemoveItem(this);
       // }
    }
}
