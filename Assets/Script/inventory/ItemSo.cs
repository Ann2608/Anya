using UnityEngine;

[CreateAssetMenu]
public class ItemSo : ScriptableObject 
{

    public string itemName;
    public StatToChange statToChange = new StatToChange();
    public int amountToChangeStat;

    public AttributesToChange attributesToChange = new AttributesToChange();
    public int amountToChangeAttributes;

    public void UseItem()
    {
        if (statToChange == StatToChange.health)
        {
            //GameObject.Find("HealthManager").GetComponent<PlayerHealth>().ChangeHealth(amountToChangeStat);
        }
    }
    public enum StatToChange
    {
        none,
        health,
        speed
    }; public enum AttributesToChange
    {
        none,
        dmg,
        speed
    };

}

   

