using UnityEngine.UI;
using UnityEngine;
using TMPro;


public class I : MonoBehaviour
{
    [SerializeField] private GameObject itemButttonPrefab;
    [SerializeField] private Transform buttonContainer;

    public GameObject MenuPanel;
    public GameObject buttonPrefab;

    private vendingMachine currentMachine;

    public void OpenMenu(vendingMachine machine)
    {
        currentMachine = machine;
        MenuPanel.SetActive(true);
        BuildMenu();

    }

    public void CloseMenu()
    {
        MenuPanel.SetActive(false);
        currentMachine = null;
    }

    public void BuildMenu()
    {
        foreach (Transfrom child in buttonContainer)
        {
            Destroy(gameObject);
        }

        for (int i = 0; i < buttonContainer.childCount; i++)
        {
            GameObject buttonObj = Instantiate(itemButttonPrefab, buttonContainer);

            TMP_Text label = buttonObj.GetComponent<TMP_Text>();
            if (label != null)
            {
                label.text = currentMachine.GetItemName(i);
            }

            int index = i;
            buttonObj.GetComponent<Button>().onClick.AddListener(() => currentMachine.DispenseItem(index));
        }
    }
    
    
 
}
