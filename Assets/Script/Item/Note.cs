﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note : MonoBehaviour
{
    private string NoteContent = "A D Để di chuyển\n Space để nhảy\n Z để tấn công\n X để bắn súng \n Nhặt key để qua màn tiếp theo";
    private AnyaMv anya;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anya = collision.GetComponent<AnyaMv>();
            if(anya != null )
            {
                anya.SetNoteContent(NoteContent);
            }
        }
    }
}
