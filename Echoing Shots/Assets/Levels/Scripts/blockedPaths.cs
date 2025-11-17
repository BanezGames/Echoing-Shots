using UnityEngine;
using System.Collections;

public class blockedPaths : MonoBehaviour
{
    enum blockerType { bridge, gate};
    [SerializeField] blockerType type;
    [SerializeField] GameObject blocker;
    [SerializeField] float rotMax;

    float openAmount;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (type == blockerType.gate)
        {
            openAmount = blocker.GetComponent<Collider>().bounds.size.y;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void openBlocker(float amount)
    {
        if (type == blockerType.bridge)
        {
            blocker.transform.localRotation = Quaternion.Euler((amount * rotMax), 0, 0);
        }

        if (type == blockerType.gate)
        {
            blocker.transform.position += Vector3.up * (amount/25);
        }
    }
}
