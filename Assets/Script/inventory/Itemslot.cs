using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Itemslot : MonoBehaviour, IPointerClickHandler
{
    // ============================item data ==============================
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public bool isFull;
    public string itemDescription;
    public Sprite EmptySprite;

    [SerializeField]
    private int maxNumberOfItems;

    // ==================== item slot =========================================
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image Itemimage;
    public GameObject selectedShader;
    public bool SelectedItem;

    private InventoryManager inventoryManager;
    // ============== Item Description Slot ===================
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    private void Start()
    {
        inventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        if (isFull)
        {
            return quantity;
        }

        this.itemName = itemName;
        this.sprite = sprite;
        this.itemDescription = itemDescription;
        this.quantity += quantity;

        // Cập nhật UI
        Itemimage.sprite = sprite;
        Itemimage.enabled = true;

        if (this.quantity >= maxNumberOfItems)
        {
            isFull = true;
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            return extraItems;
        }

        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        else if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        if (SelectedItem)
        {
            inventoryManager.UseItem(itemName);
        }

        inventoryManager.DeSelectAllSlots();

        if (selectedShader != null)
        {
            selectedShader.SetActive(true);
        }

        SelectedItem = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = sprite ?? EmptySprite;
    }

    public void OnRightClick()
    {
        // Chức năng chuột phải (nếu cần)
    }
}
