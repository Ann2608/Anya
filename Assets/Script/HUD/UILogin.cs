using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UILogin : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable pre = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (pre != null)
            {
                pre.Select();
            }
        }
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
    }
}
