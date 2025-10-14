using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void start()
    {
        SceneManager.LoadScene(1);


    }
    // method for the load button
    public void loadGame()
    {
        if (SaveManager.instance != null && SaveManager.instance.HasSaveFile())
        {
            SaveManager.instance.LoadGame();
        }
        else
        {
            Debug.Log("No save file found!");
            // Optionally start a new game instead
            start();
        }
    }
}
