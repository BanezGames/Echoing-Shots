using System.Collections;
using UnityEngine;
using TMPro;

public class objectiveUpdates : MonoBehaviour
{
    [SerializeField] TMP_Text objective;
    [SerializeField] string text;
    //[SerializeField] float textTime;

    //void start()
    //{ 

        
   // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && gameManager.instance.PlayerSpawnPos.transform.position != transform.position)
        {
            gameManager.instance.PlayerSpawnPos.transform.position = transform.position;
            objective.text = text;
            
            StartCoroutine(feedback());
            
            
            
            
            
        }
        ///objective.SetActive(false);
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player")) 
    //    { Destroy(gameObject); }
            
    //}

    IEnumerator feedback()
    {
        gameManager.instance.objectivePopup.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameManager.instance.objectivePopup.SetActive(false);

    }
}
