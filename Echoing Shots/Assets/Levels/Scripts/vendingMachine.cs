using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;

public class VendingMachine : MonoBehaviour
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
            gameManager.instance.OpenVendingMenu(this);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerinRange = true;
            gameManager.instance.showInteraction(4);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerinRange = false;
            gameManager.instance.hideInteraction();
        }
    }


    public void DispenseItem(int index)
    {
        if (index < 0 || index >= storeItems.Count)
        {
            return;
        }
        if(index >= itemCosts.Count || index >= itemNames.Length)
        {
            return;
        }

        int cost = itemCosts[index];

        if(PlayerInventory.instance.Coins >= cost)
        {
            PlayerInventory.instance.AddGold(-cost);

            GameObject newItem = Instantiate(storeItems[index], dispensePoint.position,Quaternion.identity);

            if(audioSource && dispenseSound)
            {
                audioSource.PlayOneShot(dispenseSound);
            }

            Debug.Log("Dispnesed" + itemNames[index]);
        }
       else
        {
            Debug.Log("Not enough coins to buy: " + itemNames[index]);
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