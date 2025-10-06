using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour, IDamage
{
    [SerializeField] Renderer model;
    [SerializeField] int hp;

    Color originColor;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        originColor = model.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    

    public void takeDamage(int amount)
    {
        hp -= amount;
        if (hp <= 0)
        {
            Destroy(gameObject);
        }
        else 
        {
            StartCoroutine(flashRed());
        }
    }

    IEnumerator flashRed()
    {
        model.material.color = originColor;
        yield return new WaitForSeconds(0.1f);
        model.material.color = originColor;
    }
   
}
