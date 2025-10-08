using System.Numerics;
using UnityEngine;

public class itemAI : MonoBehaviour, Iitem
{
    //first you need an item
    //then you have to have an inventory
    //item then has to be able to go in to iventory
    //then you have to see item in inventory
    //be able to hold item so hand postion
    int itemInInvetory;
    
    
    bool isInteractable;
    bool isInInve;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            Destroy(gameObject);
            gameManager.instance.updateItemGoal(1);
            //itemInInvetory++;
        }
    }

    public void isItem(GameObject item)
    {
        
    }
}
