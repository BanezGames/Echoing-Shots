using System.Collections;
using UnityEngine;



public enum itemtype
{
    Consumable,
    Weapon

}

[CreateAssetMenu]

public class itemStats : ScriptableObject
{
    public itemtype var;
    public int Healing;
    public bool givesInvinc;
    public int duration;
    public GameObject projectile;
    public int durability;
    


}
