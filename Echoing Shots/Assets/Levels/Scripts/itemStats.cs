using System.Collections;
using UnityEngine;
using UnityEngine.UI;



public enum itemtype
{
    Consumable,
    Weapon

}

[CreateAssetMenu]

public class itemStats : ScriptableObject
{
    public itemtype var;
    public Texture image;
    public int Healing;
    public int InvincDuration;
    //public int duration;
    public GameObject projectile;
    public int durability;
    


}
