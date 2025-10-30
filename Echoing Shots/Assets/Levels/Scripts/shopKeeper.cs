using System.Numerics;
using UnityEngine;

public class shopKeeper : MonoBehaviour
{
   public class ShopItem
    {
        public GameObject item;
        public int cost;
        public string itemName;
    }

    [SerializeField] private ShopItem[] itemsForSale;


    public void BuyItem(int indx)
    {
        if (indx < 0 || indx >= itemsForSale.Length) return;
        
        ShopItem items = itemsForSale[indx];

        if(PlayerInventory.instance.Coins >= items.cost)
        {
            PlayerInventory.instance.AddGold(-items.cost);
            Debug.Log("Purchased: " +items.item.name);
        }
        else
        {
            Debug.Log("Not enough coins to buy " + items.item.name);
        }
    }

}
