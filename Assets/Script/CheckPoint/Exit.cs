using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (Key.hasKey)
            {
                // Chuyển sang màn tiếp theo
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                // Reset trạng thái key cho màn mới
                Key.hasKey = false;
            }
            else
            {
                Debug.Log("You need to collect the key first!");
            }
        }
    }
}