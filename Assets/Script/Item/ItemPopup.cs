using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemPopup : MonoBehaviour
{
    public static ItemPopup Instance { get; private set; }
    public GameObject PopupPrefab;
    public int MaxPopup = 3;
    public float PopupDuration;         // thoi gian hien thi popup

    private readonly Queue<GameObject> _active = new();     // Queue<GameObject>: những phần tử đầu tiên được lấy ra đầu tiên
                                                            // readonly: chỉ gán duy nhất một giá trị và không thể thay đổi được
                                                            // Queue: đây là hàng đợi
    private void Awake()        // chạy trước start và chạy ngay khi được gọi
    {
        if(Instance == null)
        {
            Instance = this;        //chỉ có một instante trong scene
        }
        else
        {
            Debug.LogError("Another instance of this singleton already exists!");
            Destroy(gameObject);
            // nếu tồn tại một cái thứ 2 ngay lập tức huỷ
        }
    }

    public void ShowItem(string ItemName, Sprite IconItem)
    {
        GameObject NewPopup = Instantiate(PopupPrefab, transform);      // tạo popup mới
        NewPopup.GetComponentInChildren<Text>().text = ItemName;        // gán tên cho vật phẩm mới

        Image ItemImage = NewPopup.transform.Find("ItemIcon")?.GetComponent<Image>();
        if(ItemImage)
        {
            ItemImage.sprite = IconItem;        // gán Icon cho vật phẩm
        }

        _active.Enqueue(NewPopup);
        if(_active.Count > MaxPopup)        // nếu vượt quá số lượng
        {
            Destroy(_active.Dequeue());     // xoá popup cũ
        }

        StartCoroutine(FadeoutandDestroy(NewPopup));        // Bắt đầu hiệu ứng fade out
    }
    private IEnumerator FadeoutandDestroy(GameObject Popup)
    {
        yield return new WaitForSeconds(PopupDuration);     // thời gian popup xuất hiện
        if(Popup == null) yield break;      // thoát nếu popup bị huỷ

        CanvasGroup cv = Popup.GetComponent<CanvasGroup>();
        for(float timepass = 0f; timepass < 1f; timepass += Time.deltaTime)
        {
            if(Popup == null) yield break ;
            cv.alpha = 1 - timepass;        //giảm Fadeout
            yield return null;      //chờ popup khác xuất hiện
        }
        Destroy(Popup);     // huỷ popup sau khi Fadeout
    }
}
