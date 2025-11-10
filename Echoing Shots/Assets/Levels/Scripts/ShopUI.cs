using UnityEngine.UI;
using UnityEngine;


public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private GameObject itemButttonPrefab;
    [SerializeField] private Transform buttonContainer;

    public void BuildMenu(vendingMachine vendMachine)
    {
        foreach (Transfrom child in buttonContainer)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < buttonContainer.childCount; i++)
        {
            GameObject buttonObj = Instantiate(itemButttonPrefab, buttonContainer);
            Text buttonText = buttonObj.GetComponentInChildren<Text>();
            buttonText.text = vendMachine.GetItemName(i) + " - " + vendMachine.GetItemCost(i);


            int index = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => vendMachine.DispenseItem(index));
        }
    }
    
    
 
}
