using UnityEngine;
using UnityEngine.SceneManagement;

public class startGame : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        if (musicManager.instance != null)
        {
            musicManager.instance.StopMusic();

        }
        SceneManager.LoadScene(1);
    }
}
