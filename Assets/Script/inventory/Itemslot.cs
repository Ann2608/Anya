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

    private InventoryManager iventoryManager;
    // ============== Item Des Slot ===================
    public Image itemDescriptionImage;
    public TMP_Text itemDescriptionNameText;
    public TMP_Text itemDescriptionText;





    private void Start()
    {
        iventoryManager = GameObject.Find("IvenCanvas").GetComponent<InventoryManager>();
    }

    public int AddItem(string itemName, int quantity, Sprite sprite, string itemDescription)
    {
        // kiem tra xem iven day chua
        if (isFull)
        {
            return quantity;
        }
        this.itemName = itemName;

        this.quantity += quantity;
        if (this.quantity >= maxNumberOfItems)
        {
            quantityText.text = maxNumberOfItems.ToString();
            quantityText.enabled = true;
            isFull = true;

            //
            int extraItems = this.quantity - maxNumberOfItems;
            this.quantity = maxNumberOfItems;
            return extraItems;

        }  
        quantityText.text = this.quantity.ToString();
        quantityText.enabled = true;
        return 0;



        this.sprite = sprite;
        this.itemDescription = itemDescription;
        

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
