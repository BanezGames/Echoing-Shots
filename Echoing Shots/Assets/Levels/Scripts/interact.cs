using UnityEngine;
using System.Collections;

public class interact : MonoBehaviour
{
    enum interactionType { door, chest, lever}

    [SerializeField] interactionType type;

    [SerializeField] GameObject[] turrets;
    
    [SerializeField] GameObject chestHinge;
    [SerializeField] int lidOpenSpeed;

    [SerializeField] bool keyRequired;

    int number;

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (type == interactionType.chest)
            {
                openChest();
            }
        }
    }

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

    private void openChest()
    {
        Quaternion rot = Quaternion.Euler(-135,0,0);
        chestHinge.transform.rotation = rot;

        // To Be implemented, not smoothly opening at the moment.
        //chestHinge.transform.rotation = Quaternion.Lerp(chestHinge.transform.rotation, rot, lidOpenSpeed*Time.deltaTime);

        this.GetComponent<BoxCollider>().enabled = false;
        gameManager.instance.hideInteraction();
    }
}
