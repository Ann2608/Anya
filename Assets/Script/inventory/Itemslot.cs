using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Itemslot : MonoBehaviour, IPointerClickHandler
{
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public bool isFull;
    public string itemDescription;
    public Sprite EmptySprite;

    [SerializeField]
    private int maxNumberOfItems;

    [SerializeField]
    public TMP_Text quantityText;
    [SerializeField]
    public Image Itemimage;
    public GameObject selectedShader;
    public bool SelectedItem;

    private InventoryManager inventoryManager;

    private void Start()
    {
        inventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        if (isFull) return quantity; // Nếu ô đầy, trả lại số item dư

        this.itemName = itemName;
        this.quantity += quantity;

        if (this.quantity >= maxNumberOfItems)
        {
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            isFull = true;
            UpdateUI();
            return extraItems;
        }

        UpdateUI();
        return 0;
    }

    private void UpdateUI()
    {
        quantityText.text = quantity > 0 ? quantity.ToString() : "";
        quantityText.enabled = quantity > 0;
        Itemimage.sprite = quantity > 0 ? sprite : EmptySprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeselectAllSlots();
        selectedShader.SetActive(true);
        SelectedItem = true;
    }

    public void UseSelectedItem()
    {
        Debug.Log("Sử dụng item: " + itemName);
        quantity--;

        if (quantity <= 0)
        {
            itemName = null;
            isFull = false;
        }

        UpdateUI();
    }
    public void ClearSlot()
    {
        itemName = null;
        quantity = 0;
        isFull = false;
        Itemimage.sprite = EmptySprite;
        quantityText.text = "";
    }
    public void UpdateQuantityText()
    {
        quantityText.text = quantity > 0 ? quantity.ToString() : "";
    }
}
