using UnityEngine;
using System.Collections;

public class interact : MonoBehaviour
{
    enum interactionType { door, chest, lever}

    [SerializeField] interactionType type;

    int number;

    private void OnTriggerEnter(Collider other)
    {
        IInteract action = other.GetComponent<IInteract>();

        if (action != null)
        {
            if (type == interactionType.door)
            {
                number = 0;
            }
            else if(type == interactionType.chest)
            {
                number = 1;
            }
            else if(type == interactionType.lever)
            {
                number = 2;
            }
            
            gameManager.instance.showInteraction(number);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hideInteraction();
    }
}
