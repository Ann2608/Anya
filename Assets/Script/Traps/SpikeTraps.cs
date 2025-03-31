using UnityEngine;

public class SpikeTraps : MonoBehaviour
{
    [SerializeField] private int trapDamage; 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra nếu va chạm với Player
        PlayerController player = collision.GetComponent<PlayerController>();   
        if (player != null)
        {
            player.TakeDamage(trapDamage); // Gọi hàm TakeDamage từ PlayerController
        }
    }
}