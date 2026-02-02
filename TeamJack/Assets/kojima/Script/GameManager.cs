using DG.Tweening;
using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager s_Instance;
    public static GameManager Instance => s_Instance;

    // ゲームが初期化されているか
    public bool IsGameInitialized { get; set; }

    /// <summary>
    /// 進捗
    /// </summary>
    public enum Progresses
    {
        GetJoyMask,   // 喜仮面を取得
        GetAngryMask, // 怒仮面を取得
        GetSadMask,   // 哀仮面を取得
        GetFunMask,   // 楽仮面を取得
        End
    }
    public bool[] ProgressesBool { get; private set; }

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        IsGameInitialized = true;
        ProgressesBool = new bool[Enum.GetValues(typeof(Progresses)).Length];

        DOTween.SetTweensCapacity(500, 100);
    }

    private void Start()
    {
        
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 進捗達成
    /// </summary>
    public void ProgressAchievement(Progresses progresses)
    {
        ProgressesBool[(int)progresses] = true;
    }
}
