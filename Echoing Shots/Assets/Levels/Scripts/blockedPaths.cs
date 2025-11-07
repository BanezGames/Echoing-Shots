using UnityEngine;

public class blockedPaths : MonoBehaviour
{
    [SerializeField] GameObject hinge;
    [SerializeField] float rotMax;

    float rotOrig;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rotOrig = this.transform.localRotation.eulerAngles.x;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void lowerBridge(float amount)
    {
        hinge.transform.localRotation = Quaternion.Euler((amount * rotMax),0,0);
    }
}
