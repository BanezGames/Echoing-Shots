using UnityEngine;

[CreateAssetMenu]
public class tomeStats : ScriptableObject
{
    public string tomeName;
    public GameObject tomeModel;
    [Range(5, 50)] public int sanityCost;
    [Range(10, 30)] public float castRate;
    public GameObject bullet;
    public Color tomeColor;

    public AudioClip[] shootSound;
    [Range(0, 1)] public float shootSoundVol;

}