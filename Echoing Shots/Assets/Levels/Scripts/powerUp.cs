using UnityEngine;
//using System.Collections;
using UnityEngine.UI;

public class powerUp : MonoBehaviour
{
    enum PowerUpType { health, shield, damageBoost, sanity }
    [SerializeField] PowerUpType type;

    [SerializeField] int healAmount;
    [SerializeField] int shieldDuration;
    [SerializeField] int damageBoostDuration;
    [SerializeField] int damageBoostAmount;
    [SerializeField] itemStats item;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") || gameManager.instance.itemInventory[gameManager.instance.selectedIndex] !=null)
            return;

        playerController player= other.GetComponent<playerController>();
        if (player == null)
            return;

        //if(type == PowerUpType.health)
        //{
        // player.Heal(healAmount);
        //}
        //else if (type == PowerUpType.shield)
        //{
        //player.StartCoroutine(player.Shield(shieldDuration));
        //}
        //else if (type == PowerUpType.damageBoost)
        //{
        //player.StartCoroutine( player.DamageBoost(damageBoostAmount, damageBoostDuration));
        //}
        //Debug.Log("Index" + gameManager.instance.selectedIndex);
        if (type != PowerUpType.sanity)
        {
            gameManager.instance.itemInventory[gameManager.instance.selectedIndex] = item;
            gameManager.instance.InventorySlotsImage[gameManager.instance.selectedIndex].GetComponent<RawImage>().texture = item.image;
            gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex] = item.durability;
            gameManager.instance.inventoryDurability[gameManager.instance.selectedIndex].text = gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex].ToString();
        }
        else
        {
            player.RestoreSanity(healAmount);
        }
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
