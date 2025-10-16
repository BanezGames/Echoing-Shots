using UnityEngine;
using System.Collections;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] Doors;

    [SerializeField] GameObject[] enemyList;
    [Range(1,10)][SerializeField] int[] maxEnemies;
    [SerializeField] int spawnRate;
    [SerializeField] Transform[] spawnPos;

    int enemyCount;

    float spawnTimer;

    bool startSpawning;
    bool hasEntered;
    bool spawnNext;
    int enemyListIndex, enemyAmount;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !hasEntered)
        {
            
            hasEntered = true;
            doorState(true);   
            StartCoroutine(startSpawningEnemies());
           
            

        }

    }

    void spawn(int index)
    {
        int arrayPos = Random.Range(0, spawnPos.Length);

        GameObject tempEnemy = Instantiate(enemyList[index], spawnPos[arrayPos].position, spawnPos[arrayPos].rotation);
        tempEnemy.GetComponent<enemyAI>().thisRoom = this;
        enemyCount++;
        spawnTimer = 0;

    }

    void doorState(bool state)
    {
        //door1.SetActive(state);
        //door2.SetActive(state);
    }

    public void updateEnemyCount(int amount)
    {
        enemyCount += amount;

        if (enemyCount <= 0)
        {
            doorState(false);
            startSpawning = false;
        }
    }

    IEnumerator startSpawningEnemies()
    {
        yield return new WaitForSeconds(spawnRate);
        spawn(enemyListIndex);
        enemyAmount++;
        if(enemyAmount >= maxEnemies[enemyListIndex])
        {
            enemyAmount = 0;
            enemyListIndex++;
        }
        if(enemyListIndex>= enemyList.Length)
        {
            startSpawning = false;
        }
        else
        {
            StartCoroutine(startSpawningEnemies());
        }
        
        
        

    }
}
