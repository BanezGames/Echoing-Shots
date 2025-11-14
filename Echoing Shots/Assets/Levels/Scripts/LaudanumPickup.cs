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
            player.RestoreSanity(10);
            Destroy(gameObject);
        }
    }
}
