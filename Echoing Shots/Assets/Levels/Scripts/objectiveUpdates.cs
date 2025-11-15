using System.Collections;
using UnityEngine;
using TMPro;

public class objectiveUpdates : MonoBehaviour
{
    [SerializeField] TMP_Text objective;
    [SerializeField] string text;
    public GameObject uiObject;
    public BoxCollider popUpWall;
    //[SerializeField] float textTime;

    //void start()
    //{ 

        
   // }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
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
        yield return new WaitForSeconds(3f);
        gameManager.instance.objectivePopup.SetActive(false);
        //Destroy(uiObject);
        Destroy(popUpWall);

    }
}
