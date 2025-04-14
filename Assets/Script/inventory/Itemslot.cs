using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static SpeedItem;

public class Itemslot : MonoBehaviour, IPointerClickHandler
{
    // ============================item data ==============================
    public string itemName;
    public int quantity;
    public Sprite sprite;
    public bool isFull;
    public string itemDescription;
    public ItemType itemType; // Thêm itemType
    public Sprite EmptySprite;

    // ==================== item slot =========================================
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image Itemimage;
    public GameObject selectedShader;
    public bool SelectedItem;

    private InventoryManager inventoryManager;
    // ============== Item Des Slot ===================
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;

    private void Start()
    {
        inventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
    }

    private void Update()
    {
        if (SelectedItem && Input.GetKeyDown(KeyCode.E) && isFull)
        {
            UseItem();
        }
    }

    public void AddItem(string itemName, int quantity, Sprite sprite, string itemDescription, ItemType itemType)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.sprite = sprite;
        this.itemDescription = itemDescription;
        this.itemType = itemType;
        isFull = true;

        quantityText.text = quantity.ToString();
        quantityText.enabled = true;
        Itemimage.sprite = sprite;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            OnLeftClick();
        }
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
        }
    }

    public void OnLeftClick()
    {
        inventoryManager.DeSelectAllSlots();
        selectedShader.SetActive(true);
        SelectedItem = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage.sprite = sprite;

        if (itemDescriptionImage.sprite == null)
        {
            itemDescriptionImage.sprite = EmptySprite;
        }
    }

    public void OnRightClick() { }

    private void UseItem()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        AnyaMv anyaMv = player.GetComponent<AnyaMv>();
        AnyaAtk anyaAtk = player.GetComponent<AnyaAtk>();

        switch (itemType)
        {
            case ItemType.SpeedItem:
                if (anyaMv != null)
                    anyaMv.Startspeedboost(2.0f); // Giá trị giả định
                break;
            case ItemType.SwordItem:
                if (anyaAtk != null)
                    anyaAtk.EquipSword();
                break;
            case ItemType.DmgItem:
                if (anyaAtk != null)
                    anyaAtk.AttackIncrease(5); // Giá trị giả định
                break;
        }

        ClearSlot();
    }

    private void ClearSlot()
    {
        itemName = "";
        quantity = 0;
        sprite = null;
        itemDescription = "";
        itemType = ItemType.None;
        isFull = false;

        quantityText.text = "";
        quantityText.enabled = false;
        Itemimage.sprite = EmptySprite;
        itemDescriptionNameText.text = "";
        itemDescriptionText.text = "";
        itemDescriptionImage.sprite = EmptySprite;

        selectedShader.SetActive(false);
        SelectedItem = false;
    }
}