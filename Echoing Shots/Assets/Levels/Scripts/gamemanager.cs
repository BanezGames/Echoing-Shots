using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject enemy;
    [SerializeField] TextMeshProUGUI Counter;
    [SerializeField] Slider HealthBar;
    [SerializeField] Image Reticle;


    [SerializeField] int maxItems;

    public GameObject player;
    public playerController playerScript;

    int gameItemCount; 
   
    
    
    public bool isPaused;

    float timeScaleOrig;

    int gameGoalCount;

    [SerializeField]int waveCount;

    [SerializeField] GameObject[] EnemiesList;
    [SerializeField] GameObject[] Spawners;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        instance = this;
        timeScaleOrig = Time.timeScale;

        Spawners = GameObject.FindGameObjectsWithTag("Spawner");
        player = GameObject.FindWithTag("Player");
        playerScript = player.GetComponent<playerController>();

        spawnEnemies();
        

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (menuActive == null)
            {
                statePause();
                menuActive = menuPause;
                menuActive.SetActive(true);
            }
            else if (menuActive == menuPause)
            {
                stateUnpause();
            }
        }
    }

    public void statePause()
    {
        isPaused = !isPaused;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void stateUnpause()
    {
        isPaused = !isPaused;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(false);
        menuActive = null;
    }

    public void updateGameGoal(int amount)
    {
        gameGoalCount += amount;

        if (gameGoalCount <= 1 && amount < 0)
        {
            
            if(gameGoalCount <= 0)
            {
                gameGoalCount = 0;
            }
            waveCount++;
            spawnEnemies();
            


        }
    }

    public void spawnEnemies()
    {
        //for (int i = 0; i < waveCount; i++)
        //{
            //int randPos = Random.Range(0, SpawnLocations.Length);
            //Instantiate(enemy, SpawnLocations[randPos], Quaternion.identity);
        //}
        for(int i = 0; i < Spawners.Length; i++)
        {
            Spawners[i].GetComponent<Spawner>().Spawning(1, 0, 0);
        }
    }

    public void updateItemGoal(int items)
    {
        gameItemCount += items;
        Counter.text = gameItemCount + "/" + maxItems;
        if (gameItemCount >= maxItems)
        {
            statePause();
            menuActive = menuWin;
            menuActive.SetActive(true);
            gameItemCount = 0;
        }
    }

    public void youLose()
    {
        statePause();
        menuActive = menuLose;
        menuActive.SetActive(true);
    }

    public Image getReticle()
    {
        return Reticle;
    }

    public Slider getHealthBar()
    {
        return HealthBar;
    }

    public GameObject[] getEnemyList()
    {
        return EnemiesList;
    }

}
