using UnityEngine;
using UnityEngine.UI;

public class Select : MonoBehaviour
{
    [SerializeField] private RectTransform[] option;
    private RectTransform rect;     //dùng để điều chỉnh các đối tượng UI
    private int CurrentPosition;
    //[SerializeField] private AudioClip ChangeSound;
    //[SerializeField] private AudioClip SelectSound;
    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }
    private void Update()
    {
        //thay đổi vị trí mũi tên
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            ChangePosition(-1); // có 1 mảng -1 0 1 và mũi tên đang chỉ vào số 0 thì muốn đi lên phải là -1
        }
        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            ChangePosition(1);  // đi xuống là 1
        }

        // Bấm vào các option ở trên màn hình
        if (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }

    }

    private void ChangePosition(int _Change)
    {
        CurrentPosition += _Change;     // mỗi lần thay đổi sẽ tăng giá trị

        if (_Change != 0)       //tức là có thay đổi vị trí ban đầu
        {
            //SoundManager.instance.PlaySound(ChangeSound);
        }
        if (CurrentPosition < 0)
        {
            CurrentPosition = option.Length - 1;
        }
        else if (CurrentPosition > option.Length - 1)         //tránh mũi tên bay ra ngoài vùng lựa chọn
        {
            CurrentPosition = 0;
        }

        //Gán giá trị Y cho mũi tên đi lên đi xuống tuỳ theo tuỳ chọn
        rect.position = new Vector3(rect.position.x, option[CurrentPosition].position.y, 0);
    }

    private void Interact()
    {
        //SoundManager.instance.PlaySound(SelectSound);
        option[CurrentPosition].GetComponent<Button>().onClick.Invoke();        // Câu lệnh để bấm nút

    }
}
