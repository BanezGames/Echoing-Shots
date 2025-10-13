using UnityEngine;

public class lever : MonoBehaviour
{
    [SerializeField] GameObject[] traps;
    [SerializeField] GameObject leverHandle;

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
            pullLever();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteract lever = other.GetComponent<IInteract>();
        if (lever!=null)
        {
            gameManager.instance.showInteraction(2);
            canSeePlayer=true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hideInteraction();
        canSeePlayer = false;
    }

    private void pullLever()
    {
        Quaternion rot = Quaternion.Euler(45, 0, 0);
        leverHandle.transform.localRotation = rot;

        for (int i = 0; i < traps.Length; i++)
        {
            traps[i].gameObject.GetComponent<turretAI>().isActive = false;
        }

        this.GetComponent<BoxCollider>().enabled = false;
        gameManager.instance.hideInteraction();
    }
}
