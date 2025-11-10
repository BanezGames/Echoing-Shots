using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

public class vendingMachine : MonoBehaviour
{
    [SerializeField] private List<GameObject> storeItems = new List<GameObject>();
    [SerializeField] private List<int> itemCosts = new List<int>();
    [SerializeField] private string[] itemNames;
    [SerializeField] private Transform dispensePoint;
    [SerializeField] private int itemindexToSell = 0;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip dispenseSound;



    private bool isPlayerinRange = false;



    public void Update()
    {
        if (isPlayerinRange && Input.GetKeyDown(KeyCode.E))
        {
            DispenseItem(itemindexToSell);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerinRange = true;
            Debug.Log("Press E to buy" + itemNames[itemindexToSell]);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerinRange = false;
        }
    }


    public void DispenseItem(int index)
    {
        if (index < 0 || index >= storeItems.Count || index >= itemCosts.Count || index >= itemNames.Length)
        {
            return;
        }

        GameObject item = storeItems[index];
        int cost = itemCosts[index];

        if (PlayerInventory.instance.Coins >= cost)
        {
            PlayerInventory.instance.AddGold(-cost);
            item.transform.position = dispensePoint.position;
            item.SetActive(true);

            if (audioSource != null && dispenseSound != null)
            {
                audioSource.PlayOneShot(dispenseSound);
            }

            Debug.Log("Dispensed " + itemNames[index]);
        }
        else
        {
            Debug.Log("Not enough coins to buy " + itemNames[index]);
        }
    }

    public string GetItemName(int index)
    {
        if (index >= 0 && index < itemNames.Count())
        {
            return itemNames[index];
        }

        return " ";
    }

    public int GetItemCost(int index)
    {
        if (index >= 0 && index < itemCosts.Count())
        {
            return itemCosts[index];
        }

        return 0;
    }

    public int ItemCount()
    {
        return storeItems.Count;
    }

}