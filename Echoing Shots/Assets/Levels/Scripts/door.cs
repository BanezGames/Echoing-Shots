using UnityEngine;

public class door : MonoBehaviour
{
    [SerializeField] GameObject doorCollider;
    [SerializeField] GameObject doorHingeLeft;
    [SerializeField] GameObject doorHingeRight;
    [SerializeField] bool keyRequired;

    bool canSeePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSeePlayer = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Interact") && canSeePlayer)
        {
            openDoor();
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        IInteract door = other.GetComponent<IInteract>();
        if (door != null)
        {
            gameManager.instance.showInteraction(0);
            canSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hideInteraction();
        canSeePlayer = false;
    }

    private void openDoor()
    {
        if (!keyRequired) // add an OR for how to track if the player has a Key in their inventory.
        {
            doorHingeLeft.transform.localRotation = Quaternion.Euler(0, 100, 0);
            doorHingeRight.transform.localRotation = Quaternion.Euler(0, -100, 0);

            this.GetComponent<BoxCollider>().enabled = false;
            doorCollider.GetComponent<BoxCollider>().enabled = false;
            gameManager.instance.hideInteraction();
        }
    }
}
