using UnityEngine;
using static SpeedItem;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActived = false;
    public Itemslot[] itemslot;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            menuActived = !menuActived;
            InventoryMenu.SetActive(menuActived);
        }
    }

    public void AddItem(string itemName, int quantity, Sprite sprite, string itemDescription, ItemType itemType = ItemType.None)
    {
        Debug.Log($"itemName = {itemName}, quantity = {quantity}, sprite = {sprite}, type = {itemType}");
        for (int i = 0; i < itemslot.Length; i++)
        {
            if (itemslot[i].isFull == false)
            {
                itemslot[i].AddItem(itemName, quantity, sprite, itemDescription, itemType);
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