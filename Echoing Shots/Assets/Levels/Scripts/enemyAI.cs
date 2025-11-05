using UnityEngine;
using System.Collections;
using UnityEngine.AI;
public class enemyAI : MonoBehaviour , IDamage
{
    [SerializeField] Animator anim;
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] GameObject Item;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;
    [SerializeField] int FOV;
    [SerializeField] int roamDist;
    [SerializeField] int roamPauseTime;

    [SerializeField] bool isMelee;
    [SerializeField] float attackRange;

    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;
    [SerializeField] int dropChanceItem;
    [SerializeField] int dropChancePowerUp;
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] GameObject meleeHitBox;


    Color colorOrig;

    float shootTimer;
    float roamTimer;
    float angleToPlayer;
    float stoppingDistOrig;

    bool playerInRange;

    Vector3 playerDir;
    Vector3 startingPos;

    public RoomManager thisRoom;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        thisRoom.updateEnemyCount(1);
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
        playerInRange = false;
    }

    // Update is called once per frame
    void Update()
    {
        
        anim.SetFloat("Movement", agent.velocity.normalized.magnitude);
        
        
        shootTimer += Time.deltaTime;
        
        if (agent.remainingDistance < 0.01f)
        {
            roamTimer += Time.deltaTime;
        }
        
        if (playerInRange && !canSeePlayer())
        {
            
            checkRoam();
        }
        
        if (!playerInRange)
        {
            
            
            checkRoam();
        }
    }

    void checkRoam()
    {
        if(roamTimer >= roamPauseTime && agent.remainingDistance < 0.01f)
        {
            roam();
        }
    }

    void roam()
    {
        roamTimer = 0;
        
        agent.stoppingDistance = 0;

        Vector3 ranPos = Random.insideUnitSphere * roamDist;
        ranPos += startingPos;

        NavMeshHit hit;
        NavMesh.SamplePosition(ranPos, out hit, roamDist, 1);
        agent.SetDestination(hit.position);
    }

    bool canSeePlayer()
    {
        playerDir = gameManager.instance.player.transform.position - headPos.position;
        angleToPlayer = Vector3.Angle(playerDir, transform.forward);
        Debug.DrawRay(headPos.position, playerDir, Color.red);

        RaycastHit hit;
        if(Physics.Raycast(headPos.position, playerDir, out hit))
        {
            

            if (angleToPlayer <= FOV && hit.collider.CompareTag("Player"))
            {
                agent.SetDestination(gameManager.instance.player.transform.position);
                if (shootTimer > shootRate && attackRange >= Vector3.Distance(transform.position,gameManager.instance.player.transform.position));
                {
                    if (anim != null)
                    {
                        anim.SetTrigger("Shoot");
                    }
                }
                if (agent.remainingDistance <= stoppingDistOrig)
                    faceTarget();
                agent.stoppingDistance = stoppingDistOrig;
                playerInRange = true;
                return true;
            }
        }
        
        agent.stoppingDistance = 0;
        return false;
    }

    void faceTarget()
    {
        Quaternion Rot = Quaternion.LookRotation(new Vector3(playerDir.x, 0, playerDir.z));
        transform.rotation = Quaternion.Lerp(transform.rotation, Rot, Time.deltaTime * faceTargetSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            playerInRange = false;
            
            agent.stoppingDistance = 0;
        }
    }


    void shoot() 
    {
        if(agent.remainingDistance <= attackRange)
        {
            shootTimer = 0;
            
            
            Instantiate(bullet, shootPos.position, transform.rotation);


        }
        




    }

    void meleeAttack()
    {
        shootTimer = 0;
        StartCoroutine(meleeAttackE(2));
    }

    public void takeDamage(int amount)
    {
        HP -= amount;
        agent.SetDestination(gameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            Destroy(gameObject);
            thisRoom.updateEnemyCount(-1);
            int rand = Random.Range(0, dropChanceItem);
            int randPowerUp = Random.Range(0, dropChancePowerUp);
            Debug.Log(rand);
            //if(rand == 0)
            //{
               // Instantiate(Item, transform.position, Quaternion.identity);
            //}
            
            if(randPowerUp == 0 && powerUpPrefabs.Length > 0)
            {
                Debug.Log("spawnHealth");
                int randPU = Random.Range(0, powerUpPrefabs.Length);
                Instantiate(powerUpPrefabs[randPU], transform.position, Quaternion.identity);
            }
        }
        else
        {
            StartCoroutine( flashRed());
        }
    }

    IEnumerator meleeAttackE(int duration)
    {
        meleeHitBox.SetActive(true);
        yield return new WaitForSeconds(duration);
        meleeHitBox.SetActive(false);
    }
    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        model.material.color = colorOrig;

    }

}
