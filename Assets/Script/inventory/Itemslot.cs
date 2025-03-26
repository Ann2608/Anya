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



    // ==================== item slot =========================================
    [SerializeField]
    private TMP_Text quantityText;
    [SerializeField]
    private Image Itemimage;
    public GameObject selectedShader;
    public bool SelectedItem;

    private InventoryManager iventoryManager;
    // ============== Item Des Slot ===================
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;





    private void Start()
    {
        iventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
    }

    public void AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        this.itemName = itemName;
        this.quantity = quantity;
        this.sprite = sprite;
        this.itemDescription = itemDescription;
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
        iventoryManager.DeSelectAllSlots();
        selectedShader.SetActive(true);
        SelectedItem = true;
        itemDescriptionNameText.text = itemName;
        itemDescriptionText.text = itemDescription;
        itemDescriptionImage .sprite = sprite;

        if (itemDescriptionImage.sprite == null) 
        { 
            itemDescriptionImage.sprite = EmptySprite;
        }
    }


    public void OnRightClick() { }
}
