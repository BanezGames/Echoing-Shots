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
        if (other.CompareTag("Player"))
        {
            objective.text = text;
            
            StartCoroutine(feedback());
            
            
        }
        ///objective.SetActive(false);
    }

    IEnumerator feedback()
    {
        gameManager.instance.objectivePopup.SetActive(true);
        yield return new WaitForSeconds(2f);
        gameManager.instance.objectivePopup.SetActive(false);

    }
}
