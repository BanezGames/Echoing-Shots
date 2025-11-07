using UnityEngine;
using System.Collections.Generic;
using System;


public class shopKeeper : MonoBehaviour

{
    [SerializeField] private GameObject[] itemPrefabs;
    [SerializeField] private List<int> itemCosts;
    [SerializeField] private string[] itemNames;
    [SerializeField] Transform spawnPoint;
    [SerializeField] private int selecteditemindex = 0;
    [SerializeField] private AudioSource audioSource;
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
            SellItem(selecteditemindex);
        }
                
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press 'E' to buy " + itemNames[selecteditemindex] + " for " + itemCosts[selecteditemindex]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange= false;
        }
    }

    private void SellItem(int index)
    {
        if (index < 0 || index >= itemPrefabs.Length || index >= itemCosts.Count || index >= itemNames.Length)
        {
            return;
        }

        int cost = itemCosts[index];
        GameObject item = itemPrefabs[index];

        if(PlayerInventory.instance.Coins>=cost)
        {
            PlayerInventory.instance.AddGold(-cost);
            Instantiate(item,spawnPoint.position, Quaternion.identity);
            Debug.Log("Purchased: " + itemNames[index]);
        }
        else
        {
            Debug.Log("Not enough coins to buy " + itemNames[index]);
        }
    }

    private void SetSelectedItem(int index)
    {
        if(index >= 0 && index < itemPrefabs.Length)
        {
            selecteditemindex = index;
        }
    }
}
