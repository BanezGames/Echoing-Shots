using Unity.VisualScripting;
using UnityEngine;
using System.Collections.Generic;

public class vendingMachine : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabItems;
    [SerializeField] private List<int>[] itemCosts;
    [SerializeField] private string[] itemNames;
    [SerializeField] private Transform dispensePoint;
    [SerializeField] private int itemindexToSell = 0;
     

    private bool isPlayerinRange = false;



    public void Update()
    {
        if (isPlayerinRange && Input.GetKeyDown(KeyCode.E))
        {
            BuyItem(itemindexToSell);
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerinRange = true;
            Debug.Log("Press E to buy" + itemNames[itemindexToSell]);
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            isPlayerinRange=false;
        }
    }


    public void BuyItem(int index)
    {
        if (index < 0 || index >= prefabItems.Length)
            return;

        
        GameObject item = prefabItems[index];
        List<int> list = itemCosts[index];
   
        if(PlayerInventory.instance.Coins >= index)
        {
            PlayerInventory.instance.AddGold(-index);
            Instantiate(item, dispensePoint.position, Quaternion.identity);
            Debug.Log("Now dispensing: " + itemNames[index]);
        }
        else
        {
            Debug.Log("Not enough coins to buy: " + itemNames[index]);
        }

    }
}
