using UnityEngine;

[CreateAssetMenu]
public class tomeStats : ScriptableObject
{
    public GameObject tomeModel;
    [Range(1, 10)] public int shootDamage;
    [Range(15, 1000)] public int shootDist;
    [Range(5, 50)] public int sanityCost;
    [Range(30, 120)] public float castRate;
    public GameObject bullet;
    public Color tomeColor;

    public AudioClip[] shootSound;
    [Range(0, 1)] public float shootSoundVol;

}