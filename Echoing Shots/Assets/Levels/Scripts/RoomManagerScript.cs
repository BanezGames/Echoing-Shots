using UnityEngine;

public class RoomManagerScript : MonoBehaviour
{


    [SerializeField] GameObject[] EnemiesList;
    [SerializeField] bool hasSpawned;
    [Range(0, 10)] public int[] EnemyAmounts;

    Transform[] spawnPoints;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        spawnPoints = new Transform[transform.childCount];
        for(int i = 0; i < spawnPoints.Length; i++)
        {
            spawnPoints[i] = transform.GetChild(i);
        }
        //SpawnEnemies();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemies()
    {
        hasSpawned = true;
        int randomPos;
        for(int i = 0; i < EnemiesList.Length; i++)
        {
            for(int j = 0; j < EnemyAmounts[i]; j++)
            {
                randomPos = Random.Range(0, spawnPoints.Length - 1);
                Instantiate(EnemiesList[i], spawnPoints[randomPos].position, Quaternion.identity);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasSpawned && other.gameObject.CompareTag("Player"))
        {
            SpawnEnemies();
        }
    }

}
