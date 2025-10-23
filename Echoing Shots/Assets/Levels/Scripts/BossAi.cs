using UnityEngine;
using System.Collections;
using UnityEngine.AI;
using Unity.VisualScripting;
public class BossAi : MonoBehaviour , IDamage
{
    [SerializeField] Animator anim;
    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform headPos;
    [SerializeField] GameObject Item;

    [SerializeField] int HP;
    [SerializeField] int faceTargetSpeed;
    //[SerializeField] int FOV;
    //[SerializeField] int roamDist;
    //[SerializeField] int roamPauseTime;

    [SerializeField] bool isMelee;
    [SerializeField] float attackRange;

    [SerializeField] Transform shootPos;
    [SerializeField] Transform spawnerPos;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject DirectFireBolt;


    [SerializeField] float shootRate;
    [SerializeField] int dropChanceItem;
    [SerializeField] int dropChancePowerUp;
    [SerializeField] GameObject[] powerUpPrefabs;
    [SerializeField] GameObject[] enemyList;
    [SerializeField] int skyXRange;
    [SerializeField] int skyZRange;
    [SerializeField] int skyAmount;
    [SerializeField] int yAdd;

    Color colorOrig;

    float shootTimer;
    float roamTimer;
    float angleToPlayer;
    float stoppingDistOrig;

    bool playerInRange;

    Vector3 playerDir;
    Vector3 startingPos;

    //public RoomManager thisRoom;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        colorOrig = model.material.color;
        //thisRoom.updateEnemyCount(1);
        startingPos = transform.position;
        stoppingDistOrig = agent.stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {

        shootTimer += Time.deltaTime;

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
                    int rand = Random.Range(0, 3);
                    
                    switch (rand)
                    {
                        
                        case 0:
                            anim.SetTrigger("DirectAttack");
                            //shootDirectFireBall();

                            break;
                        case 1:
                            anim.SetTrigger("Spawn");
                            //spawnEnemy();
                            break;
                        case 2:
                            anim.SetTrigger("SkyAttack");
                            //skyAttack();
                            break;
                        default:
                            break;
                    }
                    shootTimer = 0;
                    
                }
            }
            
        }
     

        

    }

    void faceTarget()
    {
        Quaternion Rot = Quaternion.LookRotation(new Vector3(playerDir.x, playerDir.y, playerDir.z));
        shootPos.transform.rotation = Quaternion.Lerp(transform.rotation, Rot, Time.deltaTime * faceTargetSpeed);
    }

    public void shootDirectFireBall()
    {
        
        Instantiate(DirectFireBolt, shootPos.position, shootPos.transform.rotation);
    }

    public void spawnEnemy()
    {
        int randomEnemy = Random.Range(0, enemyList.Length);
        Instantiate(enemyList[randomEnemy], spawnerPos.position, Quaternion.identity);
    }

    public void skyAttack()
    {
        for(int i = 0; i < skyAmount; i++)
        {
            float xAdd = Random.Range(-skyXRange, skyXRange) + gameManager.instance.player.transform.position.x;
            float zAdd = Random.Range(-skyXRange, skyZRange) + gameManager.instance.player.transform.position.z;
            Instantiate(DirectFireBolt, new Vector3(xAdd, gameManager.instance.player.transform.position.y + yAdd, zAdd), Quaternion.Euler(90, 0, 0));
        }
    }



    public void takeDamage(int amount)
    {
        HP -= amount;
        agent.SetDestination(gameManager.instance.player.transform.position);

        if (HP <= 0)
        {
            Destroy(gameObject);
            //thisRoom.updateEnemyCount(-1);
            
            int randPowerUp = Random.Range(0, dropChancePowerUp);
            //Debug.Log(rand);
            

            if (randPowerUp == 0 && powerUpPrefabs.Length > 0)
            {
                Debug.Log("spawnHealth");
                int randPU = Random.Range(0, powerUpPrefabs.Length);
                Instantiate(powerUpPrefabs[randPU], transform.position, Quaternion.identity);
            }

            gameManager.instance.youWin();
        }
        else
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        model.material.color = Color.red;
        yield return new WaitForSeconds(0.3f);
        model.material.color = colorOrig;

    }

}
