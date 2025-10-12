using Unity.VisualScripting;
using UnityEngine;

public class chest : MonoBehaviour
{

    [SerializeField] GameObject chestHinge;
    [SerializeField] int lidOpenSpeed;

    bool canSeePlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSeePlayer = false;   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canSeePlayer)
        {
            openChest();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteract chest = other.GetComponent<IInteract>();
        if (chest != null)
        {
            gameManager.instance.showInteraction(1);
            canSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hideInteraction();
        canSeePlayer = false;
    }

    private void openChest()
    {
        Quaternion rot = Quaternion.Euler(-135, 0, 0);
        chestHinge.transform.localRotation = rot;

        // To Be implemented, not smoothly opening at the moment.
        //chestHinge.transform.rotation = Quaternion.Lerp(chestHinge.transform.rotation, rot, lidOpenSpeed*Time.deltaTime);

        this.GetComponent<BoxCollider>().enabled = false;
        gameManager.instance.hideInteraction();
    }
}
