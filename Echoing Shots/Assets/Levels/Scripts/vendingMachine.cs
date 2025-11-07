using UnityEngine;
using System.Collections.Generic;
using System.Reflection;



public class vendingMachine : MonoBehaviour
{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private List<int> itemCosts;
    [SerializeField] private string[] itemNames;
    [SerializeField] private Transform dispensePoint;
    [SerializeField] private int itemIndexToSell = 0;
    [SerializeField] private AudioSource audiSource;
    [SerializeField] private AudioClip dispenseSound;

    private bool playerInRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            BuyItem(itemIndexToSell);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to buy " + itemNames[itemIndexToSell] + "for" + itemCosts[itemIndexToSell]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }


    public void BuyItem(int index)
    {
        if (index < 0 || index >= itemPrefabs.Length || index >= itemCosts.Count || index >= itemNames.Length)
        {
            return;
        }

        GameObject item = itemPrefabs[index];
        int cost = itemCosts[index];

        if (PlayerInventory.instance.Coins >= cost)
        {
            PlayerInventory.instance.AddGold(-cost);
            Instantiate(item, dispensePoint.position, Quaternion.identity);
            Debug.Log("Dispensed: " + itemNames[index]);
        }
        else
        {
            Debug.Log("Not enough coins to buy: " + itemNames[index]);
        }
    }
}
