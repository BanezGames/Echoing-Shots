using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value;

    void Update()
    {

    }
    private void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (PlayerInventory.instance != null)
                PlayerInventory.instance.AddGold(value);

            Destroy(gameObject);
        }
    }
}
