using UnityEngine;
using TMPro;

public class Story : MonoBehaviour
{
    //[SerializeField] GameObject imagePage;
    [SerializeField] int readIndex;

    bool canSeePlayer;
    bool showText;
    void Start()
    {
        canSeePlayer = false;
        showText = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canSeePlayer)
        {
            gameManager.instance.statePause();
            showPage();
        }
        if (Input.GetButton("Cancel") && showText)
        {
            gameManager.instance.stateUnpause();
            removePage();
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        IInteract story = other.GetComponent<IInteract>();
        canSeePlayer = true;

        gameManager.instance.showInteraction(4);
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hideInteraction();
        canSeePlayer = false;
    }

    void showPage()
    { 
        gameManager.instance.readPage(readIndex);
        showText = true;
    }

    void removePage() 
    {
        gameManager.instance.hidePage(readIndex);
        showText = false;
    }
 

  
}
