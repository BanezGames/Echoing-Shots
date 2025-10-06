using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class enemyAI : MonoBehaviour, IDamage
{


    [SerializeField] Renderer model;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] int hp;

    [SerializeField] Transform shootPos;
    [SerializeField] GameObject bullet;
    [SerializeField] float shootRate;



    Color originColor;

    float shootTimer;
    bool playerInRange;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originColor = model.material.color;

     // gamemanager.instance.updateGameGoal(1);//
    }

    // Update is called once per frame
    void Update()
    {
        shootTimer += Time.deltaTime;
        if (playerInRange)
        {
            agent.SetDestination(gamemanager.instance.player.transform.position);
            if (shootTimer > shootRate)
            {
                shoot();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }


    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playerInRange= false;
        }
    }
    public void shoot()
    {
        shootTimer = 0;
        Instantiate(bullet, shootPos.position, Quaternion.identity);    
    }
    


    
    public void takeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        else 
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        model.material.color = originColor;
        yield return new WaitForSeconds(0.1f);
        model.material.color = originColor;
    }
   
}
