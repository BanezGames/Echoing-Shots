using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour , IDamage, IInteract,IPickup
{
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] CharacterController controller;

    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] [Range(0.1f, 1.0f)] float crouchHeightMultiplier; 
    [SerializeField] int jumpSpeed;
    [SerializeField] int jumpCountMax;
    [SerializeField] int gravity;
    [SerializeField] int swimMod;
    [SerializeField] int sanityMax = 10;
    [SerializeField] int sanity;
    [SerializeField] float sanityDrainRate = 1.0f;
    [SerializeField] float sanityRecoveryRate = 2.0f;

    [SerializeField] List<gunStats> gunList = new List<gunStats>();
    
    [SerializeField] GameObject gunModel;
    
    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;
    [SerializeField] float shootRate;
    [SerializeField] bool isLosingSanity = true;

    [SerializeField] AudioSource aud;
    [SerializeField] AudioClip[] audSteps;
    [Range(0, 1)][SerializeField] float audStepsVol;
    [SerializeField] AudioClip[] audJump;
    [Range(0, 1)][SerializeField] float audJumpVol;
    [SerializeField] AudioClip[] audHurt;
    [Range(0, 1)][SerializeField] float audHurtVol;
    [SerializeField] AudioClip[] audDeath;
    [Range(0, 1)][SerializeField] float audDeathVol;

    Vector3 playerVel;
    Vector3 moveDir;

    int jumpCount;

    int HPOrig;
    int gravityOrig;
    int gunListPos;

    float shootTimer;
    bool isSprinting;
    bool isPlayingSteps;
    bool isUncrouching;
    

    public bool isSwimming;

    bool isInvincible;
    int damageOrig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPOrig = HP;
        spawnPlayer();
        gravityOrig = gravity;
        damageOrig = shootDamage;
        sanity = sanityMax;
        //gameManager.instance.playerHPBar = HP;
        updatePlayerUI();
       
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist,Color.red);
        shootTimer += Time.deltaTime;
        movement();
        sprint();
       

        if (isUncrouching)
        {
            RaycastHit hit;
            if (!Physics.Raycast(transform.position, Vector3.up, out hit, 1.5f))
            {
                isUncrouching = false;
                transform.localScale = new Vector3(transform.localScale.x, 1.0f, transform.localScale.z);
            }
        }
    }

    void movement()
    {
        if(controller.isGrounded)
        {
            if (moveDir.normalized.magnitude > 0.3f && !isPlayingSteps)
            {
                StartCoroutine(playStep());
            }
            playerVel = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            playerVel.y -= gravity * Time.deltaTime;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);
        crouch();

        if (!isSwimming)
        {
            jump();
        }
        else
        {
            swim();
        }

            controller.Move(playerVel * Time.deltaTime);

        if (shootTimer >= shootRate)
        {
            gameManager.instance.getReticle().color = Color.red;
        }
        else
        {
            gameManager.instance.getReticle().color = Color.gray;
        }

        if (Input.GetButton("Fire1") && gunList.Count > 0 && gunList[gunListPos].ammoCur > 0 && shootTimer >= shootRate)
        {
            shoot();
        }
        if (Input.GetButton("Slot1"))
        {
            gameManager.instance.SetOff();
            gameManager.instance.SetOnSlot(0);
        }
        else if (Input.GetButton("Slot2"))
        {
            gameManager.instance.SetOff();
            gameManager.instance.SetOnSlot(1);
        }
        else if (Input.GetButton("Slot3"))
        {
            gameManager.instance.SetOff();
            gameManager.instance.SetOnSlot(2);
        }
        if (gameManager.instance.itemInventory[gameManager.instance.selectedIndex] != null &&Input.GetButtonDown("Use"))
        {
            useItem();
        }


            selectGun();
        reload();
    }

    
    void jump()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            aud.PlayOneShot(audJump[Random.Range(0, audJump.Length)], audJumpVol);
            playerVel.y = jumpSpeed;
            jumpCount++;
        }
    }

    void swim()
    {
        if(Input.GetButtonDown("Jump"))
        {
            playerVel.y = jumpSpeed;
        }


    }

    void sprint() 
    {
        if (Input.GetButtonDown("Sprint"))
        {
            speed *= sprintMod;
        }
        else if (Input.GetButtonUp("Sprint"))
        {
            speed /= sprintMod;
        }
    }
    void crouch()
    {
        if (Input.GetButtonDown("Crouch"))
        {
            isUncrouching = false;
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * crouchHeightMultiplier, transform.localScale.z);

        }
        if (Input.GetButtonUp("Crouch"))
        {
            isUncrouching = true;
        }

    }
    void shoot()
    {
        shootTimer = 0;
        gunList[gunListPos].ammoCur--;
        aud.PlayOneShot(gunList[gunListPos].shootSound[Random.Range(0, gunList[gunListPos].shootSound.Length)], gunList[gunListPos].shootSoundVol);
        updatePlayerUI();

        RaycastHit hit;
        if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, shootDist, ~ignoreLayer))
        {
            IDamage dmg = hit.collider.GetComponent<IDamage>();
            if (dmg != null)
            {
                dmg.takeDamage(shootDamage);
            }
            Debug.Log(hit.collider.name);
        }
    }
    void reload()
    {
        if(Input.GetButtonDown("Reload") && gunList.Count>1)
        {
            gunList[gunListPos].ammoCur = gunList[gunListPos].ammoMax;
            updatePlayerUI();
        }
    }

    void useItem()
    {
        gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex]--;
        Heal(gameManager.instance.itemInventory[gameManager.instance.selectedIndex].Healing);
        if (gameManager.instance.itemInventory[gameManager.instance.selectedIndex].InvincDuration > 0) StartCoroutine(Shield(gameManager.instance.itemInventory[gameManager.instance.selectedIndex].InvincDuration));
        //Debug.Log("Test: " + gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex]);
        if (gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex] <=0)
        {
            gameManager.instance.clearSlot();
        }
        gameManager.instance.inventoryDurability[gameManager.instance.selectedIndex].text = gameManager.instance.itemDurabilityList[gameManager.instance.selectedIndex].ToString();
        updatePlayerUI();
        
    }
    public void Heal(int amount)
    {
        HP += amount;

        if (HP > HPOrig)
        {
            HP = HPOrig;
        }

        //gameManager.instance.getHealthBar().value = HP;
    }

    public IEnumerator Shield(int duration)
    {
        bool original = isInvincible;
        isInvincible = true;
        gameManager.instance.playerShieldScreen.SetActive(true);
        yield return new WaitForSeconds(duration);
        isInvincible = original;
        gameManager.instance.playerShieldScreen.SetActive(false);
    }

    public IEnumerator DamageBoost(int amount, int duration)
    {
        shootDamage = damageOrig * amount;
        yield return new WaitForSeconds(duration);
        shootDamage = damageOrig;
    }

    public void takeDamage(int amount)
    {
        if (isInvincible)
            return;

        HP -= amount;
        if (HP > 0)
        {
            aud.PlayOneShot(audHurt[Random.Range(0, audHurt.Length)], audHurtVol);
        }

        StartCoroutine( flashPlayerDmg());
        updatePlayerUI();

        if (HP <= 0) 
        {
            aud.PlayOneShot(audDeath[Random.Range(0, audDeath.Length)], audDeathVol);
            //gameManager.instance.playerHPBar.value = 0;
            gameManager.instance.youLose();
        }
        else
        {
            
        }
    }
    IEnumerator flashPlayerDmg()
    {
        gameManager.instance.playerDamageScreen.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        gameManager.instance.playerDamageScreen.SetActive(false);
    }

   
    public void getGunStats(gunStats gun) 
    {
        gunList.Add(gun);
        gunListPos = gunList.Count - 1;

        changeGun();

    }

    void changeGun()
    {
        shootDamage = gunList[gunListPos].shootDamage;
        shootDist = gunList[gunListPos].shootDist;
        shootRate = gunList[gunListPos].shootRate;

        gunModel.GetComponent<MeshFilter>().sharedMesh = gunList[gunListPos].gunModel.GetComponent<MeshFilter>().sharedMesh;
        gunModel.GetComponent<MeshRenderer>().sharedMaterial = gunList[gunListPos].gunModel.GetComponent<MeshRenderer>().sharedMaterial;

        updatePlayerUI();
    }
   
    void selectGun()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0 && gunListPos <gunList.Count - 1)
        {
            gunListPos++;
            changeGun();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0 && gunListPos > 0)
        {
            gunListPos--;
            changeGun();
        }
    }

    public void updatePlayerUI()
    {
        gameManager.instance.playerHPBar.fillAmount = (float)HP / HPOrig;
        //gameManager.instance.getHealthBar().value = HP;

        if (gunList.Count > 0)
        {
            gameManager.instance.ammoCur.text = gunList[gunListPos].ammoCur.ToString("F0");
            gameManager.instance.ammoMax.text = gunList[gunListPos].ammoMax.ToString("F0");
        }

        gameManager.instance.playerSanityBar.fillAmount = (float)sanity / sanityMax;
    }
    public void spawnPlayer()
    {
        controller.transform.position = gameManager.instance.PlayerSpawnPos.transform.position;
        HP = HPOrig;
        updatePlayerUI();
    }

    IEnumerator playStep()
    {
        isPlayingSteps = true;
        aud.PlayOneShot(audSteps[Random.Range(0, audSteps.Length)], audStepsVol);

        if (isSprinting)
        {
            yield return new WaitForSeconds(0.3f);
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
        }

        isPlayingSteps = false;
    }

    IEnumerator sanityDrain()
    {
        while (true)
        {
            if(isLosingSanity)
            {
                sanity -= Mathf.RoundToInt(sanityDrainRate * Time.deltaTime);
                sanity = Mathf.Clamp(sanity, 0, sanityMax);
            
                
                if(sanity <= sanityMax * 0.25f)
                {
                    //Here is where we put the effect of the window going dark and Samuel hearing voices//
                }

                if(sanity <= 0)
                {
                    takeDamage(5);
                }
            }

            updatePlayerUI();
            yield return null;
        }
    }


    public void RetstoreSanity(int amount)
    {
        sanity += amount;
        sanity = Mathf.Clamp(sanity, 0, sanityMax);
        updatePlayerUI();
    }



}
