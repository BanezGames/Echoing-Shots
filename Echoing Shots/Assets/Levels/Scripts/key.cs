using UnityEngine;

public class key : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameManager.instance.keyCount++;
            Destroy(gameObject);
        }
    }
}
