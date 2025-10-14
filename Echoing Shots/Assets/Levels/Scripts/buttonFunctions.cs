using UnityEngine;
using UnityEngine.SceneManagement;

public class buttonFunctions : MonoBehaviour
{
    public void resume()
    {
        gameManager.instance.stateUnpause();
    }

    public void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        gameManager.instance.stateUnpause();
    }
    //save button in pause menu
    public void saveGame()
    {
        if (SaveManager.instance != null)
        {
            SaveManager.instance.SaveGame();
        }
    }

    //load button in main menu
    public void loadGame()
    {
        if (SaveManager.instance != null)
        {
            SaveManager.instance.LoadGame();
        }
    }
    public void quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
