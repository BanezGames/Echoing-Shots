using UnityEngine;

public class floorTrigger : MonoBehaviour
{
    [SerializeField] GameObject[] Gates;


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
        IInteract tile = other.GetComponent<IInteract>();
        if (tile != null) 
        {
            Gates[0].gameObject.GetComponent<gateAI>().openGate();

        }
    }
}
