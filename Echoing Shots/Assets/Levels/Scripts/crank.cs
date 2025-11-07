using UnityEngine;

public class crank : MonoBehaviour
{
    [SerializeField] GameObject crankable;
    [SerializeField] GameObject valve;
    [SerializeField] int crankSpeed;

    bool canSeePlayer;

    float rot;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canSeePlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Interact") && canSeePlayer)
        {
            rot += (crankSpeed * Time.deltaTime);
            turnCrank(rot);
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

    private void turnCrank(float rot)
    {
        valve.transform.localRotation = Quaternion.Euler(0, 0, rot);
    }
}
