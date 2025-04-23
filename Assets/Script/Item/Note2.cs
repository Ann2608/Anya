using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note2 : MonoBehaviour
{
    private string NoteContent = "Cẩn thận bẫy ẩn trên đường";
    private AnyaMv anya;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            anya = collision.GetComponent<AnyaMv>();
            if (anya != null)
            {
                anya.SetNoteContent(NoteContent);
            }
        }
    }
}
