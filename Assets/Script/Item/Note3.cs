using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Note3 : MonoBehaviour
{
    private string NoteContent = "Nhặt vật phẩm để mở khoá kĩ năng mới";
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
