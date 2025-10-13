using UnityEngine;

public class turretAI : MonoBehaviour
{
    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;

    public bool isActive;

    float shootTimer;

    Vector3 player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;

        if(isActive)
        {
            player = gameManager.instance.player.transform.position;
            shoot();
        }
    }

    void shoot()
    {
        //Quaternion heading = Quaternion.LookRotation(new Vector3(player.x, player.y, player.z));
        Quaternion heading = Quaternion.LookRotation(player, Vector3.up);
        shootTimer = 0;
        Instantiate(bullet, shootPos.position, heading);
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
