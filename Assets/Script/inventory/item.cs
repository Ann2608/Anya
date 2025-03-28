using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemSo itemData;
    public int quantity = 1; // Số lượng mặc định

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            InventoryManager inventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
            int leftOverItems = inventoryManager.AddItem(itemData, quantity);
            if (leftOverItems <= 0)
            {
                Destroy(gameObject); // Xóa vật phẩm khi được nhặt hết
            }
            else
            {
                quantity = leftOverItems;
            }
        }
    }
}
