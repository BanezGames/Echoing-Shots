using UnityEngine;

public class Laudanum : MonoBehaviour, IConsumablePickup
{
    public itemStats laudanum;


    public void pickupItem(itemStats item)
    {
        gameManager.instance.itemInventory[gameManager.instance.selectedIndex] = item;
        gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex] = item.durability;
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            pickupItem(laudanum);
        }
    }
}
