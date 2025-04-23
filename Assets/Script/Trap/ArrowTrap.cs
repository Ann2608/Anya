using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float AtkCoolDown;     // thời gian hồi chiêu tấn công - tự động đánh
    [SerializeField] private Transform FirePoint;   //vị trí mũi tên bay ra
    [SerializeField] private GameObject[] Arrow;
    private float CoolDownTimer;        //  thời gian giữa các đòn đánh
    [SerializeField] private AudioClip ArrowSound;

    private void Atk()
    {
        CoolDownTimer = 0;
        //SoundManager.instance.PlaySound(ArrowSound);

        int arrowIndex = FindArrow();

        if (arrowIndex != -1) // Kiểm tra xem có mũi tên hợp lệ hay không
        {
            Arrow[arrowIndex].transform.position = FirePoint.position;   // Đặt lại mũi tên về vị trí ban đầu sau mỗi lần bắn
            Arrow[arrowIndex].GetComponent<Arrow>().Activate();
        }
        else
        {
            Debug.LogWarning("Không thể bắn mũi tên vì tất cả đều đã được sử dụng!");
        }
    }

    private int FindArrow()     //cái này dùng để xem có mũi tên nào chưa được sử dụng hay không tránh việc bị lặp mũi tên
    {
        for (int i = 0; i < Arrow.Length; i++)
        {
            if (!Arrow[i].activeInHierarchy)
                return i;       // trả về những mũi tên chưa được sử dụng

        }
        return -1;       //đã sử dụng hết
    }

    private void Update()
    {
        CoolDownTimer += Time.deltaTime;
        if (CoolDownTimer >= AtkCoolDown)        // nếu thời gian giữa các đòn đánh lớn hơn thời gian hồi chiêu tấn công
        {
            Atk();
        }
    }
}
