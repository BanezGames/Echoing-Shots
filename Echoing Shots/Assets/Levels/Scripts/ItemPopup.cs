using UnityEngine;
using System.Collections;
using TMPro;
using Unity.VisualScripting;
public class ItemPopup : MonoBehaviour
{
    [SerializeField] GameObject imagePage;
    [SerializeField] TMP_Text aboutText;
    [SerializeField] string text;
    public BoxCollider popUpWall;
    bool pageup;

    private void Start()
    {
        pageup = false;
    }
    private void Update()
    {
        if (Input.GetButton("Jump") && pageup)
        {
            gameManager.instance.itempopup.SetActive(false);
            pageup = false;
            gameManager.instance.stateUnpause();
            
            
            
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            aboutText.text = text;

            pageup = true;
            StartCoroutine(ItemFeedback());





        }
        ///objective.SetActive(false);
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(popUpWall);
        }
    }

    IEnumerator ItemFeedback()
    {
        pageup = true;
        gameManager.instance.itempopup.SetActive(true);
        gameManager.instance.statePause();
        
    
        yield return null;
    }
}
