using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int HealthPlus;
    public int ID;
    public string Name;
    //[SerializeField] private AudioClip HealthSound;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //SoundManager.instance.PlaySound(HealthSound);
            collision.GetComponent<AnyaHealth>().HealthPlus(HealthPlus);
            gameObject.SetActive(false);
            PickUp();
        }
    }
    public virtual void PickUp()
    {
        Sprite IconItem = GetComponent<SpriteRenderer>().sprite;
        if (ItemPopup.Instance != null)
        {
            ItemPopup.Instance.ShowItem(Name, IconItem);
        }
    }
}
