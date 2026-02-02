using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.Video;

public class SceneChangeManager : MonoBehaviour
{
    public static SceneChangeManager Instance;   // ★ 반드시 필요

    [Header("Opening Video")]
    public VideoPlayer openingVideoPlayer;
    [Header("Ending Video")]
    public VideoPlayer endingVideoPlayer;

    public GameObject videoScreenUI;

    [Header("Scene Settings")]
    public string playSceneName = "PlayScene 1";
    public string titleSceneName = "TitleScene";

    [SerializeField]
    private GameObject ui;

    bool isPlaying = false;
    bool isEnding = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
            openingVideoPlayer.loopPointReached += OnOPVideoEnd;
        }

        if (endingVideoPlayer != null)
        {
            endingVideoPlayer.playOnAwake = false;
            endingVideoPlayer.Stop();
            endingVideoPlayer.loopPointReached += OnENDVideoEnd;
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
        if(isEnding) return;

        isEnding = true;
        if (videoScreenUI != null)
            videoScreenUI.SetActive(true);

        if (endingVideoPlayer != null)
        {
            endingVideoPlayer.time = 0;
            endingVideoPlayer.Play();
        }
        else
        {
            GameManager.Instance.ResetProgress();
            SceneManager.LoadScene(titleSceneName);
        }
    }

    void OnOPVideoEnd(VideoPlayer vp)
    {
        if (videoScreenUI != null)
            videoScreenUI.SetActive(false);
        SceneManager.LoadScene(playSceneName);
        isPlaying = false;
    }

    void OnENDVideoEnd(VideoPlayer vp)
    {
        if (videoScreenUI != null)
            videoScreenUI.SetActive(false);
        GameManager.Instance.ResetProgress();
        SceneManager.LoadScene(titleSceneName);
        isEnding = false;
    }

    void OnDestroy()
    {
        if (openingVideoPlayer != null)
            openingVideoPlayer.loopPointReached -= OnOPVideoEnd;
        if (endingVideoPlayer != null)
            endingVideoPlayer.loopPointReached -= OnENDVideoEnd;
    }
}
