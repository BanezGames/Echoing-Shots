using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.UIElements;
using Unity.VisualScripting;

public class Story : MonoBehaviour
{
    [SerializeField] TMP_Text text;
    [SerializeField] GameObject imagePage;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    bool canSeePlayer;
    //// Update is called once per frame
    void Start()
    {
        canSeePlayer = false;
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact") && canSeePlayer)
        {
           
           
                removeRead();
            gameManager.instance.storyPopup.SetActive(false);
            showPage();
            
           

        }
        if (Input.GetButton("Fire1"))
        {
            showRead();
            removePage();
        }
        

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSeePlayer = true;
            StartCoroutine(showRead());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canSeePlayer = false;
            StartCoroutine(removeRead());
        }
    }

    //void showRead()
    //{
       
    //}

    IEnumerator showRead()
    {
        gameManager.instance.interactTipPub.SetActive(true);
        gameManager.instance.storyPopup.SetActive(true);
        yield return null;
    }
    IEnumerator removeRead()
    {
        gameManager.instance.interactTipPub.SetActive(false);
        gameManager.instance.storyPopup.SetActive(false);
        yield return null;
    }

    void showPage()
    {
        
        imagePage.SetActive(true);
    }

    void removePage() 
    {
        imagePage.SetActive(false );
    }
 

  
}
