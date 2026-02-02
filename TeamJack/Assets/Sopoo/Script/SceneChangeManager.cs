using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager Instance;   // ★ 반드시 필요

    [Header("Opening Video")]
    public VideoPlayer openingVideoPlayer;
    public GameObject videoScreenUI;

    [Header("Scene Settings")]
    public string playSceneName = "PlayScene 1";

    [SerializeField]
    private GameObject ui;

    bool isPlaying = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (videoScreenUI != null)
            videoScreenUI.SetActive(false);

        if (openingVideoPlayer != null)
        {
            openingVideoPlayer.playOnAwake = false;
            openingVideoPlayer.Stop();
            openingVideoPlayer.loopPointReached += OnVideoEnd;
        }
    }

    public void PlayWithOpening()
    {
        if (isPlaying) return;

        isPlaying = true;
        if (ui != null)
            ui.SetActive(false);

        if (videoScreenUI != null)
            videoScreenUI.SetActive(true);

        if (openingVideoPlayer != null)
        {
            openingVideoPlayer.time = 0;
            openingVideoPlayer.Play();
        }
        else
        {
            SceneManager.LoadScene(playSceneName);
        }
    }

    public void PlayWithEnding()
    {
        
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        SceneManager.LoadScene(playSceneName);
    }

    void OnDestroy()
    {
        if (openingVideoPlayer != null)
            openingVideoPlayer.loopPointReached -= OnVideoEnd;
    }
}
