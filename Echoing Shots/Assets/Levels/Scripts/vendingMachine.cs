using UnityEngine;

public class vendingMachine : MonoBehaviour
{
    [SerializeField] private GameObject[] prefabItems;
    [SerializeField] private int itemCosts;
    [SerializeField] private string[] itemNames;
    [SerializeField] private Transform dispensePoint;


    public void BuyItem(int coins)
    {
        if (coins < 0 || coins >= prefabItems.Length)
            return;

        int cost = itemCosts * (coins - 1);
        GameObject item = prefabItems[coins];

        if(PlayerInventory.instance.Coins >= cost)
        {
            PlayerInventory.instance.AddGold(-cost);
            Instantiate(item, dispensePoint.position, Quaternion.identity);
            Debug.Log("Now dispensing: " + itemNames[coins]);
        }
        else
        {
            Debug.Log("Not enough coins to buy: " + itemNames[coins]);
        }

    }
}
