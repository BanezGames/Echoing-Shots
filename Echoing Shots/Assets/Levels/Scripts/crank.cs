using UnityEngine;

public class crank : MonoBehaviour
{
    [SerializeField] GameObject crankable;
    [SerializeField] GameObject wheel;
    [SerializeField] int crankSpeed;
    [SerializeField] float rotMax;

    bool canSeePlayer;

    float rot;
    float rotNorm;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSeePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact") && canSeePlayer && rot < rotMax)
        {
            rot += (crankSpeed * Time.deltaTime);
            turnCrank(rot);
            rotNorm = rot / rotMax;
            crankable.GetComponent<blockedPaths>().openBlocker(rotNorm);
        }

        if(rot >= rotMax)
        {
            this.GetComponent<BoxCollider>().enabled = false;
            gameManager.instance.hideInteraction();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        IInteract crank = other.GetComponent<IInteract>();

        if (crank != null)
        {
            gameManager.instance.showInteraction(3);
            canSeePlayer = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        gameManager.instance.hideInteraction();
        canSeePlayer = false;
    }

    private void turnCrank(float rot)
    {
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rot);
    }
}
