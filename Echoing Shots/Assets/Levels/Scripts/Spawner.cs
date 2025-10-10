using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    enum enemyType { Easy, Normal, Strong, Mixed}

    [SerializeField] enemyType enemy;

    [SerializeField] List<GameObject> enemiesList;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        

    }

    // Update is called once per frame
    public void Spawning(int easy, int normal, int strong)
    {
        if (enemy == enemyType.Easy)
        {
            for(int i = 0; i < easy; i++)
            {
                Instantiate(enemiesList[0],transform.position, Quaternion.identity);
            }
        }
        else if (enemy == enemyType.Normal)
        {

        }
        else if (enemy == enemyType.Strong)
        {

        }
        else
        {

        }
    }
}
