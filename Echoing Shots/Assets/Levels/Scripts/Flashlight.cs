using UnityEngine;

public class Flashlight : MonoBehaviour
{
    
    public bool isFlashup;
    public bool isOn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFlashup = true;
            Destroy(gameObject);
        }
    }
}
