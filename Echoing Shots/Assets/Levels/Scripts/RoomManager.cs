using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] GameObject[] Doors;

    [SerializeField] GameObject[] enemyList;
    [Range(0,10)][SerializeField] int[] maxEnemies;
    [SerializeField] int spawnRate;
    [SerializeField] Transform[] spawnPos;

    int enemyCount;

    float spawnTimer;

    bool startSpawning;
    bool hasEntered;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (startSpawning)
        {
            Debug.Log("Started");
            spawnTimer += Time.deltaTime;
            for(int i = 0;  i < enemyList.Length; i++)
            {
                Debug.Log("I: " + i);
                for(int j = 0; j < maxEnemies[i]; j++)
                {
                    Debug.Log("J: " + j);
                    if (spawnTimer >= spawnRate)
                    {
                        spawn(i);
                    }
                }
                
            }
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player") && !hasEntered)
        {
            startSpawning = true;
            hasEntered = true;
            doorState(true);

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
}
