using UnityEngine;
using System.Collections;

public class powerUp : MonoBehaviour
{
    enum PowerUpType { health, shield, damageBoost }
    [SerializeField] PowerUpType type;

    [SerializeField] int healAmount;
    [SerializeField] int shieldDuration;
    [SerializeField] int damageBoostDuration;
    [SerializeField] int damageBoostAmount;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            return;

        playerController player= other.GetComponent<playerController>();
        if (player == null)
            return;

        if(type == PowerUpType.health)
        {
            player.Heal(healAmount);
        }
        else if (type == PowerUpType.shield)
        {
            player.StartCoroutine(player.Shield(shieldDuration));
        }
        else if (type == PowerUpType.damageBoost)
        {
           player.StartCoroutine( player.DamageBoost(damageBoostDuration, damageBoostAmount));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
