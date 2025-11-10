using UnityEngine;

public class sceneChange : MonoBehaviour, IRoomInterface
{
    [SerializeField] string sceneName;

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
        IInteract blackHole = other.GetComponent<IInteract>();
        if (blackHole != null) 
        {
            musicManager.instance.StopMusic();
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }

    public void roomEnded()
    {
        this.gameObject.SetActive(true);
    }

    public void roomStarted()
    {
        this.gameObject.SetActive(false);
    }
}
