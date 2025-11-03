using UnityEngine;

public class musicManager : MonoBehaviour
{
    private static musicManager instance;

    [SerializeField] AudioSource musicSource;
    [Range(0f, 1f)][SerializeField] float musicVol;
    [SerializeField] AudioSource ambientSource;
    [Range(0f, 1f)][SerializeField] float ambientVol;

    [SerializeField] AudioClip mainMenuMusic;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip ambientLoop;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }
    void Start()
    {
        PlayAmbient(ambientLoop);
        PlayMusic(mainMenuMusic);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayMusic(AudioClip Clip)
    {
        if (Clip == null) 
        { 
            return; 
        }

        musicSource.clip = Clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayAmbient(AudioClip Clip)
    {
        if (Clip == null)
        {
            return;
        }
        ambientSource.clip = Clip;
        ambientSource.loop = true;
        ambientSource.Play();
    }
}
