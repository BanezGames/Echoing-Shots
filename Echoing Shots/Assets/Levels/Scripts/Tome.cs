using UnityEngine;

[CreateAssetMenu]
public class Tome : ScriptableObject
{
    public GameObject tomeModel;

    [Range(1, 10)] public int shootDamage;
    [Range(15, 1000)] public int shootDist;
    [Range(0.1f, 3)] public float shootRate;
    public int sanityReduction;
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
}
