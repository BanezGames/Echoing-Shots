using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class weepingAngel : MonoBehaviour
{
    public NavMeshAgent AI;
    public Transform player;

    Vector3 dest;

    public Camera playerCam, jumpscareCam;
    public float AISpeed, catchDistance, jumpscareTime;

    private void Start()
    {
        player = gameManager.instance.player.transform;
        playerCam = Camera.main;
        
    }
    private void Update()
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(playerCam);
        float distance = Vector3.Distance(transform.position, player.position);

        if (GeometryUtility.TestPlanesAABB(planes,this.gameObject.GetComponent<Renderer>().bounds))
        {
            AI.speed = 0;
            AI.SetDestination(transform.position);
        }
        if (!GeometryUtility.TestPlanesAABB(planes, this.gameObject.GetComponent<Renderer>().bounds))
        {
            AI.speed = AISpeed;
            dest = player.position;
            AI.destination = dest;

            //if (distance <= catchDistance)
            //{
                //jumpscareCam.gameObject.SetActive(true);
                //gameManager.instance.playerScript.takeDamage(100);
                //player.gameObject.SetActive(false);
                
                //StartCoroutine(killPlayer());
                //gameManager.instance.youLose();
            //}
        }
        
    }

    IEnumerator killPlayer()
    {
        yield return new WaitForSeconds(jumpscareTime);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.stateUnpause();
    }
}
