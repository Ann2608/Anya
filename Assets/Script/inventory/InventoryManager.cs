using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActived = false;
    public Itemslot[] itemslot;
    public ItemSo[] itemSOs;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Nhấn phím "I"
        {
            menuActived = !menuActived; // Đảo trạng thái
            InventoryMenu.SetActive(menuActived); // Hiển thị hoặc ẩn menu
        }
    }

    public void UseItem(string itemName)
    {
        foreach (var item in itemSOs)
        {
            if (item.itemName == itemName)
            {
                item.UseItem();
            }
        }
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        Debug.Log($"itemName = {itemName}, quantity = {quantity}, Sprite = {sprite}");

        for (int i = 0; i < itemslot.Length; i++)
        {
            if (!itemslot[i].isFull || itemslot[i].quantity == 0)
            {
                // Nếu slot trống hoàn toàn, cập nhật thông tin
                if (itemslot[i].quantity == 0)
                {
                    itemslot[i].itemName = itemName;
                    itemslot[i].sprite = sprite;
                    itemslot[i].itemDescription = itemDescription;
                }

                int leftOverItems = itemslot[i].AddItem(itemName, quantity, sprite, itemDescription);
                if (leftOverItems > 0)
                {
                    return AddItem(itemName, leftOverItems, sprite, itemDescription);
                }
                return 0;
            }
        }
        return quantity;
    }

    public void DeSelectAllSlots()
    {
        foreach (var slot in itemslot)
        {
            slot.selectedShader.SetActive(false);
            slot.SelectedItem = false;
        }
    }
}
