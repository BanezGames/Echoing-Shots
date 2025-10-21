using UnityEngine;
using UnityEngine.AI;

public class turretAI : MonoBehaviour
{
    [SerializeField] Transform shootPos;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;

    public bool isActive;

    float shootTimer;

    Vector3 playerDir;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if (isActive)
        {
            canSeePlayer();
        }
    }

    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - shootPos.position;
        Debug.DrawRay(shootPos.position, playerDir, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(shootPos.position, playerDir, out hit))
        {
            if (hit.collider.CompareTag("Player"))
            {
                faceTarget();
                if (shootTimer > shootRate)
                {
                    shoot();
                }
            }
            return true;
        }
        return false;
    }

    void shoot()
    {
            shootTimer = 0;
            Instantiate(bullet, shootPos.position, shootPos.transform.rotation);
    }

    void faceTarget()
    {
        Quaternion Rot = Quaternion.LookRotation(new Vector3(playerDir.x, playerDir.y, playerDir.z));
        shootPos.transform.rotation = Quaternion.Lerp(transform.rotation, Rot, Time.deltaTime * faceTargetSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isActive = true;
            this.GetComponent<SphereCollider>().enabled = false;
        }
    }
}
