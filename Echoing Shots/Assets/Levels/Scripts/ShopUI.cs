using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject itemButttonPrefab;
    [SerializeField] private Transform buttonContainer;

    public void BuildMenu(VendingMachine vendMachine)
    {
       
        for(int i = buttonContainer.childCount - 1; i>= 0; i--)
        {
            Destroy(buttonContainer.GetChild(i).gameObject);


        }
        int itemCount = vendMachine.ItemCount();

        for(int i = 0; i < itemCount; i++)
        {
            GameObject buttonObj = Instantiate(itemButttonPrefab, buttonContainer);

            TMP_Text tmpText = buttonObj.GetComponentInChildren<TMP_Text>();

            if (tmpText != null)
            {
                tmpText.text = vendMachine.GetItemName(i) + " - " + vendMachine.GetItemCost(i) + "coins";


                int index = i;

                buttonObj.GetComponent<Button>().onClick.AddListener(() =>
                {
                    vendMachine.DispenseItem(index);
                });

             
            
            }


        }
    }
    
    
 
}
