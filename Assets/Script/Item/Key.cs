using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Key : MonoBehaviour
{
    public static bool hasKey = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            hasKey = true;
            Destroy(gameObject);
        }
    }
}