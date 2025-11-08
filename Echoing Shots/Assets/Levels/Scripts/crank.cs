using UnityEngine;

public class crank : MonoBehaviour
{
    [SerializeField] GameObject crankable;
    [SerializeField] GameObject wheel;
    [SerializeField] int crankSpeed;
    [SerializeField] float rotMax;

    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip crankSound;

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
            if (!audioSource.isPlaying)
            {
                audioSource.clip = crankSound;
                audioSource.loop = true;
                audioSource.Play();
            }
            rot += (crankSpeed * Time.deltaTime);
            turnCrank(rot);
            rotNorm = rot / rotMax;
            crankable.GetComponent<blockedPaths>().openBlocker(rotNorm);
        } 
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
        }

        if (rot >= rotMax)
        {
            canSeePlayer = false;
            this.GetComponent<BoxCollider>().enabled = false;
            gameManager.instance.hideInteraction();

            if(audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
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

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
            audioSource.loop = false;
        }
    }

    private void turnCrank(float rot)
    {
        wheel.transform.localRotation = Quaternion.Euler(0, 0, rot);
    }
}
