using TMPro;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory instance;

    [SerializeField] private TextMeshProUGUI coin;
    public int Coins 
    { 
        get; 
        private set; 
    }

    void Update()
    {

    }

    void Start()
    {
        UpdateUI();
    }

    void Awake()
    {
        if (instance != null && instance != this) 
        { 
            Destroy(gameObject); 
            return; 
        }
        instance = this;
    }

    public void AddGold(int amount)
    {
        Coins += amount;
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (coin != null)
            coin.text = Coins.ToString();
    }
}