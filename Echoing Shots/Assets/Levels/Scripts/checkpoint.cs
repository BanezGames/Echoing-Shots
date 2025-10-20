using UnityEngine;
using System.Collections;

public class checkpoint : MonoBehaviour
{
    [SerializeField] Renderer model;

    Color colorOrig;

    private void Start()
    {
        colorOrig = model.material.color;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            gameManager.instance.PlayerSpawnPos.transform.position = transform.position;
            StartCoroutine(feedback());
        }
    }

    IEnumerator feedback()
    {
        model.material.color = Color.red;
        gameManager.instance.checkpintPopup.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        gameManager.instance.checkpintPopup.SetActive(false);
        model.material.color = colorOrig;

    }
}
