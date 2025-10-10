using UnityEngine;
using UnityEngine.UI;

public class playerController : MonoBehaviour , IDamage
{
    [SerializeField] LayerMask ignoreLayer;
    [SerializeField] CharacterController controller;

    [SerializeField] int HP;
    [SerializeField] int speed;
    [SerializeField] int sprintMod;
    [SerializeField] int jumpSpeed;
    [SerializeField] int jumpCountMax;
    [SerializeField] int gravity;

    [SerializeField] int shootDamage;
    [SerializeField] int shootDist;
    [SerializeField] float shootRate;



    Vector3 playerVel;
    Vector3 moveDir;

    int jumpCount;
    int HPOrig;

    float shootTimer;
    bool isSprinting;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HPOrig = HP;
        gameManager.instance.getHealthBar().value = HP;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * shootDist,Color.red);
        shootTimer += Time.deltaTime;
        movement();
        sprint();
    }

    void movement()
    {
        if(controller.isGrounded)
        {
            playerVel = Vector3.zero;
            jumpCount = 0;
        }
        else
        {
            playerVel.y -= gravity * Time.deltaTime;
        }

        moveDir = Input.GetAxis("Horizontal") * transform.right + Input.GetAxis("Vertical") * transform.forward;
        controller.Move(moveDir * speed * Time.deltaTime);

        jump();

        controller.Move(playerVel * Time.deltaTime);

        if(shootTimer >= shootRate)
        {
            gameManager.instance.getReticle().color = Color.red;
        }
        else
        {
            gameManager.instance.getReticle().color = Color.gray;

            
        }

        if (Input.GetButton("Fire1") && shootTimer >= shootRate)
        {
            shoot();
        }
    }

    void jump()
    {
        if(Input.GetButtonDown("Jump") && jumpCount < jumpCountMax)
        {
            playerVel.y = jumpSpeed;
            jumpCount++;
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
    void shoot()
    {
        shootTimer = 0;

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

    public void takeDamage(int amount)
    {
        HP -= amount;
        gameManager.instance.getHealthBar().value = HP;
        if (HP <= 0) 
        {
            gameManager.instance.getHealthBar().value = 0;
            gameManager.instance.youLose();
        }
        else
        {
            
        }
    }
}
