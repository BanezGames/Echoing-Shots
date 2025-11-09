using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public enum itemtype
{
    Consumable,
    Weapon

}

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item Stats")]

public class itemStats : ScriptableObject
{
    public itemtype var;
    public Texture image;
    public int Healing;
    public int InvincDuration;
    public int SanityRestore;
    //public int duration;
    public GameObject projectile;
    public int durability;
    


}
