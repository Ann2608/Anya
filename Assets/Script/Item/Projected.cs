using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projected : MonoBehaviour
{
    public int dmg = 2;
    //public GameObject ExplosionPrefab;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            AnyaMv enemy = collision.gameObject.GetComponent<AnyaMv>();
            if (enemy != null)
            {
                AnyaHealth Health;
                Health = collision.gameObject.GetComponent<AnyaHealth>();
                Health.TakeDmg(dmg);
            }
        }

        // Xóa viên đạn sau khi va chạm với bất kỳ đối tượng nào
        Destroy(gameObject);
    }
}
