using UnityEngine;
using UnityEngine.UIElements;

public class InventoryManager : MonoBehaviour
{
    public GameObject InventoryMenu;
    private bool menuActived = false;
    public Itemslot[] itemSlots;
    public PlayerController playerController;

    private int selectedIndex = 0; // Ô đầu tiên mặc định được chọn
    private int columns = 5; // Số cột của inventory
    //private int rows = 5; // Số hàng của inventory

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I)) // Bật/tắt menu inventory
        {
            ToggleInventory();
        }

        if (menuActived)
        {
            HandleKeyboardInput();
        }
    }

    private void ToggleInventory()
    {
        menuActived = !menuActived;
        InventoryMenu.SetActive(menuActived);
        

        if (menuActived)
        {
            Time.timeScale = 0f; // Dừng game khi mở inventory
            playerController.enabled = false; // Tắt điều khiển nhân vật
           
        }
        else
        {
            Time.timeScale = 1f; // Tiếp tục game khi đóng inventory
            playerController.enabled = true; //  điều khiển nhân vật
        }
    }

    private void HandleKeyboardInput()
    {
        int previousIndex = selectedIndex;

        if (Input.GetKeyDown(KeyCode.A)) // Sang trái
        {
            if (selectedIndex % columns != 0) // Kiểm tra có ở cột đầu tiên không
                selectedIndex--;
        }
        else if (Input.GetKeyDown(KeyCode.D)) // Sang phải
        {
            if ((selectedIndex + 1) % columns != 0) // Kiểm tra có ở cột cuối không
                selectedIndex++;
        }
        else if (Input.GetKeyDown(KeyCode.W)) // Lên trên
        {
            if (selectedIndex - columns >= 0)
                selectedIndex -= columns;
        }
        else if (Input.GetKeyDown(KeyCode.S)) // Xuống dưới
        {
            if (selectedIndex + columns < itemSlots.Length)
                selectedIndex += columns;
        }

        if (previousIndex != selectedIndex)
        {
            DeselectAllSlots();
            SelectSlot(selectedIndex);
        }
    }

    private void SelectSlot(int index)
    {
        itemSlots[index].selectedShader.SetActive(true);
        itemSlots[index].SelectedItem = true;
    }

    public void DeselectAllSlots()
    {
        foreach (var slot in itemSlots)
        {
            slot.selectedShader.SetActive(false);
            slot.SelectedItem = false;
        }
    }

    public void UseItem()
    {
        if (itemSlots[selectedIndex].SelectedItem)
        {
            itemSlots[selectedIndex].UseSelectedItem();
        }
    }

    public void RemoveItem(ItemSo itemToRemove)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemSlots[i].itemName == itemToRemove.itemName)
            {
                itemSlots[i].quantity--;

                if (itemSlots[i].quantity <= 0)
                {
                    itemSlots[i].ClearSlot();
                }
                else
                {
                    itemSlots[i].UpdateQuantityText();
                }
                return;
            }
        }
    }

    public int AddItem(ItemSo itemData, int quantity)
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (!itemSlots[i].isFull || itemSlots[i].itemName == itemData.itemName)
            {
                return itemSlots[i].AddItem(itemData.itemName, quantity, itemData.itemIcon, "Mô tả item");
            }
        }
        return quantity; // Trả lại số lượng item không thể thêm
    }
}
