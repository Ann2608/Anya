using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActived = false;
    public Itemslot[] itemslot;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Nhấn phím "I"
        {
            menuActived = !menuActived; // Đảo trạng thái
            InventoryMenu.SetActive(menuActived); // Hiển thị hoặc ẩn menu
        }
    }
    public void AddItem (string itemName, int quantity , Sprite sprite, string itemDescription)
    {
        Debug.Log("itemName = " + itemName + "quantity =" + quantity + "Sprite"+ sprite);
        for (int i = 0; i< itemslot.Length; i++)
        {
            if (itemslot[i].isFull == false)
            {
                itemslot[i].AddItem(itemName, quantity, sprite, itemDescription);
                return;
            }
        }

    }
    public void DeSelectAllSlots()
    {
        for (int i = 0; i < itemslot.Length; i++)
        {
            itemslot[i].selectedShader.SetActive(false);
            itemslot[i].SelectedItem = false;
        }

    }
}
