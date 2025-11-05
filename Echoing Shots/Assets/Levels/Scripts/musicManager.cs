using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class musicManager : MonoBehaviour
{
    public static musicManager instance;

    [SerializeField] AudioSource musicSource;
    [Range(0f, 1f)][SerializeField] float musicVol;
    [SerializeField] AudioSource ambientSource;
    [Range(0f, 1f)][SerializeField] float ambientVol;

    [SerializeField] AudioClip[] mainMenuMusic;
    [Range(0f, 1f)][SerializeField] float mainMenuMusicVol;
    [SerializeField] AudioClip gameMusic;
    [SerializeField] AudioClip ambientLoop;

    int currentMusicIndex = 0;

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
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void Start()
    {
        PlayAmbient(ambientLoop);
        PlayMusic(mainMenuMusic[currentMusicIndex]);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentMusicIndex = scene.buildIndex;

        
        if (scene.buildIndex == 0)
        {
            PlayMusic(mainMenuMusic[0]);
        }
        else if (scene.buildIndex == 1)
        {
            PlayMusic(gameMusic);
        }
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
        musicSource.volume = musicVol;
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
        ambientSource.volume = ambientVol;
        ambientSource.loop = true;
        ambientSource.Play();
    }
    public void StopMusic()
    {
        musicSource.Stop();
    }

    public void FadeOutMusic(float fadeTime = 2f)
    {
        StartCoroutine(FadeOutCoroutine(fadeTime));
    }

    IEnumerator FadeOutCoroutine(float fadeTime)
    {
        float startVol = musicSource.volume;

        while (musicSource.volume > 0)
        {
            musicSource.volume -= startVol * Time.deltaTime / fadeTime;
            yield return null;
        }

        musicSource.Stop();
        musicSource.volume = startVol;
    }
}
