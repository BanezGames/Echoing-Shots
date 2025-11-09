using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class shopKeeper : MonoBehaviour
{
    [SerializeField] private List<GameObject> itemPrefabs;
    [SerializeField] private List<int> itemCosts;
    [SerializeField] private List<string> itemNames;
    [SerializeField] GameObject shopMenuUI;
    [SerializeField] private Transfrom spawnPoint;

    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            shopMenuUI.SetActive(true);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player")) playerInRange = true; 
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = false;
            shopMenuUI.SetActive(false);
        }
    }

    private void BuildMenu(int index)
    {
        Transform container = shopMenuUI.transform.Find("ButtonContainer");
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        for(int i = 0; i < itemPrefabs.Count; i++)
        {
            GameObject button = Instantiate(Resources.Load<GameObject>("ItemButton"), container);
            button.GetComponentInChildren<Text>().text = itemNames[i] + " - " + itemCosts + " coins";

            int indx = i;
            button.GetComponent<Button>().onClick.AddListener(() => BuyItem(indx));
        }
    }

    private void BuyItem(int index)
    {
        if(PlayerInventory.instance.Coins >= itemCosts[index])
        {
            PlayerInventory.instance.AddGold(-itemCosts[index]);
            Debug.Log("Bought: " + itemNames[index]);
        }
        else
        {
            Debug.Log("Not enough coins!");
        }

    }
}
