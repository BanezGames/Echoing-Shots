using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;
    private string savePath;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        savePath = Path.Combine(Application.persistentDataPath, "savegame.json");
    }

    public void SaveGame()
    {
        if (gameManager.instance == null || gameManager.instance.player == null)
            return;

        Vector3 playerPos = gameManager.instance.player.transform.position;
        int playerHP = gameManager.instance.playerScript.GetHealth();
        int coins = PlayerInventory.instance != null ? PlayerInventory.instance.Coins : 0;
        int itemCount = gameManager.instance.GetCurrentItemCount();
        int wave = gameManager.instance.GetCurrentWaveCount();
        string currentScene = SceneManager.GetActiveScene().name;

        saveSystem saveData = new saveSystem(playerPos, playerHP, coins, itemCount, wave, currentScene);

        string json = JsonUtility.ToJson(saveData, true);
        File.WriteAllText(savePath, json);

        Debug.Log("Game Saved!");
    }

    public bool LoadGame()
    {
        if (!File.Exists(savePath))
        {
            Debug.Log("No save file found!");
            return false;
        }

        string json = File.ReadAllText(savePath);
        saveSystem saveData = JsonUtility.FromJson<saveSystem>(json);

        // Load the saved scene
        SceneManager.LoadScene(saveData.SceneName);

        // Apply the loaded data after scene loads
        StartCoroutine(ApplyLoadedData(saveData));

        Debug.Log("Game Loaded!");
        return true;
    }

    private System.Collections.IEnumerator ApplyLoadedData(saveSystem saveData)
    {
        // Wait for scene to load
        yield return new WaitForSeconds(0.1f);

        // Apply loaded data
        if (gameManager.instance != null && gameManager.instance.player != null)
        {
            gameManager.instance.player.transform.position = saveData.playerPosition;
            gameManager.instance.playerScript.SetHealth(saveData.playerHealth);
            gameManager.instance.SetItemCount(saveData.gameItemCount);
            gameManager.instance.SetWaveCount(saveData.waveCount);
        }

        if (PlayerInventory.instance != null)
        {
            PlayerInventory.instance.Setcoins(saveData.playerCoins);
        }
    }

    public bool HasSaveFile()
    {
        return File.Exists(savePath);
    }
}