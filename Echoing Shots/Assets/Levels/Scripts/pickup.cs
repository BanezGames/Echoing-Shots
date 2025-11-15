
using UnityEngine;

public class pickup : MonoBehaviour
{
    [SerializeField] gunStats gun;
    [SerializeField] tomeStats tome;

    private void OnTriggerEnter(Collider other)
    {
        IPickup pickup = other.GetComponent<IPickup>();
        if(pickup != null && gun != null)
        {
            gun.ammoCur = gun.ammoMax;
            pickup.getGunStats(gun);
            Destroy(gameObject);
        }
        
        if(pickup != null && tome != null)
        {
            pickup.getTomeStats(tome);
            Destroy(gameObject);
        }
    }
}
