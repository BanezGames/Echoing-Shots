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
    [SerializeField] GameObject flash;

    [SerializeField] GameObject interactTip;
    [SerializeField] GameObject interactDoor;
    [SerializeField] GameObject interactChest;
    [SerializeField] GameObject interactLever;
    [SerializeField] GameObject interactCrank;
    [SerializeField] GameObject readPage;
    public List<GameObject> InventorySlotsImage = new List<GameObject>();
    public List<TMP_Text> inventoryDurability = new List<TMP_Text>();

    [SerializeField] GameObject enemy;
    [SerializeField] TextMeshProUGUI Counter;
    //[SerializeField] Slider HealthBar;
    public Image playerHPBar;
    public GameObject interactTipPub;
    public GameObject playerDamageScreen;
    public GameObject playerShieldScreen;
    public TMP_Text gameGoalCountText;
    public TMP_Text ammoCur, ammoMax;
    public GameObject checkpointPopup;
    public GameObject objectivePopup;
    public GameObject storyPopup;
    public itemStats[] itemInventory;
    public int[] itemDurabilityList;
    
    [SerializeField] Image Reticle;


    [SerializeField] int maxItems;

    public GameObject player;
    public playerController playerScript;
    public GameObject PlayerSpawnPos;

    public int keyCount;

    int gameItemCount; 
   
    
    
    public bool isPaused;
    public bool isOn;
    public int selectedIndex;

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

        PlayerSpawnPos = GameObject.FindWithTag("Player Spawn Pos");
        selectedIndex = 0;
        itemInventory = new itemStats[3];
        itemDurabilityList = new int[3];
        spawnEnemies();
        isOn = false;


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
        flashLight();
    }

    public void SetOff()
    {
        InventorySlotsImage[0].GetComponent<Outline>().enabled = false;
        InventorySlotsImage[1].GetComponent<Outline>().enabled = false;
        InventorySlotsImage[2].GetComponent<Outline>().enabled = false;

    }
    public void SetOnSlot(int index)
    {
        InventorySlotsImage[index].GetComponent<Outline>().enabled = true;
        selectedIndex = index;
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
        gameGoalCountText.text = gameGoalCount.ToString("F0");

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

    public void youWin()
    {
        statePause();
        menuActive = menuWin;
        menuActive.SetActive(true);
    }

    public Image getReticle()
    {
        return Reticle;
    }

    //public Slider getHealthBar()
    //{
    //    return HealthBar;
    //}

    public GameObject[] getEnemyList()
    {
        return EnemiesList;
    }

    public void showInteraction(int action)
    {
        switch (action)
        {
            case 0:
                {
                    interactTip.SetActive(true);
                    interactDoor.SetActive(true);
                    break;
                }
            case 1:
                {
                    interactTip.SetActive(true);
                    interactChest.SetActive(true);
                    break;
                }
            case 2:
                {
                    interactTip.SetActive(true);
                    interactLever.SetActive(true);
                    break;
                }
            case 3:
                {
                    interactTip.SetActive(true);
                    interactCrank.SetActive(true);
                    break;
                }
            default:
                break;
        }
    }

    public void hideInteraction()
    {
        interactTip.SetActive(false);
        interactDoor.SetActive(false);
        interactChest.SetActive(false);
        interactLever.SetActive(false);
    }

    public void clearSlot()
    {
        InventorySlotsImage[gameManager.instance.selectedIndex].GetComponent<RawImage>().texture = null;
        //InventorySlotsImage[selectedIndex] = null;
        itemInventory[selectedIndex] = null;
        itemDurabilityList[selectedIndex] = 0;
    }
    void flashLight()
    {

        if (Input.GetButtonDown("f"))
        {

            if (!isOn)
            {
                flash.SetActive(true);
                isOn = true;
            }
            else if (isOn)
            {
                flash.SetActive(false);
                isOn = false;
            }


        }

    }

}
