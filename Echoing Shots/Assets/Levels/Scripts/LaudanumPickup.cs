using UnityEngine;

public class LaudanumPickup : MonoBehaviour
{
    public itemStats laudanum;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ConsumeLaudanum(other.GetComponent<playerController>());
        }
    }


    void ConsumeLaudanum(playerController player)
    {
        if (player != null)
        {
            gameManager.instance.playerSanityBar.fillAmount += 1.0f;
            Destroy(gameObject);
        }
    }
}
