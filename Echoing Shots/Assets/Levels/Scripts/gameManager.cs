using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    [SerializeField] GameObject menuActive;
    [SerializeField] GameObject menuPause;
    [SerializeField] GameObject menuWin;
    [SerializeField] GameObject menuLose;
    [SerializeField] GameObject flash;
    [SerializeField] GameObject VendingMenu;

    [SerializeField] GameObject interactTip;
    [SerializeField] GameObject interactDoor;
    [SerializeField] GameObject interactChest;
    [SerializeField] GameObject interactLever;
    [SerializeField] GameObject interactCrank;
    [SerializeField] GameObject readPage;
    
    public VendingMachine currentVendingMachine;

    [SerializeField] GameObject enemy;
    [SerializeField] TextMeshProUGUI Counter;
    //[SerializeField] Slider HealthBar;
    public Image playerHPBar;
    public Image playerSanityBar;
    public Image castCooldown;
    public GameObject interactTipPub;
    public GameObject playerDamageScreen;
    public GameObject playerShieldScreen;
    public TMP_Text gameGoalCountText;
    public TMP_Text ammoCur, ammoMax;
    public GameObject checkpointPopup;
    public GameObject objectivePopup;
    public GameObject storyPopup;
    public GameObject itempopup;
    public itemStats[] itemInventory;
    public List<GameObject> InventorySlotsImage = new List<GameObject>();
    public List<TMP_Text> inventoryDurability = new List<TMP_Text>();
    public int[] itemDurabilityList;
    
    [SerializeField] Image Reticle;

    [SerializeField] AudioSource audUI;
    [SerializeField] AudioClip pauseSound;
    [SerializeField] AudioClip unpausedSound;
    [Range(0f, 1f)][SerializeField] float uiVol;


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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
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
        if (isPaused)
            return;
        isPaused = true;
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        if (audUI != null && pauseSound != null)
        {
            audUI.PlayOneShot(pauseSound, uiVol);
        }
    }

    public void stateUnpause()
    {
        if (!isPaused)
            return;

        isPaused = false;
        Time.timeScale = timeScaleOrig;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        menuActive.SetActive(false);
        menuActive = null;

        if (audUI != null && unpausedSound != null)
        {
            audUI.PlayOneShot(unpausedSound, uiVol);
        }
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
        interactCrank.SetActive(false);
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

    public void OpenVendingMenu(VendingMachine vm)
    {
        statePause();
        currentVendingMachine  = vm;

        menuActive = VendingMenu;
        menuActive.SetActive(true);
    }

    public void CloseVendingMenu(VendingMachine vm)
    {
        stateUnpause();
        currentVendingMachine = null;
    }

    public void onLoadnewScene()
    {
        PlayerSpawnPos = GameObject.FindWithTag("Player Spawn Pos");
        player.transform.position = PlayerSpawnPos.transform.position;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        onLoadnewScene();
        
    }

}
